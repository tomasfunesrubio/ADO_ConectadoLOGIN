using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Tp_Ejercicio_3
{
    public partial class Form1 : Form
    {
        SqlConnection co;
        SqlCommand cm;
        SqlDataReader dr;
        int contador = 3;
        int ID_Operacion = 0;
        public Form1()
        {
            InitializeComponent();
            co = new SqlConnection("Data Source=.;Initial Catalog=\"Tp Ejercicio 3\";Integrated Security=True");
            cm = new SqlCommand("select * from usuario", co);

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LeerProductos(string sql = "select * from producto")
        {
            cm.CommandText = sql;
            dr.Close();
            dr = cm.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(new object[] { dr.GetValue(0), dr.GetValue(1), dr.GetValue(2) });
            }
            dr.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (co.State == ConnectionState.Closed)
                {
                    co.Open();
                    if (contador == 0) throw new Exception("Usted esta bloqueado");


                    string Nombre = Interaction.InputBox("Ingrese el nombre de usuario: ", "Inicio sesion");
                    string Contraseña1 = Interaction.InputBox("Ingrese la contraseña del usuaurio: ", "Inicio sesion");

                    cm.CommandText = $"select * from usuario where Nombre = '{Nombre}' and Constraseña = '{Contraseña1}'";
                    dr = cm.ExecuteReader();



                    if (dr.Read())
                    {

                        MessageBox.Show("Bienvenido");
                        LeerProductos();
                        button1.Text = "Cerrar Sesion";

                        cm.CommandText = $"insert into operacion(ID_Operacion,Fecha,Operacion,Usuario) values ({ID_Operacion++}, {4}, '{"LogIN"}', '{Nombre}')";
                        cm.ExecuteNonQuery();

                        
                    }
                    else
                    {

                        contador--;
                        MessageBox.Show("intente de nuevo");

                        co.Close();
                    }

                    dr.Close();
                }
                else
                {
                    button1.Text = "Iniciar Sesion";

                    cm.CommandText = $"insert into operacion(ID_Operacion,Fecha,Operacion,Usuario) values ({ID_Operacion++}, {4}, '{"LogOUT"}', '{""}')";
                    cm.ExecuteNonQuery();
                    co.Close() ;
                }

                


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (co.State == ConnectionState.Closed)
            {
                co.Open();
                button2.Text = "Desconectar";
            }
            else
            {
                co.Close();
                button2.Text = "Conectar";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int ID = int.Parse(Interaction.InputBox("Ingrese el id del producto: "));
            string Nombre = Interaction.InputBox("Ingrese el nombre del producto: ");
            int cantidad = int.Parse(Interaction.InputBox("Ingrese la cantidad: "));

            cm.CommandText = $"insert into producto(ID_Producto,Nombre,Cantidad) values ({ID},'{Nombre}',{cantidad})";
            cm.ExecuteNonQuery();
            LeerProductos();

            cm.CommandText = $"insert into operacion(ID_Operacion,Fecha,Operacion,Usuario) values ({ID_Operacion++}, {1}, '{"Agregar"}', '{Nombre}')";
            cm.ExecuteNonQuery();
            
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            cm.CommandText = $"delete from producto where ID_Producto = {dataGridView1.SelectedRows[0].Cells[0].Value}";
            cm.ExecuteNonQuery();
            LeerProductos() ;

            cm.CommandText = $"insert into operacion(ID_Operacion,Fecha,Operacion,Usuario) values ({ID_Operacion++}, {2}, '{"Borrar"}', '{dataGridView1.SelectedRows[0].Cells[1].Value}')";
            cm.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Nombre = Interaction.InputBox("Ingrese el nombre: ");
            int cantidad = int.Parse(Interaction.InputBox("Ingrese cantidad: "));
            cm.CommandText = $"update producto set Nombre = '{Nombre}', Cantidad = '{cantidad}' where ID_Producto = {dataGridView1.SelectedRows[0].Cells[0].Value}";
            cm.ExecuteNonQuery();
            LeerProductos() ;

            cm.CommandText = $"insert into operacion(ID_Operacion,Fecha,Operacion,Usuario) values ({ID_Operacion++}, {3}, '{"Modificar"}', '{dataGridView1.SelectedRows[0].Cells[1].Value}')";
            cm.ExecuteNonQuery();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 nf = new Form2();
            nf.Show();
        }
    }
}
