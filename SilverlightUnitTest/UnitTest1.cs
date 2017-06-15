using System;
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
		public void SetFieldTest()
		{
			var obj = new NonStaticClass();
			Publicer.SetField(obj.GetType(), "field", obj, "field set value");
			var result = Publicer.GetField<string>(obj.GetType(), "field", obj);
			Assert.AreEqual("field set value", result);
		}

		[TestMethod]
		public void GetPropertyTest()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetProperty<string>(obj.GetType(), "Property", obj);
			Assert.AreEqual("property", result);
		}

		[TestMethod]
		public void SetPropertyTest()
		{
			var obj = new NonStaticClass();
			Publicer.SetProperty(obj.GetType(), "Property", obj, "property set value");
			var result = Publicer.GetProperty<string>(obj.GetType(), "Property", obj);
			Assert.AreEqual("property set value", result);
		}

		[TestMethod]
		public void InvokeMethodTest1()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<bool>(typeof(NonStaticClass), "Method", new Type[0], obj);
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void InvokeMethodTest2()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method", new[] { typeof(int) }, obj, 5);
			Assert.AreEqual("05", result);
		}

		[TestMethod]
		public void InvokeMethodTest3()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method", new[] { typeof(string), typeof(bool) }, obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
