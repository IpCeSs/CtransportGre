using transportLibrairie;
using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using transportLibrairie.Properties;


namespace transportLibrairie
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = Settings.Default.URL;

            Lib Bib=new Lib("http://data.metromobilite.fr/api/linesNear/json?x=5.709360123&y=45.176494599999984&dist=800&details=true");
            List<Lignes> ListLib = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lignes>>(Bib.ResponseFromServer);
            /*
             *On crée une liste qui groupe by nom (touts les memes noms sont mis ensemble, et on select 
             * seulement le first de cette nouvelle liste
             */
            List<Lignes> LibSansDoublons = ListLib.GroupBy(n => n.Name).Select(g => g.First()).ToList();

            // Display the content.
            /*
             * on fait un liste de la classe Arnaud
             * que l'on converti grâce au nuget json.net pour pouvoir utiliser les objets
             * on itère dans la liste avec un foreach
             */
            foreach (Lignes Lib in LibSansDoublons)
            {
                WriteLine($"arret : {Lib.Name} ligne : {Lib.Lines[0]} ");
            }
            Read();
        }
    }
}

