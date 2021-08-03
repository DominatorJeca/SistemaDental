using System.Collections.Generic;

namespace SistemaDental
{
    public class ClaseTratamiento
    {
        public int IdTratamiento { get; set; }
        public int IdMaterial { get; set; }
        public string NombreTratamiento { get; set; }
        public string NombreMaterial { get; set; }
        public int Cantidad { get; set; }
        public string fecha { get; set; }

        public float precioTrat { get; set; }
        public List<ClaseInventario> Materiales { get; set; }

        public string TratamientoNombre { get; set; }
        public float precioSugerido { get; set; }
        public int Estado { get; set; }
        public int masUno { get; set; }
        public ClaseTratamiento() { }

        //public List<ClaseTratamiento> mostrarTratamientos()
        //{
        //    sqlConnection.Open();

        //    try
        //    {
        //        SqlCommand command = new SqlCommand("MostrarTratamiento", sqlConnection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        SqlDataReader reader = command.ExecuteReader();

        //        List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
        //        //ClaseTratamiento tratamiento = null;

        //        while (reader.Read())
        //        {
        //            /*tratamiento = new ClaseTratamiento();
        //            tratamiento.NombreTratamiento = reader["nombre"].ToString();
        //            tratamiento.IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString());*/
        //            tratamientos.Add(new ClaseTratamiento {NombreTratamiento=reader["nombre"].ToString(), IdTratamiento=Convert.ToInt32(reader["id_tratamiento"].ToString()) });
        //        }
        //        return tratamientos;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public List<ClaseTratamiento> mostrarIdPacientes()
        //{
        //    sqlConnection.Open();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand("MostrarPacientes", sqlConnection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        SqlDataReader reader = command.ExecuteReader();

        //        List<ClaseTratamiento> pacientes = new List<ClaseTratamiento>();
        //        ClaseTratamiento paciente = null;

        //        while (reader.Read())
        //        {
        //            paciente = new ClaseTratamiento();
        //            paciente.IdPaciente = reader["id_paciente"].ToString();
        //            pacientes.Add(paciente);
        //        }
        //        return pacientes;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public List<ClaseTratamiento> mostrarMateriales(int idtratamiento)
        //{
        //    sqlConnection.Open();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand("materialnecesario", sqlConnection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@id", idtratamiento);
        //        SqlDataReader reader = command.ExecuteReader();

        //        List<ClaseTratamiento> materiales = new List<ClaseTratamiento>();
        //        ClaseTratamiento material = null;

        //        while (reader.Read())
        //        {
        //            material = new ClaseTratamiento();
        //            material.NombreMaterial = reader["nombre"].ToString();
        //            material.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());
        //            materiales.Add(material);
        //        }
        //        return materiales;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public void IngresarAlHistorial(ClaseTratamiento tratamiento)
        //{
        //    sqlConnection.Open();

        //    try
        //    {
        //        SqlCommand command = new SqlCommand("IngresoHistorial", sqlConnection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@idpaciente", tratamiento.IdPaciente);
        //        command.Parameters.AddWithValue("@fecha", Convert.ToDateTime(tratamiento.fecha));
        //        command.Parameters.AddWithValue("@idtratamiento", tratamiento.IdTratamiento);
        //        command.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public void ActualizarMaterialDisponible(ClaseTratamiento tratamiento)
        //{
        //    sqlConnection.Open();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand("actualizarCantidad", sqlConnection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@cantidad", tratamiento.Cantidad);
        //        command.Parameters.AddWithValue("@nombre", tratamiento.NombreMaterial);
        //        command.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}
    }
}
