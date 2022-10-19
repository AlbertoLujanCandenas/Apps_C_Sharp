using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace UniversalAppDialogoMVVM.ViewModel
{
    public class Class1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand ComandoBoton { get; set; }

        private string resultado;
        public string Resultado
        {
            get { return resultado; }
            set
            {
                resultado = value;

                OnPropertyChanged("Resultado");
            }
        }

        public Class1()
        {
            ComandoBoton = new Command(AccionPulsarBtnAsync);
        }

        private async void AccionPulsarBtnAsync(object obj)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("No internet connection has been found.");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Try again",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            Resultado = command.Label;
        }
    }
}
