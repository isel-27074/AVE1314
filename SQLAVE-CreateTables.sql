IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = 'AVE' OR name = 'AVE')))
	CREATE DATABASE AVE;
GO

USE AVE;

if OBJECT_ID('Products') is not null
	drop table [Products];

CREATE TABLE Products (
	ProductID			SMALLINT			NOT NULL,
	ProductName			NVARCHAR(100)		NOT NULL,
	QuantityPerUnit		NVARCHAR(10)		NOT NULL,
	UnitPrice			DECIMAL				NOT NULL,
	UnitsInStock		SMALLINT			NOT NULL,
	UnitsOnOrder		SMALLINT			NOT NULL,
	CONSTRAINT pk_productID PRIMARY KEY (productID));


