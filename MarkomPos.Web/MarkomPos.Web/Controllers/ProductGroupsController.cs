using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using MarkomPos.Repository;
using MarkomPos.Repository.Repository;

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class ProductGroupsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            using (var productGroupRepository = new ProductGroupRepository())
            {
                var productGroups = productGroupRepository.GetAll();
                return View(productGroups.ToList());
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var productGroupRepository = new ProductGroupRepository())
            {
                var productGroup = productGroupRepository.GetById(id.GetValueOrDefault(0));
                if (productGroup == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", productGroup);
            }
        }

        public ActionResult Create()
        {
            using (var productGroupRepository = new ProductGroupRepository())
            {
                var productGroup = new ProductGroupVm();
                productGroup.productGroupVms = productGroupRepository.GetSelectListItems();
                return PartialView("_AddProductGroup", productGroup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductGroupVm productGroup)
        {
            if (ModelState.IsValid)
            {
                using (var productGroupRepository = new ProductGroupRepository())
                {
                    var result = productGroupRepository.AddUpdateProductGroups(productGroup);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var productGroupRepository = new ProductGroupRepository())
            {
                var productGroup = productGroupRepository.GetEditRecord(id.GetValueOrDefault(0));
                if (productGroup == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddProductGroup", productGroup);
            }
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            db.ProductGroups.Remove(productGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetIsMainGroup(int id)
        {
            using (var productGroupRepository = new ProductGroupRepository())
            {
                bool isMainGroup = false;
                if (id > 0)
                {
                    isMainGroup = productGroupRepository.checkIsMainGroup(id);
                }
                return Json(isMainGroup, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetIsLast(int id)
        {
            using (var productGroupRepository = new ProductGroupRepository())
            {
                bool isLast = true;
                if (id > 0)
                {
                    isLast = productGroupRepository.checkIsLastLevel(id);
                }
                return Json(isLast, JsonRequestBehavior.AllowGet);
            }
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
