using System;
using Text;
using UnityEngine;

// Token: 0x020004A9 RID: 1193
public class window_buying_history : WindowBase
{
	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x06002342 RID: 9026 RVA: 0x000D3D60 File Offset: 0x000D1F60
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x000D3D68 File Offset: 0x000D1F68
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		base.enabled = false;
		if (this.m_closeBtn != null)
		{
			UIPlayAnimation component = this.m_closeBtn.GetComponent<UIPlayAnimation>();
			if (component != null)
			{
				EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
			}
			UIButtonMessage component2 = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (component2 == null)
			{
				this.m_closeBtn.AddComponent<UIButtonMessage>();
				component2 = this.m_closeBtn.GetComponent<UIButtonMessage>();
			}
			if (component2 != null)
			{
				component2.enabled = true;
				component2.trigger = UIButtonMessage.Trigger.OnClick;
				component2.target = base.gameObject;
				component2.functionName = "OnClickCloseButton";
			}
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component3 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component3;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		this.UpdateView();
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x000D3E8C File Offset: 0x000D208C
	private void OnDestroy()
	{
		global::Debug.LogWarning("window_buying_history::OnDestroy()");
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x000D3E98 File Offset: 0x000D2098
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x000D3EAC File Offset: 0x000D20AC
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x000D3EB8 File Offset: 0x000D20B8
	private void UpdateView()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (ServerInterface.PlayerState != null)
		{
			num = ServerInterface.PlayerState.m_numBuyRedRings;
			num2 = ServerInterface.PlayerState.m_numBuyRings;
			num3 = ServerInterface.PlayerState.m_numBuyEnergy;
			num4 = ServerInterface.PlayerState.m_numRedRings - num;
			num5 = ServerInterface.PlayerState.m_numRings - num2;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				num6 = (int)(instance.PlayerData.ChallengeCount - (uint)num3);
			}
		}
		num = Mathf.Clamp(num, 0, 99999999);
		num2 = Mathf.Clamp(num2, 0, 99999999);
		num3 = Mathf.Clamp(num3, 0, 99999999);
		num4 = Mathf.Clamp(num4, 0, 99999999);
		num5 = Mathf.Clamp(num5, 0, 99999999);
		num6 = Mathf.Clamp(num6, 0, 99999999);
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "buying_info");
		TextUtility.SetCommonText(this.m_redRingText, "Item", "red_star_ring");
		TextUtility.SetCommonText(this.m_ringText, "Item", "ring");
		TextUtility.SetCommonText(this.m_energyText, "Item", "energy");
		TextUtility.SetCommonText(this.m_redRingGetText, "Option", "take");
		TextUtility.SetCommonText(this.m_ringGetText, "Option", "take");
		TextUtility.SetCommonText(this.m_energyGetText, "Option", "take");
		TextUtility.SetCommonText(this.m_redRingBuyText, "Option", "buy");
		TextUtility.SetCommonText(this.m_ringBuyText, "Option", "buy");
		TextUtility.SetCommonText(this.m_energyBuyText, "Option", "buy");
		TextUtility.SetCommonText(this.m_redRingGetScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num4));
		TextUtility.SetCommonText(this.m_ringGetScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num5));
		TextUtility.SetCommonText(this.m_energyGetScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num6));
		TextUtility.SetCommonText(this.m_redRingBuyScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num));
		TextUtility.SetCommonText(this.m_ringBuyScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num2));
		TextUtility.SetCommonText(this.m_energyBuyScoreText, "Score", "number_of_pieces", "{NUM}", HudUtility.GetFormatNumString<int>(num3));
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x000D4124 File Offset: 0x000D2324
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		if (this.m_uiAnimation != null)
		{
			this.UpdateView();
			this.m_uiAnimation.Play(true);
		}
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x000D415C File Offset: 0x000D235C
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		UIButtonMessage component = this.m_closeBtn.GetComponent<UIButtonMessage>();
		if (component != null)
		{
			component.SendMessage("OnClick");
		}
	}

	// Token: 0x04001FF9 RID: 8185
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x04001FFA RID: 8186
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04001FFB RID: 8187
	[SerializeField]
	private UILabel m_redRingText;

	// Token: 0x04001FFC RID: 8188
	[SerializeField]
	private UILabel m_redRingGetScoreText;

	// Token: 0x04001FFD RID: 8189
	[SerializeField]
	private UILabel m_redRingBuyScoreText;

	// Token: 0x04001FFE RID: 8190
	[SerializeField]
	private UILabel m_redRingGetText;

	// Token: 0x04001FFF RID: 8191
	[SerializeField]
	private UILabel m_redRingBuyText;

	// Token: 0x04002000 RID: 8192
	[SerializeField]
	private UILabel m_ringText;

	// Token: 0x04002001 RID: 8193
	[SerializeField]
	private UILabel m_ringGetScoreText;

	// Token: 0x04002002 RID: 8194
	[SerializeField]
	private UILabel m_ringBuyScoreText;

	// Token: 0x04002003 RID: 8195
	[SerializeField]
	private UILabel m_ringGetText;

	// Token: 0x04002004 RID: 8196
	[SerializeField]
	private UILabel m_ringBuyText;

	// Token: 0x04002005 RID: 8197
	[SerializeField]
	private UILabel m_energyText;

	// Token: 0x04002006 RID: 8198
	[SerializeField]
	private UILabel m_energyGetScoreText;

	// Token: 0x04002007 RID: 8199
	[SerializeField]
	private UILabel m_energyBuyScoreText;

	// Token: 0x04002008 RID: 8200
	[SerializeField]
	private UILabel m_energyGetText;

	// Token: 0x04002009 RID: 8201
	[SerializeField]
	private UILabel m_energyBuyText;

	// Token: 0x0400200A RID: 8202
	private bool m_isEnd;

	// Token: 0x0400200B RID: 8203
	private UIPlayAnimation m_uiAnimation;
}
