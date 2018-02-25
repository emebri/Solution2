using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_ENTIDAD;
using C_LOGICA;

namespace C_PRESENTACION.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult CerrarSesion()
        {
            Session.Remove("usuarito");
            return RedirectToAction("VerificarAcceso", "Login");
        }

        public ActionResult Index()
        {
            Usuario u = (Usuario)Session["usuarito"];
            ViewBag.mensaje = u.trabajador.nombre + " " + u.trabajador.apellidos;
            //ViewBag.usuario = u.trabajador.nombre + " " + u.trabajador.apellidos ;
            return View();
        }
       
        public ActionResult Listar()
        {
            try
            {
                List<Trabajador> lista = logTrabajador.Instancia.ListarTrabajador();
                return View(lista);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { mensajerror = ex.Message});
            }
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Usuario u)
        {
            try
            {
                Boolean inserto = logUsuario.Instancia.InsertarTrabajador(u);
                if (inserto)
                {
                    return RedirectToAction("Listar");
                }
                else { return View(u); }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { mensajerror = ex.Message });
            }
        }

    }
}