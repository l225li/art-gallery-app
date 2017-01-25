using System;
using System.Collections.Generic;

namespace arraylistStudent // namespace is related to the visibility of your project
{
	class Student
	{
		private String s_name;
		private float s_score;

		public String name
		{
			get { return s_name; }
			set { if (value.Length == 2) { s_name = value; } }
		}
		public float score
		{
			get { return s_score; }
			set { if (value >= 0 && value <= 20) { s_score = value; } else { s_score = -1; } }
		}

		//public int CompareTo(object obj)
		//{
		//	return -this.score.CompareTo((obj as Student).score);
		//}

		public override string ToString()
		{
			return string.Format("[Student: name={0}, score={1}]", name, score);
		}
	}

	// Comparer for student on names 
	public class studentComparerOnName : IComparer<object>
	{
		public int Compare(object x, object y)
		{
			var a = x as Student;
			var b = y as Student;
			return String.Compare(a.name, b.name, StringComparison.Ordinal);
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
			//try
			//{
			//	object myObject = null; // my promise to sth, but not yet initialized
			//	Console.WriteLine("Let's start");
			//	myObject.ToString();
			//}
			//catch(Exception e) 
			//{
			//	Console.WriteLine(e.Message);
			//	Console.ReadLine();
			//}

			List<Student> students = new List<Student>();
			int counter = 0;
			bool ifContinue = true;
			while (ifContinue)
			{
				Student student = new Student();
				String input;
				Console.WriteLine("Please enter your name:");
				input = Console.ReadLine();
				student.name = input;
				Console.WriteLine("enter score:");
				input = Console.ReadLine();
				student.score = float.Parse(input);
				Console.WriteLine("name: " + student.name + " score: " + student.score);
				Console.WriteLine(student.ToString());
				students.Add(student);
				counter++;
				Console.WriteLine("Continue? (y/n)");
				input = Console.ReadLine();
				if (input == "y")
				{
					ifContinue = true;
				}
				else
				{
					ifContinue = false;


					students.ForEach(delegate (Student student1)
					{
						Console.WriteLine(student1.ToString());
					});
				}

			}

		}
	}
}
