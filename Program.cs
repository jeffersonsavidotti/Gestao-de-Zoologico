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
                Comandos comandos = new Comandos();
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

                escolhainicial = Convert.ToInt32(Console.ReadLine());

                switch (escolhainicial)
                {
                    case 1:
                        // [MENU VETERINARIO]
                        int escolha = -1;

                        while (escolha != 0)
                        {
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("5 - Inserir Veterinário");
                            Console.WriteLine("6 - Deletar Veterinário");
                            Console.WriteLine("7 - Atualizar Nome do Veterinário");
                            Console.WriteLine("8 - Consultar Nome do Veterinário");
                            Console.WriteLine("0 - Voltar");

                            escolha = Convert.ToInt32(Console.ReadLine());

                            switch (escolha)
                            {
                                case 5:
                                    // Inserção de dados de veterinário
                                    DALZoologico.InserirVeterinario();
                                    Comandos.InserirVet();
                                    break;
                                case 6:
                                    // Deleção de dados de veterinário
                                    DALZoologico.DeletarVeterinario();
                                    //Comandos.DeletarVet();
                                    break;
                                case 7:
                                    // Atualização de nome de veterinário
                                    DALZoologico.AtualizarNomeVeterinario();
                                    //Comandos.AtualizarVet();
                                    break;
                                case 8:
                                    // Consulta de veterinários com parâmetros
                                    DALZoologico.GetVeterinariosComParametro();
                                    //Comandos.ConsultarVet();
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
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("9 - Inserir Animal");
                            Console.WriteLine("10 - Deletar Animal");
                            Console.WriteLine("11 - Atualizar Nome do Animal");
                            Console.WriteLine("12 - Consultar Nome do Animal");
                            Console.WriteLine("0 - Voltar");

                            escolha1 = Convert.ToInt32(Console.ReadLine());

                            switch (escolha1)
                            {
                                case 9:
                                    // Inserção de dados de animal
                                    DALZoologico.InserirAnimal();
                                    //Comandos.InserirVet();
                                    break;
                                case 10:
                                    // Deleção de dados de animal
                                    DALZoologico.DeletarAnimal();
                                    //Comandos.DeletarVet();
                                    break;
                                case 11:
                                    // Atualização de nome de animal
                                    DALZoologico.AtualizarNomeAnimal();
                                    //Comandos.AtualizarVet();
                                    break;
                                case 12:
                                    // Consulta de animais com parâmetros
                                    DALZoologico.GetAnimaisComParametro();
                                    //Comandos.ConsultarVet();
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
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("13 - Inserir Visitante");
                            Console.WriteLine("14 - Deletar Visitante");
                            Console.WriteLine("15 - Atualizar Nome do Visitante");
                            Console.WriteLine("16 - Consultar Nome do Visitante");
                            Console.WriteLine("0 - Voltar");

                            escolha2 = Convert.ToInt32(Console.ReadLine());

                            switch (escolha2)
                            {
                                case 13:
                                    // Inserção de dados de visitante
                                    DALZoologico.InserirVisitante();
                                    //Comandos.InserirVis();
                                    break;
                                case 14:
                                    // Deleção de dados de visitante
                                    DALZoologico.DeletarVisitante();
                                    //Comandos.DeletarVis();
                                    break;
                                case 15:
                                    // Atualização de nome de visitante
                                    DALZoologico.AtualizarNomeVisitante();
                                    //Comandos.AtualizarVis();
                                    break;
                                case 16:
                                    // Consulta de visitantes com parâmetros
                                    DALZoologico.GetVisitantesComParametro();
                                    //Comandos.ConsultarVis();
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
                            Console.WriteLine("Escolha uma operação:");
                            Console.WriteLine("17 - Inserir Administrador");
                            Console.WriteLine("18 - Deletar Administrador");
                            Console.WriteLine("19 - Atualizar Nome do Administrador");
                            Console.WriteLine("20 - Consultar Nome do Administrador");
                            Console.WriteLine("0 - Sair");

                            escolha3 = Convert.ToInt32(Console.ReadLine());

                            switch (escolha3)
                            {
                                case 17:
                                    // Inserção de dados de administrador
                                    DALZoologico.InserirAdministrador();
                                    //Comandos.InserirAdm();
                                    break;
                                case 18:
                                    // Deleção de dados de administrador
                                    DALZoologico.DeletarAdministrador();
                                    //Comandos.DeletarAdm();
                                    break;
                                case 19:
                                    // Atualização de nome de administrador
                                    DALZoologico.AtualizarNomeAdministrador();
                                    //Comandos.AtualizarAdm();
                                    break;
                                case 20:
                                    // Consulta de administradores com parâmetros
                                    DALZoologico.GetAdministradoresComParametro();
                                    //Comandos.ConsultarAdm();
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
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}
