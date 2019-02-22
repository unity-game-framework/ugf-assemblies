using System;

namespace UGF.Assemblies.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Delegate | AttributeTargets.Enum)]
    public class AssemblyBrowsableTypeAttribute : Attribute
    {
    }
}