using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using AspViagens.Models;
using AspViagens.Models.Ações;
using AspViagens.Models.Banco;
using Asp_Viagens.Models.Banco.Produtos;

namespace TCC_Viagens.Controllers
{
    public class PacoteController : Controller
    {
        AcPlano AcPl = new AcPlano();
        AcPacote AcP = new AcPacote();
        Plano CadP = new Plano();
        FiltrarPlanoPacote fpl = new FiltrarPlanoPacote();
        List<SelectListItem> nmPacote = new List<SelectListItem>();
        public ActionResult CadPacote()
        {

            nmPacote.Add(new SelectListItem
            {
                Text = "Entretenimento",
                Value = "Entretenimento"
            });
            nmPacote.Add(new SelectListItem
            {
                Text = "Educação",
                Value = "Educação"
            });
            nmPacote.Add(new SelectListItem
            {
                Text = "Profissional",
                Value = "Profissional"
            });

            ViewBag.nmPacote = new SelectList(nmPacote, "Text", "Value");

            return View();
        }
        public ActionResult InserirCadPacote(Plano cl)
        {
            Pacote cp = TempData["Cp"] as Pacote;

            string dsPreco = cp.dsPreco;
            dsPreco = dsPreco.Replace(".", "").Replace(",", ".");

            AcP.InserirPacote(cp, cl,dsPreco);
            return RedirectToAction("ListarTodosPacotes", "Pacote");

        }
        public ActionResult DeletarPacote(string id, Plano plano)
        {
            try
            {
                AcPl.ConsultaIdPlano(id, plano); 
                AcPl.DeletarPlanoId(plano);
                AcP.DeletarPacote(id);
            }
            catch
            {
                TempData["Delete Erro"] = "Clientes já compraram esse pacote, não pode excluir";
            }
            return RedirectToAction("ListarTodosPacotes", "Pacote");
        }

        [HttpGet]
        public ActionResult EditarPacoteId(string id, Pacote cadPacote)
        {
            nmPacote.Add(new SelectListItem
            {
                Text = "Entretenimento",
                Value = "Entretenimento"
            });
            nmPacote.Add(new SelectListItem
            {
                Text = "Educação",
                Value = "Educação"
            });
            nmPacote.Add(new SelectListItem
            {
                Text = "Profissional",
                Value = "Profissional"
            });

            ViewBag.nmPacote = new SelectList(nmPacote, "Text", "Value");

            return View(AcP.SelecionaCadPacote(id, cadPacote));
        }

        [HttpPost]
        public ActionResult EditarPacoteId(string id, Pacote objPacote, HttpPostedFileBase file, string nmPacote)
        {
            if (file != null)
            {
                //Imagem
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/" + arquivo;
                string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                file.SaveAs(_path);

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                string dsPreco = objPacote.dsPreco;
                dsPreco = dsPreco.Replace(".", "").Replace(",", ".");

                //Salvar no Banco
                objPacote.nmPacote = nmPacote;
                objPacote.Imagem = file2;
                AcP.EditarPacote(id, objPacote, dsPreco);                
                return RedirectToAction("EditarPlanoId", "Plano", new { pacoteId = id });
            }
            else
            {
                TempData["Mensagem Erro Imagem"] = "É obrigatorio inserir uma imagem";
                return RedirectToAction("EditarPacoteId", "Pacote");
            }
        }
        public ActionResult ListarTodosPacotes()
        {
            return View(AcP.ListarPacote());
        }
        public ActionResult FiltrarTodosPacotes(string nmPacote, string nmPais, string valor)
        {
            if (nmPacote == "" && nmPais == "" && valor == "")
            {
                fpl.valor1 = "000";
                fpl.valor2 = "000";
                fpl.nmPacote = "erro";
                fpl.nmPais = "erro";

                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote != "" && nmPais == "" && valor == "")
            {
                fpl.valor1 = "000";
                fpl.valor2 = "000";
                fpl.nmPacote = nmPacote;
                fpl.nmPais = "erro";
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote == "" && nmPais != "" && valor == "")
            {
                fpl.valor1 = "000";
                fpl.valor2 = "000";
                fpl.nmPacote = "erro";
                fpl.nmPais = nmPais;
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote == "" && nmPais == "" && valor != "")
            {
                string[] partes = valor.Split('/');
                fpl.valor1 = partes[0];
                fpl.valor2 = partes[1];

                fpl.nmPacote = "erro";
                fpl.nmPais = "erro";
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote == "" && nmPais != "" && valor != "")
            {
                string[] partes = valor.Split('/');
                fpl.valor1 = partes[0];
                fpl.valor2 = partes[1];

                fpl.nmPais = nmPais;
                fpl.nmPacote = "erro";
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote != "" && nmPais != "" && valor == "")
            {
                fpl.valor1 = "000";
                fpl.valor2 = "000";
                fpl.nmPais = nmPais;
                fpl.nmPacote = nmPacote;
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else if (nmPacote != "" && nmPais == "" && valor != "")
            {
                string[] partes = valor.Split('/');
                fpl.valor1 = partes[0];
                fpl.valor2 = partes[1];
                fpl.nmPais = "erro";
                fpl.nmPacote = nmPacote;
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
            else
            {
                string[] partes = valor.Split('/');
                fpl.valor1 = partes[0];
                fpl.valor2 = partes[1];

                fpl.nmPais = nmPais;
                fpl.nmPacote = nmPacote;
                return View(AcP.FiltrarPlanoPacote(fpl));
            }
        }
    }
}