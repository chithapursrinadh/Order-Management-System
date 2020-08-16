using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        OrdersModelEntities dbContext;
        public OrdersController()
        {
           dbContext= new OrdersModelEntities();
        }
        [HttpGet]
        public HttpResponseMessage GetOrders(int userRoleId,int id=1)
        {
            try
            {
                if(userRoleId <= 2)
                {
                    var model = dbContext.getOrderDetails(id, userRoleId);
                    var i = model.OrderByDescending(x => x.Order_Id).Select(x => x.Order_Id);
                    if (Convert.ToInt32(i)<=id)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,model );
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Given id=" + id + " id not found");
                    }
                  //  return Request.CreateResponse(HttpStatusCode.OK, model);
                }
               
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Given userRoleId=" + userRoleId + " is not exist");
                }
            
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage InsertOrder([FromBody]Order order)
        {
            try
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                ResponseMail.SendMail(order.OrderId);
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }
        [HttpPut]
        public HttpResponseMessage UpdateOrder(int id,[FromBody]Order order)
        {
            try
            {
              var model=  dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
                if(model != null)
                {
                    model.ShippingAddress = order.ShippingAddress;
                    model.ProductId = order.ProductId;
                    model.orderStatuscode = order.orderStatuscode;
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Given id=" + id + " is not the record");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteOrder(int id)
        {
            try
            {
               var order= dbContext.Orders.Find(id);
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
