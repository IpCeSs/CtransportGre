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
        public string Id { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public List<string> ListArnauds { get; set; }
    }
}
