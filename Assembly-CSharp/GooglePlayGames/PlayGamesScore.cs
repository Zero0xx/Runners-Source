using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x0200002F RID: 47
	public class PlayGamesScore : IScore
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00005824 File Offset: 0x00003A24
		public void ReportScore(Action<bool> callback)
		{
			PlayGamesPlatform.Instance.ReportScore(this.mValue, this.mLbId, callback);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005840 File Offset: 0x00003A40
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005848 File Offset: 0x00003A48
		public string leaderboardID
		{
			get
			{
				return this.mLbId;
			}
			set
			{
				this.mLbId = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005854 File Offset: 0x00003A54
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000585C File Offset: 0x00003A5C
		public long value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005868 File Offset: 0x00003A68
		public DateTime date
		{
			get
			{
				return new DateTime(1970, 1, 1, 0, 0, 0);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000587C File Offset: 0x00003A7C
		public string formattedValue
		{
			get
			{
				return this.mValue.ToString();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000588C File Offset: 0x00003A8C
		public string userID
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005894 File Offset: 0x00003A94
		public int rank
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000086 RID: 134
		private string mLbId;

		// Token: 0x04000087 RID: 135
		private long mValue;
	}
}
