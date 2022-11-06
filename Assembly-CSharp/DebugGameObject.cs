using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataTable;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000A39 RID: 2617
public class DebugGameObject : SingletonGameObject<DebugGameObject>
{
	// Token: 0x1700096B RID: 2411
	// (get) Token: 0x060045B6 RID: 17846 RVA: 0x00166690 File Offset: 0x00164890
	public bool firstLogin
	{
		get
		{
			return this.m_titleFirstLogin;
		}
	}

	// Token: 0x1700096C RID: 2412
	// (get) Token: 0x060045B7 RID: 17847 RVA: 0x00166698 File Offset: 0x00164898
	public DebugGameObject.LOADING_SUFFIXE loadingSuffixe
	{
		get
		{
			return this.m_titleLoadingSuffixe;
		}
	}

	// Token: 0x1700096D RID: 2413
	// (get) Token: 0x060045B8 RID: 17848 RVA: 0x001666A0 File Offset: 0x001648A0
	public string suffixeBaseText
	{
		get
		{
			return this.m_suffixeBaseText;
		}
	}

	// Token: 0x1700096E RID: 2414
	// (get) Token: 0x060045B9 RID: 17849 RVA: 0x001666A8 File Offset: 0x001648A8
	public bool debugActive
	{
		get
		{
			return this.m_debugActive;
		}
	}

	// Token: 0x1700096F RID: 2415
	// (get) Token: 0x060045BA RID: 17850 RVA: 0x001666B0 File Offset: 0x001648B0
	public bool rankingDebug
	{
		get
		{
			return this.m_rankingDebug;
		}
	}

	// Token: 0x17000970 RID: 2416
	// (get) Token: 0x060045BB RID: 17851 RVA: 0x001666B8 File Offset: 0x001648B8
	public RankingUtil.RankingRankerType targetRankingRankerType
	{
		get
		{
			if (!this.rankingDebug)
			{
				return RankingUtil.RankingRankerType.RIVAL;
			}
			return this.m_targetRankingRankerType;
		}
	}

	// Token: 0x17000971 RID: 2417
	// (get) Token: 0x060045BC RID: 17852 RVA: 0x001666D0 File Offset: 0x001648D0
	public RankingUtil.RankingScoreType rivalRankingScoreType
	{
		get
		{
			if (!this.rankingDebug)
			{
				return RankingUtil.RankingScoreType.HIGH_SCORE;
			}
			return this.m_rivalRankingScoreType;
		}
	}

	// Token: 0x17000972 RID: 2418
	// (get) Token: 0x060045BD RID: 17853 RVA: 0x001666E8 File Offset: 0x001648E8
	public RankingUtil.RankingScoreType spRankingScoreType
	{
		get
		{
			if (!this.rankingDebug)
			{
				return RankingUtil.RankingScoreType.TOTAL_SCORE;
			}
			return this.m_spRankingScoreType;
		}
	}

	// Token: 0x17000973 RID: 2419
	// (get) Token: 0x060045BE RID: 17854 RVA: 0x00166700 File Offset: 0x00164900
	public RouletteCategory rouletteDummyCategory
	{
		get
		{
			return this.m_rouletteDummyCategory;
		}
	}

	// Token: 0x17000974 RID: 2420
	// (get) Token: 0x060045BF RID: 17855 RVA: 0x00166708 File Offset: 0x00164908
	public bool rouletteTutorial
	{
		get
		{
			return this.m_rouletteTutorial;
		}
	}

	// Token: 0x17000975 RID: 2421
	// (get) Token: 0x060045C0 RID: 17856 RVA: 0x00166710 File Offset: 0x00164910
	public bool crypt
	{
		get
		{
			return this.m_crypt;
		}
	}

	// Token: 0x17000976 RID: 2422
	// (get) Token: 0x060045C1 RID: 17857 RVA: 0x00166718 File Offset: 0x00164918
	public bool rankingLog
	{
		get
		{
			return this.m_rankingLog;
		}
	}

	// Token: 0x17000977 RID: 2423
	// (get) Token: 0x060045C2 RID: 17858 RVA: 0x00166720 File Offset: 0x00164920
	public List<Constants.Campaign.emType> debugCampaign
	{
		get
		{
			return this.m_debugCampaign;
		}
	}

	// Token: 0x17000978 RID: 2424
	// (get) Token: 0x060045C3 RID: 17859 RVA: 0x00166728 File Offset: 0x00164928
	private ResourceSceneLoader debugSceneLoader
	{
		get
		{
			if (this.m_debugSceneLoader == null)
			{
				GameObject gameObject = new GameObject("DebugTextLoader");
				if (gameObject != null)
				{
					this.m_debugSceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				}
			}
			return this.m_debugSceneLoader;
		}
	}

	// Token: 0x060045C4 RID: 17860 RVA: 0x00166770 File Offset: 0x00164970
	public void PopLog(string log, float xRate, float yRate, DebugGameObject.GUI_RECT_ANCHOR anchor = DebugGameObject.GUI_RECT_ANCHOR.CENTER)
	{
	}

	// Token: 0x060045C5 RID: 17861 RVA: 0x00166774 File Offset: 0x00164974
	public bool CheckMsgText(string msg)
	{
		if (!string.IsNullOrEmpty(msg) && this.m_msgMax < msg.Length)
		{
			this.m_msgMax = msg.Length;
			return true;
		}
		return false;
	}

	// Token: 0x060045C6 RID: 17862 RVA: 0x001667A4 File Offset: 0x001649A4
	public void CheckUpdate(string name = "")
	{
		if (this.m_currentUpdCost == null)
		{
			this.m_currentUpdCost = new Dictionary<string, int>();
			this.m_currentUpdCost.Add("TOTAL_COST", 1);
			this.m_keys = new List<string>();
			this.m_keys.Add("TOTAL_COST");
			if (!string.IsNullOrEmpty(name) && !this.m_currentUpdCost.ContainsKey(name))
			{
				this.m_currentUpdCost.Add(name, 1);
				this.m_keys.Add(name);
			}
		}
		else
		{
			if (this.m_currentUpdCost.ContainsKey("TOTAL_COST"))
			{
				Dictionary<string, int> currentUpdCost;
				Dictionary<string, int> dictionary = currentUpdCost = this.m_currentUpdCost;
				string key2;
				string key = key2 = "TOTAL_COST";
				int num = currentUpdCost[key2];
				dictionary[key] = num + 1;
			}
			if (!string.IsNullOrEmpty(name))
			{
				if (!this.m_currentUpdCost.ContainsKey(name))
				{
					this.m_currentUpdCost.Add(name, 1);
					this.m_keys.Add(name);
				}
				else
				{
					Dictionary<string, int> currentUpdCost2;
					Dictionary<string, int> dictionary2 = currentUpdCost2 = this.m_currentUpdCost;
					int num = currentUpdCost2[name];
					dictionary2[name] = num + 1;
				}
			}
		}
		if (this.m_updCost == null)
		{
			this.m_updCost = new Dictionary<string, int>();
			this.m_updCost.Add("TOTAL_COST", 0);
			if (!string.IsNullOrEmpty(name) && !this.m_updCost.ContainsKey(name))
			{
				this.m_updCost.Add(name, 0);
			}
		}
		else if (!string.IsNullOrEmpty(name) && !this.m_updCost.ContainsKey(name))
		{
			this.m_updCost.Add(name, 0);
		}
	}

	// Token: 0x060045C7 RID: 17863 RVA: 0x00166934 File Offset: 0x00164B34
	private void OnGUI()
	{
		base.enabled = false;
	}

