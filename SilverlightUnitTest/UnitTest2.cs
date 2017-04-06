using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest2
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		public void GetPropertyTestStr()
		{
			var obj = new Class1();
			var result = Publicer.GetProperty<int>(obj.GetType(), "PropertyGet", obj);
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public void GetPropertyTestInfo()
		{
			var obj = new Class1();
			var result = Publicer.GetProperty<int>(obj.GetType().GetProperty("PropertyGet", (BindingFlags)int.MaxValue), obj);
			Assert.AreEqual(3, result);
		}
	}
}
