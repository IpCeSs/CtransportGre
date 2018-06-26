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



        public string ResponseFromServer { get; set; }

        /*
         * Constructeur de nouvelle instance lib cree une connexion à l'api
         * à chaque nouvelle instanciation de la classe
         */
        public Lib(String url)
        {
            WebRequest request = WebRequest.Create(url);

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            ResponseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response. 
            reader.Close();
            response.Close();
        }
    }
}
