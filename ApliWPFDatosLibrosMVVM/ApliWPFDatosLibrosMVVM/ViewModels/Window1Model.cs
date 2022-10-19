using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Input;
using LibretaDireccionesMVVM.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace ApliWPFDatosLibrosMVVM.ViewModels
{
    public class Window1Model : INotifyPropertyChanged
    {
        bool bandera; //al declararla se realiza como falso
        OleDbConnection Conexion;
        OleDbDataAdapter DD;

        DataSet DataS;

        OleDbCommandBuilder Builder;

        int i;

        public ICommand ComandoPrimero { get; set; }
        public ICommand ComandoSiguiente { get; set; }
        public ICommand ComandoAnterior { get; set; }
        public ICommand ComandoUltimo { get; set; }
        public ICommand ComandoNuevo { get; set; }
        public ICommand ComandoInsertar { get; set; }
        public ICommand ComandoModificar { get; set; }
        public ICommand ComandoBorrar { get; set; }
        public ICommand ComandoActualizar { get; set; }

        private String titulo;
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged("Titulo");
            }


        }
        private string autor;
        public string Autor
        {
            get { return autor; }
            set
            {
                autor = value;

                OnPropertyChanged("Autor");
            }
        }
        private string isbn;
        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;

                OnPropertyChanged("ISBN");
            }
        }

        private string editorial;
        public string Editorial
        {
            get { return editorial; }
            set
            {
                editorial = value;

                OnPropertyChanged("Editorial");
            }
        }

        private string nreg;
        public string Nreg
        {
            get { return nreg; }
            set
            {
                nreg = value;

                OnPropertyChanged("Nreg");
            }

        }

        public Window1Model()
        {


            Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\alber\Downloads\Librerias.accdb";

            DD = new OleDbDataAdapter("Select * from Libros", Conexion);

            Builder = new OleDbCommandBuilder(DD);

            DataS = new DataSet();

            Conexion.Open();

            DD.Fill(DataS, "Libros");

            Conexion.Close();

            ComandoPrimero = new Command(AccionPrimero);
            ComandoAnterior = new Command(AccionAnterior);
            ComandoSiguiente = new Command(AccionSiguiente);
            ComandoUltimo = new Command(AccionUltimo);
            ComandoNuevo = new Command(AccionNuevo);
            ComandoInsertar = new Command(AccionInsertar);
            ComandoModificar = new Command(AccionModificar);
            ComandoBorrar = new Command(AccionBorrar);
            ComandoActualizar = new Command(AccionActualizar);

            i = 0;

            ActivarCajasTexto = false;
            ActivarBotonInsertar = false;
            ActivarRestoBotones = true;

            VerDatos();

        }

        private void VerDatos()
        {
            DataRow F;

            F = DataS.Tables["Libros"].Rows[i];
            Titulo = F["Titulo"].ToString();
            ISBN = F[1].ToString();
            Autor = F[2].ToString();
            Editorial = F[3].ToString();

            Reg();
        }

        private void Reg()
        {
            int P = i + 1;
            Nreg = P + "/" + (DataS.Tables["Libros"].Rows.Count).ToString();
        }

        private void AccionActualizar(object obj)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            Conexion.Open();
            DD.Update(DataS, "Libros"); // esta linea actualiza la base de datos
            MessageBox.Show("Base de Datos Actualizada");
            Conexion.Close();
        }

        private void AccionBorrar(object obj)
        {
            Actualizar();

            DataRow F;
            F = DataS.Tables["Libros"].Rows[i]; //miramos la fila en la que estamos

            //preguntamos si quiere borrar y guardamos la opcion en "respuesta"
            MessageBoxResult respuesta = MessageBox.Show("¿Estas seguro de borrar este registro?"
                , "Eliminacion registro", MessageBoxButton.YesNo);
            if (respuesta == MessageBoxResult.Yes)
            {
                F.Delete();

                //esto se hace para bases de datos desconectadas
                DataTable TablaBorrados;
                TablaBorrados = DataS.Tables["Libros"].GetChanges(DataRowState.Deleted);

                DD.Update(TablaBorrados); // actualizamos base de datos, se abre automaticamente
                Conexion.Close();

                DataS.Tables["Libros"].AcceptChanges();

                i = 0;//refrescamos para que no aparezca el registro borrado
                VerDatos();
            }

        }

        private void AccionModificar(object obj)
        {
            if(bandera == false)
            {
                bandera = true;
                ActivarCajasTexto = true;
            }
            else
            {
                DataRow F;
                F = DataS.Tables["Libros"].Rows[i];
                F["Titulo"] = Titulo;
                F["ISBN"] = ISBN;
                F["Autor"] = Autor;
                F["Editorial"] = Editorial;
                MessageBox.Show("Registro Modificado");

                bandera = false;
                ActivarCajasTexto = false;
            }
        }

        private bool foco;
        public bool Foco
        {
            get { return foco; }
            set
            {
                foco = value;
                OnPropertyChanged("Foco");
            }
        }

        private void AccionInsertar(object obj)
        {
                DataRow F;
                F = DataS.Tables["Libros"].NewRow();

                F["Titulo"] = Titulo;
                F["ISBN"] = ISBN;
                F["Autor"] = Autor;
                F["Editorial"] = Editorial;

                DataS.Tables["Libros"].Rows.Add(F);//Añadir nueva fila
                MessageBox.Show("Registro Insertado");

                i = DataS.Tables["Libros"].Rows.Count - 1; // para saber cuantas filas tiene
                Reg();

            ActivarCajasTexto = false;
            ActivarRestoBotones = true;
            ActivarBotonInsertar = false;

            Foco = false;
        }

        private void AccionNuevo(object obj)
        {
            ActivarCajasTexto = true;
            ActivarBotonInsertar = true;
            ActivarRestoBotones = false;

            Titulo = "";
            ISBN = "";
            Autor = "";
            Editorial = "";

            Nreg = "Nuevo";

            Foco = true;
        }

        private void AccionUltimo(object obj)
        {
            i = DataS.Tables["Libros"].Rows.Count - 1;
            VerDatos();
        }

        private void AccionAnterior(object obj)
        {
            if (i == 0)
            {
                MessageBox.Show("Primer Registro");
            }
            else
            {
                i = i - 1;
                VerDatos();
            }
        }

        private void AccionSiguiente(object obj)
        {
            if (i == DataS.Tables["Libros"].Rows.Count - 1)
            {
                MessageBox.Show("último Registro");
            }
            else
            {
                i = i + 1;
                VerDatos();
            }
        }

        private void AccionPrimero(object obj)
        {
            i = 0;
            VerDatos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //METODOS
        private bool activarCajasTexto;
        public bool ActivarCajasTexto
        {
            get { return activarCajasTexto; }
            set
            {
                activarCajasTexto = value;
                OnPropertyChanged("ActivarCajasTexto");
            }
        }

        private bool activarBotonInsertar;
        public bool ActivarBotonInsertar
        {
            get { return activarBotonInsertar; }
            set
            {
                activarBotonInsertar = value;
                OnPropertyChanged("ActivarBotonInsertar");
            }
        }

        private bool activarRestoBotones;
        public bool ActivarRestoBotones
        {
            get { return activarRestoBotones; }
            set
            {
                activarRestoBotones = value;
                OnPropertyChanged("ActivarRestoBotones");
            }
        }

    }
}