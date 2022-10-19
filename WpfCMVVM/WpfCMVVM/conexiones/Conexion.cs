using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Text;
using WpfCMVVM.model;
using System.Data;
using System.Windows;

namespace WpfCMVVM.conexiones
{
    public class Conexion
    {
        private String cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\alber\Downloads\Librerias.accdb";

        private OleDbConnection CN;
        private OleDbCommand CMD;
        private OleDbDataReader RDR;
        //Metodo que devuelve los registros
        public ObservableCollection<CLibro> ObtenerLibros()
        {
            //Instanciamos la variable CN pasndole por paremtro a su constructor la ruta de la BBDD
            CN = new OleDbConnection(cadenaConexion);
            //Instanciamos la variable CMD pasandole por parametro a su constructor la QUERY que queramos usar y la conexion donde la realizará
            CMD = new OleDbCommand("SELECT * FROM Libros", CN);
            //Tipo de comando
            CMD.CommandType = CommandType.Text;

            //Creamos una coleccion de tipo Libro que "envuelve" a los registros de la tabla que se van a recuperar
            ObservableCollection<CLibro> ListaDeLibros = new ObservableCollection<CLibro>();

            //Intentamos ...
            try
            {
                CN.Open(); //Abrir la conexion
                RDR = CMD.ExecuteReader(); //Ejecutar la siguiente instruccion OleDb SELECT

                while (RDR.Read())
                {

                    //Crear un objeto que "enuvelve" el registro actual
                    CLibro libroActual = new CLibro();
                    libroActual.Titulo = (String)RDR["Titulo"];
                    libroActual.Isbn = (String)RDR["Isbn"];
                    libroActual.Autor = (String)RDR["Autor"];
                    libroActual.Editorial = (String)RDR["Editorial"];

                    ListaDeLibros.Add(libroActual);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CN.Close(); //Cerramos la conexion
            }
            return ListaDeLibros; //Regresamos la lista
        }

        //Metodo que inserta un libro en la tabla
        public void GuardarNuevoLibro(CLibro nuevoLibro)
        {
            CN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand();
            CMD.Connection = CN;
            CMD.CommandType = CommandType.Text;

            CMD.CommandText = "INSERT INTO Libros (Titulo, Isbn, Autor, Editorial) VALUES (@p1, @p2, @p3, @p4);";

            //Establecemos los valores que tomaran los parametros de la instruccion OleDb
            CMD.Parameters.AddWithValue("@p1", nuevoLibro.Titulo);
            CMD.Parameters.AddWithValue("@p2", nuevoLibro.Isbn);
            CMD.Parameters.AddWithValue("@p3", nuevoLibro.Autor);
            CMD.Parameters.AddWithValue("@p4", nuevoLibro.Editorial);
            
            //Insertamos registros sin utilizar parametros
            //CMD.CommandText ="INSERT INTO Libros VALUE ("" + nuevoLibro.Titulo + "", "" + nuevoLibro.Isbn + "", "" + nuevoLibro.Autor + "", "" + nuevoLibro.Editorial + "")";

            CN.Open();
            CMD.ExecuteNonQuery();
            MessageBox.Show("Registro agregado correctamente");
            CN.Close();
        }

        //Metodo que borra un registro de la tabla
        public int BorrarLibro(String isbn)
        {
            CN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand("DELETE FROM LIBROS WHERE ISBN = @p0", CN);
            CMD.CommandType = CommandType.Text;
            CMD.Parameters.AddWithValue("@p0", isbn);

            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }
        }

        //Metodo que actualzia un registro de la tabla
        public int ActualizarLibroActual(String Titulo, String Isbn, String Autor, String Editorial)
        {
            CN = new OleDbConnection(cadenaConexion);
            CMD.CommandType = CommandType.Text;
            CMD = new OleDbCommand("UPDATE Libros SET isbn = '" + Isbn + "', Titulo = '" + Titulo + "', Autor = '" + Autor + "', Editorial = '" + Editorial + "' WHERE ISBN ='" + Isbn + "'", CN);

            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }

        }
    }
}