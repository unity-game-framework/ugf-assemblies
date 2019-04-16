using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    public struct AssemblyBrowsableTypesAllEnumerable : IEnumerable<Type>
    {
        private readonly Assembly[] m_assemblies;
        private readonly Type m_attributeType;
        private readonly bool m_inherit;

        public struct Enumerator : IEnumerator<Type>
        {
            public Type Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly Assembly[] m_assemblies;
            private readonly Type m_attributeType;
            private readonly bool m_inherit;
            private AssemblyBrowsableAssembliesEnumerable.Enumerator m_assembliesEnumerator;
            private Type m_current;

            public Enumerator(Assembly[] assemblies, Type attributeType, bool inherit)
            {
                m_assemblies = assemblies;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_assembliesEnumerator = new AssemblyBrowsableAssembliesEnumerable.Enumerator(m_assemblies);
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while (m_assembliesEnumerator.MoveNext())
                {
                    Assembly assembly = m_assembliesEnumerator.Current;

                    if (assembly != null)
                    {
                        var enumerator = new AssemblyBrowsableTypesEnumerable.Enumerator(assembly.GetTypes(), m_attributeType, m_inherit);

                        while (enumerator.MoveNext())
                        {
                            m_current = enumerator.Current;
                            return true;
                        }
                    }
                }

                return false;
            }

            public void Reset()
            {
                m_assembliesEnumerator = new AssemblyBrowsableAssembliesEnumerable.Enumerator(m_assemblies);
                m_current = null;
            }
        }

        public AssemblyBrowsableTypesAllEnumerable(Assembly[] assemblies, Type attributeType, bool inherit)
        {
            m_assemblies = assemblies;
            m_attributeType = attributeType;
            m_inherit = inherit;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_assemblies, m_attributeType, m_inherit);
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
