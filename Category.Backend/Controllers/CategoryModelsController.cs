

namespace Category.Backend.Controllers
{
    using Category.Backend.Models;
    using Category.Domain;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    //[Authorize(Roles ="Admin")]

    [Authorize]
    public class CategoryModelsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: CategoryModels
        public async Task<ActionResult> Index()
        {
            return View(await db.CategoryModels.ToListAsync());
        }

        // GET: CategoryModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // GET: CategoryModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,Description")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                db.CategoryModels.Add(categoryModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        // GET: CategoryModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // POST: CategoryModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,Description")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        // GET: CategoryModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // POST: CategoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoryModel categoryModel = await db.CategoryModels.FindAsync(id);
            db.CategoryModels.Remove(categoryModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
