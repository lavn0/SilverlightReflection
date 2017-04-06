using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SilverlightUnitTest
{
	/// <summary>Silverlightのセキュリティを超えてprivate/internalメンバを呼び出すためのクラス</summary>
	public static class Publicer
	{
		public static readonly BindingFlags AllFlags = (BindingFlags)int.MaxValue;

		public static T GetMember<T>(Type type, string memberName, object instance)
		{
			return PublicerCore.GetMemberCore<T>(type.GetMember(memberName, AllFlags).First(), instance);
		}

		public static T GetMember<T>(MemberInfo memberInfo, object instance)
		{
			return PublicerCore.GetMemberCore<T>(memberInfo, instance);
		}

		public static T CallMethod<T>(Type type, string memberName, object instance, params object[] parameters)
		{
			return PublicerCore.GetMemberCore<T>(type.GetMember(memberName, AllFlags).First(), instance, parameters);
		}

		public static T CallMethod<T>(MethodInfo methodInfo, object instance, params object[] parameters)
		{
			return PublicerCore.GetMemberCore<T>(methodInfo, instance, parameters);
		}

		public static T GetProperty<T>(Type type, string memberName, object instance)
		{
			return PublicerCore.GetMemberCore<T>(type.GetProperty(memberName, AllFlags), instance);
		}

		public static T GetProperty<T>(PropertyInfo propertyInfo, object instance)
		{
			return PublicerCore.GetMemberCore<T>(propertyInfo, instance);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void Clear()
		{
			PublicerCore.Clear();
		}

		private class PublicerCore
		{
			private readonly static Dictionary<string, Delegate> funcs = new Dictionary<string, Delegate>();

			[EditorBrowsable(EditorBrowsableState.Never)]
			public static void Clear()
			{
				funcs.Clear();
			}

			public static T GetMemberCore<T>(MemberInfo memberInfo, object instance, params object[] parameters)
			{
				string fullName = $"{memberInfo.DeclaringType.FullName}.{memberInfo.Name}";
				Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(memberInfo));
				return (T)func.DynamicInvoke(new object[] { instance }.Concat(parameters).ToArray());
			}

			private static Delegate GetDelegate(MemberInfo memberInfo)
			{
				var paramExp = Expression.Parameter(typeof(object), "item");
				var exp = Expression.Convert(paramExp, memberInfo.DeclaringType);
				Expression accessor = null;
				IEnumerable<ParameterExpression> parameters = Enumerable.Empty<ParameterExpression>();
				switch (memberInfo.MemberType)
				{
					case MemberTypes.Method:
						var methodInfo = (MethodInfo)memberInfo;
						var pp = methodInfo.GetParameters();
						parameters = Enumerable.Range(1, pp.Length)
							.Select(i => Expression.Parameter(typeof(object), $"item{i}")).ToList();
						var methodParameter = pp.Zip(
							parameters,
							(mp, dp) => Expression.Convert(dp, mp.ParameterType)).ToList();
						accessor = Expression.Call(exp, methodInfo, methodParameter);
						break;

					case MemberTypes.Property:
					case MemberTypes.Field:
						accessor = Expression.PropertyOrField(exp, memberInfo.Name);
						break;
				}

				return Expression.Lambda(accessor, new[] { paramExp }.Concat(parameters)).Compile();
			}
		}
	}
}
