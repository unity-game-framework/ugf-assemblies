using System;
using System.Collections.Generic;

namespace UGF.Assemblies.Editor
{
    public static partial class AssemblyEditorUtility
    {
        /// <summary>
        /// Gets collection of asset paths that belongs to the specified assembly definition file and contains specified extension.
        /// </summary>
        /// <param name="assemblyDefinitionFilePath">The path of assembly definition file.</param>
        /// <param name="assetExtension">The asset extension. (Use format with '.')</param>
        [Obsolete("GetAssetPathsUnderAssemblyDefinitionFile has been deprecated. It will be removed in the next major release.")]
        public static List<string> GetAssetPathsUnderAssemblyDefinitionFile(string assemblyDefinitionFilePath, string assetExtension)
        {
            if (assemblyDefinitionFilePath == null) throw new ArgumentNullException(nameof(assemblyDefinitionFilePath));
            if (assetExtension == null) throw new ArgumentNullException(nameof(assetExtension));

            var paths = new List<string>();

            GetAssetPathsUnderAssemblyDefinitionFile(paths, assemblyDefinitionFilePath, assetExtension);

            return paths;
        }
    }
}
