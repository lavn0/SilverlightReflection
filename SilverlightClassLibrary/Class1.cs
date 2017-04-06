namespace SilverlightClassLibrary
{
	public class Class1
	{
		private int PropertyGet { get { return 3; } }
		private int propertySet;
		private int PropertySet { set { propertySet = value; } }
		private int PropertyGetSet { get; set; }

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
