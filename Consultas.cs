using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace zoologico
{

    public class Consultas
    {
        // [EXIBE TABELA VETERINÁRIOS]
        public static void ConsultarVet()
        {
            try
            {
                // Exibe um título para a consulta de administradores
                Console.WriteLine("### TABELA VETERINARIOS ###");

                // Obtém a lista de administradores usando o método GetAdministradoresList da classe DALZoologico
                List<Veterinario> veterinarios = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
                // Exibe uma linha horizontal separadora
                Console.WriteLine(new string('-', 25));
                // Itera sobre a lista de administradores e exibe os detalhes de cada um
                foreach (Veterinario veterinario in veterinarios)
                {
                    // Exibir os dados formatados na tabela
                    Console.WriteLine("{0,-10} {1}", veterinario.Id, veterinario.Nome);
                }
                Console.WriteLine("");
                Console.WriteLine("Consulta realizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        // [EXIBE TABELA ADMINISTRADOR]
        public static void ConsultarAdm()
        {
            try
            {
                // Exibe um título para a consulta de administradores
                Console.WriteLine("### TABELA ADMINISTRADOR ###");

                // Obtém a lista de administradores usando o método GetAdministradoresList da classe DALZoologico
                List<Administrador> administradores = DALZoologico.GetAdministradoresList();

            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe a mensagem de erro
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        // [EXIBE TABELA VISITANTE]
        public static void ConsultarVis()
        {
            try
            {
                // Exibe um título para a consulta de visitantes
                Console.WriteLine("### TABELA VISITANTES ###");

                // Obtém a lista de visitantes usando o método GetVisitantesList da classe DALZoologico
                List<Visitante> visitantes = DALZoologico.GetVisitantesList();
              
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe a mensagem de erro
                Console.WriteLine("Erro: " + ex.Message);
            }
        }


        public static void Consultar_Animal()
        {
            try
            {
                // Exibe um título para a consulta de animais
                Console.WriteLine("### TABELA ANIMAIS ###");

                // Obtém a lista de animais usando o método GetAnimaisList da classe DALZoologico
                List<Animal> animais = DALZoologico.GetAnimaisList();

            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe a mensagem de erro
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
