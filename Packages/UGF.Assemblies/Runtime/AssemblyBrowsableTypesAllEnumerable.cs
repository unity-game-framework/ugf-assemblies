using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
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
            private AssemblyBrowsableAssembliesEnumerable.Enumerator m_assembliesEnumerator;
            private AssemblyBrowsableTypesEnumerable.Enumerator m_typesEnumerator;
            private bool m_typesInit;
            private Type m_current;

            public Enumerator(Assembly assembly, Type attributeType, bool inherit)
            {
                m_assembly = assembly;
                m_assemblies = null;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_assembliesEnumerator = default;
                m_typesEnumerator = new AssemblyBrowsableTypesEnumerable.Enumerator(m_assembly.GetTypes(), m_attributeType, m_inherit);
                m_typesInit = true;
                m_current = null;
            }

            public Enumerator(IReadOnlyList<Assembly> assemblies, Type attributeType, bool inherit)
            {
                m_assembly = null;
                m_assemblies = assemblies;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_assembliesEnumerator = new AssemblyBrowsableAssembliesEnumerable.Enumerator(m_assemblies);
                m_typesEnumerator = default;
                m_typesInit = false;
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return m_assemblies == null ? MoveAssembly() : MoveAssemblies();
            }

            public void Reset()
            {
                if (m_assemblies != null)
                {
                    m_assembliesEnumerator = new AssemblyBrowsableAssembliesEnumerable.Enumerator(m_assemblies);
                    m_typesEnumerator = default;
                    m_typesInit = false;
                }
                else
                {
                    m_assembliesEnumerator = default;
                    m_typesEnumerator = new AssemblyBrowsableTypesEnumerable.Enumerator(m_assembly.GetTypes(), m_attributeType, m_inherit);
                    m_typesInit = true;
                }

                m_current = null;
            }

            private bool MoveAssembly()
            {
                if (m_typesInit && m_typesEnumerator.MoveNext())
                {
                    m_current = m_typesEnumerator.Current;
                    return true;
                }

                m_typesEnumerator = default;
                m_typesInit = false;
                return false;
            }

            private bool MoveAssemblies()
            {
                bool state = MoveAssembly();

                if (!state)
                {
                    while (!m_typesInit && m_assembliesEnumerator.MoveNext())
                    {
                        Assembly assembly = m_assembliesEnumerator.Current;

                        if (assembly != null)
                        {
                            m_typesEnumerator = new AssemblyBrowsableTypesEnumerable.Enumerator(assembly.GetTypes(), m_attributeType, m_inherit);
                            m_typesInit = true;

                            if (MoveAssembly())
                            {
                                return true;
                            }
                        }
                    }
                }

                return state;
            }
        }

        public AssemblyBrowsableTypesAllEnumerable(Assembly assembly, Type attributeType, bool inherit)
        {
            m_assembly = assembly;
            m_assemblies = null;
            m_attributeType = attributeType;
            m_inherit = inherit;
        }

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
