using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetroMVVM.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using transportLibrairie;


namespace MetroMVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour MetroView.xaml
    /// </summary>
    public partial class MetroView : UserControl
    {
        /*
         * On déclare une variable du type de notre Viewmodel
         */
        private MetroViewModel _metroViewModel;

        private String X { get; set; }
        private String Y { get; set; }
        private String D { get; set; }


        public MetroView()
        {
            InitializeComponent();
            /*
             * à l'initialisation de l'app on instancie notre
             * variable précédement crée
             */
            _metroViewModel = new MetroViewModel();


        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {

            X = Longi.Text;
            Y = Lati.Text;
            D = Dist.Text;
            /*
             * On utilise la méthode LoadStopList du viewModel
             * qui appelle l'openData de la métro via le model
             * qui existe dans transportLibrairie : Lignes
             */
            _metroViewModel.LoadStopList(X, Y, D);
            /*
             * On indique le data contexte 
             */
            base.DataContext = _metroViewModel;
            /*
             * On attribut à la listview Lv son itemsSource
             * qui permettra d'utiliser
             * la directive Binding dans la view
             * nb. On aurait pu mettre cette propriété directement dans la balise
             * ListView de MetroView mais pour pouvoir la redéfinir à null (écraser la valeur donnée par
             * le biais de la fonction button_click) dans Reset
             * on la définit ici
             */
            Lv.ItemsSource = _metroViewModel.FinalLib;
            /*
             * On boucle sur la Méthode Add_Pin_Stops pour afficher les pushpin correspondants aux arrets
             * sur la carte.
             * FinalLib est la liste que l'on récupère grâce à l'instance _metroViewModel
             * issue de notre viewModel
             */
            for (int i = 0; i < _metroViewModel.FinalLib.Count; i++)
            {
                Add_Pin_Stops(_metroViewModel.FinalLib[i].Lat, _metroViewModel.FinalLib[i].Lon, _metroViewModel.FinalLib[i].Name);
            }
            /*
             * On appelle la fonction qui place le pushpin
             * Vous êtes ici
             */
            You_Are_Here(Y, X);

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

        /*
         * Lors du clique sur Reset, On indique que la listView
         * devient null, ce qui la remet à zéro.
         * Lorsque une nouvelle recherche sera triggered via button_click
         * l'itemSource de la Lv redeviendra _metroViewModel.FinalLib
         */

        public void Button_Reset(object sender, RoutedEventArgs e)
        {
            Lv.ItemsSource = null;
            myMap.Children.Clear();
        }


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
