using System;

namespace delegateTest
{
	public delegate float powDelegate(float x);

	class MainClass
	{
		public static void Main(string[] args)
		{
			
			Math math = new Math();
			powDelegate pow2 = new powDelegate(math.Square);
			powDelegate pow3 = new powDelegate(math.Cube);
			Calculator calc = new Calculator(pow3);
			Console.WriteLine("Power 3 of 3: " + calc.Invoke(3) + ", power 4 of 2: " + pow2.Invoke(4));

		}

	}

	class Calculator
	{
		powDelegate pDel;

		public Calculator(powDelegate arg) 
		{
			pDel = arg;
		}
		float doCalculus(float f)
		{
			return pDel(f);
		}
	}

	class Math
	{ 
		public float Square(float a)
		{
			float square;
			square = a * a;
			return square;
		}

		public float Cube(float a)
		{
			float cube;
			cube = a * a * a;
			return cube;
		}

		public float fourthPower(float a)
		{
			float fp;
			fp = a * a * a * a;
			return fp;
		}
	}
}
