using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspViagens.Dados;

namespace AspViagens.Models.Ações
{
    public class AcDeletar
    {
        Conexao con = new Conexao();


        // criando o metodo testarUsuario e passando a model login com referencia a user
        public void DeletarUsuario(login user)
        {
            // criando a conexão  com o banco de dados e fazendo o select fazendo validação na classe conexão
            MySqlCommand cmd = new MySqlCommand("delete from tbl_login where dsSenha = @dsSenha and dsLogin = @dsLogin", con.MyConectarBD());
            //parametros do usuario e senha e recebendo os campos da model
            cmd.Parameters.Add("@dsLogin", MySqlDbType.VarChar).Value = user.dsLogin;
            cmd.Parameters.Add("@dsSenha", MySqlDbType.VarChar).Value = user.dsSenha;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

        }
    }
}