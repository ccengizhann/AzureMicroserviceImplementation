using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderModel> orderModels, string ErrorMessage)> GetOrdersAsync(int customerId);

    }
}
