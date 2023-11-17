namespace RecursiveCalcEngine
{
	public static class DigitSolver
	{
		public static double Solve(double leftPart, string digit, double rightPart)
		{
			switch (digit)
			{
				case "+": return Add(leftPart,rightPart);
				case "-": return Sub(leftPart, rightPart);
				case "/": return Div(leftPart, rightPart);
				case "*": return Mult(leftPart, rightPart);
				case "^": return Pow(leftPart, rightPart);
				default: throw new Exception("invalid digit");
			}
		}

		public static double Add(double leftPart, double rightPart)
		{
			return leftPart+rightPart;
		}
		public static double Sub(double leftPart, double rightPart)
		{
			return leftPart - rightPart;
		}
		public static double Div(double leftPart, double rightPart)
		{
			return leftPart / rightPart;
		}
		public static double Mult(double leftPart, double rightPart)
		{
			return leftPart * rightPart;
		}
		public static double Pow(double leftPart, double rightPart)
		{
			return Math.Pow(leftPart,rightPart);
		}

	}
}
