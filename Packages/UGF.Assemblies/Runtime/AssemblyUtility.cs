using System;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    /// <summary>
    /// Provides utility for working with assemblies.
    /// </summary>
    public static class AssemblyUtility
    {
        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from all assemblies.
        /// </summary>
        /// <param name="result">The result collection to fill.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetBrowsableTypes<T>(List<Type> result, bool inherit = true) where T : Attribute
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            
            GetBrowsableTypes(result, typeof(T), inherit);
        }
        
        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from all assemblies.
        /// </summary>
        /// <param name="result">The result collection to fill.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetBrowsableTypes(List<Type> result, Type attributeType, bool inherit = true)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    GetBrowsableTypes(result, assembly, attributeType, inherit);   
                }
            }
        }
        
        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from the specified assembly.
        /// </summary>
        /// <param name="result">The result collection to fill.</param>
        /// <param name="assembly">The assembly to browse.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetBrowsableTypes(List<Type> result, Assembly assembly, Type attributeType, bool inherit = true)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            
            Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                if (type.IsDefined(attributeType, inherit))
                {
                    result.Add(type);
                }
            }
        }

        /// <summary>
        /// Gets the assemblies marked with the <see cref="AssemblyBrowsableAttribute"/>.
        /// </summary>
        /// <param name="result">The result collection to fill.</param>
        public static void GetBrowsableAssemblies(List<Assembly> result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    result.Add(assembly);
                }
            }
        }
    }
}