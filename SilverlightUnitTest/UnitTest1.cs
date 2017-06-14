using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		public void GetFieldTest()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetField<int>(obj.GetType(), "GetMethod", obj);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetPropertyTestStr()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetProperty<int>(obj.GetType(), "PropertyGet", obj);
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public void InvokeMethodTest()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "GetMethodWithParams", obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
