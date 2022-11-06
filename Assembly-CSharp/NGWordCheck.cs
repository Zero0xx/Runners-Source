using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataTable;
using UnityEngine;

// Token: 0x0200048C RID: 1164
public class NGWordCheck
{
	// Token: 0x060022C0 RID: 8896 RVA: 0x000D05A4 File Offset: 0x000CE7A4
	public static void Load()
	{
		if (NGWordCheck.m_sceneLoader == null)
		{
			string name = "NGWordResourceSceneLoader";
			GameObject gameObject = GameObject.Find(name);
			if (gameObject == null)
			{
				gameObject = new GameObject(name);
			}
			if (gameObject != null)
			{
				NGWordCheck.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				bool onAssetBundle = true;
				if (NGWordCheck.m_sceneLoader.AddLoadAndResourceManager("NGWordTable", onAssetBundle, ResourceCategory.ETC, true, false, null))
				{
					NGWordCheck.DebugDrawLocal("Load");
				}
			}
		}
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x000D0620 File Offset: 0x000CE820
	public static bool IsLoaded()
	{
		return !(NGWordCheck.m_sceneLoader != null) || NGWordCheck.m_sceneLoader.Loaded;
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x000D0640 File Offset: 0x000CE840
	public static void ResetData()
	{
		NGWordTable ngwordTable = GameObjectUtil.FindGameObjectComponent<NGWordTable>("NGWordTable");
		if (ngwordTable != null)
		{
			UnityEngine.Object.Destroy(ngwordTable.gameObject);
		}
		if (NGWordCheck.m_sceneLoader != null)
		{
			UnityEngine.Object.Destroy(NGWordCheck.m_sceneLoader.gameObject);
		}
		NGWordCheck.DebugDrawLocal("ResetData");
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x000D0698 File Offset: 0x000CE898
	public static string Check(string target_word, UILabel uiLabel)
	{
		NGWordCheck.DebugDrawLocal("check target_word=" + target_word);
		if (NGWordCheck.isCheckUILabel(target_word, uiLabel))
		{
			NGWordCheck.ErrorDraw("target_word=" + target_word + " check error UILabel");
			return target_word;
		}
		if (NGWordCheck.isCheckEmoji(target_word))
		{
			NGWordCheck.ErrorDraw("target_word=" + target_word + " check error emoji");
			return target_word;
		}
		if (NGWordCheck.isCheckKisyuIzon(target_word))
		{
			NGWordCheck.ErrorDraw("target_word=" + target_word + " check error kisyu izon");
			return target_word;
		}
		NGWordCheck.SetupWordData();
		string text = NGWordCheck.convertKana(target_word);
		NGWordCheck.DebugDrawLocal("convertKana=" + text);
		int space_count = 0;
		string nospace_word = NGWordCheck.StrReplace(" ", string.Empty, text, ref space_count);
		return NGWordCheck.checkProc(text, nospace_word, space_count);
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x000D0758 File Offset: 0x000CE958
	private static void SetupWordData()
	{
		if (NGWordCheck.m_wordData.Count == 0)
		{
			NGWordData[] dataTable = NGWordTable.GetDataTable();
			if (dataTable != null)
			{
				foreach (NGWordData item in dataTable)
				{
					NGWordCheck.m_wordData.Add(item);
				}
			}
		}
		NGWordCheck.DebugDrawLocal("SetupWordData m_wordData.Count=" + NGWordCheck.m_wordData.Count);
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x000D07C4 File Offset: 0x000CE9C4
	private static string checkProc(string check_str, string nospace_word, int space_count)
	{
		NGWordCheck.DebugDrawLocal("checkProc check_str=" + check_str + " nospace_word=" + nospace_word);
		int num = 0;
		foreach (NGWordData ngwordData in NGWordCheck.m_wordData)
		{
			if (ngwordData.param == 0)
			{
				if (check_str.IndexOf(ngwordData.word) != -1)
				{
					NGWordCheck.ErrorDraw(string.Concat(new string[]
					{
						"0 check_str=",
						check_str,
						" checkProc index=",
						num.ToString(),
						" row.word=",
						ngwordData.word
					}));
					return ngwordData.word;
				}
				if (nospace_word.IndexOf(ngwordData.word) != -1)
				{
					NGWordCheck.ErrorDraw(string.Concat(new string[]
					{
						"0 nospace_word=",
						nospace_word,
						" checkProc index=",
						num.ToString(),
						" row.word=",
						ngwordData.word
					}));
					return ngwordData.word;
				}
			}
			else
			{
				if (check_str == ngwordData.word)
				{
					NGWordCheck.ErrorDraw(string.Concat(new string[]
					{
						"1 check_str=",
						check_str,
						" checkProc index=",
						num.ToString(),
						" row.word=",
						ngwordData.word
					}));
					return ngwordData.word;
				}
				if (nospace_word == ngwordData.word)
				{
					NGWordCheck.ErrorDraw(string.Concat(new string[]
					{
						"1 nospace_word=",
						nospace_word,
						" checkProc index=",
						num.ToString(),
						" row.word=",
						ngwordData.word
					}));
					return ngwordData.word;
				}
			}
			num++;
		}
		return null;
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x000D09C4 File Offset: 0x000CEBC4
	private static bool isCheckUILabel(string str, UILabel uiLabel)
	{
		if (str != null && uiLabel != null)
		{
			UIFont font = uiLabel.font;
			if (font != null)
			{
				BMFont bmFont = font.bmFont;
				if (bmFont != null)
				{
					for (int i = 0; i < str.Length; i++)
					{
						char c = str[i];
						if (!font.isDynamic)
						{
							if (bmFont.GetGlyph((int)c) == null)
							{
								NGWordCheck.DebugDrawLocal(string.Concat(new object[]
								{
									"isCheckUILabel BMGlyph str=",
									str,
									" i=",
									i,
									" c=",
									c
								}));
								return true;
							}
						}
						else
						{
							Font dynamicFont = font.dynamicFont;
							CharacterInfo characterInfo;
							if (dynamicFont != null && !dynamicFont.GetCharacterInfo(c, out characterInfo, font.dynamicFontSize, font.dynamicFontStyle))
							{
								NGWordCheck.DebugDrawLocal(string.Concat(new object[]
								{
									"isCheckUILabel dynamicFont str=",
									str,
									" i=",
									i,
									" c=",
									c
								}));
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x000D0AF4 File Offset: 0x000CECF4
	private static bool isCheckEmoji(string str)
	{
		string text = "[\\u2002-\\u2005]|\\u203C|\\u2049|\\u2122|\\u2139|[\\u2194-\\u2199]|\\u21A9|\\u21AA";
		if (NGWordCheck.PregMatch(text, str))
		{
			NGWordCheck.DebugDrawLocal("PregMatch " + text);
			return true;
		}
		string text2 = "\\u231A|\\u231B|[\\u23E9-\\u23EC]|\\u23F0|\\u23F3|\\u24C2|\\u25AA|\\u25AB|\\u25B6|\\u25C0|[\\u25FB-\\u25FE]";
		if (NGWordCheck.PregMatch(text2, str))
		{
			NGWordCheck.DebugDrawLocal("PregMatch " + text2);
			return true;
		}
		string text3 = "[\\u2600-\\u27FF]|\\u2934|\\u2935|[\\u2B05-\\u2B07]|\\u2B1B|\\u2B1C|\\u2B50|\\u2B55|\\u3030|\\u303D|\\u3297|\\u3299";
		if (NGWordCheck.PregMatch(text3, str))
		{
			NGWordCheck.DebugDrawLocal("PregMatch " + text3);
			return true;
		}
		string text4 = "[\\uE000-\\uF8FF]";
		if (NGWordCheck.PregMatch(text4, str))
		{
			NGWordCheck.DebugDrawLocal("PregMatch " + text4);
			return true;
		}
		string text5 = "[\\uD800-\\uE000]";
		if (NGWordCheck.PregMatch(text5, str))
		{
			NGWordCheck.DebugDrawLocal("PregMatch " + text5);
			return true;
		}
		return false;
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x000D0BBC File Offset: 0x000CEDBC
	private static bool isCheckKisyuIzon(string str)
	{
		string text = "～∥―－①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩ㍉㌔㌢㍍㌘㌧㌃㌶㍑㍗㌍㌦㌣㌫㍊㌻㎜㎝㎞㎎㎏㏄㎡㍻〝〟№㏍℡㊤㊥㊦㊧㊨㈱㈲㈹㍾㍽㍼≒≡∫∮∑√⊥∠∟⊿∵∩∪ⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩ￢￤＇＂㈱№℡∵";
		foreach (char value in text)
		{
			if (str.IndexOf(value) != -1)
			{
				NGWordCheck.DebugDrawLocal("isCheckKisyuIzon=" + value.ToString());
				return true;
			}
		}
		return false;
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x000D0C18 File Offset: 0x000CEE18
	private static string convertKana(string value)
	{
		string text = NGWordCheck.PregReplace("\\s+", " ", value);
		text = NGWordCheck.MbConvertKana_r(text);
		text = NGWordCheck.MbConvertKana_n(text);
		text = NGWordCheck.MbConvertKana_K(text);
		text = NGWordCheck.MbConvertKana_C(text);
		text = NGWordCheck.MbConvertKana_V(text);
		text = NGWordCheck.StrToLower(text);
		text = NGWordCheck.str2UpperKana(text);
		return NGWordCheck.replaceSpecialString(text);
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x000D0C70 File Offset: 0x000CEE70
	private static string MbConvertKana_r(string str)
	{
		string text = "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";
		string text2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		char[] array = text.ToCharArray();
		char[] array2 = text2.ToCharArray();
		if (array.Length == array2.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				str = str.Replace(array[i], array2[i]);
			}
			char[] array3 = text.ToLower().ToCharArray();
			char[] array4 = text2.ToLower().ToCharArray();
			for (int j = 0; j < array3.Length; j++)
			{
				str = str.Replace(array3[j], array4[j]);
			}
		}
		return str;
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x000D0D10 File Offset: 0x000CEF10
	private static string MbConvertKana_n(string str)
	{
		string text = "０１２３４５６７８９";
		string text2 = "0123456789";
		char[] array = text.ToCharArray();
		char[] array2 = text2.ToCharArray();
		if (array.Length == array2.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				str = str.Replace(array[i], array2[i]);
			}
		}
		return str;
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x000D0D6C File Offset: 0x000CEF6C
	private static string MbConvertKana_K(string str)
	{
		string text = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン゛゜ァィゥェォャュョッ";
		string text2 = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝﾞﾟｧｨｩｪｫｬｭｮｯ";
		char[] array = text.ToCharArray();
		char[] array2 = text2.ToCharArray();
		if (array.Length == array2.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				str = str.Replace(array2[i], array[i]);
			}
		}
		return str;
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000D0DC8 File Offset: 0x000CEFC8
	private static string MbConvertKana_C(string str)
	{
		string text = "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろゎわゐゑをん";
		string text2 = "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロヮワヰヱヲン";
		char[] array = text.ToCharArray();
		char[] array2 = text2.ToCharArray();
		if (array.Length == array2.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				str = str.Replace(array[i], array2[i]);
			}
		}
		return str;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x000D0E24 File Offset: 0x000CF024
	private static string MbConvertKana_V(string str)
	{
		str = NGWordCheck.PregReplace("゛+", "゛", str);
		str = NGWordCheck.PregReplace("゜+", "゜", str);
		bool flag;
		do
		{
			flag = false;
			string str2 = str;
			str = NGWordCheck.MbConvertKana_V_bottom(str2, ref flag);
		}
		while (flag);
		return str;
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x000D0E70 File Offset: 0x000CF070
	private static string MbConvertKana_V_bottom(string str, ref bool change)
	{
		string text = "ウヴカガキギクグケゲコゴサザシジスズセゼソゾタダチヂツヅテデトドハバヒビフブヘベホボ";
		string text2 = "ヴヴガガギギググゲゲゴゴザザジジズズゼゼゾゾダダヂヂヅヅデデドドババビビブブベベボボ";
		char[] array = text2.ToCharArray();
		string text3 = "ハパヒピフプヘペホポ";
		string text4 = "パパピピププペペポポ";
		char[] array2 = text4.ToCharArray();
		change = false;
		if (str != null && str.Length > 0)
		{
			char[] array3 = str.ToCharArray();
			int num = str.Length - 1;
			for (int i = num; i >= 0; i--)
			{
				char c = array3[i];
				if (c == '゛')
				{
					int num2 = i - 1;
					if (num2 >= 0)
					{
						char value = array3[num2];
						int num3 = text.IndexOf(value, 0);
						if (num3 != -1 && num3 < array.Length)
						{
							string ptn = value.ToString() + c.ToString();
							change = true;
							return NGWordCheck.PregReplace(ptn, array[num3].ToString(), str);
						}
					}
				}
				else if (c == '゜')
				{
					int num4 = i - 1;
					if (num4 >= 0)
					{
						char value2 = array3[num4];
						int num5 = text3.IndexOf(value2, 0);
						if (num5 != -1 && num5 < array2.Length)
						{
							string ptn2 = value2.ToString() + c.ToString();
							change = true;
							return NGWordCheck.PregReplace(ptn2, array2[num5].ToString(), str);
						}
					}
				}
			}
		}
		return str;
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x000D0FCC File Offset: 0x000CF1CC
	private static string replaceSpecialString(string value)
	{
		string[] ptn = new string[]
		{
			"ー",
			"×",
			"○",
			"！",
			"．",
			"ー"
		};
		string[] newStr = new string[]
		{
			"-",
			"x",
			"0",
			"!",
			".",
			"-"
		};
		return NGWordCheck.PregReplace(ptn, newStr, value);
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x000D1050 File Offset: 0x000CF250
	private static string str2UpperKana(string value)
	{
		string[] ptn = new string[]
		{
			"ァ",
			"ィ",
			"ゥ",
			"ェ",
			"ォ",
			"ッ",
			"ャ",
			"ュ",
			"ョ",
			"ヮ",
			"ヵ",
			"ヶ"
		};
		string[] newStr = new string[]
		{
			"ア",
			"イ",
			"ウ",
			"エ",
			"オ",
			"ツ",
			"ヤ",
			"ユ",
			"ヨ",
			"ワ",
			"カ",
			"ケ"
		};
		return NGWordCheck.PregReplace(ptn, newStr, value);
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x000D113C File Offset: 0x000CF33C
	private static string StrToLower(string str)
	{
		return str.ToLower();
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x000D1144 File Offset: 0x000CF344
	private static string StrReplace(string oldWord, string newWord, string str, ref int count)
	{
		string[] separator = new string[]
		{
			oldWord
		};
		string[] array = str.Split(separator, StringSplitOptions.None);
		count = array.Length - 1;
		return NGWordCheck.PregReplace(oldWord, newWord, str);
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x000D1174 File Offset: 0x000CF374
	private static string PregReplace(string ptn, string newStr, string str)
	{
		return Regex.Replace(str, ptn, newStr);
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x000D1180 File Offset: 0x000CF380
	private static string PregReplace(string[] ptn, string[] newStr, string str)
	{
		if (ptn.Length == newStr.Length)
		{
			for (int i = 0; i < ptn.Length; i++)
			{
				str = NGWordCheck.PregReplace(ptn[i], newStr[i], str);
			}
		}
		return str;
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x000D11BC File Offset: 0x000CF3BC
	private static bool PregMatch(string ptn, string str)
	{
		return Regex.IsMatch(str, ptn);
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x000D11D0 File Offset: 0x000CF3D0
	private static void DebugDrawLocal(string str)
	{
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x000D11D4 File Offset: 0x000CF3D4
	private static void ErrorDraw(string str)
	{
	}

	// Token: 0x04001F65 RID: 8037
	private static List<NGWordData> m_wordData = new List<NGWordData>();

	// Token: 0x04001F66 RID: 8038
	private static bool m_debugDrawLocal = false;

	// Token: 0x04001F67 RID: 8039
	private static bool m_errorDraw = true;

	// Token: 0x04001F68 RID: 8040
	private static ResourceSceneLoader m_sceneLoader = null;
}
