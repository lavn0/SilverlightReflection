using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SilverlightUnitTest
{
	public static class Publicer
	{
		public static readonly BindingFlags AllFlags = (BindingFlags)int.MaxValue;

		public static T CallMethod<T>(object instance, Type type, string methodName)
		{
			var methodInfo = type.GetMethod(methodName, AllFlags);
			var lambda = Expression.Lambda<Func<T>>(Expression.Call(Expression.Constant(instance), methodInfo));
			return lambda.Compile().Invoke();
		}

		private readonly static Dictionary<string, Delegate> funcs = new Dictionary<string, Delegate>();
		public static T GetMember<T>(Type type, string memberName, object instance)
		{
			string fullName = $"{type.FullName}.{memberName}";
			Delegate func;
			if (!funcs.TryGetValue(fullName, out func))
			{
				var memberInfo = type.GetMember(memberName, AllFlags).First();
				func = funcs[fullName] = GetDelegate(memberInfo);
			}

			return (T)func.DynamicInvoke(instance);
		}

		public static T GetMember<T>(MemberInfo memberInfo, object instance)
		{
			string fullName = $"{memberInfo.DeclaringType.FullName}.{memberInfo.Name}";
			Delegate func;
			if (!funcs.TryGetValue(fullName, out func))
			{
				func = funcs[fullName] = GetDelegate(memberInfo);
			}

			return (T)func.DynamicInvoke(instance);
		}

		private static Delegate GetDelegate(MemberInfo memberInfo)
		{
			var paramExp = Expression.Parameter(typeof(object));
			var exp = Expression.Convert(paramExp, memberInfo.DeclaringType);
			Expression accessor = null;
			switch (memberInfo.MemberType)
			{
				case MemberTypes.Method:
					accessor = Expression.Call(exp, (MethodInfo)memberInfo);
					break;

				case MemberTypes.Property:
				case MemberTypes.Field:
					accessor = Expression.PropertyOrField(exp, memberInfo.Name);
					break;
			}

			var t = Expression.Lambda(accessor, paramExp).Compile();
			return t;
		}
	}
}
