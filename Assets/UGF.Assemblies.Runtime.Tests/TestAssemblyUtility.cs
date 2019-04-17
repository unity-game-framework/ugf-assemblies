using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace UGF.Assemblies.Runtime.Tests
{
    public class TestAssemblyUtility
    {
        [AssemblyBrowsableType]
        public class TestTarget
        {
        }

        [Test]
        public void GetBrowsableTypesEnumerableAll()
        {
            string[] types = AssemblyUtility.GetBrowsableTypes(typeof(AssemblyBrowsableTypeAttribute)).Select(x => x.Name).ToArray();

            Assert.AreEqual(3, types.Length);
            Assert.Contains("TestBrowsableType", types);
            Assert.Contains("TestBrowsableType2", types);
            Assert.Contains("TestTarget", types);
        }

        [Test]
        public void GetBrowsableTypesEnumerableAssembly()
        {
            AssemblyUtility.TryGetBrowsableAssembly("TestAssembly", out Assembly assembly);

            string[] types = AssemblyUtility.GetBrowsableTypes(typeof(AssemblyBrowsableTypeAttribute), assembly).Select(x => x.Name).ToArray();

            Assert.AreEqual(2, types.Length);
            Assert.Contains("TestBrowsableType", types);
            Assert.Contains("TestBrowsableType2", types);
        }

        [Test]
        public void GetBrowsableAssembliesEnumerable()
        {
            string[] assemblies = AssemblyUtility.GetBrowsableAssemblies().Select(x => x.GetName().Name).ToArray();

            Assert.Contains("TestAssembly", assemblies);
        }

        [Test]
        public void GetBrowsableTypesGeneric()
        {
            ICollection<Type> types = new List<Type>();

            AssemblyUtility.GetBrowsableTypes<AssemblyBrowsableTypeAttribute>(types);

            Assert.AreEqual(3, types.Count);
            Assert.Contains(typeof(TestTarget), (ICollection)types);
        }

        [Test]
        public void GetBrowsableTypes()
        {
            ICollection<Type> types = new List<Type>();

            AssemblyUtility.GetBrowsableTypes(types, typeof(AssemblyBrowsableTypeAttribute));

            Assert.AreEqual(3, types.Count);
            Assert.Contains(typeof(TestTarget), (ICollection)types);
        }

        [Test]
        public void GetBrowsableTypesFromAssembly()
        {
            ICollection<Type> types = new List<Type>();

            AssemblyUtility.TryGetBrowsableAssembly("TestAssembly", out Assembly assembly);
            AssemblyUtility.GetBrowsableTypes(types, typeof(AssemblyBrowsableTypeAttribute), assembly);

            Assert.AreEqual(2, types.Count);
            Assert.Contains("TestBrowsableType", types.Select(x => x.Name).ToArray());
            Assert.Contains("TestBrowsableType2", types.Select(x => x.Name).ToArray());
        }

        [Test]
        public void GetBrowsableAssemblies()
        {
            var assemblies = new List<Assembly>();

            AssemblyUtility.GetBrowsableAssemblies(assemblies);

            Assert.GreaterOrEqual(assemblies.Count, 1);
            Assert.Contains("TestAssembly", assemblies.Select(x => x.GetName().Name).ToArray());
        }

        [Test]
        public void TryGetBrowsableAssembly()
        {
            bool result = AssemblyUtility.TryGetBrowsableAssembly("TestAssembly", out Assembly assembly);

            Assert.True(result);
            Assert.NotNull(assembly);
            Assert.AreEqual("TestAssembly", assembly.GetName().Name);
        }
    }
}
