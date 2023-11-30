using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspViagens.Dados;

namespace AspViagens.Models
{
    public class AcLogin
    {
        // instanciando a classe conexão
        Conexao con = new Conexao();


        // criando o metodo testarUsuario e passando a model login com referencia a user
        public void TestarUsuario(login user)
        {
            // criando a conexão  com o banco de dados e fazendo o select fazendo validação na classe conexão
            MySqlCommand cmd = new MySqlCommand("call sp_VerificaLog(@dsLogin, @dsSenha);", con.MyConectarBD());
            //parametros do usuario e senha e recebendo os campos da model
            cmd.Parameters.Add("@dsLogin", MySqlDbType.VarChar).Value = user.dsLogin;
            cmd.Parameters.Add("@dsSenha", MySqlDbType.VarChar).Value = user.dsSenha;

            // como é um select preciso ler os dados do banco é preciso de um dataReader 
            MySqlDataReader leitor;

            // executando o datareader
            leitor = cmd.ExecuteReader();

            // vai verificar as linhas do leitor 
            if(leitor.HasRows)
            {
                // enquanto o leitor faz a leitura
                while (leitor.Read())
                {
                    {
                        //pegando os valores verificados no banco 
                        user.dsLogin = Convert.ToString(leitor["dsLogin"]);
                        user.dsSenha = Convert.ToString(leitor["dsSenha"]);
                        user.tipo = Convert.ToString(leitor["tipo"]);
                        user.idCliente = Convert.ToString(leitor["idCliente"]);
                    }                  
                }
            }

            else
            {
                //limpando os campos
                user.dsLogin = null;
                user.dsSenha = null;
                user.tipo = null;
            }
            con.MyDesConectarBD();

        }


    }
}