using System;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class ChaoMergeWindow : WindowBase
{
	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001956 RID: 6486 RVA: 0x00093188 File Offset: 0x00091388
	// (set) Token: 0x06001957 RID: 6487 RVA: 0x00093190 File Offset: 0x00091390
	public bool isSetuped
	{
		get
		{
			return this.m_isSetup;
		}
		set
		{
			this.m_isSetup = false;
		}
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x0009319C File Offset: 0x0009139C
	public void PlayStart(int chaoId, int level, int rarity, RouletteUtility.AchievementType achievement = RouletteUtility.AchievementType.NONE)
	{
		RouletteManager.OpenRouletteWindow();
		this.m_chaoId = chaoId;
		this.m_level = level;
		this.m_backKeyVaild = false;
		this.m_isEnd = false;
		this.m_achievementType = achievement;
		if (!this.m_isSetup)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_ok");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OkButtonClickedCallback";
			}
			UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "skip_collider");
			if (uibuttonMessage2 != null)
			{
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "SkipButtonClickedCallback";
			}
			this.m_SeFlagFog = new HudFlagWatcher();
			GameObject watchObject = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag_fog");
			this.m_SeFlagFog.Setup(watchObject, new HudFlagWatcher.ValueChangeCallback(this.SeFlagFogCallback));
			this.m_isSetup = true;
		}
		base.gameObject.SetActive(true);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
		}
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			component.Stop();
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_menu_chao_merge_Window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationEndCallback), true);
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
			if (uiimageButton != null)
			{
				uiimageButton.isEnabled = false;
			}
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_chao_bg_rare");
		if (uisprite != null)
		{
			ChaoWindowUtility.ChangeRaritySpriteFromServerChaoId(uisprite, this.m_chaoId);
		}
		int idFromServerId = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
		foreach (string name in ChaoMergeWindow.ChaoIconNameList)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, name);
			if (uitexture != null)
			{
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
				ChaoTextureManager.Instance.GetTexture(idFromServerId, info);
			}
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_chao_1_lv");
		if (uilabel != null)
		{
			uilabel.text = TextUtility.GetTextLevel((this.m_level - 1).ToString());
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_chao_2_lv");
		if (uilabel2 != null)
		{
			uilabel2.text = TextUtility.GetTextLevel("0");
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_chao_3_lv");
		if (uilabel3 != null)
		{
			uilabel3.text = TextUtility.GetTextLevel(this.m_level.ToString());
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_chao_name");
		if (uilabel4 != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", ChaoWindowUtility.GetChaoLabelName(this.m_chaoId)).text;
			uilabel4.text = text;
		}
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_chao_lv");
		if (uilabel5 != null)
		{
			uilabel5.text = TextUtility.GetTextLevel(this.m_level.ToString());
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_type_icon");
		if (uisprite2 != null)
		{
			ChaoData chaoData = ChaoTable.GetChaoData(idFromServerId);
			if (chaoData != null)
			{
				CharacterAttribute charaAtribute = chaoData.charaAtribute;
				string spriteName = "ui_chao_set_type_icon_" + charaAtribute.ToString().ToLower();
				uisprite2.spriteName = spriteName;
			}
		}
		UILabel uilabel6 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_type");
		if (uilabel6 != null)
		{
			ChaoData chaoData2 = ChaoTable.GetChaoData(idFromServerId);
			if (chaoData2 != null)
			{
				CharacterAttribute charaAtribute2 = chaoData2.charaAtribute;
				string cellName = charaAtribute2.ToString().ToLower();
				string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaAtribute", cellName).text;
				uilabel6.text = text2;
			}
		}
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_bonus_icon");
		if (uisprite3 != null)
		{
			uisprite3.enabled = false;
		}
		UILabel uilabel7 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_bonus");
		if (uilabel7 != null)
		{
			uilabel7.text = HudUtility.GetChaoGrowAbilityText(idFromServerId, -1);
		}
		SoundManager.SePlay("sys_window_open", "SE");
		UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "window_chaoset_alpha_clip");
		if (uidraggablePanel != null)
		{
			uidraggablePanel.ResetPosition();
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_4");
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_6");
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_7");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(true);
		}
		if (gameObject3 != null)
		{
			gameObject3.SetActive(true);
		}
		if (gameObject4 != null)
		{
			gameObject4.SetActive(true);
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001959 RID: 6489 RVA: 0x0009369C File Offset: 0x0009189C
	public bool IsPlayEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x000936A4 File Offset: 0x000918A4
	private void Start()
	{
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x000936A8 File Offset: 0x000918A8
	private void Update()
	{
		if (this.m_SeFlagFog != null)
		{
			this.m_SeFlagFog.Update();
		}
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000936C0 File Offset: 0x000918C0
	private void SeFlagFogCallback(float newValue, float prevValue)
	{
		if (newValue == 1f)
		{
			ChaoWindowUtility.PlaySEChaoUnite(this.m_chaoId);
		}
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x000936D8 File Offset: 0x000918D8
	private void OkButtonClickedCallback()
	{
		RouletteManager.CloseRouletteWindow();
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_achievementType != RouletteUtility.AchievementType.NONE)
		{
			RouletteManager.RouletteGetWindowClose(this.m_achievementType, RouletteUtility.NextType.NONE);
			this.m_achievementType = RouletteUtility.AchievementType.NONE;
		}
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_menu_chao_merge_Window_outro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationEndCallback), true);
		}
		this.m_backKeyVaild = false;
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x00093760 File Offset: 0x00091960
	private void InAnimationEndCallback()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
		if (uiimageButton != null)
		{
			uiimageButton.isEnabled = true;
		}
		this.m_backKeyVaild = true;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000937BC File Offset: 0x000919BC
	private void OutAnimationEndCallback()
	{
		this.DeleteChaoTexture();
		base.gameObject.SetActive(false);
		this.m_isEnd = true;
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000937D8 File Offset: 0x000919D8
	private void SkipButtonClickedCallback()
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			foreach (object obj in component)
			{
				AnimationState animationState = (AnimationState)obj;
				if (!(animationState == null))
				{
					animationState.time = animationState.length * 0.99f;
				}
			}
		}
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x00093878 File Offset: 0x00091A78
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		foreach (string name in ChaoMergeWindow.ChaoIconNameList)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, name);
			if (uitexture != null)
			{
				uitexture.enabled = true;
				uitexture.mainTexture = data.tex;
			}
		}
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000938D0 File Offset: 0x00091AD0
	private void DeleteChaoTexture()
	{
		foreach (string name in ChaoMergeWindow.ChaoIconNameList)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, name);
			if (uitexture != null)
			{
				uitexture.enabled = false;
				uitexture.mainTexture = null;
			}
		}
		ChaoTextureManager.Instance.RemoveChaoTexture(this.m_chaoId);
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x00093934 File Offset: 0x00091B34
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (this.m_backKeyVaild)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_ok");
			if (gameObject != null)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
	}

	// Token: 0x040016C2 RID: 5826
	private bool m_isSetup;

	// Token: 0x040016C3 RID: 5827
	private int m_chaoId;

	// Token: 0x040016C4 RID: 5828
	private int m_level;

	// Token: 0x040016C5 RID: 5829
	private int m_rarity;

	// Token: 0x040016C6 RID: 5830
	private bool m_backKeyVaild;

	// Token: 0x040016C7 RID: 5831
	private bool m_isEnd;

	// Token: 0x040016C8 RID: 5832
	private RouletteUtility.AchievementType m_achievementType;

	// Token: 0x040016C9 RID: 5833
	private static readonly string[] ChaoIconNameList = new string[]
	{
		"img_chao_1",
		"img_chao_2",
		"img_chao_3"
	};

	// Token: 0x040016CA RID: 5834
	private HudFlagWatcher m_SeFlagFog;
}
