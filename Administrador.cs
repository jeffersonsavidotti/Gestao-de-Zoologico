using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace zoologico
{
    // Definição da classe Administrador
    public class Administrador
    {
        // Propriedades de encapsulamento
        public int Id { get; set; }  // Identificador do administrador
        public string Nome { get; set; }  // Nome do administrador

        // Construtor explícito
        public Administrador(int id, string nome)
        {
            // Inicializa as propriedades com os valores passados como argumentos
            Id = id;
            Nome = nome;
        }

    }
}
