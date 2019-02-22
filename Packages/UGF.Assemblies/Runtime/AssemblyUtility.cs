using System;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    public static class AssemblyUtility
    {
        public static void GetBrowsableTypes<T>(List<Type> result, bool inherit = true) where T : Attribute
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            
            GetBrowsableTypes(result, typeof(T), inherit);
        }
        
        public static void GetBrowsableTypes(List<Type> result, Type attributeType, bool inherit = true)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    GetBrowsableTypes(result, assembly, attributeType, inherit);   
                }
            }
        }
        
        public static void GetBrowsableTypes(List<Type> result, Assembly assembly, Type attributeType, bool inherit = true)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (assembly == null) throw new ArgumentNullException(nameof(result));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            
            var types = assembly.GetTypes();

            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];

                if (type.IsDefined(attributeType, inherit))
                {
                    result.Add(type);
                }
            }
        }

        public static void GetBrowsableAssemblies(List<Assembly> result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    result.Add(assembly);
                }
            }
        }
    }
}