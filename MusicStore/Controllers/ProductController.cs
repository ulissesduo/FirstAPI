using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Models;
using Newtonsoft.Json;
using System.Text;

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


        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel newProduct)
        {
            try
            {
                // Convert your ProductViewModel to the AddProductRequestDto if needed
                var addProductRequestDto = new AddProductRequestDto
                {
                    // Map properties from ProductViewModel to AddProductRequestDto
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Price = newProduct.Price,
                    // Map other properties as needed
                };

                // Serialize the object to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(addProductRequestDto), Encoding.UTF8, "application/json");

                // Send a POST request to the API
                using (HttpResponseMessage response = await _httpClient.PostAsync("api/products/Create", jsonContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle the successful response here
                        string responseData = await response.Content.ReadAsStringAsync();
                        // Deserialize the response if needed

                        // Redirect to another view or return a specific view based on the data
                        return RedirectToAction("Index");
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
