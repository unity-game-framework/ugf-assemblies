using UnityEditor;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor
{
    internal static class AssemblyEditorMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/AssemblyBrowsableAttribute", false, 1000)]
        private static void AssemblyBrowsableAttributeMenu(MenuCommand menuCommand)
        {
            string attribute = "[assembly:UGF.Assemblies.Runtime.AssemblyBrowsableAttribute]";

            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(importer.assetPath);
            bool isActive = AssemblyEditorUtility.IsAttributeActive(assemblyDefinition, attribute);

            AssemblyEditorUtility.SetAttributeActive(assemblyDefinition, attribute, !isActive);
        }

        [MenuItem("CONTEXT/AssemblyDefinitionImporter/AssemblyBrowsableAttribute", true, 1000)]
        private static bool AssemblyBrowsableAttributeValidate(MenuCommand menuCommand)
        {
            string attribute = "[assembly:UGF.Assemblies.Runtime.AssemblyBrowsableAttribute]";

            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(importer.assetPath);
            bool isActive = AssemblyEditorUtility.IsAttributeActive(assemblyDefinition, attribute);

            Menu.SetChecked("CONTEXT/AssemblyDefinitionImporter/AssemblyBrowsableAttribute", isActive);

            return true;
        }
    }
}
