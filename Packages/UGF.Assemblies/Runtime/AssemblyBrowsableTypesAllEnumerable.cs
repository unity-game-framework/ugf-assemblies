using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    /// <summary>
    /// Represents enumerable through the all types that contains the specified attribute.
    /// </summary>
    public struct AssemblyBrowsableTypesAllEnumerable : IEnumerable<Type>
    {
        private readonly Assembly m_assembly;
        private readonly IReadOnlyList<Assembly> m_assemblies;
        private readonly Type m_attributeType;
        private readonly bool m_inherit;

        public struct Enumerator : IEnumerator<Type>
        {
            public Type Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly Assembly m_assembly;
            private readonly IReadOnlyList<Assembly> m_assemblies;
            private readonly Type m_attributeType;
            private readonly bool m_inherit;
            private int m_assemblyIndex;
            private Type[] m_types;
            private int m_typeIndex;
            private Type m_current;

            public Enumerator(Assembly assembly, Type attributeType, bool inherit)
            {
                m_assembly = assembly;
                m_assemblies = null;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            public Enumerator(IReadOnlyList<Assembly> assemblies, Type attributeType, bool inherit)
            {
                m_assembly = null;
                m_assemblies = assemblies;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (!Next())
                {
                    NextAssembly();
                }

                while (Next())
                {
                    Type type = m_types[m_typeIndex++];

                    if (type != null && CheckType(type))
                    {
                        m_current = type;
                        return true;
                    }

                    if (!Next())
                    {
                        NextAssembly();
                    }
                }

                return false;
            }

            public void Reset()
            {
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            private bool Next()
            {
                return m_types != null && m_typeIndex < m_types.Length;
            }

            private void NextAssembly()
            {
                m_types = null;
                m_typeIndex = 0;

                if (m_assemblies != null)
                {
                    while (m_assemblyIndex < m_assemblies.Count && m_types == null)
                    {
                        Assembly assembly = m_assemblies[m_assemblyIndex++];

                        if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                        {
                            Type[] types = GetTypes(assembly);

                            if (types.Length > 0)
                            {
                                m_types = types;
                            }
                        }
                    }
                }

                if (m_assembly != null)
                {
                    if (m_assemblyIndex < 1)
                    {
                        Type[] types = GetTypes(m_assembly);

                        if (types.Length > 0)
                        {
                            m_types = types;
                            m_assemblyIndex++;
                        }
                    }
                }
            }

            private bool CheckType(Type type)
            {
                try
                {
                    return type.IsDefined(m_attributeType, m_inherit);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            private Type[] GetTypes(Assembly assembly)
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    return exception.Types;
                }
            }
        }

        /// <summary>
        /// Creates enumerable from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to browse.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public AssemblyBrowsableTypesAllEnumerable(Assembly assembly, Type attributeType, bool inherit)
        {
            m_assembly = assembly;
            m_assemblies = null;
            m_attributeType = attributeType;
            m_inherit = inherit;
        }

        /// <summary>
        /// Creates enumerable from the specified assembly collection.
        /// </summary>
        /// <param name="assemblies">The assemblies to browse.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public AssemblyBrowsableTypesAllEnumerable(Assembly[] assemblies, Type attributeType, bool inherit)
        {
            m_assembly = null;
            m_assemblies = assemblies;
            m_attributeType = attributeType;
            m_inherit = inherit;
        }

        public Enumerator GetEnumerator()
        {
            return m_assemblies != null
                ? new Enumerator(m_assemblies, m_attributeType, m_inherit)
                : new Enumerator(m_assembly, m_attributeType, m_inherit);
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
