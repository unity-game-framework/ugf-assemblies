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
        public static AssemblyBrowsableTypesAllEnumerable GetBrowsableTypes(Type attributeType, bool inherit = true)
        {
            return new AssemblyBrowsableTypesAllEnumerable(AppDomain.CurrentDomain.GetAssemblies(), attributeType, inherit);
        }

        public static AssemblyBrowsableTypesEnumerable GetBrowsableTypes(Type attributeType, Assembly assembly, bool inherit = true)
        {
            return new AssemblyBrowsableTypesEnumerable(assembly.GetTypes(), attributeType, inherit);
        }

        public static AssemblyBrowsableAssembliesEnumerable GetBrowsableAssemblies()
        {
            return new AssemblyBrowsableAssembliesEnumerable(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Gets the browsable types marked with the specified attribute type.
        /// <para>
        /// If an assembly not specified, will search through the all assemblies that marked as browsable.
        /// </para>
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        /// <param name="assembly">The assembly to browse.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetBrowsableTypes<T>(ICollection<Type> results, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            GetBrowsableTypes(results, typeof(T), assembly, inherit);
        }

        /// <summary>
        /// Gets the browsable types marked with the specified attribute type.
        /// <para>
        /// If an assembly not specified, will search through the all assemblies that marked as browsable.
        /// </para>
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="assembly">The assembly to browse.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetBrowsableTypes(ICollection<Type> results, Type attributeType, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

            if (assembly == null)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                for (int i = 0; i < assemblies.Length; i++)
                {
                    Assembly targetAssembly = assemblies[i];

                    if (targetAssembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                    {
                        InternalGetBrowsableTypes(results, targetAssembly, attributeType, inherit);
                    }
                }
            }
            else
            {
                InternalGetBrowsableTypes(results, assembly, attributeType, inherit);
            }
        }

        public static void GetBrowsableTypes<T>(ICollection<Type> results, AssemblyBrowsableTypeValidateHandler validate, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (validate == null) throw new ArgumentNullException(nameof(validate));

            GetBrowsableTypes(results, validate, typeof(T), assembly, inherit);
        }

        public static void GetBrowsableTypes(ICollection<Type> results, AssemblyBrowsableTypeValidateHandler validate, Type attributeType, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (validate == null) throw new ArgumentNullException(nameof(validate));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

            if (assembly == null)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                for (int i = 0; i < assemblies.Length; i++)
                {
                    Assembly targetAssembly = assemblies[i];

                    if (targetAssembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                    {
                        InternalGetBrowsableTypes(results, targetAssembly, attributeType, validate, inherit);
                    }
                }
            }
            else
            {
                InternalGetBrowsableTypes(results, assembly, attributeType, validate, inherit);
            }
        }

        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from all assemblies.
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        [Obsolete("GetBrowsableTypes has been deprecated. It will be removed in the next major release.")]
        public static void GetBrowsableTypes<T>(List<Type> results, bool inherit = true) where T : Attribute
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            GetBrowsableTypes(results, typeof(T), inherit);
        }

        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from all assemblies.
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        [Obsolete("GetBrowsableTypes has been deprecated. It will be removed in the next major release.")]
        public static void GetBrowsableTypes(List<Type> results, Type attributeType, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    InternalGetBrowsableTypes(results, assembly, attributeType, inherit);
                }
            }
        }

        /// <summary>
        /// Gets the browsable types marked with the specified attribute type from the specified assembly.
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        /// <param name="assembly">The assembly to browse.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        [Obsolete("GetBrowsableTypes has been deprecated. It will be removed in the next major release.")]
        public static void GetBrowsableTypes(List<Type> results, Assembly assembly, Type attributeType, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

            InternalGetBrowsableTypes(results, assembly, attributeType, inherit);
        }

        /// <summary>
        /// Gets the assemblies marked with the <see cref="AssemblyBrowsableAttribute"/>.
        /// </summary>
        /// <param name="results">The result collection to fill.</param>
        public static void GetBrowsableAssemblies(ICollection<Assembly> results)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                {
                    results.Add(assembly);
                }
            }
        }

        /// <summary>
        /// Tries to find assembly be the specified name.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="assembly">The found assembly.</param>
        public static bool TryGetBrowsableAssembly(string assemblyName, out Assembly assembly)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];

                if (assembly.GetName().Name == assemblyName)
                {
                    return true;
                }
            }

            assembly = null;
            return false;
        }

        private static void InternalGetBrowsableTypes(ICollection<Type> results, Assembly assembly, Type attributeType, bool inherit)
        {
            Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                if (type.IsDefined(attributeType, inherit))
                {
                    results.Add(type);
                }
            }
        }

        private static void InternalGetBrowsableTypes(ICollection<Type> results, Assembly assembly, Type attributeType, AssemblyBrowsableTypeValidateHandler validate, bool inherit)
        {
            Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                if (type.IsDefined(attributeType, inherit) && validate(type))
                {
                    results.Add(type);
                }
            }
        }
    }
}
