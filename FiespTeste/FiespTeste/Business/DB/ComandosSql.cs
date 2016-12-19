using FiespTeste.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FiespTeste.Business.DB
{
    public class ComandosSql
    {
        private SqlConnection conexao { get; set; }
        public ComandosSql()
        {
            conexao = new SqlConnection("Data Source=localhost\\sqlexpress;Initial Catalog=Teste;User ID=usr_teste;Password=fiesp");            
        }

        /// <summary>
        ///     Faz abertura da conexao
        /// </summary>
        public void AbrirConexao()
        {
            conexao.Open();
        }

        /// <summary>
        ///     Efetua fechamento da conexao
        /// </summary>
        public void FecharConexao()
        {
            conexao.Close();
        }

        /// <summary>
        ///     Salva a empresa
        /// </summary>
        /// <param name="model"></param>
        public void Salvar(EmpresaModels model)
        {
            using (var conn = conexao)
            {
                AbrirConexao();
                var sql = conn.CreateCommand();
                sql.CommandText = "spi_InserirEmpresa";
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                sql.Parameters.AddWithValue("@Faturamento", model.Faturamento);
                sql.Parameters.AddWithValue("@Expectativa", model.ExpectativaVenda);
                sql.Parameters.AddWithValue("@Vendedor", model.Vendedor);

                var dataReader = sql.ExecuteNonQuery();
                FecharConexao();
            }
        }

        /// <summary>
        ///     Recupera o vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PessoaModels RecuperarVendedor(int id)
        {
            var vendedor = new PessoaModels();

            using (var conn = conexao)
            {
                AbrirConexao();
                var sql = conn.CreateCommand();
                sql.CommandText = "spl_Pessoa";
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@pes_cod", id);

                var dataReader = sql.ExecuteReader();
                while (dataReader.Read())
                {
                    vendedor.Telefone = dataReader.GetString(2);
                    vendedor.Regiao = dataReader.GetString(3);
                }
                FecharConexao();
            }

            return vendedor;
        }

        /// <summary>
        ///     Recupera todos vendedores
        /// </summary>
        /// <returns></returns>
        public IList<PessoaModels> RecuperarVendedores()
        {
            var vendedores = new List<PessoaModels>();

            using (var conn = conexao)
            {
                AbrirConexao();
                var sql = conn.CreateCommand();
                sql.CommandText = "spl_Pessoa_Nome";
                sql.CommandType = CommandType.StoredProcedure;
                var dataReader = sql.ExecuteReader();
                while (dataReader.Read())
                {
                    var vendedor = new PessoaModels();

                    vendedor.Codigo = dataReader.GetInt32(0);
                    vendedor.Nome = dataReader.GetString(1);
                    
                    vendedores.Add(vendedor);
                }
                FecharConexao();
            }

            return vendedores;
        }
    }
}
