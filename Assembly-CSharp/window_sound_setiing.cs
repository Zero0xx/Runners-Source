using System;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
public class window_sound_setiing : WindowBase
{
	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x06002382 RID: 9090 RVA: 0x000D5504 File Offset: 0x000D3704
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x06002383 RID: 9091 RVA: 0x000D550C File Offset: 0x000D370C
	public bool IsOverwrite
	{
		get
		{
			return this.m_isOverwrite;
		}
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x000D5514 File Offset: 0x000D3714
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		if (this.m_BGMSlider != null)
		{
			this.m_BGMSlider.value = SoundManager.BgmVolume;
			EventDelegate.Add(this.m_BGMSlider.onChange, new EventDelegate.Callback(this.OnChangeBGMSlider));
		}
		if (this.m_SESlider != null)
		{
			this.m_SESlider.value = SoundManager.SeVolume;
			EventDelegate.Add(this.m_SESlider.onChange, new EventDelegate.Callback(this.OnChangeSESlider));
		}
		this.m_preBgmVolume = Mathf.Clamp((int)(SoundManager.BgmVolume * 100f), 0, 100);
		this.m_preSeVolume = Mathf.Clamp((int)(SoundManager.SeVolume * 100f), 0, 100);
		float num = 1f / (float)this.m_tickMarkNum;
		if (num > 0f)
		{
			for (int i = 0; i < this.m_tickMarkNum; i++)
			{
				if (i != this.m_tickMarkNum - 1)
				{
					float item = num * (float)(i + 1);
					this.m_tickMarkValue.Add(item);
				}
				else
				{
					this.m_tickMarkValue.Add(1f);
				}
			}
		}
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "sound_config");
		TextUtility.SetCommonText(this.m_headerSubTextLabel, "Option", "sound_config_info");
		TextUtility.SetCommonText(this.m_BGMTextLabel, "Option", "sound_bgm");
		TextUtility.SetCommonText(this.m_SETextLabel, "Option", "sound_se");
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
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x000D579C File Offset: 0x000D399C
	private void Update()
	{
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x000D57A0 File Offset: 0x000D39A0
	private void OnChangeBGMSlider()
	{
		SoundManager.BgmVolume = this.m_BGMSlider.value;
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x000D57B4 File Offset: 0x000D39B4
	private void OnChangeSESlider()
	{
		float seVolume = SoundManager.SeVolume;
		SoundManager.SeVolume = this.m_SESlider.value;
		this.CheckSEPlay(seVolume, this.m_SESlider.value);
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x000D57EC File Offset: 0x000D39EC
	private void CheckSEPlay(float preValue, float currentValue)
	{
		int num = -1;
		for (int i = 0; i < this.m_tickMarkNum; i++)
		{
			if (currentValue >= this.m_tickMarkValue[i])
			{
				num = i;
			}
		}
		int num2 = -1;
		for (int j = 0; j < this.m_tickMarkNum; j++)
		{
			if (preValue >= this.m_tickMarkValue[j])
			{
				num2 = j;
			}
		}
		if (num != num2)
		{
			if (preValue == 1f && num2 - num == 1)
			{
				return;
			}
			if (this.m_playID != SoundManager.PlayId.NONE)
			{
				SoundManager.SeStop(this.m_playID);
			}
			this.m_playID = SoundManager.SePlay("obj_ring", "SE");
		}
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x000D589C File Offset: 0x000D3A9C
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		int num = Mathf.Clamp((int)(SoundManager.BgmVolume * 100f), 0, 100);
		int num2 = Mathf.Clamp((int)(SoundManager.SeVolume * 100f), 0, 100);
		if (num != this.m_preBgmVolume || num2 != this.m_preSeVolume)
		{
			this.m_isOverwrite = true;
		}
		if (this.m_isOverwrite)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.bgmVolume = num;
					systemdata.seVolume = num2;
				}
			}
		}
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x000D593C File Offset: 0x000D3B3C
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x000D5948 File Offset: 0x000D3B48
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		this.m_isOverwrite = false;
		this.m_preBgmVolume = Mathf.Clamp((int)(SoundManager.BgmVolume * 100f), 0, 100);
		this.m_preSeVolume = Mathf.Clamp((int)(SoundManager.SeVolume * 100f), 0, 100);
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x000D59B4 File Offset: 0x000D3BB4
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

	// Token: 0x0400203A RID: 8250
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x0400203B RID: 8251
	[SerializeField]
	private UISlider m_BGMSlider;

	// Token: 0x0400203C RID: 8252
	[SerializeField]
	private UISlider m_SESlider;

	// Token: 0x0400203D RID: 8253
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x0400203E RID: 8254
	[SerializeField]
	private UILabel m_headerSubTextLabel;

	// Token: 0x0400203F RID: 8255
	[SerializeField]
	private UILabel m_BGMTextLabel;

	// Token: 0x04002040 RID: 8256
	[SerializeField]
	private UILabel m_SETextLabel;

	// Token: 0x04002041 RID: 8257
	[SerializeField]
	private int m_tickMarkNum = 6;

	// Token: 0x04002042 RID: 8258
	private List<float> m_tickMarkValue = new List<float>();

	// Token: 0x04002043 RID: 8259
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002044 RID: 8260
	private int m_preBgmVolume;

	// Token: 0x04002045 RID: 8261
	private int m_preSeVolume;

	// Token: 0x04002046 RID: 8262
	private SoundManager.PlayId m_playID;

	// Token: 0x04002047 RID: 8263
	private bool m_isEnd;

	// Token: 0x04002048 RID: 8264
	private bool m_isOverwrite;
}
