using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");
        
    }
}
