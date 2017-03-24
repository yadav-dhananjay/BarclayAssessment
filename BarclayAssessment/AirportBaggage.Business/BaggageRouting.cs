using System;
using System.Collections.Generic;
using System.Linq;
using AirportBaggage.Helpers.Graph;
using AirportBaggage.Models;

namespace AirportBaggage.Business
{
    public class BaggageRouting
  {
		public List<Path> Paths { get; private set; }
		public List<Departure> Departures { get; private set; }
		public List<Bag> Bags { get; private set; }

        public IEnumerable<Route> Routes { get; private set; }

        public BaggageRouting(IEnumerable<Path> paths,IEnumerable<Departure> departures,IEnumerable<Bag> bags)
		{
			if (paths.Any(p => p == null) || departures.Any(d => d == null) || bags.Any(b => b == null))
				throw new ArgumentException();
			Paths = paths.ToList();
			Departures = departures.ToList();
			Bags = bags.ToList();
		}

        public static BaggageRouting Parse(string s)
		{
			var sections = s.Split(new string[] { "# Section: Conveyor System", "# Section: Departures", "# Section: Bags" }, StringSplitOptions.RemoveEmptyEntries);
			if (sections.Length != 3)
				throw new ArgumentException("The problem statement must contain the following sections: '# Section: Conveyor System', '# Section: Departures', '# Section: Bags'.");
            return new BaggageRouting(
				sections[0].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => Path.Parse(l)),
				sections[1].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => Departure.Parse(l)),
				sections[2].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => Bag.Parse(l)));
		}

        public RouteResult Routing()
		{
			var fromNodes = new Dictionary<string, int>();
			var edges = new int[4 * Paths.Count];
			var weights = new int[2 * Paths.Count];
			// Last duplicates are used in computations.
			for (var i = 0; i < Paths.Count; ++i)
			{
				var p = Paths[i];
				if (!fromNodes.ContainsKey(p.Node1))
					fromNodes.Add(p.Node1, fromNodes.Count);
				if (!fromNodes.ContainsKey(p.Node2))
					fromNodes.Add(p.Node2, fromNodes.Count);
				edges[4 * i + 0] = fromNodes[p.Node1];
				edges[4 * i + 1] = fromNodes[p.Node2];
				edges[4 * i + 2] = fromNodes[p.Node2];
				edges[4 * i + 3] = fromNodes[p.Node1];
				weights[2 * i + 0] = p.TravelTime;
				weights[2 * i + 1] = p.TravelTime;
			}
			var toNodes = new string[fromNodes.Count];
			foreach (var p in fromNodes)
				toNodes[p.Value] = p.Key;

			var fw = new Graph.FloydWarshall(edges, weights);

			// Last duplicates are used in computations.
			var fromFlights = new Dictionary<string, string>();
			foreach (var d in Departures)
				fromFlights[d.FlightId] = d.FlightGate;

            return new RouteResult(Bags.Select(b =>
			{
				var entry = fromNodes[b.EntryPoint];
				var destination = fromNodes[fromFlights[b.FlightId]];
				return new Route(
					b.BagNumber,
					fw.GetPath(entry, destination).Select(u => toNodes[u]),
					fw.GetDistance(entry, destination));
			}));
		}
	}

}
