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
            ModelState.AddModelError("", "Login ou senha invalido");
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


        [HttpPost]
        public override ActionResult Create(Usuario obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (usuarioRepositorio.ValidarLogin(obj.Login))
                    {
                        if (usuarioRepositorio.ValidarEmail(obj.Email))
                        {
                            usuarioRepositorio.Add(obj);
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");
                        return View(obj);
                    }
                    ModelState.AddModelError("", "Login já cadastrado");
                }

                return View(obj);
            }
            catch
            {
                return View(obj);
            }
        }

        [HttpPost]
        public override ActionResult Edit(Usuario obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (obj.Login == LoginSession.Login) 
                    {
                        if (obj.Email == LoginSession.Email)
                        {
                            usuarioRepositorio.Update(obj);
                            LoginSession = obj;
                            return RedirectToAction("Index");
                        }
                        else if (usuarioRepositorio.ValidarEmail(obj.Email))
                        {
                            usuarioRepositorio.Update(obj);
                            LoginSession = obj;
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");
                        return View(obj);

                    }
                    else if(usuarioRepositorio.ValidarLogin(obj.Login))
                    {
                        if (obj.Email == LoginSession.Email)
                        {
                            usuarioRepositorio.Update(obj); 
                            LoginSession = obj;
                            return RedirectToAction("Index");
                        }
                        else if (usuarioRepositorio.ValidarEmail(obj.Email))
                        {
                            usuarioRepositorio.Update(obj);
                            LoginSession = obj;
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");
                        return View(obj);
                    }
                    ModelState.AddModelError("", "Login já cadastrado");
                }

                return View(obj);
            }
            catch
            {
                return View(obj);
            }
        }

    }
}