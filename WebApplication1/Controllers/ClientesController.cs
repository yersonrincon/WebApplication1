using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {

                return View(db.cliente.ToList());
            }
        }
        public ActionResult Create()

        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.cliente.Add(cliente);
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
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public ActionResult Edit(int id)
        {

            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Edit(cliente clienteEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuario user = db.usuario.Find(clienteEdit.id);
                    user.nombre = clienteEdit.nombre;
                    user.documento = clienteEdit.documento; 
                    user.email = clienteEdit.email;
                    

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
                cliente user = db.cliente.Find(id);
                return View(user);
            }
        }
        public ActionResult Delete(int id)
        {
            using (inventario2021Entities db = new inventario2021Entities())
            {
                var Usuario = db.cliente.Find(id);
                db.cliente.Remove(Usuario);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}