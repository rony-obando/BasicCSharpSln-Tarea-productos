using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Domain.Enums;

namespace Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Producto
    {
        public Producto()
        {

        }

        public Producto(int codigo, string nombre, string descripcion, int cantidad, decimal precio, DateTime caducidad, UnidadMedida medida)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Cantidad = cantidad;
            this.Precio = precio;
            this.Caducidad = caducidad;
            this.unidadMedida = medida;
        }


        [JsonProperty]
        public int Codigo { get; set; }
        [JsonProperty]
        public string Nombre { get; set; }
        [JsonProperty]
        public string Descripcion { get; set; }
        [JsonProperty]
        public int Cantidad { get; set; }
        [JsonProperty]
        public decimal Precio { get; set; }
        [JsonProperty]
        public DateTime Caducidad { get; set; }
        [JsonProperty]
        public UnidadMedida unidadMedida { get; set; }
        public class CompararPrecio : IComparer<Producto>
        {
            public int Compare(Producto x, Producto y)
            {
                if (x.Precio > y.Precio)
                {
                    return 1;
                }
                else if (x.Precio < y.Precio)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
