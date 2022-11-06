using System;

namespace App
{
	// Token: 0x02000202 RID: 514
	public class Env
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00050158 File Offset: 0x0004E358
		public static bool useAssetBundle
		{
			get
			{
				return Env.m_useAssetBundle;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00050160 File Offset: 0x0004E360
		public static bool isReleaseApplication
		{
			get
			{
				return Env.m_releaseApplication;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00050168 File Offset: 0x0004E368
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00050170 File Offset: 0x0004E370
		public static Env.Region region
		{
			get
			{
				return Env.mRegion;
			}
			set
			{
				Env.mRegion = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00050178 File Offset: 0x0004E378
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00050180 File Offset: 0x0004E380
		public static Env.Language language
		{
			get
			{
				return Env.mLanguage;
			}
			set
			{
				Env.mLanguage = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00050188 File Offset: 0x0004E388
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00050190 File Offset: 0x0004E390
		public static Env.ActionServerType actionServerType
		{
			get
			{
				return Env.mActionServerType;
			}
			set
			{
				Env.mActionServerType = value;
			}
		}

		// Token: 0x04000B87 RID: 2951
		public const bool isDebug = false;

		// Token: 0x04000B88 RID: 2952
		public const bool isDebugFont = false;

		// Token: 0x04000B89 RID: 2953
		public const bool forDevelop = false;

		// Token: 0x04000B8A RID: 2954
		private static readonly bool m_useAssetBundle = true;

		// Token: 0x04000B8B RID: 2955
		private static readonly bool m_releaseApplication;

		// Token: 0x04000B8C RID: 2956
		private static Env.Region mRegion;

		// Token: 0x04000B8D RID: 2957
		private static Env.Language mLanguage;

		// Token: 0x04000B8E RID: 2958
		private static Env.ActionServerType mActionServerType = Env.ActionServerType.RELEASE;

		// Token: 0x02000203 RID: 515
		public enum Region
		{
			// Token: 0x04000B90 RID: 2960
			JAPAN,
			// Token: 0x04000B91 RID: 2961
			WORLDWIDE
		}

		// Token: 0x02000204 RID: 516
		public enum Language
		{
			// Token: 0x04000B93 RID: 2963
			JAPANESE,
			// Token: 0x04000B94 RID: 2964
			ENGLISH,
			// Token: 0x04000B95 RID: 2965
			CHINESE_ZHJ,
			// Token: 0x04000B96 RID: 2966
			CHINESE_ZH,
			// Token: 0x04000B97 RID: 2967
			KOREAN,
			// Token: 0x04000B98 RID: 2968
			FRENCH,
			// Token: 0x04000B99 RID: 2969
			GERMAN,
			// Token: 0x04000B9A RID: 2970
			SPANISH,
			// Token: 0x04000B9B RID: 2971
			PORTUGUESE,
			// Token: 0x04000B9C RID: 2972
			ITALIAN,
			// Token: 0x04000B9D RID: 2973
			RUSSIAN
		}

		// Token: 0x02000205 RID: 517
		public enum ActionServerType
		{
			// Token: 0x04000B9F RID: 2975
			LOCAL1,
			// Token: 0x04000BA0 RID: 2976
			LOCAL2,
			// Token: 0x04000BA1 RID: 2977
			LOCAL3,
			// Token: 0x04000BA2 RID: 2978
			LOCAL4,
			// Token: 0x04000BA3 RID: 2979
			LOCAL5,
			// Token: 0x04000BA4 RID: 2980
			DEVELOP,
			// Token: 0x04000BA5 RID: 2981
			DEVELOP2,
			// Token: 0x04000BA6 RID: 2982
			DEVELOP3,
			// Token: 0x04000BA7 RID: 2983
			STAGING,
			// Token: 0x04000BA8 RID: 2984
			RELEASE,
			// Token: 0x04000BA9 RID: 2985
			APPLICATION
		}
	}
}
