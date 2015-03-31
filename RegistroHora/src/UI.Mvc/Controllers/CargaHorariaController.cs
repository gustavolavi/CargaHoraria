﻿using Entidades.Entities;
using Entidades.Interfaces.Repositorio;
using Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserInterfase.Controllers
{
    public class CargaHorariaController : BaseController<CargaHoraria>
    {
        ICargaHorariaRepositorio cargahoraria = new CargaHorariaRepositorio();
        ITipoDeRegistroRepositorio tipoDeRegistro = new TipoDeRegistroRepositorio();
       
        public override ActionResult Index()
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }

            List<CargaHoraria> horarios = new List<CargaHoraria>();
            horarios = cargahoraria.GetAllByUser(LoginSession.UsuarioId);

            int tamanho = (horarios.Count / 4);
            DateTime[] tempo = new DateTime[tamanho];
            int aux = horarios.Count;
            for (int i = tamanho; i > 0; i--)
            {

                CargaHoraria item1 = horarios[--aux];//e
                CargaHoraria item2 = horarios[--aux];//a.s
                CargaHoraria item3 = horarios[--aux];//a.e
                CargaHoraria item4 = horarios[--aux];//s

                TimeSpan tempo1 = item4.DataHora.Subtract(item3.DataHora);
                TimeSpan tempo2 = item2.DataHora.Subtract(item1.DataHora);

                TimeSpan total = tempo1 + tempo2;
                tempo[i-1] = Convert.ToDateTime(total.ToString());
            }
            ViewBag.Tempos = tempo;
            return View(horarios);
        }

        // GET: Base/Create
        public override ActionResult Create()
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo");
            return View();
        }

        // POST: Base/Create
        [HttpPost]
        public override ActionResult Create(CargaHoraria obj)
        {
            obj.UsuarioId = LoginSession.UsuarioId;
            try
            {
                if (ModelState.IsValid)
                {
                    obj.DaraComHora();
                    cargahoraria.Add(obj);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);
                return View(obj);
            }
        }

        public override ActionResult Edit(int id)
        {
            if (LoginSession == null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            var obj = cargahoraria.GetById(id);
            ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);
            obj.tranformarDataHora();
            return View(obj);
        }
        [HttpPost]
        public override ActionResult Edit(CargaHoraria obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.DaraComHora();
                    cargahoraria.Update(obj);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);

                return View(obj);
            }
        }
    }
}