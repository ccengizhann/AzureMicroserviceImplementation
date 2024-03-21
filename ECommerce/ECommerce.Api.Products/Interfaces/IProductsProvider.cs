using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductModel> productModel, string ErrorMessage)> GetAllProductsAsync();
        Task<(bool IsSuccess, ProductModel productModel, string ErrorMessage)> GetProductAsync(int id);
    }
}
