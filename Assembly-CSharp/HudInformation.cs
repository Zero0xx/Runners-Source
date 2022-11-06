using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
public class HudInformation : MonoBehaviour
{
	// Token: 0x0600215E RID: 8542 RVA: 0x000C8C58 File Offset: 0x000C6E58
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x000C8C6C File Offset: 0x000C6E6C
	private void Initialize()
	{
		this.m_initFlag = true;
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(mainMenuUIObject, "Anchor_7_BL");
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_0_info");
				if (gameObject2 != null)
				{
					this.m_badgeObj = GameObjectUtil.FindChildGameObject(gameObject2, "badge");
					this.m_volumeLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_present_volume");
				}
			}
		}
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
	private void UpdateInfoIcon()
	{
		int num = 0;
		if (ServerInterface.NoticeInfo != null && ServerInterface.NoticeInfo.m_noticeItems != null)
		{
			foreach (NetNoticeItem netNoticeItem in ServerInterface.NoticeInfo.m_noticeItems)
			{
				if (netNoticeItem != null && !ServerInterface.NoticeInfo.IsCheckedForMenuIcon(netNoticeItem) && ServerInterface.NoticeInfo.IsOnTime(netNoticeItem))
				{
					num++;
				}
			}
		}
		if (num == 0)
		{
			if (this.m_badgeObj != null && this.m_badgeObj.activeSelf)
			{
				this.m_badgeObj.SetActive(false);
			}
		}
		else
		{
			if (this.m_badgeObj != null && !this.m_badgeObj.activeSelf)
			{
				this.m_badgeObj.SetActive(true);
			}
			if (this.m_volumeLabel != null)
			{
				this.m_volumeLabel.text = num.ToString();
			}
		}
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000C8E14 File Offset: 0x000C7014
	public void OnUpdateInformationDisplay()
	{
		if (!this.m_initFlag)
		{
			this.Initialize();
		}
		this.UpdateInfoIcon();
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x000C8E30 File Offset: 0x000C7030
	public void OnUpdateSaveDataDisplay()
	{
		if (!this.m_initFlag)
		{
			this.Initialize();
		}
		if (EventManager.Instance != null && EventManager.Instance.IsInEvent())
		{
			GeneralUtil.SetEventBanner(this.m_mileageObj, "event_banner");
		}
		this.UpdateInfoIcon();
	}

	// Token: 0x04001E2D RID: 7725
	private GameObject m_badgeObj;

	// Token: 0x04001E2E RID: 7726
	private GameObject m_mileageObj;

	// Token: 0x04001E2F RID: 7727
	private UILabel m_volumeLabel;

	// Token: 0x04001E30 RID: 7728
	private bool m_initFlag;
}
