CREATE DATABASE bioprinz;
GO

USE biopranz;
GO

CREATE TABLE comenzi (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nrcomanda NVARCHAR(50) NOT NULL,
    datacomanda DATE NOT NULL,
    adresaclient NVARCHAR(250) NOT NULL,
    felintai NVARCHAR(50) NOT NULL,
    feldoi NVARCHAR(50) NOT NULL,
    desert NVARCHAR(150) NOT NULL
);
GO