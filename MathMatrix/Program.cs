using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Core c = new Core())
            {
                c.Start();
            }
            Console.ReadKey();
        }
    }
}
