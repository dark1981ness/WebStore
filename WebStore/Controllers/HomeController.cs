using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Models;
using WebStore.Infrastructure.Conventions;

namespace WebStore.Controllers
{
    [ActionDescription("Главный контроллер")]
    
    public class HomeController : Controller
    {
        [ActionDescription("Главное действие")]
        public IActionResult Index() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

        public IActionResult ContactUs() => View();

        public IActionResult PageNotFound() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Blog() => View();

        public IActionResult Cart() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();

        public IActionResult Checkout() => View();
        
    }
}
