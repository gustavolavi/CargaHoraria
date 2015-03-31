using Entidades.Entities;
using Entidades.Interfaces.Repositorio;
using Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                return (Usuario)Session["login"];
            }
            set
            {
                Session["login"] = value;
            }
        }
    }
}
