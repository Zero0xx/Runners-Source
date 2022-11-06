using System;
using System.Collections.Generic;
using System.Diagnostics;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class daily_challenge : MonoBehaviour
{
	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000B5698 File Offset: 0x000B3898
	public static bool isUpdateEffect
	{
		get
		{
			return daily_challenge.s_isUpdateEffect;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000B56A0 File Offset: 0x000B38A0
	public bool IsEndSetup
	{
		get
		{
			return this.m_setUp;
		}
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x000B56A8 File Offset: 0x000B38A8
	public static daily_challenge.DailyMissionInfo GetInfoFromSaveData(long todayMissionClearQuotaBefore)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return null;
		}
		int id = instance.PlayerData.DailyMission.id;
		MissionData missionData = MissionTable.GetMissionData(id);
		if (missionData == null)
		{
			return null;
		}
		daily_challenge.DailyMissionInfo dailyMissionInfo = new daily_challenge.DailyMissionInfo
		{
			DayIndex = instance.PlayerData.DailyMission.date,
			DayMax = instance.PlayerData.DailyMission.max,
			TodayMissionId = instance.PlayerData.DailyMission.id,
			TodayMissionClearQuota = instance.PlayerData.DailyMission.progress,
			InceniveIdTable = new int[instance.PlayerData.DailyMission.reward_max],
			InceniveNumTable = new int[instance.PlayerData.DailyMission.reward_max],
			ClearMissionCount = instance.PlayerData.DailyMission.clear_count,
			IsClearTodayMission = instance.PlayerData.DailyMission.missions_complete,
			TodayMissionQuota = (long)missionData.quota,
			TodayMissionText = missionData.text
		};
		int reward_max = instance.PlayerData.DailyMission.reward_max;
		for (int i = 0; i < reward_max; i++)
		{
			if (i < instance.PlayerData.DailyMission.reward_id.Length)
			{
				dailyMissionInfo.InceniveIdTable[i] = instance.PlayerData.DailyMission.reward_id[i];
			}
			if (i < instance.PlayerData.DailyMission.reward_count.Length)
			{
				dailyMissionInfo.InceniveNumTable[i] = instance.PlayerData.DailyMission.reward_count[i];
			}
		}
		dailyMissionInfo.TodayMissionClearQuota = ((dailyMissionInfo.TodayMissionClearQuota >= dailyMissionInfo.TodayMissionQuota) ? dailyMissionInfo.TodayMissionQuota : dailyMissionInfo.TodayMissionClearQuota);
		dailyMissionInfo.TodayMissionClearQuotaBefore = ((todayMissionClearQuotaBefore == -1L) ? dailyMissionInfo.TodayMissionClearQuota : todayMissionClearQuotaBefore);
		if (todayMissionClearQuotaBefore != -1L && dailyMissionInfo.IsMissionClearNotice)
		{
			dailyMissionInfo.ClearMissionCount--;
		}
		return dailyMissionInfo;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x000B58C8 File Offset: 0x000B3AC8
	public void Initialize(long todayMissionClearQuotaBefore)
	{
		this.m_info = daily_challenge.GetInfoFromSaveData(todayMissionClearQuotaBefore);
		this.m_updateBarDelay = 1f;
		this.m_barSePlayId = SoundManager.PlayId.NONE;
		GameObject gameObject = null;
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "DailyWindowUI");
		}
		if (gameObject != null)
		{
			this.m_windowAnime = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(gameObject, "blinder");
			if (this.m_windowAnime != null)
			{
				this.m_windowAnime.enabled = false;
			}
			this.m_windowBtnAnime = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(gameObject, "Btn_next");
			this.m_windowBtnTween = GameObjectUtil.FindChildGameObjectComponent<UIPlayTween>(gameObject, "Btn_next");
			if (this.m_windowBtnAnime != null)
			{
				this.m_windowBtnAnime.enabled = false;
			}
			if (this.m_windowBtnTween != null)
			{
				this.m_windowBtnTween.enabled = false;
			}
		}
		if (this.m_info != null)
		{
			this.m_clearBarValue = (float)this.m_info.TodayMissionClearQuotaBefore / (float)this.m_info.TodayMissionQuota;
			daily_challenge.DebugLog(string.Concat(new object[]
			{
				"UpdateBar ",
				this.m_info.TodayMissionClearQuotaBefore,
				"→",
				this.m_info.TodayMissionClearQuota,
				"/",
				this.m_info.TodayMissionQuota
			}));
			this.InitItem();
			this.m_isInitialized = true;
			daily_challenge.s_isUpdateEffect = true;
			this.UpdateView();
		}
		if (this.m_inspectorUi.m_clearGameObject != null)
		{
			this.m_inspectorUi.m_clearGameObject.SetActive(false);
		}
		this.m_setUp = true;
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x000B5A7C File Offset: 0x000B3C7C
	private void Update()
	{
		if (!this.m_isInitialized)
		{
			return;
		}
		this.UpdateBar(0.004f);
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x000B5A98 File Offset: 0x000B3C98
	private void UpdateBar(float speed)
	{
		this.m_updateBarDelay = ((this.m_updateBarDelay <= Time.deltaTime) ? 0f : (this.m_updateBarDelay - Time.deltaTime));
		if (this.m_updateBarDelay > 0f)
		{
			return;
		}
		float num = (float)this.m_info.TodayMissionClearQuota / (float)this.m_info.TodayMissionQuota;
		if (this.m_clearBarValue < num)
		{
			if (this.m_barSePlayId == SoundManager.PlayId.NONE)
			{
				this.m_barSePlayId = SoundManager.SePlay("sys_gauge", "SE");
			}
			this.m_clearBarValue = Mathf.Min(this.m_clearBarValue + speed, num);
			if (this.m_info.IsMissionClearNotice && this.m_clearBarValue == 1f)
			{
				this.m_info.ClearMissionCount++;
				this.StopBarSe();
				if (this.m_inspectorUi.m_clearGameObject != null)
				{
					this.m_inspectorUi.m_clearGameObject.SetActive(true);
				}
				if (this.m_inspectorUi.m_clearAnimation != null)
				{
					ActiveAnimation.Play(this.m_inspectorUi.m_clearAnimation, Direction.Forward);
				}
				if (this.m_days.Count > this.m_info.ClearMissionCount - 1)
				{
					DayObject dayObject = this.m_days[this.m_info.ClearMissionCount - 1];
					dayObject.PlayGetAnimation();
				}
				SoundManager.SePlay("sys_dailychallenge", "SE");
			}
			if (this.m_clearBarValue == num)
			{
				daily_challenge.s_isUpdateEffect = false;
				this.m_info.TodayMissionClearQuotaBefore = this.m_info.TodayMissionClearQuota;
				this.StopBarSe();
			}
			else
			{
				daily_challenge.s_isUpdateEffect = true;
			}
			this.UpdateView();
		}
		else
		{
			daily_challenge.s_isUpdateEffect = false;
		}
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x000B5C5C File Offset: 0x000B3E5C
	private void UpdateView()
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_days");
		if (uilabel != null)
		{
			uilabel.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "day").text, new Dictionary<string, string>
			{
				{
					"{DAY}",
					(this.m_info.DayMax - this.m_info.DayIndex).ToString()
				}
			});
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_daily_challenge");
		if (uilabel2 != null)
		{
			uilabel2.text = TextUtility.Replaces(this.m_info.TodayMissionText, new Dictionary<string, string>
			{
				{
					"{QUOTA}",
					this.m_info.TodayMissionQuota.ToString()
				}
			});
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_attainment_fg");
		if (gameObject != null)
		{
			gameObject.SetActive(this.m_clearBarValue > 0f);
		}
		UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_attainment");
		if (uislider != null)
		{
			uislider.value = this.m_clearBarValue;
		}
		bool flag = this.m_info.IsClearTodayMission && this.m_clearBarValue == 1f;
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_percent_clear");
		if (uilabel3 != null)
		{
			if (flag)
			{
				uilabel3.text = string.Empty;
			}
			else
			{
				float num = this.m_clearBarValue * 100f;
				if (num > 100f)
				{
					num = 100f;
				}
				uilabel3.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "clear_percent").text, new Dictionary<string, string>
				{
					{
						"{PARAM}",
						((int)num).ToString()
					}
				});
			}
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "img_clear");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(flag);
		}
		for (int i = 0; i < this.m_info.InceniveIdTable.Length; i++)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_day" + (i + 1));
			if (!(gameObject3 == null))
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject3, "img_check");
				if (gameObject4 != null)
				{
					gameObject4.SetActive(i < this.m_info.ClearMissionCount);
				}
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_daily_item");
				if (uisprite != null)
				{
					uisprite.spriteName = ((i >= this.m_info.InceniveIdTable.Length - 1) ? "ui_cmn_icon_rsring_L" : ("ui_cmn_icon_item_" + this.m_info.InceniveIdTable[i]));
				}
				UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3.gameObject, "Lbl_count");
				if (uilabel4 != null)
				{
					uilabel4.text = this.m_info.InceniveNumTable[i].ToString();
				}
			}
		}
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x000B5F98 File Offset: 0x000B4198
	private void InitItem()
	{
		if (this.m_inspectorUi != null && this.m_inspectorUi.m_dayObjectBase != null && this.m_inspectorUi.m_dayObjectOrg != null && this.m_inspectorUi.m_dayBigObjectOrg != null)
		{
			GameObject dayObjectBase = this.m_inspectorUi.m_dayObjectBase;
			float num = 0f;
			float num2 = 0f;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_inspectorUi.m_dayObjectOrg, "img_frame");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_inspectorUi.m_dayBigObjectOrg, "img_frame");
			if (uisprite != null)
			{
				num = (float)uisprite.width;
			}
			if (uisprite2 != null)
			{
				num2 = (float)uisprite2.width;
			}
			float num3 = (float)(this.m_info.InceniveIdTable.Length - 1) * -0.5f * num - (num2 - num) * 0.5f;
			if (this.m_days != null && this.m_days.Count > 0)
			{
				for (int i = 0; i < this.m_days.Count; i++)
				{
					UnityEngine.Object.Destroy(this.m_days[i].m_clearGameObject);
				}
			}
			this.m_days = new List<DayObject>();
			for (int j = 0; j < this.m_info.InceniveIdTable.Length; j++)
			{
				Color color = new Color(1f, 1f, 1f, 1f);
				GameObject gameObject;
				if (j < this.m_info.InceniveIdTable.Length - 1)
				{
					gameObject = (UnityEngine.Object.Instantiate(this.m_inspectorUi.m_dayObjectOrg) as GameObject);
					if (this.m_inspectorUi.m_dayObjectColors != null && this.m_inspectorUi.m_dayObjectColors.Count > j)
					{
						color = this.m_inspectorUi.m_dayObjectColors[j];
					}
				}
				else
				{
					gameObject = (UnityEngine.Object.Instantiate(this.m_inspectorUi.m_dayBigObjectOrg) as GameObject);
				}
				if (gameObject != null)
				{
					gameObject.transform.parent = dayObjectBase.transform;
					float x = 0f;
					if (this.m_info.InceniveIdTable.Length > 1)
					{
						x = num3 + (float)j * num;
						if (j >= this.m_info.InceniveIdTable.Length - 1)
						{
							x = num3 + (float)j * num + (num2 - num) * 0.45f;
						}
					}
					gameObject.transform.localPosition = new Vector3(x, 0f, 0f);
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					DayObject dayObject = new DayObject(gameObject, color, j + 1);
					TweenPosition tweenPosition = GameObjectUtil.FindChildGameObjectComponent<TweenPosition>(dayObject.m_clearGameObject, "img_daily_item");
					if (tweenPosition != null)
					{
						tweenPosition.ignoreTimeScale = false;
						tweenPosition.Reset();
					}
					TweenScale tweenScale = GameObjectUtil.FindChildGameObjectComponent<TweenScale>(dayObject.m_clearGameObject, "img_daily_item");
					if (tweenScale != null)
					{
						tweenScale.ignoreTimeScale = false;
						tweenScale.Reset();
					}
					dayObject.SetItem(this.m_info.InceniveIdTable[j]);
					dayObject.count = this.m_info.InceniveNumTable[j];
					int num4 = this.m_info.InceniveIdTable.Length;
					if (num4 > 0)
					{
						if (this.m_info.ClearMissionCount < num4)
						{
							dayObject.SetAlready(j < this.m_info.ClearMissionCount);
						}
						else if (this.m_info.InceniveIdTable.Length - 1 > j)
						{
							dayObject.SetAlready(true);
						}
						else
						{
							dayObject.SetAlready(this.m_info.IsClearTodayMission);
						}
					}
					else
					{
						dayObject.SetAlready(false);
					}
					this.m_days.Add(dayObject);
				}
			}
		}
		else if (this.m_inspectorUi != null && this.m_inspectorUi.m_dayObjectBase != null)
		{
			List<GameObject> list = new List<GameObject>();
			for (int k = 1; k <= 7; k++)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "day" + k);
				if (gameObject2 != null)
				{
					list.Add(gameObject2);
				}
			}
			if (list.Count > 0)
			{
				int num5 = 0;
				foreach (GameObject parent in list)
				{
					UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_bg");
					UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_check");
					UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_daily_item");
					UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_chara");
					UISprite uisprite7 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_chao");
					UISprite uisprite8 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_day_num");
					UISprite uisprite9 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_hidden");
					if (uisprite3 != null)
					{
						uisprite3.enabled = (num5 == this.m_info.InceniveIdTable.Length - 1);
					}
					if (num5 < this.m_info.InceniveIdTable.Length)
					{
						if (uisprite4 != null)
						{
							uisprite4.enabled = (num5 < this.m_info.ClearMissionCount);
						}
						int num6 = this.m_info.InceniveIdTable[num5];
						int num7 = Mathf.FloorToInt((float)num6 / 100000f);
						if (uisprite6 != null)
						{
							uisprite6.alpha = 0f;
							uisprite6.enabled = true;
						}
						if (uisprite7 != null)
						{
							uisprite7.alpha = 0f;
							uisprite7.enabled = true;
						}
						if (uisprite5 != null)
						{
							uisprite5.alpha = 0f;
							uisprite5.enabled = true;
						}
						int num8 = num7;
						if (num8 != 3)
						{
							if (num8 != 4)
							{
								if (uisprite5 != null)
								{
									uisprite5.alpha = 1f;
									if (num6 >= 0)
									{
										uisprite5.spriteName = "ui_cmn_icon_item_" + num6;
									}
									else
									{
										uisprite5.spriteName = "ui_cmn_icon_item_9";
									}
								}
							}
							else if (uisprite7 != null)
							{
								uisprite7.alpha = 1f;
								uisprite7.spriteName = string.Format("ui_tex_chao_{0:D4}", num6 - 400000);
							}
						}
						else if (uisprite6 != null)
						{
							uisprite6.alpha = 1f;
							UISprite uisprite10 = uisprite6;
							string str = "ui_tex_player_";
							ServerItem serverItem = new ServerItem((ServerItem.Id)num6);
							uisprite10.spriteName = str + CharaTypeUtil.GetCharaSpriteNameSuffix(serverItem.charaType);
						}
						if (uisprite8 != null)
						{
							uisprite8.enabled = true;
							if (num5 == this.m_info.InceniveIdTable.Length - 1)
							{
								uisprite8.color = new Color(1f, 0.7529412f, 0f, 1f);
							}
							else
							{
								uisprite8.color = new Color(0.5019608f, 1f, 1f, 1f);
							}
						}
						if (uisprite9 != null)
						{
							uisprite9.enabled = (num5 < this.m_info.ClearMissionCount);
						}
					}
					else
					{
						if (uisprite4 != null)
						{
							uisprite4.enabled = false;
						}
						if (uisprite5 != null)
						{
							uisprite5.enabled = false;
						}
						if (uisprite8 != null)
						{
							uisprite8.enabled = false;
						}
						if (uisprite9 != null)
						{
							uisprite9.enabled = true;
						}
					}
					num5++;
				}
			}
		}
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x000B6778 File Offset: 0x000B4978
	private void OnStartDailyMissionInMileageMap(long todayMissionClearQuotaBefore)
	{
		this.m_setUp = false;
		this.Initialize(todayMissionClearQuotaBefore);
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x000B6788 File Offset: 0x000B4988
	private void OnStartDailyMissionInScreen()
	{
		this.Initialize(-1L);
		HudMenuUtility.SendStartInformaionDlsplay();
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x000B6798 File Offset: 0x000B4998
	private void OnClickNextButton(GameObject dailyWindowGameObject)
	{
		float num = (float)this.m_info.TodayMissionClearQuota / (float)this.m_info.TodayMissionQuota;
		if (this.m_clearBarValue >= num)
		{
			if (this.m_windowBtnAnime != null)
			{
				this.m_windowBtnAnime.enabled = true;
			}
			if (this.m_windowBtnTween != null)
			{
				this.m_windowBtnTween.enabled = true;
			}
			if (this.m_windowAnime != null)
			{
				this.m_windowAnime.enabled = true;
				this.m_windowAnime.Play(false);
			}
		}
		else
		{
			this.UpdateBar(1f);
		}
		this.StopBarSe();
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x000B6844 File Offset: 0x000B4A44
	private void StopBarSe()
	{
		if (this.m_barSePlayId != SoundManager.PlayId.NONE)
		{
			SoundManager.SeStop(this.m_barSePlayId);
		}
		SoundManager.SeStop("sys_gauge", "SE");
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x000B686C File Offset: 0x000B4A6C
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x000B6880 File Offset: 0x000B4A80
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04001BF9 RID: 7161
	private const float BAR_SPEED = 0.004f;

	// Token: 0x04001BFA RID: 7162
	private static bool s_isUpdateEffect;

	// Token: 0x04001BFB RID: 7163
	[SerializeField]
	private daily_challenge.InspectorUi m_inspectorUi;

	// Token: 0x04001BFC RID: 7164
	private bool m_isInitialized;

	// Token: 0x04001BFD RID: 7165
	private bool m_setUp;

	// Token: 0x04001BFE RID: 7166
	private float m_updateBarDelay;

	// Token: 0x04001BFF RID: 7167
	private float m_clearBarValue;

	// Token: 0x04001C00 RID: 7168
	private SoundManager.PlayId m_barSePlayId;

	// Token: 0x04001C01 RID: 7169
	private List<DayObject> m_days;

	// Token: 0x04001C02 RID: 7170
	private UIPlayAnimation m_windowAnime;

	// Token: 0x04001C03 RID: 7171
	private UIPlayAnimation m_windowBtnAnime;

	// Token: 0x04001C04 RID: 7172
	private UIPlayTween m_windowBtnTween;

	// Token: 0x04001C05 RID: 7173
	private daily_challenge.DailyMissionInfo m_info;

	// Token: 0x02000404 RID: 1028
	[Serializable]
	private class InspectorUi
	{
		// Token: 0x04001C06 RID: 7174
		[SerializeField]
		public GameObject m_clearGameObject;

		// Token: 0x04001C07 RID: 7175
		[SerializeField]
		public Animation m_clearAnimation;

		// Token: 0x04001C08 RID: 7176
		[SerializeField]
		public GameObject m_dayObjectOrg;

		// Token: 0x04001C09 RID: 7177
		[SerializeField]
		public GameObject m_dayBigObjectOrg;

		// Token: 0x04001C0A RID: 7178
		[SerializeField]
		public GameObject m_dayObjectBase;

		// Token: 0x04001C0B RID: 7179
		[SerializeField]
		public List<Color> m_dayObjectColors;
	}

	// Token: 0x02000405 RID: 1029
	public class DailyMissionInfo
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000B68A4 File Offset: 0x000B4AA4
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x000B68AC File Offset: 0x000B4AAC
		public int DayIndex { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000B68B8 File Offset: 0x000B4AB8
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		public int TodayMissionId { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x000B68CC File Offset: 0x000B4ACC
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x000B68D4 File Offset: 0x000B4AD4
		public long TodayMissionClearQuota { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x000B68E0 File Offset: 0x000B4AE0
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x000B68E8 File Offset: 0x000B4AE8
		public int[] InceniveIdTable { get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x000B68F4 File Offset: 0x000B4AF4
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x000B68FC File Offset: 0x000B4AFC
		public int[] InceniveNumTable { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000B6908 File Offset: 0x000B4B08
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x000B6910 File Offset: 0x000B4B10
		public int ClearMissionCount { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000B691C File Offset: 0x000B4B1C
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000B6924 File Offset: 0x000B4B24
		public bool IsClearTodayMission { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000B6930 File Offset: 0x000B4B30
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x000B6938 File Offset: 0x000B4B38
		public long TodayMissionQuota { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000B6944 File Offset: 0x000B4B44
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x000B694C File Offset: 0x000B4B4C
		public string TodayMissionText { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000B6958 File Offset: 0x000B4B58
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x000B6960 File Offset: 0x000B4B60
		public int DayMax { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000B696C File Offset: 0x000B4B6C
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x000B6974 File Offset: 0x000B4B74
		public long TodayMissionClearQuotaBefore { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000B6980 File Offset: 0x000B4B80
		public bool IsMissionClearNotice
		{
			get
			{
				return this.TodayMissionClearQuotaBefore < this.TodayMissionClearQuota && this.TodayMissionClearQuota == this.TodayMissionQuota;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000B69B0 File Offset: 0x000B4BB0
		public bool IsMissionEvent
		{
			get
			{
				return !this.IsClearTodayMission || this.IsMissionClearNotice;
			}
		}
	}
}
