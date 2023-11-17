namespace RecursiveCalcEngine
{
	public class OperatorSolver
	{
		public static double Solve(string operatorPart, string prompt, string args)
		{
			switch (operatorPart)
			{
				case "!": return FactorialOperator(operatorPart, prompt, args);
				case "abs": return AbsOperator(operatorPart, prompt, args);
				case "ctg": return CtgOperator(operatorPart, prompt, args);
				case "tg": return TgOperator(operatorPart, prompt, args);
				case "cos": return CosOperator(operatorPart, prompt, args);
				case "sin": return SinOperator(operatorPart, prompt, args);
				case "tgh": return TgHOperator(operatorPart, prompt, args);
				case "cosh": return CosHOperator(operatorPart, prompt, args);
				case "sinh": return SinHOperator(operatorPart, prompt, args);
				case "arctg": return ArcTgOperator(operatorPart, prompt, args);
				case "arccos": return ArcCosOperator(operatorPart, prompt, args);
				case "arcsin": return ArcSinOperator(operatorPart, prompt, args);
				case "sqrt": return SqrtOperator(operatorPart, prompt, args);
				case "√": return SqrtOperator(operatorPart, prompt, args);
				case "%": return ReminderOperator(operatorPart, prompt, args);
				case "rem": return ReminderOperator(operatorPart, prompt, args);
				case "round": return RoundOperator(operatorPart, prompt, args);
				case "∜": args = "4"; return SqrtOperator(operatorPart, prompt, args);
				case "∛": args = "4"; return SqrtOperator(operatorPart, prompt, args);
				case "pow": return PowOperator(operatorPart, prompt, args);
				case "exp": return ExpOperator(operatorPart, prompt, args);
				case "e": return ExpOperator(operatorPart, prompt, args);
				case "pi": return PiOperator(operatorPart, prompt, args);
				case "": return EmptyOperator(operatorPart, prompt, args);
				default: throw new Exception("invalid operator");
			}
		}

		public static double EmptyOperator(string operatorPart, string prompt, string args) 
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = promptResult;
			return result;
		}

		public static double RoundOperator(string operatorPart, string prompt, string args) 
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 3;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Round(promptResult, (int)(Math.Round(argsResult, 0)));
			return result;
		}

		public static double ReminderOperator(string operatorPart, string prompt, string args) 
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 1;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = promptResult % argsResult;
			return result;
		}

		public static double PowOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Pow(promptResult, argsResult);
			return result;
		}

		public static double SqrtOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			argsResult = 1 / argsResult;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Pow(promptResult, argsResult);
			return result;
		}

		public static double SinOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Sin(promptResult);
			return result;
		}
		public static double ArcSinOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Asin(promptResult);
			return result;
		}

		public static double CosOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Cos(promptResult);
			return result;
		}
		public static double ArcCosOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Acos(promptResult);
			return result;
		}

		public static double TgOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Tan(promptResult);
			return result;
		}

		public static double ArcTgOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Atan(promptResult);
			return result;
		}

		public static double SinHOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Sinh(promptResult);
			return result;
		}
		public static double CosHOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Cosh(promptResult);
			return result;
		}
		public static double TgHOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Tanh(promptResult);
			return result;
		}

		public static double CtgOperator(string operatorPart, string prompt, string args)
		{
			return 1 / TgOperator(operatorPart, prompt, args);
		}

		public static double AbsOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;
			var result = Math.Abs(promptResult);
			return result;
		}

		public static double FactorialOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 2;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 0;

			if(promptResult > 0)
			{
				return promptResult * FactorialOperator(operatorPart, (--promptResult).ToString(), args);
			}
			else
			{
				return 1;
			}
		}

		public static double PiOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 0;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 1;
			var result = Math.Pow(Math.PI,promptResult);
			return result;
		}

		public static double ExpOperator(string operatorPart, string prompt, string args)
		{
			double argsResult;
			if (!double.TryParse(args, out argsResult)) argsResult = 0;
			double promptResult;
			if (!double.TryParse(prompt, out promptResult)) promptResult = 1;
			var result = Math.Exp(promptResult);
			return result;
		}
	}
}
