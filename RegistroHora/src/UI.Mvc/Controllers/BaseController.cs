using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Infra.Repositorio;
using Entidades.Entities;
using System.Collections.Generic;
using Entidades.Interfaces.Repositorio;

namespace UserInterfase.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        IRepositorioBase<T> rp;

        public BaseController() 
        {
            rp = new RepositorioBase<T>();
        }
        public virtual ActionResult Index()
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View(rp.GetAll());
        }
        public virtual ActionResult Details(int id)
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View(rp.GetById(id));
        }

        public virtual ActionResult Create()
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(T obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rp.Add(obj);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        public virtual ActionResult Edit(int id)
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View(rp.GetById(id));
        }
        [HttpPost]
        public virtual ActionResult Edit(T obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rp.Update(obj);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        public virtual ActionResult Delete(int id)
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View(rp.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public virtual ActionResult DeleteConfirmado(int id)
        {
            var obj = rp.GetById(id);
            try
            {

                rp.Delete(obj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        public Usuario LoginSession
        {
            get
            {
                var user =  (Usuario)Session["login"];
                ViewBag.User = user;
                return user;
            }
            set
            {
                Session["login"] = value;
            }
        }
    }
}
