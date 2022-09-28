#include <cstdlib>
#include <iostream>
#include <fstream>
#include <filesystem>
#include <string>
namespace fs = std::filesystem;

int main()
{
	std::string assignmentHeader;
	std::string assignmentCodeName;

	std::cout << "Enter task (Uppgift 0___):\n";
	std::getline(std::cin, assignmentHeader);
	assignmentHeader = "Uppgift 0" + assignmentHeader;

	int projectNamesFound = 0;
	try
	{
		for (auto const& entry : fs::directory_iterator("."))
		{
			if (fs::is_regular_file(entry) && entry.path().extension() == ".sln")
			{
				assignmentCodeName = entry.path().stem().string();
				projectNamesFound++;
			}
		}
	}
	catch (const std::exception&)
	{
		std::cout << "sln search crashed\n";
		system("pause");
		return 0;
	}
	
	if (projectNamesFound == 0 || projectNamesFound > 1)
	{
		std::cout << "No or multiple projects found. Aborting.\n";
		system("pause");
		return 0;
	}

	std::cout << "Found project: " << assignmentCodeName << "\n";
	//std::cout << "Enter project name:\n";
	//std::getline(std::cin, assignmentCodeName);

	const auto copyOptions = fs::copy_options::update_existing;

	fs::create_directories(assignmentHeader + "/Exe");
	
	try
	{
		fs::copy_file("x64/Release/" + assignmentCodeName + ".exe", assignmentHeader + "/Exe/" + assignmentCodeName + ".exe", copyOptions);
	}
	catch (const std::exception&)
	{
		std::cout << "Missing 64 bit release build\n";
	}
	
	fs::create_directories(assignmentHeader + "/Source");
	
	try
	{
		fs::copy_file(assignmentCodeName + ".sln", assignmentHeader + "/Source/" + assignmentCodeName + ".sln", copyOptions);
	}
	catch (const std::exception&)
	{
		std::cout << "Failed to copy sln\n";
	}
	
	try
	{
		fs::copy(assignmentCodeName + "/", assignmentHeader + "/Source/" + assignmentCodeName + "/", copyOptions);
	}
	catch (const std::exception&)
	{
		std::cout << "Failed to copy code files\n";
	}
	
	std::cout << "Done\n";
	
	system("pause");
}

//
// <assaignment text>
// |-Exe
// | |- <code name>.exe <- (./x64/Release/<code name>.exe)
// |-Source
//   |- <code name>
//   |  |- "All files, no sub dir" <- (./<code name>/)
//   |- <code name>.sln <- (./<code name>.sln) 
//