using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Projeto_01.Models
{
    public class Alunos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Curso { get; set; }

        public void CadastrarAluno()
        {
            // ABRIR CONEXÃO COM O BANCO DE DADOS
            string connectionString = "Server=localhost\\sqlexpress;Database=Escola;Trusted_Connection=True;Encrypt=False;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            Console.WriteLine("<Cadastrar Aluno>");
            Console.WriteLine("Digite o nome do aluno:");
            Nome = Console.ReadLine();

            Console.WriteLine("Digite a idade do aluno:");
            Idade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o curso do aluno:");
            Curso = Console.ReadLine();

            Console.WriteLine($"Aluno cadastrado com sucesso: Nome: {Nome}, Idade: {Idade}, Curso: {Curso}");

            // INSERIR DADOS NO BANCO DE DADOS
            try
            {
                using var command = new SqlCommand("INSERT INTO Alunos (Nome, Idade, Curso) Values (@Nome, @Idade, @Curso)", connection, transaction);
                command.Parameters.AddWithValue("@Nome", Nome);
                command.Parameters.AddWithValue("@Idade", Idade);
                command.Parameters.AddWithValue("@Curso", Curso);
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ExibirAlunos()
        {
            // ABRIR CONEXÃO COM O BANCO DE DADOS
            string connectionString = "Server=localhost\\sqlexpress;Database=Escola;Trusted_Connection=True;Encrypt=False;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            Console.WriteLine("<Exibir Alunos>");

            // CONSULTAR DADOS NO BANCO DE DADOS
            using var command = new SqlCommand("SELECT * FROM Alunos", connection);
            using var reader = command.ExecuteReader();

            // EXIBIR DADOS NO CONSOLE
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}, Curso: {reader["Curso"]}, Data de Cadastro: {reader["DataCadastro"]}, Ultima Atualização: {reader["UltimaAtualizacao"]}");
            }
        }

        public void ExcluirALuno()
        {
            // ABRIR CONEXÃO COM O BANCO DE DADOS
            string connectionString = "Server=localhost\\sqlexpress;Database=Escola;Trusted_Connection=True;Encrypt=False;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            Console.WriteLine("<Excluir Aluno>");
            Console.WriteLine("Digite o ID do aluno que deseja excluir:");
            Id = Convert.ToInt32(Console.ReadLine());

            // EXCLUIR DADOS NO BANCO DE DADOS SELECIONANDO PELO ID INFORMADO PELO USUÁRIO
            using var command = new SqlCommand("DELETE FROM Alunos WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", Id);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Aluno excluído com sucesso. Linhas afetadas: {rowsAffected}");
        }

        public void AtulizarAluno()
        {
            // ABRIR CONEXÃO COM O BANCO DE DADOS
            string connectionString = "Server=localhost\\sqlexpress;Database=Escola;Trusted_Connection=True;Encrypt=False;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // ADICIONANDO TRANSAÇÃO PARA GARANTIR QUE A ATUALIZAÇÃO SEJA EFETUADA CORRETAMENTE
            using var transaction = connection.BeginTransaction();

            Console.WriteLine("<Atualizar Aluno>");
            Console.WriteLine("Digite o ID do aluno que deseja atualizar:");
            Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o novo nome do aluno:");
            Nome = Console.ReadLine();

            Console.WriteLine("Digite a nova idade do aluno:");
            Idade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o novo curso do aluno:");
            Curso = Console.ReadLine();

            DateTime dateTime = DateTime.Now;

            // ATUALIZAR DADOS NO BANCO DE DADOS SELECIONANDO PELO ID INFORMADO PELO USUÁRIO COM COMANDO SQL UPDATE
            try
            {
                using var command = new SqlCommand("UPDATE Alunos SET Nome = @Nome, Idade = @Idade, Curso = @Curso, UltimaAtualizacao = @UltimaAtualizacao WHERE Id = @Id", connection, transaction);
            command.Parameters.AddWithValue("@Nome", Nome);
            command.Parameters.AddWithValue("@Idade", Idade);
            command.Parameters.AddWithValue("@Curso", Curso);
            command.Parameters.AddWithValue("@UltimaAtualizacao", dateTime);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
                transaction.Commit();     
            Console.WriteLine($"Aluno atualizado com sucesso: Nome: {Nome}, Idade: {Idade}, Curso: {Curso}");    
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}