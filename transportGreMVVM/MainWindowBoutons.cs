using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using transportLibrairie;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Globalization;


namespace transportGreMVVM
{
    public partial class MainWindow : Window
    {
        /*
         * On crée des variables dans lequelles on va récupérer
         * la saisie utilisateur
         */

        private String XSaisi { get; set; }
        private String YSaisi { get; set; }
        private String DSaisi { get; set; }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
             * On attribue les valeurs récupérées grâce
             * aux textbox Longi et Lati (saisie utilisateur) 
             */
            XSaisi = Longi.Text;
            YSaisi = Lati.Text;
            DSaisi = Dist.Text;

           /*
            * On encapsule l'action du click dans un try catch
            * qui attrappera l'exception si l'utilisateur
            * entre un mauvais format de données (!=coordonées Geoloc)
            */
            try
            {
                //On appelle la fonction qui affiche le pin vous êtes ici
                You_Are_Here(YSaisi, XSaisi);

                /*
                 * On crée une nouvelle instance de notre librairie transportLibrairie
                 * qui prend en charge la connexion à l'openDATA de la Metro de Grenoble
                 */

                Lib Bib = new Lib("http://data.metromobilite.fr/api/linesNear/json?x=" + XSaisi + "&y=" + YSaisi + "&dist=" + DSaisi + "&details=true");
               
                /*
                 * écriture alternative pour la concaténation >> $ {}
                 * Lib Bib = new Lib ($"http://data.metromobilite.fr/api/linesNear/json?x={XSaisi}&y={YSaisi}&dist={DSaisi}&details=true"); 
                 */

                /*
                 * on fait un liste de la classe Lignes
                 * que l'on converti grâce au nuget json.net 
                 * pour pouvoir utiliser les objets
                 */
                List<Lignes> ListLib = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lignes>>(Bib.ResponseFromServer);
                List<Lignes> LibSansDoublons = ListLib.GroupBy(n => n.Name).Select(g => g.First()).ToList();

                /*
                 * on boucle dans la liste avec un foreach
                 * pour afficher chaque arret à proximité
                 * ainsi que les pushpin correspondants
                 */
                foreach (Lignes Lib in LibSansDoublons)
                {
                    Lx.Items.Add(Lib.Name);
                    Add_Pin_Stops(Lib.Lat, Lib.Lon, Lib.Name);
                }

            }
            catch (Exception ex)
            {
                Lx.Items.Add("Numbers Only. Press reset to try again!");
                Debug.WriteLine(ex.GetType().FullName);
            }
        }

       /* Fonction pour bouton Reset qui supprime et les éléments 
        * présents dans la liste de recherche (Items)
        * et les pins(children) correspondants sur la map
        */
        public void Button_Reset(object sender, RoutedEventArgs e)
        {
            Lx.Items.Clear();
            myMap.Children.Clear();
        }

    }
}
