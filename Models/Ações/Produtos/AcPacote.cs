using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AspViagens.Dados;
using AspViagens.Models.Banco;
using Asp_Viagens.Models.Banco.Produtos;

namespace AspViagens.Models.Ações
{
    public class AcPacote
    {
        // instanciando a classe conexão
        Conexao con = new Conexao();
        Plano CadPlano = new Plano();

        public void InserirPacote(Pacote cp,Plano cl,string dsPreco)
        {
            // criando a conexão  com o banco de dados e fazendo o select fazendo validação na classe conexão
            MySqlCommand cmd = new MySqlCommand("call sp_InsPlanosPacotes(@nmPacote,@dsPreco,@dsPacote,@Imagem,@nmPais,@nmCidade,@dsCidade,@dsClima,@dsEstadiaI,@dsEstadiaT,@dsEstadiaM,@dsMoeda,@dsIdiomaP,@dsIdiomaS);", con.MyConectarBD());
            //parametros do usuario e senha e recebendo os campos da model
            cmd.Parameters.Add("@nmPacote", MySqlDbType.VarChar).Value = cp.nmPacote;
            cmd.Parameters.Add("@dsPreco", MySqlDbType.VarChar).Value = dsPreco;
            cmd.Parameters.Add("@dsPacote", MySqlDbType.VarChar).Value = cp.dsPacote;
            cmd.Parameters.Add("@Imagem", MySqlDbType.VarChar).Value = cp.Imagem;
            cmd.Parameters.Add("@nmPais", MySqlDbType.VarChar).Value = cl.nmPais;
            cmd.Parameters.Add("@nmCidade", MySqlDbType.VarChar).Value = cl.nmCidade;
            cmd.Parameters.Add("@dsCidade", MySqlDbType.VarChar).Value = cl.dsCidade;
            cmd.Parameters.Add("@dsClima", MySqlDbType.VarChar).Value = cl.dsClima;
            cmd.Parameters.Add("@dsEstadiaI", MySqlDbType.VarChar).Value = cl.dsEstadiaInicio;
            cmd.Parameters.Add("@dsEstadiaT", MySqlDbType.VarChar).Value = cl.dsEstadiaTermino;
            cmd.Parameters.Add("@dsEstadiaM", MySqlDbType.VarChar).Value = cl.dsEstadiaTotal;
            cmd.Parameters.Add("@dsMoeda", MySqlDbType.VarChar).Value = cl.dsMoeda;
            cmd.Parameters.Add("@dsIdiomaP", MySqlDbType.VarChar).Value = cl.dsIdiomaP;
            cmd.Parameters.Add("@dsIdiomaS", MySqlDbType.VarChar).Value = cl.dsIdiomaS;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void DeletarPacote(string id)
        {
            
            MySqlCommand cmd = new MySqlCommand("Call sp_DeletePacote(@idPacote)", con.MyConectarBD());
            cmd.Parameters.Add("@idPacote", MySqlDbType.VarChar).Value = id;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
           
        }

        public List<Pacote> ListarPacote()
        {
            List<Pacote> Pacote = new List<Pacote>();
            MySqlCommand cmd = new MySqlCommand("call sp_ConsPacote();", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Pacote.Add(
                    new Pacote
                    {
                        IdPacote = Convert.ToString(dr["IdPacote"]),
                        nmPacote = Convert.ToString(dr["nmPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        dsPacote = Convert.ToString(dr["dsPacote"]),
                        Imagem = Convert.ToString(dr["Imagem"])
                    }
                    );
            }
            return Pacote;
        }

        public void EditarPacote(string id, Pacote itens, string dsPreco)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_UpPacoteId(@nmPacote,@dsPreco,@dsPacote,@Imagem,@idPacote);",
            con.MyConectarBD());
            cmd.Parameters.Add("@idPacote", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@nmPacote", MySqlDbType.VarChar).Value = itens.nmPacote;
            cmd.Parameters.Add("@dsPreco", MySqlDbType.VarChar).Value = dsPreco;
            cmd.Parameters.Add("@dsPacote", MySqlDbType.VarChar).Value = itens.dsPacote;
            cmd.Parameters.Add("@Imagem", MySqlDbType.VarChar).Value = itens.Imagem;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public Pacote SelecionaCadPacote(string id, Pacote cadPacote)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where idPacote = @cod", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    {
                        cadPacote.IdPacote = Convert.ToString(leitor["idPacote"]);
                        cadPacote.nmPacote = Convert.ToString(leitor["nmPacote"]);
                        cadPacote.dsPreco = Convert.ToString(leitor["dsPreco"]);
                        cadPacote.dsPacote = Convert.ToString(leitor["dsPacote"]);
                        cadPacote.Imagem = Convert.ToString(leitor["Imagem"]);

                    }
                }
            }
            else
            {
                cadPacote.IdPacote = null;
                cadPacote.nmPacote = null;
                cadPacote.dsPreco = null;
                cadPacote.dsPacote = null;
                cadPacote.Imagem = null;
            }
            con.MyDesConectarBD();
            return cadPacote;

        }
        public List<Pacote> SelecionaEducacaoPacote()
        {
            List<Pacote> cadPacote = new List<Pacote>();
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where nmPacote = 'Educação';", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cadPacote.Add(
                    new Pacote
                    {
                        IdPacote = Convert.ToString(dr["idPacote"]),
                        nmPacote = Convert.ToString(dr["nmPacote"]),
                        dsPacote = Convert.ToString(dr["dsPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        Imagem = Convert.ToString(dr["Imagem"])
                    }
                    );
            }
            return cadPacote;
        }
        public List<Pacote> SelecionaEntretenimentoPacote()
        {
            List<Pacote> cadPacote = new List<Pacote>();
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where nmPacote = 'Entretenimento';", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cadPacote.Add(
                    new Pacote
                    {
                        IdPacote = Convert.ToString(dr["idPacote"]),
                        nmPacote = Convert.ToString(dr["nmPacote"]),
                        dsPacote = Convert.ToString(dr["dsPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        Imagem = Convert.ToString(dr["Imagem"])
                    }
                    );
            }
            return cadPacote;
        }
        public List<Pacote> SelecionaProfissionalPacote()
        {
            List<Pacote> cadPacote = new List<Pacote>();
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where nmPacote = 'Profissional';", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cadPacote.Add(
                    new Pacote
                    {
                        IdPacote = Convert.ToString(dr["idPacote"]),
                        nmPacote = Convert.ToString(dr["nmPacote"]),
                        dsPacote = Convert.ToString(dr["dsPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        Imagem = Convert.ToString(dr["Imagem"])
                    }
                    );
            }
            return cadPacote;
        }
        public List<Pacote> SelecionaidPacote()
        {
            List<Pacote> cadPacote = new List<Pacote>();
            MySqlCommand cmd = new MySqlCommand("select idPacote from tbl_ItensCompra;", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cadPacote.Add(
                    new Pacote
                    {
                        IdPacote = Convert.ToString(dr["idPacote"])
                    }
                    );
            }
            return cadPacote;
        }

