using System;
using System.Collections.Generic;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000028 RID: 40
	public class TurnBasedMatch
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004BE0 File Offset: 0x00002DE0
		internal TurnBasedMatch(string matchId, byte[] data, bool canRematch, string selfParticipantId, List<Participant> participants, int availableAutomatchSlots, string pendingParticipantId, TurnBasedMatch.MatchTurnStatus turnStatus, TurnBasedMatch.MatchStatus matchStatus, int variant)
		{
			this.mMatchId = matchId;
			this.mData = data;
			this.mCanRematch = canRematch;
			this.mSelfParticipantId = selfParticipantId;
			this.mParticipants = participants;
			this.mParticipants.Sort();
			this.mAvailableAutomatchSlots = availableAutomatchSlots;
			this.mPendingParticipantId = pendingParticipantId;
			this.mTurnStatus = turnStatus;
			this.mMatchStatus = matchStatus;
			this.mVariant = variant;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004C4C File Offset: 0x00002E4C
		public string MatchId
		{
			get
			{
				return this.mMatchId;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004C54 File Offset: 0x00002E54
		public byte[] Data
		{
			get
			{
				return this.mData;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004C5C File Offset: 0x00002E5C
		public bool CanRematch
		{
			get
			{
				return this.mCanRematch;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004C64 File Offset: 0x00002E64
		public string SelfParticipantId
		{
			get
			{
				return this.mSelfParticipantId;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004C6C File Offset: 0x00002E6C
		public Participant Self
		{
			get
			{
				return this.GetParticipant(this.mSelfParticipantId);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004C7C File Offset: 0x00002E7C
		public Participant GetParticipant(string participantId)
		{
			foreach (Participant participant in this.mParticipants)
			{
				if (participant.ParticipantId.Equals(participantId))
				{
					return participant;
				}
			}
			Logger.w("Participant not found in turn-based match: " + participantId);
			return null;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004D08 File Offset: 0x00002F08
		public List<Participant> Participants
		{
			get
			{
				return this.mParticipants;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004D10 File Offset: 0x00002F10
		public string PendingParticipantId
		{
			get
			{
				return this.mPendingParticipantId;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004D18 File Offset: 0x00002F18
		public Participant PendingParticipant
		{
			get
			{
				return (this.mPendingParticipantId != null) ? this.GetParticipant(this.mPendingParticipantId) : null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004D38 File Offset: 0x00002F38
		public TurnBasedMatch.MatchTurnStatus TurnStatus
		{
			get
			{
				return this.mTurnStatus;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004D40 File Offset: 0x00002F40
		public TurnBasedMatch.MatchStatus Status
		{
			get
			{
				return this.mMatchStatus;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004D48 File Offset: 0x00002F48
		public int Variant
		{
			get
			{
				return this.mVariant;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004D50 File Offset: 0x00002F50
		public int AvailableAutomatchSlots
		{
			get
			{
				return this.mAvailableAutomatchSlots;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004D58 File Offset: 0x00002F58
		public override string ToString()
		{
			return string.Format("[TurnBasedMatch: mMatchId={0}, mData={1}, mCanRematch={2}, mSelfParticipantId={3}, mParticipants={4}, mPendingParticipantId={5}, mTurnStatus={6}, mMatchStatus={7}, mVariant={8}]", new object[]
			{
				this.mMatchId,
				this.mData,
				this.mCanRematch,
				this.mSelfParticipantId,
				this.mParticipants,
				this.mPendingParticipantId,
				this.mTurnStatus,
				this.mMatchStatus,
				this.mVariant
			});
		}

		// Token: 0x04000065 RID: 101
		private string mMatchId;

		// Token: 0x04000066 RID: 102
		private byte[] mData;

		// Token: 0x04000067 RID: 103
		private bool mCanRematch;

		// Token: 0x04000068 RID: 104
		private int mAvailableAutomatchSlots;

		// Token: 0x04000069 RID: 105
		private string mSelfParticipantId;

		// Token: 0x0400006A RID: 106
		private List<Participant> mParticipants;

		// Token: 0x0400006B RID: 107
		private string mPendingParticipantId;

		// Token: 0x0400006C RID: 108
		private TurnBasedMatch.MatchTurnStatus mTurnStatus;

		// Token: 0x0400006D RID: 109
		private TurnBasedMatch.MatchStatus mMatchStatus;

		// Token: 0x0400006E RID: 110
		private int mVariant;

		// Token: 0x02000029 RID: 41
		public enum MatchStatus
		{
			// Token: 0x04000070 RID: 112
			Active,
			// Token: 0x04000071 RID: 113
			AutoMatching,
			// Token: 0x04000072 RID: 114
			Cancelled,
			// Token: 0x04000073 RID: 115
			Complete,
			// Token: 0x04000074 RID: 116
			Expired,
			// Token: 0x04000075 RID: 117
			Unknown,
			// Token: 0x04000076 RID: 118
			Deleted
		}

		// Token: 0x0200002A RID: 42
		public enum MatchTurnStatus
		{
			// Token: 0x04000078 RID: 120
			Complete,
			// Token: 0x04000079 RID: 121
			Invited,
			// Token: 0x0400007A RID: 122
			MyTurn,
			// Token: 0x0400007B RID: 123
			TheirTurn,
			// Token: 0x0400007C RID: 124
			Unknown
		}
	}
}
