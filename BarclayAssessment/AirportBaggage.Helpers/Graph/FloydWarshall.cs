using System;
using System.Collections.Generic;

namespace AirportBaggage.Helpers.Graph
{
	public static class Graph
	{
        //https://en.wikipedia.org/wiki/Floyd%E2%80%93Warshall_algorithm
		public class FloydWarshall
		{
		    private readonly int[,] _next;
		    readonly int[,] _distances;
			public bool HasNegativeCycle { get; private set; }

			public FloydWarshall(int[] edges, int[] weights)
			{
				if (edges.Length != 2 * weights.Length)
					throw new ArgumentException("edges.Length != 2 * weights.Length");
				var n = 0;
				for (var i = 0; i < edges.Length; ++i)
					if (n < edges[i])
						n = edges[i];
				if (edges.Length > 0)
					++n;
				_next = new int[n, n];
				_distances = new int[n, n];
				for (var i = 0; i < n; ++i)
					for (var j = 0; j < n; ++j)
					{
						_next[i, j] = -1;
						_distances[i, j] = int.MaxValue;
					}
				for (var i = 0; i < edges.Length; i += 2)
				{
					_next[edges[i], edges[i + 1]] = edges[i + 1];
					_distances[edges[i], edges[i + 1]] = weights[i / 2];
				}
				for (var i = 0; i < n; ++i)
					if (_distances[i, i] > 0)
					{
						_next[i, i] = i;
						_distances[i, i] = 0;
					}
				for (var p = 0; p < n; ++p)
					for (var i = 0; i < n; ++i)
					{
						if (_next[i, p] != -1)
							for (var j = 0; j < n; ++j)
								if (_next[p, j] != -1 && _distances[i, j] > checked(_distances[i, p] + _distances[p, j]))
								{
									_next[i, j] = _next[i, p];
									_distances[i, j] = _distances[i, p] + _distances[p, j];
								}
						if (_distances[i, i] < 0)
						{
							HasNegativeCycle = true;
							return;
						}
					}
			}

			public int GetDistance(int u, int v)
			{
				if (HasNegativeCycle)
					throw new InvalidOperationException("There is a negative cycle.");
				return _distances[u, v];
			}

			public bool HasPath(int u, int v)
			{
				return _next[u, v] != -1;
			}

			public IEnumerable<int> GetPath(int u, int v)
			{
				if (HasNegativeCycle)
					throw new InvalidOperationException("There is a negative cycle.");
				if (_next[u, v] != -1)
				{
					yield return u;
					while (u != v)
					{
						u = _next[u, v];
						yield return u;
					}
				}
			}
		}

       // https://en.wikipedia.org/wiki/Dijkstra's_algorithm
        public class Dijkstra
        {
            
        }
	}
}
