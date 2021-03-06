using System;
using System.Linq;
using WebStore.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.ViewModels;
using WebStore.Infrastructure.Mapping;

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
                .ToView()
            });
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product.ToView());
        }
    }
}
