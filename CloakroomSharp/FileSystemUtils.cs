using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloakroomSharp
{
	public static class FileSystemUtils
	{
		public static bool CopyDirectory(string aSource, string aDestination, string aDirName, SearchOption aSearchOption = SearchOption.AllDirectories)
		{
			try
			{
				string sourceDirName = Path.Combine(aSource, aDirName);
				string destDirName = Path.Combine(aDestination, aDirName);

				Directory.CreateDirectory(destDirName);

				if (aSearchOption == SearchOption.AllDirectories)
				{
					var allAssetDirectories = Directory.GetDirectories(sourceDirName, "*", aSearchOption);
					foreach (string dir in allAssetDirectories)
					{
						string dirToCreate = dir.Replace(sourceDirName, destDirName);
						Directory.CreateDirectory(dirToCreate);
					}
				}

				var allAssetFiles = Directory.GetFiles(sourceDirName, "*.*", aSearchOption);
				foreach (string newPath in allAssetFiles)
				{
					File.Copy(newPath, newPath.Replace(sourceDirName, destDirName), false);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to copy folder");
				Console.WriteLine(e.ToString());

				return false;
			}

			return true;
		}
	}
}
