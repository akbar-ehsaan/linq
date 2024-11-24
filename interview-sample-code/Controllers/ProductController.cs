using interview_sample_code.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace interview_sample_code.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        List<Product> products;
        List<Order> orders;
        List<OrderLine> orderLines;
        public ProductController()
        {
            products = JsonSerializer.Deserialize<List<Product>>(@"[

{

""Id"": ""c7a1a5a0-eaf9-45a8-b53e-0dc003056ca1"",

""Price"": 100,

""Title"": ""Product 1""

},

{

""Id"": ""bfa2f79f-d5a4-4a71-9e8f-1b6e8f9e7154"",

""Price"": 200,

""Title"": ""Product 2""

},

{

""Id"": ""5c5fdd3b-76c4-42b6-91f3-d2b781c84a5c"",

""Price"": 300,

""Title"": ""Product 3""

}

] ");

            orders = JsonSerializer.Deserialize<List<Order>>(@"[

{

""Id"": ""a1f3b65b-7312-4e5a-89f7-1e4cddb1df6d"",

""Number"": ""ORD-001"",

""TotalPrice"": 500,

""Date"": ""2024-11-10T00:00:00""

},

{

""Id"": ""f2b8e16a-9132-4f65-8d79-81c169a77dbd"",

""Number"": ""ORD-002"",

""TotalPrice"": 800,

""Date"": ""2024-11-12T00:00:00""

},

{

""Id"": ""c6c8f7f4-16ef-4041-b87c-503b88fd45e5"",

""Number"": ""ORD-003"",

""TotalPrice"": 300,

""Date"": ""2024-11-15T00:00:00""}]");

            orderLines = JsonSerializer.Deserialize<List<OrderLine>>(@"[

{

""Id"": ""5a77edc9-28f3-40ba-859b-90f07a5c0897"",

""OrderId"": ""a1f3b65b-7312-4e5a-89f7-1e4cddb1df6d"",

""ProductId"": ""c7a1a5a0-eaf9-45a8-b53e-0dc003056ca1"",

""Quantity"": 2,

""Price"": 100

},

{

""Id"": ""f679b59d-8ed8-4042-9c96-c7cfad78a4e2"",

""OrderId"": ""f2b8e16a-9132-4f65-8d79-81c169a77dbd"",

""ProductId"": ""bfa2f79f-d5a4-4a71-9e8f-1b6e8f9e7154"",

""Quantity"": 3,

""Price"": 200

},

{

""Id"": ""f678b59d-8ed8-4042-9c96-c7cfad78a4e2"",

""OrderId"": ""f2b8e16a-9132-4f65-8d79-81c169a77dbd"",

""ProductId"": ""bfa2f79f-d5a4-4a71-9e8f-1b6e8f9e7154"",

""Quantity"": 9,

""Price"": 200

},

{

""Id"": ""9faabbe2-baf6-4df3-87c6-99dbedbc7296"",

""OrderId"": ""c6c8f7f4-16ef-4041-b87c-503b88fd45e5"",

""ProductId"": ""5c5fdd3b-76c4-42b6-91f3-d2b781c84a5c"",

""Quantity"": 1,

""Price"": 300

}

]");
        }
        [HttpGet("Report")]
        public  void GenerateReport()
        {
            var q = (from i in orders
                     join b in orderLines on i.Id equals b.OrderId
                     join p in products on b.ProductId equals p.Id

                     where i.Date>=DateTime.Now.Date.AddDays(-7)

                     select new
                     {
                         p.Id,
                         i.Date.Date,
                         b.Quantity
                     });


            var q2 = q.GroupBy(x => new { x.Id, x.Date }).Select(t => new
            {
                p = t.Key.Id,
                date = t.Key.Date,
                count = t.Sum(x=>x.Quantity)

            }).ToList();
                  
                  
        }

    }
}
