using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace ServiciosCuentaUsuario
{
   
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class ServicioCuentaUsuario : IServicioCuentaUsuario
    {
        int salida;
        int salida2;
        Cuenta cuenta;
        int idCuenta;
        string telefono;
        CuentaCompleta cuentaC;
        public CuentaCompleta IniciarSesion(string correo, string contrasena)
        {
            try
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                "Select Cuenta_idCuenta from Contrasena where contrasena='{0}'", contrasena), Conexion.ObtenerConexion());
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    salida = reader.GetInt32(0);
                }

                MySqlCommand comando2 = new MySqlCommand(string.Format(
                    "Select Cuenta_idCuenta from Correo where correo='{0}'", correo), Conexion.ObtenerConexion());
                MySqlDataReader reader2 = comando2.ExecuteReader();
                while (reader2.Read())
                {
                    salida2 = reader2.GetInt32(0);
                }

                if (salida == salida2)
                {
                    MySqlCommand comando3 = new MySqlCommand(string.Format(
                    "Select idCuenta, nombreUsuario, idFotoCuentaUsuario ,Genero_idGenero from Cuenta where idCuenta='{0}'", salida), Conexion.ObtenerConexion());
                    MySqlDataReader reader3 = comando3.ExecuteReader();
                   
                    cuentaC = new CuentaCompleta(salida, cuenta.getNombreUsuario(), correo, contrasena, telefono, cuenta.getIdFotoCuentaUsuario(), cuenta.getGenero_idGenero());

                    return cuentaC;

                }
            }
            catch(Exception e)
            {
                return cuentaC;
            }
            return cuentaC;
        }

        public int ModificarUsuario(int idCuenta, string nombreUsuario, string correo, string contrasena, string telefono, int idFotoCuentaUsuario, int Genero_idGenero)
        {
            int retorno=0;
            try
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
               "Update Cuenta set nombreUsuario='{0}', idFotoCuentaUsuario='{1}',Genero_idGenero='{2}', correo='{3}', contrasena='{4}', telefono='{5}' where idCuenta='{6}'", nombreUsuario, idFotoCuentaUsuario, Genero_idGenero, correo, contrasena, telefono, idCuenta), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return retorno;
            }

            return retorno;
        }

        public int RegistrarUsuario(string nombreUsuario, string correo, string contrasena, string telefono, int idFotoCuentaUsuario, int Genero_idGenero)
        {
            int retorno = 0;
            try
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                "Insert into Cuenta (nombreUsuario,idFotoCuentaUsuario,Genero_idGenero, nombreUsuario, correo, contrasena, telefono) values ('{0}','{1}','{2}','{3}','{4}','{5}' )", nombreUsuario, idFotoCuentaUsuario, Genero_idGenero, nombreUsuario, correo, contrasena, telefono), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return retorno;
            }

            return retorno;
        }

        public int validarExistencia(string nombreUsuario)
        {
            int retorno=0;
            MySqlCommand comando2 = new MySqlCommand(string.Format(
               "Select nombreUsuario from Cuenta where nombreUsuario='{0}'", nombreUsuario), Conexion.ObtenerConexion());
            MySqlDataReader reader2 = comando2.ExecuteReader();
            while (reader2.Read())
            {
                if (nombreUsuario.Equals(reader2.GetString(0)))
                {
                    retorno = 1;
                }
                else
                {
                    retorno = 0;
                }
            }

            return retorno;
        }
    }
}
