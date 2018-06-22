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


namespace transportGreMVVM
{
     public partial class MainWindow : Window
    {

        private String XSaisi { get; set; }
        private String YSaisi { get; set; }
        private String DSaisi { get; set; }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            XSaisi = Lon.Text.ToString();
            YSaisi = Lat.Text.ToString();
            DSaisi = Dist.Text.ToString();
            try
            {
                Lib Bib = new Lib("http://data.metromobilite.fr/api/linesNear/json?x=" + XSaisi + "&y=" + YSaisi + "&dist=" + DSaisi + "&details=true");

                List<Lignes> ListLib = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lignes>>(Bib.ResponseFromServer);
                List<Lignes> LibSansDoublons = ListLib.GroupBy(n => n.Name).Select(g => g.First()).ToList();


                // Display the content.
                /*
                 * on fait un liste de la classe Arnaud
                 * que l'on converti grâce au nuget json.net pour pouvoir utiliser les objets
                 * on itère dans la liste avec un foreach
                 */
                foreach (Lignes Lib in LibSansDoublons)
                {
                    Lx.Items.Add(Lib.Name);
                }
            }
            catch (Exception ex)
            {
                Lx.Items.Add("Numbers Only. Press reset to try again!");
                Debug.WriteLine(ex.GetType().FullName);
            }
        }

        public void Button_Reset(object sender, RoutedEventArgs e)
        {
            Lx.Items.Clear();
        }
    }
}
