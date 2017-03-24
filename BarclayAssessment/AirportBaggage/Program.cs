using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportBaggage.Business;
using AirportBaggage.Models;

namespace AirportBaggage
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 Approch. uncomments below code and comment the 2nd approach
            
            var paths = new List<Path>();
            var bags = new List<Bag>();
            var departures = new List<Departure>();
            paths.Add(Path.Parse("Concourse_A_Ticketing A5 5"));

            paths.Add(Path.Parse("A5 BaggageClaim 5"));
            paths.Add(Path.Parse("A5 A10 4"));
            paths.Add(Path.Parse("A5 A1 6"));
            paths.Add(Path.Parse("A1 A2 1"));
            paths.Add(Path.Parse("A2 A3 1"));
            paths.Add(Path.Parse("A3 A4 1"));
            paths.Add(Path.Parse("A10 A9 1"));
            paths.Add(Path.Parse("A9 A8 1"));
            paths.Add(Path.Parse("A8 A7 1"));
            paths.Add(Path.Parse("A7 A6 1"));


            departures.Add(Departure.Parse("UA10 A1 MIA 08:00"));
            departures.Add(Departure.Parse("UA11 A1 LAX 09:00"));
            departures.Add(Departure.Parse("UA12 A1 JFK 09:45"));
            departures.Add(Departure.Parse("UA13 A2 JFK 08:30"));
            departures.Add(Departure.Parse("UA14 A2 JFK 09:45"));
            departures.Add(Departure.Parse("UA15 A2 JFK 10:00"));
            departures.Add(Departure.Parse("UA16 A3 JFK 09:00"));
            departures.Add(Departure.Parse("UA17 A4 MHT 09:15"));
            departures.Add(Departure.Parse("UA18 A5 LAX 10:15"));


            bags.Add(Bag.Parse("0001 Concourse_A_Ticketing UA12"));
            bags.Add(Bag.Parse("0002 A5 UA17"));
            bags.Add(Bag.Parse("0003 A2 UA10"));
            bags.Add(Bag.Parse("0004 A8 UA18"));
            bags.Add(Bag.Parse("0005 A7 ARRIVAL"));
            var p = new BaggageRouting(paths, departures, bags); 
            

            

           



            // I would rather just add an additional section in the input to handle this.
            p.Departures.Add(Departure.Parse("ARRIVAL BaggageClaim XXX 00:00"));
            var result = p.Routing();
            Console.Write(result);
            Console.ReadKey();
        }
    }
}
