using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Empleado
    {
        public int Id { get; set; }

        public string DNI { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public decimal Salario { get; set; }
    }
}
