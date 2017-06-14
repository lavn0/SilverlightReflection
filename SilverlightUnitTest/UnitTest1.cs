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
			var result = Publicer.GetField<string>(obj.GetType(), "field", obj);
			Assert.AreEqual("field", result);
		}

		[TestMethod]
		public void GetPropertyTestStr()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetProperty<string>(obj.GetType(), "Property", obj);
			Assert.AreEqual("property", result);
		}

		[TestMethod]
		public void InvokeMethodTest1()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<bool>(typeof(NonStaticClass), "Method1", obj);
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void InvokeMethodTest2()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method2", obj, 5);
			Assert.AreEqual("05", result);
		}

		[TestMethod]
		public void InvokeMethodTest3()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method3", obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
