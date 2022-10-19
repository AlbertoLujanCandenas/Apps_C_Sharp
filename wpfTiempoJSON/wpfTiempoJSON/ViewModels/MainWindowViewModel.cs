using Newtonsoft.Json;
using wpfTiempoJSON.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace wpfTiempoJSON.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string temp;
        private string temp_min;
        private string temp_max;
        private double lat;
        private double lon;
        private string speed;
        private string image;

        public MainWindowViewModel()
        {
            LeerJSON();
        }

        public string Temp
        {
            get { return temp; }
            set
            {
                temp = value;
                OnPropertyChanged("Temp");
            }
        }

        public string Temp_min
        {
            get { return temp_min; }
            set
            {
                temp_min = value;
                OnPropertyChanged("Temp_min");
            }
        }

        public string Temp_max
        {
            get { return temp_max; }
            set
            {
                temp_max = value;
                OnPropertyChanged("Temp_max");
            }
        }

        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                OnPropertyChanged("Lat");
            }
        }

        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                OnPropertyChanged("Lon");
            }
        }

        public string Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        
        private async void LeerJSON()
        {
            string urlData = "http://api.openweathermap.org/data/2.5/find?&q=Toledo,eslang=es&units=metric&APPID=278857e8dee51f914026df21d0d40c19";
            var client = new System.Net.Http.HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(new Uri(urlData));

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var dataString = data.Result.Substring(0, data.Result.Length);
                var tiempoData = JsonConvert.DeserializeObject<Root>(dataString);
                Temp = ((Root)tiempoData).list[0].main.temp + "º";
                Temp_max = ((Root)tiempoData).list[0].main.temp_max + "º";
                Temp_min = ((Root)tiempoData).list[0].main.temp_min + "º";
                Lat = ((Root)tiempoData).list[0].coord.lat;
                Lon = ((Root)tiempoData).list[0].coord.lon;
                Speed = ((Root)tiempoData).list[0].wind.speed + "km/h";
                var imageCode = ((Root)tiempoData).list[0].weather[0].icon;
                Image = "https://openweathermap.org/img/wn/" + imageCode + "@4x.png";

                //System.Diagnostics.Debug.WriteLine(tiempoData.list[0].main.temp);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
