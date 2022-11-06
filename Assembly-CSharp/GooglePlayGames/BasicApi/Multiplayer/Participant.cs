using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000024 RID: 36
	public class Participant : IComparable<Participant>
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00004984 File Offset: 0x00002B84
		internal Participant(string displayName, string participantId, Participant.ParticipantStatus status, Player player, bool connectedToRoom)
		{
			this.mDisplayName = displayName;
			this.mParticipantId = participantId;
			this.mStatus = status;
			this.mPlayer = player;
			this.mIsConnectedToRoom = connectedToRoom;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000049DC File Offset: 0x00002BDC
		public string DisplayName
		{
			get
			{
				return this.mDisplayName;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000049E4 File Offset: 0x00002BE4
		public string ParticipantId
		{
			get
			{
				return this.mParticipantId;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000049EC File Offset: 0x00002BEC
		public Participant.ParticipantStatus Status
		{
			get
			{
				return this.mStatus;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000049F4 File Offset: 0x00002BF4
		public Player Player
		{
			get
			{
				return this.mPlayer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000049FC File Offset: 0x00002BFC
		public bool IsConnectedToRoom
		{
			get
			{
				return this.mIsConnectedToRoom;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004A04 File Offset: 0x00002C04
		public bool IsAutomatch
		{
			get
			{
				return this.mPlayer == null;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004A10 File Offset: 0x00002C10
		public override string ToString()
		{
			return string.Format("[Participant: '{0}' (id {1}), status={2}, player={3}, connected={4}]", new object[]
			{
				this.mDisplayName,
				this.mParticipantId,
				this.mStatus.ToString(),
				(this.mPlayer != null) ? this.mPlayer.ToString() : "NULL",
				this.mIsConnectedToRoom
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004A84 File Offset: 0x00002C84
		public int CompareTo(Participant other)
		{
			return this.mParticipantId.CompareTo(other.mParticipantId);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004A98 File Offset: 0x00002C98
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(Participant))
			{
				return false;
			}
			Participant participant = (Participant)obj;
			return this.mParticipantId.Equals(participant.mParticipantId);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004AEC File Offset: 0x00002CEC
		public override int GetHashCode()
		{
			return (this.mParticipantId == null) ? 0 : this.mParticipantId.GetHashCode();
		}

		// Token: 0x04000055 RID: 85
		private string mDisplayName = string.Empty;

		// Token: 0x04000056 RID: 86
		private string mParticipantId = string.Empty;

		// Token: 0x04000057 RID: 87
		private Participant.ParticipantStatus mStatus = Participant.ParticipantStatus.Unknown;

		// Token: 0x04000058 RID: 88
		private Player mPlayer;

		// Token: 0x04000059 RID: 89
		private bool mIsConnectedToRoom;

		// Token: 0x02000025 RID: 37
		public enum ParticipantStatus
		{
			// Token: 0x0400005B RID: 91
			NotInvitedYet,
			// Token: 0x0400005C RID: 92
			Invited,
			// Token: 0x0400005D RID: 93
			Joined,
			// Token: 0x0400005E RID: 94
			Declined,
			// Token: 0x0400005F RID: 95
			Left,
			// Token: 0x04000060 RID: 96
			Finished,
			// Token: 0x04000061 RID: 97
			Unresponsive,
			// Token: 0x04000062 RID: 98
			Unknown
		}
	}
}
