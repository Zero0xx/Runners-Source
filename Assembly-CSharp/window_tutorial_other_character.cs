using System;
using Text;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
public class window_tutorial_other_character : WindowBase
{
	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000D66D4 File Offset: 0x000D48D4
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x000D66DC File Offset: 0x000D48DC
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		if (this.m_closeBtn != null)
		{
			UIPlayAnimation component = this.m_closeBtn.GetComponent<UIPlayAnimation>();
			if (component != null)
			{
				EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
			}
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component2 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component2;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		if (this.m_texture != null)
		{
			this.m_texture.enabled = false;
		}
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x000D679C File Offset: 0x000D499C
	public void SetScrollInfo(window_tutorial.ScrollInfo info)
	{
		this.m_scrollInfo = info;
		this.m_isLoading = false;
		this.m_isEnd = false;
		if (this.m_texture != null)
		{
			this.m_texture.enabled = false;
		}
		this.m_pageIndex = 0;
		this.m_pageCount = 0;
		if (this.m_scrollInfo != null)
		{
			this.m_pageCount = HudTutorial.GetTexuterPageCount(this.m_scrollInfo.HudId);
		}
		this.CheckLoadTexture();
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x000D6810 File Offset: 0x000D4A10
	public void PlayOpenWindow()
	{
		this.m_playOpen = true;
		base.enabled = true;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x000D682C File Offset: 0x000D4A2C
	private void Update()
	{
		if (this.m_playOpen)
		{
			this.SetupText();
			this.SetNextBtn();
			if (this.m_uiAnimation != null)
			{
				this.m_uiAnimation.Play(true);
			}
			SoundManager.SePlay("sys_window_open", "SE");
			this.m_playOpen = false;
		}
		if (this.m_isLoading && this.m_loaderComponent != null && this.m_loaderComponent.Loaded)
		{
			this.SetupTexture();
			this.m_loaderComponent = null;
			this.m_isLoading = false;
		}
		if (!this.m_playOpen && !this.m_isLoading)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x000D68E4 File Offset: 0x000D4AE4
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x000D68F8 File Offset: 0x000D4AF8
	private void OnClickLeftButton()
	{
		this.m_pageIndex--;
		if (this.m_pageIndex < 0)
		{
			this.m_pageIndex = 0;
		}
		this.SetupTexture();
		this.SetupText();
		this.SetPage();
		SoundManager.SePlay("sys_page_skip", "SE");
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000D6948 File Offset: 0x000D4B48
	private void OnClickRightButton()
	{
		this.m_pageIndex++;
		if (this.m_pageIndex == this.m_pageCount)
		{
			this.m_pageIndex = this.m_pageCount - 1;
		}
		this.SetupTexture();
		this.SetupText();
		this.SetPage();
		SoundManager.SePlay("sys_page_skip", "SE");
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x000D69A4 File Offset: 0x000D4BA4
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x000D69B0 File Offset: 0x000D4BB0
	private void CheckLoadTexture()
	{
		if (this.m_scrollInfo != null)
		{
			switch (this.m_scrollInfo.DispType)
			{
			case window_tutorial.DisplayType.QUICK:
				this.m_sceneName = HudTutorial.GetLoadQuickModeSceneName(this.m_scrollInfo.HudId);
				break;
			case window_tutorial.DisplayType.CHARA:
				this.m_sceneName = HudTutorial.GetLoadSceneName(this.m_scrollInfo.Chara);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_1:
				this.m_sceneName = HudTutorial.GetLoadSceneName(BossType.MAP1);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_2:
				this.m_sceneName = HudTutorial.GetLoadSceneName(BossType.MAP2);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_3:
				this.m_sceneName = HudTutorial.GetLoadSceneName(BossType.MAP3);
				break;
			}
			if (!string.IsNullOrEmpty(this.m_sceneName))
			{
				GameObject x = GameObject.Find(this.m_sceneName);
				if (x == null)
				{
					this.LoadTexture(this.m_sceneName);
				}
				else
				{
					this.SetupTexture();
				}
			}
		}
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x000D6AA0 File Offset: 0x000D4CA0
	private void SetupTexture()
	{
		if (this.m_scrollInfo != null && !string.IsNullOrEmpty(this.m_sceneName))
		{
			GameObject gameObject = GameObject.Find(this.m_sceneName);
			if (gameObject != null)
			{
				HudTutorialTexture component = gameObject.GetComponent<HudTutorialTexture>();
				if (component != null && this.m_texture != null)
				{
					uint num = (uint)component.m_texList.Length;
					if (this.m_pageIndex < (int)num)
					{
						this.m_texture.mainTexture = component.m_texList[this.m_pageIndex];
						this.m_texture.enabled = true;
					}
				}
			}
		}
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x000D6B40 File Offset: 0x000D4D40
	private void LoadTexture(string sceneName)
	{
		if (!this.m_isLoading)
		{
			if (this.m_loaderComponent == null)
			{
				this.m_loaderComponent = base.gameObject.AddComponent<ResourceSceneLoader>();
			}
			if (this.m_loaderComponent != null)
			{
				this.m_loaderComponent.AddLoad(sceneName, true, false);
				this.m_isLoading = true;
			}
		}
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x000D6BA4 File Offset: 0x000D4DA4
	private void SetupText()
	{
		if (this.m_scrollInfo != null)
		{
			string text = string.Empty;
			switch (this.m_scrollInfo.DispType)
			{
			case window_tutorial.DisplayType.QUICK:
			{
				string commonText = TextUtility.GetCommonText("Tutorial", "caption_quickmode_tutorial");
				text = TextUtility.GetCommonText("Tutorial", "caption_explan2", "{PARAM_NAME}", commonText);
				break;
			}
			case window_tutorial.DisplayType.CHARA:
			{
				string cellID = CharaName.Name[(int)this.m_scrollInfo.Chara];
				string commonText2 = TextUtility.GetCommonText("CharaName", cellID);
				text = TextUtility.GetCommonText("Option", "chara_operation_method", "{CHARA_NAME}", commonText2);
				break;
			}
			case window_tutorial.DisplayType.BOSS_MAP_1:
			{
				string textCommonBossName = BossTypeUtil.GetTextCommonBossName(BossType.MAP1);
				text = TextUtility.GetCommonText("Option", "boss_attack_method", "{BOSS_NAME}", textCommonBossName);
				break;
			}
			case window_tutorial.DisplayType.BOSS_MAP_2:
			{
				string textCommonBossName2 = BossTypeUtil.GetTextCommonBossName(BossType.MAP2);
				text = TextUtility.GetCommonText("Option", "boss_attack_method", "{BOSS_NAME}", textCommonBossName2);
				break;
			}
			case window_tutorial.DisplayType.BOSS_MAP_3:
			{
				string textCommonBossName3 = BossTypeUtil.GetTextCommonBossName(BossType.MAP3);
				text = TextUtility.GetCommonText("Option", "boss_attack_method", "{BOSS_NAME}", textCommonBossName3);
				break;
			}
			}
			if (this.m_headerTextLabel != null)
			{
				this.m_headerTextLabel.text = text;
			}
			if (this.m_mainTextLabel != null)
			{
				this.m_mainTextLabel.text = HudTutorial.GetExplainText(this.m_scrollInfo.HudId, this.m_pageIndex);
			}
		}
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x000D6D10 File Offset: 0x000D4F10
	private void SetNextBtn()
	{
		if (this.m_scrollInfo != null && this.m_nextBtn != null)
		{
			bool flag = this.m_pageCount > 1;
			this.m_nextBtn.SetActive(flag);
			if (flag)
			{
				this.SetPage();
			}
		}
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x000D6D5C File Offset: 0x000D4F5C
	private void SetPage()
	{
		if (this.m_nextTextLabel != null)
		{
			int num = this.m_pageIndex + 1;
			this.m_nextTextLabel.text = num + "/" + this.m_pageCount;
		}
		if (this.m_rightImageBtn != null)
		{
			this.m_rightImageBtn.isEnabled = (this.m_pageCount != this.m_pageIndex + 1);
		}
		if (this.m_leftImageBtn != null)
		{
			this.m_leftImageBtn.isEnabled = (this.m_pageIndex != 0);
		}
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x000D6E00 File Offset: 0x000D5000
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

	// Token: 0x0400206F RID: 8303
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x04002070 RID: 8304
	[SerializeField]
	private GameObject m_nextBtn;

	// Token: 0x04002071 RID: 8305
	[SerializeField]
	private UIImageButton m_rightImageBtn;

	// Token: 0x04002072 RID: 8306
	[SerializeField]
	private UIImageButton m_leftImageBtn;

	// Token: 0x04002073 RID: 8307
	[SerializeField]
	private UITexture m_texture;

	// Token: 0x04002074 RID: 8308
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04002075 RID: 8309
	[SerializeField]
	private UILabel m_mainTextLabel;

	// Token: 0x04002076 RID: 8310
	[SerializeField]
	private UILabel m_nextTextLabel;

	// Token: 0x04002077 RID: 8311
	private ResourceSceneLoader m_loaderComponent;

	// Token: 0x04002078 RID: 8312
	private bool m_isEnd;

	// Token: 0x04002079 RID: 8313
	private bool m_isLoading;

	// Token: 0x0400207A RID: 8314
	private bool m_playOpen;

	// Token: 0x0400207B RID: 8315
	private int m_pageCount;

	// Token: 0x0400207C RID: 8316
	private int m_pageIndex;

	// Token: 0x0400207D RID: 8317
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x0400207E RID: 8318
	private window_tutorial.ScrollInfo m_scrollInfo;

	// Token: 0x0400207F RID: 8319
	private string m_sceneName;
}
