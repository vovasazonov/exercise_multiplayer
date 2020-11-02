using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Serialization.BinaryFormatterSerialization
{
    sealed class CustomizedBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            String exeAssembly = Assembly.GetExecutingAssembly().FullName;
            
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, exeAssembly));

            return typeToDeserialize;
        }
    }
}