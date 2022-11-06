using System;
using App;
using UnityEngine;

// Token: 0x02000A49 RID: 2633
public class Language
{
	// Token: 0x0600465B RID: 18011 RVA: 0x0016F52C File Offset: 0x0016D72C
	public static string GetLocalLanguage()
	{
		string result;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("java.util.Locale"))
		{
			using (AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getDefault", new object[0]))
			{
				string text = androidJavaObject.Call<string>("getLanguage", new object[0]);
				result = text;
			}
		}
		return result;
	}

	// Token: 0x0600465C RID: 18012 RVA: 0x0016F5C8 File Offset: 0x0016D7C8
	private static string GetLocale()
	{
		string result;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("java.util.Locale"))
		{
			using (AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getDefault", new object[0]))
			{
				result = androidJavaObject.Call<string>("toString", new object[0]);
			}
		}
		return result;
	}

	// Token: 0x0600465D RID: 18013 RVA: 0x0016F664 File Offset: 0x0016D864
	private static void SetEditorLanguage()
	{
	}

	// Token: 0x0600465E RID: 18014 RVA: 0x0016F668 File Offset: 0x0016D868
	private static void SetIPhoneLanguage()
	{
		SystemLanguage systemLanguage = Application.systemLanguage;
		switch (systemLanguage)
		{
		case SystemLanguage.Italian:
			Env.language = Env.Language.ITALIAN;
			break;
		case SystemLanguage.Japanese:
			Env.language = Env.Language.JAPANESE;
			break;
		case SystemLanguage.Korean:
			Env.language = Env.Language.KOREAN;
			break;
		default:
			switch (systemLanguage)
			{
			case SystemLanguage.English:
				Env.language = Env.Language.ENGLISH;
				break;
			default:
				if (systemLanguage != SystemLanguage.Chinese)
				{
					if (systemLanguage != SystemLanguage.Spanish)
					{
						Env.language = Env.Language.ENGLISH;
					}
					else
					{
						Env.language = Env.Language.SPANISH;
					}
				}
				else
				{
					string localLanguage = Language.GetLocalLanguage();
					global::Debug.Log("Language.InitAppEnvLanguage() GetLocalLanguage = " + localLanguage);
					if (localLanguage.Contains("zh-Hans"))
					{
						Env.language = Env.Language.CHINESE_ZHJ;
					}
					else
					{
						Env.language = Env.Language.CHINESE_ZH;
					}
				}
				break;
			case SystemLanguage.French:
				Env.language = Env.Language.FRENCH;
				break;
			case SystemLanguage.German:
				Env.language = Env.Language.GERMAN;
				break;
			}
			break;
		case SystemLanguage.Portuguese:
			Env.language = Env.Language.PORTUGUESE;
			break;
		case SystemLanguage.Russian:
			Env.language = Env.Language.RUSSIAN;
			break;
		}
	}

	// Token: 0x0600465F RID: 18015 RVA: 0x0016F790 File Offset: 0x0016D990
	private static void SetAndroidLanguage()
	{
		string localLanguage = Language.GetLocalLanguage();
		if (localLanguage == "ja")
		{
			Env.language = Env.Language.JAPANESE;
		}
		else if (localLanguage == "en")
		{
			Env.language = Env.Language.ENGLISH;
		}
		else if (localLanguage == "de")
		{
			Env.language = Env.Language.GERMAN;
		}
		else if (localLanguage == "es")
		{
			Env.language = Env.Language.SPANISH;
		}
		else if (localLanguage == "fr")
		{
			Env.language = Env.Language.FRENCH;
		}
		else if (localLanguage == "it")
		{
			Env.language = Env.Language.ITALIAN;
		}
		else if (localLanguage == "ko")
		{
			Env.language = Env.Language.KOREAN;
		}
		else if (localLanguage == "pt")
		{
			Env.language = Env.Language.PORTUGUESE;
		}
		else if (localLanguage == "ru")
		{
			Env.language = Env.Language.RUSSIAN;
		}
		else if (localLanguage == "zh")
		{
			Env.language = Env.Language.CHINESE_ZHJ;
			string locale = Language.GetLocale();
			if (locale != null && locale == "zh_TW")
			{
				Env.language = Env.Language.CHINESE_ZH;
			}
		}
		else
		{
			Env.language = Env.Language.ENGLISH;
		}
	}

	// Token: 0x06004660 RID: 18016 RVA: 0x0016F8DC File Offset: 0x0016DADC
	public static void InitAppEnvLanguage()
	{
		Language.SetAndroidLanguage();
	}
}
