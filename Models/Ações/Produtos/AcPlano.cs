using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspViagens.Dados;
using AspViagens.Models.Banco;

namespace AspViagens.Models.Ações
{
    public class AcPlano
    {

        Conexao con = new Conexao();
        public List<Plano> SaibaMaisPlano(string Pais)
        {
            List<Plano> cadPlano = new List<Plano>();
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Plano where nmPais = @nmPais;", con.MyConectarBD());
            cmd.Parameters.Add("@nmPais", MySqlDbType.VarChar).Value = Pais;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cadPlano.Add(
                    new Plano
                    {
                        idPlano = Convert.ToString(dr["idPlano"]),
                        nmCidade = Convert.ToString(dr["nmCidade"]),
                        nmPais = Convert.ToString(dr["nmPais"]),
                        dsCidade = Convert.ToString(dr["dsCidade"]),
                        dsClima = Convert.ToString(dr["dsClima"]),
                        dsEstadiaInicio = Convert.ToString(dr["dsEstadiaInicio"]),
                        dsEstadiaTermino = Convert.ToString(dr["dsEstadiaTermino"]),
                        dsEstadiaTotal = Convert.ToString(dr["dsEstadiaTotal"]),
                        dsIdiomaP = Convert.ToString(dr["dsIdiomaP"]),
                        dsIdiomaS = Convert.ToString(dr["dsIdiomaS"]),
                        dsMoeda = Convert.ToString(dr["dsMoeda"]),
                    }
                    );
            }
            return cadPlano;
        }

        public Plano ConsultaIdPlano(string id,Plano plano)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Plano where idPacote = @idPacote;", con.MyConectarBD());
            cmd.Parameters.Add("@idPacote", MySqlDbType.VarChar).Value = id;
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    {
                        plano.idPlano = Convert.ToString(leitor["idPlano"]);
                        plano.nmPais = Convert.ToString(leitor["nmPais"]);
                        plano.nmCidade = Convert.ToString(leitor["nmCidade"]);
                        plano.dsCidade = Convert.ToString(leitor["dsCidade"]);
                        plano.dsClima = Convert.ToString(leitor["dsClima"]);
                        plano.dsEstadiaInicio = Convert.ToString(leitor["dsEstadiaInicio"]);
                        plano.dsEstadiaTermino = Convert.ToString(leitor["dsEstadiaTermino"]);
                        plano.dsEstadiaTotal = Convert.ToString(leitor["dsEstadiaTotal"]);
                        plano.dsMoeda = Convert.ToString(leitor["dsMoeda"]);
                        plano.dsIdiomaP = Convert.ToString(leitor["dsIdiomaP"]);
                        plano.dsIdiomaS = Convert.ToString(leitor["dsIdiomaS"]);
                    }
                }
            }
            else
            {
                plano.idPlano = null;
            }
            con.MyDesConectarBD();
            return plano;
        }

        public void DeletarPlanoId(Plano plano)
        {
            MySqlCommand cmd = new MySqlCommand("Call sp_DeletePlano(@idPlano)", con.MyConectarBD());
            cmd.Parameters.Add("@idPlano", MySqlDbType.VarChar).Value = plano.idPlano;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void EditarPlano(string id, Plano itens)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_UpPlanoId(@idPlano,@nmPais,@nmCidade,@dsCidade,@dsClima,@dsEstadiaInicio,@dsEstadiaTermino,@dsEstadiaTotal,@dsMoeda,@dsIdiomaP,@dsIdiomaS);",
            con.MyConectarBD());           
            cmd.Parameters.Add("@idPlano", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@nmPais", MySqlDbType.VarChar).Value = itens.nmPais;
            cmd.Parameters.Add("@nmCidade", MySqlDbType.VarChar).Value = itens.nmCidade;
            cmd.Parameters.Add("@dsCidade", MySqlDbType.VarChar).Value = itens.dsCidade;
            cmd.Parameters.Add("@dsClima", MySqlDbType.VarChar).Value = itens.dsClima;
            cmd.Parameters.Add("@dsEstadiaInicio", MySqlDbType.VarChar).Value = itens.dsEstadiaInicio;
            cmd.Parameters.Add("@dsEstadiaTermino", MySqlDbType.VarChar).Value = itens.dsEstadiaTermino;
            cmd.Parameters.Add("@dsEstadiaTotal", MySqlDbType.VarChar).Value = itens.dsEstadiaTotal;
            cmd.Parameters.Add("@dsMoeda", MySqlDbType.VarChar).Value = itens.dsMoeda;
            cmd.Parameters.Add("@dsIdiomaP", MySqlDbType.VarChar).Value = itens.dsIdiomaP;
            cmd.Parameters.Add("@dsIdiomaS", MySqlDbType.VarChar).Value = itens.dsIdiomaS;
            
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

    }
}