using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Category.API.Models;
using Category.Domain;

namespace Category.API.Controllers
{
    [Authorize]
    public class CategoryModelsController : ApiController
    {

        private DataContext db = new DataContext();

        // GET: api/CategoryModels
        public async Task<IHttpActionResult> GetCategoryModels()
        {
            var categories= await db.CategoryModels.ToListAsync();
            var categoriesResponse = new List<CategoryResponse>();

            foreach (var category in categories)
            {
                var productsResponse = new List<ProductResponse>();
                foreach (var producto in category.Products)
                {
                    productsResponse.Add(new ProductResponse
                    {
                        CategoryId = producto.CategoryId,
                        ProductId =producto.ProductId,
                        Image=producto.Image,
                        IsActive=producto.IsActive,
                        LastPurchase= producto.LastPurchase,
                        Price=producto.Price,
                        Description=producto.Description,
                        Remarks=producto.Remarks,
                        Stock=producto.Stock,

                    });
                }

                categoriesResponse.Add(new CategoryResponse
                {
                        CategoryId = category.CategoryId,
                        Description=category.Description,
                        Products = productsResponse,

                });
            }

            return Ok(categoriesResponse);
        }

        // GET: api/CategoryModels/5
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> GetCategoryModel(int id)
        {
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }

        // PUT: api/CategoryModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategoryModel(int id, CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryModel.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("Ya existe una categoria con esa descripcion");


                }

                else
                {
                    return BadRequest(ex.Message);


                }

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CategoryModels
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> PostCategoryModel(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryModels.Add(categoryModel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null && ex.InnerException.InnerException!=null && ex.InnerException.InnerException.Message.Contains("Index")) 
                {
                    return BadRequest("Ya existe una categoria con esa descripcion");


                }

                else
                {
                    return BadRequest(ex.Message);


                }

            }
           

            return CreatedAtRoute("DefaultApi", new { id = categoryModel.CategoryId }, categoryModel);
        }

        // DELETE: api/CategoryModels/5
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> DeleteCategoryModel(int id)
        {
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            db.CategoryModels.Remove(categoryModel);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    return BadRequest("No puedes borrar esta categoria porque contiene productos relacionados");


                }

                else
                {
                    return BadRequest(ex.Message);


                }

            }

            return Ok(categoryModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryModelExists(int id)
        {
            return db.CategoryModels.Count(e => e.CategoryId == id) > 0;
        }
    }
}