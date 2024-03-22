using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;

        private readonly IProductsService _productsService;

        private readonly ICustomersSevice _customersSevice;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersSevice customersSevice)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersSevice = customersSevice;
        }

        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductAsync();
            var customersResult = await _customersSevice.GetCustomerAsync(customerId);

            if (ordersResult.IsSuccess)
            {

                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.Id)?.Name :
                            "Product information is not available";
                    }
                }
                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" },
                    Orders = ordersResult.Orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
