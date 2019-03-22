using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor
{
    /// <summary>
    /// Provides utility for working with assemblies in editor.
    /// </summary>
    public static class AssemblyEditorUtility
    {
        /// <summary>
        /// Adds or removes a text representation of the attribute in attributes file of the specified assembly definition asset, depends on the specified state value.
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
        /// <param name="attribute">The text representation of the attribute.</param>
        /// <param name="state">The state of the attribute.</param>
        public static void SetAttributeActive(AssemblyDefinitionAsset assemblyDefinition, string attribute, bool state)
        {
            if (assemblyDefinition == null) throw new ArgumentNullException(nameof(assemblyDefinition));
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            
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

        /// <summary>
        /// Loads all attributes from the attributes file of the specified assembly definition asset.
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
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

        /// <summary>
        /// Saves the specified attributes to the attributes file of the specified assembly definition asset.
        /// <para>
        /// If the count of the passed attributes is zero and attribute file already exists, will delete empty attribute file.
        /// </para>
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
        /// <param name="attributes">The collection of the text representation of the attributes.</param>
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

        /// <summary>
        /// Determines whether the specified text representation of the attribute exist in attributes file of the specified assembly definition asset.
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
        /// <param name="attribute">The text representation of the attribute.</param>
        public static bool IsAttributeActive(AssemblyDefinitionAsset assemblyDefinition, string attribute)
        {
            return LoadAttributes(assemblyDefinition).Contains(attribute);
        }

        /// <summary>
        /// Determines whether the attribute file exists for the specified assembly definition file.
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
        public static bool IsAttributesFileExists(AssemblyDefinitionAsset assemblyDefinition)
        {
            string path = GetAttributesFilePath(assemblyDefinition);

            return File.Exists(path);
        }

        /// <summary>
        /// Gets the attributes file for the specified assembly definition file.
        /// </summary>
        /// <param name="assemblyDefinition">The assembly definition asset.</param>
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