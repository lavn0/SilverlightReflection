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
		private static readonly BindingFlags AllFlags = (BindingFlags)int.MaxValue;
		private static readonly Dictionary<string, Delegate> funcs = new Dictionary<string, Delegate>();

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void Clear()
		{
			funcs.Clear();
		}

		public static T GetField<T>(Type type, string name, object instance)
		{
			string fullName = $"{type.FullName}.{name}";
			Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(type.GetField(name, AllFlags)));
			return (T)func.DynamicInvoke(instance, false, null);
		}

		public static void SetField(Type type, string name, object instance, object value)
		{
			string fullName = $"{type.FullName}.{name}";
			Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(type.GetField(name, AllFlags)));
			func.DynamicInvoke(instance, true, value);
		}

		public static T GetProperty<T>(Type type, string name, object instance)
		{
			string fullName = $"g:{type.FullName}.{name}";
			Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(type.GetProperty(name, AllFlags).GetGetMethod(true)));
			return (T)func.DynamicInvoke(instance);
		}

		public static void SetProperty(Type type, string name, object instance, params object[] parameters)
		{
			string fullName = $"s:{type.FullName}.{name}";
			Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(type.GetProperty(name, AllFlags).GetSetMethod(true)));
			func.DynamicInvoke(new[] { instance }.Concat(parameters).ToArray());
		}

		public static T InvokeMethod<T>(Type type, string name, Type[] parameterTypes, object instance, params object[] parameters)
		{
			string fullName = $"m:{type.FullName}.{name}({string.Join(",", parameterTypes.Select(t => t.FullName))}";
			Delegate func = funcs.TryGetValue(fullName, out func) ? func : (funcs[fullName] = GetDelegate(type.GetMethod(name, AllFlags, null, parameterTypes, null)));
			return (T)func.DynamicInvoke(new[] { instance }.Concat(parameters).ToArray());
		}

		private static Delegate GetDelegate(MemberInfo memberInfo)
		{
			var paramExp = Expression.Parameter(typeof(object), "item");
			Expression objExp = null;
			Expression accessor = null;
			IEnumerable<ParameterExpression> parameterExpressions = Enumerable.Empty<ParameterExpression>();
			switch (memberInfo.MemberType)
			{
				case MemberTypes.Field:
					var fieldInfo = (FieldInfo)memberInfo;
					objExp = fieldInfo.IsStatic ? null : Expression.Convert(paramExp, memberInfo.DeclaringType);
					var fieldExp = Expression.PropertyOrField(objExp, memberInfo.Name);
					var paramExp1 = Expression.Parameter(typeof(object), "item1");
					var param1 = Expression.Convert(paramExp1, typeof(bool));
					var paramExp2 = Expression.Parameter(typeof(object), "item2");
					var param2 = Expression.Convert(paramExp2, fieldInfo.FieldType);
					parameterExpressions = new[] { paramExp1, paramExp2 };
					accessor = Expression.Block(Expression.IfThen(param1, Expression.Assign(fieldExp, param2)), fieldExp);
					break;

				case MemberTypes.Property:
					throw new InvalidOperationException();

				case MemberTypes.Method:
					var methodInfo = (MethodInfo)memberInfo;
					objExp = methodInfo.IsStatic ? null : Expression.Convert(paramExp, memberInfo.DeclaringType);
					var methodParameterInfos = methodInfo.GetParameters();
					parameterExpressions = Enumerable.Range(1, methodParameterInfos.Length)
						.Select(i => Expression.Parameter(typeof(object), $"item{i}")).ToList();
					var methodParameter = methodParameterInfos.Zip(
						parameterExpressions,
						(mp, expp) => Expression.Convert(expp, mp.ParameterType)).ToList();
					accessor = Expression.Call(objExp, methodInfo, methodParameter);
					break;
			}

			return Expression.Lambda(accessor, new[] { paramExp }.Concat(parameterExpressions)).Compile();
		}
	}
}
