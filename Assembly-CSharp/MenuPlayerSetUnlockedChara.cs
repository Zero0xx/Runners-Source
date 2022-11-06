using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class MenuPlayerSetUnlockedChara : MonoBehaviour
{
	// Token: 0x0600242F RID: 9263 RVA: 0x000D8BFC File Offset: 0x000D6DFC
	public void Setup(CharaType charaType, GameObject pageRootObject)
	{
		this.m_charaType = charaType;
		this.m_pageRootObject = pageRootObject;
	}

	// Token: 0x06002430 RID: 9264 RVA: 0x000D8C0C File Offset: 0x000D6E0C
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		this.m_ringCost = 0;
		this.m_redStartRingCost = 0;
		this.OnEnable();
		this.m_currentDeckSetStock = DeckUtil.GetDeckCurrentStockIndex();
		this.SetupTabView();
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x000D8CA4 File Offset: 0x000D6EA4
	private void LateUpdate()
	{
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x000D8CA8 File Offset: 0x000D6EA8
	private void OnEnable()
	{
		if (this.m_pageRootObject == null)
		{
			return;
		}
		ServerPlayerState playerState = ServerInterface.PlayerState;
		ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_charaType);
		if (serverCharacterState == null)
		{
			return;
		}
		bool flag = false;
		if (serverCharacterState.IsUnlocked)
		{
			flag = true;
		}
		ServerCharacterState.LockCondition lockCondition = ServerCharacterState.LockCondition.RING_OR_RED_STAR_RING;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(this.m_charaType);
			if (serverCharacterState2 != null)
			{
				lockCondition = serverCharacterState2.Condition;
			}
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "pattern_lock");
		if (gameObject != null)
		{
			gameObject.SetActive(!flag);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "Btn_pay_ring");
		if (gameObject2 != null)
		{
			if (lockCondition == ServerCharacterState.LockCondition.RING_OR_RED_STAR_RING)
			{
				gameObject2.SetActive(true);
				UIButtonMessage uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickRingCostButton";
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "Btn_pay_rsring");
		if (gameObject3 != null)
		{
			if (lockCondition == ServerCharacterState.LockCondition.RING_OR_RED_STAR_RING)
			{
				gameObject3.SetActive(true);
				UIButtonMessage uibuttonMessage2 = gameObject3.AddComponent<UIButtonMessage>();
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "OnClickRedStartRingCostButton";
			}
			else
			{
				gameObject3.SetActive(false);
			}
		}
		this.InitCost();
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_pageRootObject, "Lbl_lock_txt");
		if (uilabel != null)
		{
			string text = string.Empty;
			string cellName = "recommend_chara_unlock_text_" + CharaName.Name[(int)this.m_charaType];
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", cellName).text;
			uilabel.text = text;
		}
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x000D8E6C File Offset: 0x000D706C
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

	// Token: 0x06002434 RID: 9268 RVA: 0x000D8F64 File Offset: 0x000D7164
	private void InitCost()
	{
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState == null)
		{
			return;
		}
		ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_charaType);
		if (serverCharacterState == null)
		{
			return;
		}
		bool active = false;
		ServerItem serverItem = new ServerItem(this.m_charaType);
		int id = (int)serverItem.id;
		ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.CharacterUpgradeCost, id);
		int num;
		int num2;
		if (campaignInSession != null)
		{
			int iContent = campaignInSession.iContent;
			int iSubContent = campaignInSession.iSubContent;
			num = iContent;
			num2 = iSubContent;
			active = true;
		}
		else
		{
			num = serverCharacterState.Cost;
			num2 = serverCharacterState.NumRedRings;
		}
		if (this.m_ringCost != num)
		{
			this.m_ringCost = num;
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "img_icon_sale_ring");
			if (gameObject != null)
			{
				gameObject.SetActive(active);
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_pageRootObject, "Lbl_pay_ring");
			if (uilabel != null)
			{
				uilabel.text = this.m_ringCost.ToString();
			}
		}
		if (this.m_redStartRingCost != num2)
		{
			this.m_redStartRingCost = num2;
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "img_icon_sale_rsring");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(active);
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_pageRootObject, "Lbl_pay_rsring");
			if (uilabel2 != null)
			{
				uilabel2.text = this.m_redStartRingCost.ToString();
			}
		}
	}

	// Token: 0x06002435 RID: 9269 RVA: 0x000D90CC File Offset: 0x000D72CC
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
			if (signal == 100)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitRingButtonPressed)));
				return TinyFsmState.End();
			}
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitRedStartRingButtonPressed)));
			return TinyFsmState.End();
		case 4:
		{
			this.InitCost();
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

	// Token: 0x06002436 RID: 9270 RVA: 0x000D9194 File Offset: 0x000D7394
	private TinyFsmState StateWaitRingButtonPressed(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				int ringCost = this.m_ringCost;
				int ringCount = (int)SaveDataManager.Instance.ItemData.RingCount;
				if (ringCost <= ringCount)
				{
					this.UnLock(new ServerItem(ServerItem.Id.RING));
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFailedPurchaseRing)));
				}
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x000D9290 File Offset: 0x000D7490
	private TinyFsmState StateWaitRedStartRingButtonPressed(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				int redStartRingCost = this.m_redStartRingCost;
				int redRingCount = (int)SaveDataManager.Instance.ItemData.RedRingCount;
				if (redStartRingCost <= redRingCount)
				{
					this.UnLock(new ServerItem(ServerItem.Id.RSRING));
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFailedPurchaseRedRing)));
				}
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x000D938C File Offset: 0x000D758C
	private TinyFsmState StateFailedPurchaseRing(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RingMissing",
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_text").text,
				anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC",
				buttonType = GeneralWindow.ButtonType.ShopCancel,
				finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowClosedCallback)
			});
			this.m_isWindowClose = false;
			return TinyFsmState.End();
		case 4:
			if (this.m_isWindowClose)
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RING_TO_SHOP, false);
				}
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x000D949C File Offset: 0x000D769C
	private TinyFsmState StateFailedPurchaseRedRing(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.name = "RingMissing";
			info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption").text;
			if (ServerInterface.IsRSREnable())
			{
				info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption_text").text;
				info.buttonType = GeneralWindow.ButtonType.ShopCancel;
			}
			else
			{
				info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption_text_2").text;
				info.buttonType = GeneralWindow.ButtonType.Ok;
			}
			info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
			info.finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowClosedCallback);
			GeneralWindow.Create(info);
			this.m_isWindowClose = false;
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isWindowClose)
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
				}
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600243A RID: 9274 RVA: 0x000D95DC File Offset: 0x000D77DC
	private void UnLock(ServerItem serverItem)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUnlockedCharacter(this.m_charaType, serverItem, base.gameObject);
		}
		else
		{
			CharaData charaData = SaveDataManager.Instance.CharaData;
			charaData.Status[(int)this.m_charaType] = 1;
			SaveDataManager.Instance.ItemData.RingCount -= (uint)this.m_ringCost;
			this.ServerUnlockedCharacter_Succeeded(null);
		}
	}

	// Token: 0x0600243B RID: 9275 RVA: 0x000D9650 File Offset: 0x000D7850
	private void OnClickRingCostButton()
	{
		GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
		info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_caption").text;
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_ring_text");
		int ringCost = this.m_ringCost;
		text.ReplaceTag("{RING_COST}", ringCost.ToString());
		info.message = text.text;
		info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
		info.buttonType = GeneralWindow.ButtonType.YesNo;
		GeneralWindow.Create(info);
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600243C RID: 9276 RVA: 0x000D96F8 File Offset: 0x000D78F8
	private void OnClickRedStartRingCostButton()
	{
		GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
		info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_caption").text;
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_rsring_text");
		int redStartRingCost = this.m_redStartRingCost;
		text.ReplaceTag("{RING_COST}", redStartRingCost.ToString());
		info.message = text.text;
		info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
		info.buttonType = GeneralWindow.ButtonType.YesNo;
		GeneralWindow.Create(info);
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600243D RID: 9277 RVA: 0x000D97A0 File Offset: 0x000D79A0
	private void ServerUnlockedCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		Animation component = this.m_pageRootObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Forward);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.BuyAnimEndCallback), true);
			}
		}
		else
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "pattern_lock");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		GameObject playerSetRoot = MenuPlayerSetUtil.GetPlayerSetRoot();
		MenuPlayerSetContents component2 = playerSetRoot.GetComponent<MenuPlayerSetContents>();
		if (component2 != null)
		{
			component2.UnlockedChara(this.m_charaType);
		}
		SoundManager.SePlay("sys_buy", "SE");
	}

	// Token: 0x0600243E RID: 9278 RVA: 0x000D9858 File Offset: 0x000D7A58
	private void GeneralWindowClosedCallback()
	{
		this.m_isWindowClose = true;
	}

	// Token: 0x0600243F RID: 9279 RVA: 0x000D9864 File Offset: 0x000D7A64
	private void BuyAnimEndCallback()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "pattern_lock");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x040020BA RID: 8378
	private TinyFsmBehavior m_fsm;

	// Token: 0x040020BB RID: 8379
	private CharaType m_charaType;

	// Token: 0x040020BC RID: 8380
	private GameObject m_pageRootObject;

	// Token: 0x040020BD RID: 8381
	private int m_ringCost;

	// Token: 0x040020BE RID: 8382
	private int m_redStartRingCost;

	// Token: 0x040020BF RID: 8383
	private bool m_isWindowClose = true;

	// Token: 0x040020C0 RID: 8384
	private int m_currentDeckSetStock;

	// Token: 0x040020C1 RID: 8385
	private List<GameObject> m_deckObjects;

	// Token: 0x020004CA RID: 1226
	private enum EventSignal
	{
		// Token: 0x040020C3 RID: 8387
		BUTTON_RING_BUTTON_PRESSED = 100,
		// Token: 0x040020C4 RID: 8388
		BUTTON_RS_RING_BUTTON_PRESSED
	}
}
