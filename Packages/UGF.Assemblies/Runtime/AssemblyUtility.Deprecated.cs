using System;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    public static partial class AssemblyUtility
    {
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
    }
}
