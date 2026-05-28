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
            var response = await _http.GetAsync($"orderItems/customer/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error, null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<List<OrderItems>>() ?? new List<OrderItems>();
        }

        public async Task<OrderItems?> GetOrderById(int id)
        {
            var response = await _http.GetAsync($"orderItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error, null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<OrderItems>();
        }
    }
}