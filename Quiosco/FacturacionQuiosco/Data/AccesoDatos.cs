using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FacturacionQuiosco.Data
{
    internal class AccesoDatos
    {
        SqlConnection cnn;
        SqlCommand cmd;
        string cadenaConexion;

        public AccesoDatos()
        {
            cadenaConexion = @"Data Source=DESKTOP-7LTGOLV;Initial Catalog=VENTAS_LOCAL;Integrated Security=True";
            cnn = new SqlConnection(cadenaConexion);
        }

        public void Conectar()
        {
            cnn.Open();
            cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public void Desconectar()
        {
            cnn.Close();
        }

        public DataTable ConsultarDB(string sp)
        {
            DataTable dt = new DataTable();
            Conectar();
            cmd.CommandText = sp;
            dt.Load(cmd.ExecuteReader());
            Desconectar();
            return dt;
        }

        public string ProximoId(string sp)
        {
            Conectar();
            cmd.CommandText = sp;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "@proximo";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parametro);
            cmd.ExecuteNonQuery();

            string proximoId = parametro.Value.ToString();
            return proximoId;
        }

       
    }
}
