using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
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
