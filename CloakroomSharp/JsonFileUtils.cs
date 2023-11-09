using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CloakroomSharp
{
	public static class JsonFileUtils
	{
		private static readonly JsonSerializerSettings _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

		public static void SimpleWrite(object obj, string fileName)
		{
			var jsonString = JsonConvert.SerializeObject(obj, _options);
			File.WriteAllText(fileName, jsonString);
		}
	}
}
