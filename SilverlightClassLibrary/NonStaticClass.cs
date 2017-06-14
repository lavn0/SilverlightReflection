﻿namespace SilverlightClassLibrary
{
	public class NonStaticClass
	{
		private int Property { get; set; }

		private int GetMethod()
		{
			return 1;
		}

		private string GetMethodWithParam(int i)
		{
			return i.ToString("D2");
		}

		private string GetMethodWithParams(string item1, bool item2)
		{
			return item1 + item2;
		}
	}
}