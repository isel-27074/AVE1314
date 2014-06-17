--CREATE DATABASE AVE;

USE AVE;

CREATE TABLE Product (
	productID			SMALLINT			NOT NULL,
	productName			NVARCHAR(100)		NOT NULL,
	quantityPerUnit		NVARCHAR(10)		NOT NULL,
	UnitPrice			DECIMAL				NOT NULL,
	UnitsInStock		SMALLINT			NOT NULL,
	UnitsOnOrder		SMALLINT			NOT NULL,
	CONSTRAINT pk_productID PRIMARY KEY (productID));


