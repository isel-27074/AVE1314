USE AVE;

/*[int] IDENTITY(1,1)
[ProductID][ProductName][QuantityPerUnit][UnitPrice][UnitsInStock][UnitsOnOrder]*/
INSERT INTO Products VALUES ('Batata', '5', 5, 25, 15);
INSERT INTO Products VALUES ('Cenoura', '1', 1, 9, 3);
INSERT INTO Products VALUES ('Couve', '1', 2, 29, 0);
INSERT INTO Products VALUES ('Cebola', '6', 1, 17, 4);
INSERT INTO Products VALUES ('Alho', '12', 2, 12, 2);
INSERT INTO Products VALUES ('Abóbora', '1', 4, 5, 2);
/*[nchar](5)
[CustomerID][CompanyName][ContactName][ContactTitle][Address][City][Region][PostalCode][Country][Phone][Fax]
INSERT INTO Customers VALUES (
*/


/*[int] IDENTITY(1,1)
[EmployeeID][LastName][FirstName][Title][TitleOfCourtesy][BirthDate][HireDate][Address][City][Region][PostalCode]-
											  [int] NULL
-[Country][HomePhone][Extension][Photo][Notes][ReportsTo][PhotoPath]
INSERT INTO Employees VALUES (
*/


/*
[int] IDENTITY(1,1) - [nchar](5) NULL - [int] NULL
[OrderID]			- [CustomerID]	  - [EmployeeID][OrderDate][RequiredDate][ShippedDate][ShipVia][Freight]-
-[ShipName][ShipAddress][ShipCity][ShipRegion][ShipPostalCode][ShipCountry]
INSERT INTO Orders VALUES (
*/
