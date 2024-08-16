using RecursiveCalcEngine;

namespace FormulaSolver.Common
{
	public class Solver
	{
		public static string Solve(string data)
		{
			bool skip = false;
			Dictionary<string, double> variables = new();
			List<string> equasions = new();
			data.Split('\n').ToList()
				.ForEach(l => equasions.Add(l));
			List<string> results = new();

			foreach (string equasion in equasions)
			{
				if (string.IsNullOrEmpty(equasion)) { results.Add(equasion); continue; }
				if (equasion == "{" || equasion == "}") { skip = !skip; }
				if (!skip && equasion.Contains('='))
				{
					double result;

					if (equasion.Contains('='))
					{
						var cleanPart = Calc.Clean(equasion);
						var resultPart = cleanPart.Substring(cleanPart.LastIndexOf('=') + 1);
						if (double.TryParse(resultPart, out result))
						{
							var variableName = cleanPart.Substring(0, cleanPart.IndexOf('='));
							if (variables.Keys.Contains(variableName))
							{
								variables[variableName] = result;
							}
							else
							{
								variables.Add(variableName, result);
							}
							results.Add(equasion);
						}
						else
						{
							var vars = variables.OrderBy(p => (p.Key.Length)/1).Reverse();
							foreach (var variable in vars)
							{
								if (resultPart.Contains(variable.Key))
								{
									resultPart = resultPart.Replace(variable.Key, variable.Value.ToString());
								}
							}
							var history = new List<string>();
							result = Calc.Solve(resultPart, history).Result;

							var historyBlock = string.Empty;
							historyBlock += "\n{\n";
							historyBlock += "\tprompt:" + resultPart + "\n";
							history.ForEach(l => historyBlock += "\t" + l + "\n");
							historyBlock += "}";

							var variableName = cleanPart.Substring(0, cleanPart.IndexOf('='));
							if (variables.Keys.Contains(variableName))
							{
								variables[variableName] = result;
							}
							else
							{
								variables.Add(variableName, result);
							}
							results.Add(equasion + "= " + result.ToString() + historyBlock);
						}
					}
				}
				else { results.Add(equasion); }
			}

			data = string.Empty;
			results.ForEach(l => data += l + "\n");
			data = data.Substring(0, data.Length - 1);
			return data;
		}

		public static string Clean(string data)
		{
			List<string> equasions = new();
			data.Split('\n').ToList()
				.ForEach(l => equasions.Add(l));
			var skip = false;
			List<string> results = new();
			foreach (var equasion in equasions) 
			{
				if (equasion == "{" || equasion == "}") { skip = !skip; }
				else if(!skip)
				{
					var startIndex = equasion.IndexOf('=');
					var lastIndex = equasion.LastIndexOf('=');
					if (startIndex != lastIndex && startIndex != -1)
					{
						var cleanEq = equasion.Substring(0, lastIndex);
						results.Add(cleanEq);
					}
					else
					{
						results.Add(equasion);
					}
				}
			}

			data = string.Empty;
			results.ForEach (l => data += l + "\n");
			data = data.Substring(0, data.Length - 1);
			return data;
		}
	}
}
