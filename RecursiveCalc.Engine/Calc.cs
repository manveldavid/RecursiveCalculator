using System.Globalization;

namespace RecursiveCalcEngine
{
	public class Calc
	{
		public static string Clean(string prompt, CalcConfig? config = null)
		{
            if (config == null) config = new CalcConfig();
			prompt = prompt.ToLower();

			prompt = prompt.Replace(" ", "");
			prompt = prompt.Replace("\n", "");
			prompt = prompt.Replace("\r", "");
			prompt = prompt.Replace("\t", "");
			prompt = prompt.Replace("\"", "");
            prompt = prompt.Replace("'", "");

            foreach (var group in config.Digits)
			{
				var currentDigit = group.First();

				foreach (var digit in group.Skip(1))
					if (digit != null && prompt.Contains(digit))
						prompt = prompt.Replace(digit, currentDigit);
			}

			return prompt;
		}

		public static CalcResult Solve(string prompt, List<string>? history = null, int occurence = -1)
        {
            var config = new CalcConfig();
			config.Occurrence = occurence;

			if (history == null)
				history = new List<string>();

			var result = Solve(prompt, history, config);

			return new CalcResult { 
				Result = 
					config.Occurrence != -1 ? 
					Math.Round(result, config.Occurrence) : 
					result, 
				History = history 
			};
		}

		public static double Solve(string prompt, List<string> history, CalcConfig config)
        {
            if (config == null) 
				config = new CalcConfig();

			if (history == null) 
				history = new List<string>();

			double result = 0;
			double resultRound;	

			if (!double.TryParse(prompt, CultureInfo.InvariantCulture, out result) && (prompt.Length > 0))
			{
				var bra = config.Bra.First();
				var ket = config.Ket.First();
				var allDigits = config.CurDigits.Skip(1);

				#region SolveBrakets
				if (prompt.Contains(bra))
				{
					if (prompt.Count(c => c == bra.First()) != prompt.Count(c => c == ket.First()))
					{
						throw new Exception("invalid brakets");
					}
					while (prompt.Contains(bra))
					{
						#region GetBrakets
						int braLastIndex = prompt.LastIndexOf(bra);
						var leftPart = prompt.Substring(0, braLastIndex);
						var braketPart = prompt.Substring(braLastIndex);
						int ketFirstIndex = braketPart.IndexOf(ket);
						var rightPart = braketPart.Substring(ketFirstIndex + 1);
						braketPart = braketPart.Substring(0, ketFirstIndex + 1);
						#endregion

						#region GetOperator
						var operatorPart = string.Empty;
						if (leftPart.Length > 0)
						{
							int operatorIndex = leftPart.Length - 1;
							for (; operatorIndex >= 0; operatorIndex--)
							{
								if (!char.IsLetter(leftPart[operatorIndex]) &&
									config.CharOperator.All(o => o.First() != leftPart[operatorIndex]))
								{
									break;
								}
							}
							operatorIndex++;

							operatorPart = leftPart.Substring(operatorIndex);
							leftPart = leftPart.Substring(0, operatorIndex);
						}
						#endregion

						#region GetArgsAndPrompt
						var braketPromptPart = braketPart.Substring(1, braketPart.Length - 2);
						var operatorArgs = string.Empty;
						var braketsResult = string.Empty;
						var divideDigit = config.DivideDigits.First();
						if (braketPromptPart.Contains(divideDigit))
						{
							var divideIndex = braketPromptPart.IndexOf(divideDigit);
							operatorArgs = braketPromptPart.Substring(divideIndex + 1);
							braketPromptPart = braketPromptPart.Substring(0, divideIndex);
						}
						#endregion

						#region SolvePrompt
						if (braketPromptPart != string.Empty)
						{
							braketsResult = Solve(braketPromptPart, history, config).ToString();
						}
						#endregion

						#region SolveArgs
						if (operatorArgs != string.Empty)
						{
							operatorArgs = Solve(operatorArgs, history, config).ToString();
						}
						#endregion

						#region ReplaseBraket
						result = OperatorSolver.Solve(operatorPart, braketsResult, operatorArgs);
						prompt = leftPart + result + rightPart;
						var args = string.IsNullOrEmpty(operatorArgs) ? string.Empty : divideDigit + operatorArgs;
						resultRound = config.Occurrence != -1? Math.Round(result, config.Occurrence) : result;
						var historyMessage = operatorPart + bra + braketsResult + args + ket + config.EqualsDigits.First() + resultRound;
						if (!string.IsNullOrEmpty(operatorPart))
						{
							history.Add(historyMessage);
						}
						#endregion
					}
				}
				#endregion

				#region SolveDigit
				if (config.CurDigits.Any(d => prompt.Contains(d)))
				{
					#region ReplaceMinus
					for (int i = 1; i < prompt.Length; i++)
					{
						if (prompt[i] == config.Subs.First().First() && char.IsDigit(prompt[i - 1]))
						{
							var leftPart = prompt.Substring(0, i);
							var rightPart = prompt.Substring(i);
							prompt = leftPart + config.Adds.First() + rightPart;
						}
					}
					if (prompt.Contains("--")) { prompt = prompt.Replace("--", ""); }
					#endregion
					var digitPrompt = prompt;
					while (allDigits.Any(d => digitPrompt.Contains(d)))
					{
						foreach (var priorDigit in allDigits)
						{
							int digitIndex = digitPrompt.IndexOf(priorDigit);
							if (digitIndex != -1)
							{
								var leftPart = prompt.Substring(0, digitIndex);
								var digit = prompt.Substring(digitIndex, priorDigit.Length);
								var rightPart = prompt.Substring(digitIndex + priorDigit.Length);
								var leftResult = Solve(leftPart, history, config);
								var rightResult = Solve(rightPart, history, config);
								result = DigitSolver.Solve(leftResult, digit, rightResult);
								digitPrompt = result.ToString();
								resultRound = config.Occurrence != -1? Math.Round(result, config.Occurrence) : result;
								var historyMessage = leftResult + digit + rightResult + config.EqualsDigits.First() + resultRound;
								history.Add(historyMessage);
							}
						}
					}
				}
				#endregion
			}
			
			return result;
		}
	}
}
