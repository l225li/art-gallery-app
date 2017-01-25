using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace test // namespace is related to the visibility of your project
{
	public class Student : IComparable
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

		public int CompareTo(object obj)
		{
			return -this.score.CompareTo((obj as Student).score);
		}

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
		Student[] students;
		public void displayStudent() 
		{
			for (int i = 0; i < students.Length; i++)
			{
				Console.WriteLine(students[i].ToString());
			}
		}
		public void sortByScore()
		{
			Array.Sort(students);
		}
		public void sortByName()
		{
			studentComparerOnName sc = new studentComparerOnName();
			Array.Sort(students, sc);

		}
		public void start()
		{

			//String input;
			//int numStudents;
			//Console.WriteLine("Please enter the number of students?");
			//input = Console.ReadLine();
			//bool result = int.TryParse(input, out numStudents);
			//studentComparerOnName sc = new studentComparerOnName();
			//if ((sc != null) && result)
			//{
			//	students = new Student[numStudents];

			//	for (int i = 0; i < numStudents; i++)
			//	{
			//		Student student = new Student();
			//		Console.WriteLine("Please enter your name:");
			//		input = Console.ReadLine();
			//		student.name = input;
			//		Console.WriteLine("enter score:");
			//		input = Console.ReadLine();
			//		student.score = float.Parse(input);
			//		students[i] = student;

			//	}
			//	Console.WriteLine("\nStudents:\n");
			//	//Array.Sort(students);

			//	Array.Sort(students, sc);
			//	for (int i = 0; i < numStudents; i++)
			//	{
			//		Console.WriteLine(students[i].ToString());
			//	}
			// // =================== Serializaiton =======================
			//	XmlSerializer xs = new XmlSerializer(typeof(Student[]));
			//	using (StreamWriter wr = new StreamWriter("students.xml"))
			//	{
			//		xs.Serialize(wr, students);
			//	}

			//}
			//else
			//{
			//	Console.WriteLine("Wrong input!");
			//}

			// =================== Deserializaiton =======================
			XmlSerializer xs = new XmlSerializer(typeof(Student[]));
			using (StreamReader rd = new StreamReader("students.xml"))
			{
				students = xs.Deserialize(rd) as Student[];
			}
			sortByScore();
			displayStudent();
		}
		public static void Main(string[] args)
		{
			MainClass m = new MainClass();
			m.start();


		}

	}
}
