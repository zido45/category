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
using Category.Domain;

namespace Category.API.Controllers
{
    [Authorize]
    public class CategoryModelsController : ApiController
    {

        private DataContext db = new DataContext();

        // GET: api/CategoryModels
        public IQueryable<CategoryModel> GetCategoryModels()
        {
            return db.CategoryModels;
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
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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
            await db.SaveChangesAsync();

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
            await db.SaveChangesAsync();

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