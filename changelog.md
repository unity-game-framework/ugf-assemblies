# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased - 2019-01-01
- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/0.0.0...0.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/0?closed=1)

### Added
- `AssemblyUtility.GetBrowsableTypes` with results collection as `ICollection<Type>` and optional `Assembly` argument. 
- `AssemblyUtility.TryGetBrowsableAssembly` to get browsable assembly by name.

### Changed
- `AssemblyUtility.GetBrowsableTypes` overloads that takes results collection as `List<Type>` have been deprecated.

### Deprecated
- Nothing.

### Removed
- Nothing.

### Fixed
- Nothing.

### Security
- Nothing.

## 1.2.0 - 2019-04-01
- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.1.0...1.2.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/4?closed=1)

### Added
- `AssemblyEditorUtility`: methods to work with assembly definitions (#18)
  - `TryFindAssemblyDefinitionFilePathFromAssetPath` to find assembly Definition file path from the asset path that belongs to it.
  - `GetAssetPathsUnderAssemblyDefinitionFile` to get all asset paths that belongs to the specified assembly definition.
  - `TryFindCompilationAssemblyByName` to find editor compilation assembly by the specified assembly name.

## 1.0.1 - 2019-03-32
- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.0.1...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/3?closed=1)

### Added
- `AssemblyEditorUtility` to control assembly attributes.
- Context menu to `Assembly Definition File` to toggle usage of the `AssemblyBrowsableAttribute`.

### Changed
- `AssemblyBrowsableAttribute`: `AttributeUsage.AllowMultiple` set to `true` to allow multiple usage of the attribute on assembly in project.

## 1.0.1 - 2019-03-14
- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/1.0.0...1.0.1)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/2?closed=1)

### Changed
- Refactoring: fix usage of 'var' (#11)

## 1.0.0 - 2019-02-22
- [Commits](https://github.com/unity-game-framework/ugf-assemblies/compare/a43e504...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-assemblies/milestone/1?closed=1)

### Added
- This is a initial release.

---
> Unity Game Framework | Copyright 2019
