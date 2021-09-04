using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Newtonsoft.Json;
using Domain.Enums;

namespace Infraestructure
{
    public class ProductoModel
    {
        [JsonProperty]
        public Producto[] productos;

        public void Add(Producto e)
        {
            if (e == null)
            {
                throw new ArgumentException("Error, no puede ser null");
            }
            if (productos == null)
            {
                productos = new Producto[1];
                productos[0] = e;
            }
            else
            {

                Producto[] tmp = new Producto[productos.Length + 1];
                Array.Copy(productos, tmp, productos.Length);
                tmp[tmp.Length - 1] = e;
                productos = tmp;
            }
        }
        public void Update(Producto e)
        {
            for (int i = 0; i < productos.Length; i++)
            {
                if (e.Codigo == productos[i].Codigo)
                {
                    productos[i] = e;
                }
            }
        }
        public void Delete(int e)
        {
            for (int i = 0; i < productos.Length; i++)
            {
                if (e == productos[i].Codigo)
                {
                    productos[i] = productos[productos.Length - 1];
                    Producto[] tmp = new Producto[productos.Length - 1];
                    Array.Copy(productos, tmp, productos.Length - 1);
                    productos = tmp;
                }
            }
        }
        public int Fcorrecto(int b)
        {
            int a = 0; 
            for (int i=0;i<productos.Length;i++)
            {
                if (b==productos[i].Codigo)
                {
                    a = 1;
                }
            }
            return a;
        }
        public string FindByld(int codigo)
        {
            string a = "";
            for (int i = 0; i < productos.Length; i++)
            {
                if (codigo == productos[i].Codigo)
                {
                    a = $@"Codigo: {productos[i].Codigo}{Environment.NewLine}
Nombre: {productos[i].Nombre}{Environment.NewLine}
Descripción: {productos[i].Descripcion}{Environment.NewLine}
Precio: {productos[i].Precio}{Environment.NewLine}
Cantidad: {productos[i].Cantidad}{Environment.NewLine}
Caducidad: {productos[i].Caducidad}{Environment.NewLine}
Unidad de medida: {productos[i].unidadMedida}{Environment.NewLine}{Environment.NewLine}" + a;
                }
            }
            return a;
        }
        public Producto[] GetProductosByUnidadMedida(UnidadMedida u)
        {
            Producto[] unidad = new Producto[1];
            Producto e;
            for (int i = 0; i < productos.Length; i++)
            {
                if (u == productos[i].unidadMedida)
                {
                    e = new Producto()
                    {
                        unidadMedida = productos[i].unidadMedida,
                        Nombre = productos[i].Nombre,
                        Caducidad = productos[i].Caducidad,
                        Codigo = productos[i].Codigo,
                        Cantidad = productos[i].Cantidad,
                        Descripcion = productos[i].Descripcion,
                        Precio = productos[i].Precio,
                    };


                    Producto[] tmp = new Producto[unidad.Length + 1];
                    Array.Copy(unidad, tmp, unidad.Length);
                    tmp[tmp.Length - 1] = e;
                    unidad = tmp;

                }
            }
            return unidad;
        }
        public Producto[] GetProductosByCaducidad(DateTime dt)
        {
            Producto[] caducidad = new Producto[1];
            Producto e;
            for (int i = 0; i < productos.Length; i++)
            {
                if (dt == productos[i].Caducidad)
                {
                    e = new Producto()
                    {
                        unidadMedida = productos[i].unidadMedida,
                        Nombre = productos[i].Nombre,
                        Caducidad = productos[i].Caducidad,
                        Codigo = productos[i].Codigo,
                        Cantidad = productos[i].Cantidad,
                        Descripcion = productos[i].Descripcion,
                        Precio = productos[i].Precio,
                    };


                    Producto[] tmp = new Producto[caducidad.Length + 1];
                    Array.Copy(caducidad, tmp, caducidad.Length);
                    tmp[tmp.Length - 1] = e;
                    caducidad = tmp;

                }
            }
            return caducidad;
        }
        public Producto[] GetProductosByPrecio(decimal d1, decimal d2)
        {
            Producto[] precio = new Producto[1];
            Producto e;
            for (int i = 0; i < productos.Length; i++)
            {
                if ((d1 <= productos[i].Precio && d2 >= productos[i].Precio))
                {

                    e = new Producto()
                    {
                        unidadMedida = productos[i].unidadMedida,
                        Nombre = productos[i].Nombre,
                        Caducidad = productos[i].Caducidad,
                        Codigo = productos[i].Codigo,
                        Cantidad = productos[i].Cantidad,
                        Descripcion = productos[i].Descripcion,
                        Precio = productos[i].Precio,
                    };


                    Producto[] tmp = new Producto[precio.Length + 1];
                    Array.Copy(precio, tmp, precio.Length);
                    tmp[tmp.Length - 1] = e;
                    precio = tmp;

                }
            }
            return precio;
        }
        public Producto[] ordenarByPrecio()
        {
            Producto e;
            Producto[] orden = new Producto[1];
            Array.Sort(productos, new Producto.CompararPrecio());
            for (int i = 0; i < productos.Length; i++)
            {
                e = new Producto()
                {
                    unidadMedida = productos[i].unidadMedida,
                    Nombre = productos[i].Nombre,
                    Caducidad = productos[i].Caducidad,
                    Codigo = productos[i].Codigo,
                    Cantidad = productos[i].Cantidad,
                    Descripcion = productos[i].Descripcion,
                    Precio = productos[i].Precio,
                };
                Producto[] tmp = new Producto[orden.Length + 1];
                Array.Copy(orden, tmp, orden.Length);
                tmp[tmp.Length - 1] = e;
                orden = tmp;

            }
            return orden;
        }
        public string GetProductoAsJson(Producto[] ps)
        {
            string tmp = "";
            Producto u;
            for (int i = 0; i < productos.Length; i++)
            {
                u = new Producto()
                {
                    unidadMedida = productos[i].unidadMedida,
                    Nombre = productos[i].Nombre,
                    Caducidad = productos[i].Caducidad,
                    Codigo = productos[i].Codigo,
                    Cantidad = productos[i].Cantidad,
                    Descripcion = productos[i].Descripcion,
                    Precio = productos[i].Precio,
                };
                ps = new Producto[productos.Length + 1];
                Array.Copy(productos, ps, productos.Length);
                ps[ps.Length - 1] = u;
                ps = productos;

            }
            for (int i = 0; i < productos.Length; i++)
            {
                tmp = JsonConvert.SerializeObject(ps[i]) + tmp;
            }
            return tmp;
        }
        
    }
}
