﻿using System.IO;
using System.Reflection;

namespace AOC_2018 {
	class Program {

		internal static string Input_Files_Path { get => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\Inputs")); }

		static void Main(string[] args) {
			//System.Console.WriteLine("Day 1 - Part 1 - Frequency Drift: {0:N0}", Day_01.Calculate_Frequency_Drift());
			//System.Console.WriteLine("Day 1 - Part 2 - Period Frequency : {0:N0}", Day_01.Find_Period_Frequecy());

			System.Console.WriteLine("Checksum= {0:N0}", Day_02.GetChecksum());
			System.Console.WriteLine("Common letters for the two correct Box Ids= {0:N0}", Day_02.GetCommonLettersForCorrectBoxId());
		}
	}
}
