using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomersSevice
    {      
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}
