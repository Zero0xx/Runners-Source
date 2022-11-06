using System;
using App;
using SaveData;

// Token: 0x02000693 RID: 1683
internal class NetBaseUtil
{
	// Token: 0x170005D5 RID: 1493
	// (get) Token: 0x06002D3E RID: 11582 RVA: 0x0010F378 File Offset: 0x0010D578
	// (set) Token: 0x06002D3F RID: 11583 RVA: 0x0010F380 File Offset: 0x0010D580
	public static string DebugServerUrl
	{
		get
		{
			return NetBaseUtil.m_debugServerUrl;
		}
		set
		{
			NetBaseUtil.m_debugServerUrl = value;
		}
	}

	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x06002D40 RID: 11584 RVA: 0x0010F388 File Offset: 0x0010D588
	// (set) Token: 0x06002D41 RID: 11585 RVA: 0x0010F398 File Offset: 0x0010D598
	public static bool IsDebugServer
	{
		get
		{
			return NetBaseUtil.m_debugServerUrl != null;
		}
		private set
		{
		}
	}

	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x06002D42 RID: 11586 RVA: 0x0010F39C File Offset: 0x0010D59C
	public static string ActionServerURL
	{
		get
		{
			if (NetBaseUtil.IsDebugServer)
			{
				return NetBaseUtil.DebugServerUrl;
			}
			return NetBaseUtil.mActionServerUrlTable[(int)Env.actionServerType];
		}
	}

	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x06002D43 RID: 11587 RVA: 0x0010F3BC File Offset: 0x0010D5BC
	public static string SecureActionServerURL
	{
		get
		{
			if (NetBaseUtil.IsDebugServer)
			{
				return NetBaseUtil.DebugServerUrl;
			}
			return NetBaseUtil.mSecureActionServerUrlTable[(int)Env.actionServerType];
		}
	}

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x06002D44 RID: 11588 RVA: 0x0010F3DC File Offset: 0x0010D5DC
	public static string ServerTypeString
	{
		get
		{
			if (NetBaseUtil.IsDebugServer)
			{
				return "i";
			}
			return NetBaseUtil.mServerTypeStringTable[(int)Env.actionServerType];
		}
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x0010F3FC File Offset: 0x0010D5FC
	public static void SetAssetServerURL()
	{
		ServerLoginState loginState = ServerInterface.LoginState;
		string text = string.Empty;
		if (loginState != null)
		{
			text = loginState.AssetsVersionString;
		}
		string text2 = NetBaseUtil.mAssetURLTable[(int)Env.actionServerType];
		if (text != string.Empty)
		{
			NetBaseUtil.m_assetServerVersionUrl = string.Concat(new string[]
			{
				text2,
				CurrentBundleVersion.version,
				"_",
				text,
				"/"
			});
		}
		else
		{
			NetBaseUtil.m_assetServerVersionUrl = text2;
		}
		if (SystemSaveManager.GetSystemSaveData() != null && SystemSaveManager.GetSystemSaveData().highTexture)
		{
			NetBaseUtil.m_assetServerVersionUrl += "tablet/";
		}
	}

	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x06002D46 RID: 11590 RVA: 0x0010F4A8 File Offset: 0x0010D6A8
	public static string AssetServerURL
	{
		get
		{
			if (NetBaseUtil.m_assetServerVersionUrl != string.Empty)
			{
				return NetBaseUtil.m_assetServerVersionUrl;
			}
			return NetBaseUtil.mAssetURLTable[(int)Env.actionServerType];
		}
	}

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x06002D47 RID: 11591 RVA: 0x0010F4D0 File Offset: 0x0010D6D0
	public static string InformationServerURL
	{
		get
		{
			ServerLoginState loginState = ServerInterface.LoginState;
			string text = string.Empty;
			if (loginState != null)
			{
				text = loginState.InfoVersionString;
			}
			string text2 = NetBaseUtil.mInformationURLTable[(int)Env.actionServerType];
			if (text != string.Empty)
			{
				return text2 + text + "/";
			}
			return text2;
		}
	}

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x06002D48 RID: 11592 RVA: 0x0010F520 File Offset: 0x0010D720
	// (set) Token: 0x06002D49 RID: 11593 RVA: 0x0010F528 File Offset: 0x0010D728
	public static string RedirectInstallPageUrl
	{
		get
		{
			return NetBaseUtil.mRedirectInstallPageUrl;
		}
		private set
		{
		}
	}

	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x06002D4A RID: 11594 RVA: 0x0010F52C File Offset: 0x0010D72C
	public static string RedirectTrmsOfServicePageUrlForTitle
	{
		get
		{
			if (Env.language == Env.Language.JAPANESE)
			{
				return "http://sonicrunners.sega-net.com/jp/rule/";
			}
			return "http://www.sega.com/legal";
		}
	}

	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x06002D4B RID: 11595 RVA: 0x0010F544 File Offset: 0x0010D744
	public static string RedirectTrmsOfServicePageUrl
	{
		get
		{
			if (RegionManager.Instance != null && RegionManager.Instance.IsJapan())
			{
				return "http://sonicrunners.sega-net.com/jp/rule/";
			}
			return "http://sonicrunners.sega-net.com/rule.html";
		}
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x0010F57C File Offset: 0x0010D77C
	public static int GetVersionValue(string versionString, int scaleOffset)
	{
		string[] array = versionString.Split(new char[]
		{
			'.'
		});
		int num = array.Length;
		int[] array2 = new int[num];
		bool flag = true;
		for (int i = 0; i < num; i++)
		{
			if (!int.TryParse(array[i], out array2[i]))
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			int num2 = 0;
			int num3 = 1;
			for (int j = num - 1; j >= 0; j--)
			{
				num2 += array2[j] * num3;
				num3 *= scaleOffset;
			}
			return num2;
		}
		return -1;
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x0010F618 File Offset: 0x0010D818
	public static int GetVersionValue(string versionString)
	{
		return NetBaseUtil.GetVersionValue(versionString, 10);
	}

	// Token: 0x040029B4 RID: 10676
	private static string m_debugServerUrl = null;

	// Token: 0x040029B5 RID: 10677
	private static string m_assetServerVersionUrl = string.Empty;

	// Token: 0x040029B6 RID: 10678
	private static string[] mActionServerUrlTable = new string[]
	{
		"http://157.109.83.27/sdl/",
		"http://157.109.82.105/sdl/",
		"http://157.109.82.106/sdl/",
		"http://157.109.83.127/sdl/",
		"http://157.109.82.108/sdl/",
		"http://216.98.105.183/sdl/",
		"http://216.98.105.184/sdl/",
		"http://216.98.105.185/sdl/",
		"http://157.109.115.83/sdl/",
		"http://216.98.105.168/sdl/",
		"http://216.98.105.177/sdl/"
	};

	// Token: 0x040029B7 RID: 10679
	private static string[] mSecureActionServerUrlTable = new string[]
	{
		"https://157.109.83.27/sdl/",
		"https://157.109.82.105/sdl/",
		"https://157.109.82.106/sdl/",
		"https://157.109.83.127/sdl/",
		"https://157.109.82.108/sdl/",
		"https://216.98.105.183/sdl/",
		"https://216.98.105.184/sdl/",
		"https://216.98.105.185/sdl/",
		"https://157.109.115.83/sdl/",
		"https://216.98.105.168/sdl/",
		"http://216.98.105.177/sdl/"
	};

	// Token: 0x040029B8 RID: 10680
	private static string[] mServerTypeStringTable = new string[]
	{
		"_L1",
		"_L2",
		"_L3",
		"_L4",
		"_L5",
		"_D1",
		"_D2",
		"_D3",
		"_S",
		string.Empty,
		"a"
	};

	// Token: 0x040029B9 RID: 10681
	private static string[] mAssetURLTable = new string[]
	{
		"http://157.109.83.27/assets/",
		"http://157.109.83.27/assets/",
		"http://157.109.83.27/assets/",
		"http://157.109.83.27/assets/",
		"http://157.109.83.27/assets/",
		"http://216.98.105.183/assets/",
		"http://216.98.105.184/assets/",
		"http://216.98.105.185/assets/",
		"http://157.109.115.83/assets/",
		"http://dl.sega-pc.com/srn/app/assets/",
		"http://216.98.105.177/assets/"
	};

	// Token: 0x040029BA RID: 10682
	private static string[] mInformationURLTable = new string[]
	{
		"http://157.109.83.27/information/",
		"http://157.109.83.27/information/",
		"http://157.109.83.27/information/",
		"http://157.109.83.27/information/",
		"http://157.109.83.27/information/",
		"http://216.98.105.183/information/",
		"http://216.98.105.184/information/",
		"http://216.98.105.185/information/",
		"http://157.109.115.83/0.0.5_001/information/",
		"http://dl.sega-pc.com/srn/app/information/",
		"http://216.98.105.177/information/"
	};

	// Token: 0x040029BB RID: 10683
	private static string mRedirectInstallPageUrl = "https://play.google.com/store/apps/details?id=com.sega.sonicrunners";
}
