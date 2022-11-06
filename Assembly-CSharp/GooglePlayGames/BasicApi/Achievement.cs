using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x0200001B RID: 27
	public class Achievement
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00004630 File Offset: 0x00002830
		public override string ToString()
		{
			return string.Format("[Achievement] id={0}, name={1}, desc={2}, type={3},  revealed={4}, unlocked={5}, steps={6}/{7}", new object[]
			{
				this.Id,
				this.Name,
				this.Description,
				(!this.IsIncremental) ? "STANDARD" : "INCREMENTAL",
				this.IsRevealed,
				this.IsUnlocked,
				this.CurrentSteps,
				this.TotalSteps
			});
		}

		// Token: 0x0400003B RID: 59
		public string Id = string.Empty;

		// Token: 0x0400003C RID: 60
		public bool IsIncremental;

		// Token: 0x0400003D RID: 61
		public bool IsRevealed;

		// Token: 0x0400003E RID: 62
		public bool IsUnlocked;

		// Token: 0x0400003F RID: 63
		public int CurrentSteps;

		// Token: 0x04000040 RID: 64
		public int TotalSteps;

		// Token: 0x04000041 RID: 65
		public string Description = string.Empty;

		// Token: 0x04000042 RID: 66
		public string Name = string.Empty;
	}
}
