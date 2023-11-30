using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspViagens.Dados;
using AspViagens.Models.Banco;

namespace AspViagens.Models.Ações
{
    public class AcItem
    {
        Conexao con = new Conexao();

        public void inserirItem(modelPacote mp)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_ItensCompra values(default,@idCom,@idPacote)", con.MyConectarBD());

            cmd.Parameters.Add("@idCom", MySqlDbType.VarChar).Value = mp.idComp;
            cmd.Parameters.Add("@idPacote", MySqlDbType.VarChar).Value = mp.idPacote;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}