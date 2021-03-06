﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class PublicerTest
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

		[TestMethod]
		public void InvokeMethodTest123()
		{
			var obj = new NonStaticClass();
			var result1 = Publicer.InvokeMethod<bool>(typeof(NonStaticClass), "Method", new Type[0], obj);
			Assert.AreEqual(true, result1);
			var result2 = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method", new[] { typeof(int) }, obj, 5);
			Assert.AreEqual("05", result2);
			var result3 = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "Method", new[] { typeof(string), typeof(bool) }, obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result3);
		}

		[TestMethod]
		public void GetStaticFieldTest()
		{
			var result = Publicer.GetField<string>(typeof(NonStaticClass), "StaticField", null);
			Assert.AreEqual("static field", result);
		}

		[TestMethod]
		public void SetStaticFieldTest()
		{
			Publicer.SetField(typeof(NonStaticClass), "StaticField", null, "field set value");
			var result = Publicer.GetField<string>(typeof(NonStaticClass), "StaticField", null);
			Assert.AreEqual("field set value", result);
		}

		[TestMethod]
		public void GetStaticPropertyTest()
		{
			var result = Publicer.GetProperty<string>(typeof(NonStaticClass), "StaticProperty", null);
			Assert.AreEqual("static property", result);
		}

		[TestMethod]
		public void SetStaticPropertyTest()
		{
			Publicer.SetProperty(typeof(NonStaticClass), "StaticProperty", null, "property set value");
			var result = Publicer.GetProperty<string>(typeof(NonStaticClass), "StaticProperty", null);
			Assert.AreEqual("property set value", result);
		}

		[TestMethod]
		public void InvokeStaticMethodTest1()
		{
			var result = Publicer.InvokeMethod<bool>(typeof(NonStaticClass), "StaticMethod", new Type[0], null);
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void InvokeStaticMethodTest2()
		{
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "StaticMethod", new[] { typeof(int) }, null, 5);
			Assert.AreEqual("static 05", result);
		}

		[TestMethod]
		public void InvokeStaticMethodTest3()
		{
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "StaticMethod", new[] { typeof(string), typeof(bool) }, null, "hoge", true);
			Assert.AreEqual("static hogeTrue", result);
		}

		[TestMethod]
		public void InvokeStaticMethodTest123()
		{
			var result1 = Publicer.InvokeMethod<bool>(typeof(NonStaticClass), "StaticMethod", new Type[0], null);
			Assert.AreEqual(false, result1);
			var result2 = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "StaticMethod", new[] { typeof(int) }, null, 5);
			Assert.AreEqual("static 05", result2);
			var result3 = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "StaticMethod", new[] { typeof(string), typeof(bool) }, null, "hoge", true);
			Assert.AreEqual("static hogeTrue", result3);
		}

		[TestMethod]
		[Description("イベントハンドラの実装の基本テスト")]
		public void HandlerTest1()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			obj.SetEvent((sender, args) => { str += "2nd"; });
			obj.CallEvent();
			Assert.AreEqual("1st2nd", str);
		}

		[TestMethod]
		[Description("イベントハンドラを取得するテスト")]
		public void HandlerTest2()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			obj.SetEvent((sender, args) => { str += "2nd"; });
			var handler = Publicer.GetField<EventHandler<EventArgs>>(typeof(NonStaticClass), "handler", obj);
			handler.Invoke(null, EventArgs.Empty);
			Assert.AreEqual("1st2nd", str);
		}

		[TestMethod]
		[Description("イベントハンドラの最後に追加するテスト")]
		public void HandlerTest3()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			var handler = Publicer.GetField<EventHandler<EventArgs>>(typeof(NonStaticClass), "handler", obj);
			handler += ((sender, args) => { str += "2nd"; });
			handler.Invoke(null, EventArgs.Empty);
			Assert.AreEqual("1st2nd", str);
		}

		[TestMethod]
		[Description("イベントハンドラにnullを設定するテスト")]
		public void HandlerTest4()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			obj.SetEvent((sender, args) => { str += "2nd"; });
			Publicer.SetField(typeof(NonStaticClass), "handler", obj, null);
			obj.CallEvent();
			Assert.AreEqual("1st", str);
		}

		[TestMethod]
		[Description("イベントハンドラの末尾に追加するテスト")]
		public void HandlerTest5()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			Publicer.SetField(typeof(NonStaticClass), "handler", obj, (EventHandler<EventArgs>)((sender, args) => { str += "2nd"; }));
			obj.CallEvent();
			Assert.AreEqual("1st2nd", str);
		}

		[TestMethod]
		[Description("イベントハンドラの先頭に挿入するテスト")]
		public void HandlerTest6()
		{
			var obj = new NonStaticClass();
			string str = "1st";
			obj.SetEvent((sender, args) => { str += "3rd"; });
			var presentHandler = Publicer.GetField<EventHandler<EventArgs>>(typeof(NonStaticClass), "handler", obj);
			var handler = (EventHandler<EventArgs>)((sender, args) => { str += "2nd"; });
			handler += presentHandler;
			Publicer.SetField(typeof(NonStaticClass), "handler", obj, handler);
			obj.CallEvent();
			Assert.AreEqual("1st2nd3rd", str);
		}
	}
}
