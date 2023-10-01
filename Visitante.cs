using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace zoologico
{
    // Definição da classe Visitante
    public class Visitante
    {
        // Propriedades de encapsulamento
        public int Id { get; set; }  // Identificador do visitante
        public string Nome { get; set; }  // Nome do visitante

        // Construtor explícito
        public Visitante(int id, string nome)
        {
            // Inicializa as propriedades com os valores passados como argumentos
            Id = id;
            Nome = nome;
        }

    }
}
