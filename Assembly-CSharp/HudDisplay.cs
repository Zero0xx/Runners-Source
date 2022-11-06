using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public class HudDisplay
{
	// Token: 0x06002133 RID: 8499 RVA: 0x000C7B30 File Offset: 0x000C5D30
	public HudDisplay()
	{
		for (int i = 0; i < 14; i++)
		{
			this.m_obj_list[i] = new List<GameObject>();
		}
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			GameObject @object = this.GetObject(menuAnimUIObject, "ChaoSetUIPage");
			this.m_obj_list[0].Add(@object);
			this.m_obj_list[0].Add(this.GetObject(@object, "ChaoSetUI"));
			this.m_obj_list[6].Add(this.GetObject(menuAnimUIObject, "RouletteTopUI"));
			this.m_obj_list[2].Add(this.GetObject(menuAnimUIObject, "MainMenuUI4"));
			this.m_obj_list[3].Add(this.GetObject(menuAnimUIObject, "PlayerSet_3_UI"));
			GameObject object2 = this.GetObject(menuAnimUIObject, "ShopPage");
			this.m_obj_list[7].Add(object2);
			this.m_obj_list[7].Add(this.GetObject(object2, "ShopUI2"));
			this.m_obj_list[4].Add(this.GetObject(menuAnimUIObject, "OptionUI"));
			this.m_obj_list[5].Add(this.GetObject(menuAnimUIObject, "InformationUI"));
			this.m_obj_list[8].Add(this.GetObject(menuAnimUIObject, "PresentBoxUI"));
			this.m_obj_list[9].Add(this.GetObject(menuAnimUIObject, "DailyWindowUI"));
			this.m_obj_list[10].Add(this.GetObject(menuAnimUIObject, "DailyInfoUI"));
			this.m_obj_list[1].Add(this.GetObject(menuAnimUIObject, "ItemSet_3_UI"));
			this.m_obj_list[11].Add(this.GetObject(menuAnimUIObject, "ui_mm_ranking_page"));
			this.m_obj_list[13].Add(this.GetObject(menuAnimUIObject, "ui_mm_mileage2_page"));
		}
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject object3 = this.GetObject(cameraUIObject, "SpecialStageWindowUI");
			if (object3 != null)
			{
				this.m_obj_list[12].Add(object3);
			}
			GameObject object4 = this.GetObject(cameraUIObject, "RaidBossWindowUI");
			if (object4 != null)
			{
				this.m_obj_list[12].Add(object4);
			}
		}
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x000C7D70 File Offset: 0x000C5F70
	public void SetAllDisableDisplay()
	{
		for (int i = 0; i < 14; i++)
		{
			if (this.m_obj_list[i] != null)
			{
				this.SetActiveListObj(this.m_obj_list[i], false);
			}
		}
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x000C7DAC File Offset: 0x000C5FAC
	public void SetDisplayHudObject(HudDisplay.ObjType obj_type)
	{
		for (int i = 0; i < 14; i++)
		{
			if (this.m_obj_list[i] != null)
			{
				bool active_flag = i == (int)obj_type;
				this.SetActiveListObj(this.m_obj_list[i], active_flag);
			}
		}
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x000C7DF0 File Offset: 0x000C5FF0
	private GameObject GetObject(GameObject menu_anim_obj, string obj_name)
	{
		if (menu_anim_obj != null && obj_name != null)
		{
			Transform transform = menu_anim_obj.transform.Find(obj_name);
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x000C7E30 File Offset: 0x000C6030
	private void SetActiveListObj(List<GameObject> obj_list, bool active_flag)
	{
		if (obj_list != null)
		{
			foreach (GameObject gameObject in obj_list)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(active_flag);
					ButtonEvent.DebugInfoDraw("SetActive " + gameObject.name + " " + active_flag.ToString());
				}
			}
		}
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x000C7EC4 File Offset: 0x000C60C4
	public static HudDisplay.ObjType CalcObjTypeFromSequenceType(MsgMenuSequence.SequeneceType seqType)
	{
		HudDisplay.ObjType result = HudDisplay.ObjType.Main;
		switch (seqType)
		{
		case MsgMenuSequence.SequeneceType.MAIN:
			result = HudDisplay.ObjType.Main;
			break;
		case MsgMenuSequence.SequeneceType.STAGE:
			result = HudDisplay.ObjType.NONE;
			break;
		case MsgMenuSequence.SequeneceType.PRESENT_BOX:
			result = HudDisplay.ObjType.PresentBox;
			break;
		case MsgMenuSequence.SequeneceType.DAILY_CHALLENGE:
			result = HudDisplay.ObjType.DailyChallenge;
			break;
		case MsgMenuSequence.SequeneceType.DAILY_BATTLE:
			result = HudDisplay.ObjType.DailyBattle;
			break;
		case MsgMenuSequence.SequeneceType.CHARA_MAIN:
			result = HudDisplay.ObjType.Player;
			break;
		case MsgMenuSequence.SequeneceType.CHAO:
			result = HudDisplay.ObjType.Chao;
			break;
		case MsgMenuSequence.SequeneceType.PLAY_ITEM:
		case MsgMenuSequence.SequeneceType.EPISODE_PLAY:
		case MsgMenuSequence.SequeneceType.QUICK:
		case MsgMenuSequence.SequeneceType.PLAY_AT_EPISODE_PAGE:
		case MsgMenuSequence.SequeneceType.MAIN_PLAY_BUTTON:
			result = HudDisplay.ObjType.NewItem;
			break;
		case MsgMenuSequence.SequeneceType.OPTION:
			result = HudDisplay.ObjType.Option;
			break;
		case MsgMenuSequence.SequeneceType.INFOMATION:
			result = HudDisplay.ObjType.Infomation;
			break;
		case MsgMenuSequence.SequeneceType.ROULETTE:
		case MsgMenuSequence.SequeneceType.CHAO_ROULETTE:
		case MsgMenuSequence.SequeneceType.ITEM_ROULETTE:
			result = HudDisplay.ObjType.Roulette;
			break;
		case MsgMenuSequence.SequeneceType.SHOP:
			result = HudDisplay.ObjType.Shop;
			break;
		case MsgMenuSequence.SequeneceType.EPISODE:
			result = HudDisplay.ObjType.Mileage;
			break;
		case MsgMenuSequence.SequeneceType.EPISODE_RANKING:
		case MsgMenuSequence.SequeneceType.QUICK_RANKING:
			result = HudDisplay.ObjType.Ranking;
			break;
		case MsgMenuSequence.SequeneceType.EVENT_TOP:
		case MsgMenuSequence.SequeneceType.EVENT_SPECIAL:
		case MsgMenuSequence.SequeneceType.EVENT_RAID:
		case MsgMenuSequence.SequeneceType.EVENT_COLLECT:
			result = HudDisplay.ObjType.Event;
			break;
		}
		return result;
	}

	// Token: 0x04001DF1 RID: 7665
	private List<GameObject>[] m_obj_list = new List<GameObject>[14];

	// Token: 0x0200044C RID: 1100
	public enum ObjType
	{
		// Token: 0x04001DF3 RID: 7667
		Chao,
		// Token: 0x04001DF4 RID: 7668
		NewItem,
		// Token: 0x04001DF5 RID: 7669
		Main,
		// Token: 0x04001DF6 RID: 7670
		Player,
		// Token: 0x04001DF7 RID: 7671
		Option,
		// Token: 0x04001DF8 RID: 7672
		Infomation,
		// Token: 0x04001DF9 RID: 7673
		Roulette,
		// Token: 0x04001DFA RID: 7674
		Shop,
		// Token: 0x04001DFB RID: 7675
		PresentBox,
		// Token: 0x04001DFC RID: 7676
		DailyChallenge,
		// Token: 0x04001DFD RID: 7677
		DailyBattle,
		// Token: 0x04001DFE RID: 7678
		Ranking,
		// Token: 0x04001DFF RID: 7679
		Event,
		// Token: 0x04001E00 RID: 7680
		Mileage,
		// Token: 0x04001E01 RID: 7681
		NUM,
		// Token: 0x04001E02 RID: 7682
		NONE
	}
}
