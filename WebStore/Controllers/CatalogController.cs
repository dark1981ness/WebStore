using System;
using System.Linq;
using WebStore.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Index(int? BrandId, int? SectionId)
        {
            var filters = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = _productData.GetProducts(filters);
            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                .OrderBy(p => p.Order)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
            });
        }
    }
}
