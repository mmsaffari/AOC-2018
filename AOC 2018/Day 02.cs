using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2018 {
	static class Day_02 {

		class Box {
			public string Id { get; set; }
			public bool HasTwoLetterReps { get; set; }
			public bool HasThreeLetterReps { get; set; }
			
			public override string ToString() {
				return $"{Id} - {HasTwoLetterReps} - {HasThreeLetterReps}";
			}
		}


		private static List<Box> Input { get; set; } = new List<Box>();
		private static Regex rTwoLetterReps = new Regex(@"(([a-z])\2)", RegexOptions.IgnoreCase);
		private static Regex rThreeLetterReps = new Regex(@"(([a-z])\2{2})", RegexOptions.IgnoreCase);
		private static Regex rCleaner = new Regex(@"(([a-z])\2{3,})", RegexOptions.IgnoreCase);

		static Day_02() {
			string input_file_path = Path.Combine(Program.Input_Files_Path, "day_02_part_1__input.txt");
			Input = File.ReadAllLines(input_file_path)
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => new Box() { Id = l, HasTwoLetterReps = false, HasThreeLetterReps = false })
				.ToList();
		}

		public static int GetChecksum() {

			Input.ForEach(i => {
				string s = string.Join("", i.Id.ToCharArray().OrderBy(c => c).ToArray());
				i.Id = s;
				s = rCleaner.Replace(s, "");
				i.HasThreeLetterReps = rThreeLetterReps.IsMatch(s);
				s = rThreeLetterReps.Replace(s, "___");
				i.HasTwoLetterReps = rTwoLetterReps.IsMatch(s);
				System.Console.WriteLine(i);
				File.AppendAllText(Path.Combine(Program.Input_Files_Path, "day_02_part_1__debug.txt"), i.ToString() + "\n");
			});
			return Input.Select(i => i.HasTwoLetterReps ? 1 : 0).Sum() * Input.Select(i => i.HasThreeLetterReps ? 1 : 0).Sum();
		}
	}
}
