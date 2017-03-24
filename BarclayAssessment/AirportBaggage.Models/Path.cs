using System;

namespace AirportBaggage.Models
{
    public class Path
    {
        public string Node1 { get; private set; }
        public string Node2 { get; private set; }
        public int TravelTime { get; private set; }

        public Path(string node1, string node2, int travelTime)
        {
            if (node1 == null || node2 == null)
                throw new ArgumentNullException();
            if (travelTime < 0)
                throw new ArgumentException();
            Node1 = node1;
            Node2 = node2;
            TravelTime = travelTime;
        }

        public static Path Parse(string s)
        {
            var a = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int travelTime;
            if (a.Length != 3 || !int.TryParse(a[2], out travelTime))
                throw new ArgumentException("Each entry in 'Section: Conveyor System' must have the following format: {{string} {string} {int}}.");
            return new Path(a[0], a[1], travelTime);
        }
    }
}
