using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC_2018 {
	static class Day_01 {
		private static int[] Input { get; set; } = new int[] { };

		static Day_01() {
			string input_file_path = Path.Combine(Program.Input_Files_Path, "day_01_part_1__input.txt");
			Input = File.ReadAllLines(input_file_path)
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => Convert.ToInt32(l))
				.ToArray();
		}

		public static int Calculate_Frequency_Drift() {
			int result = 0;
			result = Input.Sum();
			return result;
		}

		public static int Find_Period_Frequecy() {
			List<int> lFrequencies = new List<int>();
			int index = 0;
			int nCurrentFrequency = 0;
			while (true) {
				index %= Input.Length;
				nCurrentFrequency += Input[index++];
				if (lFrequencies.Any(f => f == nCurrentFrequency)) {
					break;
				}
				lFrequencies.Add(nCurrentFrequency);
			}
			return nCurrentFrequency;
		}
	}
}
