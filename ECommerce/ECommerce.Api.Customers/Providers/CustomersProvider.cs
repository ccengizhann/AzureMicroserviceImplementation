using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _context;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext context, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedCustomerData();
        }

        private void SeedCustomerData()
        {
           if(!_context.Customers.Any()) 
            {
                _context.Customers.Add(new Customer()
                {
                    Id = 1,
                    Name = "John Doe",
                    Address = "123 Main Street"
                });

                _context.Customers.Add(new Customer()
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Address = "456 Elm Street"
                });

                _context.Customers.Add(new Customer()
                {
                    Id = 3,
                    Name = "Michael Johnson",
                    Address = "789 Oak Street"
                });

                _context.SaveChanges();

            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> customerModels, string ErrorMessage)> GetAllCustomerAsync()
        {
            try
            {
                var customer = await _context.Customers.ToListAsync();

                if(customer != null && customer.Any()) 
                {
                    var result = _mapper.Map<IEnumerable<Customer>,IEnumerable<CustomerModel>>(customer);

                    return (true, result, null);
                }

                return(false, null, "Not found");

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, CustomerModel customerModels, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customer != null)
                {
                    var result = _mapper.Map<Customer, CustomerModel>(customer);

                    return (true, result, null);
                }

                return (false, null, "Not found");

            }
           
             catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
