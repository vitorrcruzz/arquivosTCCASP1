using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Windows;
using AspViagens.Models;
using AspViagens.Models.Ações;

namespace AspViagens.Controllers
{
    public class LoginController : Controller
    {

        // instancia da classe ações 
        AcLogin AcL = new AcLogin();
        AcCadastro AcC = new AcCadastro();
        AcDeletar AcD = new AcDeletar();
        Models.login login = new Models.login();
        public ActionResult Login()
        {
            return View();
        }
        // metodo verificarUsuario passando o model e passando usuario
        public ActionResult VerificaUsuario(Models.login user)
        {
            //chamando o metodo testar usauario
            AcL.TestarUsuario(user);
            // verificando se os usuario foram em brancos 
            if (user.dsLogin != null && user.dsSenha != null)
            {
                // entrar na sessão indo busca o usuario e senha no banco de dados e convertendo para string
                Session["dsLogin"] = user.dsLogin.ToString();
                Session["dsSenha"] = user.dsSenha.ToString();
                Session["idCli"] = user.idCliente.ToString();

                if(user.tipo == "1")
                {
                    Session["tipoLogado1"] = user.tipo.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["tipoLogado2"] = user.tipo.ToString();
                    return RedirectToAction("Dashboard", "Home");
                }

            }
            else
            {
                // caso contrario continua na tela login controller login
                TempData["Mensagem Erro"] = "Usuário e/ou Senha invalido, tente novamente.";
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult usuarioAdmin()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                if (Session["usuarioLogado1"] != null)
                {
                    ViewBag.tipo = Session["usuarioLogado1"];
                }
                else
                {
                    ViewBag.tipo = Session["usuarioLogado2"];
                }
                return View();
            }
        }

        public ActionResult usuarioComum()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nome = Session["usuarioLogado"];
                if (Session["usuarioLogado1"] != null)
                {
                    ViewBag.tipo = Session["usuarioLogado1"];
                }
                else
                {
                    ViewBag.tipo = Session["usuarioLogado2"];
                }
                return View();
            }
        }     

        // metodo do Cadastro para ser criado a tela
        public ActionResult Cadastrar()
        {
            return View();
        }

        // metodo CadastrarUsuario passando os models
        public ActionResult CadastrarUsuario(Cadastro inser, Cadastro TinserCPF, Cadastro TinserEmail, Models.login TinserLogin)
        {  
            AcC.TestarCadastroEmail(TinserEmail);
            AcC.TestarCadastroCPF(TinserCPF);
            AcC.TestarCadastroLogin(TinserLogin);

            if (TinserCPF.noCPF != null && TinserEmail.dsEmail != null)
            {
                TempData["Mensagem Erro Cadastro CPF Email"] = "O CPF e Email inserido já possui cadastro";
                return RedirectToAction("Cadastrar", "Login");
            }

            else if (TinserLogin.dsLogin != null)
            {
                TempData["Mensagem Erro Cadastro Login"] = "O Login inserido já possui cadastro";
                return RedirectToAction("Cadastrar", "Login");
            }
          
            else if (TinserEmail.dsEmail != null)
            {
                TempData["Mensagem Erro Cadastro Email"] = "O Email inserido já possui cadastro";
                return RedirectToAction("Cadastrar", "Login");
            }

            else if (TinserCPF.noCPF != null)
            {
                TempData["Mensagem Erro Cadastro CPF"] = "O CPF inserido já possui cadastro";
                return RedirectToAction("Cadastrar", "Login");
            }
            else
            {
                AcC.InserirUsuario(inser);
                return RedirectToAction("Login", "Login");
            }
        }    

        // metodo do Excluir para ser criado a tela
        public ActionResult Excluir()
        {
            return View();
        }

        // metodo ExcluirUsuario passando os models
        public ActionResult ExcluirUsuario(Models.login user)
        {
            AcL.TestarUsuario(user);

       
            if (user.dsLogin != null && user.dsSenha != null)
            {               
                Session["dsLogin"] = user.dsLogin.ToString();
                Session["dsSenha"] = user.dsSenha.ToString();

                AcD.DeletarUsuario(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Mensagem Erro Excluir"] = "Login e/ou Senha invalidos, tente novamente";
                return RedirectToAction("Excluir", "Login");
            }      
        }
    }
}