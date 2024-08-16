using FormulaSolver.Common;
using System.Diagnostics;
using System.Text;

var workDir = "files";

var list = "_ls";
var open = "_open";
var copy = "_copy";
var solve = "_solve";
var clean = "_clean";
var delete = "_delete";
var create = "_create";

var absPath = Path.Combine(Directory.GetCurrentDirectory(), workDir);
if (!Directory.Exists(absPath))
{
	Directory.CreateDirectory(absPath);
}

while (true)
{
	Console.Write("\n>");
	var prompt = Console.ReadLine();
	if (prompt == open)
	{
		Process.Start("explorer.exe", Path.Combine(absPath)).WaitForExit();
	}
	else if (prompt == list)
	{
		Directory.GetFiles(absPath)
			.Select(d => d.Substring(d.LastIndexOf('\\') + 1).Replace(".txt", ""))
			.ToList().ForEach(Console.WriteLine);
	}
	else
	{
		var filename = prompt.Replace(open, "");
		filename = filename.Replace(create, "");
		filename = filename.Replace(copy, "");
		filename = filename.Replace(clean, "");
		filename = filename.Replace(solve, "");
		filename = filename.Replace(delete, "");
		filename = filename.Replace(" ", "");

		filename = filename + ".txt";
		var path = Path.Combine(absPath, filename);

		if (prompt.Contains(create))
		{
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
			continue;
		}
		if (File.Exists(path))
		{
			if (prompt.Contains(delete))
			{
				File.Delete(path);
				continue;
			}

			if (prompt.Contains(copy))
			{
				var data = File.ReadAllText(path);
				filename = filename.Replace(".txt", "");
				filename += "-copy";
				filename += ".txt";
				path = Path.Combine(absPath, filename);
				File.Create(path).Close();
				File.WriteAllText(path, data);
				continue;
			}

			if (prompt.Contains(solve))
			{
				var fileData = File.ReadAllText(path, Encoding.UTF8);
				fileData = Solver.Clean(fileData);
				var result = Solver.Solve(fileData);
				File.WriteAllText(path, result);
			}

			if (prompt.Contains(clean))
			{
				var fileData = File.ReadAllText(path, Encoding.UTF8);
				var result = Solver.Clean(fileData);
				File.WriteAllText(path, result);
			}

			if (!filename.Contains('_'))
			{
				var proc = Process.Start(new ProcessStartInfo(path)
				{
					UseShellExecute = true,

				});
				proc?.WaitForExit();
			}
		}
	}
}