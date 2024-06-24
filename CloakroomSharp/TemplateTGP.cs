using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloakroomSharp
{
	class TemplateTGP : ProjectTemplate
	{

		public override bool Run(string aPath, string aAssignmentName, string aHandInVersion)
		{
			const string BUILD_CONFIG = "Release";

			string turnInFolderPath = aAssignmentName;
			string turnInFolderExePath = turnInFolderPath + "/Exe";
			string turnInFolderSourcePath = turnInFolderPath + "/Source";


			const string projectPath = ".";

			string projectExePath = "Bin/" + BUILD_CONFIG;

			List<string> PEDirsToCopy = new List<string>();
			PEDirsToCopy.Add("Bin/EngineAssets");
			PEDirsToCopy.Add("Bin/Assets");
			PEDirsToCopy.Add("Bin/" + BUILD_CONFIG + "/Content/Shaders");

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
				//File.Copy(projectExePath + "/Modelviewer.exe", turnInFolderExePath + "/Modelviewer.exe");

				//var allBinDirectories = Directory.GetDirectories(projectExePath, "*", SearchOption.AllDirectories);
				//foreach (string dir in allBinDirectories)
				//{
				//	string dirToCreate = dir.Replace(projectExePath, turnInFolderExePath);
				//	Directory.CreateDirectory(dirToCreate);
				//}

				var allBinFiles = Directory.GetFiles(projectExePath, "*.*", SearchOption.TopDirectoryOnly);
				foreach (string newPath in allBinFiles)
				{
					string extention = Path.GetExtension(newPath);
					if (extention == ".pdb")
					{
						continue;
					}
					File.Copy(newPath, newPath.Replace(projectExePath, turnInFolderExePath), false);
				}

				// Copy PE data
				foreach (string dir in PEDirsToCopy)
				{
					string dirName = dir.Substring(dir.LastIndexOf('/') + 1);
					string sourceDirPath = dir.Substring(0, dir.LastIndexOf('/'));
					FileSystemUtils.CopyDirectory(sourceDirPath, turnInFolderExePath, dirName);
				}

				// Settings file
				File.Copy(projectPath + "/Bin/Settings.json", turnInFolderExePath + "/Settings.json", true);
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
				List<string> dirToCopy = new List<string>()
				{
					"AssetManager",
					"Bin",
					"CommonUtilities",
					"Core",
					"GraphicsEngine",
					"ImGui",
					"Logging",
					"ModelViewer",
					"ThirdParty",
				};

				foreach (string dir in dirToCopy)
				{
					FileSystemUtils.CopyDirectory(projectPath, turnInFolderSourcePath, dir);
				}

				File.Copy(projectPath + "/TGP22.sln", turnInFolderSourcePath + "/TGP22.sln");
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
				if (aHandInVersion != "")
				{
					ZipFile.CreateFromDirectory(turnInFolderPath, turnInFolderPath + " " + aHandInVersion + ".zip");
				}
				else
				{
					ZipFile.CreateFromDirectory(turnInFolderPath, turnInFolderPath + ".zip");
				}
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
