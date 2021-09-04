using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Enums;
using Domain;
using Infraestructure;

namespace BasicCSharp
{
    public partial class FormProducto : Form
    {
        ProductoModel PModel;
        public FormProducto()
        {
            PModel = new ProductoModel();
            InitializeComponent();
        }

        private void FormProducto_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void ValidateProducto(string nombre, string descripcion, string codigo, string cantidad, string caducidad, string precio, string unid)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(precio) ||
                string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(cantidad) || string.IsNullOrWhiteSpace(cantidad) || string.IsNullOrWhiteSpace(unid))
            {
                throw new ArgumentException("Error, todos todos los datos son requeridos");
            }
            if (!DateTime.TryParse(txtCaducidad.Text, out DateTime d))
            {
                throw new ArgumentException("Error, la fecha de caducidad no tiene el formato correcto: [00-00-00]");
            }
            if (nombre.Length > 25)
            {
                throw new ArgumentException("Error, maximo de caracteres permitidos es 20");
            }
            if (!decimal.TryParse(txtPrecio.Text, out decimal p))
            {
                throw new ArgumentException($@"Error, esto: {txtPrecio.Text} no es un precio valido");
            }
            if (!int.TryParse(txtCantidad.Text, out int c))
            {
                throw new ArgumentException($@"Error, esto: {txtCantidad.Text} no es una cantidad valida(solo numeros enteros)");
            }
            if (!int.TryParse(txtCodigo.Text, out int cod))
            {
                throw new ArgumentException($@"Error, esto: {txtCodigo.Text} no es un codigo valido(solo numeros esteros)");
            }
            

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombre, descripcion;
            DateTime caducidad;
            int codigo, cantidad;
            decimal precio;
            try
            {
                ValidateProducto(txtNombre.Text, txtDescripcion.Text, txtCodigo.Text, txtCantidad.Text, txtCaducidad.Text, txtPrecio.Text, cmbMedida.Text);
                nombre = txtNombre.Text;
                descripcion = txtDescripcion.Text;
                caducidad = DateTime.Parse(txtCaducidad.Text);
                codigo = int.Parse(txtCodigo.Text);
                cantidad = int.Parse(txtCantidad.Text);
                precio = decimal.Parse(txtPrecio.Text);
                UnidadMedida medida = (UnidadMedida)cmbMedida.SelectedIndex;
                Producto producto = new Producto()
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Caducidad = caducidad,
                    Codigo = codigo,
                    Cantidad = cantidad,
                    Precio = precio,
                    unidadMedida = medida,
                };
                PModel.Add(producto);
                eliminar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void eliminar()
        {
            txtCaducidad.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            cmbMedida.Text = string.Empty;

        }
        
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (PModel.productos == null)
                {
                    throw new ArgumentException("Error, no ha ingresado datos");
                }
                string mostrar = $@"Nombre: {PModel.productos[PModel.productos.Length-1].Nombre}
                            Codigo:{PModel.productos[PModel.productos.Length-1].Codigo}
                            Descripcion: {PModel.productos[PModel.productos.Length-1].Descripcion}
                            Cantidad: {PModel.productos[PModel.productos.Length-1].Cantidad}
                            Precio: {PModel.productos[PModel.productos.Length-1].Precio}
                            Caducidad: {PModel.productos[PModel.productos.Length-1].Caducidad}
                            Unidad de Medida: {PModel.productos[PModel.productos.Length-1].unidadMedida}";
                MessageBox.Show(mostrar, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCodigo.Text, out int cod))
                {
                    throw new ArgumentException($@"Error, esto: {txtCodigo.Text} no es un codigo valido(solo numeros esteros)");
                }else if (PModel.Fcorrecto(int.Parse(txtCodigo.Text)) == 0)
                {
                    throw new ArgumentException("No hay ningun producto que coincida con ese codigo");
                }
                else
                {
                    PModel.Delete(int.Parse(txtCodigo.Text));
                    eliminar();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string nombre, descripcion;
            DateTime caducidad;
            int codigo, cantidad;
            decimal precio;
            try
            {
                ValidateProducto(txtNombre.Text, txtDescripcion.Text, txtCodigo.Text, txtCantidad.Text, txtCaducidad.Text, txtPrecio.Text, cmbMedida.Text);
                if (PModel.Fcorrecto(int.Parse(txtCodigo.Text)) == 0)
                {
                    throw new ArgumentException("No hay ningun producto que coincida con ese codigo");
                }
                else
                {
                    nombre = txtNombre.Text;
                    descripcion = txtDescripcion.Text;
                    caducidad = DateTime.Parse(txtCaducidad.Text);
                    codigo = int.Parse(txtCodigo.Text);
                    cantidad = int.Parse(txtCantidad.Text);
                    precio = decimal.Parse(txtPrecio.Text);
                    UnidadMedida medida = (UnidadMedida)cmbMedida.SelectedIndex;
                    Producto producto = new Producto()
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Caducidad = caducidad,
                        Codigo = codigo,
                        Cantidad = cantidad,
                        Precio = precio,
                        unidadMedida = medida
                    };
                    PModel.Update(producto);
                    eliminar();
                }
                
                    
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    throw new ArgumentException("Favor de ingresar El codigo por el que desea buscar un producto");
                }
                else if (!int.TryParse(txtCodigo.Text, out int cod))
                {
                    throw new ArgumentException($@"Error, esto: {txtCodigo.Text} no es un codigo valido(solo numeros esteros)");
                }else if (PModel.Fcorrecto(int.Parse(txtCodigo.Text))==0)
                {
                    throw new ArgumentException("No hay ningun producto que coincida con ese codigo");
                }
                else
                {
                    string mostrar = PModel.FindByld(int.Parse(txtCodigo.Text));
                    MessageBox.Show(mostrar);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnMiguales_Click(object sender, EventArgs e)
        {
            try
            { 
                if (cmbOrdenar.SelectedIndex == 0)
                {
                    if (cmbMedida.Text == "")
                    {
                        throw new ArgumentException("No ha ingreado por que medida desee que se muestre");
                    }
                    else
                    {
                        richTextBox1.Text = "";
                        UnidadMedida unidad = (UnidadMedida)cmbMedida.SelectedIndex;
                        Producto[] tmp;
                        tmp = PModel.GetProductosByUnidadMedida(unidad);
                        string mostrar = "";
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i] != null)
                            {


                                mostrar = $@"Codigo: {tmp[i].Codigo}{Environment.NewLine}
Nombre: {tmp[i].Nombre}{Environment.NewLine}
Descripción: {tmp[i].Descripcion}{Environment.NewLine}
Precio: {tmp[i].Precio}{Environment.NewLine}
Cantidad: {tmp[i].Cantidad}{Environment.NewLine}
Caducidad: {tmp[i].Caducidad}{Environment.NewLine}
Unidad de medida: {tmp[i].unidadMedida}{Environment.NewLine}{Environment.NewLine}" + mostrar;
                            }

                        }
                        richTextBox1.Text = mostrar;
                    }

                }
                else if (cmbOrdenar.SelectedIndex == 1)
                {
                    if (txtCaducidad.Text == "")
                    {
                        throw new ArgumentException("No ha ingreado por que fecha desee que se muestre");
                    }
                    else if (!DateTime.TryParse(txtCaducidad.Text, out DateTime d))
                    {
                        throw new ArgumentException("Error, la fecha de caducidad no tiene el formato correcto: [00-00-00]");
                    }
                    else
                    {
                        richTextBox1.Text = "";
                        DateTime caducidad = DateTime.Parse(txtCaducidad.Text);
                        Producto[] tmp;
                        tmp = PModel.GetProductosByCaducidad(caducidad);
                        string mostrar = "";
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i] != null)
                            {


                                mostrar = $@"Codigo: {tmp[i].Codigo}{Environment.NewLine}
Nombre: {tmp[i].Nombre}{Environment.NewLine}
Descripción: {tmp[i].Descripcion}{Environment.NewLine}
Precio: {tmp[i].Precio}{Environment.NewLine}
Cantidad: {tmp[i].Cantidad}{Environment.NewLine}
Caducidad: {tmp[i].Caducidad}{Environment.NewLine}
Unidad de medida: {tmp[i].unidadMedida}{Environment.NewLine}{Environment.NewLine}" + mostrar;
                            }

                        }
                        richTextBox1.Text = mostrar;
                    }
                }
                else if (cmbOrdenar.SelectedIndex == 2)
                {
                    if (txtPrecio.Text == "" || txtPrecio2.Text == "")
                    {
                        throw new ArgumentException("No ha ingreado los limites del rango del precio de el producto que desea mostrar");
                    }
                    else if (!decimal.TryParse(txtPrecio.Text, out decimal p) || !decimal.TryParse(txtPrecio2.Text, out decimal p2))
                    {
                        throw new ArgumentException($@"Error, esto: {txtPrecio.Text} no es un precio valido");
                    }
                    else if (decimal.Parse(txtPrecio.Text) > decimal.Parse(txtPrecio2.Text))
                    {
                        throw new ArgumentException("Ha ingresado los datos erroneamente, favor de intercambiar los valores");
                    }
                    else
                    {
                        richTextBox1.Text = "";
                        decimal caducidad = decimal.Parse(txtPrecio.Text);
                        decimal caducidad2 = decimal.Parse(txtPrecio2.Text);
                        Producto[] tmp;
                        tmp = PModel.GetProductosByPrecio(caducidad, caducidad2);
                        string mostrar = "";
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i] != null)
                            {


                                mostrar = $@"Codigo: {tmp[i].Codigo}{Environment.NewLine}
Nombre: {tmp[i].Nombre}{Environment.NewLine}
Descripción: {tmp[i].Descripcion}{Environment.NewLine}
Precio: {tmp[i].Precio}{Environment.NewLine}
Cantidad: {tmp[i].Cantidad}{Environment.NewLine}
Caducidad: {tmp[i].Caducidad}{Environment.NewLine}
Unidad de medida: {tmp[i].unidadMedida}{Environment.NewLine}{Environment.NewLine}" + mostrar;
                            }

                        }
                        richTextBox1.Text = mostrar;
                    }
                }
                else if (cmbOrdenar.SelectedIndex == 3)
                {


                    richTextBox1.Text = "";
                    Producto[] tmp;
                    tmp = PModel.ordenarByPrecio();
                    string mostrar = "";
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i] != null)
                        {


                            mostrar = $@"Codigo: {tmp[i].Codigo}{Environment.NewLine}
Nombre: {tmp[i].Nombre}{Environment.NewLine}
Descripción: {tmp[i].Descripcion}{Environment.NewLine}
Precio: {tmp[i].Precio}{Environment.NewLine}
Cantidad: {tmp[i].Cantidad}{Environment.NewLine}
Caducidad: {tmp[i].Caducidad}{Environment.NewLine}
Unidad de medida: {tmp[i].unidadMedida}{Environment.NewLine}{Environment.NewLine}" + mostrar;
                        }

                    }
                    richTextBox1.Text = mostrar;

                }
                else if (cmbOrdenar.SelectedIndex == 4)
                {
                    richTextBox1.Text = "";
                    string tmp = PModel.GetProductoAsJson(PModel.productos);
                    richTextBox1.Text = tmp;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
