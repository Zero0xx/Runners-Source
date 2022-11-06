using System;
using System.Collections.Generic;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class HudCharacterPanel : MonoBehaviour
{
	// Token: 0x06002113 RID: 8467 RVA: 0x000C67AC File Offset: 0x000C49AC
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x000C67C0 File Offset: 0x000C49C0
	private void Update()
	{
		this.m_timer -= Time.deltaTime;
		if (this.m_timer < 0f)
		{
			this.SetChageBtn(true);
			base.enabled = false;
		}
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x000C6800 File Offset: 0x000C4A00
	private void Initialize()
	{
		this.m_init_flag = true;
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			for (uint num = 0U; num < 1U; num += 1U)
			{
				Transform transform = mainMenuUIObject.transform.FindChild("Anchor_5_MC/2_Character/Btn_2_player/" + this.m_path_name[(int)((UIntPtr)num)]);
				if (transform != null)
				{
					this.m_data_obj[(int)((UIntPtr)num)] = transform.gameObject;
				}
				else
				{
					this.m_init_flag = false;
				}
			}
			for (uint num2 = 0U; num2 < 5U; num2 += 1U)
			{
				Transform transform2 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/info_bonus/" + this.m_bonusPathName[(int)((UIntPtr)num2)]);
				if (transform2 != null)
				{
					this.m_bonusDataObj[(int)((UIntPtr)num2)] = transform2.gameObject;
				}
				else
				{
					this.m_init_flag = false;
				}
			}
			Transform transform3 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/info_notice");
			if (transform3 != null)
			{
				this.m_detailTextLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(transform3.gameObject, "Lbl_bonusnotice");
				this.m_detailTextBg = GameObjectUtil.FindChildGameObjectComponent<UISprite>(transform3.gameObject, "img_base_bg");
			}
			Transform transform4 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/player_set");
			if (transform4 != null)
			{
				this.m_playerSetObj = transform4.gameObject;
				this.m_changeBtnObj = GameObjectUtil.FindChildGameObject(this.m_playerSetObj, "Btn_change");
				if (this.m_changeBtnObj != null)
				{
					UIButtonMessage component = this.m_changeBtnObj.GetComponent<UIButtonMessage>();
					if (component != null)
					{
						component.target = base.gameObject;
						component.functionName = "OnClilckChange";
					}
				}
			}
		}
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x000C69AC File Offset: 0x000C4BAC
	private void SetBonusParam(GameObject obj, Dictionary<BonusParam.BonusType, float> param, BonusParam.BonusType type)
	{
		if (obj != null && param != null)
		{
			float num = 0f;
			if (param.ContainsKey(type))
			{
				num = param[type];
			}
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "clear_percent").text, new Dictionary<string, string>
				{
					{
						"{PARAM}",
						num.ToString()
					}
				});
			}
		}
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x000C6A34 File Offset: 0x000C4C34
	private void OnClilckChange()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			CharaType mainChara = instance.PlayerData.MainChara;
			CharaType subChara = instance.PlayerData.SubChara;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(mainChara);
				if (serverCharacterState != null)
				{
					subCharaId = serverCharacterState.Id;
				}
				int mainCharaId = -1;
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(subChara);
				if (serverCharacterState2 != null)
				{
					mainCharaId = serverCharacterState2.Id;
				}
				loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
			}
			else
			{
				instance.PlayerData.MainChara = subChara;
				instance.PlayerData.SubChara = mainChara;
				this.ServerChangeCharacter_Succeeded(null);
			}
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002118 RID: 8472 RVA: 0x000C6B04 File Offset: 0x000C4D04
	private void SetChageBtn(bool flag)
	{
		if (this.m_changeBtnObj != null)
		{
			UIImageButton component = this.m_changeBtnObj.GetComponent<UIImageButton>();
			if (component != null)
			{
				component.isEnabled = flag;
			}
		}
	}

	// Token: 0x06002119 RID: 8473 RVA: 0x000C6B44 File Offset: 0x000C4D44
	private void ServerChangeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		base.enabled = true;
		this.m_timer = 2f;
		this.SetChageBtn(false);
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x000C6B64 File Offset: 0x000C4D64
	private void ServerChangeCharacter_Failed(MsgServerConnctFailed msg)
	{
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x000C6B68 File Offset: 0x000C4D68
	public void OnUpdateSaveDataDisplay()
	{
		if (!this.m_init_flag)
		{
			this.Initialize();
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			CharaType mainChara = instance.PlayerData.MainChara;
			if (HudCharacterPanelUtil.CheckValidChara(mainChara) && this.m_charaType != mainChara)
			{
				this.m_charaType = mainChara;
				if (this.m_textureRequest != null)
				{
				}
				this.m_textureRequest = new TextureRequestChara(this.m_charaType, this.m_data_obj[(int)((UIntPtr)0)].GetComponent<UITexture>());
				TextureAsyncLoadManager.Instance.Request(this.m_textureRequest);
			}
			CharaType subChara = instance.PlayerData.SubChara;
			int mainChaoID = instance.PlayerData.MainChaoID;
			int subChaoID = instance.PlayerData.SubChaoID;
			BonusParamContainer currentBonusData = BonusUtil.GetCurrentBonusData(mainChara, subChara, mainChaoID, subChaoID);
			if (currentBonusData != null)
			{
				Dictionary<BonusParam.BonusType, float> bonusInfo = currentBonusData.GetBonusInfo(-1);
				if (bonusInfo != null)
				{
					HudCharacterPanelUtil.SetupAbilityIcon(currentBonusData, mainChara, subChara, this.m_playerSetObj);
					HudCharacterPanelUtil.SetupNoticeView(currentBonusData, this.m_detailTextLabel, this.m_detailTextBg);
					this.SetBonusParam(this.m_bonusDataObj[0], bonusInfo, BonusParam.BonusType.SCORE);
					this.SetBonusParam(this.m_bonusDataObj[1], bonusInfo, BonusParam.BonusType.RING);
					this.SetBonusParam(this.m_bonusDataObj[2], bonusInfo, BonusParam.BonusType.ANIMAL);
					this.SetBonusParam(this.m_bonusDataObj[3], bonusInfo, BonusParam.BonusType.DISTANCE);
					this.SetBonusParam(this.m_bonusDataObj[4], bonusInfo, BonusParam.BonusType.ENEMY_OBJBREAK);
				}
			}
			if (this.m_changeBtnObj != null)
			{
				this.m_changeBtnObj.SetActive(subChara != CharaType.UNKNOWN);
			}
		}
	}

	// Token: 0x04001DCC RID: 7628
	private const string COMMON_PATH = "Anchor_5_MC/2_Character/Btn_2_player/";

	// Token: 0x04001DCD RID: 7629
	private const string BONUS_COMMON_PATH = "Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/info_bonus/";

	// Token: 0x04001DCE RID: 7630
	private const string NOTICE_COMMON_PATH = "Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/info_notice";

	// Token: 0x04001DCF RID: 7631
	private const string PLAYER_COMMON_PATH = "Anchor_5_MC/mainmenu_contents/grid/page_3/slot/ui_mm_main2_page(Clone)/player_set";

	// Token: 0x04001DD0 RID: 7632
	private string[] m_path_name = new string[]
	{
		"img_player_main"
	};

	// Token: 0x04001DD1 RID: 7633
	private string[] m_bonusPathName = new string[]
	{
		"Lbl_bonus_0",
		"Lbl_bonus_1",
		"Lbl_bonus_2",
		"Lbl_bonus_3",
		"Lbl_bonus_4"
	};

	// Token: 0x04001DD2 RID: 7634
	private GameObject[] m_data_obj = new GameObject[1];

	// Token: 0x04001DD3 RID: 7635
	private GameObject[] m_bonusDataObj = new GameObject[5];

	// Token: 0x04001DD4 RID: 7636
	private GameObject m_playerSetObj;

	// Token: 0x04001DD5 RID: 7637
	private GameObject m_changeBtnObj;

	// Token: 0x04001DD6 RID: 7638
	private UILabel m_detailTextLabel;

	// Token: 0x04001DD7 RID: 7639
	private UISprite m_detailTextBg;

	// Token: 0x04001DD8 RID: 7640
	private float m_timer;

	// Token: 0x04001DD9 RID: 7641
	private bool m_init_flag;

	// Token: 0x04001DDA RID: 7642
	private CharaType m_charaType = CharaType.UNKNOWN;

	// Token: 0x04001DDB RID: 7643
	private TextureRequestChara m_textureRequest;

	// Token: 0x02000445 RID: 1093
	private enum DataType
	{
		// Token: 0x04001DDD RID: 7645
		IMAGE,
		// Token: 0x04001DDE RID: 7646
		NUM
	}

	// Token: 0x02000446 RID: 1094
	private enum BonusType
	{
		// Token: 0x04001DE0 RID: 7648
		Score,
		// Token: 0x04001DE1 RID: 7649
		Ring,
		// Token: 0x04001DE2 RID: 7650
		Animal,
		// Token: 0x04001DE3 RID: 7651
		Distance,
		// Token: 0x04001DE4 RID: 7652
		Enemy,
		// Token: 0x04001DE5 RID: 7653
		NUM
	}
}
