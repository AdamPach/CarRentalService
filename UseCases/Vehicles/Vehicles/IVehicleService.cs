﻿using CarRentalService.UseCases.Vehicles.Vehicles.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Vehicles;

public interface IVehicleService
{
    Task<Result<IEnumerable<VehiclePreviewDto>>> GetAllVehiclesPreviewsAsync();
}