using Microsoft.AspNetCore.Mvc;
using MusicStore.Models;
using Newtonsoft.Json;

namespace MusicStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Configure the base address of your API
            //_httpClient.BaseAddress = new Uri("http://localhost:7077/api/");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                using (HttpResponseMessage response = await _httpClient.GetAsync("api/products/Get"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle the successful response here
                        string data = await response.Content.ReadAsStringAsync();
                        // Process the data as needed

                        List<ProductViewModel> products = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

                        // Redirect to another view or return a specific view based on the data
                        return View("Index", products);
                    }
                    else
                    {
                        // Handle the error response here
                        ViewBag.ErrorMessage = $"Error: {response.StatusCode}";
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }

        }
    }
}
