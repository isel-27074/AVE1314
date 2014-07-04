USE AVE;

/*
[Table] Products
[int] IDENTITY(1,1)
[ProductID][ProductName][QuantityPerUnit][UnitPrice][UnitsInStock][UnitsOnOrder]*/
INSERT INTO Products VALUES ('Batata', '5', 5, 25, 15);
INSERT INTO Products VALUES ('Cenoura', '1', 1, 9, 3);
INSERT INTO Products VALUES ('Couve', '1', 2, 29, 0);
INSERT INTO Products VALUES ('Cebola', '6', 1, 17, 4);
INSERT INTO Products VALUES ('Alho', '12', 2, 12, 2);
INSERT INTO Products VALUES ('Abóbora', '1', 4, 5, 2);

/*
[Table] Customers
[String] IDENTITY(1,1)
[CustomerID][CompanyName][ContactName][ContactTitle][Address][City][Region][PostalCode][Country][Phone][Fax]*/
INSERT INTO Customers VALUES ('1','Company1', 'Contact1', 'Mr' ,'Rua xxx' ,'Porto' ,' North' ,'4950 ' ,' Portugal' ,' 91123456' ,'2151421 ' );
INSERT INTO Customers VALUES ('2','Company2', 'Contact2', 'Mrs' ,'Rua yy' ,'Porto' ,' North' ,'4950 ' ,' Portugal' ,' 91123456' ,'2151421 ' );
INSERT INTO Customers VALUES ('3','Company3', 'Contact3', 'Ms' ,'Rua qq' ,'Porto' ,' North' ,'4950 ' ,' Portugal' ,' 91123456' ,'2151421 ' );
INSERT INTO Customers VALUES ('4','Company4', 'Contact4', 'Mr' ,'Rua www' ,'Lisbon' ,' Center' ,'4950 ' ,' Portugal' ,' 91123456' ,'2151421 ' );
INSERT INTO Customers VALUES ('5','Company5', 'Contact5', 'Mrs' ,'Rua eee' ,'Sevilha' ,' Center' ,'4950 ' ,' Spain' ,' 91123456' ,'2151421 ' );
INSERT INTO Customers VALUES ('6','Company6', 'Contact6', 'Ms' ,'Rua rrrr' ,'Sevilha' ,' Center' ,'4950 ' ,' Spain' ,' 91123456' ,'2151421 ' );

/*
[Table] Employees
[int] IDENTITY(1,1)																																			  [int] NULL
[EmployeeID][LastName][FirstName][Title][TitleOfCourtesy][BirthDate][HireDate][Address][City][Region][PostalCode][Country][HomePhone][Extension][Photo][Notes][ReportsTo][PhotoPath]*/
INSERT INTO Employees VALUES ('Rodrigues', 'Tatiana', 'Ms', 'Ms', '1900-01-01 12:10:05.123', '1900-01-01 12:10:05.123', 'Rua xxx', 'Porto', 'North', '3521','Portugal','12387643', '521',' ',' ',NULL,' ');
INSERT INTO Employees VALUES ('Sevilha', 'Teste', 'Mr', 'Mr', '1900-01-01 12:10:05.123', '1900-01-01 12:10:05.123', 'Rua yyy', 'Braga', 'North', '3521','Portugal','12387643', '521',' ',' ',NULL,' ');
INSERT INTO Employees VALUES ('Geraldes', 'Guida', 'Ms', 'Ms', '1900-01-01 12:10:05.123', '1900-01-01 12:10:05.123', 'Rua qqq', 'Lisbon', 'Center', '3521','Portugal','12387643', '521',' ',' ',NULL,' ');
INSERT INTO Employees VALUES ('Botas', 'Zé', 'Mr', 'Mr', '1900-01-01 12:10:05.123', '1900-01-01 12:10:05.123', 'Rua www', 'Faro', 'South', '3521','Portugal','12387643', '521',' ',' ',NULL,' ');
INSERT INTO Employees VALUES ('Domingos', 'Diana', 'Ms', 'Ms', '1900-01-01 12:10:05.123', '1900-01-01 12:10:05.123', 'Rua eee', 'Porto', 'North', '3521','Portugal','12387643', '521',' ',' ',NULL,' ');
/*
[Table] Orders
[int] IDENTITY(1,1)
[OrderID][CustomerID][EmployeeID][OrderDate][RequiredDate][ShippedDate][ShipVia][Freight][ShipName][ShipAddress][ShipCity][ShipRegion][ShipPostalCode][ShipCountry]*/
INSERT INTO Orders VALUES (1,1,'1900-01-01 12:10:05.123','1900-01-01 12:10:05.123','1900-01-01 12:10:05.123',2,22,'Narvas','Rua yyy', 'Porto', 'North', '1234', 'Portugal');
INSERT INTO Orders VALUES (2,2,'1900-01-01 12:10:05.123','1900-01-01 12:10:05.123','1900-01-01 12:10:05.123',1,33,'Chronopost','Rua xxx', 'Braga', 'North', '1234', 'Portugal');
INSERT INTO Orders VALUES (3,3,'1900-01-01 12:10:05.123','1900-01-01 12:10:05.123','1900-01-01 12:10:05.123',0,44,'UPS','Rua qqq', 'Lisbon', 'Center', '1234', 'Portugal');
INSERT INTO Orders VALUES (4,4,'1900-01-01 12:10:05.123','1900-01-01 12:10:05.123','1900-01-01 12:10:05.123',3,55,'Chronopost','Rua www', 'Faro', 'South', '1234', 'Portugal');
INSERT INTO Orders VALUES (5,5,'1900-01-01 12:10:05.123','1900-01-01 12:10:05.123','1900-01-01 12:10:05.123',4,66,'UPS','Rua eee', 'Beja', 'South', '1234', 'Portugal');
