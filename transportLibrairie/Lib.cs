using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using static System.Console;

namespace transportLibrairie
{
    public class Lib
    {
        /*
         *Obtenus grâce à http://json2csharp.com/
         * on a passé l'url openData dans le convertiseur json to c#
         */
        public string Id { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public List<string> Lines { get; set; }


        public static void RequeteMetroLignes()
        {
            WebRequest request = WebRequest.Create("http://data.metromobilite.fr/api/linesNear/json?x=5.709360123&y=45.176494599999984&dist=200&details=true");

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response. 
            reader.Close();
            response.Close();

            List<Lib> ListLib = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lib>>(responseFromServer);
            /*
             *On crée une liste qui groupe by nom (touts les memes noms sont mis ensemble, et on select 
             * seulement le first de cette nouvelle liste
             */
            List<Lib> LibSansDoublons = ListLib.GroupBy(n => n.Name).Select(g => g.First()).ToList();

            // Display the content.
            /*
             * on fait un liste de la classe Arnaud
             * que l'on converti grâce au nuget json.net pour pouvoir utiliser les objets
             * on itère dans la liste avec un foreach
             */
            foreach (Lib Lib in LibSansDoublons)
            {
                WriteLine($"arret : {Lib.Name} ligne : {Lib.Lines[0]} ");
            }
            Read();
        }
    }
}
