using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TemaLivreP2;

namespace TemaLivreP2.Controllers
{
    public class MaterialsController : Controller
    {
        private Prova2TercaEntities1 db = new Prova2TercaEntities1();

        // GET: Materials
        public ActionResult Index()
        {
            var material = db.Material.Include(m => m.Fornecedor1).Include(m => m.Tipo1);
            return View(material.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome");
            ViewBag.Tipo = new SelectList(db.Tipo, "id", "descricao");
            return View();
        }

        // POST: Materials/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descricao,Tipo,Fornecedor,Preco")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Material.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", material.Fornecedor);
            ViewBag.Tipo = new SelectList(db.Tipo, "id", "descricao", material.Tipo);
            return View(material);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", material.Fornecedor);
            ViewBag.Tipo = new SelectList(db.Tipo, "id", "descricao", material.Tipo);
            return View(material);
        }

        // POST: Materials/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descricao,Tipo,Fornecedor,Preco")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", material.Fornecedor);
            ViewBag.Tipo = new SelectList(db.Tipo, "id", "descricao", material.Tipo);
            return View(material);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Material.Find(id);
            db.Material.Remove(material);
            db.SaveChanges();
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
