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
           // conexion.ConnectionString = @"Data Source=.;Initial Catalog=Bases2;Integrated Security=False;User Id=sa; password=123;";
            conexion.ConnectionString = "Data Source=192.168.1.10;Initial Catalog=Bases2;Integrated Security=False;User ID=robson2013; password=123;";
                                         
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

        /// <summary>
        /// ABC CON CONSULTAS DIRECTAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void leerDATOS(ComboBox combo, String consulta)
        {
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            combo.Items.Clear();
            try
            {
                comando.Connection = conexion;

                comando.CommandText = consulta;//Realizamos nuestra consulta
                conexion.Open(); //abrimos conexion
                //Leemos nuestra tabla
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    try
                    {
                        combo.Items.Add(lector.GetString(0));//Este primero lee la primera columna
                    }

                    catch (System.InvalidCastException)
                    { }


                }
                conexion.Close();
                comando.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("error de lectura" + ex.ToString());
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "INSERT INTO Cliente (nombre,dpi) VALUES (@nombre,@dpi)";

                SqlCommand command = new SqlCommand(query, conexion);

                //a shorter syntax to adding parameters
                command.Parameters.AddWithValue("@nombre", textBox18.Text);

                command.Parameters.AddWithValue("@dpi", textBox17.Text);



                //make sure you open and close(after executing) the connection
                conexion.Open();
                command.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Exito!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.ToString());
            }
        }


        //UPDATE
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "update cliente set nombre=@nombre, dpi = @dpi where id=@id"; 

                SqlCommand command = new SqlCommand(query, conexion);

                //a shorter syntax to adding parameters
                command.Parameters.AddWithValue("@nombre", textBox21.Text);

                command.Parameters.AddWithValue("@dpi", textBox20.Text);

                command.Parameters.AddWithValue("@id", textBox19.Text);


                //make sure you open and close(after executing) the connection
                conexion.Open();
                command.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("ACTUALIZADO!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.ToString());
            }
        }

        private void tabPage14_Enter(object sender, EventArgs e)
        {

            string strQuery = "select * from cliente";
            SqlCommand cmd = new SqlCommand(strQuery);
            SqlDataAdapter DA = new SqlDataAdapter(strQuery,conexion);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView2.DataSource = DT;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
                command.CommandType = CommandType.Text;
                command.CommandText = "select id,nombre,dpi from cliente where nombre = @nombre";

                //parametros a param
                param = new SqlParameter("@nombre", comboBox2.Text);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                //llenar adaptador
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);


                textBox19.Text = ds.Tables[0].Rows[0][0].ToString();
                textBox21.Text = ds.Tables[0].Rows[0][1].ToString();
                textBox20.Text = ds.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception) { }
        }

        private void tabPage11_Enter(object sender, EventArgs e)
        {
            this.leerDATOS(comboBox2, "Select Distinct nombre from cliente");
            /*
            //comboBox2.Items.Clear();
            conexion.Open();
            SqlCommand comando2 = new SqlCommand();
            comando2.CommandType = CommandType.Text;
            SqlDataAdapter Adapter1 = new SqlDataAdapter("select * from cliente", conexion);
            DataTable Tnombres = new DataTable();
            Adapter1.Fill(Tnombres);
            //conexion.Open();
            //comando2.ExecuteNonQuery();
            comboBox2.DataSource = Tnombres;
            comboBox2.DisplayMember = "nombre";
            conexion.Close();*/
        }

        private void tabPage12_Enter(object sender, EventArgs e)
        {
            this.leerDATOS(comboBox3, "Select Distinct nombre from cliente");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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
                command.CommandType = CommandType.Text;
                command.CommandText = "select id,nombre,dpi from cliente where nombre = @nombre";

                //parametros a param
                param = new SqlParameter("@nombre", comboBox3.Text);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                //llenar adaptador
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);


                textBox22.Text = ds.Tables[0].Rows[0][0].ToString();
                textBox24.Text = ds.Tables[0].Rows[0][1].ToString();
                textBox23.Text = ds.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception) { }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "DELETE FROM cliente WHERE id=@id;";

                SqlCommand command = new SqlCommand(query, conexion);


                command.Parameters.AddWithValue("@id", textBox22.Text);


                //make sure you open and close(after executing) the connection
                conexion.Open();
                command.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("BORRADO!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.ToString());
            }
        }
    }
}
