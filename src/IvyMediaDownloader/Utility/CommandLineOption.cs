using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invary.Utility
{
	class CommandLineOption
	{
		public string Name { get; protected set; } = "";
		public string Help { get; set; } = "";
		public int ArgValueCount { get; protected set; } = 0;
		public bool IgnoreValueDoubleQuotation { get; protected set; } = true;

		public bool Activated { get; private set; } = false;

		public List<string> Values { get; private set; } = new List<string>();


		public object Tag { get; set; } = null;
		public ApplyHandler OnApply { get; set; } = null;

		public delegate void ApplyHandler(CommandLineOption option);



		public CommandLineOption()
		{
		}

		public CommandLineOption(string name, int valueCount, bool bIngnoreDoubleQuotation)
		{
			Name = name;
			ArgValueCount = valueCount;
			IgnoreValueDoubleQuotation = bIngnoreDoubleQuotation;
		}



		public void Apply()
		{
			if (Activated == false)
				return;

			OnApply?.Invoke(this);
		}



		public virtual void Destroy()
		{
			Activated = false;
			Values.Clear();
		}



		public virtual bool SetArg(string[] args, ref int nStartIndex)
		{
			if (Activated)
				return false;

			if (nStartIndex + ArgValueCount >= args.Length)
				return false;

			if (string.Compare(Name, args[nStartIndex], true) != 0)
				return false;

			Values.Clear();
			for (int i = 0; i < ArgValueCount; i++)
			{
				Values.Add(args[nStartIndex + 1 + i]);
			}

			if (IgnoreValueDoubleQuotation)
			{
				for (int i = 0; i < Values.Count; i++)
				{
					Values[i] = RemoveDoubleQuotation(Values[i]);
				}
			}

			Activated = true;
			nStartIndex += ArgValueCount + 1;

			return true;
		}



		protected static string RemoveDoubleQuotation(string text)
		{
			if (text.Length <= 1)
				return text;

			if (text[0] != '"' || text[text.Length - 1] != '"')
				return text;

			return text.Substring(1, text.Length - 2);
		}

		public static bool AnalyzeCommandLine(List<CommandLineOption> options)
		{
			var args = Environment.GetCommandLineArgs();

			int nMax = args.Length;
			int nIndex = 1;

			while (true)
			{
				bool bSet = false;
				foreach (var option in options)
				{
					bSet = option.SetArg(args, ref nIndex);
					if (bSet)
						break;
				}
				if (nIndex >= nMax)
					return true;
				if (bSet == false)
					return false;
			}
		}

	}








}
