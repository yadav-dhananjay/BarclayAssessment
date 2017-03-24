using System;

namespace AirportBaggage.Models
{
	public class Bag
	{
		public string BagNumber { get; private set; }
		public string EntryPoint { get; private set; }
		public string FlightId { get; private set; }

		public Bag(string bagNumber,string entryPoint,string flightId)
		{
			if (bagNumber == null || entryPoint == null || flightId == null)
				throw new ArgumentNullException();
			BagNumber = bagNumber;
			EntryPoint = entryPoint;
			FlightId = flightId;
		}

		public static Bag Parse(string s)
		{
			var a = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (a.Length != 3)
				throw new ArgumentException("Each entry in 'Section: Bags' must have the following format: {{string} {string} {string}}.");
			return new Bag(a[0], a[1], a[2]);
		}
	}
}