	// Token: 0x060045C8 RID: 17864 RVA: 0x00166940 File Offset: 0x00164B40
	private void GuiDummyObject()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || !this.m_debugTestBtn)
		{
			return;
		}
		int num = 0;
		if (this.m_debugDummy != null && this.m_debugDummy.Count > 0)
		{
			num = this.m_debugDummy.Count;
		}
		Rect position = this.CreateGuiRect(new Rect(0f, 90f, 70f, 25f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP);
		if (GUI.Button(position, "Stress Test\n" + num))
		{
			if (this.m_debugDummy == null)
			{
				this.m_debugDummy = new List<string>();
			}
			for (int i = 0; i < 500; i++)
			{
				this.m_debugDummy.Add(string.Empty + this.m_debugDummy.Count);
			}
		}
		if (this.m_debugDummy != null && this.m_debugDummy.Count > 0)
		{
			int num2 = 0;
			foreach (string text in this.m_debugDummy)
			{
				Rect position2 = this.CreateGuiRect(new Rect(3f * (float)(num2 % 250), 4f * (float)(num2 / 250), 2f, 3f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP);
				GUI.Box(position2, text);
				num2++;
			}
		}
	}

	// Token: 0x060045C9 RID: 17865 RVA: 0x00166ADC File Offset: 0x00164CDC
	private void GuiPop()
	{
		if (this.m_debugPop != null && this.m_debugPop.Count > 0 && this.m_camera != null)
		{
			float deltaTime = Time.deltaTime;
			long[] array = new long[this.m_debugPop.Count];
			long num = -1L;
			float num2 = this.m_camera.pixelRect.yMax * 0.05f;
			this.m_debugPop.Keys.CopyTo(array, 0);
			if (array.Length > 0)
			{
				foreach (long num3 in array)
				{
					string text = this.m_debugPop[num3] + "\n count:" + num3;
					Rect position = this.m_debugPopRect[num3];
					float num4 = (10f - this.m_debugPopTime[num3]) / 10f;
					num4 = 1f - (num4 - 1f) * (num4 - 1f);
					position.y -= num2 * num4;
					GUI.Box(position, text);
					Dictionary<long, float> debugPopTime;
					Dictionary<long, float> dictionary = debugPopTime = this.m_debugPopTime;
					long key2;
					long key = key2 = num3;
					float num5 = debugPopTime[key2];
					dictionary[key] = num5 - deltaTime;
					if (this.m_debugPopTime[num3] <= 0f)
					{
						num = num3;
					}
				}
			}
			if (num >= 0L)
			{
				this.m_debugPop.Remove(num);
				this.m_debugPopRect.Remove(num);
				this.m_debugPopTime.Remove(num);
			}
		}
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x00166C74 File Offset: 0x00164E74
	private void GuiCheck()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || this.m_debugCheckType == DebugGameObject.DEBUG_CHECK_TYPE.NONE)
		{
			return;
		}
		if (this.m_debugCheckFlag)
		{
			float num = 350f;
			float num2 = 700f;
			Rect rect = this.CreateGuiRect(new Rect(0f, 0f, num2 + 10f, num + 10f), DebugGameObject.GUI_RECT_ANCHOR.CENTER);
			GUI.Box(rect, string.Empty);
			DebugGameObject.DEBUG_CHECK_TYPE debugCheckType;
			if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 200f, 20f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "close"))
			{
				debugCheckType = this.m_debugCheckType;
				if (debugCheckType != DebugGameObject.DEBUG_CHECK_TYPE.DRAW_CALL)
				{
					if (debugCheckType == DebugGameObject.DEBUG_CHECK_TYPE.LOAD_ATLAS)
					{
						this.m_debugCheckFlag = !this.m_debugCheckFlag;
						this.m_debugDrawCallPanelCurrent = string.Empty;
						this.m_debugDrawCallMatCurrent = string.Empty;
						if (this.m_debugAtlasList != null)
						{
							this.m_debugAtlasList.Clear();
						}
						HudMenuUtility.SetConnectAlertSimpleUI(false);
					}
				}
				else if (string.IsNullOrEmpty(this.m_debugDrawCallPanelCurrent) && string.IsNullOrEmpty(this.m_debugDrawCallMatCurrent))
				{
					this.m_debugCheckFlag = !this.m_debugCheckFlag;
					this.m_debugDrawCallPanelCurrent = string.Empty;
					this.m_debugDrawCallMatCurrent = string.Empty;
					if (this.m_debugAtlasList != null)
					{
						this.m_debugAtlasList.Clear();
					}
					if (this.m_debugAtlasLangList != null)
					{
						this.m_debugAtlasLangList.Clear();
					}
					HudMenuUtility.SetConnectAlertSimpleUI(false);
				}
				else if (!string.IsNullOrEmpty(this.m_debugDrawCallMatCurrent))
				{
					this.m_debugDrawCallMatCurrent = string.Empty;
				}
				else if (!string.IsNullOrEmpty(this.m_debugDrawCallPanelCurrent))
				{
					this.m_debugDrawCallPanelCurrent = string.Empty;
					this.m_debugDrawCallMatCurrent = string.Empty;
				}
				else
				{
					this.m_debugCheckFlag = !this.m_debugCheckFlag;
					this.m_debugDrawCallPanelCurrent = string.Empty;
					this.m_debugDrawCallMatCurrent = string.Empty;
					HudMenuUtility.SetConnectAlertSimpleUI(false);
				}
			}
			debugCheckType = this.m_debugCheckType;
			if (debugCheckType != DebugGameObject.DEBUG_CHECK_TYPE.DRAW_CALL)
			{
				if (debugCheckType == DebugGameObject.DEBUG_CHECK_TYPE.LOAD_ATLAS)
				{
					this.GuiLoadAtlas(rect, num2, num);
				}
			}
			else
			{
				this.GuiDrawCall2D(rect, num2, num);
			}
		}
		else if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 200f, 20f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), string.Empty + this.m_debugCheckType))
		{
			this.m_debugCheckFlag = !this.m_debugCheckFlag;
			this.m_debugDrawCallPanelCurrent = string.Empty;
			this.m_debugDrawCallMatCurrent = string.Empty;
			DebugGameObject.DEBUG_CHECK_TYPE debugCheckType = this.m_debugCheckType;
			if (debugCheckType != DebugGameObject.DEBUG_CHECK_TYPE.DRAW_CALL)
			{
				if (debugCheckType == DebugGameObject.DEBUG_CHECK_TYPE.LOAD_ATLAS)
				{
					this.CheckLoadAtlas();
				}
			}
			else
			{
				this.CheckDrawCall2D();
			}
			HudMenuUtility.SetConnectAlertSimpleUI(true);
		}
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x00166F44 File Offset: 0x00165144
	private void GuiDeckData()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
		if (!DeckViewWindow.isActive)
		{
			if (this.m_debugDeck)
			{
				float height = 100f;
				float width = 550f;
				Rect rect = this.CreateGuiRect(new Rect(0f, 40f, width, height), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				GUI.Box(rect, string.Empty);
				if (this.m_debugDeckList != null && this.m_debugDeckList.Count > 0)
				{
					float height2 = 0.7f;
					float num = 0.13571429f;
					float num2 = 0.14285715f;
					float height3 = 0.2f;
					float width2 = num;
					float num3 = (num2 - num) * 0.5f;
					for (int i = 0; i < this.m_debugDeckList.Count; i++)
					{
						GUI.Box(this.CreateGuiRectInRate(rect, new Rect(num3 + num2 * (float)i, 0.03f, num, height2), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), this.m_debugDeckList[i]);
						if (GUI.Button(this.CreateGuiRectInRate(rect, new Rect(num3 + num2 * (float)i, -0.03f, width2, height3), DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM), "reset"))
						{
							DeckUtil.DeckReset(i);
							this.DebugCreateDeckList();
						}
					}
					if (GUI.Button(this.CreateGuiRectInRate(rect, new Rect(-num3, 0.03f, num, 0.94f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), "reload\n\n current:" + this.m_debugDeckCurrentIndex))
					{
						this.DebugCreateDeckList();
					}
				}
				if (GUI.Button(this.CreateGuiRect(new Rect(0f, 10f, 60f, 25f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "close"))
				{
					this.m_debugDeck = !this.m_debugDeck;
				}
				if (this.m_debugDeckCount > 300)
				{
					this.DebugCreateDeckList();
				}
				this.m_debugDeckCount++;
			}
			else if (this.m_debugCharaData)
			{
				float height4 = 350f;
				float width3 = 600f;
				int num4 = 6;
				Rect rect2 = this.CreateGuiRect(new Rect(0f, 40f, width3, height4), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				GUI.Box(rect2, string.Empty);
				if (this.m_debugCharaDataList != null && this.m_debugCharaDataList.Count > 0)
				{
					float height5 = 0.19f;
					float num5 = 1f / (float)num4 * 0.95f;
					float num6 = 1f / (float)num4;
					float num7 = (num6 - num5) * 0.5f;
					for (int j = 0; j < this.m_debugCharaDataList.Count; j++)
					{
						int num8 = j % num4;
						int num9 = j / num4;
						if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(num7 + num6 * (float)num8, 0.02f + 0.2f * (float)num9, num5, height5), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), this.m_debugCharaDataList[j]) && !this.m_debugCharaDataBuy)
						{
							this.DebugCreateCharaInfo(j);
						}
					}
					if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(0.01f, -0.12f, 0.175f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM), "sort\n" + this.m_debugCharaDataSort))
					{
						int num10 = 3;
						int num11 = (int)this.m_debugCharaDataSort;
						num11 = (num11 + 1) % num10;
						this.DebugCreateCharaList((ServerPlayerState.CHARA_SORT)num11);
					}
					if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(0.01f, -0.01f, 0.175f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM), "offset\n" + this.m_debugCharaDataCount))
					{
						this.DebugCreateCharaList(this.m_debugCharaDataSort);
					}
					GUI.Box(this.CreateGuiRectInRate(rect2, new Rect(-0.16f, -0.01f, 0.65f, 0.21f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM), this.m_debugCharaDataInfo);
					if (this.m_debugCharaDataBuyCost != null && this.m_debugCharaDataBuyCost.Count > 0 && !this.m_debugCharaDataBuy)
					{
						int count = this.m_debugCharaDataBuyCost.Count;
						Dictionary<ServerItem.Id, int>.KeyCollection keys = this.m_debugCharaDataBuyCost.Keys;
						float num12 = 0.2f / (float)count;
						float num13 = 0.02f / (float)count;
						float num14 = (num12 + num13) * -1f;
						int num15 = 0;
						foreach (ServerItem.Id id in keys)
						{
							if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(-0.005f, -num13 + num14 * (float)num15, 0.145f, num12), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM), string.Concat(new object[]
							{
								string.Empty,
								id,
								"\n",
								this.m_debugCharaDataBuyCost[id]
							})))
							{
								this.DebugBuyChara(id, this.m_debugCharaDataBuyCost[id]);
							}
							num15++;
						}
					}
				}
				if (GUI.Button(this.CreateGuiRect(new Rect(0f, 10f, 60f, 25f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "close"))
				{
					this.m_debugCharaData = !this.m_debugCharaData;
				}
				if (GUI.Button(this.CreateGuiRect(new Rect(200f, 10f, 100f, 25f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "roulette\n chara picup") && RouletteManager.Instance != null)
				{
					RouletteManager.Instance.RequestPicupCharaList(false);
				}
			}
			else if (GUI.Button(this.CreateGuiRect(new Rect(-100f, 5f, 60f, 25f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "deck data"))
			{
				this.m_debugDeck = !this.m_debugDeck;
				this.DebugCreateDeckList();
			}
		}
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x00167550 File Offset: 0x00165750
	private void DebugCreateDeckList()
	{
		this.m_debugDeckCount = 0;
		if (this.m_debugDeckList != null)
		{
			this.m_debugDeckList.Clear();
		}
		this.m_debugDeckList = new List<string>();
		this.m_debugDeckCurrentIndex = DeckUtil.GetDeckCurrentStockIndex();
		for (int i = 0; i < 6; i++)
		{
			CharaType charaType = CharaType.UNKNOWN;
			CharaType charaType2 = CharaType.UNKNOWN;
			int num = -1;
			int num2 = -1;
			DeckUtil.DeckSetLoad(i, ref charaType, ref charaType2, ref num, ref num2, null);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Empty + charaType);
			stringBuilder.Append("\n" + charaType2);
			stringBuilder.Append("\n" + num);
			stringBuilder.Append("\n" + num2);
			this.m_debugDeckList.Add(stringBuilder.ToString());
		}
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x00167638 File Offset: 0x00165838
	private void DebugBuyChara(ServerItem.Id itemId, int cost)
	{
		if (this.m_debugCharaDataState != null)
		{
			long itemCount = GeneralUtil.GetItemCount(itemId);
			if (itemCount >= (long)cost)
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					ServerItem item = new ServerItem(itemId);
					loggedInServerInterface.RequestServerUnlockedCharacter(this.m_debugCharaDataState.charaType, item, base.gameObject);
					this.m_debugCharaDataBuy = true;
				}
			}
			else
			{
				global::Debug.Log(string.Concat(new object[]
				{
					"DebugBuyChara error  ",
					itemId,
					":",
					itemCount
				}));
			}
		}
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x001676D0 File Offset: 0x001658D0
	private void ServerUnlockedCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		if (this.m_debugCharaDataList != null)
		{
			this.m_debugCharaDataList.Clear();
		}
		this.m_debugCharaDataList = new List<string>();
		CharaType charaType = CharaType.UNKNOWN;
		if (this.m_debugCharaDataState != null)
		{
			charaType = this.m_debugCharaDataState.charaType;
		}
		ServerPlayerState playerState = ServerInterface.PlayerState;
		this.m_debugCharaList = playerState.GetCharacterStateList(this.m_debugCharaDataSort, false, this.m_debugCharaDataCount);
		int idx = 0;
		int num = 0;
		Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = this.m_debugCharaList.Keys;
		foreach (CharaType charaType2 in keys)
		{
			if (charaType == charaType2)
			{
				idx = num;
			}
			ServerCharacterState serverCharacterState = this.m_debugCharaList[charaType2];
			CharacterDataNameInfo.Info charaInfo = this.m_debugCharaList[charaType2].charaInfo;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Empty + charaInfo.m_name);
			stringBuilder.Append("\n Lv:" + serverCharacterState.Level);
			stringBuilder.Append(" ☆" + serverCharacterState.star);
			stringBuilder.Append("\n" + charaInfo.m_attribute);
			stringBuilder.Append("\n" + charaInfo.m_teamAttribute);
			stringBuilder.Append("\n IsUnlock:" + serverCharacterState.IsUnlocked);
			this.m_debugCharaDataList.Add(stringBuilder.ToString());
			num++;
		}
		this.DebugCreateCharaInfo(idx);
		this.m_debugCharaDataBuy = false;
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x001678A4 File Offset: 0x00165AA4
	private void DebugCreateCharaInfo(int idx)
	{
		ServerPlayerState playerState = ServerInterface.PlayerState;
		Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = this.m_debugCharaList.Keys;
		int num = 0;
		foreach (CharaType key in keys)
		{
			if (idx == num)
			{
				ServerCharacterState serverCharacterState = this.m_debugCharaList[key];
				CharacterDataNameInfo.Info charaInfo = this.m_debugCharaList[key].charaInfo;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Empty + charaInfo.m_name);
				stringBuilder.Append(" Lv:" + serverCharacterState.Level);
				stringBuilder.Append(" ☆" + serverCharacterState.star);
				stringBuilder.Append("  " + charaInfo.m_attribute);
				stringBuilder.Append("  " + charaInfo.m_teamAttribute);
				stringBuilder.Append("  IsUnlock:" + serverCharacterState.IsUnlocked);
				stringBuilder.Append("\n Condition:" + serverCharacterState.Condition);
				stringBuilder.Append(" Status:" + serverCharacterState.Status);
				stringBuilder.Append(" OldStatus:" + serverCharacterState.OldStatus);
				stringBuilder.Append("\n Exp:" + serverCharacterState.Exp);
				stringBuilder.Append(" OldExp:" + serverCharacterState.OldExp);
				stringBuilder.Append(" priceRing:" + serverCharacterState.priceNumRings);
				stringBuilder.Append(" priceRSR:" + serverCharacterState.priceNumRedRings);
				stringBuilder.Append("\n teamAttribute:" + charaInfo.m_teamAttribute);
				stringBuilder.Append(" teamAttributeCategory:" + charaInfo.m_teamAttributeCategory);
				stringBuilder.Append(string.Concat(new object[]
				{
					"\n mainBonus:",
					charaInfo.m_mainAttributeBonus,
					" [",
					charaInfo.GetTeamAttributeValue(charaInfo.m_mainAttributeBonus),
					"]"
				}));
				stringBuilder.Append(string.Concat(new object[]
				{
					" subBonus:",
					charaInfo.m_subAttributeBonus,
					" [",
					charaInfo.GetTeamAttributeValue(charaInfo.m_subAttributeBonus),
					"]"
				}));
				stringBuilder.Append("\n IsRoulette:" + serverCharacterState.IsRoulette);
				this.m_debugCharaDataInfo = stringBuilder.ToString();
				this.m_debugCharaDataState = serverCharacterState;
				this.m_debugCharaDataBuyCost = this.m_debugCharaDataState.GetBuyCostItemList();
				break;
			}
			num++;
		}
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x00167BF0 File Offset: 0x00165DF0
	private void DebugCreateCharaList(ServerPlayerState.CHARA_SORT sort)
	{
		if (this.m_debugCharaDataList != null)
		{
			this.m_debugCharaDataList.Clear();
		}
		this.m_debugCharaDataList = new List<string>();
		if (sort != this.m_debugCharaDataSort)
		{
			this.m_debugCharaDataCount = 0;
		}
		else
		{
			this.m_debugCharaDataCount++;
		}
		this.m_debugCharaDataInfo = string.Empty;
		this.m_debugCharaDataSort = sort;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		this.m_debugCharaList = playerState.GetCharacterStateList(this.m_debugCharaDataSort, false, this.m_debugCharaDataCount);
		Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = this.m_debugCharaList.Keys;
		foreach (CharaType key in keys)
		{
			ServerCharacterState serverCharacterState = this.m_debugCharaList[key];
			CharacterDataNameInfo.Info charaInfo = this.m_debugCharaList[key].charaInfo;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Empty + charaInfo.m_name);
			stringBuilder.Append("\n Lv:" + serverCharacterState.Level);
			stringBuilder.Append(" ☆" + serverCharacterState.star);
			stringBuilder.Append("\n" + charaInfo.m_attribute);
			stringBuilder.Append("\n" + charaInfo.m_teamAttribute);
			stringBuilder.Append("\n IsUnlock:" + serverCharacterState.IsUnlocked);
			this.m_debugCharaDataList.Add(stringBuilder.ToString());
		}
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x00167DB8 File Offset: 0x00165FB8
	private void GuiEtc()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1)
		{
			return;
		}
		if (this.m_camera != null)
		{
			if (Time.timeScale < 5f)
			{
				if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 65f, 20f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM), "hi speed"))
				{
					Time.timeScale = 5f;
				}
			}
			else if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 65f, 20f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM), "reset"))
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x00167E74 File Offset: 0x00166074
	private void GuiMainMenu()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
		if (RouletteManager.IsRouletteEnabled())
		{
			this.m_debugMenu = false;
			this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
			this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
			this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
			this.m_debugMenuItemNum = 1;
			this.m_debugMenuItemPage = 0;
			this.m_debugMenuMileageEpi = 2;
			this.m_debugMenuMileageCha = 1;
			return;
		}
	}

	// Token: 0x060045D3 RID: 17875 RVA: 0x00167F08 File Offset: 0x00166108
	private void DebugPlayingCurrentScoreCheck()
	{
		this.m_debugScoreText = string.Empty;
		bool flag = false;
		if (EventManager.Instance != null && EventManager.Instance.IsSpecialStage())
		{
			flag = true;
		}
		StageScoreManager instance = StageScoreManager.Instance;
		if (instance != null)
		{
			long num = 0L;
			if (flag)
			{
				num = instance.SpecialCrystal;
			}
			else
			{
				num = instance.GetRealtimeScore();
			}
			if (num > 0L)
			{
				RankingManager instance2 = SingletonGameObject<RankingManager>.Instance;
				if (instance2 != null)
				{
					bool flag2 = false;
					long num2 = 0L;
					long num3 = 0L;
					int num4 = 0;
					int currentHighScoreRank = RankingManager.GetCurrentHighScoreRank(RankingUtil.RankingMode.ENDLESS, flag, ref num, out flag2, out num2, out num3, out num4);
					this.m_debugScoreText = string.Concat(new object[]
					{
						"isSpStage:",
						flag,
						" score:",
						num
					});
					string debugScoreText = this.m_debugScoreText;
					this.m_debugScoreText = string.Concat(new object[]
					{
						debugScoreText,
						"\n rank:",
						currentHighScoreRank,
						" nextRank:",
						num4,
						" isHighScore:",
						flag2
					});
					debugScoreText = this.m_debugScoreText;
					this.m_debugScoreText = string.Concat(new object[]
					{
						debugScoreText,
						"\n nextScore:",
						num2,
						" prveScore:",
						num3
					});
				}
			}
		}
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x00168078 File Offset: 0x00166278
	private void GuiPlayingStageBtn()
	{
		if (this.m_debugScore)
		{
			float num = 60f;
			float num2 = 220f;
			Rect rect = this.CreateGuiRect(new Rect(0f, -20f, num2 + 10f, num + 10f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM);
			if (this.m_debugScoreDelay <= 0f)
			{
				this.DebugPlayingCurrentScoreCheck();
				this.m_debugScoreDelay = 0.2f;
			}
			else
			{
				this.m_debugScoreDelay -= Time.deltaTime;
			}
			GUI.Box(rect, this.m_debugScoreText);
			if (GUI.Button(this.CreateGuiRectInRate(rect, new Rect(0f, -0.01f, 0.95f, 0.32f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM), "close"))
			{
				this.m_debugScore = !this.m_debugScore;
				this.m_debugScoreDelay = 0f;
			}
		}
		else if (GUI.Button(this.CreateGuiRect(new Rect(0f, -80f, 50f, 40f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM), "debug gui\n   off"))
		{
			AllocationStatus.hide = true;
			SingletonGameObject<DebugGameObject>.Remove();
		}
		if (this.m_debugPlay)
		{
			float num3 = 300f;
			float width = 120f;
			Rect rect2 = this.CreateGuiRect(new Rect(0f, 0f, width, num3 + 10f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM);
			GUI.Box(rect2, string.Empty);
			if (this.m_debugPlayType == DebugGameObject.DEBUG_PLAY_TYPE.NONE)
			{
				int num4 = 3;
				float num5 = (num3 - 40f) / num3 / (float)num4;
				float width2 = 0.95f;
				for (int i = 0; i < num4; i++)
				{
					if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(0f, 0.02f + num5 * (float)i, width2, num5 * 0.9f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), string.Empty + (DebugGameObject.DEBUG_PLAY_TYPE)i))
					{
						this.m_debugPlayType = (DebugGameObject.DEBUG_PLAY_TYPE)i;
						if (this.m_debugPlayType == DebugGameObject.DEBUG_PLAY_TYPE.BOSS_DESTORY)
						{
							this.m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;
							MsgBossEnd value = new MsgBossEnd(true);
							GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnBossEnd", value, SendMessageOptions.DontRequireReceiver);
							MsgUseEquipItem value2 = new MsgUseEquipItem();
							GameObjectUtil.SendMessageFindGameObject("StageItemManager", "OnUseEquipItem", value2, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				if (GUI.Button(this.CreateGuiRectInRate(rect2, new Rect(0f, -0.02f, 0.833f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM), "close"))
				{
					this.m_debugPlay = false;
					this.m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;
				}
			}
			else
			{
				DebugGameObject.DEBUG_PLAY_TYPE debugPlayType = this.m_debugPlayType;
				if (debugPlayType != DebugGameObject.DEBUG_PLAY_TYPE.ITEM)
				{
					if (debugPlayType == DebugGameObject.DEBUG_PLAY_TYPE.COLOR)
					{
						this.GuiPlayingStageBtnColor(rect2);
					}
				}
				else
				{
					this.GuiPlayingStageBtnItem(rect2);
				}
			}
		}
		else if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 50f, 40f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM), "debug\n menu"))
		{
			this.m_debugPlay = !this.m_debugPlay;
			this.m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;
		}
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x00168368 File Offset: 0x00166568
	private void DebugUseItem(ItemType useItem)
	{
		switch (useItem)
		{
		case ItemType.INVINCIBLE:
		case ItemType.BARRIER:
		case ItemType.MAGNET:
		case ItemType.TRAMPOLINE:
		case ItemType.COMBO:
		case ItemType.LASER:
		case ItemType.DRILL:
		case ItemType.ASTEROID:
			global::Debug.Log("debug use:" + useItem);
			break;
		default:
		{
			int min = 0;
			int num = 7;
			int num2 = UnityEngine.Random.Range(min, num + 1);
			useItem = (ItemType)num2;
			global::Debug.Log("debug use:" + useItem);
			break;
		}
		}
		if (useItem != ItemType.UNKNOWN)
		{
			StageItemManager x = UnityEngine.Object.FindObjectOfType<StageItemManager>();
			if (x != null)
			{
				GameObjectUtil.SendMessageFindGameObject("StageItemManager", "OnAddItem", new MsgAddItemToManager(useItem), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x0016841C File Offset: 0x0016661C
	private void GuiPlayingStageBtnItem(Rect target)
	{
		int num = 0;
		int num2 = 4;
		int num3 = num2 - num + 1;
		float height = 0.85f / (float)num3 * 0.95f;
		float num4 = 0.85f / (float)num3;
		int num5 = 0;
		for (int i = num; i <= num2; i++)
		{
			ItemType itemType = (ItemType)i;
			if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, 0.02f + (float)num5 * num4, 0.95f, height), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), string.Empty + itemType))
			{
				this.DebugUseItem(itemType);
			}
			num5++;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, -0.02f, 0.9f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM), "close"))
		{
			this.m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;
		}
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x001684F0 File Offset: 0x001666F0
	private void GuiPlayingStageBtnColor(Rect target)
	{
		int num = 5;
		int num2 = 7;
		int num3 = num2 - num + 1;
		float height = 0.85f / (float)num3 * 0.95f;
		float num4 = 0.85f / (float)num3;
		int num5 = 0;
		for (int i = num; i <= num2; i++)
		{
			ItemType itemType = (ItemType)i;
			if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, 0.02f + (float)num5 * num4, 0.95f, height), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), string.Empty + itemType))
			{
				this.DebugUseItem(itemType);
			}
			num5++;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, -0.02f, 0.9f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM), "close"))
		{
			this.m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;
		}
	}

	// Token: 0x060045D8 RID: 17880 RVA: 0x001685C4 File Offset: 0x001667C4
	private void GuiMainMenuBtn()
	{
		if (this.m_debugMenu)
		{
			float num = 250f;
			float num2 = 250f;
			Rect rect = this.CreateGuiRect(new Rect(-100f, 0f, num2, num), DebugGameObject.GUI_RECT_ANCHOR.CENTER_RIGHT);
			GUI.Box(rect, string.Empty);
			if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.NONE)
			{
				int num3 = 6;
				float num4 = num / (float)num3;
				float num5 = num2 - 10f;
				float num6 = num4 - 10f;
				float num7 = num5;
				float num8 = num * -0.5f - num4 * 0.5f;
				this.m_debugMenuRankingCurrentRank = 0;
				for (int i = 0; i < num3; i++)
				{
					DebugGameObject.DEBUG_MENU_TYPE debug_MENU_TYPE = (DebugGameObject.DEBUG_MENU_TYPE)i;
					if (debug_MENU_TYPE == DebugGameObject.DEBUG_MENU_TYPE.DEBUG_GUI_OFF)
					{
						num8 += num4 * 0.125f;
						num6 *= 0.75f;
						num7 *= 0.75f;
					}
					if (GUI.Button(this.CreateGuiRectInRate(rect, new Rect(0f, (num8 + num4 * (float)(i + 1)) / num, num7 / num2, num6 / num), DebugGameObject.GUI_RECT_ANCHOR.CENTER), string.Empty + debug_MENU_TYPE))
					{
						this.m_debugMenuType = debug_MENU_TYPE;
						this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
						this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
						this.m_debugMenuItemNum = 1;
						this.m_debugMenuItemPage = 0;
						this.m_debugMenuMileageEpi = 2;
						this.m_debugMenuMileageCha = 1;
						if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.ITEM)
						{
							if (this.m_debugMenuItemList != null)
							{
								this.m_debugMenuItemList.Clear();
							}
							this.m_debugMenuCharaList = new List<CharacterDataNameInfo.Info>();
							this.m_debugMenuItemList = new Dictionary<DebugGameObject.DEBUG_MENU_ITEM_CATEGORY, List<int>>();
							List<int> list = new List<int>();
							List<int> list2 = new List<int>();
							List<int> list3 = new List<int>();
							list.Add(120000);
							list.Add(120001);
							list.Add(120002);
							list.Add(120003);
							list.Add(120004);
							list.Add(120005);
							list.Add(120006);
							list.Add(120007);
							list.Add(220000);
							list.Add(900000);
							list.Add(910000);
							list.Add(920000);
							list.Add(240000);
							list.Add(230000);
							ServerPlayerState playerState = ServerInterface.PlayerState;
							DataTable.ChaoData[] dataTable = ChaoTable.GetDataTable();
							this.m_debugMenuOtomoList = new List<DataTable.ChaoData>();
							List<int> list4 = new List<int>();
							List<ServerChaoState> list5 = null;
							if (playerState != null && playerState.ChaoStates != null)
							{
								list5 = playerState.ChaoStates;
							}
							if (dataTable != null && dataTable.Length > 0)
							{
								foreach (DataTable.ChaoData chaoData in dataTable)
								{
									if (chaoData.rarity != DataTable.ChaoData.Rarity.NONE && list5 != null && list5.Count > 0)
									{
										this.m_debugMenuOtomoList.Add(chaoData);
									}
								}
							}
							if (this.m_debugMenuOtomoList != null && this.m_debugMenuOtomoList.Count > 0)
							{
								int num9 = this.m_debugMenuOtomoList.Count;
								for (int k = 0; k < num9; k++)
								{
									int item = this.m_debugMenuOtomoList[k].id + 400000;
									if (!list4.Contains(item))
									{
										list2.Add(item);
									}
								}
							}
							if (list4.Count > 0)
							{
								foreach (int num10 in list4)
								{
									list2.Add(num10 + 100000);
								}
							}
							if (playerState != null)
							{
								int num9 = 29;
								for (int l = 0; l < num9; l++)
								{
									CharaType charaType = (CharaType)l;
									ServerCharacterState serverCharacterState = playerState.CharacterState(charaType);
									if (serverCharacterState != null)
									{
										CharacterDataNameInfo.Info dataByID = CharacterDataNameInfo.Instance.GetDataByID(charaType);
										if (dataByID != null)
										{
											list3.Add(dataByID.m_serverID);
											this.m_debugMenuCharaList.Add(dataByID);
										}
									}
								}
							}
							this.m_debugMenuItemList.Add(DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.ITEM, list);
							this.m_debugMenuItemList.Add(DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO, list2);
							this.m_debugMenuItemList.Add(DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.CHARACTER, list3);
						}
						else if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.RANKING)
						{
							if (SingletonGameObject<RankingManager>.Instance != null)
							{
								RankingUtil.Ranker myRank = RankingManager.GetMyRank(RankingUtil.RankingMode.ENDLESS, RankingUtil.RankingRankerType.RIVAL, RankingManager.EndlessRivalRankingScoreType);
								if (myRank != null)
								{
									this.m_debugMenuRankingCurrentRank = myRank.rankIndex + 1;
									this.m_debugMenuRankingCurrentDummyRank = this.m_debugMenuRankingCurrentRank;
									this.m_debugMenuRankingCurrentLegMax = RankingManager.GetCurrentMyLeagueMax(RankingUtil.RankingMode.ENDLESS);
								}
							}
						}
						else if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.DEBUG_GUI_OFF)
						{
							this.m_debugMenu = !this.m_debugMenu;
							this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
							this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
							this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
							AllocationStatus.hide = true;
							SingletonGameObject<DebugGameObject>.Remove();
						}
						else if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.CHAO_TEX_RELEASE)
						{
							this.m_debugMenu = !this.m_debugMenu;
							this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
							this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
							this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
							SingletonGameObject<RankingManager>.Instance.ResetChaoTexture();
							ChaoTextureManager.Instance.RemoveChaoTextureForMainMenuEnd();
						}
						break;
					}
				}
			}
			else
			{
				switch (this.m_debugMenuType)
				{
				case DebugGameObject.DEBUG_MENU_TYPE.ITEM:
					this.GuiMainMenuBtnItem(rect);
					break;
				case DebugGameObject.DEBUG_MENU_TYPE.MILEAGE:
					this.GuiMainMenuBtnMile(rect);
					break;
				case DebugGameObject.DEBUG_MENU_TYPE.RANKING:
					this.GuiMainMenuBtnRanking(rect);
					break;
				case DebugGameObject.DEBUG_MENU_TYPE.DAILY_BATTLE:
					if (SingletonGameObject<DailyBattleManager>.Instance != null)
					{
					}
					break;
				}
			}
			if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 50f, 40f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_RIGHT), "close"))
			{
				if (this.m_debugMenuType == DebugGameObject.DEBUG_MENU_TYPE.NONE)
				{
					this.m_debugMenu = !this.m_debugMenu;
				}
				else if (this.m_debugMenuRankingCateg == DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE && this.m_debugMenuRankingType != RankingUtil.RankingRankerType.COUNT)
				{
					this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
					this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
					this.m_debugMenuMileageEpi = 2;
					this.m_debugMenuMileageCha = 1;
				}
				else
				{
					this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
					this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
					this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
					this.m_debugMenuMileageEpi = 2;
					this.m_debugMenuMileageCha = 1;
				}
			}
		}
		else if (GUI.Button(this.CreateGuiRect(new Rect(0f, 0f, 50f, 40f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_RIGHT), "debug\n menu"))
		{
			this.m_debugMenu = !this.m_debugMenu;
			this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
			this.m_debugMenuRankingCateg = DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE;
			this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
		}
	}

	// Token: 0x060045D9 RID: 17881 RVA: 0x00168C64 File Offset: 0x00166E64
	private void ChangeLoadLangeAtlas()
	{
		if (this.m_debugAtlasLangList != null && this.m_debugAtlasLangList.Count > 0)
		{
			foreach (UIAtlas uiatlas in this.m_debugAtlasLangList)
			{
				string text = this.ChangeAtlasName(uiatlas);
				global::Debug.Log("! " + text);
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, text);
				if (gameObject != null)
				{
					UIAtlas component = gameObject.GetComponent<UIAtlas>();
					if (uiatlas != null && component != null)
					{
						uiatlas.replacement = component;
						uiatlas.name = text;
					}
					global::Debug.Log("!!! " + uiatlas.name);
				}
			}
			Resources.UnloadUnusedAssets();
		}
		this.CheckLoadAtlas();
	}

	// Token: 0x060045DA RID: 17882 RVA: 0x00168D64 File Offset: 0x00166F64
	private void CheckLoadAtlas()
	{
		if (this.m_debugAtlasList != null)
		{
			this.m_debugAtlasList.Clear();
		}
		if (this.m_debugAtlasLangList != null)
		{
			this.m_debugAtlasLangList.Clear();
		}
		this.m_debugAtlasList = new List<UIAtlas>();
		this.m_debugAtlasLangList = new List<UIAtlas>();
		UIAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UIAtlas)) as UIAtlas[];
		if (array != null && array.Length > 0)
		{
			foreach (UIAtlas uiatlas in array)
			{
				if (this.IsLangAtlas(uiatlas))
				{
					this.m_debugAtlasLangList.Add(uiatlas);
				}
				else
				{
					this.m_debugAtlasList.Add(uiatlas);
				}
			}
		}
	}

	// Token: 0x060045DB RID: 17883 RVA: 0x00168E1C File Offset: 0x0016701C
	private bool IsLangAtlas(UIAtlas atlas)
	{
		bool result = false;
		if (atlas != null)
		{
			string[] array = atlas.name.Split(new char[]
			{
				'_'
			});
			if (array != null && array.Length > 1)
			{
				string text = array[array.Length - 1];
				if (!string.IsNullOrEmpty(text) && TextUtility.IsSuffix(text))
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060045DC RID: 17884 RVA: 0x00168E80 File Offset: 0x00167080
	private string ChangeAtlasName(UIAtlas atlas)
	{
		string text = null;
		if (atlas != null)
		{
			string[] array = atlas.name.Split(new char[]
			{
				'_'
			});
			if (array != null && array.Length > 1)
			{
				string text2 = array[array.Length - 1];
				if (!string.IsNullOrEmpty(text2) && TextUtility.IsSuffix(text2))
				{
					text = string.Empty;
					for (int i = 0; i < array.Length - 1; i++)
					{
						text = text + array[i] + "_";
					}
					text += this.m_debugAtlasLangCode;
				}
			}
		}
		return text;
	}

	// Token: 0x060045DD RID: 17885 RVA: 0x00168F18 File Offset: 0x00167118
	private void CheckDrawCall2D()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			List<UIPanel> list = new List<UIPanel>(gameObject.GetComponentsInChildren<UIPanel>());
			if (list.Count > 0)
			{
				this.m_debugDrawCallList = new Dictionary<string, Dictionary<string, List<UIDrawCall>>>();
				for (int i = 0; i < list.Count; i++)
				{
					UIPanel uipanel = list[i];
					if (uipanel != null)
					{
						int num = 0;
						Dictionary<string, List<UIDrawCall>> dictionary = null;
						Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
						for (int j = 0; j < UIDrawCall.list.size; j++)
						{
							UIDrawCall uidrawCall = UIDrawCall.list[j];
							if (!(uidrawCall.panel != uipanel))
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, List<UIDrawCall>>();
								}
								if (!dictionary2.ContainsKey(uidrawCall.material.name))
								{
									dictionary2.Add(uidrawCall.material.name, 1);
								}
								else
								{
									Dictionary<string, int> dictionary4;
									Dictionary<string, int> dictionary3 = dictionary4 = dictionary2;
									string name;
									string key = name = uidrawCall.material.name;
									int num2 = dictionary4[name];
									dictionary3[key] = num2 + 1;
								}
								if (dictionary2.ContainsKey(uidrawCall.material.name))
								{
									string key2 = uidrawCall.material.name + " " + dictionary2[uidrawCall.material.name];
									if (!dictionary.ContainsKey(key2))
									{
										dictionary.Add(key2, new List<UIDrawCall>
										{
											uidrawCall
										});
									}
									else
									{
										dictionary[key2].Add(uidrawCall);
									}
								}
								num++;
							}
						}
						if (dictionary != null)
						{
							this.m_debugDrawCallList.Add(uipanel.name + "  atlas:" + num, dictionary);
						}
					}
				}
			}
			else
			{
				this.m_debugDrawCallList = null;
			}
		}
		else
		{
			this.m_debugDrawCallList = null;
		}
	}

	// Token: 0x060045DE RID: 17886 RVA: 0x00169114 File Offset: 0x00167314
	private void GuiLoadAtlas(Rect target, float sizeW, float sizeH)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.007f, 0.5f, 0.056f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (GUI.Button(position, string.Empty + this.m_debugCheckType))
		{
			this.CheckLoadAtlas();
		}
		if (this.m_debugAtlasList != null || this.m_debugAtlasLangList != null)
		{
			int num = 0;
			float num2 = -0.375f;
			float width = 0.24f;
			float num3 = 0.25f;
			float height = 0.06f;
			float num4 = 0.064f;
			foreach (UIAtlas uiatlas in this.m_debugAtlasList)
			{
				Rect position2 = this.CreateGuiRectInRate(target, new Rect(num2 + num3 * (float)(num % 4), 0.065f + num4 * (float)(num / 4), width, height), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				string text = string.Concat(new object[]
				{
					uiatlas.name,
					" [",
					uiatlas.texture.width,
					"]"
				});
				if (GUI.Button(position2, string.Empty + text))
				{
					int num5 = uiatlas.texture.width * uiatlas.texture.height;
					int num6 = 0;
					int num7 = 0;
					if (uiatlas.spriteList != null)
					{
						num7 = uiatlas.spriteList.Count;
					}
					global::Debug.Log(string.Concat(new object[]
					{
						"===================== ",
						text,
						" spriteNum:",
						num7,
						" ========================"
					}));
					if (num7 > 0)
					{
						int num8 = 0;
						string text2 = string.Empty;
						foreach (UISpriteData uispriteData in uiatlas.spriteList)
						{
							if (string.IsNullOrEmpty(text2))
							{
								text2 = string.Concat(new object[]
								{
									string.Empty,
									num8,
									"  ",
									uispriteData.name,
									" [",
									uispriteData.width,
									"×",
									uispriteData.height,
									"]"
								});
							}
							else
							{
								string text3 = text2;
								text2 = string.Concat(new object[]
								{
									text3,
									"\n",
									num8,
									"  ",
									uispriteData.name,
									" [",
									uispriteData.width,
									"×",
									uispriteData.height,
									"]"
								});
							}
							num6 += uispriteData.width * uispriteData.height;
							num8++;
						}
						global::Debug.Log(text2);
					}
					global::Debug.Log("===================================== useArea: " + (float)((int)((float)num6 / (float)num5 * 1000f)) / 10f + "% =============================================");
				}
				num++;
			}
			foreach (UIAtlas uiatlas2 in this.m_debugAtlasLangList)
			{
				Rect position3 = this.CreateGuiRectInRate(target, new Rect(num2 + num3 * (float)(num % 4), 0.065f + num4 * (float)(num / 4), width, height), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				string text4 = string.Concat(new object[]
				{
					uiatlas2.name,
					" [",
					uiatlas2.texture.width,
					"]"
				});
				if (GUI.Button(position3, string.Empty + text4))
				{
					int num9 = uiatlas2.texture.width * uiatlas2.texture.height;
					int num10 = 0;
					int num11 = 0;
					if (uiatlas2.spriteList != null)
					{
						num11 = uiatlas2.spriteList.Count;
					}
					global::Debug.Log(string.Concat(new object[]
					{
						"===================== ",
						text4,
						" spriteNum:",
						num11,
						" ========================"
					}));
					if (num11 > 0)
					{
						string text5 = string.Empty;
						int num12 = 0;
						foreach (UISpriteData uispriteData2 in uiatlas2.spriteList)
						{
							if (string.IsNullOrEmpty(text5))
							{
								text5 = string.Concat(new object[]
								{
									string.Empty,
									num12,
									"  ",
									uispriteData2.name,
									" [",
									uispriteData2.width,
									"×",
									uispriteData2.height,
									"]"
								});
							}
							else
							{
								string text3 = text5;
								text5 = string.Concat(new object[]
								{
									text3,
									"\n",
									num12,
									"  ",
									uispriteData2.name,
									" [",
									uispriteData2.width,
									"×",
									uispriteData2.height,
									"]"
								});
							}
							num10 += uispriteData2.width * uispriteData2.height;
							num12++;
						}
						global::Debug.Log(text5);
					}
					global::Debug.Log("===================================== useArea: " + (float)((int)((float)num10 / (float)num9 * 1000f)) / 10f + "% =============================================");
				}
				num++;
			}
		}
	}

	// Token: 0x060045DF RID: 17887 RVA: 0x00169768 File Offset: 0x00167968
	private void GuiDrawCall2D(Rect target, float sizeW, float sizeH)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.007f, 0.5f, 0.056f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (this.m_debugDrawCallList != null && this.m_debugDrawCallList.Count > 0)
		{
			if (string.IsNullOrEmpty(this.m_debugDrawCallPanelCurrent))
			{
				if (GUI.Button(position, string.Empty + this.m_debugCheckType))
				{
					this.CheckDrawCall2D();
					this.m_debugDrawCallPanelCurrent = string.Empty;
					this.m_debugDrawCallMatCurrent = string.Empty;
				}
				Dictionary<string, Dictionary<string, List<UIDrawCall>>>.KeyCollection keys = this.m_debugDrawCallList.Keys;
				int num = 0;
				foreach (string text in keys)
				{
					Rect position2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.065f + 0.07f * (float)num, 0.96f, 0.06f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
					if (GUI.Button(position2, string.Empty + text))
					{
						this.m_debugDrawCallPanelCurrent = text;
					}
					num++;
				}
			}
			else if (this.m_debugDrawCallList.ContainsKey(this.m_debugDrawCallPanelCurrent))
			{
				float num2 = -0.25f;
				float num3 = 0.065f;
				if (string.IsNullOrEmpty(this.m_debugDrawCallMatCurrent))
				{
					GUI.Box(position, string.Empty + this.m_debugDrawCallPanelCurrent);
					Dictionary<string, List<UIDrawCall>> dictionary = this.m_debugDrawCallList[this.m_debugDrawCallPanelCurrent];
					Dictionary<string, List<UIDrawCall>>.KeyCollection keys2 = dictionary.Keys;
					int num4 = 0;
					foreach (string str in keys2)
					{
						int num5 = num4 % 2;
						int num6 = 0;
						if (num4 >= 2)
						{
							num6 = num4 / 2;
						}
						Rect position3 = this.CreateGuiRectInRate(target, new Rect(num2 + (float)num5 * 0.5f, num3 + 0.06f * (float)num6, 0.48f, 0.057f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
						GUI.Box(position3, string.Empty + str);
						num4++;
					}
				}
				else
				{
					GUI.Box(position, string.Empty + this.m_debugDrawCallMatCurrent);
					List<UIDrawCall> list = this.m_debugDrawCallList[this.m_debugDrawCallPanelCurrent][this.m_debugDrawCallMatCurrent];
					int num7 = 0;
					foreach (UIDrawCall uidrawCall in list)
					{
						int num8 = num7 % 2;
						int num9 = 0;
						if (num7 >= 2)
						{
							num9 = num7 / 2;
						}
						Rect position4 = this.CreateGuiRectInRate(target, new Rect(num2 + (float)num8 * 0.5f, num3 + 0.06f * (float)num9, 0.48f, 0.057f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
						if (GUI.Button(position4, string.Concat(new object[]
						{
							string.Empty,
							uidrawCall.name,
							" ",
							uidrawCall.gameObject.activeSelf
						})))
						{
							uidrawCall.gameObject.SetActive(!uidrawCall.gameObject.activeSelf);
						}
						num7++;
					}
				}
			}
		}
	}

	// Token: 0x060045E0 RID: 17888 RVA: 0x00169B00 File Offset: 0x00167D00
	private void GuiMainMenuBtnMile(Rect target)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.04f, 0.44f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		GUI.Box(position, "Mileage");
		Rect position2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.18f, 0.32f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (GUI.Button(position2, string.Empty + this.m_debugMenuMileageEpi))
		{
			this.m_debugMenuMileageEpi = 2;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.02f, 0.18f, 0.28f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
		{
			this.m_debugMenuMileageEpi++;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.02f, 0.18f, 0.28f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
		{
			this.m_debugMenuMileageEpi--;
			if (this.m_debugMenuMileageEpi < 2)
			{
				this.m_debugMenuMileageEpi = 2;
			}
		}
		Rect position3 = this.CreateGuiRectInRate(target, new Rect(0f, 0.4f, 0.32f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (GUI.Button(position3, string.Empty + this.m_debugMenuMileageCha))
		{
			this.m_debugMenuMileageCha = 1;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.02f, 0.4f, 0.28f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
		{
			this.m_debugMenuMileageCha++;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.02f, 0.4f, 0.28f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
		{
			this.m_debugMenuMileageCha--;
			if (this.m_debugMenuMileageCha < 1)
			{
				this.m_debugMenuMileageCha = 1;
			}
		}
		Rect position4 = this.CreateGuiRectInRate(target, new Rect(0f, 0.6f, 0.96f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (GUI.Button(position4, "ok"))
		{
			this.GuiMainMenuBtnMileSetting();
		}
		Rect position5 = this.CreateGuiRectInRate(target, new Rect(0f, 0.78f, 0.96f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		GUI.Box(position5, "※ back to title!");
	}

	// Token: 0x060045E1 RID: 17889 RVA: 0x00169D54 File Offset: 0x00167F54
	private void GuiMainMenuBtnMileSetting()
	{
		NetDebugUpdMileageData request = new NetDebugUpdMileageData(new ServerMileageMapState
		{
			m_episode = this.m_debugMenuMileageEpi,
			m_chapter = this.m_debugMenuMileageCha,
			m_point = 0,
			m_stageTotalScore = 0L,
			m_numBossAttack = 0,
			m_stageMaxScore = 0L
		});
		base.StartCoroutine(this.NetworkRequest(request, new DebugGameObject.NetworkRequestSuccessCallback(this.AddOpeMessageMileCallback), new DebugGameObject.NetworkRequestFailedCallback(this.NetworkFailedMileCallback)));
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x00169DCC File Offset: 0x00167FCC
	private void AddOpeMessageMileCallback()
	{
		this.m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;
		HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TITLE);
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x00169DDC File Offset: 0x00167FDC
	private void GuiMainMenuBtnRankingChange(Rect target)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.16f, 0.6f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		if (this.m_debugMenuRankingCurrentRank > 0 && this.m_debugMenuRankingCurrentLegMax > 1)
		{
			GUI.Box(position, "current rank:" + this.m_debugMenuRankingCurrentRank);
			Rect position2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.28f, 0.28f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
			if (GUI.Button(position2, string.Empty + this.m_debugMenuRankingCurrentDummyRank))
			{
				this.m_debugMenuRankingCurrentDummyRank = this.m_debugMenuRankingCurrentRank;
			}
			if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.02f, 0.28f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
			{
				this.m_debugMenuRankingCurrentDummyRank++;
				if (this.m_debugMenuRankingCurrentDummyRank > this.m_debugMenuRankingCurrentLegMax)
				{
					this.m_debugMenuRankingCurrentDummyRank = this.m_debugMenuRankingCurrentLegMax;
				}
			}
			if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.02f, 0.28f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
			{
				this.m_debugMenuRankingCurrentDummyRank--;
				if (this.m_debugMenuRankingCurrentDummyRank < 1)
				{
					this.m_debugMenuRankingCurrentDummyRank = 1;
				}
			}
			if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, 0.48f, 0.76f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), "dummy old rank save"))
			{
				SingletonGameObject<RankingManager>.Instance.SavePlayerRankingDataDummy(RankingUtil.RankingMode.ENDLESS, RankingUtil.RankingRankerType.RIVAL, RankingManager.EndlessRivalRankingScoreType, this.m_debugMenuRankingCurrentDummyRank);
				RankingUI.DebugInitRankingChange();
			}
		}
		else
		{
			GUI.Box(position, "no ranking data");
		}
	}

	// Token: 0x060045E4 RID: 17892 RVA: 0x00169F94 File Offset: 0x00168194
	private void GuiMainMenuBtnRankingCache(Rect target)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			RankingUtil.RankingMode rankingMode = RankingUtil.RankingMode.ENDLESS;
			if (this.m_debugMenuRankingType == RankingUtil.RankingRankerType.COUNT)
			{
				int num = 6;
				int num2 = 0;
				float num3 = 0.8f / (float)num;
				float width = 0.92f;
				for (int i = 0; i < num; i++)
				{
					RankingUtil.RankingRankerType rankingRankerType = (RankingUtil.RankingRankerType)i;
					if (SingletonGameObject<RankingManager>.Instance.IsRankingTop(rankingMode, RankingUtil.RankingScoreType.HIGH_SCORE, rankingRankerType) || SingletonGameObject<RankingManager>.Instance.IsRankingTop(rankingMode, RankingUtil.RankingScoreType.TOTAL_SCORE, rankingRankerType))
					{
						if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, 0.16f + (float)num2 * num3, width, num3 - 0.01f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), string.Empty + rankingRankerType))
						{
							this.m_debugMenuRankingType = rankingRankerType;
						}
						num2++;
					}
				}
			}
			else
			{
				Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.16f, 0.92f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				Rect rect = this.CreateGuiRectInRate(target, new Rect(0f, 0.26f, 0.92f, 0.32f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				Rect rect2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.588f, 0.92f, 0.32f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				Rect position2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.92f, 0.92f, 0.1f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				GUI.Box(position, string.Empty + this.m_debugMenuRankingType);
				if (GUI.Button(position2, "back"))
				{
					this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
				}
				if (SingletonGameObject<RankingManager>.Instance.IsRankingTop(rankingMode, RankingUtil.RankingScoreType.HIGH_SCORE, this.m_debugMenuRankingType))
				{
					this.GuiMainMenuBtnRankingCacheInfo(rect, RankingUtil.RankingScoreType.HIGH_SCORE);
				}
				else
				{
					GUI.Box(rect, "not found");
				}
				if (SingletonGameObject<RankingManager>.Instance.IsRankingTop(rankingMode, RankingUtil.RankingScoreType.TOTAL_SCORE, this.m_debugMenuRankingType))
				{
					this.GuiMainMenuBtnRankingCacheInfo(rect2, RankingUtil.RankingScoreType.TOTAL_SCORE);
				}
				else
				{
					GUI.Box(rect2, "not found");
				}
			}
		}
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x0016A188 File Offset: 0x00168388
	private void GuiMainMenuBtnRankingCacheInfo(Rect rect, RankingUtil.RankingScoreType scoreType)
	{
		string text = string.Empty + scoreType;
		List<RankingUtil.Ranker> cacheRankingList = SingletonGameObject<RankingManager>.Instance.GetCacheRankingList(RankingUtil.RankingMode.ENDLESS, scoreType, this.m_debugMenuRankingType);
		if (cacheRankingList != null && cacheRankingList.Count > 1)
		{
			RankingUtil.Ranker ranker = cacheRankingList[0];
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			if (ranker != null)
			{
				text = text + "  myRank:" + (ranker.rankIndex + 1);
			}
			else
			{
				text += "  myRank:---";
			}
			for (int i = 1; i < cacheRankingList.Count; i++)
			{
				if (num == -1)
				{
					num = cacheRankingList[i].rankIndex + 1;
					num2 = -1;
					num3 = 0;
				}
				else if (num + num3 + 1 != cacheRankingList[i].rankIndex + 1)
				{
					num2 = cacheRankingList[i - 1].rankIndex + 1;
				}
				else
				{
					num3++;
				}
				if (num != -1 && num2 != -1)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\n",
						num,
						" ～ ",
						num2
					});
					num = cacheRankingList[i].rankIndex + 1;
					num2 = -1;
					num3 = 0;
				}
			}
			if (num != -1 && num2 == -1)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n",
					num,
					" ～ ",
					cacheRankingList[cacheRankingList.Count - 1].rankIndex + 1
				});
			}
		}
		GUI.Box(rect, text);
	}

	// Token: 0x060045E6 RID: 17894 RVA: 0x0016A33C File Offset: 0x0016853C
	private void GuiMainMenuBtnRanking(Rect target)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.04f, 0.44f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		GUI.Box(position, string.Empty + this.m_debugMenuRankingCateg);
		int num = (int)this.m_debugMenuRankingCateg;
		int num2 = 2;
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, 0.02f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
		{
			num = (num + 1 + num2) % num2;
			this.m_debugMenuRankingCateg = (DebugGameObject.DEBUG_MENU_RANKING_CATEGORY)num;
			this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, 0.02f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
		{
			num = (num - 1 + num2) % num2;
			this.m_debugMenuRankingCateg = (DebugGameObject.DEBUG_MENU_RANKING_CATEGORY)num;
			this.m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;
		}
		DebugGameObject.DEBUG_MENU_RANKING_CATEGORY debugMenuRankingCateg = this.m_debugMenuRankingCateg;
		if (debugMenuRankingCateg != DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CACHE)
		{
			if (debugMenuRankingCateg == DebugGameObject.DEBUG_MENU_RANKING_CATEGORY.CHANGE_TEST)
			{
				this.GuiMainMenuBtnRankingChange(target);
			}
		}
		else
		{
			this.GuiMainMenuBtnRankingCache(target);
		}
	}

	// Token: 0x060045E7 RID: 17895 RVA: 0x0016A44C File Offset: 0x0016864C
	private void GuiMainMenuBtnItem(Rect target)
	{
		Rect position = this.CreateGuiRectInRate(target, new Rect(0f, 0.04f, 0.44f, 0.08f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
		GUI.Box(position, string.Empty + this.m_debugMenuItemSelect);
		int num = (int)this.m_debugMenuItemSelect;
		int num2 = 3;
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, 0.02f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
		{
			num = (num + 1 + num2) % num2;
			this.m_debugMenuItemNum = 1;
			this.m_debugMenuItemPage = 0;
			this.m_debugMenuItemSelect = (DebugGameObject.DEBUG_MENU_ITEM_CATEGORY)num;
		}
		if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, 0.02f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
		{
			num = (num - 1 + num2) % num2;
			this.m_debugMenuItemNum = 1;
			this.m_debugMenuItemPage = 0;
			this.m_debugMenuItemSelect = (DebugGameObject.DEBUG_MENU_ITEM_CATEGORY)num;
		}
		if ((this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.ITEM || this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO) && this.m_debugMenuItemList.ContainsKey(this.m_debugMenuItemSelect) && this.m_debugMenuItemList[this.m_debugMenuItemSelect].Count > 0)
		{
			Rect position2 = this.CreateGuiRectInRate(target, new Rect(0f, 0.15f, 0.24f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
			if (GUI.Button(position2, string.Empty + this.m_debugMenuItemNum))
			{
				this.m_debugMenuItemNum = 1;
			}
			if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.ITEM)
			{
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, 0.15f, 0.144f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">>"))
				{
					this.m_debugMenuItemNum += 10;
				}
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.18f, 0.15f, 0.16f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
				{
					this.m_debugMenuItemNum++;
				}
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, 0.15f, 0.144f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<<"))
				{
					this.m_debugMenuItemNum -= 10;
					if (this.m_debugMenuItemNum < 1)
					{
						this.m_debugMenuItemNum = 1;
					}
				}
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.18f, 0.15f, 0.16f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
				{
					this.m_debugMenuItemNum--;
					if (this.m_debugMenuItemNum < 1)
					{
						this.m_debugMenuItemNum = 1;
					}
				}
			}
			else if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO)
			{
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, 0.15f, 0.36f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
				{
					this.m_debugMenuItemNum++;
					if (this.m_debugMenuItemNum > 6)
					{
						this.m_debugMenuItemNum = 6;
					}
				}
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, 0.15f, 0.36f, 0.12f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
				{
					this.m_debugMenuItemNum--;
					if (this.m_debugMenuItemNum < 1)
					{
						this.m_debugMenuItemNum = 1;
					}
				}
			}
		}
		if (this.m_debugMenuItemList.ContainsKey(this.m_debugMenuItemSelect) && this.m_debugMenuItemList[this.m_debugMenuItemSelect].Count > 0)
		{
			List<int> list = this.m_debugMenuItemList[this.m_debugMenuItemSelect];
			int num3 = 5;
			float num4 = 0.155f;
			if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.ITEM || this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO)
			{
				num3 = 4;
				num4 = 0.275f;
				if (list.Count > num3)
				{
					if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, num4, 0.136f, 0.72f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
					{
						this.m_debugMenuItemPage++;
						if (this.m_debugMenuItemPage * num3 >= list.Count)
						{
							this.m_debugMenuItemPage--;
						}
					}
					if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, num4, 0.136f, 0.72f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
					{
						this.m_debugMenuItemPage--;
						if (this.m_debugMenuItemPage < 0)
						{
							this.m_debugMenuItemPage = 0;
						}
					}
				}
			}
			else if (list.Count > num3)
			{
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(-0.01f, num4, 0.136f, 0.84f), DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP), ">"))
				{
					this.m_debugMenuItemPage++;
					if (this.m_debugMenuItemPage * num3 >= list.Count)
					{
						this.m_debugMenuItemPage--;
					}
				}
				if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0.01f, num4, 0.136f, 0.84f), DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP), "<"))
				{
					this.m_debugMenuItemPage--;
					if (this.m_debugMenuItemPage < 0)
					{
						this.m_debugMenuItemPage = 0;
					}
				}
			}
			for (int i = 0; i < num3; i++)
			{
				int num5 = i + this.m_debugMenuItemPage * num3;
				if (num5 >= list.Count)
				{
					break;
				}
				int num6 = list[num5];
				int num7 = i;
				bool flag = true;
				string text = num6.ToString();
				if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.ITEM)
				{
					text = string.Empty + (ServerItem.Id)num6;
				}
				else if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO)
				{
					if (this.m_debugMenuOtomoList != null && this.m_debugMenuOtomoList.Count > num5)
					{
						text = this.m_debugMenuOtomoList[num5].name;
						if (this.m_debugMenuOtomoList[num5].id + 400000 != num6)
						{
							flag = false;
							text += "\n Unimplemented";
						}
						else
						{
							int level = this.m_debugMenuOtomoList[num5].level;
							if (level >= 0)
							{
								text = text + "\n Lv:" + level;
							}
							else
							{
								text += "\n not have";
							}
						}
					}
				}
				else if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.CHARACTER)
				{
					text = "[" + num6 + "]";
					if (this.m_debugMenuCharaList != null && this.m_debugMenuCharaList.Count > num5)
					{
						text = this.m_debugMenuCharaList[num5].m_name;
					}
				}
				if (flag)
				{
					if (GUI.Button(this.CreateGuiRectInRate(target, new Rect(0f, num4 + (float)num7 * 0.173f, 0.62f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), text))
					{
						if (this.m_debugMenuItemSelect == DebugGameObject.DEBUG_MENU_ITEM_CATEGORY.OTOMO && this.m_debugMenuItemNum > 1)
						{
							for (int j = 0; j < this.m_debugMenuItemNum - 1; j++)
							{
								this.DebugRequestGiftItem(num6, 1, null, false);
							}
							this.DebugRequestGiftItem(num6, 1, null, true);
						}
						else
						{
							this.DebugRequestGiftItem(num6, this.m_debugMenuItemNum, null, true);
						}
					}
				}
				else
				{
					GUI.Box(this.CreateGuiRectInRate(target, new Rect(0f, num4 + (float)num7 * 0.173f, 0.62f, 0.16f), DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP), text);
				}
			}
		}
	}

	// Token: 0x060045E8 RID: 17896 RVA: 0x0016ABF0 File Offset: 0x00168DF0
	private void GuiRoulette()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
	}

	// Token: 0x060045E9 RID: 17897 RVA: 0x0016AC40 File Offset: 0x00168E40
	private void GuiPlayerCharaList()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
	}

	// Token: 0x060045EA RID: 17898 RVA: 0x0016AC90 File Offset: 0x00168E90
	private void GuiRaid()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
	}

	// Token: 0x060045EB RID: 17899 RVA: 0x0016ACE0 File Offset: 0x00168EE0
	private void GuiRanking()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
	}

	// Token: 0x060045EC RID: 17900 RVA: 0x0016AD30 File Offset: 0x00168F30
	private void GuiCurrentScoreTest()
	{
		if (Application.loadedLevelName.IndexOf("title") != -1 || Application.loadedLevelName.IndexOf("playingstage") != -1)
		{
			return;
		}
		if (Application.loadedLevelName.IndexOf("MainMenu") == -1)
		{
			return;
		}
		if (this.m_currentScore >= 0 || this.m_currentScoreEvent >= 0)
		{
			RankingManager instance = SingletonGameObject<RankingManager>.Instance;
			if (instance != null)
			{
				if (this.m_currentScore >= 0 && GUI.Button(new Rect(30f, 110f, 150f, 40f), string.Concat(new object[]
				{
					"Current ",
					this.targetRankingRankerType,
					"\n",
					this.m_currentScore
				})))
				{
					RankingUtil.DebugCurrentRanking(false, (long)this.m_currentScore);
				}
				if (this.m_currentScoreEvent >= 0 && GUI.Button(new Rect(30f, 165f, 150f, 40f), "Current Rank Event\n" + this.m_currentScoreEvent))
				{
					RankingUtil.DebugCurrentRanking(true, (long)this.m_currentScoreEvent);
				}
			}
		}
	}

	// Token: 0x060045ED RID: 17901 RVA: 0x0016AE6C File Offset: 0x0016906C
	private void Update()
	{
		this.m_currentTimeScale = Time.timeScale;
		if (this.m_debugRouletteTime > 0f)
		{
			this.m_debugRouletteTime -= Time.deltaTime;
			if (this.m_debugRouletteTime <= 0f)
			{
				if (this.m_rouletteCommitMsg != null && !this.m_debugRouletteConectError)
				{
					this.m_rouletteCallback.SendMessage("ServerCommitWheelSpinGeneral_Succeeded", this.m_rouletteCommitMsg);
				}
				else
				{
					MsgServerConnctFailed value = new MsgServerConnctFailed(ServerInterface.StatusCode.AlreadyEndEvent);
					this.m_rouletteCallback.SendMessage("ServerCommitWheelSpinGeneral_Failed", value);
					this.m_debugRouletteConectError = false;
				}
				this.m_debugRouletteTime = 0f;
			}
		}
		if (this.m_debugGetRouletteTime > 0f)
		{
			this.m_debugGetRouletteTime -= Time.deltaTime;
			if (this.m_debugGetRouletteTime <= 0f)
			{
				if (this.m_rouletteGetMsg != null && !this.m_debugRouletteConectError)
				{
					this.m_rouletteCallback.SendMessage("ServerGetWheelOptionsGeneral_Succeeded", this.m_rouletteGetMsg);
				}
				else
				{
					MsgServerConnctFailed value2 = new MsgServerConnctFailed(ServerInterface.StatusCode.AlreadyEndEvent);
					this.m_rouletteCallback.SendMessage("ServerWheelOptionsGeneral_Failed", value2);
					this.m_debugRouletteConectError = false;
				}
				this.m_debugGetRouletteTime = 0f;
			}
		}
		base.enabled = false;
	}

	// Token: 0x060045EE RID: 17902 RVA: 0x0016AFB0 File Offset: 0x001691B0
	private void ShowUpdCost()
	{
		if (this.m_currentUpdCost != null && this.m_keys != null)
		{
			if (this.m_updateCostList == null)
			{
				this.m_updateCostList = new List<string>();
			}
			int num = this.m_keys.Count - this.m_updateCostList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					this.m_updateCostList.Add("---");
				}
			}
			int num2 = 0;
			foreach (string text in this.m_keys)
			{
				if (num2 >= this.m_updateCostList.Count)
				{
					break;
				}
				this.m_updateCostList[num2] = text + " : " + this.m_updCost[text];
				num2++;
			}
		}
	}

	// Token: 0x060045EF RID: 17903 RVA: 0x0016B0C8 File Offset: 0x001692C8
	private bool SetUpdCost(string name, float rate)
	{
		if (string.IsNullOrEmpty(name))
		{
			return false;
		}
		bool result = false;
		if (this.m_currentUpdCost != null && this.m_updCost != null && this.m_currentUpdCost.ContainsKey(name) && this.m_updCost.ContainsKey(name))
		{
			int num = 0;
			if (this.m_currentUpdCost[name] > 0)
			{
				num = Mathf.FloorToInt((float)this.m_currentUpdCost[name] / 2f * rate);
			}
			if (name == "TOTAL_COST")
			{
				this.m_updateCost = num;
			}
			this.m_updCost[name] = num;
			this.m_currentUpdCost[name] = 0;
		}
		return result;
	}

	// Token: 0x060045F0 RID: 17904 RVA: 0x0016B180 File Offset: 0x00169380
	private bool CheckDummyRequest()
	{
		bool result = true;
		if (UnityEngine.Random.Range(0, 100) < this.m_rouletteDummyError)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060045F1 RID: 17905 RVA: 0x0016B1A8 File Offset: 0x001693A8
	private void DummyRequestWheelOptionsItem(RouletteCategory cate, int rank, ref ServerWheelOptionsGeneral wheel, int max = 8)
	{
		if (wheel != null)
		{
			List<ServerItem.Id> list;
			switch (cate)
			{
			case RouletteCategory.PREMIUM:
				list = this.m_rouletteDataPremium;
				goto IL_76;
			case RouletteCategory.ITEM:
				list = this.m_rouletteDataItem;
				goto IL_76;
			case RouletteCategory.RAID:
				list = this.m_rouletteDataRaid;
				goto IL_76;
			case RouletteCategory.SPECIAL:
				list = this.m_rouletteDataSpecial;
				goto IL_76;
			}
			list = this.m_rouletteDataDefault;
			IL_76:
			for (int i = 0; i < max; i++)
			{
				int itemId = 120000;
				int num = 1;
				int weight = 1;
				if (list != null && list.Count > 0)
				{
					int num2 = (i + max * rank) % list.Count;
					if (num2 < 0)
					{
						num2 = 0;
					}
					itemId = (int)list[num2];
					if (list[num2] == ServerItem.Id.RING)
					{
						num = 1000 * (rank + 1);
					}
					else if (list[num2] == ServerItem.Id.RSRING)
					{
						num = 10 * (rank + 1);
					}
					else if (list[num2] == ServerItem.Id.ENERGY)
					{
						num = 2 * (rank + 1);
					}
					else if (list[num2] == ServerItem.Id.INVINCIBLE || list[num2] == ServerItem.Id.COMBO || list[num2] == ServerItem.Id.BARRIER || list[num2] == ServerItem.Id.MAGNET || list[num2] == ServerItem.Id.TRAMPOLINE || list[num2] == ServerItem.Id.DRILL)
					{
						num = 1 * (rank + 1);
					}
					if (num < 1)
					{
						num = 1;
					}
				}
				wheel.SetupItem(i, itemId, weight, num);
			}
		}
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x0016B360 File Offset: 0x00169560
	private ServerWheelOptionsGeneral DummyRequestWheelOptionsGeneral(int rouletteId, int rank, int costItemId, int costItemNum, int costItemStock)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		list.Add(costItemId);
		list2.Add(costItemNum);
		list3.Add(costItemStock);
		return this.DummyRequestWheelOptionsGeneral(rouletteId, rank, list, list2, list3);
	}

	// Token: 0x060045F3 RID: 17907 RVA: 0x0016B3A4 File Offset: 0x001695A4
	private ServerWheelOptionsGeneral DummyRequestWheelOptionsGeneral(int rouletteId, int rank, List<int> costItemId, List<int> costItemNum, List<int> costItemStock)
	{
		ServerWheelOptionsGeneral serverWheelOptionsGeneral = new ServerWheelOptionsGeneral();
		int remaining = 0;
		RouletteCategory rouletteCategory = (RouletteCategory)rouletteId;
		DateTime nextFree = DateTime.Now.AddDays(999.0);
		if (rank > 0)
		{
			remaining = 1;
		}
		if (rouletteCategory == RouletteCategory.ITEM)
		{
			remaining = 5;
		}
		else if (rank > 0)
		{
			remaining = 1;
		}
		if (rouletteCategory == RouletteCategory.PREMIUM || rouletteCategory == RouletteCategory.SPECIAL)
		{
			rank = 0;
		}
		if (rouletteCategory == RouletteCategory.PREMIUM && RouletteManager.Instance != null && RouletteManager.Instance.specialEgg >= 10)
		{
			rouletteCategory = RouletteCategory.SPECIAL;
			rouletteId = 8;
		}
		this.DummyRequestWheelOptionsItem(rouletteCategory, rank, ref serverWheelOptionsGeneral, 8);
		serverWheelOptionsGeneral.SetupParam(rouletteId, remaining, 9999999, rank, 5, nextFree);
		serverWheelOptionsGeneral.ResetupCostItem();
		for (int i = 0; i < costItemId.Count; i++)
		{
			int num = costItemId[i];
			int oneCost = costItemNum[i];
			int itemNum = costItemStock[i];
			if (num > 0)
			{
				serverWheelOptionsGeneral.AddCostItem(num, itemNum, oneCost);
			}
		}
		return serverWheelOptionsGeneral;
	}

	// Token: 0x060045F4 RID: 17908 RVA: 0x0016B4A4 File Offset: 0x001696A4
	public void DummyRequestGetRouletteGeneral(int eventId, int rouletteId, GameObject callbackObject)
	{
		if (callbackObject != null)
		{
			this.m_rouletteGetMsg = new MsgGetWheelOptionsGeneralSucceed();
			int costItemId;
			switch (rouletteId)
			{
			case 1:
			case 8:
				costItemId = 900000;
				goto IL_6B;
			case 2:
				costItemId = 910000;
				goto IL_6B;
			}
			costItemId = 960000;
			IL_6B:
			this.m_rouletteCallback = callbackObject;
			this.m_rouletteGetMsg.m_wheelOptionsGeneral = this.DummyRequestWheelOptionsGeneral(rouletteId, 0, costItemId, 5, 100);
			this.m_debugGetRouletteTime = this.m_rouletteConectTime;
			this.m_debugRouletteConectError = !this.CheckDummyRequest();
			if (this.m_debugRouletteConectError)
			{
				this.m_debugGetRouletteTime = this.m_rouletteConectTime * 3f;
			}
		}
	}

	// Token: 0x060045F5 RID: 17909 RVA: 0x0016B574 File Offset: 0x00169774
	public void DummyRequestCommitRouletteGeneral(ServerWheelOptionsGeneral org, int eventId, int rouletteId, int spinCostItemId, int spinNum, GameObject callbackObject)
	{
		if (this.m_debugRouletteTime > 0f)
		{
			this.m_rouletteCommitMsg = null;
			return;
		}
		if (callbackObject != null)
		{
			bool flag = false;
			MsgCommitWheelSpinGeneralSucceed msgCommitWheelSpinGeneralSucceed = new MsgCommitWheelSpinGeneralSucceed();
			ServerSpinResultGeneral serverSpinResultGeneral = new ServerSpinResultGeneral();
			List<ServerItemState> list = new List<ServerItemState>();
			List<ServerChaoData> list2 = new List<ServerChaoData>();
			int num = 0;
			int rank = 0;
			int num2 = 0;
			List<int> list3 = new List<int>();
			if (spinNum <= 1)
			{
				for (int i = 0; i < org.itemLenght; i++)
				{
					list3.Add(i);
				}
				num = list3[UnityEngine.Random.Range(0, list3.Count)];
				int num3;
				int num4;
				float num5;
				org.GetCell(num, out num3, out num4, out num5);
				if (num3 == 200000 || num3 == 200001)
				{
					rank = (int)(org.rank + 1);
				}
				else
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (num3 >= 400000 && num3 < 500000)
					{
						ServerChaoData serverChaoData = new ServerChaoData();
						serverChaoData.Id = num3;
						serverChaoData.Level = 0;
						serverChaoData.Rarity = 100;
						bool flag2 = false;
						ServerChaoState serverChaoState = playerState.ChaoStateByItemID(num3);
						if (serverChaoState != null)
						{
							switch (serverChaoState.Status)
							{
							case ServerChaoState.ChaoStatus.NotOwned:
								serverChaoData.Level = 0;
								break;
							case ServerChaoState.ChaoStatus.Owned:
								serverChaoData.Level = serverChaoState.Level + 1;
								break;
							case ServerChaoState.ChaoStatus.MaxLevel:
								serverChaoData.Level = 5;
								flag2 = true;
								break;
							}
							if (flag2)
							{
								ServerItemState serverItemState = new ServerItemState();
								serverItemState.m_itemId = 220000;
								serverItemState.m_num = 4;
								num2 += 4;
								list.Add(serverItemState);
							}
						}
						serverChaoData.Rarity = num3 / 1000 % 10;
						list2.Add(serverChaoData);
					}
					else if (num3 >= 300000 && num3 < 400000)
					{
						list2.Add(new ServerChaoData
						{
							Id = num3,
							Level = 0,
							Rarity = 100
						});
						ServerCharacterState serverCharacterState = playerState.CharacterStateByItemID(num3);
						if (serverCharacterState != null && serverCharacterState.Id >= 0 && serverCharacterState.IsUnlocked)
						{
							list.Add(new ServerItemState
							{
								m_itemId = 900000,
								m_num = 99
							});
							list.Add(new ServerItemState
							{
								m_itemId = 910000,
								m_num = 1234
							});
							ServerItemState serverItemState2 = new ServerItemState();
							serverItemState2.m_itemId = 220000;
							serverItemState2.m_num = 5;
							num2 += 5;
							list.Add(serverItemState2);
						}
					}
					else
					{
						list.Add(new ServerItemState
						{
							m_itemId = num3,
							m_num = num4
						});
					}
				}
			}
			else if (org.rank == RouletteUtility.WheelRank.Normal)
			{
				num = -1;
				List<int> list4 = new List<int>();
				List<int> list5 = new List<int>();
				list4.Add(910000);
				list4.Add(120000);
				list4.Add(120001);
				list4.Add(120003);
				list4.Add(220000);
				list4.Add(120004);
				list4.Add(120005);
				list4.Add(120006);
				list4.Add(120007);
				list4.Add(220000);
				list4.Add(900000);
				list5.Add(400000);
				list5.Add(400001);
				list5.Add(400002);
				list5.Add(300000);
				list5.Add(400003);
				list5.Add(400019);
				list5.Add(300004);
				list5.Add(401000);
				list5.Add(401001);
				list5.Add(401002);
				list5.Add(300001);
				list5.Add(401003);
				list5.Add(401004);
				list5.Add(300005);
				int num6 = UnityEngine.Random.Range(0, list4.Count);
				for (int j = 0; j < spinNum; j++)
				{
					ServerItemState serverItemState3 = new ServerItemState();
					serverItemState3.m_itemId = list4[(j + num6) % list4.Count];
					serverItemState3.m_num = 1;
					if (serverItemState3.m_itemId == 220000)
					{
						num2++;
					}
					list.Add(serverItemState3);
				}
				num6 = UnityEngine.Random.Range(0, list5.Count);
				for (int k = 0; k < spinNum; k++)
				{
					ServerChaoData serverChaoData2 = new ServerChaoData();
					serverChaoData2.Id = list5[(k + num6) % list5.Count];
					serverChaoData2.Level = 5;
					if (serverChaoData2.Id >= 300000 && serverChaoData2.Id < 400000)
					{
						serverChaoData2.Level = 0;
						serverChaoData2.Rarity = 100;
						list2.Add(serverChaoData2);
					}
					else
					{
						serverChaoData2.Rarity = serverChaoData2.Id / 1000 % 10;
						list2.Add(serverChaoData2);
						list2.Add(serverChaoData2);
						list2.Add(serverChaoData2);
						list2.Add(serverChaoData2);
						list2.Add(serverChaoData2);
						list2.Add(serverChaoData2);
					}
				}
			}
			else
			{
				flag = true;
			}
			for (int l = 0; l < list.Count; l++)
			{
				serverSpinResultGeneral.AddItemState(list[l]);
			}
			for (int m = 0; m < list2.Count; m++)
			{
				serverSpinResultGeneral.AddChaoState(list2[m]);
			}
			serverSpinResultGeneral.ItemWon = num;
			this.m_rouletteCallback = callbackObject;
			int costItemId;
			switch (org.rouletteId)
			{
			case 1:
			case 8:
				costItemId = 900000;
				goto IL_62D;
			case 2:
				costItemId = 910000;
				goto IL_62D;
			}
			costItemId = 960000;
			IL_62D:
			ServerWheelOptionsGeneral serverWheelOptionsGeneral = this.DummyRequestWheelOptionsGeneral(org.rouletteId, rank, costItemId, 5, 100);
			serverWheelOptionsGeneral.spEgg = RouletteManager.Instance.specialEgg + num2;
			if (!flag)
			{
				msgCommitWheelSpinGeneralSucceed.m_playerState = ServerInterface.PlayerState;
				msgCommitWheelSpinGeneralSucceed.m_wheelOptionsGeneral = serverWheelOptionsGeneral;
				msgCommitWheelSpinGeneralSucceed.m_resultSpinResultGeneral = serverSpinResultGeneral;
				this.m_rouletteCommitMsg = msgCommitWheelSpinGeneralSucceed;
				this.m_debugRouletteTime = this.m_rouletteConectTime;
				this.m_debugRouletteConectError = !this.CheckDummyRequest();
				if (this.m_debugRouletteConectError)
				{
					this.m_debugRouletteTime = 3f;
				}
			}
			else
			{
				this.m_rouletteCommitMsg = null;
				this.m_debugRouletteTime = 0.1f;
				this.m_debugRouletteConectError = true;
			}
		}
	}

	// Token: 0x060045F6 RID: 17910 RVA: 0x0016BC50 File Offset: 0x00169E50
	public ServerSpinResultGeneral DummyRouletteGeneralResult(int spinNum)
	{
		ServerSpinResultGeneral serverSpinResultGeneral = new ServerSpinResultGeneral();
		List<ServerItemState> list = new List<ServerItemState>();
		List<ServerChaoData> list2 = new List<ServerChaoData>();
		int itemWon;
		if (spinNum <= 1)
		{
			itemWon = 1;
			list.Add(new ServerItemState
			{
				m_itemId = 120001,
				m_num = 1
			});
		}
		else
		{
			itemWon = -1;
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			list3.Add(910000);
			list3.Add(120000);
			list3.Add(120001);
			list3.Add(120003);
			list3.Add(220000);
			list3.Add(120004);
			list3.Add(120005);
			list3.Add(120006);
			list3.Add(120007);
			list3.Add(220000);
			list3.Add(900000);
			list4.Add(400000);
			list4.Add(400001);
			list4.Add(400002);
			list4.Add(300000);
			list4.Add(400003);
			list4.Add(400019);
			list4.Add(300004);
			list4.Add(401000);
			list4.Add(401001);
			list4.Add(401002);
			list4.Add(300001);
			list4.Add(401003);
			list4.Add(401004);
			list4.Add(300005);
			int num = UnityEngine.Random.Range(0, list3.Count);
			for (int i = 0; i < spinNum; i++)
			{
				list.Add(new ServerItemState
				{
					m_itemId = list3[(i + num) % list3.Count],
					m_num = 1
				});
			}
			num = UnityEngine.Random.Range(0, list4.Count);
			for (int j = 0; j < spinNum; j++)
			{
				ServerChaoData serverChaoData = new ServerChaoData();
				serverChaoData.Id = list4[(j + num) % list4.Count];
				serverChaoData.Level = 5;
				if (serverChaoData.Id >= 300000 && serverChaoData.Id < 400000)
				{
					serverChaoData.Level = 0;
					serverChaoData.Rarity = 100;
					list2.Add(serverChaoData);
				}
				else
				{
					serverChaoData.Rarity = serverChaoData.Id / 1000 % 10;
					list2.Add(serverChaoData);
					list2.Add(serverChaoData);
					list2.Add(serverChaoData);
					list2.Add(serverChaoData);
					list2.Add(serverChaoData);
					list2.Add(serverChaoData);
				}
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			serverSpinResultGeneral.AddItemState(list[k]);
		}
		for (int l = 0; l < list2.Count; l++)
		{
			serverSpinResultGeneral.AddChaoState(list2[l]);
		}
		serverSpinResultGeneral.ItemWon = itemWon;
		return serverSpinResultGeneral;
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x0016BF68 File Offset: 0x0016A168
	public Rect CreateGuiRectIn(Rect target, Rect rect, DebugGameObject.GUI_RECT_ANCHOR anchor = DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP)
	{
		Rect rect2 = new Rect(rect.x / target.width, rect.y / target.height, rect.width / target.width, rect.height / target.height);
		return this.CreateGuiRectInRate(target, rect2, anchor);
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x0016BFC4 File Offset: 0x0016A1C4
	public Rect CreateGuiRectInRate(Rect target, Rect rect, DebugGameObject.GUI_RECT_ANCHOR anchor = DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP)
	{
		rect.x *= target.width;
		rect.y *= target.height;
		rect.width *= target.width;
		rect.height *= target.height;
		Rect result = new Rect(rect.x, rect.y, rect.width, rect.height);
		float num = 0f;
		float num2 = 0f;
		switch (anchor)
		{
		case DebugGameObject.GUI_RECT_ANCHOR.CENTER:
			num = target.width * 0.5f - rect.width * 0.5f;
			num2 = target.height * 0.5f - rect.height * 0.5f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.CENTER_LEFT:
			num = 0f;
			num2 = target.height * 0.5f - rect.height * 0.5f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.CENTER_RIGHT:
			num = target.width - rect.width;
			num2 = target.height * 0.5f - rect.height * 0.5f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP:
			num = target.width * 0.5f - rect.width * 0.5f;
			num2 = 0f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM:
			num = target.width * 0.5f - rect.width * 0.5f;
			num2 = target.height - rect.height;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP:
			num = 0f;
			num2 = 0f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM:
			num = 0f;
			num2 = target.height - rect.height;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP:
			num = target.width - rect.width;
			num2 = 0f;
			break;
		case DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM:
			num = target.width - rect.width;
			num2 = target.height - rect.height;
			break;
		}
		num += target.x;
		num2 += target.y;
		result.x = num + rect.x;
		result.y = num2 + rect.y;
		result.width = rect.width;
		result.height = rect.height;
		return result;
	}

	// Token: 0x060045F9 RID: 17913 RVA: 0x0016C230 File Offset: 0x0016A430
	public Rect CreateGuiRect(Rect rect, DebugGameObject.GUI_RECT_ANCHOR anchor = DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP)
	{
		Rect rect2 = new Rect(rect.x / 800f, rect.y / 450f, rect.width / 800f, rect.height / 450f);
		return this.CreateGuiRectRate(rect2, anchor);
	}

	// Token: 0x060045FA RID: 17914 RVA: 0x0016C280 File Offset: 0x0016A480
	public Rect CreateGuiRectRate(Rect rect, DebugGameObject.GUI_RECT_ANCHOR anchor = DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP)
	{
		if (this.m_camera == null)
		{
			Rect rect2 = new Rect(rect.x, rect.y, rect.width, rect.height);
			rect2.x = rect.x;
			rect2.y = rect.y;
			rect2.width = rect.width;
			rect2.height = rect.height;
			return rect;
		}
		rect.x *= this.m_camera.pixelRect.xMax;
		rect.y *= this.m_camera.pixelRect.yMax;
		rect.width *= this.m_camera.pixelRect.xMax;
		rect.height *= this.m_camera.pixelRect.yMax;
		Rect result = new Rect(rect.x, rect.y, rect.width, rect.height);
		if (this.m_camera != null)
		{
			float num = 0f;
			float num2 = 0f;
			switch (anchor)
			{
			case DebugGameObject.GUI_RECT_ANCHOR.CENTER:
				num = this.m_camera.pixelRect.xMax * 0.5f - rect.width * 0.5f;
				num2 = this.m_camera.pixelRect.yMax * 0.5f - rect.height * 0.5f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.CENTER_LEFT:
				num = 0f;
				num2 = this.m_camera.pixelRect.yMax * 0.5f - rect.height * 0.5f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.CENTER_RIGHT:
				num = this.m_camera.pixelRect.xMax - rect.width;
				num2 = this.m_camera.pixelRect.yMax * 0.5f - rect.height * 0.5f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP:
				num = this.m_camera.pixelRect.xMax * 0.5f - rect.width * 0.5f;
				num2 = 0f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM:
				num = this.m_camera.pixelRect.xMax * 0.5f - rect.width * 0.5f;
				num2 = this.m_camera.pixelRect.yMax - rect.height;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.LEFT_TOP:
				num = 0f;
				num2 = 0f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.LEFT_BOTTOM:
				num = 0f;
				num2 = this.m_camera.pixelRect.yMax - rect.height;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.RIGHT_TOP:
				num = this.m_camera.pixelRect.xMax - rect.width;
				num2 = 0f;
				break;
			case DebugGameObject.GUI_RECT_ANCHOR.RIGHT_BOTTOM:
				num = this.m_camera.pixelRect.xMax - rect.width;
				num2 = this.m_camera.pixelRect.yMax - rect.height;
				break;
			}
			result.x = num + rect.x;
			result.y = num2 + rect.y;
			result.width = rect.width;
			result.height = rect.height;
		}
		return result;
	}

	// Token: 0x060045FB RID: 17915 RVA: 0x0016C628 File Offset: 0x0016A828
	public bool IsDebugGift()
	{
		return this.m_debugGiftItemId == 0;
	}

	// Token: 0x060045FC RID: 17916 RVA: 0x0016C634 File Offset: 0x0016A834
	public bool DebugRequestUpPoint(int rsring, int ring = 0, int energy = 0)
	{
		NetDebugUpdPointData netDebugUpdPointData = new NetDebugUpdPointData(energy, 0, ring, 0, rsring, 0);
		netDebugUpdPointData.Request();
		return true;
	}

	// Token: 0x060045FD RID: 17917 RVA: 0x0016C654 File Offset: 0x0016A854
	public bool DebugRequestGiftItem(int itemId, int num, GameObject callbackObject, bool response = true)
	{
		this.PopLog(string.Concat(new object[]
		{
			"item:",
			itemId,
			"×",
			num,
			" response:",
			response
		}), 0f, 0f, DebugGameObject.GUI_RECT_ANCHOR.CENTER);
		if (response)
		{
			if (!this.IsDebugGift())
			{
				return false;
			}
			this.m_debugGiftItemId = itemId;
			this.m_debugGiftCallback = callbackObject;
		}
		NetDebugAddOpeMessage.OpeMsgInfo opeMsgInfo = new NetDebugAddOpeMessage.OpeMsgInfo();
		opeMsgInfo.userID = SystemSaveManager.GetGameID();
		opeMsgInfo.messageKind = 1;
		opeMsgInfo.infoId = 0;
		opeMsgInfo.itemId = itemId;
		opeMsgInfo.numItem = num;
		opeMsgInfo.additionalInfo1 = 0;
		opeMsgInfo.additionalInfo2 = 1;
		opeMsgInfo.msgTitle = "debug";
		opeMsgInfo.msgContent = "Debug Gift Item " + itemId;
		opeMsgInfo.msgImageId = string.Empty + opeMsgInfo.itemId;
		NetDebugAddOpeMessage netDebugAddOpeMessage = new NetDebugAddOpeMessage(opeMsgInfo);
		if (response)
		{
			base.StartCoroutine(this.NetworkRequest(netDebugAddOpeMessage, new DebugGameObject.NetworkRequestSuccessCallback(this.AddOpeMessageCallback), new DebugGameObject.NetworkRequestFailedCallback(this.NetworkFailedCallback)));
		}
		else
		{
			netDebugAddOpeMessage.Request();
		}
		return true;
	}

	// Token: 0x060045FE RID: 17918 RVA: 0x0016C78C File Offset: 0x0016A98C
	private IEnumerator NetworkRequest(NetBase request, DebugGameObject.NetworkRequestSuccessCallback successCallback, DebugGameObject.NetworkRequestFailedCallback failedCallback)
	{
		request.Request();
		while (request.IsExecuting())
		{
			yield return null;
		}
		if (request.IsSucceeded())
		{
			if (successCallback != null)
			{
				successCallback();
			}
		}
		else if (failedCallback != null)
		{
			failedCallback(request.resultStCd);
		}
		yield break;
	}

	// Token: 0x060045FF RID: 17919 RVA: 0x0016C7CC File Offset: 0x0016A9CC
	private void AddOpeMessageCallback()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMessageList(base.gameObject);
		}
	}

	// Token: 0x06004600 RID: 17920 RVA: 0x0016C7F8 File Offset: 0x0016A9F8
	private void ServerGetMessageList_Succeeded(MsgGetMessageListSucceed msg)
	{
		if (this.m_debugGiftCallback == null)
		{
			this.m_debugGiftItemId = 0;
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			return;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			List<ServerOperatorMessageEntry> operatorMessageList = ServerInterface.OperatorMessageList;
			if (operatorMessageList != null && operatorMessageList.Count > 0)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				foreach (ServerOperatorMessageEntry serverOperatorMessageEntry in operatorMessageList)
				{
					if (serverOperatorMessageEntry.m_presentState != null && serverOperatorMessageEntry.m_presentState.m_itemId == this.m_debugGiftItemId && serverOperatorMessageEntry.m_messageId > 0)
					{
						list2.Add(serverOperatorMessageEntry.m_messageId);
					}
				}
				if (list.Count > 0 || list2.Count > 0)
				{
					loggedInServerInterface.RequestServerUpdateMessage(list, list2, base.gameObject);
				}
				else
				{
					this.m_debugGiftCallback.SendMessage("DebugRequestGiftItem_Failed", SendMessageOptions.DontRequireReceiver);
					this.m_debugGiftCallback = null;
					this.m_debugGiftItemId = 0;
				}
			}
		}
	}

	// Token: 0x06004601 RID: 17921 RVA: 0x0016C930 File Offset: 0x0016AB30
	private void ServerUpdateMessage_Succeeded(MsgUpdateMesseageSucceed msg)
	{
		if (this.m_debugGiftCallback != null)
		{
			this.m_debugGiftCallback.SendMessage("DebugRequestGiftItem_Succeeded", SendMessageOptions.DontRequireReceiver);
		}
		this.m_debugGiftCallback = null;
		this.m_debugGiftItemId = 0;
	}

	// Token: 0x06004602 RID: 17922 RVA: 0x0016C970 File Offset: 0x0016AB70
	private void NetworkFailedCallback(ServerInterface.StatusCode statusCode)
	{
		if (this.m_debugGiftCallback != null)
		{
			this.m_debugGiftCallback.SendMessage("DebugRequestGiftItem_Failed", SendMessageOptions.DontRequireReceiver);
		}
		this.m_debugGiftCallback = null;
		this.m_debugGiftItemId = 0;
	}

	// Token: 0x06004603 RID: 17923 RVA: 0x0016C9B0 File Offset: 0x0016ABB0
	private void NetworkFailedMileCallback(ServerInterface.StatusCode statusCode)
	{
		if (this.m_debugGiftCallback != null)
		{
			this.m_debugGiftCallback.SendMessage("DebugUpdMileageData_Failed", SendMessageOptions.DontRequireReceiver);
		}
		this.m_debugGiftCallback = null;
		this.m_debugGiftItemId = 0;
	}

	// Token: 0x06004604 RID: 17924 RVA: 0x0016C9F0 File Offset: 0x0016ABF0
	public bool DebugStoryWindow()
	{
		int num = 5;
		GeneralWindow.CInfo.Event[] array = new GeneralWindow.CInfo.Event[num];
		int episode = 1;
		int pre_episode = 1;
		if (this.debugSceneLoader != null)
		{
			MileageMapText.Load(this.debugSceneLoader, episode, pre_episode);
		}
		for (int i = 0; i < num; i++)
		{
			string empty = string.Empty;
			MileageMapText.GetText(episode, empty);
			if (string.IsNullOrEmpty(empty))
			{
			}
			array[i] = new GeneralWindow.CInfo.Event
			{
				bgmCueName = string.Empty,
				seCueName = string.Empty
			};
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "DebugStoryWindow",
			buttonType = GeneralWindow.ButtonType.OkNextSkip,
			caption = "DebugStory",
			events = array,
			isNotPlaybackDefaultBgm = true,
			isSpecialEvent = true
		});
		return true;
	}

	// Token: 0x04003A1A RID: 14874
	private const float DEBUG_DISP_SIZE_W = 800f;

	// Token: 0x04003A1B RID: 14875
	private const float DEBUG_DISP_SIZE_H = 450f;

	// Token: 0x04003A1C RID: 14876
	private const float DEBUG_POP_TIME = 10f;

	// Token: 0x04003A1D RID: 14877
	private const float DEBUG_POP_MOVE_RATE = 0.05f;

	// Token: 0x04003A1E RID: 14878
	private const int DEBUG_MILEAGE_MAX = 50;

	// Token: 0x04003A1F RID: 14879
	public const float UPDATE_PERIOD_TIME = 2f;

	// Token: 0x04003A20 RID: 14880
	private const RankingUtil.RankingScoreType DEFAULT_RAIVAL_SCORE_TYPE = RankingUtil.RankingScoreType.HIGH_SCORE;

	// Token: 0x04003A21 RID: 14881
	private const RankingUtil.RankingScoreType DEFAULT_SP_SCORE_TYPE = RankingUtil.RankingScoreType.TOTAL_SCORE;

	// Token: 0x04003A22 RID: 14882
	[SerializeField]
	[Header("デバック用のオブジェクトです。不要な場合は要削除")]
	public bool m_debugActive = true;

	// Token: 0x04003A23 RID: 14883
	[SerializeField]
	public bool m_debugNetworkActive = true;

	// Token: 0x04003A24 RID: 14884
	[SerializeField]
	public bool m_debugTestBtn = true;

	// Token: 0x04003A25 RID: 14885
	[SerializeField]
	public DebugGameObject.DEBUG_CHECK_TYPE m_debugCheckType = DebugGameObject.DEBUG_CHECK_TYPE.NONE;

	// Token: 0x04003A26 RID: 14886
	[SerializeField]
	public DebugGameObject.MOUSE_R_CLICK m_mouseRightClick = DebugGameObject.MOUSE_R_CLICK.NONE;

	// Token: 0x04003A27 RID: 14887
	[SerializeField]
	public bool m_mouseWheelUseSpeed;

	// Token: 0x04003A28 RID: 14888
	[SerializeField]
	public ItemType m_mouseWheelUseItem = ItemType.UNKNOWN;

	// Token: 0x04003A29 RID: 14889
	[SerializeField]
	public float m_currentTimeScale = 1f;

	// Token: 0x04003A2A RID: 14890
	[SerializeField]
	private bool m_titleFirstLogin;

	// Token: 0x04003A2B RID: 14891
	[SerializeField]
	private DebugGameObject.LOADING_SUFFIXE m_titleLoadingSuffixe = DebugGameObject.LOADING_SUFFIXE.NONE;

	// Token: 0x04003A2C RID: 14892
	[SerializeField]
	private string m_suffixeBaseText = "title_load_index_{LANG}.html";

	// Token: 0x04003A2D RID: 14893
	[SerializeField]
	[Header("ランキング関連")]
	private bool m_rankingDebug;

	// Token: 0x04003A2E RID: 14894
	[SerializeField]
	private RankingUtil.RankingRankerType m_targetRankingRankerType = RankingUtil.RankingRankerType.RIVAL;

	// Token: 0x04003A2F RID: 14895
	[SerializeField]
	private RankingUtil.RankingScoreType m_rivalRankingScoreType;

	// Token: 0x04003A30 RID: 14896
	[SerializeField]
	private RankingUtil.RankingScoreType m_spRankingScoreType = RankingUtil.RankingScoreType.TOTAL_SCORE;

	// Token: 0x04003A31 RID: 14897
	[SerializeField]
	private bool m_rankingLog;

	// Token: 0x04003A32 RID: 14898
	[Header("現在の順位情報取得")]
	[SerializeField]
	private int m_currentScore = -1;

	// Token: 0x04003A33 RID: 14899
	[SerializeField]
	private int m_currentScoreEvent = -1;

	// Token: 0x04003A34 RID: 14900
	[Header("通信関連")]
	[SerializeField]
	private int m_msgMax;

	// Token: 0x04003A35 RID: 14901
	[Header("暗号化フラグ")]
	[SerializeField]
	private bool m_crypt = true;

	// Token: 0x04003A36 RID: 14902
	[SerializeField]
	[Header("更新頻度(数値表示確認用)")]
	private int m_updateCost;

	// Token: 0x04003A37 RID: 14903
	[SerializeField]
	private List<string> m_updateCostList;

	// Token: 0x04003A38 RID: 14904
	[Header("ルーレット関連")]
	[SerializeField]
	private RouletteCategory m_rouletteDummyCategory;

	// Token: 0x04003A39 RID: 14905
	[SerializeField]
	private bool m_rouletteTutorial;

	// Token: 0x04003A3A RID: 14906
	[SerializeField]
	private float m_rouletteConectTime = 1f;

	// Token: 0x04003A3B RID: 14907
	[SerializeField]
	private List<ServerItem.Id> m_rouletteDataPremium;

	// Token: 0x04003A3C RID: 14908
	[SerializeField]
	private List<ServerItem.Id> m_rouletteDataSpecial;

	// Token: 0x04003A3D RID: 14909
	[SerializeField]
	private List<ServerItem.Id> m_rouletteDataItem;

	// Token: 0x04003A3E RID: 14910
	[SerializeField]
	private List<ServerItem.Id> m_rouletteDataRaid;

	// Token: 0x04003A3F RID: 14911
	[SerializeField]
	private List<ServerItem.Id> m_rouletteDataDefault;

	// Token: 0x04003A40 RID: 14912
	[SerializeField]
	[Header("ダミー通信障害発生率(%)")]
	private int m_rouletteDummyError;

	// Token: 0x04003A41 RID: 14913
	[Header("デバック用キャンペーン設定")]
	[SerializeField]
	private List<Constants.Campaign.emType> m_debugCampaign;

	// Token: 0x04003A42 RID: 14914
	private List<string> m_debugDummy;

	// Token: 0x04003A43 RID: 14915
	private float m_mouseRightClickDelayTime;

	// Token: 0x04003A44 RID: 14916
	private Dictionary<string, int> m_updCost;

	// Token: 0x04003A45 RID: 14917
	private GameObject m_rouletteCallback;

	// Token: 0x04003A46 RID: 14918
	private MsgGetWheelOptionsGeneralSucceed m_rouletteGetMsg;

	// Token: 0x04003A47 RID: 14919
	private MsgCommitWheelSpinGeneralSucceed m_rouletteCommitMsg;

	// Token: 0x04003A48 RID: 14920
	private float m_debugRouletteTime;

	// Token: 0x04003A49 RID: 14921
	private float m_debugGetRouletteTime;

	// Token: 0x04003A4A RID: 14922
	private bool m_debugRouletteConectError = true;

	// Token: 0x04003A4B RID: 14923
	private List<string> m_keys;

	// Token: 0x04003A4C RID: 14924
	private Dictionary<string, int> m_currentUpdCost;

	// Token: 0x04003A4D RID: 14925
	private float m_time;

	// Token: 0x04003A4E RID: 14926
	private float m_wheelInputDelay;

	// Token: 0x04003A4F RID: 14927
	private Camera m_camera;

	// Token: 0x04003A50 RID: 14928
	private float m_cameraSizeRate = 1f;

	// Token: 0x04003A51 RID: 14929
	private bool m_debugPlay;

	// Token: 0x04003A52 RID: 14930
	private DebugGameObject.DEBUG_PLAY_TYPE m_debugPlayType = DebugGameObject.DEBUG_PLAY_TYPE.NONE;

	// Token: 0x04003A53 RID: 14931
	private bool m_debugScore;

	// Token: 0x04003A54 RID: 14932
	private float m_debugScoreDelay;

	// Token: 0x04003A55 RID: 14933
	private string m_debugScoreText = string.Empty;

	// Token: 0x04003A56 RID: 14934
	private bool m_debugMenu;

	// Token: 0x04003A57 RID: 14935
	private DebugGameObject.DEBUG_MENU_TYPE m_debugMenuType = DebugGameObject.DEBUG_MENU_TYPE.NONE;

	// Token: 0x04003A58 RID: 14936
	private DebugGameObject.DEBUG_MENU_RANKING_CATEGORY m_debugMenuRankingCateg;

	// Token: 0x04003A59 RID: 14937
	private RankingUtil.RankingRankerType m_debugMenuRankingType = RankingUtil.RankingRankerType.COUNT;

	// Token: 0x04003A5A RID: 14938
	private DebugGameObject.DEBUG_MENU_ITEM_CATEGORY m_debugMenuItemSelect;

	// Token: 0x04003A5B RID: 14939
	private Dictionary<DebugGameObject.DEBUG_MENU_ITEM_CATEGORY, List<int>> m_debugMenuItemList;

	// Token: 0x04003A5C RID: 14940
	private int m_debugMenuItemNum = 1;

	// Token: 0x04003A5D RID: 14941
	private int m_debugMenuItemPage;

	// Token: 0x04003A5E RID: 14942
	private List<DataTable.ChaoData> m_debugMenuOtomoList;

	// Token: 0x04003A5F RID: 14943
	private List<CharacterDataNameInfo.Info> m_debugMenuCharaList = new List<CharacterDataNameInfo.Info>();

	// Token: 0x04003A60 RID: 14944
	private int m_debugMenuMileageEpi = 2;

	// Token: 0x04003A61 RID: 14945
	private int m_debugMenuMileageCha = 1;

	// Token: 0x04003A62 RID: 14946
	private int m_debugMenuRankingCurrentRank;

	// Token: 0x04003A63 RID: 14947
	private int m_debugMenuRankingCurrentDummyRank;

	// Token: 0x04003A64 RID: 14948
	private int m_debugMenuRankingCurrentLegMax;

	// Token: 0x04003A65 RID: 14949
	private bool m_debugCheckFlag;

	// Token: 0x04003A66 RID: 14950
	private Dictionary<string, Dictionary<string, List<UIDrawCall>>> m_debugDrawCallList;

	// Token: 0x04003A67 RID: 14951
	private string m_debugDrawCallPanelCurrent = string.Empty;

	// Token: 0x04003A68 RID: 14952
	private string m_debugDrawCallMatCurrent = string.Empty;

	// Token: 0x04003A69 RID: 14953
	private List<UIAtlas> m_debugAtlasList;

	// Token: 0x04003A6A RID: 14954
	private List<UIAtlas> m_debugAtlasLangList;

	// Token: 0x04003A6B RID: 14955
	private string m_debugAtlasLangCode = "---";

	// Token: 0x04003A6C RID: 14956
	private Dictionary<long, string> m_debugPop;

	// Token: 0x04003A6D RID: 14957
	private Dictionary<long, Rect> m_debugPopRect;

	// Token: 0x04003A6E RID: 14958
	private Dictionary<long, float> m_debugPopTime;

	// Token: 0x04003A6F RID: 14959
	private long m_debugPopCount;

	// Token: 0x04003A70 RID: 14960
	private bool m_debugDeck;

	// Token: 0x04003A71 RID: 14961
	private int m_debugDeckCount;

	// Token: 0x04003A72 RID: 14962
	private int m_debugDeckCurrentIndex;

	// Token: 0x04003A73 RID: 14963
	private List<string> m_debugDeckList;

	// Token: 0x04003A74 RID: 14964
	private bool m_debugCharaData;

	// Token: 0x04003A75 RID: 14965
	private int m_debugCharaDataCount;

	// Token: 0x04003A76 RID: 14966
	private ServerPlayerState.CHARA_SORT m_debugCharaDataSort;

	// Token: 0x04003A77 RID: 14967
	private List<string> m_debugCharaDataList;

	// Token: 0x04003A78 RID: 14968
	private Dictionary<CharaType, ServerCharacterState> m_debugCharaList;

	// Token: 0x04003A79 RID: 14969
	private string m_debugCharaDataInfo = string.Empty;

	// Token: 0x04003A7A RID: 14970
	private ServerCharacterState m_debugCharaDataState;

	// Token: 0x04003A7B RID: 14971
	private Dictionary<ServerItem.Id, int> m_debugCharaDataBuyCost;

	// Token: 0x04003A7C RID: 14972
	private bool m_debugCharaDataBuy;

	// Token: 0x04003A7D RID: 14973
	private ResourceSceneLoader m_debugSceneLoader;

	// Token: 0x04003A7E RID: 14974
	private int m_debugGiftItemId;

	// Token: 0x04003A7F RID: 14975
	private GameObject m_debugGiftCallback;

	// Token: 0x02000A3A RID: 2618
	public enum GUI_RECT_ANCHOR
	{
		// Token: 0x04003A81 RID: 14977
		CENTER,
		// Token: 0x04003A82 RID: 14978
		CENTER_LEFT,
		// Token: 0x04003A83 RID: 14979
		CENTER_RIGHT,
		// Token: 0x04003A84 RID: 14980
		CENTER_TOP,
		// Token: 0x04003A85 RID: 14981
		CENTER_BOTTOM,
		// Token: 0x04003A86 RID: 14982
		LEFT_TOP,
		// Token: 0x04003A87 RID: 14983
		LEFT_BOTTOM,
		// Token: 0x04003A88 RID: 14984
		RIGHT_TOP,
		// Token: 0x04003A89 RID: 14985
		RIGHT_BOTTOM
	}

	// Token: 0x02000A3B RID: 2619
	public enum LOADING_SUFFIXE
	{
		// Token: 0x04003A8B RID: 14987
		DEBUG_JA,
		// Token: 0x04003A8C RID: 14988
		DEBUG_DE,
		// Token: 0x04003A8D RID: 14989
		DEBUG_EN,
		// Token: 0x04003A8E RID: 14990
		DEBUG_ES,
		// Token: 0x04003A8F RID: 14991
		DEBUG_FR,
		// Token: 0x04003A90 RID: 14992
		DEBUG_IT,
		// Token: 0x04003A91 RID: 14993
		DEBUG_KO,
		// Token: 0x04003A92 RID: 14994
		DEBUG_PT,
		// Token: 0x04003A93 RID: 14995
		DEBUG_RU,
		// Token: 0x04003A94 RID: 14996
		DEBUG_ZH,
		// Token: 0x04003A95 RID: 14997
		DEBUG_ZHJ,
		// Token: 0x04003A96 RID: 14998
		NONE
	}

	// Token: 0x02000A3C RID: 2620
	public enum MOUSE_R_CLICK
	{
		// Token: 0x04003A98 RID: 15000
		PAUSED,
		// Token: 0x04003A99 RID: 15001
		ATLAS,
		// Token: 0x04003A9A RID: 15002
		HI_SPEED,
		// Token: 0x04003A9B RID: 15003
		LOW_SPEED,
		// Token: 0x04003A9C RID: 15004
		NONE
	}

	// Token: 0x02000A3D RID: 2621
	public enum DEBUG_CHECK_TYPE
	{
		// Token: 0x04003A9E RID: 15006
		DRAW_CALL,
		// Token: 0x04003A9F RID: 15007
		LOAD_ATLAS,
		// Token: 0x04003AA0 RID: 15008
		NONE
	}

	// Token: 0x02000A3E RID: 2622
	private enum DEBUG_PLAY_TYPE
	{
		// Token: 0x04003AA2 RID: 15010
		ITEM,
		// Token: 0x04003AA3 RID: 15011
		COLOR,
		// Token: 0x04003AA4 RID: 15012
		BOSS_DESTORY,
		// Token: 0x04003AA5 RID: 15013
		NUM,
		// Token: 0x04003AA6 RID: 15014
		NONE
	}

	// Token: 0x02000A3F RID: 2623
	private enum DEBUG_MENU_TYPE
	{
		// Token: 0x04003AA8 RID: 15016
		ITEM,
		// Token: 0x04003AA9 RID: 15017
		MILEAGE,
		// Token: 0x04003AAA RID: 15018
		RANKING,
		// Token: 0x04003AAB RID: 15019
		CHAO_TEX_RELEASE,
		// Token: 0x04003AAC RID: 15020
		DAILY_BATTLE,
		// Token: 0x04003AAD RID: 15021
		DEBUG_GUI_OFF,
		// Token: 0x04003AAE RID: 15022
		NUM,
		// Token: 0x04003AAF RID: 15023
		NONE
	}

	// Token: 0x02000A40 RID: 2624
	private enum DEBUG_MENU_RANKING_CATEGORY
	{
		// Token: 0x04003AB1 RID: 15025
		CACHE,
		// Token: 0x04003AB2 RID: 15026
		CHANGE_TEST,
		// Token: 0x04003AB3 RID: 15027
		NUM
	}

	// Token: 0x02000A41 RID: 2625
	private enum DEBUG_MENU_ITEM_CATEGORY
	{
		// Token: 0x04003AB5 RID: 15029
		ITEM,
		// Token: 0x04003AB6 RID: 15030
		OTOMO,
		// Token: 0x04003AB7 RID: 15031
		CHARACTER,
		// Token: 0x04003AB8 RID: 15032
		NUM
	}

	// Token: 0x02000AB4 RID: 2740
	// (Invoke) Token: 0x0600490A RID: 18698
	private delegate void NetworkRequestSuccessCallback();

	// Token: 0x02000AB5 RID: 2741
	// (Invoke) Token: 0x0600490E RID: 18702
	private delegate void NetworkRequestFailedCallback(ServerInterface.StatusCode statusCode);
}
