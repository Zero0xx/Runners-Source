using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class RaidEnergyStorage : MonoBehaviour
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x000571F4 File Offset: 0x000553F4
	public static bool CheckEnergy(ref int energyFree, ref int energyBuy, ref DateTime atTime)
	{
		if ((long)(energyFree + energyBuy) < 3L)
		{
			bool flag = atTime <= NetBase.GetCurrentTime();
			int num = 0;
			if (flag)
			{
				while (flag)
				{
					num++;
					if ((long)(energyFree + energyBuy + num) >= 3L)
					{
						atTime = DateTime.MinValue;
						flag = false;
					}
					else
					{
						atTime += new TimeSpan(0, 0, 1800);
						flag = (atTime <= NetBase.GetCurrentTime());
					}
				}
				if (num > 0)
				{
					energyFree += num;
					if ((long)(energyFree + energyBuy) >= 3L)
					{
						atTime = DateTime.MinValue;
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x000572B0 File Offset: 0x000554B0
	private bool CanAddEnergy()
	{
		return !this.IsFillUpCount() && this.m_renew_at_time <= this.GetCurrentTime();
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x000572D0 File Offset: 0x000554D0
	public bool IsFillUpCount()
	{
		return this.m_refresh_time.Ticks <= 0L;
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000572E4 File Offset: 0x000554E4
	public void Init()
	{
		this.OnUpdateSaveDataDisplay();
		this.m_count_upd = 0L;
		if (!this.IsFillUpCount())
		{
			this.m_current_time = this.GetRestTimeForRenew();
			if (this.m_lblEnergy != null)
			{
				this.m_lblEnergy.text = this.energyCount.ToString();
				if ((long)this.m_last_count <= (long)((ulong)this.energyCount) || this.m_last_count < 0)
				{
					this.m_lblEnergy.gameObject.SetActive(true);
					this.m_last_count = (int)this.energyCount;
				}
			}
			if (this.m_lblOverEnergy != null)
			{
				this.m_lblOverEnergy.gameObject.SetActive(false);
			}
			if (this.m_lblTime != null)
			{
				if (this.m_current_time.Minutes >= 0 && this.m_current_time.Seconds >= 0)
				{
					this.m_lblTime.text = string.Format("{0:D2}:{1:D2}", this.m_current_time.Minutes, this.m_current_time.Seconds);
				}
				this.m_lblTime.gameObject.SetActive(true);
			}
			this.m_update_time = 0.2f;
		}
		else
		{
			if (this.m_lblEnergy != null)
			{
				this.m_lblEnergy.gameObject.SetActive(false);
			}
			if (this.m_lblOverEnergy != null)
			{
				this.m_lblOverEnergy.gameObject.SetActive(true);
				if ((long)this.m_last_count <= (long)((ulong)this.energyCount) || this.m_last_count < 0)
				{
					this.m_lblOverEnergy.text = this.energyCount.ToString();
					this.m_last_count = (int)this.energyCount;
				}
			}
			if (this.m_lblTime != null)
			{
				this.m_lblTime.gameObject.SetActive(false);
			}
			this.m_update_time = 1f;
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x000574DC File Offset: 0x000556DC
	private void InitEnergy()
	{
		if (!this.IsFillUpCount())
		{
			this.m_current_time = this.GetRestTimeForRenew();
			if (this.m_lblEnergy != null)
			{
				this.m_lblEnergy.gameObject.SetActive(true);
				this.m_lblEnergy.text = this.energyCount.ToString();
				this.m_last_count = (int)this.energyCount;
			}
			if (this.m_lblOverEnergy != null)
			{
				this.m_lblOverEnergy.gameObject.SetActive(false);
			}
			if (this.m_lblTime != null)
			{
				this.m_lblTime.gameObject.SetActive(true);
				if (this.m_current_time.Minutes >= 0 && this.m_current_time.Seconds >= 0)
				{
					this.m_lblTime.text = string.Format("{0:D2}:{1:D2}", this.m_current_time.Minutes, this.m_current_time.Seconds);
				}
			}
		}
		else
		{
			if (this.m_lblEnergy != null)
			{
				this.m_lblEnergy.gameObject.SetActive(false);
			}
			if (this.m_lblOverEnergy != null)
			{
				this.m_lblOverEnergy.gameObject.SetActive(true);
				this.m_lblOverEnergy.text = this.energyCount.ToString();
				this.m_last_count = (int)this.energyCount;
			}
			if (this.m_lblTime != null)
			{
				this.m_lblTime.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x00057674 File Offset: 0x00055874
	private void UpdateEnergy()
	{
		this.m_update_time -= Time.deltaTime;
		if (this.m_update_time <= 0f)
		{
			this.m_count_upd += 1L;
			if (!this.IsFillUpCount())
			{
				this.m_current_time = this.GetRestTimeForRenew();
				if (this.m_lblEnergy != null)
				{
					this.m_lblEnergy.gameObject.SetActive(true);
					if ((long)this.m_last_count <= (long)((ulong)this.energyCount) || this.m_count_upd > 3L)
					{
						this.m_lblEnergy.text = this.energyCount.ToString();
						this.m_last_count = (int)this.energyCount;
						this.m_count_upd = 0L;
					}
				}
				if (this.m_lblOverEnergy != null)
				{
					this.m_lblOverEnergy.gameObject.SetActive(false);
				}
				if (this.m_lblTime != null)
				{
					this.m_lblTime.gameObject.SetActive(true);
					if (this.m_current_time.Minutes >= 0 && this.m_current_time.Seconds >= 0)
					{
						this.m_lblTime.text = string.Format("{0:D2}:{1:D2}", this.m_current_time.Minutes, this.m_current_time.Seconds);
					}
				}
				this.m_update_time = 0.2f;
			}
			else
			{
				if (this.m_lblEnergy != null)
				{
					this.m_lblEnergy.gameObject.SetActive(false);
				}
				if (this.m_lblOverEnergy != null)
				{
					this.m_lblOverEnergy.gameObject.SetActive(true);
					if ((long)this.m_last_count <= (long)((ulong)this.energyCount) || this.m_count_upd > 3L)
					{
						this.m_lblOverEnergy.text = this.energyCount.ToString();
						this.m_last_count = (int)this.energyCount;
						this.m_count_upd = 0L;
					}
				}
				if (this.m_lblTime != null)
				{
					this.m_lblTime.gameObject.SetActive(false);
				}
				this.m_update_time = 1f;
			}
		}
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000578A4 File Offset: 0x00055AA4
	private void Update()
	{
		this.m_time += Time.deltaTime;
		if (this.m_time > 0.1f)
		{
			bool flag = this.CanAddEnergy();
			if (flag)
			{
				while (flag)
				{
					this.m_energyAdd_count += 1U;
					if (this.m_energyAdd_count >= this.m_energyAdd_max)
					{
						this.m_renew_at_time = DateTime.MinValue;
						this.m_refresh_time = new TimeSpan(0, 0, 0);
						flag = false;
					}
					else
					{
						this.m_renew_at_time += this.m_refresh_time;
						flag = this.CanAddEnergy();
					}
				}
				this.ReflectChallengeCount();
			}
			this.UpdateEnergy();
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00057954 File Offset: 0x00055B54
	public uint energyCount
	{
		get
		{
			return this.m_energy_count + this.m_energyStock_count + this.m_energyAdd_count;
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x0005796C File Offset: 0x00055B6C
	public TimeSpan GetRestTimeForRenew()
	{
		return this.m_renew_at_time - this.GetCurrentTime();
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x00057980 File Offset: 0x00055B80
	private DateTime GetCurrentTime()
	{
		return NetBase.GetCurrentTime();
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00057988 File Offset: 0x00055B88
	public void ReflectChallengeCount()
	{
		if (EventManager.Instance != null)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			global::Debug.Log(string.Concat(new object[]
			{
				"+ RaidEnergyStorage ReflectChallengeCount ChallengeCount:",
				EventManager.Instance.RaidbossChallengeCount,
				" !!!!!!!!!!!!!!!!! time:",
				realtimeSinceStartup
			}));
			if (EventManager.Instance.RaidBossState != null)
			{
				EventManager.Instance.RaidBossState.RaidBossEnergy = (int)(this.m_energy_count + this.m_energyAdd_count);
				EventManager.Instance.RaidBossState.EnergyRenewsAt = this.m_renew_at_time;
			}
			global::Debug.Log(string.Concat(new object[]
			{
				"- RaidEnergyStorage ReflectChallengeCount ChallengeCount:",
				EventManager.Instance.RaidbossChallengeCount,
				" !!!!!!!!!!!!!!!!! time:",
				realtimeSinceStartup
			}));
		}
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x00057A64 File Offset: 0x00055C64
	private void OnUpdateSaveDataDisplay()
	{
		this.m_time = 0f;
		this.m_energy_count = 0U;
		this.m_energyStock_count = 0U;
		this.m_energyAdd_count = 0U;
		this.m_energyAdd_max = 0U;
		this.m_renew_at_time = DateTime.MinValue;
		this.m_refresh_time = new TimeSpan(0, 0, 1);
		this.m_last_count = -1;
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			ServerEventUserRaidBossState raidBossState = instance.RaidBossState;
			if (raidBossState != null)
			{
				this.m_energy_count = (uint)raidBossState.RaidBossEnergy;
				this.m_energyStock_count = (uint)raidBossState.RaidbossEnergyBuy;
				if (this.m_energy_count + this.m_energyStock_count < 3U)
				{
					this.m_energyAdd_max = 3U - (this.m_energy_count + this.m_energyStock_count);
					this.m_refresh_time = new TimeSpan(0, 0, 1800);
					this.m_renew_at_time = raidBossState.EnergyRenewsAt;
				}
				else
				{
					this.m_renew_at_time = DateTime.MinValue;
					this.m_refresh_time = new TimeSpan(0, 0, 0);
				}
			}
		}
		this.InitEnergy();
	}

	// Token: 0x04000CD7 RID: 3287
	private const float REFRESH_TIME = 30f;

	// Token: 0x04000CD8 RID: 3288
	private const float UPDATE_TIME = 0.2f;

	// Token: 0x04000CD9 RID: 3289
	private const uint FILL_UP_BORDER_COUNT = 3U;

	// Token: 0x04000CDA RID: 3290
	[SerializeField]
	private UILabel m_lblEnergy;

	// Token: 0x04000CDB RID: 3291
	[SerializeField]
	private UILabel m_lblOverEnergy;

	// Token: 0x04000CDC RID: 3292
	[SerializeField]
	private UILabel m_lblTime;

	// Token: 0x04000CDD RID: 3293
	private DateTime m_renew_at_time = DateTime.MinValue;

	// Token: 0x04000CDE RID: 3294
	private TimeSpan m_refresh_time = new TimeSpan(0, 0, 1);

	// Token: 0x04000CDF RID: 3295
	private TimeSpan m_current_time = new TimeSpan(0, 0, 0);

	// Token: 0x04000CE0 RID: 3296
	private uint m_energy_count = 3U;

	// Token: 0x04000CE1 RID: 3297
	private uint m_energyStock_count;

	// Token: 0x04000CE2 RID: 3298
	private uint m_energyAdd_count;

	// Token: 0x04000CE3 RID: 3299
	private uint m_energyAdd_max;

	// Token: 0x04000CE4 RID: 3300
	private int m_last_count = -1;

	// Token: 0x04000CE5 RID: 3301
	private long m_count_upd;

	// Token: 0x04000CE6 RID: 3302
	private float m_update_time;

	// Token: 0x04000CE7 RID: 3303
	private float m_time;
}
