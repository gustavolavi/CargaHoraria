using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Infra.Repositorio;
using Entidades.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entidades.Interfaces.Repositorio;

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
            ViewBag.Tempos = Tempo();
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

        public DateTime[] Tempo()
        {
            List<CargaHoraria> horarios = new List<CargaHoraria>();
            horarios = cargahoraria.GetAllByUser(LoginSession.UsuarioId);
            int tamanho = (horarios.Count / 4);
            DateTime[] tempo = new DateTime[tamanho];
            int aux = horarios.Count;
            for (int i = tamanho; i > 0; i--)
            {

                CargaHoraria item1 = horarios[--aux];//entrada
                CargaHoraria item2 = horarios[--aux];//almoço.saida
                CargaHoraria item3 = horarios[--aux];//almoço.entrada
                CargaHoraria item4 = horarios[--aux];//saida

                TimeSpan tempo1 = item4.DataHora.Subtract(item3.DataHora);
                TimeSpan tempo2 = item2.DataHora.Subtract(item1.DataHora);

                TimeSpan total = tempo1 + tempo2;
                tempo[i - 1] = Convert.ToDateTime(total.ToString());

            }
            return tempo;
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
                    switch (obj.TipoId)
                    {
                        case 1: //Entrada
                            break;
                        case 2: //Saida
                            if (cargahoraria.GetLastTipoId(obj.UsuarioId) != 3)
                            {
                                ModelState.AddModelError("", "Desculpe mas o horário de almoço é obrigatorio! Porfavor cadastre uma saida e entrada de almoço.");
                                ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);
                                return View(obj);
                            }
                            break;
                        case 3: // a.entrada
                            if (cargahoraria.GetLastTipoId(obj.UsuarioId) != 4)
                            {
                                ModelState.AddModelError("", "Desculpe mas o horário de almoço é obrigatorio! Porfavor cadastre uma saida de almoço.");
                                ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);
                                return View(obj);
                            }
                            break;
                        case 4: // a.saida 
                            if (cargahoraria.GetLastTipoId(obj.UsuarioId) != 1)
                            {
                                ModelState.AddModelError("", "Cadastre uma entrada");
                                ViewBag.TipoId = new SelectList(tipoDeRegistro.GetAll(), "TipoId", "Tipo", obj.TipoId);
                                return View(obj);
                            }
                            break;

                    }
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

        public JsonResult JsonCargaHorarias()
        {
            var all = cargahoraria.GetAllByUser(LoginSession.UsuarioId);
            List<Object> resultado = new List<object>();
            var temp = Tempo();

            int aux = 0;
            foreach (var i in all)
            {
                if (i.TipoId == 2)
                {
                    resultado.Add(new { Id = i.CargaHorariaId, Tipo = i.TipoDeRegisto.Tipo, Data = Convert.ToString(i.DataHora), Dia = "Dia " + i.DataHora.Day, TempoSemana = String.Format("{0:h:mm}", temp[aux]) + " Horas" });
                    aux++;
                }
                else
                {
                    resultado.Add(new { Id = i.CargaHorariaId, Tipo = i.TipoDeRegisto.Tipo, Data = Convert.ToString(i.DataHora) });
                }
            }

            return Json(new { lista = resultado, nome = all [0].Usuario.Nome}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            return View();
        }
    }
}