        public List<Pacote> consultaGrafico(List<Pacote> minhaLista)
        {
            List<Pacote> pacoteList = new List<Pacote>();

            foreach (var item in minhaLista)
            {

                MySqlCommand cmd = new MySqlCommand("call sp_consultaGrafico(@cod)", con.MyConectarBD());
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = item.IdPacote;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                con.MyDesConectarBD();

                foreach (DataRow dr in dt.Rows)
                {
                    pacoteList.Add(
                        new Pacote
                        {
                            dsPreco = Convert.ToString(dr["dsPreco"]),
                            DataComp = Convert.ToString(dr["dtComp"])
                        });
                }
            }
            return pacoteList;
        }

        public List<Pacote> consultaGrafico3(List<Pacote> minhaLista)
        {
            List<Pacote> pacoteList = new List<Pacote>();

            foreach (var item in minhaLista)
            {

                MySqlCommand cmd = new MySqlCommand("call sp_consultaGrafico3(@cod)", con.MyConectarBD());
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = item.IdPacote;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                con.MyDesConectarBD();

                foreach (DataRow dr in dt.Rows)
                {
                    pacoteList.Add(
                        new Pacote
                        {
                            DataComp = Convert.ToString(dr["dtComp"]),
                        });
                }
            }
            return pacoteList;
        }
        public List<Pacote> consultaGrafico2(List<Pacote> minhaLista)
        {
            List<Pacote> pacoteList = new List<Pacote>();

            foreach (var item in minhaLista)
            {

                MySqlCommand cmd = new MySqlCommand("call sp_consultaGrafico2(@cod)", con.MyConectarBD());
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = item.IdPacote;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                con.MyDesConectarBD();

                foreach (DataRow dr in dt.Rows)
                {
                    pacoteList.Add(
                        new Pacote
                        {
                            dsPreco = Convert.ToString(dr["dsPreco"]),
                        });
                }
            }
            return pacoteList;
        }
        public List<modelPacote> selecionaPlano()
        {
            List<modelPacote> PlanosList = new List<modelPacote>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Plano ", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                PlanosList.Add(
                    new modelPacote
                    {
                        idPacote = Convert.ToString(dr["idPlano"]),
                        nomePacote = Convert.ToString(dr["nmPlano"]),
                        imagem = Convert.ToString(dr["Imagem"])
                    });
            }
            return PlanosList;
        }

        public List<modelPacote> selecionaConsPacote(string id)
        {
            List<modelPacote> pacoteList = new List<modelPacote>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where idPacote = @cod", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                pacoteList.Add(
                    new modelPacote
                    {
                        idPacote = Convert.ToString(dr["idPacote"]),
                        nomePacote = Convert.ToString(dr["nmPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        imagem = Convert.ToString(dr["Imagem"])
                    });
            }
            return pacoteList;
        }

        public List<Pacote> selecionaConsPacoteList(List<modelPacote> minhaLista2)
        {
            List<Pacote> pacoteList = new List<Pacote>();

            foreach (var item in minhaLista2)
            {

                MySqlCommand cmd = new MySqlCommand("select * from tbl_Pacote where idPacote = @cod", con.MyConectarBD());
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = item.idPacote;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                con.MyDesConectarBD();

                foreach (DataRow dr in dt.Rows)
                {
                    pacoteList.Add(
                        new Pacote
                        {
                            IdPacote = Convert.ToString(dr["IdPacote"]),
                            nmPacote = Convert.ToString(dr["nmPacote"]),
                            dsPacote = Convert.ToString(dr["dsPacote"]),
                            dsPreco = Convert.ToString(dr["dsPreco"]),
                            Imagem = Convert.ToString(dr["Imagem"])
                        });
                }
            }
            return pacoteList;
        }
        public List<FiltrarPlanoPacote> FiltrarPlanoPacote(FiltrarPlanoPacote Fpl)
        {
            List<FiltrarPlanoPacote> FiltrarPlanoPacote = new List<FiltrarPlanoPacote>();

            MySqlCommand cmd = new MySqlCommand("call sp_FiltrarPlanos(@p_nmPais,@p_valor1,@p_valor2,@p_interesse);", con.MyConectarBD());
            cmd.Parameters.Add("@p_nmPais", MySqlDbType.VarChar).Value = Fpl.nmPais;
            cmd.Parameters.Add("@p_valor1", MySqlDbType.VarChar).Value = Fpl.valor1;
            cmd.Parameters.Add("@p_valor2", MySqlDbType.VarChar).Value = Fpl.valor2;
            cmd.Parameters.Add("@p_interesse", MySqlDbType.VarChar).Value = Fpl.nmPacote;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                FiltrarPlanoPacote.Add(
                    new FiltrarPlanoPacote
                    {
                        IdPacote = Convert.ToString(dr["idPacote"]),
                        nmPacote = Convert.ToString(dr["nmPacote"]),
                        dsPreco = Convert.ToString(dr["dsPreco"]),
                        dsPacote = Convert.ToString(dr["dsPacote"]),
                        Imagem = Convert.ToString(dr["Imagem"]),
                        nmPais = Convert.ToString(dr["nmPais"]),
                    });
            }
            return FiltrarPlanoPacote;
        }

    }

}