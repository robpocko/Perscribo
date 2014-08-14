using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.EF.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            PerscriboContext db = new PerscriboContext();

            var dbQuery = db.Agencies.Where(id => id.ID > 0).ToArray();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
