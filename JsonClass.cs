using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Tr3umPHantDesigns
{
	public class JsonClass
	{
		public static string JSONSerialize<T>(T obj) // serialize json for sending to php
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
			using (MemoryStream ms = new MemoryStream())
			{
				serializer.WriteObject(ms, obj);
				return Encoding.Default.GetString(ms.ToArray());
			}
		}
	}
}

