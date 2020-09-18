# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.6.0-preview](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.6.0-preview) - 2020-09-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/11?closed=1)  
    

### Changed

- Update project to Unity 2020.1 ([#45](https://github.com/unity-game-framework/ugf-assemblies/pull/45))

## [1.5.2](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.5.2) - 2019-05-16  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.5.1...1.5.2)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/10?closed=1)

### Changed
- `GetAssetPathsUnderAssemblyDefinitionFile` now use only extension name instead of file search pattern. (#41)

## [1.5.1](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.5.1) - 2019-04-19  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.5.0...1.5.1)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/9?closed=1)

### Fixed
- `AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile` does not collect directories from root with `asmdef` file (#38)

## [1.5.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.5.0) - 2019-04-19  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.4.1...1.5.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/8?closed=1)

### Added
- `GetAssetPathsUnderAssemblyDefinitionFile` overload with type argument to get assets that represents the specified asset type. (#33)
- `GetAssetPathsUnderAssemblyDefinitionFile` overload with `ICollection<string>` argument as results.

### Deprecated
- `GetAssetPathsUnderAssemblyDefinitionFile` overload that returns new `List<string>` of the paths has been deprecated.

## [1.4.1](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.4.1) - 2019-04-18  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.4.0...1.4.1)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/7?closed=1)

### Changed
- `AssemblyBrowsableTypesAllEnumerable` has been refactored. (#29)

## [1.4.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.4.0) - 2019-04-17  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.3.0...1.4.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/6?closed=1)

### Added
- `GetBrowsableTypes` to get enumerable through the all browsable types from all assemblies or only from one. (#2)
- `GetBrowsableAssemblies` to get enumerable through the all browsable assemblies. (#2)
- `GetBrowsableTypes` with enumerable, will allow to implement additional filtering of the types. (#26)

## [1.3.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.3.0) - 2019-04-16  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.2.0...1.3.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/5?closed=1)

### Added
- `AssemblyUtility.GetBrowsableTypes` with results collection as `ICollection<Type>` and optional `Assembly` argument. (#22 #23)
- `AssemblyUtility.TryGetBrowsableAssembly` to get browsable assembly by name.

### Deprecated
- `AssemblyUtility.GetBrowsableTypes` overloads that takes results collection as `List<Type>` have been deprecated.

## [1.2.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.2.0) - 2019-04-01  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.1.0...1.2.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/4?closed=1)

### Added
- `AssemblyEditorUtility`: methods to work with assembly definitions (#18)
  - `TryFindAssemblyDefinitionFilePathFromAssetPath` to find assembly Definition file path from the asset path that belongs to it.
  - `GetAssetPathsUnderAssemblyDefinitionFile` to get all asset paths that belongs to the specified assembly definition.
  - `TryFindCompilationAssemblyByName` to find editor compilation assembly by the specified assembly name.

## [1.1.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.1.0) - 2019-03-22  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.0.1...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/3?closed=1)

### Added
- `AssemblyEditorUtility` to control assembly attributes.
- Context menu to `Assembly Definition File` to toggle usage of the `AssemblyBrowsableAttribute`.

### Changed
- `AssemblyBrowsableAttribute`: `AttributeUsage.AllowMultiple` set to `true` to allow multiple usage of the attribute on assembly in project.

## [1.0.1](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.0.1) - 2019-03-14  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.0.0...1.0.1)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/2?closed=1)

### Changed
- Refactoring: fix usage of 'var' (#11)

## [1.0.0](https://github.com/unity-game-framework/ugf-assemblies/releases/tag/1.0.0) - 2019-02-22  

- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/a43e504...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/1?closed=1)

### Added
- This is a initial release.


