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
using System.Security.Policy;

namespace SistemaDental
{

    public class ClaseProcedimiento : BDConnexion
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        Usuario objusuario = new Usuario();

        public int anio { get; set; }
        public int mes { get; set; }

        public string Nombre { get; set; }

        public string tratamiento { get; set; }

        public string NombreEmpleado { get; set; }

        public string Paciente { get; set; }

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
        public void IngresarUsuario(Usuario usuario)
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_UsuarioEmpleado_Insertar";
                command.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                command.Parameters.AddWithValue("@Identidad", usuario.Id);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@Correo", usuario.Correo);
                command.Parameters.AddWithValue("@PuestoID", usuario.Puesto);
                command.Parameters.AddWithValue("@GeneroID", usuario.Genero);
                command.Parameters.AddWithValue("@contrasena", usuario.Contraseña);
                command.Parameters.AddWithValue("@Estado", usuario.Estado);
                command.Parameters.AddWithValue("@Administrador", usuario.Administrador);
                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {    //Cerrar conexion
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void EditarEmpleado(Usuario usuario)
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Empleados_Actualizar";
                command.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                command.Parameters.AddWithValue("@EmpleadoID", usuario.Ide);
                command.Parameters.AddWithValue("@Identidad", usuario.Id);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@Correo", usuario.Correo);
                command.Parameters.AddWithValue("@PuestoID", usuario.Puesto);
                command.Parameters.AddWithValue("@GeneroID", usuario.Genero);
                command.Parameters.AddWithValue("@Estado",usuario.Estado);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {    //Cerrar conexion
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
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
                        usuario.Ide = Convert.ToInt32(reader[0].ToString());
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



        }


        #endregion
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
                    usuario.Id=Convert.ToString(reader[0]);
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
                    user.Id =Convert.ToString( reader[0]);
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

        public List<ClaseInventario> MostrarInventarioxTratamiento(int idTratamiento)
        {
            try
            {
                List<ClaseInventario> prod = new List<ClaseInventario>();
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "MostrarMaterialxTratamiento";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idtratamiento", idTratamiento);
                //Definir las variables del procedimiento mediante los parametros obtenidos
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClaseInventario prods = new ClaseInventario();
                    prods.IdMaterial = Convert.ToInt32(reader["InventarioID"]);
                    prods.NombreMaterial = Convert.ToString(reader["Nombre"]);
                    prods.Cantidad = Convert.ToInt32(reader["CantidadUsada"]);
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
        public List<ClaseInventario> mostrarUsoTratamiento(int idmaterial)
        {
            try
            {

                List<ClaseInventario> prod = new List<ClaseInventario>();
                command.Connection = con.Open();
                command.CommandText = "MostrarUso";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idmaterial", idmaterial);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClaseInventario prods = new ClaseInventario();
                    prods.NombreTratamiento = reader["Nombre"].ToString();
                    prods.CantidadUsada = Convert.ToInt32(reader["CantidadUsada"]);
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

        public void RestarMaterial(ClaseInventario inventario)
        {
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "InventarioActualizar";
                command.Parameters.AddWithValue("@cantidad", inventario.Cantidad*-1);
                command.Parameters.AddWithValue("@InventarioId", inventario.IdMaterial);
                command.CommandType = CommandType.StoredProcedure;
               command.ExecuteNonQuery();


            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void EditarNombreMaterial(ClaseInventario inventario)
        {
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "InventarioActualizar";
                command.Parameters.AddWithValue("@nombre", inventario.NombreMaterial);
                command.Parameters.AddWithValue("@InventarioId", inventario.IdMaterial);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                MessageBox.Show("Revise si el material ya existe" + E);
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
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
                #region html
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
                #endregion
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

        public string obtenerusuario(string idEmpleado)
        {

            try
            {//Abrir la conexion sql

                string usu = "";
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "MostrarUsuario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empleado",idEmpleado);
                //Definir las variables del procedimiento mediante los parametros obtenidos
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usu = Convert.ToString(reader["Usuario"]);
                }

                return usu;
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();

            }



        }
        #region Empleados
        public List<Usuario> MostrarEmpleados(bool act)


        {
            try
            {//Abrir la conexion sql

                List<Usuario> prod = new List<Usuario>();
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Empleados_Mostrar_Activos";
                command.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario prods = new Usuario();
                    prods.Ide= Convert.ToInt32(reader["EmpleadoID"]);
                    prods.Id= Convert.ToString(reader["Identidad"]);
                    prods.Nombre = Convert.ToString(reader["Nombre"]);
                    prods.Apellido = Convert.ToString(reader["Apellido"]);
                    prods.Telefono = Convert.ToString(reader["Telefono"]);
                    prods.Correo = Convert.ToString(reader["Correo"]);
                    prods.PuestoNombre = Convert.ToString(reader["NombrePuesto"]);
                    prods.GeneroNombre = Convert.ToString(reader["NombreGenero"]);
                    prods.Contraseña = Convert.ToString(reader["contrasena"]);
                    prods.Administrador = Convert.ToBoolean(reader["administrador"]);
                    prods.Estado = Convert.ToBoolean(reader["Estado"]);
                    if (prods.Estado == act) {
                    prod.Add(prods);
                    }
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

        public List<Puesto> MostrarPuestos()
        {
            // Inicializar una lista vacía de puestos
            List<Puesto> puestos = new List<Puesto>();


            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "MostrarPuestos";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                // Obtener los datos de los puestos

                    while (reader.Read())
                       puestos.Add(new Puesto { Id = Convert.ToInt32(reader["PuestoID"]), NombrePuesto = reader["NombrePuesto"].ToString() });


                return puestos;
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

        public DataTable Citasporempleado()
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = con.Open();
                command.CommandText = "CitasPorEmpleado";
                command.Parameters.AddWithValue("@nombre", SqlDbType.NVarChar).Value = NombreEmpleado;
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




        public void AgendarCita(ClaseCitas cita)

        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("sp_Cita_Insertar", sqlConnection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpleadoID", cita.IdEmpleado);
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
                command.Parameters.AddWithValue("@EmpleadoID", cita.IdEmpleado);
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



        public void EliminarCita(int citas)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("sp_cita_eliminar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", citas);
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
                SqlDataReader   reader1 = command.ExecuteReader();
                reader1.Read();
                citas.trtamientoprecio= reader1["PrecioSugerido"].ToString();
                citas.nombreTramientoindividual = reader1["Nombre"].ToString();
                reader1.Close();
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
        #region Empleado



        public Usuario DatosUsuarios(string nombreusuario)
        {
            try
            {
                Usuario usuario = null;
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "DatosUsuario";
                command.Parameters.AddWithValue("@usuario",nombreusuario);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Contraseña = Convert.ToString(reader[0]);
                    usuario.Nombre = Convert.ToString(reader[1]);
                    usuario.Apellido = Convert.ToString(reader[2]);
                    usuario.Telefono = Convert.ToString(reader[3]);
                    usuario.Correo = Convert.ToString(reader[4]);
                    usuario.PuestoNombre = Convert.ToString(reader[5]);
                    usuario.GeneroNombre = Convert.ToString(reader[6]);
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

        public List<ClaseProcedimiento> CitasUsuario(string usuario)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "UsuarioCita";
                command.Parameters.AddWithValue("@usuario",usuario);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseProcedimiento> TestList = new List<ClaseProcedimiento>();
                ClaseProcedimiento proc = null;

                while (reader.Read())
                {
                    proc = new ClaseProcedimiento();
                    proc.Paciente = reader["Paciente"].ToString();
                    proc.tratamiento = reader["Tratamientos"].ToString();
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
                command.Parameters.Clear();
            }
        }

        public void EditarUsuario(Usuario usuario)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText ="EditarUsuario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@usuario", usuario.usuario);
                command.Parameters.AddWithValue("@contrasenia", usuario.Contraseña);
                command.Parameters.AddWithValue("@contranueva", usuario.Contrasenianueva);
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@correo", usuario.Correo);
                command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
        #endregion



        #region Transacciones


        public List<ClaseCaja> traerTransaccionesCitas(int pacienteID)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "BuscarTransacciones";
                command.Parameters.AddWithValue ("@pacienteId", pacienteID);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                List<ClaseCaja> ListaDetransacciones = new List<ClaseCaja>();
                while (reader.Read())
                {

                    var cita = new ClaseCitas();
                    var transaccion = new ClaseCaja();
                    transaccion.Abonado = (reader["Cantidad Abonada"].ToString() == "")? float.Parse("0.00000"): float.Parse(reader["Cantidad Abonada"].ToString());
                    cita.IdCita = Convert.ToInt32(reader["CitaId"]);
                    transaccion.tratamientos = Convert.ToString(reader["Tratamientos"]);
                    cita.NombreDoctor = Convert.ToString(reader["Doctor"]);
                    transaccion.UltimoAbono =(reader["Ultima Fecha Abonada"].ToString()=="")?  "No se ha hecho ningun pago": reader["Ultima Fecha Abonada"].ToString() ;
                    transaccion.Cobrado = (reader["Cantidad cobrada"].ToString() == "") ? float.Parse(0.0+""):float.Parse(reader["Cantidad cobrada"].ToString());
                    cita.fechaCita = DateTime.Parse(reader["FechaCita"].ToString());
                    cita.Observaciones = reader["Observaciones"].ToString();

                    transaccion.cita = cita;
                    ListaDetransacciones.Add(transaccion);
                }

                return ListaDetransacciones;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally
            {
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();

            }
        }

        public void InsertarTransaccion (int usuarioID,float cantidad,string observaciones,int CitaID)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "InsertarTransaccion";
                command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                command.Parameters.AddWithValue("@CitaID", CitaID);
                command.Parameters.AddWithValue("@Fecha ", DateTime.Now);
                command.Parameters.AddWithValue("@Monto", cantidad);
                command.Parameters.AddWithValue("@observaciones", observaciones);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();
            }
        }
        #endregion
        #region Pacientes

        public List<ClasePaciente> MostrarPacientesAct ()
        {
            try
            {


                command.Connection = con.Open();
                command.CommandText = "sp_Pacientes_Mostrar";

                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                List<ClasePaciente> ListaPacientes = new List<ClasePaciente>();
                while (reader.Read())
                {
                    var paciente = new ClasePaciente();
                    paciente.Id_paciente = Convert.ToInt32(reader[0]);
                    paciente.identificacionPaciente = reader[4].ToString();
                    paciente.NombrePaciente = reader[1].ToString()+ " "+reader[2].ToString();
                        ListaPacientes.Add(paciente);
                }
                return ListaPacientes;
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
        #endregion

        public List<ClasePaciente> MostrarPacientes()
        {

            List<ClasePaciente> paciente = new List<ClasePaciente>();


            try
            {
                sqlConnection.Open();

                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("sp_Pacientes_Mostrar", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Obtener los datos de los puestos
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())

                        paciente.Add(new ClasePaciente { Id_paciente = Convert.ToInt32(rdr["PacienteID"].ToString()), NombrePaciente = rdr["Nombre"].ToString(), ApellidoPaciente = rdr["Apellido"].ToString(), FechaNac = ((DateTime)rdr["Fechanac"]), Telefono = rdr["Telefono"].ToString(), Genero = rdr["GeneroID"].ToString(), Identidad = rdr["Identidad"].ToString(), Estado = rdr["Estado"].ToString(), Correo = rdr["Correo"].ToString() });

                }

                return paciente;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
              sqlConnection.Close();
            }
          
        }

        
        #endregion



        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>

        #region Citas


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
                    citas.Add(new ClaseCitas { IdEmpleado = reader["EmpleadoID"].ToString(), NombreDoctor = reader["nombre"].ToString() });
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
                sqlConnection.Close();
            }
        }

        public List<ClaseTratamiento> MostrarTratamientoCitas(int citaID)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarTratamientos";
                command.Parameters.AddWithValue("@citaId", citaID);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
                while (reader.Read())
                {
                    var trat = new ClaseTratamiento();
                    trat.IdMaterial = Convert.ToInt32(reader["DetalleCitaID"].ToString());
                    trat.IdTratamiento = Convert.ToInt32(reader["TratamientoID"].ToString());
                    trat.NombreTratamiento = reader["Nombre"].ToString();
                    trat.precioTrat = float.Parse(reader["PrecioCobrado"].ToString());
                    tratamientos.Add(trat);
                }

                return tratamientos;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }





        public void FinalizarCita(ClaseCitas cita)
        {

            try
            {
                command.Connection = con.Open();
               command.CommandText= "sp_Cita_Actualizar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CitaID", cita.IdCita);
                command.Parameters.AddWithValue("@Estado", 1);
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
                    citas.Add(new ClaseCitas { IdCita = Convert.ToInt32(reader["CitaID"].ToString()),
                        IdEmpleado = reader["EmpleadoID"].ToString(),
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
                sqlConnection.Close();
            }
          }
        public void ActualizarDatosPaciente(ClasePaciente paciente)
        {

            try
            {
                //Abrir la conexion sql
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("sp_Pacientes_Actualizar", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                sqlCommand.Parameters.AddWithValue("@Nombre", paciente.NombrePaciente);
                sqlCommand.Parameters.AddWithValue("@Apellido", paciente.ApellidoPaciente);
                sqlCommand.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                sqlCommand.Parameters.AddWithValue("@Identidad", paciente.Id_paciente);
                sqlCommand.Parameters.AddWithValue("@FechaNac", paciente.FechaNac);
                sqlCommand.Parameters.AddWithValue("@generoID", paciente.Genero);
                sqlCommand.Parameters.AddWithValue("@Estado", paciente.Estado);
                sqlCommand.Parameters.AddWithValue("@Correo", paciente.Correo);
                sqlCommand.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                sqlConnection.Close();
            }
          }

        public List<ClaseCitas> MostrarCitasHoy()
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText= "sp_Cita_Mostrar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@tipo", "Realizar");
                SqlDataReader reader = command.ExecuteReader();
                List<ClaseCitas> citas = new List<ClaseCitas>();
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas
                    {
                        IdCita = Convert.ToInt32(reader["CitaID"].ToString()),
                        IdEmpleado = reader["EmpleadoID"].ToString(),
                        NombreDoctor = reader["NombreDoctor"].ToString(),
                        ApellidoPaciente = reader["NombrePaciente"].ToString(),
                        IdPacientes = reader["PacienteID"].ToString(),
                        NombreTratamiento = reader["Tratamientos"].ToString(),
                        Preciototal = reader["Cantidad_cobrada"].ToString(),
                        fechaCita = Convert.ToDateTime(reader["FechaCita"].ToString())
                        ,Observaciones = reader["Observaciones"].ToString()
                    });
                }

                return citas;
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

        public void ActualizarDetallesCita(ClaseTratamiento tratamiento)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "[sp_DetalleCita_Acctualizar]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DetalleCita", tratamiento.IdMaterial);
                command.Parameters.AddWithValue("@PrecioCobrado", tratamiento.precioTrat);
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




        public List<ClasePaciente> ObtenerPacientesDatos()
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Pacientes_Mostrar";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClasePaciente> TestList = new List<ClasePaciente>();
                ClasePaciente paciente = null;

                while (reader.Read())
                {
                    paciente = new ClasePaciente();
                    paciente.IdHistorial = int.Parse(reader["PacienteID"].ToString());
                    paciente.NombrePaciente = reader["Nombre"].ToString();
                    TestList.Add(paciente);
                }

                return TestList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable ObtenerTratamientosDatos()
        {
            DataTable dt = new DataTable();
            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Tratamiento_Mostrar";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable ObtenerMaterialesDatos()
        {
            DataTable dt = new DataTable();
            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarInventario";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public int InsertarTratamiento(ClaseTratamiento trat)
        {
            int id;
            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Tratamiento_Insertar";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = trat.TratamientoNombre;
                command.Parameters.AddWithValue("@PrecioSugerido", SqlDbType.Money).Value = trat.precioSugerido;
                command.Parameters.AddWithValue("@Estado", SqlDbType.Bit).Value = trat.Estado;
                command.Parameters.AddWithValue("@MasUno", SqlDbType.Bit).Value = trat.masUno;

                command.Parameters.Add("@TratamientoID", SqlDbType.Int);
                command.Parameters["@TratamientoID"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                id = int.Parse(command.Parameters["@TratamientoID"].Value.ToString());

                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void InsertarTratamientoDetalle(int TratamientoID, int InventarioID, int cantidad)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "[dbo].[sp_InventarioTratamiento_Insertar]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TratamientoID", SqlDbType.Int).Value = TratamientoID;
                command.Parameters.AddWithValue("@InventarioID", SqlDbType.Int).Value = InventarioID;
                command.Parameters.AddWithValue("@CantidadUsada", SqlDbType.Int).Value = cantidad;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable BuscarTratamiento (int TratamientoID)
        {
            DataTable dt = new DataTable();
            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Tratamiento_Buscar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TratamientoID", SqlDbType.Int).Value = TratamientoID;
                command.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable BuscarInventarioTratamiento(int TratamientoID)
        {
            DataTable dt = new DataTable();
            try
            {
                command.Connection = con.Open();
                command.CommandText = "[dbo].[sp_InventarioTratamiento_Buscar]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TratamientoID", SqlDbType.Int).Value = TratamientoID;
                command.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void EliminarInventarioTratamiento(int TratamientoID)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "[dbo].[sp_InventarioTratamiento_Eliminar]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TratamientoID", SqlDbType.Int).Value = TratamientoID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void ActualizarTratamiento(ClaseTratamiento trat)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Tratamiento_Actualizar";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TratamientoID", SqlDbType.Int).Value = trat.IdTratamiento;
                command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = trat.TratamientoNombre;
                command.Parameters.AddWithValue("@PrecioSugerido", SqlDbType.Money).Value = trat.precioSugerido;
                command.Parameters.AddWithValue("@Estado", SqlDbType.Bit).Value = trat.Estado;
                command.Parameters.AddWithValue("@MasUno", SqlDbType.Bit).Value = trat.masUno;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }



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



        public List<ClasePaciente> MostrarHistorial(ClasePaciente paciente)
        {


            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("MostrarHistorial", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@identidad", paciente.Id_paciente);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<ClasePaciente> pacientes = new List<ClasePaciente>();

                while (reader.Read())
                {
                    pacientes.Add(new ClasePaciente {Paciente = reader["Paciente"].ToString(), NombreTratamiento = reader["Nombre"].ToString(),Doctor = reader["Doctor"].ToString(), FechaCita = Convert.ToDateTime(reader["FechaCita"].ToString()) });
                }

                return pacientes;
            }
            catch (Exception e)

            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void AgregarPaciente(ClasePaciente paciente)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Pacientes_Insertar", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Nombre", paciente.NombrePaciente);
                sqlCommand.Parameters.AddWithValue("@Apellido", paciente.ApellidoPaciente);
                sqlCommand.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                sqlCommand.Parameters.AddWithValue("@Identidad", paciente.Id_paciente);
                sqlCommand.Parameters.AddWithValue("@FechaNac", paciente.FechaNac);
                sqlCommand.Parameters.AddWithValue("@GeneroID", paciente.Genero);
                sqlCommand.Parameters.AddWithValue("@Estado", paciente.Estado);
                sqlCommand.Parameters.AddWithValue("@Correo", paciente.Correo);
                sqlCommand.ExecuteNonQuery();
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
