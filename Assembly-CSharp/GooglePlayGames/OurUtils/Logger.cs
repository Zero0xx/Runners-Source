using System;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000031 RID: 49
	public class Logger
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000058D0 File Offset: 0x00003AD0
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000058D8 File Offset: 0x00003AD8
		public static bool DebugLogEnabled
		{
			get
			{
				return Logger.debugLogEnabled;
			}
			set
			{
				Logger.debugLogEnabled = value;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000058E0 File Offset: 0x00003AE0
		public static void d(string msg)
		{
			if (Logger.debugLogEnabled)
			{
				Debug.Log(Logger.LOG_PREF + msg);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000058FC File Offset: 0x00003AFC
		public static void w(string msg)
		{
			Debug.LogWarning("!!! " + Logger.LOG_PREF + " WARNING: " + msg);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005918 File Offset: 0x00003B18
		public static void e(string msg)
		{
			Debug.LogWarning("*** " + Logger.LOG_PREF + " ERROR: " + msg);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005934 File Offset: 0x00003B34
		public static string describe(byte[] b)
		{
			return (b != null) ? ("byte[" + b.Length + "]") : "(null)";
		}

		// Token: 0x04000088 RID: 136
		private static string LOG_PREF = "[Play Games Plugin DLL] ";

		// Token: 0x04000089 RID: 137
		private static bool debugLogEnabled;
	}
}
