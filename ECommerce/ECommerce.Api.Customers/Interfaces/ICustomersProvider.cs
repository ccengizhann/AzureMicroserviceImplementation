using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerModel> customerModels, string ErrorMessage)> GetAllCustomerAsync();
        Task<(bool IsSuccess, CustomerModel customerModels, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
