using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Assemblies.Runtime
{
    /// <summary>
    /// Represents enumerable through the all browsable assemblies from the specified collection.
    /// </summary>
    public struct AssemblyBrowsableAssembliesEnumerable : IEnumerable<Assembly>
    {
        private readonly IReadOnlyList<Assembly> m_assemblies;

        public struct Enumerator : IEnumerator<Assembly>
        {
            public Assembly Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly IReadOnlyList<Assembly> m_assemblies;
            private int m_index;
            private Assembly m_current;

            public Enumerator(IReadOnlyList<Assembly> assemblies)
            {
                m_assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
                m_index = 0;
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while (m_index < m_assemblies.Count)
                {
                    Assembly assembly = m_assemblies[m_index++];

                    if (assembly.IsDefined(typeof(AssemblyBrowsableAttribute)))
                    {
                        m_current = assembly;
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

        /// <summary>
        /// Creates enumerable from the specified assemblies collection.
        /// </summary>
        /// <param name="assemblies">The collection of the assemblies to enumerate.</param>
        public AssemblyBrowsableAssembliesEnumerable(IReadOnlyList<Assembly> assemblies)
        {
            m_assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_assemblies);
        }

        IEnumerator<Assembly> IEnumerable<Assembly>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
