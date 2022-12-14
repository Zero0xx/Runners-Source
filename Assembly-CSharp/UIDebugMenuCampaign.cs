using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class UIDebugMenuCampaign : UIDebugMenuTask
{
	// Token: 0x06000CB6 RID: 3254 RVA: 0x000485B4 File Offset: 0x000467B4
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(400f, 100f, 150f, 50f), "Decide", base.gameObject);
		this.m_CampaignField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_CampaignField.Setup(new Rect(200f, 200f, 350f, 50f), "キャンペーンタイプ(IF定義のキャンペーン番号)");
		this.m_IdField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_IdField.Setup(new Rect(200f, 280f, 350f, 50f), "ID(必要な場合のみ)");
		this.m_IdField.text = "0";
		this.m_StartTimeField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_StartTimeField.Setup(new Rect(200f, 360f, 350f, 50f), "何分前に始まった？");
		this.m_StartTimeField.text = "1";
		this.m_EndTimeField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_EndTimeField.Setup(new Rect(200f, 440f, 350f, 50f), "何分後に終わる？");
		this.m_EndTimeField.text = "10";
		this.m_ContentField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_ContentField.Setup(new Rect(200f, 520f, 350f, 50f), "キャンペーン値");
		this.m_ContentField.text = "10";
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x000487A4 File Offset: 0x000469A4
	protected override void OnTransitionTo()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(false);
		}
		if (this.m_CampaignField != null)
		{
			this.m_CampaignField.SetActive(false);
		}
		if (this.m_IdField != null)
		{
			this.m_IdField.SetActive(false);
		}
		if (this.m_StartTimeField != null)
		{
			this.m_StartTimeField.SetActive(false);
		}
		if (this.m_EndTimeField != null)
		{
			this.m_EndTimeField.SetActive(false);
		}
		if (this.m_ContentField != null)
		{
			this.m_ContentField.SetActive(false);
		}
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0004887C File Offset: 0x00046A7C
	protected override void OnTransitionFrom()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(true);
		}
		if (this.m_CampaignField != null)
		{
			this.m_CampaignField.SetActive(true);
		}
		if (this.m_IdField != null)
		{
			this.m_IdField.SetActive(true);
		}
		if (this.m_StartTimeField != null)
		{
			this.m_StartTimeField.SetActive(true);
		}
		if (this.m_EndTimeField != null)
		{
			this.m_EndTimeField.SetActive(true);
		}
		if (this.m_ContentField != null)
		{
			this.m_ContentField.SetActive(true);
		}
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00048954 File Offset: 0x00046B54
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			ServerCampaignState campaignState = ServerInterface.CampaignState;
			if (campaignState != null)
			{
				ServerCampaignData serverCampaignData = new ServerCampaignData();
				serverCampaignData.campaignType = (Constants.Campaign.emType)int.Parse(this.m_CampaignField.text);
				serverCampaignData.id = int.Parse(this.m_IdField.text);
				serverCampaignData.beginDate = 0L;
				serverCampaignData.endDate = 0L;
				DateTime currentTime = NetBase.GetCurrentTime();
				DateTime dateTime = currentTime.AddMinutes(-double.Parse(this.m_StartTimeField.text));
				DateTime dateTime2 = currentTime.AddMinutes(double.Parse(this.m_EndTimeField.text));
				serverCampaignData.beginDate = NetUtil.GetUnixTime(dateTime);
				serverCampaignData.endDate = NetUtil.GetUnixTime(dateTime2);
				serverCampaignData.iContent = int.Parse(this.m_ContentField.text);
				serverCampaignData.iSubContent = 0;
				campaignState.RegistCampaign(serverCampaignData);
			}
		}
	}

	// Token: 0x04000A03 RID: 2563
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A04 RID: 2564
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A05 RID: 2565
	private UIDebugMenuTextField m_CampaignField;

	// Token: 0x04000A06 RID: 2566
	private UIDebugMenuTextField m_IdField;

	// Token: 0x04000A07 RID: 2567
	private UIDebugMenuTextField m_StartTimeField;

	// Token: 0x04000A08 RID: 2568
	private UIDebugMenuTextField m_EndTimeField;

	// Token: 0x04000A09 RID: 2569
	private UIDebugMenuTextField m_ContentField;
}
