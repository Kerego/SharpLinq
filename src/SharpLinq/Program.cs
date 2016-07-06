using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpLinq
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//Create and populate list of students
			List<Student> students = GenerateStudents();

			//using delegate
			var comparison = new Comparison<Student>(CompareStudents);
			students.Sort(comparison);

			//using anon functions
			students.Sort(delegate (Student a, Student b)
			{
				return a.Mark - b.Mark;
			});

			//using lambda
			students.Sort((a, b) => a.Mark - b.Mark);

			//extension methods, deffered execution
			var paginated = students.Paginate(pageSize: 3);

			students.RemoveAll(x => x.Name.Contains("Scott"));

			int cursor = 0;
			int count = paginated.Count();

			while (true)
			{
				Console.WriteLine(paginated.ElementAt(cursor).ToJson());
				var key = Console.ReadKey();
				Console.Clear();

				if (key.Key == ConsoleKey.LeftArrow)
				{
					cursor = cursor > 0 ? cursor - 1 : 0;
				}
				else if (key.Key == ConsoleKey.RightArrow)
				{
					cursor = cursor < count - 1 ? cursor + 1 : count - 1;
				}
			}
			
		}

		private static List<Student> GenerateStudents()
		{
			string[] names = { "Bill", "Scott", "James" };
			string[] surnames = { "Guthrie", "Hanselman", "Gates", "Hunter" };

			List<Student> students = new List<Student>();
			Random rand = new Random();

			for (int i = 0; i < names.Length; i++)
				for (int k = 0; k < surnames.Length; k++)
					students.Add(new Student
					{
						Name = $"{names[i]} {surnames[k]}",
						Mark = rand.Next(5, 10),
						Id = i * surnames.Length + k
					});

			return students;
		}

		public static int CompareStudents(Student a, Student b) =>
			a.Mark - b.Mark;

	}

}
