using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace zoologico
{
    // Definição da classe Animal
    public class Animal
    {
        // Propriedades de encapsulamento
        public int Id { get; set; }  // Identificador do animal
        public string Nome { get; set; }  // Nome do animal
        public string Especie { get; set; }  // Espécie do animal

        // Construtor explícito
        public Animal(int id, string nome, string especie)
        {
            // Inicializa as propriedades com os valores passados como argumentos
            Id = id;
            Nome = nome;
            Especie = especie;
        }

    }
}
