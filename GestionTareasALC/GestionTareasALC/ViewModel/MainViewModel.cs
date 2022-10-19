using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GestionTareasALC.Conexiones;
using GestionTareasALC.Model;

namespace GestionTareasALC.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Conexion conexionDeDatos;

        public ICommand ComandoNuevo { get; set; }
        public ICommand ComandoGuardar { get; set; }
        public ICommand ComandoBorrar { get; set; }
        public ICommand ComandoModificar { get; set; }
        public ICommand ComandoEditar { get; set; }
        public ICommand ComandoCancelar { get; set; }
        public ICommand ComandoActualizar { get; set; }

        public MainViewModel()
        {
            conexionDeDatos = new Conexion();
            ListaDeTareas = conexionDeDatos.ObtenerTareas();

            ComandoNuevo = new Command(AccionNuevo);
            ComandoGuardar = new Command(AccionGuardar);
            ComandoBorrar = new Command(AccionEliminar);
            ComandoModificar = new Command(AccionModificar);
            ComandoActualizar = new Command(AccionActualizar);
            
        }

        private ObservableCollection<TTarea> listaDeTareas;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<TTarea> ListaDeTareas
        {
            get { return listaDeTareas; }
            set
            {
                listaDeTareas = value;
                OnPropertyChanged("ListaDeTareas");
            }
        }
        private TTarea tareaActual;
        public TTarea TareaActual
        {
            get { return tareaActual; }
            set
            {
                tareaActual = value;
                OnPropertyChanged("TareaActual");
                if(tareaActual != null)
                {
                    Tarea = tareaActual.Tarea;
                    Notas = tareaActual.Notas;
                    Realizada = tareaActual.Realizada;
                }
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private String tarea;
        public String Tarea
        {
            get { return tarea; }
            set
            {
                tarea = value;
                OnPropertyChanged("Tarea");
            }
        }
        private String notas;
        public String Notas
        {
            get { return notas; }
            set
            {
                notas = value;
                OnPropertyChanged("Notas");
            }
        }
        private Boolean realizada;
        public Boolean Realizada
        {
            get { return realizada; }
            set
            {
                realizada = value;
                OnPropertyChanged("Realizada");
            }
        }

        private void AccionGuardar(object obj)
        {
            TTarea nuevaTarea;
            try
            {
                nuevaTarea = new TTarea();

                nuevaTarea.Tarea = Tarea;
                nuevaTarea.Notas = Notas;
                nuevaTarea.Realizada = Realizada;

                conexionDeDatos.GuardarNuevaTarea(nuevaTarea);

                ListaDeTareas.Add(nuevaTarea);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AccionNuevo(object obj)
        {
            Tarea = "";
            Notas = "";
            Realizada = false;

            OnPropertyChanged("AccionNuevo");
        }
        private void AccionEliminar(object obj)
        {
            if (tareaActual != null)
            {
                MessageBoxResult ss = MessageBox.Show("¿Seguro que desea eliminar el libro seleccionado?", "Confirmar eliminacion de registro", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (ss == MessageBoxResult.Yes)
                {
                    int resultado = 0;
                    try
                    {
                        resultado = conexionDeDatos.BorrarTarea(tareaActual);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    if (resultado > 0)
                    {
                        ListaDeTareas.Remove(tareaActual);

                        Tarea = "";
                        Notas = "";
                        Realizada = false;
                    }
                    tareaActual = null;
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningun libro");
                }
            }
        }
        private void AccionModificar(object onj)

        {
            if (tareaActual != null)
            {
                //Activar cajas de texto
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar una tarea de la lista");
            }
        }
        private void AccionActualizar(object obj)
        {
            try
            {
                int resultado = 0;

                resultado = conexionDeDatos.ActualizarTareaActual(Tarea, Notas, Realizada);

                if (resultado > 0)
                {
                    MessageBox.Show("Libro modificado");

                    ListaDeTareas = conexionDeDatos.ObtenerTareas();

                    tareaActual.Tarea = "";
                    tareaActual.Notas = "";
                    tareaActual.Realizada = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
