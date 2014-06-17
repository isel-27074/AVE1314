IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = 'AVE' OR name = 'AVE')))
	CREATE DATABASE AVE;
GO

USE AVE;

if OBJECT_ID('Product') is not null
	drop table [Product];

CREATE TABLE Product (
	productID			SMALLINT			NOT NULL,
	productName			NVARCHAR(100)		NOT NULL,
	quantityPerUnit		NVARCHAR(10)		NOT NULL,
	UnitPrice			DECIMAL				NOT NULL,
	UnitsInStock		SMALLINT			NOT NULL,
	UnitsOnOrder		SMALLINT			NOT NULL,
	CONSTRAINT pk_productID PRIMARY KEY (productID));


