using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;
using transportLibrairie;

namespace MetroMVVM.ViewModel
{
    class MetroViewModel
    {
        /*
         * On crée une list de Lignes (MODEL issu de transportLibrairie)
         * Vide qui récupérera la valeur de LoadStopList()
         */
        public  List<Lignes> FinalLib
        {
            get;
            set;
        }
        public  void LoadStopList(String XSaisi, String YSaisi, String DSaisi)
        {
            
        
            /*
             * On encapsule dans un try catch
             * qui attrappera l'exception si l'utilisateur
             * entre un mauvais format de données (!=coordonées Geoloc)
             */
            try
            {
                
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
                 * On Stoke la liste obtenue dans notre Liste crée
                 * en début de classe. C'est elle que l'on appelera pour récupérer
                 * les valeurs
                 */
                FinalLib = LibSansDoublons;


            }
            catch (Exception ex)
            {
               // Lv.Items.Add("Numbers Only. Press reset to try again!");
                Debug.WriteLine(ex.GetType().FullName);
                
            }
        }

       
    }
}
