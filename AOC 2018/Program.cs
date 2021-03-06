﻿using System;
using System.IO;
using System.Reflection;

namespace AOC_2018 {
	class Program {

		internal static string Input_Files_Path { get => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\Inputs")); }

		static void Main(string[] args) {
			//Console.WriteLine("Day 1 - Part 1 - Frequency Drift: {0:N0}", Day_01.Calculate_Frequency_Drift());
			//Console.WriteLine("Day 1 - Part 2 - Period Frequency : {0:N0}", Day_01.Find_Period_Frequecy());

			//Console.WriteLine("Checksum= {0:N0}", Day_02.GetChecksum());
			//Console.WriteLine("Common letters for the two correct Box Ids= {0:N0}", Day_02.GetCommonLettersForCorrectBoxId());

			Console.WriteLine("Shared Claims Area= {0:N0}", Day_03.CalculateOverlappingArea());
			Console.WriteLine("Shared Claims Area [Graphically]= {0:N0}", Day_03.CalculateOverlappingArea_Graphical());
			Console.WriteLine("Non-overlapping claim Id: {0}", Day_03.FindNonOverlappingClaimId());
		}
	}
}
