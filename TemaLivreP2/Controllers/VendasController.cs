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
    public class VendasController : Controller
    {
        private Prova2TercaEntities1 db = new Prova2TercaEntities1();

        // GET: Vendas
        public ActionResult Index()
        {
            var venda = db.Venda.Include(v => v.Cliente1).Include(v => v.Fornecedor1).Include(v => v.Material1);
            return View(venda.ToList());
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            ViewBag.Cliente = new SelectList(db.Cliente, "id", "nome");
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome");
            ViewBag.Material = new SelectList(db.Material, "id", "descricao");
            return View();
        }

        // POST: Vendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Fornecedor,Cliente,Material,Data")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Venda.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente = new SelectList(db.Cliente, "id", "nome", venda.Cliente);
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", venda.Fornecedor);
            ViewBag.Material = new SelectList(db.Material, "id", "descricao", venda.Material);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente = new SelectList(db.Cliente, "id", "nome", venda.Cliente);
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", venda.Fornecedor);
            ViewBag.Material = new SelectList(db.Material, "id", "descricao", venda.Material);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Fornecedor,Cliente,Material,Data")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(db.Cliente, "id", "nome", venda.Cliente);
            ViewBag.Fornecedor = new SelectList(db.Fornecedor, "id", "nome", venda.Fornecedor);
            ViewBag.Material = new SelectList(db.Material, "id", "descricao", venda.Material);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Venda.Find(id);
            db.Venda.Remove(venda);
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
