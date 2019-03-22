using UnityEditor;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor
{
    internal static class AssemblyAttributeMenuAssemblyBrowsable
    {
        private const string k_menuPath = "CONTEXT/AssemblyDefinitionImporter/AssemblyBrowsableAttribute";
        private const string k_attribute = "[assembly:UGF.Assemblies.Runtime.AssemblyBrowsableAttribute]";

        [MenuItem(k_menuPath, false, 1000)]
        private static void Menu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(importer.assetPath);
            bool isActive = AssemblyEditorUtility.IsAttributeActive(assemblyDefinition, k_attribute);

            AssemblyEditorUtility.SetAttributeActive(assemblyDefinition, k_attribute, !isActive);
        }

        [MenuItem(k_menuPath, true, 1000)]
        private static bool Validate(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;
            var assemblyDefinition = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(importer.assetPath);
            bool isActive = AssemblyEditorUtility.IsAttributeActive(assemblyDefinition, k_attribute);

            UnityEditor.Menu.SetChecked(k_menuPath, isActive);

            return true;
        }
    }
}