using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using static System.Console;

namespace transportGre
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("http://data.metromobilite.fr/api/linesNear/json?x=5.709360123&y=45.176494599999984&dist=1000&details=true");

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


            // Display the content.
            /*
             * on fait un liste de la classe Arnaud
             * que l'on converti grâce au nuget json.net pour pouvoir utiliser les objets
             * on itère dans la liste avec un foreach
             */
            List<Arnaud> ListArnauds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Arnaud>>(responseFromServer);
            /*
             *On crée une liste qui groupe by nom (touts les memes noms sont mis ensemble, et on select 
             * seulement le first de cette nouvelle liste
             */
            List<Arnaud> ArnaudSansDoublons = ListArnauds.GroupBy(n => n.Name).Select(g => g.First()).ToList();
            foreach (Arnaud ListArnaud in ArnaudSansDoublons)
            {
                          
                WriteLine(ListArnaud.Name);
            }

            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
            Read();
        }
    }
}
