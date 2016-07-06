namespace SharpLinq
{
	public class Student : NamedEntity
	{
		public int Mark { get; set; }

		public override string ToString() => $"Name: {Name,-10}, Mark: {Mark,2}";
	}
}