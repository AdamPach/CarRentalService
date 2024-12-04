CREATE TABLE IF NOT EXISTS "Person" (
    "Id" UUID PRIMARY KEY,
    "FirstName" VARCHAR(50) NOT NULL,
    "LastName" VARCHAR(50) NOT NULL,
    "DateOfBirth" TIMESTAMP NOT NULL,
    "Email" VARCHAR(255),
    "PhoneNumber" VARCHAR(50) NOT NULL,
    "Street" VARCHAR(255) NOT NULL,
    "City" VARCHAR(50) NOT NULL,
    "ZipCode" VARCHAR(10) NOT NULL
);

CREATE TABLE IF NOT EXISTS "Customer" (
    "Id" UUID PRIMARY KEY NOT NULL,
    "RegistrationDate" TIMESTAMP NOT NULL,
    "LicenseNumber" VARCHAR(50) NOT NULL,
    CONSTRAINT "FK_Customer_Person" FOREIGN KEY ("Id") REFERENCES "Person"("Id")
);

CREATE TABLE IF NOT EXISTS "Employee" (
    "Id" UUID PRIMARY KEY NOT NULL,
    "EmployeeNumber" VARCHAR(50) NOT NULL,
    "HireDate" TIMESTAMP NOT NULL,
    "TerminationDate" TIMESTAMP,
    "Salary" DECIMAL NOT NULL,
    "Password" VARCHAR(255),
    "EmployeeType" INT NOT NULL 
);

CREATE TABLE IF NOT EXISTS "Vehicle" (
    "Id" UUID PRIMARY KEY NOT NULL,
    "BrandName" VARCHAR(50) NOT NULL,
    "DateOfManufacture" TIMESTAMP NOT NULL,
    "LicensePlate" VARCHAR(50) NOT NULL,
    "Color" VARCHAR(50),
    "PricePerDay" DECIMAL NOT NULL,
    "EngineType" INT NOT NULL,
    "Seats" INT NOT NULL,
    "Model" VARCHAR(50) NOT NULL,
    "VehicleType" INT NOT NULL,
    "EngineDisplacement" INT,
    "HelmetStorage" BOOLEAN,
    "TrunkSize" INT,
    "Doors" INT,
    CONSTRAINT "CK_Is_Car" CHECK (
        ("VehicleType" = 0 AND 
        "EngineDisplacement" IS NULL AND
        "HelmetStorage" IS NULL AND
        "TrunkSize" IS NOT NULL AND
        "Doors" IS NOT NULL) OR 
        ("VehicleType" = 1 AND
         "EngineDisplacement" IS NOT NULL AND
         "HelmetStorage" IS NOT NULL AND
         "TrunkSize" IS NULL AND
         "Doors" IS NULL))
);