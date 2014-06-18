IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = 'AVE' OR name = 'AVE')))
	CREATE DATABASE AVE;
GO

USE AVE;

if OBJECT_ID('Product') is not null
	drop table [Product];


