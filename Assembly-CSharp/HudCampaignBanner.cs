using System;
using Text;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class HudCampaignBanner : MonoBehaviour
{
	// Token: 0x060021A9 RID: 8617 RVA: 0x000CA8E4 File Offset: 0x000C8AE4
	private void Start()
	{
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x000CA8E8 File Offset: 0x000C8AE8
	private void OnDestroy()
	{
		if (this.m_replaceTex != null)
		{
			this.m_replaceTex.mainTexture = null;
			this.m_replaceTex = null;
		}
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x000CA91C File Offset: 0x000C8B1C
	private void Update()
	{
		if (this.m_infoWindow != null && this.m_infoWindow.IsEnd())
		{
			base.enabled = false;
		}
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x000CA954 File Offset: 0x000C8B54
	public void Initialize(GameObject mainMenuObject, bool quickMode)
	{
		this.m_quick = quickMode;
		if (mainMenuObject == null)
		{
			return;
		}
		this.m_mainMenuObject = mainMenuObject;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		string name = (!this.m_quick) ? "0_Endless" : "1_Quick";
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, name);
		if (gameObject2 == null)
		{
			return;
		}
		this.m_textureObj = GameObjectUtil.FindChildGameObject(gameObject2, "img_ad_tex");
		if (this.m_textureObj == null)
		{
			return;
		}
		UIButtonMessage component = this.m_textureObj.GetComponent<UIButtonMessage>();
		if (component != null)
		{
			component.enabled = true;
			component.trigger = UIButtonMessage.Trigger.OnClick;
			component.target = base.gameObject;
			component.functionName = "CampaignBannerClicked";
		}
		this.m_replaceTex = this.m_textureObj.GetComponent<UITexture>();
		this.m_textureObj.SetActive(false);
		this.UpdateView();
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x000CAA50 File Offset: 0x000C8C50
	public void UpdateView()
	{
		bool flag = false;
		if (EventManager.Instance != null)
		{
			if (EventManager.Instance.Type == EventManager.EventType.QUICK)
			{
				flag = this.m_quick;
			}
			else if (EventManager.Instance.Type == EventManager.EventType.BGM)
			{
				EventStageData stageData = EventManager.Instance.GetStageData();
				if (stageData != null)
				{
					flag = ((!this.m_quick) ? stageData.IsEndlessModeBGM() : stageData.IsQuickModeBGM());
				}
			}
		}
		if (flag)
		{
			if (ServerInterface.NoticeInfo != null && ServerInterface.NoticeInfo.m_eventItems != null)
			{
				foreach (NetNoticeItem netNoticeItem in ServerInterface.NoticeInfo.m_eventItems)
				{
					if (this.m_id != netNoticeItem.Id)
					{
						this.m_id = netNoticeItem.Id;
						if (InformationImageManager.Instance != null)
						{
							InformationImageManager.Instance.Load(netNoticeItem.ImageId, true, new Action<Texture2D>(this.OnLoadCallback));
						}
						if (this.m_textureObj != null)
						{
							this.m_textureObj.SetActive(true);
						}
						break;
					}
				}
			}
		}
		else
		{
			if (this.m_textureObj != null)
			{
				this.m_textureObj.SetActive(false);
			}
			if (this.m_replaceTex != null && this.m_replaceTex.mainTexture != null)
			{
				this.m_replaceTex.mainTexture = null;
			}
		}
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x000CAC00 File Offset: 0x000C8E00
	public void OnLoadCallback(Texture2D texture)
	{
		if (this.m_replaceTex != null && texture != null)
		{
			this.m_replaceTex.mainTexture = texture;
		}
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x000CAC2C File Offset: 0x000C8E2C
	private void CampaignBannerClicked()
	{
		if (this.m_mainMenuObject == null)
		{
			return;
		}
		this.m_infoWindow = base.gameObject.GetComponent<InformationWindow>();
		if (this.m_infoWindow == null)
		{
			this.m_infoWindow = base.gameObject.AddComponent<InformationWindow>();
		}
		if (this.m_infoWindow != null && ServerInterface.NoticeInfo != null && ServerInterface.NoticeInfo.m_eventItems != null)
		{
			foreach (NetNoticeItem netNoticeItem in ServerInterface.NoticeInfo.m_eventItems)
			{
				if (this.m_id == netNoticeItem.Id)
				{
					InformationWindow.Information info = default(InformationWindow.Information);
					info.pattern = InformationWindow.ButtonPattern.OK;
					info.imageId = netNoticeItem.ImageId;
					info.caption = TextUtility.GetCommonText("Informaion", "announcement");
					GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
					if (cameraUIObject != null)
					{
						GameObject newsWindowObj = GameObjectUtil.FindChildGameObject(cameraUIObject, "NewsWindow");
						this.m_infoWindow.Create(info, newsWindowObj);
						base.enabled = true;
						SoundManager.SePlay("sys_menu_decide", "SE");
					}
					break;
				}
			}
		}
	}

	// Token: 0x04001E63 RID: 7779
	private GameObject m_mainMenuObject;

	// Token: 0x04001E64 RID: 7780
	private GameObject m_textureObj;

	// Token: 0x04001E65 RID: 7781
	private UITexture m_replaceTex;

	// Token: 0x04001E66 RID: 7782
	private InformationWindow m_infoWindow;

	// Token: 0x04001E67 RID: 7783
	private long m_id = -1L;

	// Token: 0x04001E68 RID: 7784
	private bool m_quick;
}
