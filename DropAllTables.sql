-- Works only on SQL Server 2016

DROP TABLE IF EXISTS dbo.__MigrationHistory
DROP TABLE IF EXISTS dbo.AspNetUserRoles;
DROP TABLE IF EXISTS dbo.AspNetUserClaims;
DROP TABLE IF EXISTS dbo.AspNetUserLogins;
DROP TABLE IF EXISTS dbo.AspNetRoles;
ALTER TABLE dbo.AspNetUsers DROP CONSTRAINT store_id;
DROP TABLE IF EXISTS dbo.AspNetUsers;
ALTER TABLE dbo.HotDogStores DROP CONSTRAINT HotDogCustomer_id;
DROP TABLE IF EXISTS dbo.HotDogStores;
DROP TABLE IF EXISTS dbo.HotDogs;