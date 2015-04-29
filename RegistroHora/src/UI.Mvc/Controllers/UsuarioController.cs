using System;
using System.Web;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using Infra.Repositorio;
using Entidades.Entities;
using UserInterfase.Controllers;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using Entidades.Interfaces.Repositorio;

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
            var usuario = usuarioRepositorio.Login(login, GerarHashMd5(senha));

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
                            obj.Senha = GerarHashMd5(obj.Senha);
                            obj.ConfirmarSenha = GerarHashMd5(obj.ConfirmarSenha);
                            usuarioRepositorio.Add(obj);
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");

                        return RedirectToAction("Index", obj);
                    }
                    ModelState.AddModelError("", "Login já cadastrado");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return RedirectToAction("Index",obj);
            }
        }
        public override ActionResult Delete(int id)
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return RedirectToAction("Index", "CargaHoraria");
        }
        public override ActionResult Edit(int id)
        {
            if (id != LoginSession.UsuarioId && LoginSession.Login != "GustavoLaviola")
            {
                return RedirectToAction("Index", "CargaHoraria");
            }
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            var user = usuarioRepositorio.GetById(id);
            user.Senha = null;
            return View(user);
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
                            FazerEdição(obj);
                            return RedirectToAction("Index");
                        }
                        else if (usuarioRepositorio.ValidarEmail(obj.Email))
                        {
                            FazerEdição(obj);
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");
                        return View(obj);

                    }

                    else if (usuarioRepositorio.ValidarLogin(obj.Login))
                    {
                        if (obj.Email == LoginSession.Email)
                        {
                            FazerEdição(obj);
                            return RedirectToAction("Index");
                        }
                        else if (usuarioRepositorio.ValidarEmail(obj.Email))
                        {
                            FazerEdição(obj);
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Email já cadastrado");
                        return View(obj);
                    }
                    ModelState.AddModelError("", "Login já cadastrado");
                }

                return View(obj);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return View(obj);
            }
        }
        public void FazerEdição(Usuario obj)
        {
            obj.Senha = GerarHashMd5(obj.Senha);
            obj.ConfirmarSenha = GerarHashMd5(obj.ConfirmarSenha);
            usuarioRepositorio.Update(obj);
            LoginSession = obj;
        }
        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}