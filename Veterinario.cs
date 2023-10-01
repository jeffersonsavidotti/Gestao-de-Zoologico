using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace zoologico
{
    // Definição da classe Veterinario
    public class Veterinario
    {
        // Propriedades de encapsulamento
        public int Id { get; set; }  // Identificador do veterinário
        public string Nome { get; set; }  // Nome do veterinário

        // Construtor explícito
        public Veterinario(int id, string nome)
        {
            // Inicializa as propriedades com os valores passados como argumentos
            Id = id;
            Nome = nome;
        }

    }
}
