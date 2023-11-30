using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AspViagens.Dados;
using AspViagens.Models.Banco;

namespace AspViagens.Models.Ações
{
    public class AcCompra
    {
        Conexao con = new Conexao();

        public void inserirCompra(modelCompra cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Compra values(default, @dtComp, @idCliente)", con.MyConectarBD());

            cmd.Parameters.Add("@dtComp", MySqlDbType.VarChar).Value = cm.dtComp;
            cmd.Parameters.Add("@idCliente", MySqlDbType.VarChar).Value = cm.idCliente;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void buscaIdComp(modelCompra comp)
        {
            MySqlCommand cmd = new MySqlCommand("select idCom from tbl_Compra order by idCom desc limit 1", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comp.idComp = dr[0].ToString();
            }
            con.MyDesConectarBD();
        }
        public List<modelCompra> buscarIdTdComp(modelCompra comp)
        {
            List<modelCompra> mc = new List<modelCompra>();
            MySqlCommand cmd = new MySqlCommand("select idCom,dtComp,idCliente from tbl_Compra", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                mc.Add(
                    new modelCompra
                    {
                        idComp = Convert.ToString(dr["idCom"]),
                        idCliente = Convert.ToString(dr["idCliente"]),
                        dtComp = Convert.ToString(dr["dtComp"])
                    });
            }
            return mc;
        }
        public List<modelCompra> buscarDadosCompra(modelCompra modelCompra)
        {
            List<modelCompra> mc = new List<modelCompra>();
            MySqlCommand msc = new MySqlCommand("select idCom,dtComp from tbl_Compra where idCliente = @idCliente;", con.MyConectarBD());
            msc.Parameters.Add("@idCliente", MySqlDbType.VarChar).Value = modelCompra.idCliente;
            MySqlDataAdapter da = new MySqlDataAdapter(msc);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                mc.Add(
                    new modelCompra
                    {
                        idComp = Convert.ToString(dr["idCom"]),
                        dtComp = Convert.ToString(dr["dtComp"])
                    });
            }
            return mc;

        }
        public List<modelPacote> buscarPlanoId(List<modelCompra> listModel)
        {
            List<modelPacote> modelPlano = new List<modelPacote>();

            foreach (var item in listModel)
            {
                
                MySqlCommand msc = new MySqlCommand("select idPacote from tbl_ItensCompra where idCom = @idCom;", con.MyConectarBD());
                msc.Parameters.Add("@idCom", MySqlDbType.VarChar).Value = item.idComp;
                MySqlDataAdapter da = new MySqlDataAdapter(msc);

                DataTable dt = new DataTable();

                da.Fill(dt);

                con.MyDesConectarBD();

                foreach (DataRow dr in dt.Rows)
                {
                    modelPlano.Add(
                        new modelPacote
                        {
                            idPacote = Convert.ToString(dr["idPacote"])
                        });
                }
            }
            return modelPlano;

        }

        public void Tipo_Pagamento(int idCli, string Tipo_Pagamento)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Tipo_Pagamento values(default,@idCliente, @nm_Pagamento)", con.MyConectarBD());

            cmd.Parameters.Add("@idCliente", MySqlDbType.VarChar).Value = idCli;
            cmd.Parameters.Add("@nm_Pagamento", MySqlDbType.VarChar).Value = Tipo_Pagamento;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

    }
}