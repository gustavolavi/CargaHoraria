using Entidades.Entities;
using Entidades.Interfaces.Repositorio;
using Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterfase.Controllers;

namespace UI.Mvc.Controllers
{
    public class UsuarioController : BaseController<Usuario>
    {
        IUsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
        public override ActionResult Index()
        {
            if (LoginSession != null)
            {
                return RedirectToAction("Index", "CargaHoraria");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string senha)
        {
            var usuario = usuarioRepositorio.Login(login, senha);

            if (usuario != null)
            {
                LoginSession = usuario;
                return RedirectToAction("Index", "CargaHoraria");
            }
            ViewBag.Erro = "Login ou Senha invalida";
            return View();
        }

        public ActionResult Deslogar()
        {
            LoginSession = null;
            return RedirectToAction("index");
        }

        public override ActionResult Create()
        {
            if (LoginSession != null)
            {
                return RedirectToAction("Index", "CargaHoraria");
            }
            return View();
        }

        public ActionResult LoginUnico(string login)
        {
            return Json(usuarioRepositorio.ListaDeLogins().All(x => x.ToLower() != login.ToLower()), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmailUnico(string email)
        {
            return Json(usuarioRepositorio.ListaDeEmails().All(x => x.ToLower() != email.ToLower()), JsonRequestBehavior.AllowGet);
        }
    }
}