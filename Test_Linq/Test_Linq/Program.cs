using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var emptyList = new List<string>();
            var result = emptyList.Where(e => e.Equals("asdf"));
            result.ToList().ForEach(r => Console.WriteLine(r));
            Console.WriteLine("test");
            Console.ReadKey();

        }
    }
}
