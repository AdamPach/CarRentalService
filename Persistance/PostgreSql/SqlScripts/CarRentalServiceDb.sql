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
    "PersonId" UUID PRIMARY KEY NOT NULL,
    "RegistrationDate" TIMESTAMP NOT NULL,
    "LicenseNumber" VARCHAR(50) NOT NULL,
    CONSTRAINT "FK_Customer_Person" FOREIGN KEY ("PersonId") REFERENCES "Person"("Id")
);