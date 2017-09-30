using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Agenda
{
    class ContatoDao
    {
        //private String conexao = @"Server=.\mssqlserver1;Database=Agenda;Trusted_Connection=True;";
        private String conexao = @"Server=.\sqlexpress;Database=Agenda;Trusted_Connection=True;";

        public void Inserir(Contato c)
        {
            String sql = "INSERT INTO CONTATOS ( NOME,EMAIL,FONECEL,FONERES,FONECOM,ENDER,BAIRRO,CIDADE,UF,CEP) "
                        + "VALUES ('" + c.Nome + "','" + c.Email + "','" + c.FoneCel + "','" + c.FoneRes + "','" +
                                          c.FoneCom + "','" + c.Ender + "','" + c.Bairro + "','" + c.Cidade + "','" + c.Uf + "','" + c.Cep + "')";

            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Contato cadastrado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void Editar(Contato c)
        {
            String sql = "UPDATE CONTATOS SET NOME = '" + c.Nome + "', EMAIL = '" + c.Email + "', FONECEL = '" + c.FoneCel + "', FONERES = '" + c.FoneRes + "', "
                        + " FONECOM = '" + c.FoneCom + "', ENDER = '" + c.Ender + "', BAIRRO = '" + c.Bairro + "', CIDADE = '" + c.Cidade + "', UF = '" + c.Uf + "', CEP = '" + c.Cep + "' WHERE ID=" + c.Id;


            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Contato alterado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void excluir(int id)
        {
            string query = "DELETE FROM CONTATOS WHERE ID = " + id;
            SqlConnection con = new SqlConnection(conexao);
            try
            {

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Contato excluido com sucesso!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public List<Contato> Pesquisar(int tipo, String campo)
        {
            SqlConnection con = new SqlConnection(conexao);
            List<Contato> lista = new List<Contato>();
            String sql = "SELECT * FROM CONTATOS ";

            // tipo de pesquisa 1=nome, 2=email, 3=telefone
            if (tipo == 1)
            {
                sql += "where nome like '%" + campo + "%' ";
            }
            else if (tipo == 2)
            {
                sql += "where email like '" + campo + "%' ";
            }
            else if (tipo == 3)
            {
                sql += "where foneRes = '" + campo + "' or foneCel = '" + campo + "' or foneCom = '" + campo + "' ";
            }


            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Contato c = new Contato();
                    c.Id = Convert.ToInt32(sdr["id"]);
                    c.Nome = sdr["nome"].ToString();
                    c.Email = sdr["email"].ToString();
                    c.FoneCel = sdr["foneCel"].ToString();
                    c.FoneRes = sdr["foneRes"].ToString();
                    c.FoneCom = sdr["foneCom"].ToString();
                    c.Ender = sdr["ender"].ToString();
                    c.Bairro = sdr["bairro"].ToString();
                    c.Cidade = sdr["cidade"].ToString();
                    c.Uf = sdr["uf"].ToString();
                    c.Cep = sdr["cep"].ToString();
                    lista.Add(c);
                }
            }

            con.Close();
            return lista;
        }


        public List<Contato> listar()
        {
            SqlConnection con = new SqlConnection(conexao);
            List<Contato> lista = new List<Contato>();
            String sql = "SELECT * FROM CONTATOS";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Contato c = new Contato();
                    c.Id = Convert.ToInt32(sdr["id"]);
                    c.Nome = sdr["nome"].ToString();
                    c.Email = sdr["email"].ToString();
                    c.FoneCel = sdr["foneCel"].ToString();
                    c.FoneRes = sdr["foneRes"].ToString();
                    c.FoneCom = sdr["foneCom"].ToString();
                    c.Ender = sdr["ender"].ToString();
                    c.Bairro = sdr["bairro"].ToString();
                    c.Cidade = sdr["cidade"].ToString();
                    c.Uf = sdr["uf"].ToString();
                    c.Cep = sdr["cep"].ToString();
                    lista.Add(c);
                }
            }

            con.Close();
            return lista;
        }


    }
}
