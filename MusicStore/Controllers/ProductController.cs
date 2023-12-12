using Microsoft.AspNetCore.Mvc;
using MusicStore.Models;
using Newtonsoft.Json;

namespace MusicStore.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:7077/api");
        private readonly HttpClient _httpClient;
        public ProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        public  IActionResult Index()        
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync("/products/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response here, e.g., read content
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            else
            {
                // Handle the error response here
                // For example, you can set an error message and return an error view
                ViewBag.ErrorMessage = "Error fetching products";
                return View("Error");
            }

            return View(productList);
        }

    }
}
