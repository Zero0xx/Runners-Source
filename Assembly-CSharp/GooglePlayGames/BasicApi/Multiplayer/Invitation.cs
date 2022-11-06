using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000020 RID: 32
	public class Invitation
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00004774 File Offset: 0x00002974
		internal Invitation(Invitation.InvType invType, string invId, Participant inviter, int variant)
		{
			this.mInvitationType = invType;
			this.mInvitationId = invId;
			this.mInviter = inviter;
			this.mVariant = variant;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000479C File Offset: 0x0000299C
		public Invitation.InvType InvitationType
		{
			get
			{
				return this.mInvitationType;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000047A4 File Offset: 0x000029A4
		public string InvitationId
		{
			get
			{
				return this.mInvitationId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000047AC File Offset: 0x000029AC
		public Participant Inviter
		{
			get
			{
				return this.mInviter;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000047B4 File Offset: 0x000029B4
		public int Variant
		{
			get
			{
				return this.mVariant;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000047BC File Offset: 0x000029BC
		public override string ToString()
		{
			return string.Format("[Invitation: InvitationType={0}, InvitationId={1}, Inviter={2}, Variant={3}]", new object[]
			{
				this.InvitationType,
				this.InvitationId,
				this.Inviter,
				this.Variant
			});
		}

		// Token: 0x04000043 RID: 67
		private Invitation.InvType mInvitationType;

		// Token: 0x04000044 RID: 68
		private string mInvitationId;

		// Token: 0x04000045 RID: 69
		private Participant mInviter;

		// Token: 0x04000046 RID: 70
		private int mVariant;

		// Token: 0x02000021 RID: 33
		public enum InvType
		{
			// Token: 0x04000048 RID: 72
			RealTime,
			// Token: 0x04000049 RID: 73
			TurnBased,
			// Token: 0x0400004A RID: 74
			Unknown
		}
	}
}
