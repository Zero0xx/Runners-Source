using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004CB RID: 1227
public class MenuPlayerSetCharaButton : MonoBehaviour
{
	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x06002441 RID: 9281 RVA: 0x000D98A8 File Offset: 0x000D7AA8
	// (set) Token: 0x06002442 RID: 9282 RVA: 0x000D98B0 File Offset: 0x000D7AB0
	public bool AnimEnd
	{
		get
		{
			return this.m_animEnd;
		}
		private set
		{
		}
	}

	// Token: 0x06002443 RID: 9283 RVA: 0x000D98B4 File Offset: 0x000D7AB4
	public void Setup(CharaType charaType, GameObject pageRootObject)
	{
		this.m_charaType = charaType;
		this.m_pageRootObject = pageRootObject;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "Btn_player_main");
		if (gameObject != null)
		{
			UIButtonMessage uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnSelected";
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_speacies");
		if (uisprite != null)
		{
			uisprite.spriteName = HudUtility.GetCharaAttributeSpriteName(this.m_charaType);
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_genus");
		if (uisprite2 != null)
		{
			uisprite2.spriteName = HudUtility.GetTeamAttributeSpriteName(this.m_charaType);
		}
		this.m_currentDeckSetStock = DeckUtil.GetDeckCurrentStockIndex();
		this.SetupTabView();
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x000D9970 File Offset: 0x000D7B70
	private void SetupTabView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "deck_tab");
		if (gameObject != null)
		{
			if (this.m_deckObjects != null)
			{
				this.m_deckObjects.Clear();
			}
			this.m_deckObjects = new List<GameObject>();
			for (int i = 0; i < 10; i++)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "tab_" + (i + 1));
				if (!(gameObject2 != null))
				{
					break;
				}
				this.m_deckObjects.Add(gameObject2);
			}
			if (this.m_deckObjects.Count > 0 && this.m_deckObjects.Count > this.m_currentDeckSetStock)
			{
				for (int j = 0; j < this.m_deckObjects.Count; j++)
				{
					this.m_deckObjects[j].SetActive(this.m_currentDeckSetStock == j);
				}
			}
		}
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x000D9A68 File Offset: 0x000D7C68
	public void UpdateRibbon()
	{
		this.m_ribbon = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_pageRootObject, "img_ribbon");
		if (this.m_ribbon != null)
		{
			PlayerData playerData = SaveDataManager.Instance.PlayerData;
			if (this.m_charaType == playerData.MainChara)
			{
				this.m_ribbon.gameObject.SetActive(true);
				this.m_ribbon.spriteName = "ui_mm_player_ribbon_0";
			}
			else if (this.m_charaType == playerData.SubChara)
			{
				this.m_ribbon.gameObject.SetActive(true);
				this.m_ribbon.spriteName = "ui_mm_player_ribbon_1";
			}
			else
			{
				this.m_ribbon.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002446 RID: 9286 RVA: 0x000D9B28 File Offset: 0x000D7D28
	private void Start()
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_pageRootObject, "img_player_tex");
		if (uitexture != null)
		{
			TextureRequestChara request = new TextureRequestChara(this.m_charaType, uitexture);
			TextureAsyncLoadManager.Instance.Request(request);
		}
		this.m_charaName = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_pageRootObject, "Lbl_player_name");
		if (this.m_charaName != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", CharaName.Name[(int)this.m_charaType]).text;
			this.m_charaName.text = text;
		}
		this.m_charaLevel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_pageRootObject, "Lbl_player_lv");
		if (this.m_charaLevel != null)
		{
			int totalLevel = MenuPlayerSetUtil.GetTotalLevel(this.m_charaType);
			this.m_charaLevel.text = TextUtility.GetTextLevel(string.Format("{0:000}", totalLevel));
		}
		this.UpdateRibbon();
		this.m_fsm = base.gameObject.AddComponent<TinyFsmBehavior>();
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		this.m_objectContainer = base.gameObject.AddComponent<UIObjectContainer>();
		List<GameObject> list = new List<GameObject>();
		GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_player_main");
		list.Add(GameObjectUtil.FindChildGameObject(parent, "eff_0"));
		list.Add(GameObjectUtil.FindChildGameObject(parent, "eff_1"));
		list.Add(GameObjectUtil.FindChildGameObject(parent, "img_player_main_sale_word"));
		this.m_objectContainer.Objects = list.ToArray();
	}

	// Token: 0x06002447 RID: 9287 RVA: 0x000D9CD0 File Offset: 0x000D7ED0
	public void LevelUp(MenuPlayerSetCharaButton.AnimEndCallback callback)
	{
		this.m_animEndCallback = callback;
		this.m_animEnd = false;
		int totalLevel = MenuPlayerSetUtil.GetTotalLevel(this.m_charaType);
		this.m_charaLevel.text = TextUtility.GetTextLevel(string.Format("{0:000}", totalLevel));
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "Btn_player_main");
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, Direction.Forward);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.LevelUpAnimEndCallback), true);
			}
			if (this.m_objectContainer != null)
			{
				this.m_objectContainer.SetActive(true);
			}
		}
		AchievementManager.RequestUpdate();
	}

	// Token: 0x06002448 RID: 9288 RVA: 0x000D9D84 File Offset: 0x000D7F84
	public void SkipLevelUp()
	{
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "Btn_player_main");
		if (animation != null)
		{
			foreach (object obj in animation)
			{
				AnimationState animationState = (AnimationState)obj;
				if (!(animationState == null))
				{
					animationState.time = animationState.length * 0.99f;
				}
			}
		}
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x000D9E28 File Offset: 0x000D8028
	public void OnSelected()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		if (HudMenuUtility.IsTutorial_11())
		{
			HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.CHARA_MAIN);
		}
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x000D9E6C File Offset: 0x000D806C
	private void LateUpdate()
	{
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000D9E70 File Offset: 0x000D8070
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
		{
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			int playableCharaCount = MenuPlayerSetUtil.GetPlayableCharaCount();
			if (playableCharaCount <= 1)
			{
				GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
				info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "recommend_chara_unlock_caption").text;
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "recommend_chara_unlock_text").text;
				info.message = text;
				info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
				info.buttonType = GeneralWindow.ButtonType.Ok;
				GeneralWindow.Create(info);
			}
			else
			{
				this.m_charaChangeWindow = PlayerSetWindowUIWithButton.Create(this.m_charaType);
				if (this.m_fsm != null)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCharaChangeWindow)));
				}
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
			if (this.m_currentDeckSetStock != deckCurrentStockIndex)
			{
				this.m_currentDeckSetStock = deckCurrentStockIndex;
				this.SetupTabView();
			}
			return TinyFsmState.End();
		}
		}
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000D9F9C File Offset: 0x000D819C
	private TinyFsmState StateRecommendUnlockChara(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed && this.m_fsm != null)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x000DA028 File Offset: 0x000D8228
	private TinyFsmState StateCharaChangeWindow(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_charaChangeWindow.isClickMain)
			{
				if (this.m_fsm != null)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
				}
				GameObject playerSetRoot = MenuPlayerSetUtil.GetPlayerSetRoot();
				MenuPlayerSetContents component = playerSetRoot.GetComponent<MenuPlayerSetContents>();
				if (component != null)
				{
					component.ChangeMainChara(this.m_charaType);
				}
			}
			if (this.m_charaChangeWindow.isClickSub)
			{
				if (this.m_fsm != null)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
				}
				GameObject playerSetRoot2 = MenuPlayerSetUtil.GetPlayerSetRoot();
				MenuPlayerSetContents component2 = playerSetRoot2.GetComponent<MenuPlayerSetContents>();
				if (component2 != null)
				{
					component2.ChangeSubChara(this.m_charaType);
				}
			}
			if (this.m_charaChangeWindow.isClickCancel && this.m_fsm != null)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x000DA17C File Offset: 0x000D837C
	private void LevelUpAnimEndCallback()
	{
		this.m_animEnd = true;
		if (this.m_objectContainer != null)
		{
			this.m_objectContainer.SetActive(false);
		}
		if (this.m_animEndCallback != null)
		{
			this.m_animEndCallback();
			this.m_animEndCallback = null;
		}
	}

	// Token: 0x040020C5 RID: 8389
	private TinyFsmBehavior m_fsm;

	// Token: 0x040020C6 RID: 8390
	private GameObject m_pageRootObject;

	// Token: 0x040020C7 RID: 8391
	private CharaType m_charaType;

	// Token: 0x040020C8 RID: 8392
	private UISprite m_charaIcon;

	// Token: 0x040020C9 RID: 8393
	private UILabel m_charaName;

	// Token: 0x040020CA RID: 8394
	private UILabel m_charaLevel;

	// Token: 0x040020CB RID: 8395
	private UISprite m_ribbon;

	// Token: 0x040020CC RID: 8396
	[SerializeField]
	private UIObjectContainer m_objectContainer;

	// Token: 0x040020CD RID: 8397
	private PlayerSetWindowUIWithButton m_charaChangeWindow;

	// Token: 0x040020CE RID: 8398
	private MenuPlayerSetCharaButton.AnimEndCallback m_animEndCallback;

	// Token: 0x040020CF RID: 8399
	private int m_currentDeckSetStock;

	// Token: 0x040020D0 RID: 8400
	private List<GameObject> m_deckObjects;

	// Token: 0x040020D1 RID: 8401
	private bool m_animEnd = true;

	// Token: 0x020004CC RID: 1228
	private enum EventSignal
	{
		// Token: 0x040020D3 RID: 8403
		CHARA_CHANGE = 100
	}

	// Token: 0x02000A94 RID: 2708
	// (Invoke) Token: 0x0600488A RID: 18570
	public delegate void AnimEndCallback();
}
