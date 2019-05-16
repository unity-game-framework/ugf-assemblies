using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Assemblies.Editor.Tests
{
    public class TestAssemblyEditorUtility
    {
        private readonly string m_path = "Assets/UGF.Assemblies.Editor.Tests/UGF.Assemblies.Editor.Tests.asmdef";
        private readonly string m_attribute = "[assembly:UGF.Assemblies.Runtime.AssemblyBrowsableAttribute]";
        private readonly string m_testEditorAssembly = "Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/TestEditorAssembly.asmdef";
        private readonly string m_testEditorAssemblyAsset = "Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/TestEditorAssemblyMaterial.mat";

        [Test]
        public void SetAttributeActive()
        {
            Assert.Ignore();
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

        [Test]
        public void SaveAttributes()
        {
            Assert.Ignore();
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

        [Test]
        public void TryFindAssemblyDefinitionFilePathFromAssetPath()
        {
            bool result = AssemblyEditorUtility.TryFindAssemblyDefinitionFilePathFromAssetPath(m_testEditorAssemblyAsset, out string assemblyDefinitionPath);

            Assert.True(result);
            Assert.AreEqual(m_testEditorAssembly, assemblyDefinitionPath);
        }

        [Test]
        public void GetAssetPathsUnderAssemblyDefinitionFileByAssetType()
        {
            var assets = new List<string>();

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(assets, m_testEditorAssembly, typeof(Material));

            Assert.AreEqual(2, assets.Count);
            Assert.Contains(m_testEditorAssemblyAsset, assets);
            Assert.Contains("Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/Other/OtherMaterial.mat", assets);
        }

        [Test]
        public void GetAssetPathsUnderAssemblyDefinitionFileByAssetTypeAndTextAsset()
        {
            var assets = new List<string>();

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(assets, m_testEditorAssemblyAsset, typeof(TextAsset));

            Assert.AreEqual(3, assets.Count);
            Assert.Contains(m_testEditorAssembly, assets);
            Assert.Contains("Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/TestEditorAssemblyUtility.cs", assets);
            Assert.Contains("Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/Other/OtherTextAsset.txt", assets);
        }

        [Test]
        public void GetAssetPathsUnderAssemblyDefinitionFile()
        {
            var assets = new List<string>();

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(assets, m_testEditorAssembly, "mat");

            Assert.AreEqual(2, assets.Count);
            Assert.Contains(m_testEditorAssemblyAsset, assets);
            Assert.Contains("Assets/UGF.Assemblies.Editor.Tests/TestEditorAssembly/Other/OtherMaterial.mat", assets);
        }

        [Test]
        public void TryFindCompilationAssemblyByName()
        {
            bool result = AssemblyEditorUtility.TryFindCompilationAssemblyByName("UGF.Assemblies.Editor.Tests", out Assembly assembly);

            Assert.True(result);
            Assert.NotNull(assembly);
            Assert.AreEqual("UGF.Assemblies.Editor.Tests", assembly.name);
        }
    }
}
