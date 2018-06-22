using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transportLibrairie;

namespace Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            Lib test = new Lib("https://www.google.com/");
            Console.WriteLine(test.ResponseFromServer);
            Console.Read();
        }
    }
}
