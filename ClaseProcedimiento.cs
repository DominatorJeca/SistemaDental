using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace SistemaDental
{

    class ClaseProcedimiento : BDConnexion
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        SqlDataReader reader1;

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
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }



        }
        public Usuario ModificarUsuario(int UsuarioId = 0, string Usuario= null, string contra= null,bool admin = false,string contraCambio = null, string correo = null)
        {
            try
            {
                var user = new Usuario();
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Usuario_Actualizar";
                command.Parameters.AddWithValue("@UsuarioID", UsuarioId);
                command.Parameters.AddWithValue("@usuario", Usuario);
                command.Parameters.AddWithValue("@contrasena", contra);
                command.Parameters.AddWithValue("@administrador", admin);
                command.Parameters.AddWithValue("@contrasenaCambio", contraCambio);
                command.Parameters.AddWithValue("@correo", correo);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.Id =Convert.ToInt32( reader[0]);
                    user.Nombre = Convert.ToString(reader[1]);
                    user.Administrador = Convert.ToBoolean(reader[2]);
                }
                return user;

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

        public string EnviarCodigoRecuperacion (string correoDestino)
        {
            try
            {

                var usuario = BuscarEmail(correoDestino);
                if (usuario == null)
                    throw new Exception("No se encontro este correo");

                Random rand = new Random();
                var randomCode = (rand.Next(999999).ToString());
                MailMessage message = new MailMessage();
                var messageBody = @"<table align='center'  cellpadding='0' cellspacing='0' width='600' style='background-image:url(https://imgur.com/LdPJmpH.jpg); background-size:500px;background-color:#D1D1D1'>
<tr> <!--Primera fila -->
        <td  align='center' style='padding: 0px 0 0px 0; font-family: Arial, sans-serif;' >
             <img src='https://imgur.com/RzOOQm0.png' align='left' alt='Ferreteria Maresa S.A' width='150' height='150' style='display: block;' />
             <br>
        </td>
    </tr>
    <tr>  <!--Segunda fila -->
        <td  style='padding: 30px 30px 30px 30px; font-family: Arial, sans-serif; font-size: 14px;'>
          <table>  <!--Tabla principal de 2da fila -->
            <tr>     <!--Primera fila -->
              <td>
                <p style='margin: 0;'>Buenas tardes su codigo de verificacion es el siguiente: " + randomCode+@" </p>
                <br>
                <p>Pasos a seguir: </p>
                <br>
                </td>
              </tr>
              <tr>  <!--Segunda fila -->
                <td>
                  <table border='1' cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse;'> <!--tabla dentro de la tabla 2 -->
                        <tr>
                          <td style='padding: 10px 10px; font-family: Arial, sans-serif; font-size: 11px;'>
                            <p style='margin: 0;'> Paso 1: Copie el codigo presentado anteriormente
                          </td>
                        </tr>
                        <tr>
                          <td style='padding: 10px 10px; font-family: Arial, sans-serif; font-size: 11px;'>
                            <p>
                              Paso 2: Dirigase al sistema de la empresa e ingrese el codigo.
                            </p>
                          </td>
                        </tr>
                        <tr>
                          <td style='padding: 10px 10px; font-family: Arial, sans-serif; font-size: 11px;'>
                            <p>
                               Paso 3: Una vez verificado cambie su contraseña y posteriormente ingrese sesion nuevamente
                            </p>
                          </td>
                        </tr>
                  </table>
                </td>
              </tr>


                </table>

        </td>
    </tr>
    <tr>  <!--Tercera fila -->
        <td  style='padding: 30px 30px;'>
            <table border='1' cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse; border:0px'> <!--Tabla pie de pagina -->
                <tr>
                  <td>
                    <p>
                      Sistema Dental
                    </p>
                  </td>
                  <td align='Right'>
                    <table border='0' >
                      <tr>
                        <td>
                            Universidad Catolica de Honduras. Campus San Pedro y San Pablo
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Telefono: +504 9818-2388
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
            </table>
        </td>
    </tr>
</table>";
                message.To.Add(correoDestino);
                message.From = new MailAddress("clinicadentalsps4@gmail.com");
                message.Body = messageBody;
                message.Subject = "Correo de recuperación de contraseña";
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("clinicadentalsps4@gmail.com", "clinica1234");
                smtp.Send(message);
                return randomCode;
            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrio un error al realizar esta accion: " + E);
                return null;
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
                reader.Close();
                command.Connection = con.Close();
            }
        }








        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>

        #region Citas 
        public List<ClaseCitas> mostrarIdPacientes()
        {
            sqlConnection.Open();
            try
            {


                SqlCommand command = new SqlCommand("sp_Pacientes_Mostrar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> pacientes = new List<ClaseCitas>();


                while (reader.Read())
                {

                    pacientes.Add(new ClaseCitas { IdPacientes = reader["PacienteID"].ToString() });
                }

                return pacientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();
                sqlConnection.Close();
               

            }

        }

        public List<ClaseCitas> MostrarEmpleado()
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("sp_Empleados_Mostrar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdDoctor = reader["EmpleadoID"].ToString(), NombreDoctor = reader["nombre"].ToString() });
                }
                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<ClaseCitas> MostrarTratamiento()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("MostrarTratamientos", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                List<ClaseCitas> citas = new List<ClaseCitas>();
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdTratamiento = Convert.ToInt32(reader["TratamientoID"].ToString()), NombreTratamiento = reader["nombre"].ToString() });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally

            {
                reader.Close();
                sqlConnection.Close();
            }
        }

        public void AgendarCita(ClaseCitas cita)
           
		    
			
			
			
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("sp_Cita_Insertar", sqlConnection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpleadoID", cita.IdDoctor);
                command.Parameters.AddWithValue("@PacienteID", cita.IdPacientes);
                command.Parameters.AddWithValue("@FechaCita", cita.fechaCita);
                command.Parameters.AddWithValue("@Estado", 0);
                command.Parameters.AddWithValue("@Descuento", 0);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

              reader.Read();
               cita.IdCita= Convert.ToInt32(reader[0]) ;
                reader.Close();

            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                sqlConnection.Close();
            }
        }

        public void EditarCita(ClaseCitas cita)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("sp_Cita_Actualizar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CitaID", cita.IdCita);
                command.Parameters.AddWithValue("@EmpleadoID", cita.IdDoctor);
                command.Parameters.AddWithValue("@PacienteID", cita.IdPacientes);
                command.Parameters.AddWithValue("@FechaCita", cita.fechaCita);
                command.Parameters.AddWithValue("@Estado", 0);
                command.Parameters.AddWithValue("@Descuento",0);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InsertarDetalleCita(int IdTratamiento, float trtamientoprecio, int cita)
        {
            try
            {
                command.Connection = con.Open();
                
                command.CommandText = "sp_DetalleCita_Insertar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CitaID", cita);
                command.Parameters.AddWithValue("@TratamientoID", IdTratamiento);
                command.Parameters.AddWithValue("@PrecioCobrado", trtamientoprecio);        
            
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

       


        public void eliminardetallecita(int idcita)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Detalle_cita_delete";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Citaid", idcita);
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

            public List<ClaseCitas> MostrarCitas()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("sp_Cita_Mostrar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
             
                List<ClaseCitas> citas = new List<ClaseCitas>();
           
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdCita = Convert.ToInt32(reader["CitaID"].ToString()), IdEmpleado = reader["EmpleadoID"].ToString(),
                        NombreDoctor = reader["NombreDoctor"].ToString(),
                        ApellidoPaciente = reader["NombrePaciente"].ToString(),
                        IdPacientes = reader["PacienteID"].ToString(),
                        NombreTratamiento = reader["Tratamientos"].ToString(),
                        Preciototal = reader["Cantidad_cobrada"].ToString(),
                        fechaCita = Convert.ToDateTime(reader["FechaCita"].ToString())}) ;
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                sqlConnection.Close();
            }
        }
        public List<ClaseCitas> Mostrartratmientos(int cita)
        {
      
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("MostrarTratamientos", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                List<ClaseCitas> citas = new List<ClaseCitas>();
                command.Parameters.AddWithValue("@citaId", cita );
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas
                    {
                        IdTratamiento = Convert.ToInt32(reader["TratamientoID"]),
                        nombreTramientoindividual = reader["Nombre"].ToString(),
                        trtamientoprecio = reader["PrecioCobrado"].ToString(),
                        detalleCita = Convert.ToInt32(reader["DetalleCitaID"].ToString())
                    });
                }
                reader.Close();
                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                sqlConnection.Close();
            }
        }



        public void EliminarCita(ClaseCitas citas)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("EliminarCitas", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", citas.IdCita);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void mostraridtrtamientos(ClaseCitas citas,int idtratamiento)
        {
            command.Connection = con.Open();

            try
            {
                command.CommandText="sp_Buscaidtratamiento";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idtratamiento", idtratamiento);
                command.ExecuteNonQuery();
                reader1 = command.ExecuteReader();
                reader1.Read();
                citas.trtamientoprecio= reader1["PrecioSugerido"].ToString();
                citas.nombreTramientoindividual = reader1["Nombre"].ToString();
               
            }
            catch
            {
                throw;
            }
            finally
            {
                reader1.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }


        }

        #endregion

        public List<ClaseCitas> mostrarPacientes()
        {
            sqlConnection.Open();
            try
            {

                SqlCommand command = new SqlCommand("sp_Pacientes_Mostrar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> pacientes = new List<ClaseCitas>();


                while (reader.Read())
                {

                    pacientes.Add(new ClaseCitas { IdPacientes = reader["PacienteID"].ToString(), NombrePaciente = reader["Nombre"].ToString(), Nombre_Id_paciente = reader["PacienteID"].ToString() +", "+ reader["Nombre"].ToString() });
                }

                return pacientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
               
            }

        }


    }
}
