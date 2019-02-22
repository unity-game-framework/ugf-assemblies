using System;

namespace UGF.Assemblies.Runtime
{
    /// <summary>
    /// Attribute used to mark specific assembly as available for types search.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class AssemblyBrowsableAttribute : Attribute
    {
    }
}