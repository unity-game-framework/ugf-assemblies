using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor
{
    public static class AssemblyEditorUtility
    {
        public static void SetAttributeActive(AssemblyDefinitionAsset assemblyDefinition, string attribute, bool state)
        {
            HashSet<string> attributes = LoadAttributes(assemblyDefinition);

            if (state)
            {
                attributes.Add(attribute);
            }
            else
            {
                attributes.Remove(attribute);
            }

            SaveAttributes(assemblyDefinition, attributes);
        }

        public static bool IsAttributeActive(AssemblyDefinitionAsset assemblyDefinition, string attribute)
        {
            return LoadAttributes(assemblyDefinition).Contains(attribute);
        }

        public static HashSet<string> LoadAttributes(AssemblyDefinitionAsset assemblyDefinition)
        {
            var attributes = new HashSet<string>();

            if (IsAttributesFileExists(assemblyDefinition))
            {
                string path = GetAttributesFilePath(assemblyDefinition);
                string text = File.ReadAllText(path);

                ParseAttributes(text, attributes);
            }

            return attributes;
        }

        public static void SaveAttributes(AssemblyDefinitionAsset assemblyDefinition, HashSet<string> attributes)
        {
            if (attributes.Count > 0)
            {
                var builder = new StringBuilder();

                foreach (string attribute in attributes)
                {
                    builder.AppendLine(attribute);
                }

                string path = GetAttributesFilePath(assemblyDefinition);
                string text = builder.ToString();

                File.WriteAllText(path, text);
                AssetDatabase.ImportAsset(path);
            }
            else if (IsAttributesFileExists(assemblyDefinition))
            {
                AssetDatabase.DeleteAsset(GetAttributesFilePath(assemblyDefinition));
            }
            
            AssetDatabase.SaveAssets();
        }

        public static bool IsAttributesFileExists(AssemblyDefinitionAsset assemblyDefinition)
        {
            string path = GetAttributesFilePath(assemblyDefinition);

            return File.Exists(path);
        }

        public static string GetAttributesFilePath(AssemblyDefinitionAsset assemblyDefinition)
        {
            string path = AssetDatabase.GetAssetPath(assemblyDefinition);
            string directory = Path.GetDirectoryName(path)?.Replace('\\', '/');
            var builder = new StringBuilder();

            if (!string.IsNullOrEmpty(directory))
            {
                builder.Append(directory);
                builder.Append('/');
            }

            builder.Append(assemblyDefinition.name);
            builder.Append(".Attributes.cs");
            
            return builder.ToString();
        }

        private static void ParseAttributes(string text, HashSet<string> attributes)
        {
            var builder = new StringBuilder();
            bool append = false;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];

                switch (ch)
                {
                    case '[':
                    {
                        append = true;
                        builder.Append(ch);
                        break;
                    }
                    case ']':
                    {
                        append = false;
                        builder.Append(ch);

                        attributes.Add(builder.ToString());

                        builder.Clear();
                        break;
                    }
                    default:
                    {
                        if (append)
                        {
                            builder.Append(ch);
                        }
                        break;
                    }
                }
            }
        }
    }
}