using System;
using UnityEngine;

// Token: 0x0200040D RID: 1037
public class FirstLaunchUserName : MonoBehaviour
{
	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000B7DC8 File Offset: 0x000B5FC8
	// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x000B7E08 File Offset: 0x000B6008
	public static bool IsFirstLaunch
	{
		get
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				ServerSettingState settingState = ServerInterface.SettingState;
				if (settingState.m_userName != string.Empty)
				{
					return false;
				}
			}
			return true;
		}
		private set
		{
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000B7E0C File Offset: 0x000B600C
	// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000B7E2C File Offset: 0x000B602C
	public bool IsEndPlay
	{
		get
		{
			return this.m_settingName == null || this.m_settingName.IsEndPlay();
		}
		private set
		{
		}
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x000B7E30 File Offset: 0x000B6030
	public void Setup(string anchorName)
	{
		if (anchorName == null)
		{
			return;
		}
		if (!FirstLaunchUserName.IsFirstLaunch)
		{
			return;
		}
		this.m_settingName = base.gameObject.GetComponent<SettingUserName>();
		if (this.m_settingName == null)
		{
			this.m_settingName = base.gameObject.AddComponent<SettingUserName>();
		}
		this.m_settingName.SetCancelButtonUseFlag(false);
		this.m_settingName.Setup(anchorName);
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x000B7E9C File Offset: 0x000B609C
	public void PlayStart()
	{
		if (!FirstLaunchUserName.IsFirstLaunch)
		{
			return;
		}
		if (this.m_settingName == null)
		{
			return;
		}
		this.m_settingName.PlayStart();
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x000B7ED4 File Offset: 0x000B60D4
	private void Start()
	{
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x000B7ED8 File Offset: 0x000B60D8
	private void Update()
	{
	}

	// Token: 0x04001C4C RID: 7244
	private SettingUserName m_settingName;
}
