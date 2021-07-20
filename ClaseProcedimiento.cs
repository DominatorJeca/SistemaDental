using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace SistemaDental
{

    class ClaseProcedimiento : BDConnexion
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        public int anio { get; set; }
        public int mes { get; set; }

        public string Nombre { get; set; }

        public string tratamiento { get; set; }

        public string NombreEmpleado { get; set; }

        public string Empleado { get; set; }

        #region Turno - Procedimientos

        public void ActualizarTurno(Turno turno)
        {

            try
            {//Abrir la conexion sql
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Turno_Actualizar";
                command.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                
                command.Parameters.AddWithValue("@UsuarioID", turno.UsuarioID);
              
                command.Parameters.AddWithValue("@FinalTurno", turno.FinalTurno);

                command.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {

                command.Connection = con.Close();
                command.Parameters.Clear();
            }


        }

        public void AgregarTurno(Turno turno)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "VerificarTurno";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@usuarioID", turno.UsuarioID);
                command.Parameters.AddWithValue("@fecha", turno.ComienzoTurno);
                command.Parameters.AddWithValue("@resultado", 0);

                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

       /* public Turno SeleccionarTurno(Turno turno)
        {
            //objeto que contendrá los datos del usuario
            Usuario usuario = new Usuario();
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "SeleccionarTurno";

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@usuarioID", turno.UsuarioID);
                command.Parameters.AddWithValue("@fecha", turno.ComienzoTurno);
                reader = command.ExecuteReader();


                using (reader)
                {

                    turno.AgendaID = Convert.ToInt32(reader["AgendaID"]);
                    turno.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                    turno.ComienzoTurno = Convert.ToDateTime(reader["ComienzoTurno"]);
                    turno.FinalTurno = Convert.ToDateTime(reader["FinalTurno"]);

                }

                return turno;
            }
            catch (Exception e)
            {

                throw e;

            }
            finally
            {    //Cerrar conexion
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }

        }*/
        #region Compras

        public void EditarCompra(int CompraID, int InventarioID, int EmpleadoID, DateTime FechaCompra, DateTime FechaVenci, double precio, int cantidad)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "EditarCompra";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompraID", CompraID);
                command.Parameters.AddWithValue("@InventarioID", InventarioID);
                command.Parameters.AddWithValue("@EmpleadoID", EmpleadoID);
                command.Parameters.AddWithValue("@FechaCompra", FechaCompra);
                command.Parameters.AddWithValue("@FechaVencimiento", FechaVenci);
                command.Parameters.AddWithValue("@PrecioCompra", precio);
                command.Parameters.AddWithValue("@cantidad", cantidad);
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }

        }
        public void EliminarTurno(int AgendaID)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Turno_Eliminar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AgendaID", AgendaID);


                command.ExecuteNonQuery();
              }
              catch
              {
                  throw;
              }
              finally
              {
                  command.Parameters.Clear();
                  command.Connection = con.Close();
              }

        }

       
        #endregion

        #region Usuario-Procedimientos

        public Usuario BuscarUsuario(string user, string contra)
        {
            //objeto que contendrá los datos del usuario
            Usuario usuario = null;
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Usuario_VerificarLogin";

                command.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                command.Parameters.AddWithValue("@nombreUsuario", user);
                command.Parameters.AddWithValue("@contrasena", contra);
                reader = command.ExecuteReader();


                using (reader)
                {
                    while (reader.Read())
                    {
                        //Obtener valores del usuario
                        usuario = new Usuario();
                        usuario.Nombre = Convert.ToString(reader["Nombre"]);
                        usuario.Apellido = Convert.ToString(reader["Apellido"]);
                        usuario.Administrador = Convert.ToBoolean(reader["administrador"]);
                        usuario.Id = Convert.ToInt32(reader[0]);
                    }
                }

                return usuario;
            }
            catch (Exception e)
            {

                throw e;

            }
            finally
            {    //Cerrar conexion
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
            #endregion


        }
        public Usuario BuscarEmail(string correo)
        {
            try
            {
                Usuario usuario = null;
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "BuscarEmail";
                command.Parameters.AddWithValue("@CorreoElectronico", correo);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id=Convert.ToInt32(reader[0]);
                    usuario.Nombre = Convert.ToString(reader[1]);

                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

            }

        }
        public List<ClaseInventario> MostrarInventario ()
        {
            try
            {//Abrir la conexion sql
                
                List<ClaseInventario> prod = new List<ClaseInventario>();
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "MostrarInventario";
                command.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClaseInventario prods = new ClaseInventario();
                    prods.IdMaterial = Convert.ToInt32(reader["InventarioID"]);
                    prods.NombreMaterial = Convert.ToString(reader["Nombre"]);
                    prods.Cantidad = Convert.ToInt32(reader["CantidadDisponible"]);
                    prod.Add(prods);
                }
                return prod;
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                reader.Close();
                command.Connection = con.Close();
                command.Parameters.Clear();
            }


        }
        public int InsertarCompra(int empleadoId)
        {
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Compra_Insertar";
                command.Parameters.AddWithValue("@EmpleadoID", empleadoId);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                reader.Read();
                return Convert.ToInt32(reader[0]);
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void InsertarDetalleCompra(int compraid,int inventarioId,int cantidad,float precio,DateTime fechavenc)
        {
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_DetalleCompra_Insertar";
                command.Parameters.AddWithValue("@FKCompraID", compraid);
                command.Parameters.AddWithValue("@FKInventario", inventarioId);
                command.Parameters.AddWithValue("@Cantidad", cantidad);
                command.Parameters.AddWithValue("@PrecioCompra", precio);
                command.Parameters.AddWithValue("@fechaVenc", fechavenc);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
               
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
        #endregion

        public DataTable FechaVenc()
        {
           

            try
            {
                DataTable dt = new DataTable();

                    command.Connection = con.Open();
                    command.CommandText = "Report_FechaVencimiento";
                    command.Parameters.AddWithValue("@mes", SqlDbType.Int).Value = mes;
                    command.Parameters.AddWithValue("@año", SqlDbType.Int).Value = anio;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                
                return dt;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public DataTable TotalCompraMes()
        {


            try
            {

                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "Report_TotalCompraxMes";
                command.Parameters.AddWithValue("@anio", SqlDbType.Int).Value = anio;
                command.Parameters.AddWithValue("@mes", SqlDbType.Int).Value = mes;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public List<ClaseProcedimiento> NombreTratamientos()
        {
            try
            {


                command.Connection = con.Open();
                command.CommandText = "NombreTratamientos";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseProcedimiento> TestList = new List<ClaseProcedimiento>();
                ClaseProcedimiento proc = null;

                while (reader.Read())
                {
                    proc = new ClaseProcedimiento();
                    proc.Nombre = reader["Nombre"].ToString();
                    TestList.Add(proc);
                }

                return TestList;
            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
            }
        }

        public DataTable TotalxTratamiento()
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "Report_TotalxTratamiento";
                command.Parameters.AddWithValue("@tratamiento", SqlDbType.NVarChar).Value = tratamiento;
                command.Parameters.AddWithValue("@anio", SqlDbType.Int).Value = anio;
                command.Parameters.AddWithValue("@mes", SqlDbType.Int).Value = mes;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public DataTable AnioGanancias()
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "Reporte_Ganancias";
                command.Parameters.AddWithValue("@anio", SqlDbType.Int).Value = anio;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public DataTable VentasMes()
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "Report_VentasxMes";
                command.Parameters.AddWithValue("@mes", SqlDbType.Int).Value = mes;
                command.Parameters.AddWithValue("@anio", SqlDbType.Int).Value = anio;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public DataTable TurnosxEmpleado()
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "Report_TurnosxEmpleado";
                command.Parameters.AddWithValue("@Nombre", SqlDbType.NVarChar).Value = NombreEmpleado;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public List<ClaseProcedimiento> NombreEmpleados()
        {
            try
            {


                command.Connection = con.Open();
                command.CommandText = "NombreEmpleadoConcat";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseProcedimiento> TestList = new List<ClaseProcedimiento>();
                ClaseProcedimiento proc = null;

                while (reader.Read())
                {
                    proc = new ClaseProcedimiento();
                    proc.Empleado = reader["Nombre"].ToString();
                    TestList.Add(proc);
                }

                return TestList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection = con.Close();
            }
        }

    }
}
