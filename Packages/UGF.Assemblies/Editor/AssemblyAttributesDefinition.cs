using System.Collections.Generic;
using System.Text;

namespace UGF.Assemblies.Editor
{
    public class AssemblyAttributesDefinition
    {
        public List<AssemblyAttributeDefinition> Attributes { get; } = new List<AssemblyAttributeDefinition>();

        public string Format()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < Attributes.Count; i++)
            {
                builder.AppendLine(Attributes[i].Format());
            }

            return builder.ToString();
        }
    }
}