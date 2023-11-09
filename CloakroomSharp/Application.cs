using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace CloakroomSharp
{
	internal class Application
	{
		public void Run()
		{
			string handInVersion;

			string assignmentHeader;
			Console.WriteLine("Version of hand in, leave empty for first handin:");
			handInVersion = Console.ReadLine();

			Console.WriteLine("Enter task:");
			assignmentHeader = Console.ReadLine();

			assignmentHeader += " - Elias Böök";

			ProjectTemplate projectTemplate = new TemplateTGEPP();

			bool result = projectTemplate.Run(".", assignmentHeader, handInVersion);

			Console.WriteLine("Press enter to continue");
			Console.ReadLine();

			//bool hasFailed = false;

			//string assignmentHeader;
			//string assignmentCodeName = "";

			//Console.WriteLine("Enter task:");
			//assignmentHeader = Console.ReadLine();
			//assignmentHeader = "Uppgift 0" + assignmentHeader;

			//int projectNamesFound = 0;

			//try
			//{
			//	string[] filesInTop = Directory.GetFiles(".");
			//	foreach (string item in filesInTop)
			//	{
			//		if (Path.GetExtension(item) == ".sln")
			//		{
			//			assignmentCodeName = Path.GetFileNameWithoutExtension(item);
			//			projectNamesFound++;
			//		}
			//	}
			//}
			//catch (Exception)
			//{
			//	Console.WriteLine("sln search crashed");
			//	hasFailed = true;
			//}

			//if (projectNamesFound == 0 || projectNamesFound > 1)
			//{
			//	Console.WriteLine("No or multiple projects found. Aborting.");
			//	Console.WriteLine("Failed to copy code files");
			//	return;
			//}

			//Directory.CreateDirectory(assignmentHeader);

			//try
			//{
			//	Directory.CreateDirectory(assignmentHeader + "/Exe");
			//	File.Copy("x64/Release/" + assignmentCodeName + ".exe", assignmentHeader + "/Exe/" + assignmentCodeName + ".exe", true);
			//}
			//catch (Exception)
			//{
			//	Console.WriteLine("Missing 64 bit release build");
			//	hasFailed = true;
			//}

			//try
			//{
			//	Directory.CreateDirectory(assignmentHeader + "/Source");
			//	File.Copy(assignmentCodeName + ".sln", assignmentHeader + "/Source/" + assignmentCodeName + ".sln");

			//	var allFiles = Directory.GetFiles(assignmentCodeName + "/");
			//	Directory.CreateDirectory(assignmentHeader + "/Source/" + assignmentCodeName);
			//	foreach (string sourcePath in allFiles)
			//	{
			//		string newPath = sourcePath.Replace(assignmentCodeName + "/", assignmentHeader + "/Source/" + assignmentCodeName + "/");
			//		File.Copy(sourcePath, newPath, true);
			//	}
			//}
			//catch (Exception)
			//{
			//	Console.WriteLine("Failed to copy code files");
			//	hasFailed = true;
			//}

			//if (hasFailed)
			//{
			//	Console.WriteLine("Press enter to continue");
			//	Console.ReadLine();
			//	return;
			//}

			//bool zipFailed = false;

			//try
			//{
			//	ZipFile.CreateFromDirectory(assignmentHeader, assignmentHeader + ".zip");
			//}
			//catch (Exception)
			//{
			//	Console.WriteLine("Failed to zip task");
			//	zipFailed = true;
			//}

			//if (!zipFailed)
			//{
			//	Directory.Delete(assignmentHeader, true);
			//}
		}
	}
}
