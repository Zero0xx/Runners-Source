using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000022 RID: 34
	public class MatchOutcome
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004834 File Offset: 0x00002A34
		public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result, int placement)
		{
			if (!this.mParticipantIds.Contains(participantId))
			{
				this.mParticipantIds.Add(participantId);
			}
			this.mPlacements[participantId] = placement;
			this.mResults[participantId] = result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004878 File Offset: 0x00002A78
		public void SetParticipantResult(string participantId, MatchOutcome.ParticipantResult result)
		{
			this.SetParticipantResult(participantId, result, -1);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004884 File Offset: 0x00002A84
		public void SetParticipantResult(string participantId, int placement)
		{
			this.SetParticipantResult(participantId, MatchOutcome.ParticipantResult.Unset, placement);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004890 File Offset: 0x00002A90
		public List<string> ParticipantIds
		{
			get
			{
				return this.mParticipantIds;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004898 File Offset: 0x00002A98
		public MatchOutcome.ParticipantResult GetResultFor(string participantId)
		{
			return (!this.mResults.ContainsKey(participantId)) ? MatchOutcome.ParticipantResult.Unset : this.mResults[participantId];
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000048C0 File Offset: 0x00002AC0
		public int GetPlacementFor(string participantId)
		{
			return (!this.mPlacements.ContainsKey(participantId)) ? -1 : this.mPlacements[participantId];
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000048E8 File Offset: 0x00002AE8
		public override string ToString()
		{
			string str = "[MatchOutcome";
			foreach (string text in this.mParticipantIds)
			{
				str += string.Format(" {0}->({1},{2})", text, this.GetResultFor(text), this.GetPlacementFor(text));
			}
			return str + "]";
		}

		// Token: 0x0400004B RID: 75
		public const int PlacementUnset = -1;

		// Token: 0x0400004C RID: 76
		private List<string> mParticipantIds = new List<string>();

		// Token: 0x0400004D RID: 77
		private Dictionary<string, int> mPlacements = new Dictionary<string, int>();

		// Token: 0x0400004E RID: 78
		private Dictionary<string, MatchOutcome.ParticipantResult> mResults = new Dictionary<string, MatchOutcome.ParticipantResult>();

		// Token: 0x02000023 RID: 35
		public enum ParticipantResult
		{
			// Token: 0x04000050 RID: 80
			Unset = -1,
			// Token: 0x04000051 RID: 81
			None,
			// Token: 0x04000052 RID: 82
			Win,
			// Token: 0x04000053 RID: 83
			Loss,
			// Token: 0x04000054 RID: 84
			Tie
		}
	}
}
