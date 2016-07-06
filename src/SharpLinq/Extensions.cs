using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpLinq
{
	public static class Extensions
	{

		private static Random rand = new Random();
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection, int r = 0)
		{
			return collection.OrderBy(x => r = rand.Next(100));
		}

		public static string Dump<T>(this IEnumerable<T> collection)
		{
			return collection.Aggregate(string.Empty, (acc, v) => acc + v.ToJson() + "\r\n");
		}

		public static string ToJson(this object obj) =>
			JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});


		public static IEnumerable<Page<T>> Paginate<T>(this IEnumerable<T> sequence, int pageSize)
		{
			if (pageSize < 1)
				throw new ArgumentException("Invalid Page Size", nameof(pageSize));
			var items = new List<T>();
			int i = 1;
			foreach (var item in sequence)
			{
				if (items.Count == pageSize)
				{
					yield return new Page<T>(i++, items);
					items = new List<T>();
				}
				items.Add(item);
			}
			if (items.Count != 0)
				yield return new Page<T>(i++, items);
		}
	}
}
