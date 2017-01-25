using System;
using System.Collections.Generic;

namespace ArtGaleryApp
{
	public delegate bool ArtSelect(ArtPiece Piece);
	public interface IArtSelect { bool SelectDrawing(ArtPiece Piece); }
	public enum Category
	{
		PAINTING = 1,
		DRAWING = 2,
		SCULPTURE = 3
	}
	public class ArtPiece : IComparable
	{
		private String s_title;
		private Category s_catergory;
		private int s_price;

		public String title
		{
			get { return s_title; }
			set { if (value.Length != 0) { s_title = value; } }
		}
		public Category category
		{
			get { return s_catergory; }
			set { s_catergory = value; }
		}
		public int price
		{
			get { return s_price; }
			set { if (value > 0) { s_price = value; } }
		}
		public int CompareTo(object obj)
		{
			return this.price.CompareTo((obj as ArtPiece).price);
		}
		public override string ToString()
		{
			return string.Format("[ArtPiece: title={0}, category={1}, price={2}]", title, category, price);
		}
	}

	public class ArtGalery
	{
		private ArtPiece[] s_artCollection;
		ArtSelect asDel;
		DrawingSelector asInter;
		public void Initialize()
		{
			ArtPiece ap1 = new ArtPiece();
			ap1.title = "Painting 1";
			ap1.category = Category.PAINTING;
			ap1.price = 30;
			ArtPiece ap2 = new ArtPiece();
			ap2.title = "Painting 2";
			ap2.category = Category.PAINTING;
			ap2.price = 120;
			ArtPiece ap3 = new ArtPiece();
			ap3.title = "Painting 3";
			ap3.category = Category.PAINTING;
			ap3.price = 150;
			ArtPiece ap4 = new ArtPiece();
			ap4.title = "Drawing 1";
			ap4.category = Category.DRAWING;
			ap4.price = 100;
			ArtPiece ap5 = new ArtPiece();
			ap5.title = "Drawing 2";
			ap5.category = Category.DRAWING;
			ap5.price = 130;
			ArtPiece ap6 = new ArtPiece();
			ap6.title = "Drawing 3";
			ap6.category = Category.DRAWING;
			ap6.price = 180;
			ArtPiece ap7 = new ArtPiece();
			ap7.title = "Sculpture 1";
			ap7.category = Category.SCULPTURE;
			ap7.price = 30;
			ArtPiece ap8 = new ArtPiece();
			ap8.title = "Sculpture 2";
			ap8.category = Category.SCULPTURE;
			ap8.price = 80;
			ArtPiece ap9 = new ArtPiece();
			ap9.title = "Sculpture 3";
			ap9.category = Category.SCULPTURE;
			ap9.price = 120;

			s_artCollection = new ArtPiece[9];
			s_artCollection[0] = ap1;
			s_artCollection[1] = ap2;
			s_artCollection[2] = ap3;
			s_artCollection[3] = ap4;
			s_artCollection[4] = ap5;
			s_artCollection[5] = ap6;
			s_artCollection[6] = ap7;
			s_artCollection[7] = ap8;
			s_artCollection[8] = ap9;
		}
		public ArtPiece[] artCollection
		{
			get { return s_artCollection; }
		}
		public List<ArtPiece> Selector(ArtSelect arg)
		{
			asDel = arg;
			List<ArtPiece> ac = new List<ArtPiece>();
			for (int i = 0; i < s_artCollection.Length; i++)
			{
				ArtPiece p = s_artCollection[i];
				if (doSelect(p)) { ac.Add(p);}
			}
			return ac;
		}

		public List<ArtPiece> Selector (DrawingSelector arg)
		{

			asInter = arg;
			List<ArtPiece> ac = new List<ArtPiece>();
			/*
			for (int i = 0; i < s_artCollection.Length; i++)
			{ 
				ArtPiece p = s_artCollection[i];
				if (asInter.SelectDrawing(p)) { ac.Add(p); }
				
			}
*/
			foreach (ArtPiece p in s_artCollection)
			{
				if (arg.SelectDrawing(p))
					ac.Add(p);
			}
			return ac;
		}

		bool doSelect(ArtPiece p)
		{
			return asDel.Invoke(p);
		}

	}


	class MainClass
	{

		public void displayArtPiece(ArtPiece[] ac)
		{
			for (int i = 0; i < ac.Length; i++)
			{
				Console.WriteLine(ac[i].ToString());
			}
		}
		public void displaySelected(List<ArtPiece> p)
		{ 
			p.ForEach(delegate (ArtPiece piece)
			{
				Console.WriteLine(piece.ToString());
			});
		
			
		}
		public static void Main(string[] args)
		{
			MainClass m = new MainClass();
			ArtGalery ag1 = new ArtGalery();
			ag1.Initialize();
			Console.WriteLine("\nInitialization of Art Gallery :\n");

			m.displayArtPiece(ag1.artCollection);
			Array.Sort(ag1.artCollection);
			Console.WriteLine("\nAfter Sorting by Price, ascending :\n");
			m.displayArtPiece(ag1.artCollection);

			RealSelector rs = new RealSelector();
			ArtSelect paintingSelector = new ArtSelect(rs.selectPainting);


			List<ArtPiece> selectedPaiting = ag1.Selector(paintingSelector);
			Console.WriteLine("\nPAINTING with price higher than 100 :\n");
			m.displaySelected(selectedPaiting);

			ArtSelect sculptureSelector = new ArtSelect(rs.selectSculpture);
			List<ArtPiece> selectedSculpture = ag1.Selector(sculptureSelector);
			Console.WriteLine("\nSCULPTURE with price lower than 100 :\n");
			m.displaySelected(selectedSculpture);

			DrawingSelector drawingSelector = new DrawingSelector();
			List<ArtPiece> selectedDrawing = ag1.Selector(drawingSelector);
			Console.WriteLine("\nDRAWING with price higher than 120 :\n");
			m.displaySelected(selectedDrawing);
	

		}
	}

	// Real Selector for delegate 
	class RealSelector
	{
		public bool selectSculpture(ArtPiece p)
		{
			if (p.price < 100 && p.category == Category.SCULPTURE)
			{
				return true;
			}
			return false;
		}

		public bool selectPainting(ArtPiece p)
		{
			if (p.price > 100 && p.category == Category.PAINTING)
			{
				return true;
			}
			return false;
		}


	}

	public class DrawingSelector : IArtSelect
	{
		public bool SelectDrawing(ArtPiece Piece)
		{

			if (Piece.category == Category.DRAWING && Piece.price > 120)
			{
				return true;
			}
			return false;
		}

	}
}
