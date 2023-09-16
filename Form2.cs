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

namespace Tp_Ejercicio_3
{
    public partial class Form2 : Form
    {
        SqlConnection co;
        SqlCommand cm; 
        public Form2()
        {
            InitializeComponent();
            co = new SqlConnection("Data Source=.;Initial Catalog=\"Tp Ejercicio 3\";Integrated Security=True");
            cm = new SqlCommand("select * from operacion",co);
        
        }

        private void LeerOperacion()
        {
            cm.CommandText = "select * from operacion";
            SqlDataReader dr = cm.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(new object[] { dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3)});
            }
            dr.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(co.State == ConnectionState.Closed)
            {
                co.Open();
                button1.Text = "Desconectar";
                LeerOperacion();
            }
            else
            {
                co.Close();
                button1.Text = "Conectar";
            }
        }
    }
}
