using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfCMVVM.conexiones;
using WpfCMVVM.model;

namespace WpfCMVVM.viewmodel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Conexion conexionDeDatos;

        public ICommand ComandoNuevo { get; set; }
        public ICommand ComandoGuardar { get; set; }
        public ICommand ComandoEliminar { get; set; }
        public ICommand ComandoModificar { get; set; }
        public ICommand ComandoActualizar { get; set; }
        public ICommand ComandoCancelar { get; set; }


        public MainViewModel()
        {

            conexionDeDatos = new Conexion();
            ListaLibros = conexionDeDatos.ObtenerLibros();

            ComandoNuevo = new Command(AccionNuevo);
            ComandoGuardar = new Command(AccionGuardar);
            ComandoEliminar = new Command(AccionEliminar);
            ComandoModificar = new Command(AccionModificar);
            ComandoActualizar = new Command(AccionActualizar);
            ComandoCancelar = new Command(AccionCancelar);
        }

        private ObservableCollection<CLibro> listaLibros;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<CLibro> ListaLibros
        {
            get { return listaLibros; }
            set
            {
                listaLibros = value;
                OnPropertyChanged("ListaLibros");

            }
        }
        private CLibro libroActual;
        public CLibro LibroActual
        {
            get { return libroActual; }
            set
            {
                libroActual = value;
                OnPropertyChanged("LibroActual");
                if(libroActual != null)
                {
                    Titulo = libroActual.Titulo;
                    Isbn = libroActual.Isbn;
                    Autor = libroActual.Autor;
                    Editorial = libroActual.Editorial;
                }
            }
        }
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
        public string Isbn
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

        private void AccionGuardar(object obj)
        {
            CLibro nuevoLibro;
            try
            {
                nuevoLibro = new CLibro();

                nuevoLibro.Titulo = Titulo;
                nuevoLibro.Isbn = Isbn;
                nuevoLibro.Autor = Autor;
                nuevoLibro.Editorial = Editorial;

                conexionDeDatos.GuardarNuevoLibro(nuevoLibro);

                listaLibros.Add(nuevoLibro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AccionNuevo(object obj)
        {
            Titulo = "";
            Isbn = "";
            Autor = "";
            Editorial = "";

            OnPropertyChanged("AccionNuevo");
        }

        private void AccionEliminar(object obj)
        {
            if (libroActual != null)
            {
                MessageBoxResult ss = MessageBox.Show("¿Seguro que desea eliminar el libro seleccionado?", "Confirmar eliminacion de registro", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (ss == MessageBoxResult.Yes)
                {
                    int resultado = 0;
                    try
                    {
                        resultado = conexionDeDatos.BorrarLibro(Isbn);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    if (resultado > 0)
                    {
                        ListaLibros.Remove(libroActual);

                        Titulo = "";
                        Isbn = "";
                        Autor = "";
                        Editorial = "";
                    }
                    libroActual = null;
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningun libro");
                }
            }
        }

        private void AccionModificar(object onj)
        {
            if (libroActual != null)
            {
                //Activar cajas de texto
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un libro de la lista");
            }
        }

        private void AccionActualizar(object obj)
        {
            try
            {
                int resultado = 0;

                resultado = conexionDeDatos.ActualizarLibroActual(Titulo, Isbn, Autor, Editorial);

                if (resultado > 0)
                {
                    MessageBox.Show("Libro modificado");

                    ListaLibros = conexionDeDatos.ObtenerLibros();

                    libroActual.Titulo = "";
                    libroActual.Autor = "";
                    libroActual.Isbn = "";
                    libroActual.Editorial = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AccionCancelar(object obj)
        {
            libroActual = null;

            Titulo = "";
            Isbn = "";
            Autor = "";
            Editorial = "";
        }
    }
    }
