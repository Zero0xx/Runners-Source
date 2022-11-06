using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
public class window_tutorial : WindowBase
{
	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000D60EC File Offset: 0x000D42EC
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x000D60F4 File Offset: 0x000D42F4
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		base.enabled = false;
		if (this.m_closeBtn != null)
		{
			UIPlayAnimation component = this.m_closeBtn.GetComponent<UIPlayAnimation>();
			if (component != null)
			{
				EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedCloseAnimationCallback), false);
			}
			UIButtonMessage uibuttonMessage = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = this.m_closeBtn.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.enabled = true;
				uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickCloseButton";
			}
		}
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "tutorial");
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component2 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component2;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		if (ServerInterface.MileageMapState != null)
		{
			if (!HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_2) && ServerInterface.MileageMapState.m_episode > 2)
			{
				HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_2);
			}
			if (!HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_3) && ServerInterface.MileageMapState.m_episode > 3)
			{
				HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_3);
			}
		}
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x000D6254 File Offset: 0x000D4454
	private void OnClickCloseButton()
	{
		this.SetCloseBtnColliderTrigger(true);
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x000D6270 File Offset: 0x000D4470
	private void OnFinishedInAnimationCallback()
	{
		this.SetCloseBtnColliderTrigger(false);
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x000D627C File Offset: 0x000D447C
	private void OnFinishedCloseAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x000D6288 File Offset: 0x000D4488
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		if (this.m_inited)
		{
			this.UpdateRectItemStorage();
		}
		else
		{
			base.StartCoroutine(this.DelayUpdateRectItemStorage());
		}
		if (this.m_uiAnimation != null)
		{
			EventDelegate.Add(this.m_uiAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedInAnimationCallback), false);
			this.m_uiAnimation.Play(true);
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x000D630C File Offset: 0x000D450C
	public IEnumerator DelayUpdateRectItemStorage()
	{
		int waite_frame = 1;
		while (waite_frame > 0)
		{
			waite_frame--;
			yield return null;
		}
		this.UpdateRectItemStorage();
		this.m_inited = true;
		yield break;
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x000D6328 File Offset: 0x000D4528
	private void UpdateRectItemStorage()
	{
		if (this.m_itemStorage != null)
		{
			this.m_scrollInfos.Clear();
			this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.TUTORIAL, CharaType.UNKNOWN));
			this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.QUICK, CharaType.UNKNOWN));
			if (SystemSaveManager.Instance != null)
			{
				SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
				if (systemdata != null)
				{
					this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.CHARA, CharaType.SONIC));
					for (int i = 1; i < 29; i++)
					{
						SystemData.CharaTutorialFlagStatus characterSaveDataFlagStatus = CharaTypeUtil.GetCharacterSaveDataFlagStatus((CharaType)i);
						if (systemdata.IsFlagStatus(characterSaveDataFlagStatus))
						{
							this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.CHARA, (CharaType)i));
						}
					}
					this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.BOSS_MAP_1, CharaType.UNKNOWN));
					SystemData.FlagStatus bossSaveDataFlagStatus = BossTypeUtil.GetBossSaveDataFlagStatus(BossType.MAP2);
					if (systemdata.IsFlagStatus(bossSaveDataFlagStatus))
					{
						this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.BOSS_MAP_2, CharaType.UNKNOWN));
					}
					SystemData.FlagStatus bossSaveDataFlagStatus2 = BossTypeUtil.GetBossSaveDataFlagStatus(BossType.MAP3);
					if (systemdata.IsFlagStatus(bossSaveDataFlagStatus2))
					{
						this.m_scrollInfos.Add(new window_tutorial.ScrollInfo(this, window_tutorial.DisplayType.BOSS_MAP_3, CharaType.UNKNOWN));
					}
				}
			}
			this.m_itemStorage.maxItemCount = this.m_scrollInfos.Count;
			this.m_itemStorage.maxRows = this.m_scrollInfos.Count;
			this.m_itemStorage.Restart();
			ui_option_tutorial_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_tutorial_scroll>(true);
			int num = componentsInChildren.Length;
			for (int j = 0; j < num; j++)
			{
				componentsInChildren[j].gameObject.SetActive(true);
				componentsInChildren[j].UpdateView(this.m_scrollInfos[j]);
			}
		}
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x000D64D0 File Offset: 0x000D46D0
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		bool flag = false;
		ui_option_tutorial_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_tutorial_scroll>(true);
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			if (componentsInChildren[i].OpenWindow)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			UIButtonMessage component = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x000D6550 File Offset: 0x000D4750
	public void SetCloseBtnColliderTrigger(bool triggerFlag)
	{
		if (this.m_closeBtn != null)
		{
			BoxCollider component = this.m_closeBtn.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.isTrigger = triggerFlag;
			}
		}
		ui_option_tutorial_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_tutorial_scroll>(true);
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(componentsInChildren[i].gameObject, "Btn_character");
			if (boxCollider != null)
			{
				boxCollider.isTrigger = triggerFlag;
			}
		}
	}

	// Token: 0x0400205B RID: 8283
	private readonly BossType[] TUTORIAL_BOSS_TYPE_TABLE = new BossType[]
	{
		BossType.MAP2,
		BossType.MAP3
	};

	// Token: 0x0400205C RID: 8284
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x0400205D RID: 8285
	[SerializeField]
	private UIRectItemStorage m_itemStorage;

	// Token: 0x0400205E RID: 8286
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x0400205F RID: 8287
	private bool m_isEnd;

	// Token: 0x04002060 RID: 8288
	private bool m_inited;

	// Token: 0x04002061 RID: 8289
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002062 RID: 8290
	private List<window_tutorial.ScrollInfo> m_scrollInfos = new List<window_tutorial.ScrollInfo>();

	// Token: 0x020004B4 RID: 1204
	public enum DisplayType
	{
		// Token: 0x04002064 RID: 8292
		TUTORIAL,
		// Token: 0x04002065 RID: 8293
		QUICK,
		// Token: 0x04002066 RID: 8294
		CHARA,
		// Token: 0x04002067 RID: 8295
		BOSS_MAP_1,
		// Token: 0x04002068 RID: 8296
		BOSS_MAP_2,
		// Token: 0x04002069 RID: 8297
		BOSS_MAP_3,
		// Token: 0x0400206A RID: 8298
		UNKNOWN
	}

	// Token: 0x020004B5 RID: 1205
	public class ScrollInfo
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x000D65DC File Offset: 0x000D47DC
		public ScrollInfo()
		{
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000D65FC File Offset: 0x000D47FC
		public ScrollInfo(window_tutorial window, window_tutorial.DisplayType dispType, CharaType charaType = CharaType.UNKNOWN)
		{
			this.m_parentWindow = window;
			this.m_dispType = dispType;
			this.m_charaType = charaType;
			switch (this.m_dispType)
			{
			case window_tutorial.DisplayType.QUICK:
				this.m_hudId = HudTutorial.Id.QUICK_1;
				break;
			case window_tutorial.DisplayType.CHARA:
				this.m_hudId = CharaTypeUtil.GetCharacterTutorialID(this.m_charaType);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_1:
				this.m_hudId = HudTutorial.Id.MAPBOSS_1;
				break;
			case window_tutorial.DisplayType.BOSS_MAP_2:
				this.m_hudId = HudTutorial.Id.MAPBOSS_2;
				break;
			case window_tutorial.DisplayType.BOSS_MAP_3:
				this.m_hudId = HudTutorial.Id.MAPBOSS_3;
				break;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000D66AC File Offset: 0x000D48AC
		public window_tutorial.DisplayType DispType
		{
			get
			{
				return this.m_dispType;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x000D66B4 File Offset: 0x000D48B4
		public CharaType Chara
		{
			get
			{
				return this.m_charaType;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000D66BC File Offset: 0x000D48BC
		public HudTutorial.Id HudId
		{
			get
			{
				return this.m_hudId;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000D66C4 File Offset: 0x000D48C4
		public window_tutorial Parent
		{
			get
			{
				return this.m_parentWindow;
			}
		}

		// Token: 0x0400206B RID: 8299
		private window_tutorial.DisplayType m_dispType = window_tutorial.DisplayType.UNKNOWN;

		// Token: 0x0400206C RID: 8300
		private CharaType m_charaType = CharaType.UNKNOWN;

		// Token: 0x0400206D RID: 8301
		private HudTutorial.Id m_hudId = HudTutorial.Id.NONE;

		// Token: 0x0400206E RID: 8302
		private window_tutorial m_parentWindow;
	}
}
