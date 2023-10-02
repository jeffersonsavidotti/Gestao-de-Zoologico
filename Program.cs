using System;
using System.Data;
using zoologico;

namespace zoologico
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Criando uma nova instância de DataTable
                DataTable dt = new DataTable();

                // Obtendo dados de veterinários do banco de dados
                dt = DALZoologico.GetVeterinariosDataTable();

                // Loop para iterar sobre as linhas e colunas do DataTable
                // Comentado para não imprimir os resultados no console
                /*
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(col.ColumnName + ": " + row[col]);
                    }
                    Console.WriteLine();
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            try
            {
                // Criando uma nova instância de Comandos
                Consultas comandos = new Consultas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            // [MENU PRINCIPAL]
            int escolhainicial = -1;

            while (escolhainicial != 0)
            {
                Console.WriteLine("Escolha uma operação:");
                Console.WriteLine("1 - Veterinários");
                Console.WriteLine("2 - Animais");
                Console.WriteLine("3 - Visitantes");
                Console.WriteLine("4 - Administradores");
                Console.WriteLine("0 - Sair");

                escolhainicial = int.Parse(Console.ReadLine());

                switch (escolhainicial)
                {
                    case 1:
                        // [MENU VETERINARIO]
                        int escolha = -1;

                        while (escolha != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("1 - Inserir Veterinário");
                            Console.WriteLine("2 - Deletar Veterinário");
                            Console.WriteLine("3 - Atualizar Nome do Veterinário");
                            Console.WriteLine("4 - Consultar Nome do Veterinário");
                            Console.WriteLine("5 - Exibir Tabela Veterinário");
                            Console.WriteLine("0 - Voltar");

                            escolha = int.Parse(Console.ReadLine());

                            switch (escolha)
                            {
                                case 1:
                                    Console.Clear();
                                    // Inserção de dados de veterinário
                                    DALZoologico.InserirVeterinario();
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    // Deleção de dados de veterinário
                                    DALZoologico.DeletarVeterinario();
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.Clear();
                                    // Atualização de nome de veterinário
                                    DALZoologico.AtualizarNomeVeterinario();
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();    
                                    // Consulta de veterinários com parâmetros
                                    DALZoologico.GetVeterinariosComParametro();
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    Console.Clear();
                                    // Exibir tabela veterinários com parâmetros
                                    Consultas.ConsultarVet();
                                    Console.WriteLine("");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida. Tente novamente.");
                                    break;
                            }
                        }
                        break;

                    case 2:
                        // [MENU ANIMAL]
                        int escolha1 = -1;

                        while (escolha1 != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("1 - Inserir Animal");
                            Console.WriteLine("2 - Deletar Animal");
                            Console.WriteLine("3 - Atualizar Nome do Animal");
                            Console.WriteLine("4 - Consultar Nome do Animal");
                            Console.WriteLine("5 - Exibir tabela Animal");
                            Console.WriteLine("0 - Voltar");

                            escolha1 = int.Parse(Console.ReadLine());

                            switch (escolha1)
                            {
                                case 1:
                                    Console.Clear();
                                    // Inserção de dados de animal
                                    DALZoologico.InserirAnimal();
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    // Deleção de dados de animal
                                    DALZoologico.DeletarAnimal();
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.Clear();
                                    // Atualização de nome de animal
                                    DALZoologico.AtualizarNomeAnimal();
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();
                                    // Consulta de animais com parâmetros
                                    DALZoologico.GetAnimaisComParametro();
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    Console.Clear();
                                    // Exibir tabela animais com parâmetros
                                    Consultas.Consultar_Animal();
                                    Console.WriteLine("");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida. Tente novamente.");
                                    break;
                            }
                        }
                        break;

                    case 3:
                        // [MENU VISITANTE]
                        int escolha2 = -1;

                        while (escolha2 != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("1 - Inserir Visitante");
                            Console.WriteLine("2 - Deletar Visitante");
                            Console.WriteLine("3 - Atualizar Nome do Visitante");
                            Console.WriteLine("4 - Consultar Nome do Visitante");
                            Console.WriteLine("5 - Exibir tabela de Visitantes");
                            Console.WriteLine("0 - Voltar");

                            escolha2 = int.Parse(Console.ReadLine());

                            switch (escolha2)
                            {
                                case 1:
                                    Console.Clear();
                                    // Inserção de dados de visitante
                                    DALZoologico.InserirVisitante();
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    // Deleção de dados de visitante
                                    DALZoologico.DeletarVisitante();
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.Clear();
                                    // Atualização de nome de visitante
                                    DALZoologico.AtualizarNomeVisitante();
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();
                                    // Consulta de visitantes com parâmetros
                                    DALZoologico.GetVisitantesComParametro();
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    Console.Clear();
                                    // Exibir tabela visitantes com parâmetros
                                    Consultas.ConsultarVis();
                                    Console.WriteLine("");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida. Tente novamente.");
                                    break;
                            }
                        }
                        break;

                    case 4:
                        // [MENU ADMINISTRADOR]
                        int escolha3 = -1;

                        while (escolha3 != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("1 - Inserir Administrador");
                            Console.WriteLine("2 - Deletar Administrador");
                            Console.WriteLine("3 - Atualizar Nome do Administrador");
                            Console.WriteLine("4 - Consultar Nome do Administrador");
                            Console.WriteLine("5 - Exibir tabela Administradores");
                            Console.WriteLine("0 - Sair");

                            escolha3 = int.Parse(Console.ReadLine());

                            switch (escolha3)
                            {
                                case 1:
                                    Console.Clear();
                                    // Inserção de dados de administrador
                                    DALZoologico.InserirAdministrador();
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    // Deleção de dados de administrador
                                    DALZoologico.DeletarAdministrador();
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.Clear();
                                    // Atualização de nome de administrador
                                    DALZoologico.AtualizarNomeAdministrador();
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();
                                    // Consulta de administradores com parâmetros
                                    DALZoologico.GetAdministradoresComParametro();
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    Console.Clear();
                                    // Exibir tabela administradores com parâmetros
                                    Consultas.ConsultarAdm();
                                    Console.WriteLine("");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida. Tente novamente.");
                                    break;
                            }
                        }
                        break;

                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}
