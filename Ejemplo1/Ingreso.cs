using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ejemplo1
{
    public class Ingreso
    {
        public void INSERTAR(String sp , SqlConnection con,String v1,String v2) 
        
        {
            SqlCommand comando = new SqlCommand(sp,con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", v1);
            comando.Parameters.AddWithValue("@dpi", v2);
            
            con.Open();
            comando.ExecuteNonQuery();
            con.Close();
        
        }
    }
}
