﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeGeneration.LowLevelClient
{
	public static class Program
	{
		static void Main(string[] args)
		{
			bool redownloadCoreSpecification = false;
			string downloadBranch = "master";

			var answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Download online rest specifications? [Y/N] (default N): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				redownloadCoreSpecification = answer == "y";
			}

			if (redownloadCoreSpecification)
			{
				Console.Write("Branch to download specification from (default master): ");
				downloadBranch = Console.ReadLine()?.Trim();
			}
			else
			{
				// read last downloaded branch from file.
				if (File.Exists(CodeConfiguration.LastDownloadedVersionFile))
				{
					downloadBranch = File.ReadAllText(CodeConfiguration.LastDownloadedVersionFile);
				}
			}

			if (redownloadCoreSpecification)
				RestSpecDownloader.Download(downloadBranch);

			ApiGenerator.Generate(downloadBranch, "Core", "Graph", "License", "Security");
			//ApiGenerator.Generate("Core", "DeleteByQuery", "Graph", "License", "Shield");
			//ApiGenerator.Generate("Core", "Graph", "License");
			//ApiGenerator.Generate(); //generates everything under ApiSpecification
		}

	}
}
