using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor.Tests
{
    public class TestAssemblyEditorUtility
    {
        private readonly string m_path = "Assets/UGF.Assemblies.Editor.Tests/UGF.Assemblies.Editor.Tests.asmdef";
        private readonly string m_attribute = "[assembly:UGF.Assemblies.Runtime.AssemblyBrowsableAttribute]";
        
        [Test, Ignore("")]
        public void SetAttributeActive()
        {
        }

        [Test]
        public void IsAttributeActive()
        {
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(m_path);

            bool result0 = AssemblyEditorUtility.IsAttributeActive(assemblyDefinition, m_attribute);
            
            Assert.True(result0);
        }

        [Test]
        public void LoadAttributes()
        {
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(m_path);
            HashSet<string> attributes = AssemblyEditorUtility.LoadAttributes(assemblyDefinition);
            
            Assert.NotNull(attributes);
            Assert.AreEqual(1, attributes.Count);
            Assert.True(attributes.Contains(m_attribute));
        }

        [Test, Ignore("")]
        public void SaveAttributes()
        {
        }

        [Test]
        public void IsAttributesFileExists()
        {
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(m_path);

            bool result0 = AssemblyEditorUtility.IsAttributesFileExists(assemblyDefinition);
            
            Assert.True(result0);
        }

        [Test]
        public void GetAttributesFilePath()
        {
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(m_path);

            string path = AssemblyEditorUtility.GetAttributesFilePath(assemblyDefinition);
            
            Assert.AreEqual("Assets/UGF.Assemblies.Editor.Tests/UGF.Assemblies.Editor.Tests.Attributes.cs", path);
        }
    }
}