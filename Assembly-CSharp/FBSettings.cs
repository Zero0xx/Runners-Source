using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class FBSettings : ScriptableObject
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000089 RID: 137 RVA: 0x00003BC0 File Offset: 0x00001DC0
	private static FBSettings Instance
	{
		get
		{
			if (FBSettings.instance == null)
			{
				FBSettings.instance = (Resources.Load("FacebookSettings") as FBSettings);
				if (FBSettings.instance == null)
				{
					FBSettings.instance = ScriptableObject.CreateInstance<FBSettings>();
				}
			}
			return FBSettings.instance;
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00003C10 File Offset: 0x00001E10
	public void SetAppIndex(int index)
	{
		if (this.selectedAppIndex != index)
		{
			this.selectedAppIndex = index;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600008B RID: 139 RVA: 0x00003C2C File Offset: 0x00001E2C
	public int SelectedAppIndex
	{
		get
		{
			return this.selectedAppIndex;
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003C34 File Offset: 0x00001E34
	public void SetAppId(int index, string value)
	{
		if (this.appIds[index] != value)
		{
			this.appIds[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600008D RID: 141 RVA: 0x00003C58 File Offset: 0x00001E58
	// (set) Token: 0x0600008E RID: 142 RVA: 0x00003C60 File Offset: 0x00001E60
	public string[] AppIds
	{
		get
		{
			return this.appIds;
		}
		set
		{
			if (this.appIds != value)
			{
				this.appIds = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00003C7C File Offset: 0x00001E7C
	public void SetAppLabel(int index, string value)
	{
		if (this.appLabels[index] != value)
		{
			this.AppLabels[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000090 RID: 144 RVA: 0x00003CA0 File Offset: 0x00001EA0
	// (set) Token: 0x06000091 RID: 145 RVA: 0x00003CA8 File Offset: 0x00001EA8
	public string[] AppLabels
	{
		get
		{
			return this.appLabels;
		}
		set
		{
			if (this.appLabels != value)
			{
				this.appLabels = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000092 RID: 146 RVA: 0x00003CC4 File Offset: 0x00001EC4
	public static string[] AllAppIds
	{
		get
		{
			return FBSettings.Instance.AppIds;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000093 RID: 147 RVA: 0x00003CD0 File Offset: 0x00001ED0
	public static string AppId
	{
		get
		{
			return FBSettings.Instance.AppIds[FBSettings.Instance.SelectedAppIndex];
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000094 RID: 148 RVA: 0x00003CF4 File Offset: 0x00001EF4
	public static bool IsValidAppId
	{
		get
		{
			return FBSettings.AppId != null && FBSettings.AppId.Length > 0 && !FBSettings.AppId.Equals("0");
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D30 File Offset: 0x00001F30
	// (set) Token: 0x06000096 RID: 150 RVA: 0x00003D3C File Offset: 0x00001F3C
	public static bool Cookie
	{
		get
		{
			return FBSettings.Instance.cookie;
		}
		set
		{
			if (FBSettings.Instance.cookie != value)
			{
				FBSettings.Instance.cookie = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000097 RID: 151 RVA: 0x00003D6C File Offset: 0x00001F6C
	// (set) Token: 0x06000098 RID: 152 RVA: 0x00003D78 File Offset: 0x00001F78
	public static bool Logging
	{
		get
		{
			return FBSettings.Instance.logging;
		}
		set
		{
			if (FBSettings.Instance.logging != value)
			{
				FBSettings.Instance.logging = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000099 RID: 153 RVA: 0x00003DA8 File Offset: 0x00001FA8
	// (set) Token: 0x0600009A RID: 154 RVA: 0x00003DB4 File Offset: 0x00001FB4
	public static bool Status
	{
		get
		{
			return FBSettings.Instance.status;
		}
		set
		{
			if (FBSettings.Instance.status != value)
			{
				FBSettings.Instance.status = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600009B RID: 155 RVA: 0x00003DE4 File Offset: 0x00001FE4
	// (set) Token: 0x0600009C RID: 156 RVA: 0x00003DF0 File Offset: 0x00001FF0
	public static bool Xfbml
	{
		get
		{
			return FBSettings.Instance.xfbml;
		}
		set
		{
			if (FBSettings.Instance.xfbml != value)
			{
				FBSettings.Instance.xfbml = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600009D RID: 157 RVA: 0x00003E20 File Offset: 0x00002020
	// (set) Token: 0x0600009E RID: 158 RVA: 0x00003E2C File Offset: 0x0000202C
	public static string IosURLSuffix
	{
		get
		{
			return FBSettings.Instance.iosURLSuffix;
		}
		set
		{
			if (FBSettings.Instance.iosURLSuffix != value)
			{
				FBSettings.Instance.iosURLSuffix = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600009F RID: 159 RVA: 0x00003E54 File Offset: 0x00002054
	public static string ChannelUrl
	{
		get
		{
			return "/channel.html";
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003E5C File Offset: 0x0000205C
	// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003E68 File Offset: 0x00002068
	public static bool FrictionlessRequests
	{
		get
		{
			return FBSettings.Instance.frictionlessRequests;
		}
		set
		{
			if (FBSettings.Instance.frictionlessRequests != value)
			{
				FBSettings.Instance.frictionlessRequests = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00003E98 File Offset: 0x00002098
	private static void DirtyEditor()
	{
	}

	// Token: 0x04000021 RID: 33
	private const string facebookSettingsAssetName = "FacebookSettings";

	// Token: 0x04000022 RID: 34
	private const string facebookSettingsPath = "Facebook/Resources";

	// Token: 0x04000023 RID: 35
	private const string facebookSettingsAssetExtension = ".asset";

	// Token: 0x04000024 RID: 36
	private static FBSettings instance;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private int selectedAppIndex;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	private string[] appIds = new string[]
	{
		"0"
	};

	// Token: 0x04000027 RID: 39
	[SerializeField]
	private string[] appLabels = new string[]
	{
		"App Name"
	};

	// Token: 0x04000028 RID: 40
	[SerializeField]
	private bool cookie = true;

	// Token: 0x04000029 RID: 41
	[SerializeField]
	private bool logging = true;

	// Token: 0x0400002A RID: 42
	[SerializeField]
	private bool status = true;

	// Token: 0x0400002B RID: 43
	[SerializeField]
	private bool xfbml;

	// Token: 0x0400002C RID: 44
	[SerializeField]
	private bool frictionlessRequests = true;

	// Token: 0x0400002D RID: 45
	[SerializeField]
	private string iosURLSuffix = string.Empty;
}
