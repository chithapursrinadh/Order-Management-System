use order_management
go
Create table Products(
ProductId int identity(1,1) primary key,
PName varchar(50),weight int,height int,photo image,sku int,barcode varchar(20),AvailQuantities int,RequiredQuant int)

create table Orders(
OrderId int identity(1,1) primary key,
ShippingAddress varchar(100),
orderStatuscode varchar(20),
ProductId int foreign key references Products(ProductId)on update cascade on delete cascade)



select * from Products
select * from Orders



Create table UserRoles(
UserRoleId int,
UserRoleName varchar(20))

insert UserRoles values(1,'User'),(2,'Admin')
