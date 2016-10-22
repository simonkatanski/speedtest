using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Test.Adapters
{
    public static class ILMapper
    {
        private static Delegate _cachedILMapDelegate;
        private static Delegate _cachedExpressionMapDelegate;        

        public static TOut ILMap<TIn, TOut>(TIn toMap)
        {
            return ((Func<TIn, TOut>)_cachedILMapDelegate)(toMap);
        }

        public static TOut ExpressionMap<TIn, TOut>(TIn toMap)
        {
            return ((Func<TIn, TOut>)_cachedExpressionMapDelegate)(toMap);
        }

        public static void PrepareILMapping<TIn, TOut>()
        {
            var inputType = typeof(TIn);
            var outputType = typeof(TOut);

            var dynamicMethod = new DynamicMethod("ExecuteMapping", typeof(TOut), new Type[] { inputType }, Assembly.GetExecutingAssembly().ManifestModule, true);

            ConstructorInfo outputConstructor = outputType.GetConstructor(Type.EmptyTypes);
            ILGenerator generator = dynamicMethod.GetILGenerator();
            LocalBuilder localBuilder = generator.DeclareLocal(outputType);

            generator.Emit(OpCodes.Newobj, outputConstructor);
            generator.Emit(OpCodes.Stloc_0);

            foreach (var outputProperty in outputType.GetProperties())
            {
                var inputProperty = inputType.GetProperty(outputProperty.Name);
                if (outputProperty != null && outputProperty.GetSetMethod() != null)
                {
                    generator.Emit(OpCodes.Ldloc_0);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Callvirt, inputProperty.GetGetMethod());
                    generator.Emit(OpCodes.Callvirt, outputProperty.GetSetMethod());
                }
            }
            
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            _cachedILMapDelegate = dynamicMethod.CreateDelegate(typeof(Func<TIn, TOut>));             
        }
          
        public static void PrepareExpressionMapping<TIn, TOut>()
        {
            ParameterExpression sourceParameter = Expression.Parameter(typeof(TIn), "toMap");

            var bindings = new List<MemberBinding>();

            foreach (PropertyInfo sourceProperty in typeof(TIn).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {                
                PropertyInfo targetProperty = typeof(TOut).GetProperty(sourceProperty.Name);
                bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));                
            }

            var initializer = Expression.MemberInit(Expression.New(typeof(TOut)), bindings);
            _cachedExpressionMapDelegate = Expression.Lambda<Func<TIn, TOut>>(initializer, sourceParameter).Compile();
        }
    }
}