﻿using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UGF.Assemblies.Runtime.Tests.TestAssembly;

namespace UGF.Assemblies.Runtime.Tests
{
    public class TestAssemblyUtility
    {
        [Test]
        public void GetBrowsableTypesGeneric()
        {
            var types = new List<Type>();
            
            AssemblyUtility.GetBrowsableTypes<AssemblyBrowsableTypeAttribute>(types);
            
            Assert.AreEqual(1, types.Count);
            Assert.IsTrue(types[0].IsAssignableFrom(typeof(TestBrowsableType)));
        }

        [Test]
        public void GetBrowsableTypes()
        {
            var types = new List<Type>();
            
            AssemblyUtility.GetBrowsableTypes(types, typeof(AssemblyBrowsableTypeAttribute));
            
            Assert.AreEqual(1, types.Count);
            Assert.IsTrue(types[0].IsAssignableFrom(typeof(TestBrowsableType)));
        }

        [Test]
        public void GetBrowsableAssemblies()
        {
            var assemblies = new List<Assembly>();
            
            AssemblyUtility.GetBrowsableAssemblies(assemblies);
            
            Assert.AreEqual(1, assemblies.Count);
            Assert.AreEqual("TestAssembly", assemblies[0].GetName().Name);
        }
    }
}