using System.Linq;
using NUnit.Framework;

namespace UGF.Assemblies.Runtime.Tests
{
    public class TestAssemblyUtility
    {
        [Test]
        public void GetBrowsableTypesGeneric()
        {
            int count = AssemblyUtility.GetBrowsableTypes<AssemblyBrowsableTypeAttribute>().Count();
            
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetBrowsableTypes()
        {
            int count = AssemblyUtility.GetBrowsableTypes(typeof(AssemblyBrowsableTypeAttribute)).Count();
            
            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetBrowsableAssemblies()
        {
            int count = AssemblyUtility.GetBrowsableAssemblies().Count();
            
            Assert.AreEqual(1, count);
        }
    }
}