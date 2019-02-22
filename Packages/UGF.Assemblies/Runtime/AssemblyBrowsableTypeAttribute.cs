using System;

namespace UGF.Assemblies.Runtime
{
    /// <summary>
    /// Attribute used to mark specific type as available for search.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Delegate | AttributeTargets.Enum)]
    public class AssemblyBrowsableTypeAttribute : Attribute
    {
    }
}