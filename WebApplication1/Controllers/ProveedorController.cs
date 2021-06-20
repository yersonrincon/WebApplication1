﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {

                return View(db.proveedor.ToList());
            }
        }
        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            //string para guardar la ruta
            string filePath = string.Empty;

            //condicion para saber si llego el archivo
            if (fileForm != null)
            {
                //ruta de la carpeta que gurdara el archivo
                string path = Server.MapPath("~/Uploads/");

                //condicion para saber si la ruta de la carpeta existe
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //obtener el nombre del archivo
                filePath = path + Path.GetFileName(fileForm.FileName);
                //obtener la extension del archivo
                string extension = Path.GetExtension(fileForm.FileName);

                //guardar el archivo
                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);

                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newProveedor = new proveedor
                        {
                            nombre = row.Split(';')[0],
                            direccion = row.Split(';')[1],
                            telefono = row.Split(';')[2],
                            nombre_contacto = row.Split(';')[3],
                        };

                        using (var db = new inventario2021Entities())
                        {
                            db.proveedor.Add(newProveedor);
                            db.SaveChanges();
                        }
                    }
                }
            }

            return View();
        }





        public static string NombreProveedor(int idProveedor)
        {
            using (var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }
        public ActionResult Create()

        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", "error" + Ex);
                return View();
            }
        }
        public ActionResult Edit(int id)
        {

            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor findUser = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
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
        public ActionResult Edit(proveedor proveedorEdit)

        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor user = db.proveedor.Find(proveedorEdit.id);
                    user.nombre = proveedorEdit.nombre;
                    user.direccion = proveedorEdit.direccion;
                    user.telefono = proveedorEdit.telefono;
                    user.nombre_contacto = proveedorEdit.nombre_contacto;


                    db.SaveChanges();
                    return RedirectToAction("index");
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
                proveedor user = db.proveedor.Find(id);
                return View(user);
            }
        }
        public ActionResult Delete(int id)
        {
            using (inventario2021Entities db = new inventario2021Entities())
            {
                var Usuario = db.proveedor.Find(id);
                db.proveedor.Remove(Usuario);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}