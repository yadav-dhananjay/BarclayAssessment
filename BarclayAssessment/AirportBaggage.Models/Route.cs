using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportBaggage.Models
{
	public class Route
	{
		public string BagNumber { get; private set; }
		public IEnumerable<string> Points { get; private set; }
		public int TotalTravelTime { get; private set; }

		public Route(string bagNumber,IEnumerable<string> points,int totalTravelTime)
		{
			if (bagNumber == null)
				throw new ArgumentNullException();
			if (points.Any(p => p == null))
				throw new ArgumentException();
			if (totalTravelTime < 0)
				throw new ArgumentException();
			BagNumber = bagNumber;
			Points = points.ToList();
			TotalTravelTime = totalTravelTime;
		}

		public override string ToString()
		{
			var b = new StringBuilder();
			b.Append(BagNumber);
			b.Append(' ');
			foreach (var p in Points)
			{
				b.Append(p);
				b.Append(' ');
			}
			b.Append(": ");
			b.Append(TotalTravelTime);
			return b.ToString();
		}
	}
}
