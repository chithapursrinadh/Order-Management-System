alter proc getOrderDetails(@id int=1,@userRole int)
as
begin
if exists(select * from Orders where OrderId=@id  )
begin
	if(@userRole=1)--user
	begin
	select o.OrderId [Order Id],P.PName [Product Name],o.ShippingAddress [Shipping Address],o.orderStatuscode [Status Code],p.RequiredQuant [Required Quantities]
	from orders o
	left join Products p on o.ProductId=p.ProductId
	where o.ProductId=@id
	print 'user'
	end

	else  --Admin
	begin
	select  o.OrderId [Order Id],P.PName [Product Name],o.ShippingAddress [Shipping Address],o.orderStatuscode [Status Code],p.RequiredQuant [Required Quantities]
	from orders o
	left join Products p on o.ProductId=p.ProductId
	print 'admin'
	end	
end
end

--exec getOrderDetails 2,2