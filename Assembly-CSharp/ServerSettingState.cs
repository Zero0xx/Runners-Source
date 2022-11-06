using System;

// Token: 0x02000821 RID: 2081
public class ServerSettingState
{
	// Token: 0x060037D2 RID: 14290 RVA: 0x001267A0 File Offset: 0x001249A0
	public ServerSettingState()
	{
		this.m_energyRefreshTime = 0L;
		this.m_energyRecoveryMax = 1;
		this.m_invitBaseIncentive = new ServerItemState();
		this.m_rentalBaseIncentive = new ServerItemState();
		this.m_subCharaRingPayment = 300;
		this.m_userName = string.Empty;
		this.m_userId = string.Empty;
		this.m_monthPurchase = 0;
		this.m_birthday = string.Empty;
		this.m_countryId = 0;
		this.m_countryCode = string.Empty;
		this.m_onePlayCmCount = 0;
		this.m_onePlayContinueCount = 0;
		this.m_isPurchased = false;
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x00126834 File Offset: 0x00124A34
	public void CopyTo(ServerSettingState to)
	{
		to.m_energyRefreshTime = this.m_energyRefreshTime;
		to.m_energyRecoveryMax = this.m_energyRecoveryMax;
		this.m_invitBaseIncentive.CopyTo(to.m_invitBaseIncentive);
		this.m_rentalBaseIncentive.CopyTo(to.m_rentalBaseIncentive);
		to.m_subCharaRingPayment = this.m_subCharaRingPayment;
		to.m_userName = string.Copy(this.m_userName);
		to.m_userId = string.Copy(this.m_userId);
		to.m_monthPurchase = this.m_monthPurchase;
		to.m_birthday = this.m_birthday;
		to.m_countryId = this.m_countryId;
		to.m_countryCode = this.m_countryCode;
		to.m_onePlayCmCount = this.m_onePlayCmCount;
		to.m_onePlayContinueCount = this.m_onePlayContinueCount;
		to.m_isPurchased = this.m_isPurchased;
	}

	// Token: 0x04002F30 RID: 12080
	public long m_energyRefreshTime;

	// Token: 0x04002F31 RID: 12081
	public int m_energyRecoveryMax;

	// Token: 0x04002F32 RID: 12082
	public int m_onePlayCmCount;

	// Token: 0x04002F33 RID: 12083
	public int m_onePlayContinueCount;

	// Token: 0x04002F34 RID: 12084
	public int m_cmSkipCount;

	// Token: 0x04002F35 RID: 12085
	public bool m_isPurchased;

	// Token: 0x04002F36 RID: 12086
	public ServerItemState m_invitBaseIncentive;

	// Token: 0x04002F37 RID: 12087
	public ServerItemState m_rentalBaseIncentive;

	// Token: 0x04002F38 RID: 12088
	public int m_subCharaRingPayment;

	// Token: 0x04002F39 RID: 12089
	public string m_userName;

	// Token: 0x04002F3A RID: 12090
	public string m_userId;

	// Token: 0x04002F3B RID: 12091
	public int m_monthPurchase;

	// Token: 0x04002F3C RID: 12092
	public string m_birthday;

	// Token: 0x04002F3D RID: 12093
	public int m_countryId;

	// Token: 0x04002F3E RID: 12094
	public string m_countryCode;
}
