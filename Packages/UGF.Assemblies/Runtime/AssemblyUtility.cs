using System;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    public static class AssemblyUtility
    {
        public static IEnumerable<(T attribute, Type type)> GetBrowsableTypes<T>(bool inherit = true) where T : Attribute
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    var types = assembly.GetTypes();
            
                    foreach (var type in types)
                    {
                        var attribute = type.GetCustomAttribute<T>(inherit);
            
                        if (attribute != null)
                        {
                            yield return (attribute, type);
                        }
                    }
                }
            }
        }

        public static IEnumerable<(object attribute, Type type)> GetBrowsableTypes(Type attributeType, bool inherit = true)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        var attribute = type.GetCustomAttribute(attributeType, inherit);

                        if (attribute != null)
                        {
                            yield return (attribute, type);
                        }
                    }
                }
            }
        }

        public static IEnumerable<Assembly> GetBrowsableAssemblies()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    yield return assembly;
                }
            }
        }
    }
}
