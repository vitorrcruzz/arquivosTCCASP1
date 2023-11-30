using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspViagens.Models;
using AspViagens.Models.Ações;
using AspViagens.Models.Banco;
using Asp_Viagens.Models;

namespace AspViagens.Controllers
{
    public class HomeController : Controller
    {
        AcPacote AcPacote = new AcPacote();
        AcCompra AcCompra = new AcCompra();
        AcItem AcItem = new AcItem();
        login login = new login();
        List<SelectListItem> Tipo_Pagamento = new List<SelectListItem>();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            Grafico();
            return View();
        }
        public static string codigo, dsPreco;
        public ActionResult AdicionarCarrinho(string id)
        {
            if (Session["tipoLogado2"] != null)
            {
                TempData["Mensagem Erro ADM"] = "Logado como administrador você não pode realizar compras.";
                return RedirectToAction("ListarTodosPacotes", "Pacote");
            }
            else
            {
                modelCompra carrinho = Session["Carrinho"] != null ? (modelCompra)Session["Carrinho"] : new modelCompra();

                var plano = AcPacote.selecionaConsPacote(id);

                codigo = id.ToString();


                if (plano != null)
                {
                    var planoCompra = new modelPacote();

                    planoCompra.PlanoPedidoID = Guid.NewGuid();
                    planoCompra.idPacote = codigo;
                    planoCompra.nomePacote = plano[0].nomePacote;
                    planoCompra.imagem = plano[0].imagem;
                    planoCompra.dsPreco = plano[0].dsPreco;

                    List<modelPacote> x = carrinho.PlanosPedidos.FindAll(l => l.idPacote == planoCompra.idPacote);

                    if (x.Count != 0)
                    {
                        return Content("<script language= 'javascript' type='text/javascript'>alert('Pacote já incluido no carrinho'); location.href='Carrinho';</script>");
                    }
                    else
                    {
                        carrinho.PlanosPedidos.Add(planoCompra);
                    }
                    Session["Carrinho"] = carrinho;
                }
                return RedirectToAction("Carrinho");
            }
        }
        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Carrinho()
        {
            modelCompra carrinho = Session["Carrinho"] != null ? (modelCompra)Session["Carrinho"] : new modelCompra();

            double valor = 0;
            ViewBag.valor = valor;
            return View(carrinho);
        }
        public ActionResult ExcluirPacote(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (modelCompra)Session["Carrinho"] : new modelCompra();
            var planoExclusao = carrinho.PlanosPedidos.FirstOrDefault(i => i.PlanoPedidoID == id);

            carrinho.PlanosPedidos.Remove(planoExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("carrinho");
        }
        public ActionResult Pagamento()
        {
            Tipo_Pagamento.Add(new SelectListItem
            {
                Text = "Pix",
                Value = "Pix"
            });
            Tipo_Pagamento.Add(new SelectListItem
            {
                Text = "Boleto",
                Value = "Boleto"
            });
            Tipo_Pagamento.Add(new SelectListItem
            {
                Text = "Cartão",
                Value = "Cartão"
            });

            ViewBag.Tipo_Pagamento = new SelectList(Tipo_Pagamento, "Text", "Value");
            return View();
        }
        public ActionResult Confi_Pagamento(string Tipo_Pagamento)
        {
            if (Session["tipoLogado1"] == null && Session["tipoLogado2"] == null)
            {
                TempData["Mensagem Erro Carrinho"] = "É necessario estar logado para realizar a compra!!! Clique ao lado para Logar: ";
                return RedirectToAction("Carrinho", "Home");
            }
            else
            {
                string id_Cliente = Session["idCli"].ToString();
                int idCli = int.Parse(id_Cliente);
                AcCompra.Tipo_Pagamento(idCli, Tipo_Pagamento);
                return RedirectToAction("SalvarCarrinho");
            }
        }
        DateTime data;
        public ActionResult SalvarCarrinho(modelCompra x)
        {

            var carrinho = Session["Carrinho"] != null ? (modelCompra)Session["Carrinho"] : new modelCompra();

            modelCompra mc = new modelCompra();
            modelPacote mp = new modelPacote();


            DateTime data = DateTime.Now;
            mc.dtComp = data.ToString("yyyy-MM-dd hh:mm:ss");
            mc.idCliente = Session["idCli"].ToString();

            AcCompra.inserirCompra(mc);

            AcCompra.buscaIdComp(x);

            for (int i = 0; i < carrinho.PlanosPedidos.Count; i++)
            {
                mp.idComp = x.idComp;
                mp.idPacote = carrinho.PlanosPedidos[i].idPacote;

                AcItem.inserirItem(mp);
            }

            carrinho.PlanosPedidos.Clear();
            return RedirectToAction("confComp");

        }
        public ActionResult confComp()
        {
            return View();
        }
        string DataCompra;
        public ActionResult Detalhes(modelCompra mc, modelPacote mp)
        {
            mc.idCliente = Session["idCli"].ToString();
            List<modelCompra> minhaLista = AcCompra.buscarDadosCompra(mc);

            if (minhaLista.Count == 0)
            {
                return View();
            }
            else
            {
                foreach (var item in minhaLista)
                {
                    ViewBag.dtComp = item.dtComp;
                }
                List<modelPacote> minhaLista2 = AcCompra.buscarPlanoId(minhaLista);

                return View(AcPacote.selecionaConsPacoteList(minhaLista2));
            }
        }
        public ActionResult SaibaMais(int? id)
        {
            if (id == 1)
            {
                ViewBag.nnPacote = "Educação";
                return View(AcPacote.SelecionaEducacaoPacote());
            }
            else if (id == 2)
            {
                ViewBag.nnPacote = "Profissional";
                return View(AcPacote.SelecionaProfissionalPacote());
            }
            else
            {
                ViewBag.nnPacote = "Turismo";
                return View(AcPacote.SelecionaEntretenimentoPacote());
            }

        }
        public ActionResult ConsTdDetalhes(modelCompra mC)
        {

            List<modelCompra> listModel = AcCompra.buscarIdTdComp(mC);

            if (listModel.Count == 0)
            {
                return View();
            }
            else
            {
                List<modelPacote> listModel2 = AcCompra.buscarPlanoId(listModel);
                List<Pacote> listmodel3 = AcPacote.selecionaConsPacoteList(listModel2);

                ViewBag.lista = listmodel3;
                ViewBag.BotaoClicadoId = Session["BotaoClicadoId"];
                return View();

            }
        }
        double valor = 0;
        private void Grafico()
        {
            AcPacote.SelecionaidPacote();
            List<Pacote> idPacote = AcPacote.SelecionaidPacote();
            List<Pacote> idPacoteLimpo = idPacote
            .GroupBy(x => x.IdPacote)
            .Select(g => g.First())
            .ToList();

            List<Pacote> dspreçoPacote = AcPacote.consultaGrafico2(idPacoteLimpo);
            List<Pacote> dtcompPacote = AcPacote.consultaGrafico3(idPacoteLimpo);

            List<string> dspreço = dspreçoPacote.Select(p => p.dsPreco).ToList();
            List<string> dtcomp = dtcompPacote.Select(p => p.DataComp).ToList();

            List<string> apenasDatas = new List<string>();
            foreach (var dataCompleta in dtcomp)
            {
                DateTime dataConvertida = DateTime.ParseExact(dataCompleta, "dd/MM/yyyy HH:mm:ss", null);

                string parteDaData = dataConvertida.ToString("dd/MM/yyyy");

                apenasDatas.Add(parteDaData);
            }

            List<DataItem> listaComb = apenasDatas.Zip(dspreço, (data, valor) => new DataItem { dsPreco = valor, dtComp = data }).ToList();

            listaComb = listaComb.OrderBy(item => item.dtComp).ToList();

            List<string> dspreço2 = listaComb.Select(p => p.dsPreco).ToList();
            List<string> dtcomp2 = listaComb.Select(p => p.dtComp).ToList();

            ViewBag.dspreçoPacote = dspreço2;
            ViewBag.dtcompPacote = dtcomp2;

            List<double> valores = new List<double>();
            foreach (var modelo in dspreçoPacote)
            {

                if (Double.TryParse(modelo.dsPreco, out double valorConvertido))
                {
                    valores.Add(valorConvertido);
                }
            }
            if (valores != null && valores.Count > 0)
            {
                double soma = valores.Sum();
                ViewBag.SomaValores = soma;
            }
            else
            {
                ViewBag.SomaValores = 0;
            }
        }
        [HttpPost]
        public ActionResult ClicarBotao(int botaoId)
        {
            Session["BotaoClicadoId"] = botaoId;

            return RedirectToAction("ConsTdDetalhes");
        }
    }
}