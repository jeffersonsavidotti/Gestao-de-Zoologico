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
                Console.WriteLine("### TABELA VETERINARIOS ###");
                List<Veterinario> veterinarios = DALZoologico.GetVeterinariosList();

                // Exibir o cabeçalho da tabela
                Console.WriteLine("{0, -5} | {1}", "ID", "Nome");
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

        // [EXIBE TABELA ADMINISTRADOR]
        public static void ConsultarAdm()
        {
            try
            {
                // Exibe um título para a consulta de administradores
                Console.WriteLine("### TABELA ADMINISTRADOR ###");

                // Obtém a lista de administradores usando o método GetAdministradoresList da classe DALZoologico
                List<Administrador> administradores = DALZoologico.GetAdministradoresList();

                // Exibe cabeçalhos para as colunas de dados
                Console.WriteLine("{0,-10} {1}", "ID", "Nome");

                // Exibe uma linha horizontal separadora
                Console.WriteLine(new string('-', 22));

                // Itera sobre a lista de administradores e exibe os detalhes de cada um
                foreach (Administrador administrador in administradores)
                {
                    Console.WriteLine("{0,-10} {1}", administrador.Id, administrador.Nome);
                }
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

                // Exibe cabeçalhos para as colunas de dados
                Console.WriteLine("{0,-10} {1}", "ID", "Nome");

                // Exibe uma linha horizontal separadora
                Console.WriteLine(new string('-', 22));

                // Itera sobre a lista de visitantes e exibe os detalhes de cada um
                foreach (Visitante visitante in visitantes)
                {
                    Console.WriteLine("{0,-10} {1}", visitante.Id, visitante.Nome);
                }
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

                // Exibe cabeçalhos para as colunas de dados
                Console.WriteLine("{0,-10} {1,-15} {2}", "ID", "Nome", "Espécie");

                // Exibe uma linha horizontal separadora
                Console.WriteLine(new string('-', 35));

                // Itera sobre a lista de animais e exibe os detalhes de cada um
                foreach (Animal animal in animais)
                {
                    Console.WriteLine("{0,-10} {1,-15} {2}", animal.Id, animal.Nome, animal.Especie);
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe a mensagem de erro
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
