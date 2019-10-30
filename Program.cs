using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amotels
{
    
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ClubMedUI ui = new ClubMedUI();
                ui.Loading();
                ui.Start();
                Console.ReadKey();
                //To do list: Add things to the data base, add documantation, ?add customer functionality?
                
                
            }
        }
    }
}
