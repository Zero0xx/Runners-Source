using System;
using System.Collections.Generic;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class SpecialStageInfo : EventBaseInfo
{
	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0005C3DC File Offset: 0x0005A5DC
	public string eventCaption
	{
		get
		{
			return this.m_eventCaption;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0005C3E4 File Offset: 0x0005A5E4
	public string eventText
	{
		get
		{
			return this.m_eventText;
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0005C3EC File Offset: 0x0005A5EC
	public override void Init()
	{
		if (this.m_init)
		{
			return;
		}
		this.m_eventName = HudUtility.GetEventStageName();
		this.m_totalPoint = EventManager.Instance.CollectCount;
		this.m_totalPointTarget = EventBaseInfo.EVENT_AGGREGATE_TARGET.SP_CRYSTAL;
		this.m_eventCaption = this.m_eventName;
		this.m_eventText = "スペシャルステージイベント説明（デバック）\n\n\u3000あいうえお\n\u30001234567890\n\u3000ABCDEFG\n\n\u3000期間: XX/XX  XX:XX  -  XX/XX  XX:XX";
		this.m_eventMission = new List<EventMission>();
		string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", "ui_Lbl_word_get_total");
		List<ServerEventReward> rewardList = EventManager.Instance.RewardList;
		if (rewardList != null)
		{
			for (int i = 0; i < rewardList.Count; i++)
			{
				ServerEventReward serverEventReward = rewardList[i];
				string eventSpObjectName = HudUtility.GetEventSpObjectName();
				string name = TextUtility.Replace(text, "{PARAM_OBJ}", eventSpObjectName);
				this.m_eventMission.Add(new EventMission(name, serverEventReward.Param, serverEventReward.m_itemId, serverEventReward.m_num));
			}
		}
		this.m_rewardChao = new List<ChaoData>();
		RewardChaoData rewardChaoData = EventManager.Instance.GetRewardChaoData();
		if (rewardChaoData != null)
		{
			ChaoData chaoData = ChaoTable.GetChaoData(rewardChaoData.chao_id);
			if (chaoData != null)
			{
				this.m_rewardChao.Add(chaoData);
			}
		}
		EyeCatcherChaoData[] eyeCatcherChaoDatas = EventManager.Instance.GetEyeCatcherChaoDatas();
		if (eyeCatcherChaoDatas != null)
		{
			foreach (EyeCatcherChaoData eyeCatcherChaoData in eyeCatcherChaoDatas)
			{
				ChaoData chaoData2 = ChaoTable.GetChaoData(eyeCatcherChaoData.chao_id);
				if (chaoData2 != null)
				{
					this.m_rewardChao.Add(chaoData2);
				}
			}
		}
		int chaoLevel = ChaoTable.ChaoMaxLevel();
		this.m_leftTitle = TextUtility.GetCommonText("Roulette", "ui_Lbl_word_best_chao");
		if (this.m_rewardChao.Count > 0)
		{
			this.m_leftName = this.m_rewardChao[0].nameTwolines;
			this.m_leftText = this.m_rewardChao[0].GetDetailsLevel(chaoLevel);
			switch (this.m_rewardChao[0].rarity)
			{
			case ChaoData.Rarity.NORMAL:
				this.m_leftBg = "ui_tex_chao_bg_0";
				break;
			case ChaoData.Rarity.RARE:
				this.m_leftBg = "ui_tex_chao_bg_1";
				break;
			case ChaoData.Rarity.SRARE:
				this.m_leftBg = "ui_tex_chao_bg_2";
				break;
			}
			switch (this.m_rewardChao[0].charaAtribute)
			{
			case CharacterAttribute.SPEED:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_speed";
				break;
			case CharacterAttribute.FLY:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_fly";
				break;
			case CharacterAttribute.POWER:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_power";
				break;
			}
		}
		this.m_caption = TextUtility.GetCommonText("Event", "ui_Lbl_event_reward_list");
		this.m_rightTitle = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_get_total");
		this.m_rightTitleIcon = "ui_event_object_icon";
		this.m_init = true;
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0005C6B0 File Offset: 0x0005A8B0
	public override void UpdateData(MonoBehaviour obj)
	{
		if (!this.m_init)
		{
			this.Init();
		}
		else if (!this.m_dummyData)
		{
		}
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0005C6D4 File Offset: 0x0005A8D4
	protected override void DebugInit()
	{
		if (this.m_init)
		{
			return;
		}
		this.m_totalPoint = 123456L;
		this.m_totalPointTarget = EventBaseInfo.EVENT_AGGREGATE_TARGET.SP_CRYSTAL;
		this.m_dummyData = true;
		this.m_eventName = "SpecialStage";
		this.m_eventCaption = "スペシャルステージイベント（デバック）";
		this.m_eventText = "スペシャルステージイベント説明（デバック）\n\n\u3000あいうえお\n\u30001234567890\n\u3000ABCDEFG\n\n\u3000期間: XX/XX  XX:XX  -  XX/XX  XX:XX";
		this.m_rewardChao = new List<ChaoData>();
		List<ChaoData> dataTable = ChaoTable.GetDataTable(ChaoData.Rarity.NORMAL);
		List<ChaoData> dataTable2 = ChaoTable.GetDataTable(ChaoData.Rarity.RARE);
		List<ChaoData> dataTable3 = ChaoTable.GetDataTable(ChaoData.Rarity.SRARE);
		bool flag = false;
		if (dataTable.Count > 0 && dataTable2.Count > 0 && dataTable3.Count > 0)
		{
			switch (UnityEngine.Random.Range(1, 3))
			{
			case 0:
				this.m_rewardChao.Add(dataTable[UnityEngine.Random.Range(0, dataTable.Count - 1)]);
				break;
			case 1:
				this.m_rewardChao.Add(dataTable2[UnityEngine.Random.Range(0, dataTable2.Count - 1)]);
				break;
			case 2:
				this.m_rewardChao.Add(dataTable3[UnityEngine.Random.Range(0, dataTable3.Count - 1)]);
				break;
			default:
				this.m_rewardChao.Add(dataTable3[UnityEngine.Random.Range(0, dataTable3.Count - 1)]);
				break;
			}
		}
		else
		{
			flag = true;
			ChaoData chaoData = new ChaoData();
			chaoData.SetDummyData();
			this.m_rewardChao.Add(chaoData);
		}
		this.m_eventMission = new List<EventMission>();
		List<int> list = new List<int>();
		list.Add(400000 + this.m_rewardChao[0].id);
		list.Add(120100);
		list.Add(121000);
		list.Add(120101);
		list.Add(121001);
		list.Add(400000 + this.m_rewardChao[0].id);
		list.Add(120102);
		list.Add(121002);
		list.Add(120103);
		list.Add(121003);
		list.Add(400000 + this.m_rewardChao[0].id);
		list.Add(120104);
		list.Add(121004);
		list.Add(120105);
		list.Add(121005);
		list.Add(120106);
		list.Add(121006);
		list.Add(120107);
		list.Add(400000 + this.m_rewardChao[0].id);
		for (int i = 0; i < 10; i++)
		{
			long point = (long)((i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1));
			this.m_eventMission.Add(new EventMission("累計SPクリスタル_" + (i + 1), point, list[i % list.Count], i));
		}
		int chaoLevel = ChaoTable.ChaoMaxLevel();
		this.m_leftTitle = "今週の目玉";
		if (this.m_rewardChao.Count > 0)
		{
			this.m_leftName = this.m_rewardChao[0].nameTwolines;
			this.m_leftText = ((!flag) ? this.m_rewardChao[0].GetDetailsLevel(chaoLevel) : "dummy text");
			switch (this.m_rewardChao[0].rarity)
			{
			case ChaoData.Rarity.NORMAL:
				this.m_leftBg = "ui_tex_chao_bg_0";
				break;
			case ChaoData.Rarity.RARE:
				this.m_leftBg = "ui_tex_chao_bg_1";
				break;
			case ChaoData.Rarity.SRARE:
				this.m_leftBg = "ui_tex_chao_bg_2";
				break;
			}
			switch (this.m_rewardChao[0].charaAtribute)
			{
			case CharacterAttribute.SPEED:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_speed";
				break;
			case CharacterAttribute.FLY:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_fly";
				break;
			case CharacterAttribute.POWER:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_power";
				break;
			}
		}
		this.m_caption = "スペシャルステージ報酬(デバック)";
		this.m_rightTitle = "○○を集めろ";
		this.m_rightTitleIcon = "ui_event_object_icon";
		this.m_init = true;
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0005CB40 File Offset: 0x0005AD40
	public static SpecialStageInfo CreateData()
	{
		SpecialStageInfo specialStageInfo = new SpecialStageInfo();
		specialStageInfo.Init();
		return specialStageInfo;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0005CB5C File Offset: 0x0005AD5C
	public static SpecialStageInfo CreateDummyData()
	{
		SpecialStageInfo result = null;
		global::Debug.LogWarning("SpecialStageInfo:DummyDataCreate  not create!!!");
		return result;
	}

	// Token: 0x04000D7F RID: 3455
	private string m_eventCaption;

	// Token: 0x04000D80 RID: 3456
	private string m_eventText;
}
