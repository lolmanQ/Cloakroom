using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloakroomSharp
{
	class TemplateTGEPP : ProjectTemplate
	{
		private bool CopyDirectory(string aSource, string aDestination, string aDirName)
		{
			try
			{
				string sourceDirName = Path.Combine(aSource, aDirName);
				string destDirName = Path.Combine(aDestination, aDirName);

				Directory.CreateDirectory(destDirName);
				var allAssetDirectories = Directory.GetDirectories(sourceDirName, "*", SearchOption.AllDirectories);
				foreach (string dir in allAssetDirectories)
				{
					string dirToCreate = dir.Replace(sourceDirName, destDirName);
					Directory.CreateDirectory(dirToCreate);
				}

				var allAssetFiles = Directory.GetFiles(sourceDirName, "*.*", SearchOption.AllDirectories);
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

		public override bool Run(string aPath, string aAssignmentName)
		{
			bool hasFailed = false;

			string turnInFolderPath = aAssignmentName;
			string turnInFolderExePath = turnInFolderPath + "/Exe";
			string turnInFolderSourcePath = turnInFolderPath + "/Source";


			string projectPath = ".";

			string projectExePath = "Bin";
			string projectEngineAssetsPath = "EngineAssets";

			// Create the assignment folder
			try
			{
				Directory.CreateDirectory(turnInFolderPath);
				Directory.CreateDirectory(turnInFolderExePath);
				Directory.CreateDirectory(turnInFolderSourcePath);
				Console.WriteLine("Created folder structure");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to create folder structure");
				Console.WriteLine(e.ToString());

				return false;
			}

			// Copy exe data
			try
			{
				File.Copy(projectExePath + "/Game_Release.exe", turnInFolderExePath + "/Game_Release.exe");

				var allBinDirectories = Directory.GetDirectories(projectExePath, "*", SearchOption.AllDirectories);
				foreach (string dir in allBinDirectories)
				{
					string dirToCreate = dir.Replace(projectExePath, turnInFolderExePath);
					Directory.CreateDirectory(dirToCreate);
				}

				var allBinFiles = Directory.GetFiles(projectExePath, "*.*", SearchOption.AllDirectories);
				foreach (string newPath in allBinFiles)
				{
					string extention = Path.GetExtension(newPath);
					if (extention == ".pdb" || extention == ".exe")
					{
						continue;
					}
					File.Copy(newPath, newPath.Replace(projectExePath, turnInFolderExePath), false);
				}

				//Engine assets
				Directory.CreateDirectory(turnInFolderExePath + "/EngineAssets");
				var allAssetDirectories = Directory.GetDirectories(projectEngineAssetsPath, "*", SearchOption.AllDirectories);
				foreach (string dir in allAssetDirectories)
				{
					string dirToCreate = dir.Replace(projectEngineAssetsPath, turnInFolderExePath + "/EngineAssets");
					Directory.CreateDirectory(dirToCreate);
				}

				var allAssetFiles = Directory.GetFiles(projectEngineAssetsPath, "*.*", SearchOption.AllDirectories);
				foreach (string newPath in allAssetFiles)
				{
					File.Copy(newPath, newPath.Replace(projectEngineAssetsPath, turnInFolderExePath + "/EngineAssets"), false);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to copy exe data");
				Console.WriteLine(e.ToString());

				return false;
			}


			// Copy source data
			try
			{
				CopyDirectory(projectPath, turnInFolderSourcePath, "Dependencies");
				CopyDirectory(projectPath, turnInFolderSourcePath, "EngineAssets");
				CopyDirectory(projectPath, turnInFolderSourcePath, "Premake");
				CopyDirectory(projectPath, turnInFolderSourcePath, "Source");

				File.Copy(projectPath + "/generate_game.bat", turnInFolderSourcePath + "/generate_game.bat");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to copy source data");
				Console.WriteLine(e.ToString());

				return false;
			}

			bool zipFailed = false;

			try
			{
				ZipFile.CreateFromDirectory(turnInFolderPath, turnInFolderPath + ".zip");
			}
			catch (Exception)
			{
				Console.WriteLine("Failed to zip task");
				zipFailed = true;
			}

			if (!zipFailed)
			{
				Directory.Delete(turnInFolderPath, true);
			}

			Console.WriteLine("Done");
			return true;
		}
	}
}
