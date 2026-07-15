using Microsoft.Data.SqlClient;
using Projeto_01.Models;

// ABRIR CONEXÃO COM O BANCO DE DADOS
var connectionString = "Server=localhost\\sqlexpress;Database=Escola;Trusted_Connection=True;Encrypt=False;";
using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();


Alunos alunos = new Alunos();

int opcao;
bool exibirMenu = true;

// LOOP PARA EXIBIR O MENU ENQUANTO A VARIÁVEL [exibirMenu] FOR VERDADEIRA 
while (exibirMenu)
{
    Console.Clear();

    Console.WriteLine("<Menu de cadastro alunos>");
    Console.WriteLine("<Selecione a opção desejada>");
    Console.WriteLine("[1] Cadastrar Aluno.");
    Console.WriteLine("[2] Exibir Alunos.");
    Console.WriteLine("[3] Excluir Aluno.");
    Console.WriteLine("[4] Atualizar Aluno.");
    Console.WriteLine("[5] Sair.");

    opcao = Convert.ToInt32(Console.ReadLine());

    switch (opcao)
    {
        case 1: alunos.CadastrarAluno(); break;

        case 2: alunos.ExibirAlunos(); break;

        case 3: alunos.ExcluirALuno(); break;

        case 4: alunos.AtulizarAluno(); break;

        case 5: Console.WriteLine("Sair");
                exibirMenu = false;
                break;

        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

    // PERGUNTA AO USUÁRIO SE DESEJA CONTINUAR OU SAIR DO MENU (CASO NÃO SEJA A OPÇÃO 4)
    if (opcao != 5)
    {
        Console.WriteLine("Quer continuar? (S/N)");
        string resposta = Console.ReadLine();

        if (resposta.ToUpper() != "S")
            {
                exibirMenu = false;
            }
        else
            {
                exibirMenu = true;
            }
    }
}

Console.WriteLine("Saindo do sistema...");
    Environment.Exit(0);