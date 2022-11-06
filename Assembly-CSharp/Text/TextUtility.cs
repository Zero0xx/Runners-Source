using System;
using System.Collections.Generic;
using App;

namespace Text
{
	// Token: 0x02000A2B RID: 2603
	public class TextUtility
	{
		// Token: 0x060044FA RID: 17658 RVA: 0x00162D14 File Offset: 0x00160F14
		public static string Replaces(string text, Dictionary<string, string> replaceDic)
		{
			if (text == null)
			{
				Debug.LogWarning("TextUtility.Replaces() first argument is null");
				return string.Empty;
			}
			foreach (string text2 in replaceDic.Keys)
			{
				text = text.Replace(text2, replaceDic[text2]);
			}
			return text;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x00162D9C File Offset: 0x00160F9C
		public static string Replace(string text, string srcText, string dstText)
		{
			return TextUtility.Replaces(text, new Dictionary<string, string>
			{
				{
					srcText,
					dstText
				}
			});
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00162DC0 File Offset: 0x00160FC0
		public static void SetText(UILabel label, TextManager.TextType type, string groupID, string cellID)
		{
			if (label != null)
			{
				TextObject text = TextManager.GetText(type, groupID, cellID);
				if (text != null && text.text != null)
				{
					label.text = text.text;
				}
			}
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x00162E00 File Offset: 0x00161000
		public static void SetText(UILabel label, TextManager.TextType type, string groupID, string cellID, string tag, string replace)
		{
			if (label != null)
			{
				TextObject text = TextManager.GetText(type, groupID, cellID);
				if (text != null && text.text != null)
				{
					text.ReplaceTag(tag, replace);
					label.text = text.text;
				}
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x00162E4C File Offset: 0x0016104C
		public static void SetCommonText(UILabel label, string groupID, string cellID)
		{
			TextUtility.SetText(label, TextManager.TextType.TEXTTYPE_COMMON_TEXT, groupID, cellID);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x00162E58 File Offset: 0x00161058
		public static void SetCommonText(UILabel label, string groupID, string cellID, string tag, string replace)
		{
			TextUtility.SetText(label, TextManager.TextType.TEXTTYPE_COMMON_TEXT, groupID, cellID, tag, replace);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00162E68 File Offset: 0x00161068
		public static string GetText(TextManager.TextType type, string groupID, string cellID)
		{
			TextObject text = TextManager.GetText(type, groupID, cellID);
			if (text != null && text.text != null)
			{
				return text.text;
			}
			return null;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x00162E98 File Offset: 0x00161098
		public static string GetText(TextManager.TextType type, string groupID, string cellID, string tag, string replace)
		{
			TextObject text = TextManager.GetText(type, groupID, cellID);
			if (text != null && text.text != null)
			{
				text.ReplaceTag(tag, replace);
				return text.text;
			}
			return null;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x00162ED0 File Offset: 0x001610D0
		public static string GetCommonText(string groupID, string cellID)
		{
			return TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, groupID, cellID);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x00162EDC File Offset: 0x001610DC
		public static string GetCommonText(string groupID, string cellID, string tag, string replace)
		{
			return TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, groupID, cellID, tag, replace);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x00162EE8 File Offset: 0x001610E8
		public static string GetChaoText(string groupID, string cellID)
		{
			return TextUtility.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, groupID, cellID);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x00162EF4 File Offset: 0x001610F4
		public static string GetChaoText(string groupID, string cellID, string tag, string replace)
		{
			return TextUtility.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, groupID, cellID, tag, replace);
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00162F00 File Offset: 0x00161100
		public static string GetXmlLanguageDataType()
		{
			switch (Env.language)
			{
			case Env.Language.JAPANESE:
				return "Japanese";
			case Env.Language.ENGLISH:
				return "English";
			case Env.Language.CHINESE_ZHJ:
				return "SimplifiedChinese";
			case Env.Language.CHINESE_ZH:
				return "TraditionalChinese";
			case Env.Language.KOREAN:
				return "Korean";
			case Env.Language.FRENCH:
				return "French";
			case Env.Language.GERMAN:
				return "German";
			case Env.Language.SPANISH:
				return "Spanish";
			case Env.Language.PORTUGUESE:
				return "Portuguese";
			case Env.Language.ITALIAN:
				return "Italian";
			case Env.Language.RUSSIAN:
				return "Russian";
			default:
				return "English";
			}
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00162F94 File Offset: 0x00161194
		public static string GetSuffixe()
		{
			switch (Env.language)
			{
			case Env.Language.JAPANESE:
				return "ja";
			case Env.Language.ENGLISH:
				return "en";
			case Env.Language.CHINESE_ZHJ:
				return "zhj";
			case Env.Language.CHINESE_ZH:
				return "zh";
			case Env.Language.KOREAN:
				return "ko";
			case Env.Language.FRENCH:
				return "fr";
			case Env.Language.GERMAN:
				return "de";
			case Env.Language.SPANISH:
				return "es";
			case Env.Language.PORTUGUESE:
				return "pt";
			case Env.Language.ITALIAN:
				return "it";
			case Env.Language.RUSSIAN:
				return "ru";
			default:
				return "en";
			}
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00163028 File Offset: 0x00161228
		public static string GetSuffix(Env.Language language)
		{
			switch (language)
			{
			case Env.Language.JAPANESE:
				return "ja";
			case Env.Language.ENGLISH:
				return "en";
			case Env.Language.CHINESE_ZHJ:
				return "zhj";
			case Env.Language.CHINESE_ZH:
				return "zh";
			case Env.Language.KOREAN:
				return "ko";
			case Env.Language.FRENCH:
				return "fr";
			case Env.Language.GERMAN:
				return "de";
			case Env.Language.SPANISH:
				return "es";
			case Env.Language.PORTUGUESE:
				return "pt";
			case Env.Language.ITALIAN:
				return "it";
			case Env.Language.RUSSIAN:
				return "ru";
			default:
				return "en";
			}
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x001630B8 File Offset: 0x001612B8
		public static bool IsSuffix(string languageCode)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(languageCode) && languageCode != null)
			{
				if (TextUtility.<>f__switch$map7 == null)
				{
					TextUtility.<>f__switch$map7 = new Dictionary<string, int>(22)
					{
						{
							"ja",
							0
						},
						{
							"en",
							1
						},
						{
							"de",
							2
						},
						{
							"es",
							3
						},
						{
							"fr",
							4
						},
						{
							"it",
							5
						},
						{
							"ko",
							6
						},
						{
							"pt",
							7
						},
						{
							"ru",
							8
						},
						{
							"zhj",
							9
						},
						{
							"zh",
							10
						},
						{
							"JA",
							11
						},
						{
							"EN",
							12
						},
						{
							"DE",
							13
						},
						{
							"ES",
							14
						},
						{
							"FR",
							15
						},
						{
							"IT",
							16
						},
						{
							"KO",
							17
						},
						{
							"PT",
							18
						},
						{
							"RU",
							19
						},
						{
							"ZHJ",
							20
						},
						{
							"ZH",
							21
						}
					};
				}
				int num;
				if (TextUtility.<>f__switch$map7.TryGetValue(languageCode, out num))
				{
					switch (num)
					{
					case 0:
						result = true;
						break;
					case 1:
						result = true;
						break;
					case 2:
						result = true;
						break;
					case 3:
						result = true;
						break;
					case 4:
						result = true;
						break;
					case 5:
						result = true;
						break;
					case 6:
						result = true;
						break;
					case 7:
						result = true;
						break;
					case 8:
						result = true;
						break;
					case 9:
						result = true;
						break;
					case 10:
						result = true;
						break;
					case 11:
						result = true;
						break;
					case 12:
						result = true;
						break;
					case 13:
						result = true;
						break;
					case 14:
						result = true;
						break;
					case 15:
						result = true;
						break;
					case 16:
						result = true;
						break;
					case 17:
						result = true;
						break;
					case 18:
						result = true;
						break;
					case 19:
						result = true;
						break;
					case 20:
						result = true;
						break;
					case 21:
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00163318 File Offset: 0x00161518
		public static string GetTextLevel(string levelNumber)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
			return TextUtility.Replace(text, "{PARAM}", levelNumber);
		}
	}
}
