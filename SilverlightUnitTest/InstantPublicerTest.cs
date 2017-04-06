using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest0
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		[ExpectedException(typeof(MethodAccessException))]
		public void MethodAccessErrorTest()
		{
			var obj = new Class1();
			var methodInfo = typeof(Class1).GetMethod("GetMethod", (BindingFlags)int.MaxValue);
			var result = methodInfo.Invoke(obj, null);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void InstantPublicerTest()
		{
			var obj = new Class1();
			var result = InstantPublicer.CallMethod<int>(obj, obj.GetType(), "GetMethod");
			Assert.AreEqual(1, result);
		}
	}
}
