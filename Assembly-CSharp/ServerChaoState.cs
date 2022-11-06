using System;

// Token: 0x020007F2 RID: 2034
public class ServerChaoState : ServerChaoData
{
	// Token: 0x0600366F RID: 13935 RVA: 0x00121474 File Offset: 0x0011F674
	public ServerChaoState()
	{
		this.Status = ServerChaoState.ChaoStatus.NotOwned;
		this.Dealing = ServerChaoState.ChaoDealing.None;
		this.NumInvite = 0;
		this.NumAcquired = 0;
		this.Hidden = false;
		this.IsLvUp = false;
		this.IsNew = false;
	}

	// Token: 0x170007D2 RID: 2002
	// (get) Token: 0x06003670 RID: 13936 RVA: 0x001214B8 File Offset: 0x0011F6B8
	// (set) Token: 0x06003671 RID: 13937 RVA: 0x001214C0 File Offset: 0x0011F6C0
	public ServerChaoState.ChaoStatus Status { get; set; }

	// Token: 0x170007D3 RID: 2003
	// (get) Token: 0x06003672 RID: 13938 RVA: 0x001214CC File Offset: 0x0011F6CC
	// (set) Token: 0x06003673 RID: 13939 RVA: 0x001214D4 File Offset: 0x0011F6D4
	public ServerChaoState.ChaoDealing Dealing { get; set; }

	// Token: 0x170007D4 RID: 2004
	// (get) Token: 0x06003674 RID: 13940 RVA: 0x001214E0 File Offset: 0x0011F6E0
	// (set) Token: 0x06003675 RID: 13941 RVA: 0x001214E8 File Offset: 0x0011F6E8
	public int NumInvite { get; set; }

	// Token: 0x170007D5 RID: 2005
	// (get) Token: 0x06003676 RID: 13942 RVA: 0x001214F4 File Offset: 0x0011F6F4
	// (set) Token: 0x06003677 RID: 13943 RVA: 0x001214FC File Offset: 0x0011F6FC
	public int NumAcquired { get; set; }

	// Token: 0x170007D6 RID: 2006
	// (get) Token: 0x06003678 RID: 13944 RVA: 0x00121508 File Offset: 0x0011F708
	// (set) Token: 0x06003679 RID: 13945 RVA: 0x00121510 File Offset: 0x0011F710
	public bool Hidden { get; set; }

	// Token: 0x170007D7 RID: 2007
	// (get) Token: 0x0600367A RID: 13946 RVA: 0x0012151C File Offset: 0x0011F71C
	// (set) Token: 0x0600367B RID: 13947 RVA: 0x00121524 File Offset: 0x0011F724
	public bool IsLvUp { get; set; }

	// Token: 0x170007D8 RID: 2008
	// (get) Token: 0x0600367C RID: 13948 RVA: 0x00121530 File Offset: 0x0011F730
	// (set) Token: 0x0600367D RID: 13949 RVA: 0x00121538 File Offset: 0x0011F738
	public bool IsNew { get; set; }

	// Token: 0x170007D9 RID: 2009
	// (get) Token: 0x0600367E RID: 13950 RVA: 0x00121544 File Offset: 0x0011F744
	public bool IsInvite
	{
		get
		{
			return 0 < this.NumInvite;
		}
	}

	// Token: 0x170007DA RID: 2010
	// (get) Token: 0x0600367F RID: 13951 RVA: 0x00121550 File Offset: 0x0011F750
	public bool IsOwned
	{
		get
		{
			return ServerChaoState.ChaoStatus.NotOwned != this.Status;
		}
	}

	// Token: 0x020007F3 RID: 2035
	public enum ChaoStatus
	{
		// Token: 0x04002DD8 RID: 11736
		NotOwned,
		// Token: 0x04002DD9 RID: 11737
		Owned,
		// Token: 0x04002DDA RID: 11738
		MaxLevel
	}

	// Token: 0x020007F4 RID: 2036
	public enum ChaoDealing
	{
		// Token: 0x04002DDC RID: 11740
		None,
		// Token: 0x04002DDD RID: 11741
		Leader,
		// Token: 0x04002DDE RID: 11742
		Sub
	}
}
