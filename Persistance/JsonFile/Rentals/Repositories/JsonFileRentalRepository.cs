using System.Security.AccessControl;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.Persistence.JsonFile.Rentals.Mappers;
using CarRentalService.UseCases.Rentals.Repositories;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Rentals.Repositories;

internal sealed class JsonFileRentalRepository : IRentalRepository
{
    private readonly RentalCriteriaToFilterTypeMapper _rentalCriteriaToFilterTypeMapper;
    
    private readonly JsonFileManager<Rental> _rentalJsonFileManager;
    private readonly JsonFileManager<Customer> _customerJsonFileManager;
    private readonly JsonFileManager<Car> _carJsonFileManager;
    private readonly JsonFileManager<Motorbike> _motorbikeJsonFileManager;

    public JsonFileRentalRepository(
        RentalCriteriaToFilterTypeMapper rentalCriteriaToFilterTypeMapper,
        JsonFileManager<Rental> rentalJsonFileManager,
        JsonFileManager<Customer> customerJsonFileManager,
        JsonFileManager<Car> carJsonFileManager,
        JsonFileManager<Motorbike> motorbikeJsonFileManager)
    {
        _rentalCriteriaToFilterTypeMapper = rentalCriteriaToFilterTypeMapper;
        _rentalJsonFileManager = rentalJsonFileManager;
        _customerJsonFileManager = customerJsonFileManager;
        _carJsonFileManager = carJsonFileManager;
        _motorbikeJsonFileManager = motorbikeJsonFileManager;
    }

    public async Task<Result<IEnumerable<Rental>>> GetAllWithCustomerAndVehicleAsync(RentalCriteria criteria)
    {
        var rentalsResult = await _rentalJsonFileManager.ReadFromFileAsync();
        
        if (rentalsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Rental>>(rentalsResult.Errors);
        }
        
        var rentals = rentalsResult.Value.ToList();
        
        var customersResult = await _customerJsonFileManager.ReadFromFileAsync();
        
        if (customersResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Rental>>(customersResult.Errors);
        }
        
        var customers = customersResult.Value.ToList();
        
        var carsResult = await _carJsonFileManager.ReadFromFileAsync();
        var motorbikesResult = await _motorbikeJsonFileManager.ReadFromFileAsync();
        
        if (carsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Rental>>(carsResult.Errors);
        }
        
        if (motorbikesResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Rental>>(motorbikesResult.Errors);
        }
        
        var vehicles = new List<Vehicle>();
        
        vehicles.AddRange(carsResult.Value);
        vehicles.AddRange(motorbikesResult.Value);
        
        foreach (var rental in rentals)
        {
            rental.Customer = customers.Single(c => c.Id == rental.CustomerId);
            rental.Vehicle = vehicles.Single(v => v.Id == rental.VehicleId);
        }
        
        var filterType = _rentalCriteriaToFilterTypeMapper.Map(criteria).Value;
        return Result.Ok(filterType.Apply(rentals.AsQueryable()));
    }

    public async Task<Result<Rental>> GetByIdWithCustomerAndVehicleAsync(Guid rentalId)
    {
        var result = await GetAllWithCustomerAndVehicleAsync(new RentalCriteria { RentalId = rentalId });
        
        if (result.IsFailed)
        {
            return Result.Fail<Rental>(result.Errors);
        }
        
        var rentals = result.Value.ToList();
        
        return rentals.Count switch
        {
            0 => Result.Fail<Rental>("Rental not found"),
            1 => Result.Ok(rentals.Single()),
            _ => Result.Fail<Rental>("Multiple rentals found")
        };
    }

    public async Task<Result<Rental>> CreateRentalAsync(Rental rental)
    {
        rental.Id = Guid.NewGuid();
        
        var result = await _rentalJsonFileManager.ReadFromFileAsync();
        
        if (result.IsFailed)
        {
            return Result.Fail<Rental>(result.Errors);
        }
        
        var rentals = result.Value.ToList();
        
        rentals.Add(rental);
        
        return await _rentalJsonFileManager.WriteToFileAsync(rentals);
    }

    public async Task<Result> UpdateRentalAsync(Rental rental)
    {
        var result = await _rentalJsonFileManager.ReadFromFileAsync();
        
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }
        
        var rentals = result.Value.ToList();

        rentals = rentals.Where(r => r.Id != rental.Id)
            .ToList();
        
        rentals.Add(rental);
        
        return await _rentalJsonFileManager.WriteToFileAsync(rentals);
    }
}