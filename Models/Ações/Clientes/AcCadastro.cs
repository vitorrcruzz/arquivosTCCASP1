using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AspViagens.Dados;

namespace AspViagens.Models
{
    public class AcCadastro
    {
        // instanciando a classe conexão
        Conexao con = new Conexao();


        // criando o metodo testarUsuario e passando a model login com referencia a user
        public void InserirUsuario(Cadastro inser)
        {
            // criando a conexão  com o banco de dados e fazendo o select fazendo validação na classe conexão
            MySqlCommand cmd = new MySqlCommand("call sp_InsCli('1',@nm,@noCPF,@dsEmail,@dsLogin,@dsSenha,@Telefone);", con.MyConectarBD());
            //parametros do usuario e senha e recebendo os campos da model
            cmd.Parameters.Add("1", MySqlDbType.VarChar).Value = inser.tipo;
            cmd.Parameters.Add("@nm", MySqlDbType.VarChar).Value = inser.nm;
            cmd.Parameters.Add("@noCPF", MySqlDbType.VarChar).Value = inser.noCPF;
            cmd.Parameters.Add("@dsLogin", MySqlDbType.VarChar).Value = inser.dsLogin;
            cmd.Parameters.Add("@dsSenha", MySqlDbType.VarChar).Value = inser.dsSenha;
            cmd.Parameters.Add("@dsEmail", MySqlDbType.VarChar).Value = inser.dsEmail;
            cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = inser.Telefone;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public login TestarCadastroLogin(login TinserLogin)
        {
            MySqlCommand msc = new MySqlCommand("call sp_TestarLog(@dsLogin);  ", con.MyConectarBD());
            msc.Parameters.Add("@dsLogin", MySqlDbType.VarChar).Value = TinserLogin.dsLogin;
            MySqlDataReader leitor;

            leitor = msc.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    { 
                        TinserLogin.dsLogin = Convert.ToString(leitor["dsLogin"]);
                    }
                }
            }
            else
            {
                TinserLogin.dsLogin = null;
            }
            con.MyDesConectarBD();
            return TinserLogin;
        }
        public Cadastro TestarCadastroCPF(Cadastro TinserCPF)
        {
            MySqlCommand msc = new MySqlCommand("call sp_VerificaCPF(@noCPF); ", con.MyConectarBD());
            msc.Parameters.Add("@noCPF", MySqlDbType.VarChar).Value = TinserCPF.noCPF;
            MySqlDataReader leitor;

            leitor = msc.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    {
                        TinserCPF.noCPF = Convert.ToString(leitor["noCPF"]);
                    }
                }
            }
            else
            {
                TinserCPF.noCPF = null;
            }
            con.MyDesConectarBD();
            return TinserCPF;
        }

        public Cadastro TestarCadastroEmail(Cadastro TinserEmail)
        {
            MySqlCommand msc = new MySqlCommand("call sp_VerificaEmail(@dsEmail); ", con.MyConectarBD());
            //parametros do usuario e senha e recebendo os campos da model
            msc.Parameters.Add("@dsEmail", MySqlDbType.VarChar).Value = TinserEmail.dsEmail;

            // como é um select preciso ler os dados do banco é preciso de um dataReader 
            MySqlDataReader leitor;

            // executando o datareader
            leitor = msc.ExecuteReader();

            // vai verificar as linhas do leitor 
            if (leitor.HasRows)
            {
                // enquanto o leitor faz a leitura
                while (leitor.Read())
                {

                    {
                        //pegando os valores verificados no banco 
                        TinserEmail.dsEmail = Convert.ToString(leitor["dsEmail"]);
                    }
                }
            }
            else
            {
                //limpando os campos
                TinserEmail.dsEmail = null;
            }
            con.MyDesConectarBD();
            return TinserEmail;
        }

    }
}