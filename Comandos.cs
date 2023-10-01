using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace zoologico
{
    // Definição da classe Comandos
    public class Comandos
    {
        // Método para inserir veterinários
        public static void InserirVet()
        {
            try
            {
                Console.WriteLine(" ");
                Console.WriteLine("### ESTADO DO BD APÓS INSERÇÃO ###");
                List<Veterinario> veterinarios2 = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0,-10} {1}", "ID", "Nome");
                Console.WriteLine(new string('-', 22));

                foreach (Veterinario veterinario in veterinarios2)
                {
                    // Exibir os dados formatados na tabela
                    Console.WriteLine("{0,-10} {1}", veterinario.Id, veterinario.Nome);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para deletar veterinários
        public static void DeletarVet()
        {
            try
            {
                Console.WriteLine(" ");
                Console.WriteLine("### ESTADO DO BD APÓS DELEÇÃO ###");

                List<Veterinario> veterinarios3 = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0,-10} {1}", "ID", "Nome");
                Console.WriteLine(new string('-', 22));

                foreach (Veterinario veterinario in veterinarios3)
                {
                    // Exibir os dados formatados na tabela
                    //Console.WriteLine("{0,-10} {1}", veterinario.Id, veterinario.Nome);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para atualizar veterinários
        public static void AtualizarVet()
        {
            try
            {
                Console.WriteLine(" ");
                Console.WriteLine("### ESTADO DO BD APÓS ATUALIZAÇÃO ###");

                List<Veterinario> veterinarios4 = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0,-10} {1}", "ID", "Nome");
                Console.WriteLine(new string('-', 22));

                foreach (Veterinario veterinario in veterinarios4)
                {
                    // Exibir os dados formatados na tabela
                    //Console.WriteLine("{0,-10} {1}", veterinario.Id, veterinario.Nome);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // Método para consultar veterinários
        public static void ConsultarVet()
        {
            try
            {
                Console.WriteLine("### DETALHES DO VETERINARIO2 ###");
                List<Veterinario> veterinarios = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0, -5} | {1}", "ID", "Nome2");
                Console.WriteLine(new string('-', 25));

                foreach (Veterinario veterinario in veterinarios)
                {
                    // Exibir os dados formatados na tabela
                    Console.WriteLine("{0,-10} {1}", veterinario.Id, veterinario.Nome);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // ... (métodos para visitantes, administradores e animais seguem o mesmo padrão)
    }
}
