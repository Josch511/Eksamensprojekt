using Core;
using System.Net.Http.Json;


namespace WebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<OrderItems>> GetCustomerOrders(int userId)
        {
            var orders = await _http.GetFromJsonAsync<List<OrderItems>>
                ($"orderItems/customer/{userId}");

            return orders ?? new List<OrderItems>();
        }
    }
}