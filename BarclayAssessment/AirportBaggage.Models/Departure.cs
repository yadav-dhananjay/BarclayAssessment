using System;

namespace AirportBaggage.Models
{
	public class Departure
	{
		public string FlightId { get; private set; }
		public string FlightGate { get; private set; }
		public string Destination { get; private set; }
		public DateTime FlightTime { get; private set; }

		public Departure(string flightId,string flightGate,string destination,DateTime flightTime)
		{
			if (flightId == null || flightGate == null || destination == null)
				throw new ArgumentNullException();
			FlightId = flightId;
			FlightGate = flightGate;
			Destination = destination;
			FlightTime = flightTime;
		}

		public static Departure Parse(string s)
		{
			var a = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			DateTime flightTime;
			if (a.Length != 4 || !DateTime.TryParse(a[3], out flightTime))
				throw new ArgumentException("Each entry in 'Section: Departures' must have the following format: {{string} {string} {string} {DateTime}}.");
			return new Departure(a[0], a[1], a[2], flightTime);
		}
	}
}
