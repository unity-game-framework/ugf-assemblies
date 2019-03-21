using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Assemblies.Editor
{
    [Serializable]
    public class AssemblyDefinitionAttributesAsset
    {
        [SerializeField] private List<string> m_attributes = new List<string>();

        public List<string> Attributes { get { return m_attributes; } }
    }
}