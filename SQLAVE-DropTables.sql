IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = 'AVE' OR name = 'AVE')))
	CREATE DATABASE AVE;
GO

USE AVE;

IF OBJECT_ID('Orders') IS NOT NULL
	/*ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Shippers]*/
	ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Employees]
	ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Customers]
	ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [DF_Orders_Freight]
	DROP TABLE [dbo].[Orders]
GO

IF OBJECT_ID('Employees') IS NOT NULL
	ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [CK_Birthdate]
	ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Employees]
	DROP TABLE [dbo].[Employees]
GO

IF OBJECT_ID('Customers') IS NOT NULL
	DROP TABLE [dbo].[Customers]
GO

IF OBJECT_ID('Products') IS NOT NULL
	DROP TABLE [dbo].[Products];
GO
