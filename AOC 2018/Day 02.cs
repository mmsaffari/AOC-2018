using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC_2018 {
	static class Day_02 {

		class Box {
			public Box() {
				QuantizedId = new Dictionary<char, int>();
			}
			public string Id { get; set; }
			public bool HasTwoLetterReps { get => QuantizedId.Values.Any(v => v == 2); }
			public bool HasThreeLetterReps { get => QuantizedId.Values.Any(v => v == 3); }
			private Dictionary<char, int> QuantizedId { get; set; }

			public override string ToString() {
				return $"{Id} - {HasTwoLetterReps} - {HasThreeLetterReps}";
			}
			public void Quatize() {
				var x = Id.ToCharArray().GroupBy(c => c, (k, chars) => new { Char = k, Count = chars.Count() }).Where(q => q.Count == 2 || q.Count == 3);
				QuantizedId = x.ToDictionary(kvp => kvp.Char, kvp => kvp.Count);
			}
		}

		private static List<Box> Input { get; set; } = new List<Box>();

		static Day_02() {
			string input_file_path = Path.Combine(Program.Input_Files_Path, "day_02_part_1__input.txt");
			Input = File.ReadAllLines(input_file_path)
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => new Box() { Id = l })
				.ToList();
		}

		public static int GetChecksum() {
			Input.AsParallel().ForAll(i => { i.Quatize(); });
			return Input.Select(i => i.HasTwoLetterReps ? 1 : 0).Sum() * Input.Select(i => i.HasThreeLetterReps ? 1 : 0).Sum();
		}

		public static string GetCommonLettersForCorrectBoxId() {
			string result = "";
			Input = Input.OrderBy(b => b.Id).ToList();
			for (int i = 0; i < Input.Count - 1; i++) {
				int diffs = 0;
				result = "";
				if (Input[i].Id.Length != Input[i + 1].Id.Length) {
					continue;
				}

				for (int j = 0; j < Input[i].Id.Length; j++) {
					if (Input[i].Id[j] != Input[i + 1].Id[j]) {
						diffs++;
					} else {
						result += Input[i].Id[j];
					}

					if (diffs > 1) {
						break;
					}
				}
				if (diffs < 2) {
					break;
				}
			}

			return result;
		}

	}
}
