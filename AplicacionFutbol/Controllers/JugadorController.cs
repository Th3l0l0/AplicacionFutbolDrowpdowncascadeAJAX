using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AplicacionFutbol.Models;

namespace AplicacionFutbol.Controllers
{
    public class JugadorController : Controller
    {
        JugadorDAO objDAO = new JugadorDAO();

        // GET: Jugador
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListaContinentes()
        {
            return Json(objDAO.ListarContinentes(),JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListPaises(string ide_con_pais)
        {
            int id = Convert.ToInt32(ide_con_pais);
            return Json(objDAO.ListarPaises(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaJugador()
        {
            return Json(objDAO.ListarJugador(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Jugador jug)
        {
            objDAO.InsertarJugador(jug);
            return Json(jug, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int idjug)
        {
            var Jugador = objDAO.ListarJugador().Find(x => x.ide_jug.Equals(idjug));
            return Json(Jugador, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Jugador jug)
        {
            objDAO.ActualizarJugador(jug);
            return Json(jug, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int idjug)
        {
            objDAO.EliminarJugador(idjug);
            return Json(idjug, JsonRequestBehavior.AllowGet);
        }
    }
}