using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UI;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class GlowUpWindow : MonoBehaviour
{
	// Token: 0x06001B58 RID: 7000 RVA: 0x000A1B24 File Offset: 0x0009FD24
	public void PlayStart(GlowUpWindow.ExpType expType)
	{
		base.gameObject.SetActive(true);
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, false);
		}
		CharaType[] array = new CharaType[]
		{
			SaveDataManager.Instance.PlayerData.MainChara,
			SaveDataManager.Instance.PlayerData.SubChara
		};
		bool flag = true;
		for (int i = 0; i < 2; i++)
		{
			string name = GlowUpWindow.CharaPlateName[i];
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(gameObject == null))
			{
				if (this.m_charaPlate[i] == null)
				{
					this.m_charaPlate[i] = gameObject.AddComponent<GlowUpCharacter>();
				}
				GlowUpCharaBaseInfo glowUpCharaBaseInfo = new GlowUpCharaBaseInfo();
				CharaType charaType = array[i];
				if (ServerInterface.LoggedInServerInterface != null)
				{
					ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
					if (serverCharacterState != null)
					{
						glowUpCharaBaseInfo.charaType = charaType;
						glowUpCharaBaseInfo.level = this.CalcCharacterTotalLevel(serverCharacterState.OldAbiltyLevel);
						glowUpCharaBaseInfo.levelUpCost = serverCharacterState.OldCost;
						glowUpCharaBaseInfo.currentExp = serverCharacterState.OldExp;
						bool flag2 = false;
						ServerPlayCharacterState serverPlayCharacterState = ServerInterface.PlayerState.PlayCharacterState(charaType);
						if (serverPlayCharacterState != null)
						{
							flag2 = true;
						}
						glowUpCharaBaseInfo.IsActive = flag2;
						if (flag2 && serverCharacterState.OldStatus != ServerCharacterState.CharacterStatus.MaxLevel)
						{
							flag = false;
						}
					}
				}
				this.m_charaPlate[i].Setup(glowUpCharaBaseInfo);
			}
		}
		string text = string.Empty;
		string text2 = string.Empty;
		string str = string.Empty;
		if (EventManager.Instance.IsRaidBossStage())
		{
			bool flag3 = false;
			EventManager instance2 = EventManager.Instance;
			if (instance2 != null)
			{
				ServerEventRaidBossBonus raidBossBonus = instance2.RaidBossBonus;
				if (raidBossBonus != null && raidBossBonus.BeatBonus > 0)
				{
					flag3 = true;
				}
			}
			if (!flag3)
			{
				str = "ui_Lbl_player_exp_failed";
			}
			else if (flag)
			{
				str = "ui_Lbl_player_exp_level_max";
			}
			else
			{
				str = "ui_Lbl_player_exp_success_raid";
			}
		}
		else if (expType == GlowUpWindow.ExpType.BOSS_FAILED)
		{
			str = "ui_Lbl_player_exp_failed";
		}
		else if (flag)
		{
			str = "ui_Lbl_player_exp_level_max";
		}
		else if (expType == GlowUpWindow.ExpType.BOSS_SUCCESS)
		{
			str = "ui_Lbl_player_exp_success";
		}
		else
		{
			str = "ui_Lbl_player_exp";
		}
		text = str + "_caption";
		text2 = str + "_text";
		global::Debug.Log("ExpCaption: " + text);
		global::Debug.Log("ExpText: " + text2);
		GameObject labelObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_caption");
		this.SetupLabel(labelObject, "Result", text);
		GameObject labelObject2 = null;
		GameObject x = GameObjectUtil.FindChildGameObject(base.gameObject, "window_contents");
		if (x != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "body");
			if (gameObject2 != null)
			{
				labelObject2 = GameObjectUtil.FindChildGameObject(gameObject2, "Lbl_body");
			}
		}
		this.SetupLabel(labelObject2, "Result", text2);
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
		if (uiimageButton != null)
		{
			uiimageButton.isEnabled = false;
		}
		this.m_state = GlowUpWindow.State.WaitSetup;
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x06001B59 RID: 7001 RVA: 0x000A1E4C File Offset: 0x000A004C
	public bool IsPlayEnd
	{
		get
		{
			return this.m_state == GlowUpWindow.State.End;
		}
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x000A1E60 File Offset: 0x000A0060
	private void Start()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
		for (int i = 0; i < 2; i++)
		{
			this.m_charaPlate[i] = null;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_skip");
		if (gameObject != null)
		{
			UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "SkipButtonClickedCallback";
			}
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_ok");
		if (gameObject2 != null)
		{
			UIButtonMessage uibuttonMessage2 = gameObject2.GetComponent<UIButtonMessage>();
			if (uibuttonMessage2 == null)
			{
				uibuttonMessage2 = gameObject2.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage2 != null)
			{
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "ButtonClickedCallback";
			}
		}
		base.gameObject.SetActive(false);
		this.m_state = GlowUpWindow.State.Idle;
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x000A1F68 File Offset: 0x000A0168
	private void OnDestroy()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x000A1F78 File Offset: 0x000A0178
	private void Update()
	{
		switch (this.m_state)
		{
		case GlowUpWindow.State.WaitSetup:
		{
			bool flag = true;
			for (int i = 0; i < 2; i++)
			{
				GlowUpCharacter glowUpCharacter = this.m_charaPlate[i];
				if (!(glowUpCharacter == null))
				{
					if (!glowUpCharacter.IsEndSetup)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				Animation component = base.gameObject.GetComponent<Animation>();
				if (component != null)
				{
					ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_cmn_window_Anim2", Direction.Forward);
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationEndCallback), true);
				}
				this.m_state = GlowUpWindow.State.OnInAnim;
			}
			break;
		}
		case GlowUpWindow.State.Playing:
		{
			bool flag2 = true;
			foreach (GlowUpCharacter glowUpCharacter2 in this.m_charaPlate)
			{
				if (!(glowUpCharacter2 == null))
				{
					if (!glowUpCharacter2.IsEnd)
					{
						flag2 = false;
						break;
					}
				}
			}
			if (flag2)
			{
				SoundManager.SeStop("sys_gauge", "SE");
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_skip");
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
				UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = true;
				}
				this.m_state = GlowUpWindow.State.WaitTouchButton;
			}
			break;
		}
		}
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x000A2124 File Offset: 0x000A0324
	private void InAnimationEndCallback()
	{
		base.StartCoroutine(this.OnInAnimationEnd());
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x000A2134 File Offset: 0x000A0334
	private IEnumerator OnInAnimationEnd()
	{
		yield return new WaitForSeconds(0.5f);
		GameObject blinder = GameObjectUtil.FindChildGameObject(base.gameObject, "anime_blinder");
		if (blinder != null)
		{
			blinder.SetActive(false);
		}
		GameObject skipButtonObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_skip");
		if (skipButtonObject != null)
		{
			skipButtonObject.SetActive(true);
		}
		CharaType[] charaTypeList = new CharaType[]
		{
			SaveDataManager.Instance.PlayerData.MainChara,
			SaveDataManager.Instance.PlayerData.SubChara
		};
		for (int index = 0; index < 2; index++)
		{
			GlowUpCharacter charaPlate = this.m_charaPlate[index];
			if (!(charaPlate == null))
			{
				GlowUpCharaAfterInfo afterInfo = new GlowUpCharaAfterInfo();
				CharaType charaType = charaTypeList[index];
				if (ServerInterface.LoggedInServerInterface != null)
				{
					ServerCharacterState charaState = ServerInterface.PlayerState.CharacterState(charaType);
					if (charaState != null)
					{
						afterInfo.level = this.CalcCharacterTotalLevel(charaState.AbilityLevel);
						afterInfo.levelUpCost = charaState.Cost;
						afterInfo.exp = charaState.Exp;
					}
					ServerPlayCharacterState playCharaState = ServerInterface.PlayerState.PlayCharacterState(charaType);
					if (playCharaState != null)
					{
						List<AbilityType> abilityList = new List<AbilityType>();
						using (List<int>.Enumerator enumerator = playCharaState.abilityLevelUp.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AbilityType ability = (AbilityType)enumerator.Current;
								abilityList.Add(ability);
							}
						}
						afterInfo.abilityList = abilityList;
						List<int> abilityLevelupList = new List<int>();
						foreach (int cost in playCharaState.abilityLevelUpExp)
						{
							abilityLevelupList.Add(cost);
						}
						afterInfo.abilityListExp = abilityLevelupList;
					}
				}
				charaPlate.PlayStart(afterInfo);
			}
		}
		SoundManager.SePlay("sys_gauge", "SE");
		this.m_state = GlowUpWindow.State.Playing;
		yield break;
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x000A2150 File Offset: 0x000A0350
	private void OutAnimationEndCallback()
	{
		base.gameObject.SetActive(false);
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, true);
		}
		this.m_state = GlowUpWindow.State.End;
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x000A218C File Offset: 0x000A038C
	private void SkipButtonClickedCallback()
	{
		GlowUpWindow.State state = this.m_state;
		if (state == GlowUpWindow.State.Playing)
		{
			foreach (GlowUpCharacter glowUpCharacter in this.m_charaPlate)
			{
				if (!(glowUpCharacter == null))
				{
					glowUpCharacter.PlaySkip();
				}
			}
		}
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x000A21E8 File Offset: 0x000A03E8
	private void ButtonClickedCallback()
	{
		GlowUpWindow.State state = this.m_state;
		if (state == GlowUpWindow.State.WaitTouchButton)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			Animation component = base.gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationEndCallback), true);
			}
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
			if (uiimageButton != null)
			{
				uiimageButton.isEnabled = false;
			}
			this.m_state = GlowUpWindow.State.OnOutAnim;
		}
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x000A2280 File Offset: 0x000A0480
	private int CalcCharacterTotalLevel(List<int> abilityLevelList)
	{
		int num = 0;
		if (abilityLevelList == null)
		{
			return num;
		}
		foreach (int num2 in abilityLevelList)
		{
			num += num2;
		}
		return num;
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000A22EC File Offset: 0x000A04EC
	private void SetupLabel(GameObject labelObject, string groupName, string cellName)
	{
		if (labelObject == null)
		{
			return;
		}
		labelObject.SetActive(true);
		UILabel component = labelObject.GetComponent<UILabel>();
		if (component != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, groupName, cellName).text;
			component.text = text;
		}
		UILocalizeText component2 = labelObject.GetComponent<UILocalizeText>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x000A2350 File Offset: 0x000A0550
	private void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		this.ButtonClickedCallback();
	}

	// Token: 0x040018E0 RID: 6368
	private static readonly string[] CharaPlateName = new string[]
	{
		"player_main",
		"player_sub"
	};

	// Token: 0x040018E1 RID: 6369
	private GlowUpWindow.State m_state = GlowUpWindow.State.Setup;

	// Token: 0x040018E2 RID: 6370
	private GlowUpCharacter[] m_charaPlate = new GlowUpCharacter[2];

	// Token: 0x020003A3 RID: 931
	public enum ExpType
	{
		// Token: 0x040018E4 RID: 6372
		RUN_STAGE,
		// Token: 0x040018E5 RID: 6373
		BOSS_SUCCESS,
		// Token: 0x040018E6 RID: 6374
		BOSS_FAILED
	}

	// Token: 0x020003A4 RID: 932
	private enum Type
	{
		// Token: 0x040018E8 RID: 6376
		None = -1,
		// Token: 0x040018E9 RID: 6377
		Main,
		// Token: 0x040018EA RID: 6378
		Sub,
		// Token: 0x040018EB RID: 6379
		Num
	}

	// Token: 0x020003A5 RID: 933
	private enum State
	{
		// Token: 0x040018ED RID: 6381
		None = -1,
		// Token: 0x040018EE RID: 6382
		Idle,
		// Token: 0x040018EF RID: 6383
		Setup,
		// Token: 0x040018F0 RID: 6384
		WaitSetup,
		// Token: 0x040018F1 RID: 6385
		OnInAnim,
		// Token: 0x040018F2 RID: 6386
		Playing,
		// Token: 0x040018F3 RID: 6387
		WaitTouchButton,
		// Token: 0x040018F4 RID: 6388
		OnOutAnim,
		// Token: 0x040018F5 RID: 6389
		End,
		// Token: 0x040018F6 RID: 6390
		Num
	}
}
