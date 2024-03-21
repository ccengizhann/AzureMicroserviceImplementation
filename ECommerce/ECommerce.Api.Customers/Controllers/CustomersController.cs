using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider _provider;

        public CustomersController(ICustomersProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            var result = await _provider.GetAllCustomerAsync();

            if(result.IsSuccess) 
                return Ok(result.customerModels);

            return NotFound();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _provider.GetCustomerAsync(id);

            if (result.IsSuccess)
                return Ok(result.customerModels);
            return NotFound();
        }
    }
}
