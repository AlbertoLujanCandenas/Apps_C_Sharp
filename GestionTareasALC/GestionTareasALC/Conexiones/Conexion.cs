using GestionTareasALC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace GestionTareasALC.Conexiones
{
    public class Conexion
    {
        private String cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\alber\Downloads\Tareas.accdb";

        private OleDbConnection CN;
        private OleDbCommand CMD;
        private OleDbDataReader RDR;

        public ObservableCollection<TTarea> ObtenerTareas()
        {
            //Instanciamos la variable CN pasndole por paremtro a su constructor la ruta de la BBDD
            CN = new OleDbConnection(cadenaConexion);
            //Instanciamos la variable CMD pasandole por parametro a su constructor la QUERY que queramos usar y la conexion donde la realizará
            CMD = new OleDbCommand("SELECT * FROM TTareas", CN);
            //Tipo de comando
            CMD.CommandType = CommandType.Text;

            ObservableCollection<TTarea> ListaDeTareas = new ObservableCollection<TTarea>();

            try
            {
                CN.Open(); //Abrir la conexion
                RDR = CMD.ExecuteReader(); //Ejecutar la siguiente instruccion OleDb SELECT

                while (RDR.Read())
                {
                    TTarea tareaActual = new TTarea();
                    tareaActual.Tarea = (String)RDR["Tarea"];
                    tareaActual.Notas = (String)RDR["Notas"];
                    tareaActual.Realizada = (Boolean)RDR["Realizada"];

                    ListaDeTareas.Add(tareaActual);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CN.Close();
            }
            return ListaDeTareas;
        }

        public void GuardarNuevaTarea(TTarea tareaNueva)
        {
            CN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand();
            CMD.Connection = CN;
            CMD.CommandType = CommandType.Text;

            CMD.CommandText = "INSERT INTO TTareas (Tarea, Notas, Realizada) VALUES (@p2, @p3, @p4);";

            CMD.Parameters.AddWithValue("@p2", tareaNueva.Tarea);
            CMD.Parameters.AddWithValue("@p3", tareaNueva.Notas);
            CMD.Parameters.AddWithValue("@p4", tareaNueva.Realizada);

            CN.Open();
            CMD.ExecuteNonQuery();
            MessageBox.Show("Registro agregado correctamente");
            CN.Close();
        }

        internal int BorrarTarea(TTarea tarea)
        {
            CN = new OleDbConnection(cadenaConexion);

            CMD.CommandType = CommandType.Text;
            CMD = new OleDbCommand("DELETE FROM TTareas WHERE Id = " + tarea.Id, CN);

            int resultado = 0;

            try
            {
                CN.Open();
                resultado = CMD.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CN.Close();
            }
            return resultado;
        }

        public int ActualizarTareaActual(String Tarea, String Notas, Boolean Realizada)
        {
            CN = new OleDbConnection(cadenaConexion);
            CMD.CommandType = CommandType.Text;
            CMD = new OleDbCommand("UPDATE TTareas SET tarea = '" + Tarea + "', notas = '" + Notas + "', realizada = '" + Realizada + "' WHERE tarea ='" + Tarea + "'", CN);
            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();
            }catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CN.Close();
            }
        }

    }

}
