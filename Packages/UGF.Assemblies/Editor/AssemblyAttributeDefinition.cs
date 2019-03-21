using System;
using System.Text;

namespace UGF.Assemblies.Editor
{
    public class AssemblyAttributeDefinition
    {
        public Type AttributeType { get; }

        public AssemblyAttributeDefinition(Type attributeType)
        {
            AttributeType = attributeType ?? throw new ArgumentNullException(nameof(attributeType));
        }

        public string Format()
        {
            var builder = new StringBuilder();
            string space = AttributeType.Namespace;
            string name = AttributeType.Name;

            builder.Append("[assembly: ");
            
            if (!string.IsNullOrEmpty(space))
            {
                builder.Append(space);
                builder.Append('.');
            }

            builder.Append(name);
            builder.Append("()]");
            
            return builder.ToString();
        }
    }
}