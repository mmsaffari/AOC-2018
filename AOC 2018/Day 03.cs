using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2018 {
	static class Day_03 {
		class Claim {
			public int Id { get; set; }
			public Rectangle Rectangle { get; set; }

			public static Claim Parse(string s) {
				Claim result = new Claim();
				Regex rParser = new Regex(@"^\#(?<id>\d+)\s\@\s(?<left>\d+)\,(?<top>\d+)\:\s(?<width>\d+)x(?<height>\d+)$", RegexOptions.Singleline);
				Match m = rParser.Match(s);
				if (!m.Success) {
					throw new FormatException($"{s} is not well-formed.");
				}

				result.Id = int.Parse(m.Groups["id"].Value);
				result.Rectangle = new Rectangle {
					X = int.Parse(m.Groups["left"].Value),
					Y = int.Parse(m.Groups["top"].Value),
					Width = int.Parse(m.Groups["width"].Value),
					Height = int.Parse(m.Groups["height"].Value)
				};
				return result;
			}
		}

		private static List<Claim> Input { get; set; } = new List<Claim>();

		static Day_03() {
			string input_file_path = Path.Combine(Program.Input_Files_Path, "day_03_part_1__input.txt");
			Input = File.ReadAllLines(input_file_path)
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => Claim.Parse(l))
				.ToList();
		}

		public static int CalculateOverlappingArea() {
			DateTime dtStart = DateTime.Now;
			int result = 0;
			int[,] Fabric = new int[1000, 1000];
			for (int index = 0; index < Input.Count; index++) {
				Claim c = Input[index];
				for (int i = c.Rectangle.Left; i < c.Rectangle.Right; i++) {
					for (int j = c.Rectangle.Top; j < c.Rectangle.Bottom; j++) {
						Fabric[i, j] += 1;
					}
				}
			}
			result = Fabric.Cast<int>().Where(n => n > 1).Count();
			TimeSpan tsExecTime = DateTime.Now - dtStart;
			Console.WriteLine("It took {0} to execute.", tsExecTime);
			return result;
		}
	}
}
