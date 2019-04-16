using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Assemblies.Runtime
{
    public struct AssemblyBrowsableTypesEnumerable : IEnumerable<Type>
    {
        private readonly Type[] m_types;
        private readonly Type m_attributeType;
        private readonly bool m_inherit;

        public struct Enumerator : IEnumerator<Type>
        {
            public Type Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly Type[] m_types;
            private readonly Type m_attributeType;
            private readonly bool m_inherit;
            private int m_index;
            private Type m_current;

            public Enumerator(Type[] types, Type attributeType, bool inherit)
            {
                m_types = types;
                m_attributeType = attributeType;
                m_inherit = inherit;
                m_index = 0;
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while (m_index < m_types.Length)
                {
                    Type type = m_types[m_index++];

                    if (type.IsDefined(m_attributeType, m_inherit))
                    {
                        m_current = type;
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                m_index = 0;
                m_current = null;
            }
        }

        public AssemblyBrowsableTypesEnumerable(Type[] types, Type attributeType, bool inherit)
        {
            m_types = types;
            m_attributeType = attributeType;
            m_inherit = inherit;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_types, m_attributeType, m_inherit);
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
