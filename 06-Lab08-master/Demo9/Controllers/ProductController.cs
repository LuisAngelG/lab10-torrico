using Business;
using Data;
using Demo9.Models;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo9.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            BProduct product = new BProduct();
            List<Product> products = product.GetProductsByName("");
            List<ProductModel> models = new List<ProductModel>();

            models=products.Select(x=> new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Stock = x.Stock
            }).ToList();
            
            return View(models);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            try
            {
                BProduct bProduct = new BProduct();
                Product product = new Product
                {
                    Name = model.Name,
                    Stock = model.Stock
                };

                bProduct.Create(product);

                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                // Maneja cualquier excepción aquí si es necesario.
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {

            BProduct bProduct = new BProduct();
            Product product = bProduct.GetById(id);
            ProductModel model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock
            };

            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductModel model)
        {
            try
            {
                BProduct bproduct = new BProduct();
                Product product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Stock = model.Stock
                };

                bproduct.Update(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
