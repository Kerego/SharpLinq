using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLinq
{
	public struct Page<T>
	{
		public Page(int page, IEnumerable<T> items)
		{
			PageNumber = page;
			Items = items;
		}

		public int PageNumber { get; }
		public IEnumerable<T> Items { get; set; }
	}
}
