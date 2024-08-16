using RecursiveCalcEngine;
using System.Text;

Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.Unicode;

const string occurrenceCommand = "occurrence";
var lastResult = 0D;
var config = new CalcConfig() { Occurrence = 6 };
var history = new List<string>();

while (true)
{
	Console.Write(">");
	var prompt = Console.ReadLine();
	if(string.IsNullOrEmpty(prompt)) continue;

	var checkOccurrence = OccurrenceValue(prompt);
	if(checkOccurrence != null) { config.Occurrence = (int)checkOccurrence; continue; }
	prompt = Calc.Clean(prompt);
	if(FirstCharIsDigit(prompt, config)) { prompt = lastResult.ToString() + prompt; }

    history.Clear();
	var result = Calc.Solve(prompt, history, config);
	lastResult = result;

	Console.WriteLine("\nResult: " + result);
	Console.WriteLine("\nHistory:");
	foreach (var line in history)
	{
		Console.WriteLine($"\t{history.IndexOf(line) + 1}.\t{line}");
	}
	Console.WriteLine("\n\n\n");
}

bool FirstCharIsDigit(string prompt, CalcConfig config)
{
	var firstChar = prompt.First();
	if (config.CurDigits.Contains(firstChar.ToString()))
	{
		return true;
	}
	else { return false; }
}

int? OccurrenceValue(string prompt)
{
    if (prompt.Contains(occurrenceCommand))
    {
		int occurrence;
        if (int.TryParse(prompt.Replace(occurrenceCommand, ""), out occurrence))
		{
			return occurrence;
		}
    }
	return null;
}