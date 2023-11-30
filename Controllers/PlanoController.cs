using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspViagens.Models.Ações;
using AspViagens.Models.Banco;

namespace AspViagens.Controllers
{
    public class PlanoController : Controller
    {
        AcPlano Acp = new AcPlano();
        public ActionResult Inser_Planos(Pacote cp,Plano cl, HttpPostedFileBase file, string nmPacote)
        {
            if (file != null)
            {
                //Imagem
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/" + arquivo;
                string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                file.SaveAs(_path);
                cp.Imagem = file2;
                cp.nmPacote = nmPacote;

                TempData["Cp"] = cp;
            }
            else
            {
                TempData["Mensagem Erro Imagem1"] = "È obrigatorio inserir uma imagem";
                return RedirectToAction("CadPacote", "Pacote");
            }

            return View();
        }

        public ActionResult SaibaMaisPlanos(int id)

        {
            if (id == 1)
            {
                ViewBag.nnPlano = "Canadá";
                string Pais = "Canadá";
                return View(Acp.SaibaMaisPlano(Pais));
            }
            else if (id == 2)
            {
                ViewBag.nnPlano = "Grécia";
                string Pais = "Grécia";
                return View(Acp.SaibaMaisPlano(Pais));
            }
            else
            {
                ViewBag.nnPlano = "Inglaterra";
                string Pais = "Inglaterra";
                return View(Acp.SaibaMaisPlano(Pais));
            }
        }    

        [HttpGet]
        public ActionResult EditarPlanoId(Plano plano,string pacoteId)
        {        
            return View(Acp.ConsultaIdPlano(pacoteId, plano) );
        }      

        [HttpPost]
        public ActionResult EditarPlanoId2(Plano plano,string idPlano)
        {
            Acp.EditarPlano(idPlano, plano);
            return RedirectToAction("SaibaMais", "Home");
        }

        public ActionResult PlanosDsh()
        {
            return View();
        }
    }
}