using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var result = await _productsProvider.GetAllProductsAsync();
            if(result.IsSuccess)
                return Ok(result.productModel);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
                return Ok(result.productModel);

            return NotFound();
        }

    }
}
