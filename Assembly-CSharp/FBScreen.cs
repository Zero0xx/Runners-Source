using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class FBScreen
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000076 RID: 118 RVA: 0x00003A88 File Offset: 0x00001C88
	// (set) Token: 0x06000077 RID: 119 RVA: 0x00003A90 File Offset: 0x00001C90
	public static bool FullScreen
	{
		get
		{
			return Screen.fullScreen;
		}
		set
		{
			Screen.fullScreen = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000078 RID: 120 RVA: 0x00003A98 File Offset: 0x00001C98
	public static bool Resizable
	{
		get
		{
			return FBScreen.resizable;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000079 RID: 121 RVA: 0x00003AA0 File Offset: 0x00001CA0
	public static int Width
	{
		get
		{
			return Screen.width;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600007A RID: 122 RVA: 0x00003AA8 File Offset: 0x00001CA8
	public static int Height
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003AB0 File Offset: 0x00001CB0
	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003ABC File Offset: 0x00001CBC
	public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
	{
		int width2 = Screen.height / height * width;
		Screen.SetResolution(width2, Screen.height, Screen.fullScreen);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003AE4 File Offset: 0x00001CE4
	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003AE8 File Offset: 0x00001CE8
	public static FBScreen.Layout.OptionLeft Left(float amount)
	{
		return new FBScreen.Layout.OptionLeft
		{
			Amount = amount
		};
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003B04 File Offset: 0x00001D04
	public static FBScreen.Layout.OptionTop Top(float amount)
	{
		return new FBScreen.Layout.OptionTop
		{
			Amount = amount
		};
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003B20 File Offset: 0x00001D20
	public static FBScreen.Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new FBScreen.Layout.OptionCenterHorizontal();
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003B28 File Offset: 0x00001D28
	public static FBScreen.Layout.OptionCenterVertical CenterVertical()
	{
		return new FBScreen.Layout.OptionCenterVertical();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003B30 File Offset: 0x00001D30
	private static void SetLayout(IEnumerable<FBScreen.Layout> parameters)
	{
	}

	// Token: 0x0400001E RID: 30
	private static bool resizable;

	// Token: 0x02000010 RID: 16
	public class Layout
	{
		// Token: 0x02000011 RID: 17
		public class OptionLeft : FBScreen.Layout
		{
			// Token: 0x0400001F RID: 31
			public float Amount;
		}

		// Token: 0x02000012 RID: 18
		public class OptionTop : FBScreen.Layout
		{
			// Token: 0x04000020 RID: 32
			public float Amount;
		}

		// Token: 0x02000013 RID: 19
		public class OptionCenterHorizontal : FBScreen.Layout
		{
		}

		// Token: 0x02000014 RID: 20
		public class OptionCenterVertical : FBScreen.Layout
		{
		}
	}
}
