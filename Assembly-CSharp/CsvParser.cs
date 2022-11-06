using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x02000A33 RID: 2611
public class CsvParser
{
	// Token: 0x0600454B RID: 17739 RVA: 0x00164160 File Offset: 0x00162360
	public static List<CsvParser.CsvFields> ParseCsvFromText(string i_text)
	{
		return CsvParser.CsvToArrayList(i_text);
	}

	// Token: 0x0600454C RID: 17740 RVA: 0x00164168 File Offset: 0x00162368
	private static List<CsvParser.CsvFields> CsvToArrayList(string csvText)
	{
		List<CsvParser.CsvFields> list = new List<CsvParser.CsvFields>();
		csvText = csvText.Trim(new char[]
		{
			'\r',
			'\n'
		});
		Regex regex = new Regex("^.*(?:\\n|$)", RegexOptions.Multiline);
		Regex regex2 = new Regex("\\s*(\"(?:[^\"]|\"\")*\"|[^,]*)\\s*,", RegexOptions.None);
		Match match = regex.Match(csvText);
		string text = string.Empty;
		while (match.Success)
		{
			text = match.Value;
			while (CsvParser.CountString(text, "\"") % 2 == 1)
			{
				match = match.NextMatch();
				if (!match.Success)
				{
				}
				text += match.Value;
			}
			text = text.TrimEnd(new char[]
			{
				'\r',
				'\n'
			});
			text += ",";
			CsvParser.CsvFields csvFields = new CsvParser.CsvFields();
			Match match2 = regex2.Match(text);
			while (match2.Success)
			{
				string text2 = match2.Groups[1].Value;
				text2 = text2.Trim();
				if (text2.StartsWith("\"") && text2.EndsWith("\""))
				{
					text2 = text2.Substring(1, text2.Length - 2);
					text2 = text2.Replace("\"\"", "\"");
				}
				match2 = match2.NextMatch();
				if (text2.IndexOf('#') == 0)
				{
					break;
				}
				csvFields.field.Add(text2);
			}
			if (csvFields.field.Count != 0)
			{
				list.Add(csvFields);
			}
			match = match.NextMatch();
		}
		return list;
	}

	// Token: 0x0600454D RID: 17741 RVA: 0x00164308 File Offset: 0x00162508
	private static int CountString(string strInput, string strFind)
	{
		int num = 0;
		for (int i = strInput.IndexOf(strFind); i > -1; i = strInput.IndexOf(strFind, i + 1))
		{
			num++;
		}
		return num;
	}

	// Token: 0x02000A34 RID: 2612
	public class CsvFields
	{
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x00164350 File Offset: 0x00162550
		public List<string> FieldList
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x040039DF RID: 14815
		public List<string> field = new List<string>();
	}
}
