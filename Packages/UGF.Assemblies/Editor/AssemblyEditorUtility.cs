using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;

namespace UGF.Assemblies.Editor
{
    /// <summary>
    /// Provides utility for working with assemblies in editor.
    /// </summary>
    public static partial class AssemblyEditorUtility
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

                InternalParseAttributes(text, attributes);
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

        /// <summary>
        /// Tries to get an assembly definition file path from the specified asset path that belongs to assembly.
        /// </summary>
        /// <param name="assetPath">The path of the asset.</param>
        /// <param name="assemblyDefinitionPath">The found path of assembly definition file.</param>
        public static bool TryFindAssemblyDefinitionFilePathFromAssetPath(string assetPath, out string assemblyDefinitionPath)
        {
            string directory = Path.GetDirectoryName(assetPath);

            return InternalTryFindAssemblyDefinitionFilePathFromAssetPath(directory, out assemblyDefinitionPath);
        }

        /// <summary>
        /// Gets collection of asset paths that belongs to the specified assembly definition file and represents specified asset type.
        /// </summary>
        /// <param name="results">The collection of the paths to fill.</param>
        /// <param name="assemblyDefinitionFilePath">The path of assembly definition file.</param>
        /// <param name="assetType">The type of the asset.</param>
        public static void GetAssetPathsUnderAssemblyDefinitionFile(ICollection<string> results, string assemblyDefinitionFilePath, Type assetType)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (assemblyDefinitionFilePath == null) throw new ArgumentNullException(nameof(assemblyDefinitionFilePath));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));

            string directory = Path.GetDirectoryName(assemblyDefinitionFilePath);

            InternalGetAssetPathsUnderAssemblyDefinitionFile(results, directory, assetType);
        }

        /// <summary>
        /// Gets collection of asset paths that belongs to the specified assembly definition file and contains specified extension.
        /// </summary>
        /// <param name="results">The collection of the paths to fill.</param>
        /// <param name="assemblyDefinitionFilePath">The path of assembly definition file.</param>
        /// <param name="assetExtension">The asset extension. (Use format with '.')</param>
        public static void GetAssetPathsUnderAssemblyDefinitionFile(ICollection<string> results, string assemblyDefinitionFilePath, string assetExtension)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (assemblyDefinitionFilePath == null) throw new ArgumentNullException(nameof(assemblyDefinitionFilePath));
            if (assetExtension == null) throw new ArgumentNullException(nameof(assetExtension));

            string directory = Path.GetDirectoryName(assemblyDefinitionFilePath);
            string pattern = $"*{assetExtension}";

            InternalGetAssetPathsUnderAssemblyDefinitionFile(results, directory, pattern);
        }

        /// <summary>
        /// Tries to find compilation assembly by the specified assembly name.
        /// </summary>
        /// <param name="assemblyName">The name of assembly.</param>
        /// <param name="assembly">The found assembly.</param>
        public static bool TryFindCompilationAssemblyByName(string assemblyName, out Assembly assembly)
        {
            Assembly[] assemblies = CompilationPipeline.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];

                if (assembly.name == assemblyName)
                {
                    return true;
                }
            }

            assembly = null;
            return false;
        }

        private static bool InternalTryFindAssemblyDefinitionFilePathFromAssetPath(string directory, out string path)
        {
            string[] files = Directory.GetFiles(directory, "*.asmdef");

            if (files.Length == 0)
            {
                directory = Path.GetDirectoryName(directory);

                if (!string.IsNullOrEmpty(directory))
                {
                    return InternalTryFindAssemblyDefinitionFilePathFromAssetPath(directory, out path);
                }

                path = null;
                return false;
            }

            path = files[0].Replace("\\", "/");
            return true;
        }

        private static void InternalGetAssetPathsUnderAssemblyDefinitionFile(ICollection<string> paths, string directory, Type assetType)
        {
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);

            for (int i = 0; i < files.Length; i++)
            {
                string path = files[i].Replace("\\", "/");
                Type type = AssetDatabase.GetMainAssetTypeAtPath(path);

                if (assetType.IsAssignableFrom(type))
                {
                    paths.Add(path);
                }
            }

            for (int i = 0; i < directories.Length; i++)
            {
                string subDirectory = directories[i];

                if (Directory.GetFiles(subDirectory, "*.asmdef").Length == 0)
                {
                    InternalGetAssetPathsUnderAssemblyDefinitionFile(paths, subDirectory, assetType);
                }
            }
        }

        private static void InternalGetAssetPathsUnderAssemblyDefinitionFile(ICollection<string> paths, string directory, string pattern)
        {
            string[] files = Directory.GetFiles(directory, pattern);
            string[] directories = Directory.GetDirectories(directory);

            for (int i = 0; i < files.Length; i++)
            {
                string path = files[i].Replace("\\", "/");

                paths.Add(path);
            }

            for (int i = 0; i < directories.Length; i++)
            {
                string subDirectory = directories[i];

                if (Directory.GetFiles(subDirectory, "*.asmdef").Length == 0)
                {
                    InternalGetAssetPathsUnderAssemblyDefinitionFile(paths, subDirectory, pattern);
                }
            }
        }

        private static void InternalParseAttributes(string text, HashSet<string> attributes)
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
