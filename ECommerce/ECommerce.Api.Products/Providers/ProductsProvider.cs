using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product()
                {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 950,
                    Inventory = 100

                }); 
                _context.Products.Add(new Product()
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 250,
                    Inventory = 200

                }); 
                _context.Products.Add(new Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 2000,
                    Inventory = 50

                }); 
                _context.Products.Add(new Product()
                {
                    Id = 4,
                    Name = "CPU",
                    Price = 1200,
                    Inventory = 300

                }); 

                _context.SaveChanges();
                
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> productModel, string ErrorMessage)> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                if(products != null && products.Any())
                {
                   var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

                    return(true,result,null);
                }
                return(false, null, "Not found.");
            }
            catch(Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductModel productModel, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Product, ProductModel>(product);

                    return (true, result, null);
                }
                return (false, null, "Not found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
