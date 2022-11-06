using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x0200002C RID: 44
	public class PlayGamesAchievement : IAchievement
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00004DDC File Offset: 0x00002FDC
		internal PlayGamesAchievement()
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E10 File Offset: 0x00003010
		public void ReportProgress(Action<bool> callback)
		{
			PlayGamesPlatform.Instance.ReportProgress(this.mId, this.mPercentComplete, callback);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004E2C File Offset: 0x0000302C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004E34 File Offset: 0x00003034
		public string id
		{
			get
			{
				return this.mId;
			}
			set
			{
				this.mId = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004E40 File Offset: 0x00003040
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004E48 File Offset: 0x00003048
		public double percentCompleted
		{
			get
			{
				return this.mPercentComplete;
			}
			set
			{
				this.mPercentComplete = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004E54 File Offset: 0x00003054
		public bool completed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004E58 File Offset: 0x00003058
		public bool hidden
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004E5C File Offset: 0x0000305C
		public DateTime lastReportedDate
		{
			get
			{
				return this.mLastReportedDate;
			}
		}

		// Token: 0x0400007D RID: 125
		private string mId = string.Empty;

		// Token: 0x0400007E RID: 126
		private double mPercentComplete;

		// Token: 0x0400007F RID: 127
		private DateTime mLastReportedDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
	}
}
