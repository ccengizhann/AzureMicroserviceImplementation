using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetAllProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.productModel.Any());
            Assert.Null(product.ErrorMessage);


        }

        [Fact]
        public async Task GetProductReturnsProductsUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductsUsingValidId))
                .Options;

            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.productModel);
            Assert.True(product.productModel.Id == 1);
            Assert.Null(product.ErrorMessage);


        }

        [Fact]
        public async Task GetProductReturnsProductsUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductsUsingInValidId))
                .Options;

            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.productModel);
            Assert.NotNull(product.ErrorMessage);


        }




        private void CreateProducts(ProductsDbContext dbContext)
        {
           for(int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price= (decimal)(i *3.14)
                }); 

                dbContext.SaveChanges();
            }
        }
    }
}