using System;
using SaveData;
using UnityEngine;

// Token: 0x020009D2 RID: 2514
public class RegionManager : MonoBehaviour
{
	// Token: 0x170008F8 RID: 2296
	// (get) Token: 0x06004204 RID: 16900 RVA: 0x001584A8 File Offset: 0x001566A8
	// (set) Token: 0x06004205 RID: 16901 RVA: 0x001584B0 File Offset: 0x001566B0
	public static RegionManager Instance
	{
		get
		{
			return RegionManager.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x001584B4 File Offset: 0x001566B4
	public RegionInfo GetRegionInfo()
	{
		if (this.m_table != null)
		{
			string countryCode = SystemSaveManager.GetCountryCode();
			return this.m_table.GetInfo(countryCode);
		}
		return null;
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x001584E0 File Offset: 0x001566E0
	public bool IsJapan()
	{
		RegionInfo regionInfo = this.GetRegionInfo();
		return regionInfo != null && regionInfo.CountryCode == "JP";
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x00158514 File Offset: 0x00156714
	public bool IsNeedIapMessage()
	{
		return false;
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x00158518 File Offset: 0x00156718
	public bool IsNeedESRB()
	{
		RegionInfo regionInfo = this.GetRegionInfo();
		bool result = true;
		if (regionInfo != null && !string.IsNullOrEmpty(regionInfo.Limit))
		{
			string limit = regionInfo.Limit;
			if (limit.IndexOf("ESRB") == -1 && limit.IndexOf("esrb") == -1 && limit.IndexOf("Esrb") == -1)
			{
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x00158584 File Offset: 0x00156784
	public bool IsUseSNS()
	{
		bool result = true;
		if (this.IsNeedESRB())
		{
			result = false;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			int num = 1;
			if (loggedInServerInterface != null)
			{
				ServerSettingState settingState = ServerInterface.SettingState;
				if (settingState != null && !string.IsNullOrEmpty(settingState.m_birthday))
				{
					num = HudUtility.GetAge(DateTime.Parse(settingState.m_birthday), NetUtil.GetCurrentTime());
				}
			}
			if (num >= 13)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x001585F4 File Offset: 0x001567F4
	public bool IsUseHardlightAds()
	{
		RegionInfo regionInfo = this.GetRegionInfo();
		if (regionInfo == null)
		{
			return false;
		}
		if (regionInfo.CountryCode == "US")
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			int num = 1;
			if (loggedInServerInterface != null)
			{
				ServerSettingState settingState = ServerInterface.SettingState;
				if (settingState != null && !string.IsNullOrEmpty(settingState.m_birthday))
				{
					num = HudUtility.GetAge(DateTime.Parse(settingState.m_birthday), NetUtil.GetCurrentTime());
				}
			}
			return num >= 13;
		}
		return true;
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x0015867C File Offset: 0x0015687C
	private void Awake()
	{
		if (RegionManager.m_instance == null)
		{
			RegionManager.m_instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.Init();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x001586C0 File Offset: 0x001568C0
	private void Init()
	{
		this.m_table = new RegionInfoTable();
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x001586D0 File Offset: 0x001568D0
	private void OnDestroy()
	{
		if (RegionManager.m_instance == this)
		{
			RegionManager.m_instance = null;
		}
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x001586E8 File Offset: 0x001568E8
	private void Start()
	{
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x001586EC File Offset: 0x001568EC
	private void Update()
	{
	}

	// Token: 0x0400383C RID: 14396
	private static RegionManager m_instance;

	// Token: 0x0400383D RID: 14397
	private RegionInfoTable m_table;
}
