using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using transportLibrairie;

namespace transportGreMVVM
{
    public partial class MainWindow : Window
    {
        
        private void Change_Map_Mode(object sender, RoutedEventArgs e)
        {
            if (myMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.RoadMode")
            {
                //Set the map mode to Aerial with labels
                myMap.Mode = new AerialMode(true);
            }
            else if (myMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.AerialMode")
            {
                //Set the map mode to RoadMode
                myMap.Mode = new RoadMode();
            }
        }

        //Récupérer tous les points de la liste d'arrets
        public void Add_Pin_Stops(Double Lat, Double Lon, String Name)
        {
             Location pinLocation = new Location(Lat, Lon);
            //The pushpin to add to the map.
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            //permet d'afficher le nom de la station on hover sur le pushpin
            ToolTipService.SetToolTip(pin, $"{Name}");
            myMap.Children.Add(pin);
        }

        //Récupère le point de location de départ

       
        public void You_Are_Here(String Longitude, String Latitude)
        {
          
            var loc = new Location(Convert.ToDouble(Longitude, new CultureInfo("en-GB")), Convert.ToDouble(Latitude, new CultureInfo("en-GB")));
          
            Pushpin Pin_u_Here = new Pushpin();
            
            ToolTipService.SetToolTip(Pin_u_Here, "Vous êtes ici");
            myMap.Children.Add(Pin_u_Here);
        }
        

    }
}
