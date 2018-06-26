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
using System.Windows.Media;
using transportLibrairie;


namespace transportGreMVVM
{
    public partial class MainWindow : Window
    {
        /*
         * La séparation en classes partielles est faite uniquement
         * pour clarifier le code, 
         * les 3 fichiers MainWindow.xaml.cs,MainWindowMaps.cs,MainWindowBoutons.cs
         * représenten en fait la même classe
         */
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


        //Récupère le point de localisation de départ (celle entrée par l'utilisateur)
        public void You_Are_Here(String Latitude, String Longitude)
        {
            /*
             * On convertit les string (Xsaisi=Latit et YSAisi=Longit) que l'on va récupérer
             * grâce à la saisi de l'utilisateur car 
             * Location prend en charge seulement le type Double
             */
            Double LonConverted = Convert.ToDouble(Longitude, new CultureInfo("en-GB"));
            Double LatConverted = Convert.ToDouble(Latitude, new CultureInfo("en-GB"));

            //On crée un pushpin 
            Pushpin Pin_u_Here = new Pushpin();

            //auquel on attribut les coordonées récupérees dans longit et latit
            Pin_u_Here.Location = new Location(LatConverted, LonConverted);

            //permet d'afficher un message lors du survol du point
            ToolTipService.SetToolTip(Pin_u_Here, "Vous êtes ici");

            //On ajoute l'enfant(pin) à la map
            myMap.Children.Add(Pin_u_Here);

            //Recentrer la carte sur le pushpin crée
            myMap.Center = Pin_u_Here.Location;

            //Zoomer la carte sur ce même point
            myMap.ZoomLevel = 16;

            //Changer la couleur du pin pour bien le différencier des pins d'arrets
            Pin_u_Here.Background = new SolidColorBrush(Color.FromRgb(0, 0, 255));

        }


    }
}
