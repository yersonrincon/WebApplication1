using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

       
        public static string Nombreproducto(int idproducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idproducto).nombre;
            }
        }

        public ActionResult ListarProducto()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public static string Nombreusuario(int idusuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(idusuario).nombre;
            }
        }

        public ActionResult Listarusuario()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra newproducto_compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(newproducto_compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                producto_compra producto_compraDetalle = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(producto_compraDetalle);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var producto_compraDelete = db.producto_compra.Find(id);
                db.producto_compra.Remove(producto_compraDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_compra producto_compra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(producto_compra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra producto_compraEdit)
        {

            if (!ModelState.IsValid)
                return View();
            try

            {
                using (var db = new inventario2021Entities())
                {
                    var producto_compra = db.producto_compra.Find(producto_compraEdit.id);
                    producto_compra.cantidad= producto_compraEdit.cantidad;
                  

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    }
}