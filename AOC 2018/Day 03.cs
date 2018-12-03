using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

			////Test Data:
			//Input = new List<Claim> {
			//	Claim.Parse("#1 @ 1,3: 4x4"),
			//	Claim.Parse("#2 @ 3,1: 4x4"),
			//	Claim.Parse("#3 @ 5,5: 2x2")
			//};
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

		public static int[] getRGBArray(this Bitmap image) {
			if (image == null) { throw new ArgumentNullException("image"); }
			int[] result = new int[image.Width * image.Height];
			BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			try {
				byte[] ba = new byte[data.Stride];
				for (int line = 0; line < data.Height; line++) {
					Marshal.Copy(data.Scan0 + (line * data.Stride), ba, 0, data.Stride);
					for (int pixeloffset = 0; pixeloffset < data.Width; pixeloffset++) {
						result[line * image.Width + pixeloffset] =
							(ba[pixeloffset * 4 + 3] << 24) +   // A
							(ba[pixeloffset * 4 + 2] << 16) +   // R
							(ba[pixeloffset * 4 + 1] << 8) +    // G
							ba[pixeloffset * 4];                // B
					}
				}
			} finally {
				image.UnlockBits(data);
			}
			return result;
		}

		public static int CalculateOverlappingArea_Graphical() {
			const int FabricWidth = 1000;
			Color
				bg = Color.FromArgb(0, 0, 0, 0),
				signature = Color.FromArgb(0x77fe0000);
			DateTime dtStart = DateTime.Now;
			int result = 0;
			using (Bitmap img = new Bitmap(FabricWidth, FabricWidth)) {
				using (Graphics g = Graphics.FromImage(img)) {
					g.Clear(bg);
					g.FillRectangles(new SolidBrush(signature), Input.Select(c => c.Rectangle).ToArray());
					g.Flush();
				}

				//img.Save(@"C:\Workspace\VS\Advent of Code\AOC 2018\Inputs\day_03.png", ImageFormat.Png);

				////This will take almost 4 times more than what the straight way takes.
				//for (int i = 0; i < img.Width; i++) {
				//	for (int j = 0; j < img.Height; j++) {
				//		Color c = img.GetPixel(i, j);
				//		if (c.A > 0x77) { result += 1; }
				//	}
				//}

				// This one takes nearly 25% less!
				// I like this one!
				int[] pixels = img.getRGBArray();
				result = pixels.Where(p => p != 0x77fe0000).Where(p => p != 0x00).Count();
			}

			TimeSpan tsExecTime = DateTime.Now - dtStart;
			Console.WriteLine("It took {0} to execute Graphically.", tsExecTime);
			return result;
		}

		public static int FindNonOverlappingClaimId() {
			return Input
				.Where(c =>
					!Input
						.Except(new Claim[] { c })
						.Any(c1 => c1.Rectangle.IntersectsWith(c.Rectangle))
				)
				.Single()
				.Id;
		}
	}
}
