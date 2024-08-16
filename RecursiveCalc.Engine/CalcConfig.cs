namespace RecursiveCalcEngine
{
	public class CalcConfig
	{
		public int Occurrence { get; set; } = 6;

		public List<string[]> Digits = new() {
			new string[]{ "(","{","[", "〖" },
			new string[]{ ")","}","]", "〗" },
			new string[]{ ";" },
			new string[] { ".", "," },
			new string[] { "=" },
			new string[] { "-", "–" },
			new string[] { "+", },
			new string[] {"*", "∙", "·", "×", "x", "х" },
			new string[] { "/", "÷", ":", "⁄", "\\" },
			new string[] { "^", "&" },
		};

		public string[] CharOperator { get; set; } =
		{ "√", "!", "∜", "∛", "%" };

		public string[] CurDigits => Digits.Skip(5).Select(a => a.First()).ToArray();

		public string[] Bra => Digits.Skip(0).First();
		public string[] Ket => Digits.Skip(1).First();
		public string[] DivideDigits => Digits.Skip(2).First();
		public string[] Points => Digits.Skip(3).First();
		public string[] EqualsDigits => Digits.Skip(4).First();
		public string[] Subs => Digits.Skip(5).First();
		public string[] Adds => Digits.Skip(6).First();
		public string[] Multiplies => Digits.Skip(7).First();
		public string[] Divisions => Digits.Skip(8).First();
		public string[] Pows => Digits.Skip(9).First();
	}
}
