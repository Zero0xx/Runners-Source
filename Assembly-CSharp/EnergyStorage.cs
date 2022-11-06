using System;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class EnergyStorage : MonoBehaviour
{
	// Token: 0x06001068 RID: 4200 RVA: 0x00060100 File Offset: 0x0005E300
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		ServerSettingState settingState = ServerInterface.SettingState;
		if (settingState != null)
		{
			this.m_energyRecoveryMax = (uint)settingState.m_energyRecoveryMax;
		}
		this.OnUpdateSaveDataDisplay();
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00060138 File Offset: 0x0005E338
	private bool CanAddEnergy()
	{
		return this.m_energy_count < this.m_energyRecoveryMax && this.m_renew_at_time <= this.GetCurrentTime();
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x00060160 File Offset: 0x0005E360
	public bool IsFillUpCount()
	{
		return this.m_energy_count >= this.m_energyRecoveryMax;
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x00060174 File Offset: 0x0005E374
	public void Update()
	{
		bool flag = this.CanAddEnergy();
		if (flag)
		{
			while (flag)
			{
				this.m_energy_count += 1U;
				this.m_renew_at_time += this.m_refresh_time;
				flag = this.CanAddEnergy();
			}
			this.ReflectChallengeCount();
			HudMenuUtility.SendMsgUpdateChallengeDisply();
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x0600106C RID: 4204 RVA: 0x000601D0 File Offset: 0x0005E3D0
	public uint Count
	{
		get
		{
			return this.m_energy_count;
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x000601D8 File Offset: 0x0005E3D8
	public TimeSpan GetRestTimeForRenew()
	{
		return this.m_renew_at_time - this.GetCurrentTime();
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000601EC File Offset: 0x0005E3EC
	private DateTime GetCurrentTime()
	{
		return NetBase.GetCurrentTime();
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x000601F4 File Offset: 0x0005E3F4
	private void ReflectChallengeCount()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			instance.PlayerData.ChallengeCount = this.m_energy_count;
			instance.SavePlayerData();
		}
		if (ServerInterface.PlayerState != null)
		{
			ServerInterface.PlayerState.m_energyRenewsAt = this.m_renew_at_time;
		}
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00060244 File Offset: 0x0005E444
	private void OnEnergyAwarded(uint energyToAward)
	{
		this.m_energy_count += energyToAward;
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x00060254 File Offset: 0x0005E454
	private void OnUpdateSaveDataDisplay()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			this.m_energy_count = instance.PlayerData.ChallengeCount;
		}
		if (ServerInterface.PlayerState != null)
		{
			this.m_renew_at_time = ServerInterface.PlayerState.m_energyRenewsAt;
		}
		if (ServerInterface.SettingState != null)
		{
			int seconds = (int)ServerInterface.SettingState.m_energyRefreshTime;
			this.m_refresh_time = new TimeSpan(0, 0, seconds);
		}
	}

	// Token: 0x04000F03 RID: 3843
	private DateTime m_renew_at_time = DateTime.MinValue;

	// Token: 0x04000F04 RID: 3844
	private TimeSpan m_refresh_time = new TimeSpan(0, 30, 0);

	// Token: 0x04000F05 RID: 3845
	private uint m_energy_count = 1U;

	// Token: 0x04000F06 RID: 3846
	private uint m_energyRecoveryMax = 1U;
}
