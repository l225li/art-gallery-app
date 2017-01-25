using System;
using System.Threading;

namespace ThreadingTest
{
	class MainClass
	{
		String input = "go";
		EventWaitHandle flagT1 = new EventWaitHandle(false, EventResetMode.AutoReset);
		EventWaitHandle flagT2 = new EventWaitHandle(false, EventResetMode.AutoReset);


		// Struct encapsulation for greetings 
		public struct GreetingStruct
		{
			public String msg;
			//public int period;
			public EventWaitHandle flag;
			public GreetingStruct(String p1, EventWaitHandle f1)
			{
				msg = p1;
				flag = f1;
			}
		}

		// Class encapsulation for greetings
		public class Greeting
		{
			private String s_msg;
			private int s_period;
			public String msg
			{
				get { return s_msg; }
				set { s_msg = value; }
			}
			public int period
			{ 
				get { return s_period; }
				set { s_period = value;}
			}
		}

		// ThreadStart delegates only takes one object 
		// => Use encapsulation to zip the data needed (more than 1)
		void myThread(object greeting) 
		{
			// Greeting g_greeting = (Greeting)greeting;
			GreetingStruct g_greeting = (GreetingStruct)greeting;

			while (input != "quit")
			{
				Console.WriteLine(g_greeting.msg);
				g_greeting.flag.WaitOne();

			}
		}


		public static void Main(string[] args)
		{
			
			MainClass m = new MainClass();

			// =================== important note of static/non-static ================================
			// in a static method, you cannot use other method which is not static 
			// cannot use un-static method in a static method 
			// by instantiating the class, can change from static -> non-static of method myThread() and String input

			// thread prototype: void func();
			ParameterizedThreadStart threadDelegate = new ParameterizedThreadStart(m.myThread);

			// Thread 1 
			Thread MyTh1 = new Thread(threadDelegate);
			// Class encapsulation
			//Greeting greeting1 = new Greeting();
			//greeting1.msg = "Hello";
			//greeting1.period = 1000;
			// Struct encapsulation
			GreetingStruct greeting1 = new GreetingStruct("Hello", m.flagT1);


			// Thread 2
			Thread MyTh2 = new Thread(threadDelegate);
			// Class encapsulation 

			//Greeting greeting2 = new Greeting();
			//greeting2.msg = "Bonjour";
			//greeting2.period = 500;

			// Struct encapsulation
			//GreetingStruct greeting2;
			//greeting2.msg = "Bonjour";
			//greeting2.period= 1000;
			// using "new" for constructing a struct 
			GreetingStruct greeting2 = new GreetingStruct("Bonjour", m.flagT2);


			MyTh1.Start(greeting1);
			MyTh2.Start(greeting2);



			while (m.input != "quit")
			{
				//m.input = Console.ReadLine();// Yield
				Thread.Sleep(1000);
				m.flagT1.Set();
				Thread.Sleep(1000);
				m.flagT2.Set();

			}
		}
	}
}
