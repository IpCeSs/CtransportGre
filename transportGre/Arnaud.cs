using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transportGre
{
    class Arnaud
    {
        /*
         *Obtenus grâce à http://json2csharp.com/
         * on a passé l'url openData dans le convertiseur json to c#
         */
        public string id { get; set; }
        public string name { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public List<string> listArnauds { get; set; }
    }
}
