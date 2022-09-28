using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CloakroomSharp
{
	internal class Application
	{
		public void Run()
		{
			string assignmentHeader;
			string assignmentCodeName = "";

			Console.WriteLine("Enter task:");
			assignmentHeader = Console.ReadLine();
			assignmentHeader = "Uppgift 0" + assignmentHeader;

			int projectNamesFound = 0;

			try
			{
				string[] filesInTop = Directory.GetFiles(".");
				foreach (string item in filesInTop)
				{
					if(Path.GetExtension(item) == ".sln")
					{
						assignmentCodeName = Path.GetFileNameWithoutExtension(item);
						projectNamesFound++;
					}
				}
			}
			catch (Exception)
			{

				
			}

			if (projectNamesFound == 0 || projectNamesFound > 1)
			{
				Console.WriteLine("No or multiple projects found. Aborting.");
				return;
			}

			try
			{
				Directory.CreateDirectory(assignmentHeader + "/Exe");
				File.Copy("/x64/Release/" + assignmentCodeName + ".exe", "/" + assignmentHeader + "/Exe/" + assignmentCodeName + ".exe", true);
			}
			catch (Exception)
			{

			}

			try
			{
				Directory.CreateDirectory(assignmentHeader + "/Source");
				File.Copy(assignmentCodeName + ".sln", assignmentHeader + "/Source/" + assignmentCodeName + ".sln");
				var allFiles = Directory.GetFiles(assignmentCodeName + "/");

				foreach (string newPath in allFiles)
				{
					File.Copy(newPath, newPath.Replace("/" + assignmentCodeName + "/", "/" + assignmentHeader + "/Source/" + assignmentCodeName + "/"), true);
				}
			}
			catch (Exception)
			{

			}

			Console.ReadLine();

	//		try
	//		{
	//			for (auto const&entry : fs::directory_iterator("."))
	//				{
	//				if (fs::is_regular_file(entry) && entry.path().extension() == ".sln")
	//				{
	//					assignmentCodeName = entry.path().stem().string();
	//					projectNamesFound++;
	//				}
	//			}
	//		}
	//		catch (const std::exception&)
	//{
	//			std::cout << "sln search crashed\n";
	//			system("pause");
	//			return 0;
	//		}

				//		if (projectNamesFound == 0 || projectNamesFound > 1)
				//		{
				//			std::cout << "No or multiple projects found. Aborting.\n";
				//			system("pause");
				//			return 0;
				//		}

				//		std::cout << "Found project: " << assignmentCodeName << "\n";
				//		//std::cout << "Enter project name:\n";
				//		//std::getline(std::cin, assignmentCodeName);

				//		const auto copyOptions = fs::copy_options::update_existing;

				//		fs::create_directories(assignmentHeader + "/Exe");

				//		try
				//		{
				//			fs::copy_file("x64/Release/" + assignmentCodeName + ".exe", assignmentHeader + "/Exe/" + assignmentCodeName + ".exe", copyOptions);
				//		}
				//		catch (const std::exception&)
				//{
				//			std::cout << "Missing 64 bit release build\n";
				//		}

				//		fs::create_directories(assignmentHeader + "/Source");

				//		try
				//		{
				//			fs::copy_file(assignmentCodeName + ".sln", assignmentHeader + "/Source/" + assignmentCodeName + ".sln", copyOptions);
				//		}
				//		catch (const std::exception&)
				//{
				//			std::cout << "Failed to copy sln\n";
				//		}

				//		try
				//		{
				//			fs::copy(assignmentCodeName + "/", assignmentHeader + "/Source/" + assignmentCodeName + "/", copyOptions);
				//		}
				//		catch (const std::exception&)
				//{
				//			std::cout << "Failed to copy code files\n";
				//		}

				//		std::cout << "Done\n";

				//		system("pause");
		}
	}
}
