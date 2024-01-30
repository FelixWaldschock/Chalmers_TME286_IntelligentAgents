using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ObjectSerializerLibrary
{
    public static class ObjectCopier
    {
        // Copies all fields marked with the [DataMember] attribute
        public static object Copy(object obj)
        {
            object copiedObject;
            Type type = obj.GetType();
            using (MemoryStream stream = new MemoryStream())
            {
                ObjectXmlSerializer.SerializeObjectToStream(stream, obj);
                stream.Position = 0;
                copiedObject = ObjectXmlSerializer.ObtainSerializedObjectFromStream(stream, type);
            }
            return copiedObject;
        }
    }
}
