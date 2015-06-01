using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ejemplo1
{
    public partial class Form1 : Form
    {

        SqlConnection conexion = new SqlConnection();
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            conexion.ConnectionString = @"Data Source=.;Initial Catalog=Bases2;Integrated Security=False;User Id=sa; password=123;";
            
        }



        private void tabPage13_Enter(object sender, EventArgs e)
        {


            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter DA = new SqlDataAdapter("VerClientes", conexion);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
            conexion.Close();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Ingreso ing = new Ingreso();
                ing.INSERTAR("InsertarCliente", conexion, textNombre.Text, textDpi.Text);
                MessageBox.Show("Insercion Exitosa!!!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.ToString());
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand comando2 = new SqlCommand();
            comando2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Adapter1 = new SqlDataAdapter("GetNombresClientes", conexion);
            DataTable Tnombres = new DataTable();
            Adapter1.Fill(Tnombres);
            //conexion.Open();
            //comando2.ExecuteNonQuery();
            comboBox1.DataSource = Tnombres;
            comboBox1.DisplayMember = "nombre";
            conexion.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Objetos Necesarios
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();
                SqlParameter param;
                DataSet ds = new DataSet();

                //valores a command
                command.Connection = conexion;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetInfoCliente";
                
                //parametros a param
                param = new SqlParameter("@nombre", comboBox1.Text);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                //llenar adaptador
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);


                textBox5.Text = ds.Tables[0].Rows[0][0].ToString();
                textBox4.Text = ds.Tables[0].Rows[0][1].ToString();
                textBox3.Text = ds.Tables[0].Rows[0][2].ToString();

            }
            catch (Exception){}
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {
            String query = "INSERT INTO Cliente (nombre,dpi) VALUES (@nombre,@dpi)";

            SqlCommand command = new SqlCommand(query, conexion);
            
                //a shorter syntax to adding parameters
                command.Parameters.AddWithValue("@nombre",textBox18.Text);

                command.Parameters.AddWithValue("@dpi", textBox17.Text);



                //make sure you open and close(after executing) the connection
                conexion.Open();
                command.ExecuteNonQuery();
                conexion.Close();
            
        }



    }
}
