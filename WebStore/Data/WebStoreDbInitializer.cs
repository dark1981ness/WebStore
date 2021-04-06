using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД...");

            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Выполнение миграции БД...");
                _db.Database.Migrate();
                _logger.LogInformation("Выполнение миграции БД выполнено успешно");
            }

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ошибка при инициализации товаров в БД");
                throw;
            }

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private void InitializeProducts()
        {

            if (_db.Products.Any())
            {
                _logger.LogInformation("Инициализация товаров не нужна");
                return;
            }

            _logger.LogInformation("Инициализация секций...");

            var products_sections = TestData.Sections.Join(
                TestData.Products,
                section => section.Id,
                product => product.SectionId,
                (section, product) => (section, product));

            foreach (var (section, product) in products_sections)
                section.Products.Add(product);

            var products_brands = TestData.Brands.Join(
                TestData.Products,
                brand => brand.Id,
                product => product.BrandId,
                (brand, product) => (brand, product));

            foreach (var (brand, product) in products_brands)
                brand.Products.Add(product);

            var section_section = TestData.Sections.Join(
                TestData.Sections,
                parent => parent.Id,
                child => child.ParentId,
                (parent,child)=>(parent,child));

            foreach (var (parent,child) in section_section)
                child.Parent = parent;

            foreach (var product in TestData.Products)
            {
                product.Id = 0;
                product.BrandId = null;
                product.SectionId = 0;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;

            foreach (var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация товаров завершена");
        }
    }
}
