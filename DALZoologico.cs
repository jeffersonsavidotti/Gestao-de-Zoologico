using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Net;

namespace zoologico
{
    public class DALZoologico
    {
        // Caminho para o arquivo do banco de dados
        public static string path = Directory.GetCurrentDirectory() + "\\zoologico.db";
        // Exemplo do caminho final: "C:\Users\jeffe\source\repos\zoologico\bin\Debug\net6.0\zoologico.db"


        // Criação de uma propriedade para a conexão SQLite
        private static SQLiteConnection sqliteConnection;

        // Método privado para realizar a conexão com o banco de dados
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + path);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        // Método para obter os dados dos veterinários como DataTable
        public static DataTable GetVeterinariosDataTable()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM veterinarios";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
                throw;
            }
        }

        // Método para obter a lista de veterinários
        public static List<Veterinario> GetVeterinariosList()
        {
            List<Veterinario> veterinarios = new List<Veterinario>();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM veterinarios";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Veterinario veterinario = new Veterinario(Convert.ToInt32(reader["id"]), reader["nome"].ToString());
                            veterinarios.Add(veterinario);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
                return veterinarios;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL: " + ex);
                throw;
            }
        }

        // Método para inserir um veterinário
        public static void InserirVeterinario()
        {
            try
            {
                // Ler as informações do teclado do veterinário que o usuário quer inserir
                Console.WriteLine("Digite o id do veterinario que vai ser inserida: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o nome do veterinario que vai ser inserida: ");
                string nome = Console.ReadLine();

                Console.WriteLine("Deseja realmente inserir o veterinario(a)? ");
                Console.WriteLine("Digite (s) para inserir ou (n) para cancelar a inserção: ");
                string confirma = Console.ReadLine();

                if (confirma.ToLower() == "s")
                {
                    // Conexão com o banco de dados 
                    using (var cmd = DbConnection().CreateCommand())
                    {
                        // Criar a query (consulta) sql
                        cmd.CommandText = "INSERT INTO veterinarios (id, nome) VALUES (@id, @nome)";

                        // Definir os valores que vão ser substituídos pelos parâmetros
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", nome);

                        // Executar a query sql
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        // Informa o usuario:
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Inserção realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO VISITANTE INSERIDO ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                reader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi inserido.");
                        }
                    }
                }
                else if (confirma.ToLower() == "n")
                {
                    Console.WriteLine("Inserção Cancelada!!!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para deletar um veterinário
        public static void DeletarVeterinario()
        {
            try
            {
                // Ler as informações do teclado do veterinário que o usuário quer deletar
                Console.WriteLine("Digite o id do veterinario que vai ser deletada: ");
                int id = int.Parse(Console.ReadLine());

                    // Conexão com o banco de dados 
                    using (var cmd = DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM veterinarios WHERE id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("### DETALHES DO VETERINÁRIO ANTES DE ATUALIZAR ###");
                            Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                            Console.WriteLine(new string('-', 25));

                            while (reader.Read())
                            {
                                Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                            }
                            // Fecha o reader antes de modificar o CommandText
                            reader.Close();
                        }

                        Console.WriteLine("");

                    Console.WriteLine("Deseja realmente deletar o veterinario(a)? ");
                    Console.WriteLine("Digite (s) para deletar ou (n) para cancelar a deleção: ");
                    string confirma = Console.ReadLine();
                    
                    if (confirma.ToLower() == "s")
                    {
                        cmd.Parameters.Clear();
                            // Criar a query (consulta) sql
                            cmd.CommandText = "DELETE FROM veterinarios WHERE id = @id";

                            // Definir os valores que vão ser substituídos pelos parâmetros
                            cmd.Parameters.AddWithValue("@id", id);

                            // Executar a query sql
                            int rowsUpdated = cmd.ExecuteNonQuery();

                            if (rowsUpdated > 0)
                            {
                                Console.WriteLine("Deleção realizada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Nenhum registro foi atualizado.");
                            }
                        }
                        else if (confirma.ToLower() == "n")
                        {
                            Console.WriteLine("Atualização Cancelada!!!");
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. A atualização foi cancelada!");
                        }
                    }

                }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para atualizar o nome de um veterinário
        public static void AtualizarNomeVeterinario()
        {
            try
            {
                // Ler as informações do teclado do veterinário que o usuário quer atualizar
                Console.WriteLine("Digite o id do veterinario que vai ter o seu nome atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo nome do veterinario: ");
                string novoNome = Console.ReadLine();


                // Conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM veterinarios WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO VETERINÁRIO ANTES DE ATUALIZAR ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Deseja realmente alterar o nome do veterinario(a)? ");
                    Console.WriteLine("Digite (s) para alterar ou (n) para cancelar a alteração: ");
                    string confirma = Console.ReadLine();


                    if (confirma.ToLower() == "s")
                    {
                        cmd.Parameters.Clear();
                        // Criar a query (consulta) sql
                        cmd.CommandText = "UPDATE veterinarios SET nome = @nome WHERE id = @id";
                        // Definir os valores que vão ser substituídos pelos parâmetros
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", novoNome);

                        // Executar a query sql
                        int rowsUpdated = cmd.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Atualização realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM veterinarios WHERE id = @id";
                            using (var updateReader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO VETERINARIOS ATUALIZADOS ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (updateReader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", updateReader["id"], updateReader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                updateReader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi atualizado.");
                        }
                    }
                    else if (confirma.ToLower() == "n")
                    {
                        Console.WriteLine("Atualização Cancelada!!!");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. A atualização foi cancelada!");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
}

// Método para obter informações de um veterinário com um parâmetro específico
public static void GetVeterinariosComParametro()
        {
            try
            { 
                // Ler as informações do teclado do veterinário que o usuário quer consultar
                Console.WriteLine("Digite o id do veterinário que você quer saber o nome: ");
                int id = int.Parse(Console.ReadLine());

                // Conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    // Criar a query (consulta) sql
                    cmd.CommandText = "SELECT * FROM veterinarios WHERE id = @id";

                    // Definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);

                    // Executar a query sql
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Exibe um título na console indicando que os detalhes do veterinário serão exibidos
                        Console.WriteLine("### DETALHES DO VETERINÁRIO(A) ###");

                        // Exibe os cabeçalhos das colunas ID e Nome formatados para ocupar 5 e 25 caracteres, respectivamente
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");

                        // Exibe uma linha horizontal com 25 caracteres '-' para separar os cabeçalhos dos dados
                        Console.WriteLine(new string('-', 25));

                        // Enquanto houver dados a serem lidos do reader (resultados da consulta)
                        while (reader.Read())
                        {
                            // Exibe o ID e Nome do veterinário, formatados para ocupar 5 e 25 caracteres, respectivamente
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }

                        // Exibe uma linha em branco para separar os resultados da consulta de outras informações
                        Console.WriteLine("");

                        // Informa o usuário que a consulta foi realizada com sucesso
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Métodos para obter dados dos visitantes
        public static DataTable GetVisitantesDataTable()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM visitantes";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
                throw;
            }
        }

        // Método para obter uma lista de visitantes
        public static List<Visitante> GetVisitantesList()
        {
            List<Visitante> visitantes = new List<Visitante>();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM visitantes";
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### LISTA DE VISITANTES ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Visitante visitante = new Visitante(Convert.ToInt32(reader["id"]), reader["nome"].ToString());
                            visitantes.Add(visitante);

                            Console.WriteLine("{0, -5} | {1}", visitante.Id, visitante.Nome);
                        }
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
                return visitantes;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL: " + ex);
                throw;
            }
        }

        // Método para inserir um visitante
        public static void InserirVisitante()
        {
            try
            {
                // Ler as informações do teclado do visitante que o usuário quer inserir
                Console.WriteLine("Digite o id do visitante que vai ser inserida: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o nome do visitante que vai ser inserida: ");
                string nome = Console.ReadLine();

                Console.WriteLine("Deseja realmente inserir o visitante? ");
                Console.WriteLine("Digite (s) para inserir ou (n) para cancelar a inserção: ");
                string confirma = Console.ReadLine();

                if (confirma.ToLower() == "s")
                {
                    // Conexão com o banco de dados 
                    using (var cmd = DbConnection().CreateCommand())
                {
                    // Criar a query (consulta) sql
                    cmd.CommandText = "INSERT INTO visitantes (id, nome) VALUES (@id, @nome)";

                    // Definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);

                        // Executar a query sql
                    int rowsUpdated = cmd.ExecuteNonQuery();
                        // Informa o usuario:
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Inserção realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO VISITANTE INSERIDO ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                reader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi inserido.");
                        }
                    }
                }
                else if (confirma.ToLower() == "n")
                {
                    Console.WriteLine("Inserção Cancelada!!!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para deletar um visitante
        public static void DeletarVisitante()
        {
            try
            {
                // Ler as informações do teclado do visitante que o usuário quer deletar
                Console.WriteLine("Digite o id do visitante que vai ser deletada: ");
                int id = int.Parse(Console.ReadLine());

                // Conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO VISITANTE QUE DESEJA DELETAR ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Deseja realmente deletar o visitante? ");
                    Console.WriteLine("Digite (s) para deletar ou (n) para cancelar a deleção: ");
                    string confirma = Console.ReadLine();

                    if (confirma.ToLower() == "s")
                    {
                        // Reconfigurando os parâmetros para a nova query
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM visitantes WHERE id = @id";
                        cmd.Parameters.AddWithValue("@id", id);

                        // Executar a query sql
                        int rowsUpdated = cmd.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Deleção realizada com sucesso!");
                        }
                    }
                    else if (confirma.ToLower() == "n")
                    {
                        Console.WriteLine("Deleção Cancelada!!!");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. A Deleção foi cancelada!");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para atualizar o nome de um visitante
        public static void AtualizarNomeVisitante()
        {
            try
            {
                // Ler as informações do teclado do visitante que o usuário quer atualizar
                Console.WriteLine("Digite o id do visitante que vai ter o seu nome atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo nome do visitante: ");
                string novoNome = Console.ReadLine();

                // Conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO VISITANTE ANTES DE ATUALIZAR ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Deseja realmente alterar o nome do visitante? ");
                    Console.WriteLine("Digite (s) para alterar ou (n) para cancelar a alteração: ");
                    string confirma = Console.ReadLine();

                    if (confirma.ToLower() == "s")
                    {
                        // Reconfigurando os parâmetros para a nova query
                        cmd.Parameters.Clear();
                        cmd.CommandText = "UPDATE visitantes SET nome = @nome WHERE id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", novoNome);

                        // Executar a query sql
                        int rowsUpdated = cmd.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Atualização realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                            using (var updateReader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO VISITANTE ATUALIZADOS ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (updateReader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", updateReader["id"], updateReader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                updateReader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi atualizado.");
                        }
                    }
                    else if (confirma.ToLower() == "n")
                    {
                        Console.WriteLine("Atualização Cancelada!!!");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. A atualização foi cancelada!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }


        // Método para obter informações de um visitante com um parâmetro específico
        public static void GetVisitantesComParametro()
        {
            try
            {
                // Ler as informações do teclado do visitante que o usuário quer consultar
                Console.WriteLine("Digite o id do visitante que você quer saber o nome: ");
                int id = int.Parse(Console.ReadLine());

                // Conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    // Criar a query (consulta) sql
                    cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";

                    // Definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);

                    // Executar a query sql
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO VISITANTE ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        ///////////////////////////////////////ADMINISTRADORES\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public static DataTable GetAdministradoresDataTable()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                // Cria um comando de conexão com o banco de dados
                using (var cmd = DbConnection().CreateCommand())
                {
                    // Define a consulta SQL para selecionar todos os administradores
                    cmd.CommandText = "SELECT * FROM administradores";
                    // Cria um adaptador de dados para executar a consulta
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    // Preenche o DataTable com os resultados da consulta
                    da.Fill(dt);
                    return dt; // Retorna o DataTable preenchido
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
                throw;
            }
        }

        public static List<Administrador> GetAdministradoresList()
        {
            List<Administrador> administradores = new List<Administrador>();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM administradores";
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para a lista de administradores
                        Console.WriteLine("### LISTA DE ADMINISTRADORES ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            // Cria um novo objeto Administrador com os dados do banco
                            Administrador administrador = new Administrador(Convert.ToInt32(reader["id"]), reader["nome"].ToString());
                            // Adiciona o administrador à lista
                            administradores.Add(administrador);

                            // Imprime as informações do administrador
                            Console.WriteLine("{0, -5} | {1}", administrador.Id, administrador.Nome);
                        }
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
                return administradores;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL: " + ex);
                throw;
            }
        }

        // Método para inserir um novo administrador no banco de dados
        public static void InserirAdministrador()
        {
            try
            {
                // Lê as informações do teclado para o novo administrador
                Console.WriteLine("Digite o id do administrador que vai ser inserida: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o nome do administrador que vai ser inserida: ");
                string nome = Console.ReadLine();

                Console.WriteLine("Deseja realmente inserir o administrador(a)? ");
                Console.WriteLine("Digite (s) para inserir ou (n) para cancelar a inserção: ");
                string confirma = Console.ReadLine();

                if (confirma.ToLower() == "s")
                {
                    using (var cmd = DbConnection().CreateCommand())
                    {
                    // Define a consulta SQL para inserir um administrador
                    cmd.CommandText = "INSERT INTO administradores (id, nome) VALUES (@id, @nome)";
                    // Define os parâmetros com os valores fornecidos pelo usuário
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                        // Executa a consulta SQL
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        // Informa o usuario:
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Inserção realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM visitantes WHERE id = @id";
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO VISITANTE INSERIDO ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                reader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi inserido.");
                        }
                    }
                }
                else if (confirma.ToLower() == "n")
                {
                    Console.WriteLine("Inserção Cancelada!!!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para deletar um administrador do banco de dados
        public static void DeletarAdministrador()
        {
            try
            {
                // Lê o ID do administrador que o usuário deseja deletar
                Console.WriteLine("Digite o id do administrador(a) que vai ser deletada: ");
                int id = int.Parse(Console.ReadLine());

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM administradores WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO ADMINISTRADOR(A) ANTES DE ATUALIZAR ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Deseja realmente deletar o adminstrador(a)? ");
                    Console.WriteLine("Digite (s) para deletar ou (n) para cancelar a deleção: ");
                    string confirma = Console.ReadLine();

                    if (confirma.ToLower() == "s")
                    {
                        // Define a consulta SQL para deletar um administrador com o ID fornecido
                        // Reconfigurando os parâmetros para a nova query
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM administradores WHERE id = @id";
                        // Define o parâmetro com o valor do ID
                        cmd.Parameters.AddWithValue("@id", id);
                        // Executa a consulta SQL
                        int rowsUpdated = cmd.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Deleção realizada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi deletado.");
                        }
                    }
                    else if (confirma.ToLower() == "n")
                    {
                        Console.WriteLine("Deleção Cancelada!!!");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. A deleção foi cancelada!");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para atualizar o nome de um administrador no banco de dados
        public static void AtualizarNomeAdministrador()
        {
            try
            {
                // Lê o ID do administrador e o novo nome fornecido pelo usuário
                Console.WriteLine("Digite o id do administrador que vai ter o seu nome atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo nome do administrador: ");
                string novoNome = Console.ReadLine();

                    using (var cmd = DbConnection().CreateCommand())
                    {
                    // Reexecutar a consulta para exibir os detalhes atualizados
                    cmd.CommandText = "SELECT * FROM administradores WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("### DETALHES DO ADMINISTRADOR(A) ATUALIZADOS ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                    Console.WriteLine("");

                    Console.WriteLine("Deseja realmente alterar o nome do administrador(a)? ");
                    Console.WriteLine("Digite (s) para alterar ou (n) para cancelar a alteração: ");
                    string confirma = Console.ReadLine();

                    if (confirma.ToLower() == "s") 
                    {
                        // Reconfigurando os parâmetros para a nova query
                        cmd.Parameters.Clear();
                        // Define a consulta SQL para atualizar o nome do administrador com o ID fornecido
                        cmd.CommandText = "UPDATE administradores SET nome = @nome WHERE id = @id";
                        // Define os parâmetros com os valores fornecidos pelo usuário
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nome", novoNome);
                        // Executa a consulta SQL
                        int rowsUpdated = cmd.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Atualização realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM administradores WHERE id = @id";
                            using (var updateReader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("### DETALHES DO ADMINISTRADOR(A) ATUALIZADOS ###");
                                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                                Console.WriteLine(new string('-', 25));

                                while (updateReader.Read())
                                {
                                    Console.WriteLine("{0, -5} | {1}", updateReader["id"], updateReader["nome"]);
                                }
                                // Fecha o reader antes de modificar o CommandText
                                updateReader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi atualizado.");
                        }
                    }
                    else if (confirma.ToLower() == "n")
                    {
                        Console.WriteLine("Atualização Cancelada!!!");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. A atualização foi cancelada!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para obter informações de um administrador com um parâmetro específico (ID)
        public static void GetAdministradoresComParametro()
        {
            try
            {
                // Lê o ID do administrador que o usuário deseja consultar
                Console.WriteLine("Digite o id do administrador que você quer saber o nome: ");
                int id = int.Parse(Console.ReadLine());

                using (var cmd = DbConnection().CreateCommand())
                {
                    // Define a consulta SQL para selecionar um administrador com o ID fornecido
                    cmd.CommandText = "SELECT * FROM administradores WHERE id = @id";
                    // Define o parâmetro com o valor do ID
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para os detalhes do administrador
                        Console.WriteLine("### DETALHES DO ADMINISTRADOR(A) ###");
                        Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                        Console.WriteLine(new string('-', 25));

                        while (reader.Read())
                        {
                            // Imprime as informações do administrador
                            Console.WriteLine("{0, -5} | {1}", reader["id"], reader["nome"]);
                        }
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }


        /////////////////////////////////////// ANIMAIS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public static DataTable GetAnimaisDataTable()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                // Cria um comando de conexão com o banco de dados
                using (var cmd = DbConnection().CreateCommand())
                {
                    // Define a consulta SQL para selecionar todos os animais
                    cmd.CommandText = "SELECT * FROM animais";
                    // Cria um adaptador de dados para executar a consulta
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    // Preenche o DataTable com os resultados da consulta
                    da.Fill(dt);
                    return dt; // Retorna o DataTable preenchido
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
                throw;
            }
        }

        public static List<Animal> GetAnimaisList()
        {
            List<Animal> animais = new List<Animal>();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM animais";
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para a lista de animais
                        Console.WriteLine("### LISTA DE ANIMAIS ###");
                        Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                        Console.WriteLine(new string('-', 35));

                        while (reader.Read())
                        {
                            // Cria um novo objeto Animal com os dados do banco
                            Animal animal = new Animal(Convert.ToInt32(reader["id"]), reader["nome"].ToString(), reader["especie"].ToString());
                            // Adiciona o animal à lista
                            animais.Add(animal);

                            // Imprime as informações do animal
                            Console.WriteLine("{0, -5} | {1, -15} | {2}", animal.Id, animal.Nome, animal.Especie);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                    }
                }
                return animais;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL: " + ex);
                throw;
            }
        }

        // Método para inserir um novo animal no banco de dados
        public static void InserirAnimal()
        {
            try
            {
                // Lê as informações do teclado para o novo animal
                Console.WriteLine("Digite o id do animal que vai ser inserida: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o nome do animal que vai ser inserida: ");
                string nome = Console.ReadLine();
                Console.WriteLine("Digite a espécie do animal que vai ser inserida: ");
                string especie = Console.ReadLine();

                Console.WriteLine("Deseja realmente inserir o animal? ");
                Console.WriteLine("Digite (s) para inserir ou (n) para cancelar a inserção: ");
                string confirma = Console.ReadLine();

                if (confirma.ToLower() == "s")
                {
                    using (var cmd = DbConnection().CreateCommand())
                    {
                    // Define a consulta SQL para inserir um animal
                    cmd.CommandText = "INSERT INTO animais (id, nome, especie) VALUES (?, ?, ?)";

                    // Define os parâmetros com os valores fornecidos pelo usuário
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("nome", nome);
                    cmd.Parameters.AddWithValue("especie", especie);

                        // Executa a consulta SQL
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        // Informa o usuario:
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Inserção realizada com sucesso!");

                            // Reexecutar a consulta para exibir os detalhes atualizados
                            cmd.CommandText = "SELECT * FROM animais WHERE id = @id";
                            using (var reader = cmd.ExecuteReader())
                            {
                                // Imprime um cabeçalho para os detalhes do animal
                                Console.WriteLine("### DETALHES DO ANIMAL INSERIDO ###");
                                Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                                Console.WriteLine(new string('-', 35));

                                while (reader.Read())
                                {
                                    // Imprime as informações do animal
                                    Console.WriteLine("{0, -5} | {1, -15} | {2}", reader["id"], reader["nome"], reader["especie"]);
                                }
                                Console.WriteLine("");
                                // Fecha o reader antes de modificar o CommandText
                                reader.Close();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum registro foi inserido.");
                        }
                    }
                }
                else if (confirma.ToLower() == "n")
                {
                    Console.WriteLine("Inserção Cancelada!!!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para deletar um animal do banco de dados
        public static void DeletarAnimal()
        {
            try
            {
                // Lê o ID do animal que o usuário deseja deletar
                Console.WriteLine("Digite o id do animal que vai ser deletada: ");
                int id = int.Parse(Console.ReadLine());

                using (var cmd = DbConnection().CreateCommand())
                {
                    // Reexecutar a consulta para exibir os detalhes atualizados
                    cmd.CommandText = "SELECT * FROM animais WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para os detalhes do animal
                        Console.WriteLine("### DETALHES DO ANIMAL A SER DELETADO ###");
                        Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                        Console.WriteLine(new string('-', 35));

                        while (reader.Read())
                        {
                            // Imprime as informações do animal
                            Console.WriteLine("{0, -5} | {1, -15} | {2}", reader["id"], reader["nome"], reader["especie"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                        Console.WriteLine("");


                        Console.WriteLine("Deseja realmente deletar o animal? ");
                        Console.WriteLine("Digite (s) para deletar ou (n) para cancelar a deleção: ");
                        string confirma = Console.ReadLine();

                        if (confirma.ToLower() == "s")
                        {
                            // Reconfigurando os parâmetros para a nova query
                            cmd.Parameters.Clear();
                            // Define a consulta SQL para deletar um animal com o ID fornecido
                            cmd.CommandText = "DELETE FROM animais WHERE id = @id";
                            // Define o parâmetro com o valor do ID
                            cmd.Parameters.AddWithValue("@id", id);

                            // Executa a consulta SQL
                            int rowsUpdated = cmd.ExecuteNonQuery();

                            if (rowsUpdated > 0)
                            {
                                Console.WriteLine("Deleção realizada com sucesso!");
                                
                            }
                            else
                            {
                                Console.WriteLine("Nenhum registro foi deletado.");
                            }
                        }
                        else if (confirma.ToLower() == "n")
                        {
                            Console.WriteLine("Deleção Cancelada!!!");
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. A deleção foi cancelada!");
                        }
                    }
                }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para atualizar o nome e a espécie de um animal no banco de dados
        public static void AtualizarNomeAnimal()
        {
            try
            {
                // Lê o ID do animal e os novos valores fornecidos pelo usuário
                Console.WriteLine("Digite o id do animal que vai ter o seu nome atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo nome do animal: ");
                string novoNome = Console.ReadLine();

                Console.WriteLine("Digite a nova espécie do animal: ");
                string novaEspecie = Console.ReadLine();

                using (var cmd = DbConnection().CreateCommand())
                {

                    // Reexecutar a consulta para exibir os detalhes atualizados
                    cmd.CommandText = "SELECT * FROM animais WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para os detalhes do animal
                        Console.WriteLine("### DETALHES DO ANIMAL ANTES DE ATUALIZAR ###");
                        Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                        Console.WriteLine(new string('-', 35));

                        while (reader.Read())
                        {
                            // Imprime as informações do animal
                            Console.WriteLine("{0, -5} | {1, -15} | {2}", reader["id"], reader["nome"], reader["especie"]);
                        }
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                        Console.WriteLine("");

                        Console.WriteLine("Deseja realmente alterar os dados do animal? ");
                        Console.WriteLine("Digite (s) para alterar ou (n) para cancelar a alteração: ");
                        string confirma = Console.ReadLine();

                        if (confirma.ToLower() == "s")
                        {
                            // Reconfigurando os parâmetros para a nova query
                            cmd.Parameters.Clear();
                            // Define a consulta SQL para atualizar o nome e a espécie do animal com o ID fornecido
                            cmd.CommandText = "UPDATE animais SET nome = @nome, especie = @especie WHERE id = @id";
                            // Define os parâmetros com os valores fornecidos pelo usuário
                            cmd.Parameters.AddWithValue("@nome", novoNome);
                            cmd.Parameters.AddWithValue("@especie", novaEspecie);
                            cmd.Parameters.AddWithValue("@id", id);

                            // Executa a consulta SQL
                            int rowsUpdated = cmd.ExecuteNonQuery();

                            if (rowsUpdated > 0)
                            {
                                Console.WriteLine("Atualização realizada com sucesso!");

                                // Reexecutar a consulta para exibir os detalhes atualizados
                                cmd.CommandText = "SELECT * FROM animais WHERE id = @id";
                                cmd.Parameters.AddWithValue("@id", id);
                                using (var updateReader = cmd.ExecuteReader())
                                {
                                    // Imprime um cabeçalho para os detalhes do animal
                                    Console.WriteLine("### DETALHES DO ANIMAL ANTES DE ATUALIZAR ###");
                                    Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                                    Console.WriteLine(new string('-', 35));

                                    while (updateReader.Read())
                                    {
                                        // Imprime as informações do animal
                                        Console.WriteLine("{0, -5} | {1, -15} | {2}", updateReader["id"], updateReader["nome"], updateReader["especie"]);
                                    }

                                    Console.WriteLine("");
                                    // Fecha o reader antes de modificar o CommandText
                                    updateReader.Close();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhum registro foi atualizado.");
                            }
                        }
                        else if (confirma.ToLower() == "n")
                        {
                            Console.WriteLine("Atualização Cancelada!!!");
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. A atualização foi cancelada!");
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para obter informações de um animal com um parâmetro específico (ID)
        public static void GetAnimaisComParametro()
        {
            try
            {
                // Lê o ID do animal que o usuário deseja consultar
                Console.WriteLine("Digite o id do animal que você quer saber o nome: ");
                int id = int.Parse(Console.ReadLine());

                using (var cmd = DbConnection().CreateCommand())
                {
                    // Define a consulta SQL para selecionar um animal com o ID fornecido
                    cmd.CommandText = "SELECT * FROM animais WHERE id = @id";
                    // Define o parâmetro com o valor do ID
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Imprime um cabeçalho para os detalhes do animal
                        Console.WriteLine("### DETALHES DO ANIMAL ###");
                        Console.WriteLine("{0, -5} | {1, -15} | {2}", "ID", "Nome", "Espécie");
                        Console.WriteLine(new string('-', 35));

                        while (reader.Read())
                        {
                            // Imprime as informações do animal
                            Console.WriteLine("{0, -5} | {1, -15} | {2}", reader["id"], reader["nome"], reader["especie"]);
                        }
                        Console.WriteLine("");
                        // Informa o usuario:
                        Console.WriteLine("Consulta realizada com sucesso!");
                        // Fecha o reader antes de modificar o CommandText
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
