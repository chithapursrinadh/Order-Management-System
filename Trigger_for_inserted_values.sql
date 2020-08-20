Use order_management
Go
alter trigger InsertOrderStatus on dbo.Orders
after insert
as
begin
declare @newId int
set @newId=(select orderId from inserted)
if  exists(select * from inserted where OrderId=@newId)
begin
update dbo.Orders set orderStatuscode='Placed' where OrderId=@newId
end
end