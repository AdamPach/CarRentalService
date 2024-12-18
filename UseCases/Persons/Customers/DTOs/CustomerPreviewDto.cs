﻿namespace CarRentalService.UseCases.Persons.Customers.DTOs;

public class CustomerPreviewDto
{
    public Guid Id { get; set; }
    
    public required string FullName { get; set; }
    
    public required string LicenseNumber { get; set; }
    
    public required string PhoneNumber { get; set; }
    
    public required DateTime RegistrationDate { get; set; }
}