using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class ui_mm_mileage_page : MonoBehaviour
{
	// Token: 0x060021E5 RID: 8677 RVA: 0x000CBD6C File Offset: 0x000C9F6C
	private void AddEvent(ui_mm_mileage_page.BaseEvent baseEvent, int waitType = -1)
	{
		this.StopRunSe();
		if (this.m_isSkipMileage)
		{
			baseEvent.SkipMileageProcess();
		}
		else
		{
			if (waitType < 0)
			{
				this.m_events.Enqueue(new ui_mm_mileage_page.WaitEvent(base.gameObject, this.m_eventWait));
			}
			this.m_events.Enqueue(baseEvent);
			if (waitType > 0)
			{
				this.m_events.Enqueue(new ui_mm_mileage_page.WaitEvent(base.gameObject, this.m_eventWait));
			}
		}
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x000CBDE8 File Offset: 0x000C9FE8
	private void AddEventPostWait(ui_mm_mileage_page.BaseEvent baseEvent)
	{
		this.AddEvent(baseEvent, 1);
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x000CBDF4 File Offset: 0x000C9FF4
	private void AddEventNoWait(ui_mm_mileage_page.BaseEvent baseEvent)
	{
		this.AddEvent(baseEvent, 0);
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x000CBE00 File Offset: 0x000CA000
	private void AddWaypointEvents()
	{
		this.AddEventNoWait(new ui_mm_mileage_page.BalloonEffectEvent(base.gameObject, this.m_mapInfo.waypointIndex));
		MileageMapData mileageMapData = MileageMapDataManager.Instance.GetMileageMapData();
		if (mileageMapData != null)
		{
			if (this.m_mapInfo.waypointIndex < 5)
			{
				EventData event_data = mileageMapData.event_data;
				PointEventData pointEventData = event_data.point[this.m_mapInfo.waypointIndex];
				ui_mm_mileage_page.WayPointEventType wayPointEventType = (ui_mm_mileage_page.WayPointEventType)((this.m_mapInfo.waypointIndex != 0) ? pointEventData.event_type : 2);
				ui_mm_mileage_page.WayPointEventType wayPointEventType2 = wayPointEventType;
				if (wayPointEventType2 != ui_mm_mileage_page.WayPointEventType.SIMPLE)
				{
					if (wayPointEventType2 == ui_mm_mileage_page.WayPointEventType.GORGEOUS)
					{
						if (pointEventData.window_id > -1)
						{
							this.AddEventPostWait(new ui_mm_mileage_page.GorgeousEvent(base.gameObject, pointEventData.window_id, false));
						}
						if (pointEventData.reward.reward_id > -1 || pointEventData.window_id == -1)
						{
							this.AddEventPostWait(new ui_mm_mileage_page.SimpleEvent(base.gameObject, pointEventData.reward.serverId, pointEventData.reward.reward_count, MileageMapUtility.GetText("gw_item_caption", null), false));
						}
					}
				}
				else
				{
					this.AddEventPostWait(new ui_mm_mileage_page.SimpleEvent(base.gameObject, pointEventData.reward.serverId, pointEventData.reward.reward_count, MileageMapUtility.GetText("gw_item_caption", null), false));
				}
			}
			else
			{
				EventData event_data2 = mileageMapData.event_data;
				if (!event_data2.IsBossEvent())
				{
					if (event_data2.point[this.m_mapInfo.waypointIndex].balloon_on_arrival_face_id != -1)
					{
						this.AddEventPostWait(new ui_mm_mileage_page.BalloonEvent(base.gameObject, this.m_mapInfo.waypointIndex - 1, event_data2.point[this.m_mapInfo.waypointIndex].balloon_on_arrival_face_id, event_data2.point[this.m_mapInfo.waypointIndex].balloon_face_id));
					}
				}
				else if (event_data2.point[this.m_mapInfo.waypointIndex].boss.balloon_on_arrival_face_id != -1)
				{
					this.AddEventPostWait(new ui_mm_mileage_page.BalloonEvent(base.gameObject, this.m_mapInfo.waypointIndex - 1, event_data2.point[this.m_mapInfo.waypointIndex].boss.balloon_on_arrival_face_id, event_data2.point[this.m_mapInfo.waypointIndex].boss.balloon_init_face_id));
				}
				if (this.m_mapInfo.tutorialPhase != ui_mm_mileage_page.MapInfo.TutorialPhase.FIRST_EPISODE || !event_data2.IsBossEvent() || this.m_mapInfo.isBossDestroyed)
				{
					int num = event_data2.IsBossEvent() ? (this.m_mapInfo.isBossDestroyed ? event_data2.GetBossEvent().after_window_id : event_data2.GetBossEvent().before_window_id) : event_data2.point[this.m_mapInfo.waypointIndex].window_id;
					if (num > -1)
					{
						this.AddEventNoWait(new ui_mm_mileage_page.GorgeousEvent(base.gameObject, num, this.IsExistsMapClearEvents()));
					}
				}
				this.AddMapClearEvents();
			}
		}
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x000CC108 File Offset: 0x000CA308
	private bool IsExistsMapClearEvents()
	{
		bool flag = MileageMapDataManager.Instance.GetMileageMapData().event_data.IsBossEvent() && !this.m_mapInfo.isBossDestroyed;
		return !flag;
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x000CC144 File Offset: 0x000CA344
	private void AddMapClearEvents()
	{
		if (!this.IsExistsMapClearEvents())
		{
			return;
		}
		this.AddEvent(new ui_mm_mileage_page.BgmEvent(base.gameObject, "jingle_sys_mapclear"), -1);
		bool flag = true;
		foreach (RewardData rewardData in MileageMapDataManager.Instance.GetMileageMapData().map_data.reward)
		{
			if (rewardData.reward_id != -1 && rewardData.reward_count > 0)
			{
				ui_mm_mileage_page.BaseEvent baseEvent = new ui_mm_mileage_page.SimpleEvent(base.gameObject, rewardData.serverId, rewardData.reward_count, MileageMapUtility.GetText("gw_map_bonus_caption", null), true);
				if (flag)
				{
					this.AddEventNoWait(baseEvent);
					flag = false;
				}
				else
				{
					this.AddEvent(baseEvent, -1);
				}
			}
		}
		if (MileageMapDataManager.Instance.GetMileageMapData().scenario.last_chapter_flag != 0)
		{
			foreach (RewardData rewardData2 in MileageMapDataManager.Instance.GetMileageMapData().scenario.reward)
			{
				if (rewardData2.reward_id != -1 && rewardData2.reward_count > 0)
				{
					this.AddEvent(new ui_mm_mileage_page.SimpleEvent(base.gameObject, rewardData2.serverId, rewardData2.reward_count, MileageMapUtility.GetText("gw_scenario_bonus_caption", null), true), -1);
				}
			}
		}
		this.AddEvent(new ui_mm_mileage_page.RankUpEvent(base.gameObject), -1);
		this.AddEventNoWait(new ui_mm_mileage_page.BgmEvent(base.gameObject, null));
		this.AddEvent(new ui_mm_mileage_page.MapEvent(base.gameObject), -1);
		int window_id = MileageMapDataManager.Instance.GetMileageMapData().event_data.point[0].window_id;
		this.AddEvent(new ui_mm_mileage_page.GorgeousEvent(base.gameObject, window_id, false, true), -1);
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x000CC314 File Offset: 0x000CA514
	private double Minimum(double a, double b)
	{
		if (a < b)
		{
			return a;
		}
		return b;
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x060021EC RID: 8684 RVA: 0x000CC320 File Offset: 0x000CA520
	public static ui_mm_mileage_page Instance
	{
		get
		{
			return ui_mm_mileage_page.instance;
		}
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x000CC328 File Offset: 0x000CA528
	private void Awake()
	{
		if (ui_mm_mileage_page.instance == null)
		{
			ui_mm_mileage_page.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x000CC35C File Offset: 0x000CA55C
	private void Start()
	{
		if (this.m_disabled)
		{
			base.enabled = false;
			return;
		}
		this.m_fsm_behavior = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm_behavior != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
			this.m_fsm_behavior.SetUp(description);
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Pgb_score");
		if (gameObject != null)
		{
			this.m_distanceSlider = gameObject.GetComponent<UISlider>();
		}
		this.m_playerSpriteAnimations = this.m_playerSpriteAnimations[0].gameObject.GetComponents<UISpriteAnimation>();
		HudMenuUtility.SetTagHudMileageMap(base.gameObject);
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x000CC424 File Offset: 0x000CA624
	private void OnDestroy()
	{
		if (ui_mm_mileage_page.instance == this)
		{
			if (ui_mm_mileage_page.instance.m_fsm_behavior != null)
			{
				ui_mm_mileage_page.instance.m_fsm_behavior.ShutDown();
				ui_mm_mileage_page.instance.m_fsm_behavior = null;
			}
			ui_mm_mileage_page.instance = null;
		}
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x000CC478 File Offset: 0x000CA678
	private void Update()
	{
		if (!this.m_isInit)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			this.m_limitDatas[i].Update();
		}
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000CC4B0 File Offset: 0x000CA6B0
	private void ChangeState(TinyFsmState nextState)
	{
		this.m_fsm_behavior.ChangeState(nextState);
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x000CC4C0 File Offset: 0x000CA6C0
	private TinyFsmState StateIdle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060021F3 RID: 8691 RVA: 0x000CC518 File Offset: 0x000CA718
	private TinyFsmState StateHighScoreEvent(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.IsRankingUp())
			{
				this.AddEvent(new ui_mm_mileage_page.RankingUPEvent(base.gameObject), -1);
			}
			this.AddEvent(new ui_mm_mileage_page.HighscoreEvent(base.gameObject, this.m_mapInfo.highscore), -1);
			return TinyFsmState.End();
		default:
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.m_isSkipMileage = true;
			this.m_events.Clear();
			return TinyFsmState.End();
		case 4:
			if (this.m_event != null && this.m_event.Update())
			{
				this.m_event = null;
			}
			if (this.m_event == null)
			{
				if (this.m_events.Count > 0)
				{
					this.m_event = this.m_events.Dequeue();
					this.m_event.Start();
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEvent)));
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x000CC640 File Offset: 0x000CA840
	private TinyFsmState StateEvent(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_mapInfo.isBossDestroyed)
			{
				this.SetBossClearEvent();
				this.m_isReachTarget = true;
			}
			this.SetSkipBtnEnable(false);
			return TinyFsmState.End();
		default:
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.m_isSkipMileage = true;
			this.m_events.Clear();
			return TinyFsmState.End();
		case 4:
			if (this.m_event != null && this.m_event.Update())
			{
				this.m_event = null;
			}
			if (this.m_event == null)
			{
				bool flag = true;
				if (this.m_events.Count > 0)
				{
					this.m_event = this.m_events.Dequeue();
					this.m_event.Start();
					flag = false;
				}
				if (flag)
				{
					if (this.m_isSkipMileage)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StageAllSkip)));
					}
					else if (this.m_isReachTarget)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StageDailyMission)));
					}
					else
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StageRun)));
					}
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x000CC7A4 File Offset: 0x000CA9A4
	private TinyFsmState StageRun(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.SetRunEffect(false);
			this.SetSkipBtnEnable(false);
			return TinyFsmState.End();
		case 1:
			this.m_runSePlayId = SoundManager.SePlay("sys_distance", "SE");
			this.SetRunEffect(true);
			this.SetSkipBtnEnable(true);
			return TinyFsmState.End();
		default:
			if (signal == 100)
			{
				this.m_isNext = true;
				return TinyFsmState.End();
			}
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.m_isSkipMileage = true;
			return TinyFsmState.End();
		case 4:
			if (this.m_isSkipMileage)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StageAllSkip)));
			}
			else
			{
				this.RunPlayer();
				switch (this.CheckRun())
				{
				case ui_mm_mileage_page.ArraveType.POINT:
					this.StopRunSe();
					this.m_mapInfo.waypointIndex++;
					this.SetBalloonsView(true);
					this.AddWaypointEvents();
					this.SetDistanceDsiplay();
					this.SetDistanceDsiplayPos();
					this.SetDisableBolloonView();
					SoundManager.SePlay("sys_arrive", "SE");
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEvent)));
					break;
				case ui_mm_mileage_page.ArraveType.FINISH:
					this.StopRunSe();
					this.m_mapInfo.waypointIndex = ServerInterface.MileageMapState.m_point;
					this.SetDistanceDsiplay();
					this.SetDistanceDsiplayPos();
					this.m_advanceDistanceGameObject.SetActive(false);
					this.SetDisableBolloonView();
					SoundManager.SePlay("sys_arrive", "SE");
					this.m_isReachTarget = true;
					this.m_isSkipMileage = false;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEvent)));
					break;
				case ui_mm_mileage_page.ArraveType.POINT_FINISH:
					this.StopRunSe();
					this.m_mapInfo.waypointIndex++;
					this.SetBalloonsView(true);
					this.AddWaypointEvents();
					this.SetDistanceDsiplay();
					this.SetDistanceDsiplayPos();
					this.m_advanceDistanceGameObject.SetActive(false);
					SoundManager.SePlay("sys_arrive", "SE");
					this.SetDisableBolloonView();
					this.m_isReachTarget = true;
					this.m_isSkipMileage = false;
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEvent)));
					break;
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x000CC9F8 File Offset: 0x000CABF8
	private TinyFsmState StageAllSkip(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.SetAllSkip();
			return TinyFsmState.End();
		case 4:
			if (this.m_event != null && this.m_event.Update())
			{
				this.m_event = null;
			}
			if (this.m_event == null)
			{
				bool flag = true;
				if (this.m_events.Count > 0)
				{
					this.m_event = this.m_events.Dequeue();
					this.m_event.Start();
					flag = false;
				}
				if (flag)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StageDailyMission)));
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x000CCAD0 File Offset: 0x000CACD0
	private TinyFsmState StageDailyMission(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.SetRunEffect(false);
			return TinyFsmState.End();
		case 1:
			this.m_isSkipMileage = false;
			if (this.CheckClearDailyMission())
			{
				this.AddEvent(new ui_mm_mileage_page.DailyMissionEvent(base.gameObject), -1);
				if (this.m_events.Count > 0)
				{
					this.m_event = this.m_events.Dequeue();
					this.m_event.Start();
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_event != null && this.m_event.Update())
			{
				this.m_event = null;
			}
			if (this.m_event == null)
			{
				if (this.m_events.Count > 0)
				{
					this.m_event = this.m_events.Dequeue();
					this.m_event.Start();
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
				}
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x000CCBF4 File Offset: 0x000CADF4
	private TinyFsmState StateEnd(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			this.m_isProduction = false;
			return TinyFsmState.End();
		case 1:
			this.SetEndMileageProduction();
			return TinyFsmState.End();
		case 4:
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x000CCC70 File Offset: 0x000CAE70
	private void Initialize()
	{
		this.m_isNext = false;
		this.m_isSkipMileage = false;
		this.m_mapInfo = new ui_mm_mileage_page.MapInfo();
		for (int i = 0; i < 5; i++)
		{
			this.m_limitDatas[i] = new ui_mm_mileage_page.PointTimeLimit();
		}
		this.m_playerSpriteAnimations[0].enabled = false;
		this.m_playerSpriteAnimations[1].enabled = true;
		this.m_playerEffGameObject.SetActive(false);
		this.m_runSePlayId = SoundManager.PlayId.NONE;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_5_MC");
		if (gameObject != null)
		{
			this.m_eventBannerObj = GameObjectUtil.FindChildGameObject(gameObject, "event_banner");
			if (this.m_eventBannerObj != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_eventBannerObj, "banner_slot");
				if (gameObject2 != null)
				{
					this.m_bannerObj = GameObjectUtil.FindChildGameObject(gameObject2, "img_ad_tex");
					if (this.m_bannerObj != null)
					{
						UIButtonMessage component = this.m_bannerObj.GetComponent<UIButtonMessage>();
						if (component != null)
						{
							component.enabled = true;
							component.trigger = UIButtonMessage.Trigger.OnClick;
							component.target = base.gameObject;
							component.functionName = "OnEventBannerClicked";
						}
						this.m_bannerTex = this.m_bannerObj.GetComponent<UITexture>();
					}
				}
				this.m_eventBannerObj.SetActive(false);
			}
		}
		this.m_isInit = true;
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x000CCDC4 File Offset: 0x000CAFC4
	private void RunPlayer()
	{
		if (this.m_isNext)
		{
			this.m_mapInfo.scoreDistance = this.Minimum(this.m_mapInfo.nextWaypointDistance, this.m_mapInfo.targetScoreDistance);
			this.m_isNext = false;
		}
		else if (this.m_isSkipMileage)
		{
			this.m_mapInfo.scoreDistance = this.m_mapInfo.targetScoreDistance;
		}
		else
		{
			this.m_mapInfo.scoreDistance = this.Minimum(this.m_mapInfo.scoreDistance + (double)(this.m_playerRunSpeed * Time.deltaTime), this.m_mapInfo.targetScoreDistance);
			this.m_mapInfo.scoreDistance = this.Minimum(this.m_mapInfo.scoreDistance, this.m_mapInfo.nextWaypointDistance);
		}
		this.SetPlayerPosition();
		this.SetDistanceDsiplay();
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x000CCEA0 File Offset: 0x000CB0A0
	private ui_mm_mileage_page.ArraveType CheckRun()
	{
		if (this.m_mapInfo.scoreDistance == this.m_mapInfo.nextWaypointDistance && this.m_mapInfo.waypointIndex < 5)
		{
			if (this.m_mapInfo.scoreDistance == this.m_mapInfo.targetScoreDistance)
			{
				return ui_mm_mileage_page.ArraveType.POINT_FINISH;
			}
			return ui_mm_mileage_page.ArraveType.POINT;
		}
		else
		{
			if (this.m_mapInfo.scoreDistance == this.m_mapInfo.targetScoreDistance)
			{
				return ui_mm_mileage_page.ArraveType.FINISH;
			}
			return ui_mm_mileage_page.ArraveType.RUNNIG;
		}
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x000CCF18 File Offset: 0x000CB118
	public void SetBG()
	{
		if (MileageMapDataManager.Instance != null)
		{
			int mileageStageIndex = MileageMapDataManager.Instance.MileageStageIndex;
		}
		if (this.m_stageBGTex != null)
		{
			Texture bgtexture = MileageMapUtility.GetBGTexture();
			if (bgtexture != null)
			{
				this.m_stageBGTex.mainTexture = bgtexture;
			}
		}
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x000CCF74 File Offset: 0x000CB174
	private void SetRunEffect(bool flag)
	{
		this.m_playerSpriteAnimations[0].enabled = flag;
		this.m_playerSpriteAnimations[1].enabled = !flag;
		this.m_playerEffGameObject.SetActive(flag);
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x000CCFA4 File Offset: 0x000CB1A4
	private void StopRunSe()
	{
		SoundManager.SeStop("sys_distance", "SE");
		this.m_runSePlayId = SoundManager.PlayId.NONE;
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x000CCFBC File Offset: 0x000CB1BC
	private void SetArraivalFaceTexture()
	{
		PointEventData pointEventData = MileageMapDataManager.Instance.GetMileageMapData().event_data.point[5];
		if (pointEventData != null)
		{
			this.SetBalloonFaceTexture(4, pointEventData.balloon_on_arrival_face_id);
		}
	}

	// Token: 0x06002200 RID: 8704 RVA: 0x000CCFF4 File Offset: 0x000CB1F4
	private void SetBossClearEvent()
	{
		BossEvent bossEvent = MileageMapDataManager.Instance.GetMileageMapData().event_data.GetBossEvent();
		this.AddEvent(new ui_mm_mileage_page.BalloonEvent(base.gameObject, 4, bossEvent.balloon_clear_face_id, bossEvent.balloon_on_arrival_face_id), -1);
		this.AddEvent(new ui_mm_mileage_page.GorgeousEvent(base.gameObject, bossEvent.after_window_id, this.IsExistsMapClearEvents()), -1);
		this.AddMapClearEvents();
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x000CD05C File Offset: 0x000CB25C
	private bool CheckClearDailyMission()
	{
		return this.m_mapInfo.m_resultData != null && this.m_mapInfo.m_resultData.m_missionComplete;
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x000CD08C File Offset: 0x000CB28C
	private bool IsRankingUp()
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			RankingUtil.RankChange rankingRankChange = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(RankingUtil.RankingMode.ENDLESS, RankingUtil.RankingScoreType.HIGH_SCORE, RankingUtil.RankingRankerType.RIVAL);
			return rankingRankChange == RankingUtil.RankChange.UP;
		}
		return false;
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000CD0C0 File Offset: 0x000CB2C0
	private void SetAll()
	{
		this.SetPlayerPosition();
		this.SetWaypoints();
		this.SetRoutes();
		this.SetTimeLimit();
		this.SetBalloonsView(false);
		this.SetUchanged();
		this.SetDistanceDsiplay();
		this.SetDistanceDsiplayPos();
		double a = (double)MileageMapDataManager.Instance.GetMileageMapData().map_data.event_interval;
		double b = this.m_mapInfo.targetScoreDistance - this.m_mapInfo.scoreDistance;
		this.m_playerRunSpeed = (float)this.Minimum(a, b);
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x000CD13C File Offset: 0x000CB33C
	private void SetPlayerPosition()
	{
		double num = this.m_mapInfo.scoreDistance / (double)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5);
		if (this.m_playerSlider != null)
		{
			this.m_playerSlider.value = (float)num;
		}
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x000CD17C File Offset: 0x000CB37C
	private void SetWaypoints()
	{
		this.SetWaypoint(this.m_waypointsSprite[0], 2);
		EventData event_data = MileageMapDataManager.Instance.GetMileageMapData().event_data;
		if (event_data.point.Length == 6)
		{
			this.SetWaypoint(this.m_waypointsSprite[1], event_data.point[1].event_type);
			this.SetWaypoint(this.m_waypointsSprite[2], event_data.point[2].event_type);
			this.SetWaypoint(this.m_waypointsSprite[3], event_data.point[3].event_type);
			this.SetWaypoint(this.m_waypointsSprite[4], event_data.point[4].event_type);
			this.SetWaypoint(this.m_waypointsSprite[5], event_data.point[5].event_type);
		}
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x000CD240 File Offset: 0x000CB440
	private void SetWaypoint(UISprite sprite, int point_id)
	{
		if (sprite != null)
		{
			sprite.spriteName = MileageMapPointDataTable.Instance.GetTextureName(point_id);
		}
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x000CD260 File Offset: 0x000CB460
	private void SetRoutes()
	{
		for (int i = 0; i < 5; i++)
		{
			if (this.m_routesObjects[i] != null)
			{
				if (this.m_routesObjects[i].m_bonusRootGameObject != null)
				{
					this.m_routesObjects[i].m_bonusRootGameObject.SetActive(false);
				}
				if (this.m_routesObjects[i].m_lineSprite != null)
				{
					this.m_routesObjects[i].m_lineSprite.spriteName = "ui_mm_mileage_route_1";
				}
				if (this.m_routesObjects[i].m_lineEffectGameObject != null)
				{
					this.m_routesObjects[i].m_lineEffectGameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x000CD314 File Offset: 0x000CB514
	private void SetBalloonsView(bool disable_on_arrival = false)
	{
		for (int i = 0; i < 5; i++)
		{
			this.SetBalloonView(i, disable_on_arrival);
		}
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x000CD33C File Offset: 0x000CB53C
	private void SetTimeLimit()
	{
		for (int i = 0; i < 5; i++)
		{
			if (this.m_limitDatas[i] != null)
			{
				this.m_limitDatas[i].Reset();
			}
		}
		MileageMapDataManager mileageMapDataManager = MileageMapDataManager.Instance;
		int episode = mileageMapDataManager.GetMileageMapData().scenario.episode;
		int chapter = mileageMapDataManager.GetMileageMapData().scenario.chapter;
		for (int j = 0; j < 5; j++)
		{
			int point = j + 1;
			ServerMileageReward mileageReward = mileageMapDataManager.GetMileageReward(episode, chapter, point);
			if (mileageReward != null && this.m_limitDatas[j] != null)
			{
				bool incentiveFlag = this.m_mapInfo.CheckMileageIncentive(point);
				this.m_limitDatas[j].SetupLimit(mileageReward, this.m_balloonsObjects[j], incentiveFlag);
			}
		}
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x000CD404 File Offset: 0x000CB604
	private void SetDisableBolloonView()
	{
		for (int i = 0; i < 4; i++)
		{
			int num = (i + 1) * ui_mm_mileage_page.MapInfo.routeScoreDistance;
			if ((double)num <= this.m_mapInfo.scoreDistance)
			{
				this.m_balloonsObjects[i].m_gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600220B RID: 8715 RVA: 0x000CD454 File Offset: 0x000CB654
	private void SetBalloonView(int eventIndex, bool disable_on_arrival)
	{
		int num = eventIndex + 1;
		EventData event_data = MileageMapDataManager.Instance.GetMileageMapData().event_data;
		int num2 = (num >= 5) ? (event_data.IsBossEvent() ? ((this.m_mapInfo.waypointIndex >= 5 && !disable_on_arrival) ? event_data.GetBossEvent().balloon_on_arrival_face_id : event_data.GetBossEvent().balloon_init_face_id) : ((this.m_mapInfo.waypointIndex >= 5 && event_data.point[num].balloon_on_arrival_face_id != -1 && !disable_on_arrival) ? event_data.point[num].balloon_on_arrival_face_id : event_data.point[num].balloon_face_id)) : event_data.point[num].balloon_face_id;
		int num3 = eventIndex + 1;
		bool flag = num2 >= 0 && this.m_mapInfo.scoreDistance <= (double)(num3 * ui_mm_mileage_page.MapInfo.routeScoreDistance);
		if (flag && this.m_limitDatas[eventIndex] != null && this.m_limitDatas[eventIndex].FailedFlag)
		{
			flag = false;
		}
		this.m_balloonsObjects[eventIndex].m_gameObject.SetActive(flag);
		if (flag)
		{
			this.SetBalloonFrame(eventIndex);
			this.SetBalloonFaceTexture(eventIndex, num2);
		}
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x000CD598 File Offset: 0x000CB798
	private void SetBalloonFrame(int eventIndex)
	{
		ui_mm_mileage_page.BalloonObjects balloonObjects = this.m_balloonsObjects[eventIndex];
		bool limitFlag = this.m_limitDatas[eventIndex].LimitFlag;
		if (balloonObjects.m_normalFrameObject != null)
		{
			balloonObjects.m_normalFrameObject.SetActive(!limitFlag);
		}
		if (balloonObjects.m_timerFrameObject != null)
		{
			balloonObjects.m_timerFrameObject.SetActive(limitFlag);
		}
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x000CD5FC File Offset: 0x000CB7FC
	private void SetBalloonFaceTexture(int eventIndex, int faceId)
	{
		ui_mm_mileage_page.BalloonObjects balloonObjects = this.m_balloonsObjects[eventIndex];
		Texture texture = MileageMapUtility.GetFaceTexture(faceId) ?? GeneralWindow.GetDummyTexture(faceId);
		if (balloonObjects.m_texture != null && balloonObjects.m_texture.mainTexture != texture)
		{
			balloonObjects.m_texture.mainTexture = texture;
		}
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x000CD65C File Offset: 0x000CB85C
	private void SetUchanged()
	{
		int episode = MileageMapDataManager.Instance.GetMileageMapData().scenario.episode;
		int chapter = MileageMapDataManager.Instance.GetMileageMapData().scenario.chapter;
		this.m_scenarioNumberLabel.text = episode.ToString("000") + "-" + chapter.ToString();
		string title_cell_id = MileageMapDataManager.Instance.GetMileageMapData().scenario.title_cell_id;
		this.m_titleLabel.text = MileageMapText.GetText(episode, title_cell_id);
		bool active = this.m_mapInfo.m_resultData != null;
		if (this.m_mapInfo.isBossStage)
		{
			active = false;
		}
		else if (this.m_mapInfo.isNextMileage)
		{
			active = false;
		}
		else if (this.m_mapInfo.scoreDistanceRaw == 0.0)
		{
			active = false;
		}
		this.m_advanceDistanceGameObject.SetActive(active);
		this.m_advanceDistanceLabel.text = HudUtility.GetFormatNumString<double>(this.m_mapInfo.scoreDistanceRaw);
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x000CD764 File Offset: 0x000CB964
	private void SetDistanceDsiplay()
	{
		this.m_distanceLabel.text = HudUtility.GetFormatNumString<double>((double)(ui_mm_mileage_page.MapInfo.routeScoreDistance * this.m_mapInfo.nextWaypoint) - this.m_mapInfo.scoreDistance);
		this.m_advanceDistanceLabel.text = HudUtility.GetFormatNumString<double>(this.m_mapInfo.scoreDistanceRaw - this.m_mapInfo.GetRunDistance());
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x000CD7C8 File Offset: 0x000CB9C8
	private void SetDistanceDsiplayPos()
	{
		if (this.m_mapInfo.waypointIndex == 5)
		{
			if (this.m_distanceSlider != null)
			{
				this.m_distanceSlider.gameObject.SetActive(false);
			}
		}
		else
		{
			float num = (float)(this.m_mapInfo.waypointIndex + 1) * 0.2f;
			if (num > 1f)
			{
				num = 1f;
			}
			if (this.m_distanceSlider != null)
			{
				this.m_distanceSlider.gameObject.SetActive(true);
				this.m_distanceSlider.value = num;
			}
		}
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x000CD864 File Offset: 0x000CBA64
	private void SetPlayBtnImg()
	{
		if (this.m_btnPlayObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_btnPlayObject, "img_word_play");
			if (uisprite != null)
			{
				if (MileageMapUtility.IsBossStage())
				{
					uisprite.spriteName = "ui_mm_btn_word_play_boss";
				}
				else
				{
					uisprite.spriteName = "ui_mm_btn_word_play";
				}
			}
			int stageIndex = 1;
			if (MileageMapDataManager.Instance != null)
			{
				stageIndex = MileageMapDataManager.Instance.MileageStageIndex;
			}
			CharacterAttribute[] characterAttribute = MileageMapUtility.GetCharacterAttribute(stageIndex);
			if (characterAttribute != null)
			{
				for (int i = 0; i < 3; i++)
				{
					string name = "img_icon_type_" + (i + 1).ToString();
					UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_btnPlayObject, name);
					if (uisprite2 != null)
					{
						if (i < characterAttribute.Length)
						{
							switch (characterAttribute[i])
							{
							case CharacterAttribute.SPEED:
								uisprite2.enabled = true;
								uisprite2.spriteName = "ui_chao_set_type_icon_speed";
								break;
							case CharacterAttribute.FLY:
								uisprite2.enabled = true;
								uisprite2.spriteName = "ui_chao_set_type_icon_fly";
								break;
							case CharacterAttribute.POWER:
								uisprite2.enabled = true;
								uisprite2.spriteName = "ui_chao_set_type_icon_power";
								break;
							default:
								uisprite2.enabled = false;
								break;
							}
						}
						else
						{
							uisprite2.enabled = false;
						}
					}
				}
			}
			UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_btnPlayObject, "img_next_map");
			if (uisprite3 != null)
			{
				uisprite3.spriteName = "ui_mm_map_thumb_w" + stageIndex.ToString("00") + "a";
			}
		}
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000CDA00 File Offset: 0x000CBC00
	private void SetEndMileageProduction()
	{
		this.ResetRewindOffsetToSaveData();
		if (this.m_patternNextObject != null)
		{
			this.m_patternNextObject.SetActive(false);
		}
		HudMenuUtility.SendEnableShopButton(true);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		this.SetBannerCollider(true);
		if (this.IsChangeDataVersion() || this.IsTutorialEvent())
		{
			HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.EPISODE_BACK, false);
		}
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000CDA60 File Offset: 0x000CBC60
	private bool IsChangeDataVersion()
	{
		return ServerInterface.LoginState != null && (ServerInterface.LoginState.IsChangeDataVersion || ServerInterface.LoginState.IsChangeAssetsVersion);
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x000CDA98 File Offset: 0x000CBC98
	private bool IsTutorialEvent()
	{
		return HudMenuUtility.IsTutorialCharaLevelUp() || HudMenuUtility.IsRouletteTutorial() || HudMenuUtility.IsRecommendReviewTutorial();
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x000CDABC File Offset: 0x000CBCBC
	private void SetSkipBtnEnable(bool flag)
	{
		if (this.m_btnNextObject != null)
		{
			BoxCollider component = this.m_btnNextObject.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.isTrigger = !flag;
			}
		}
		if (this.m_btnSkipObject != null)
		{
			UIImageButton component2 = this.m_btnSkipObject.GetComponent<UIImageButton>();
			if (component2 != null)
			{
				component2.isEnabled = flag;
			}
		}
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x000CDB2C File Offset: 0x000CBD2C
	private void SetPlanelAlha()
	{
		UIPanel component = base.gameObject.GetComponent<UIPanel>();
		if (component != null)
		{
			component.alpha = 1f;
		}
	}

	// Token: 0x06002217 RID: 8727 RVA: 0x000CDB5C File Offset: 0x000CBD5C
	public void StartMileageMapProduction()
	{
		base.StartCoroutine(this.DelayStart());
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x000CDB6C File Offset: 0x000CBD6C
	private void SetAllSkip()
	{
		this.StopRunSe();
		this.ResetRewindOffsetToSaveData();
		this.m_isSkipMileage = false;
		if (this.m_mapInfo.IsClearMileage())
		{
			this.SetMileageClearDisplayOffset_FromResultData(this.m_mapInfo.m_resultData);
			if (this.m_mapInfo.m_resultData != null && !this.m_mapInfo.m_resultData.m_bossDestroy)
			{
				this.m_mapInfo.scoreDistance = this.m_mapInfo.targetScoreDistance;
				this.m_mapInfo.waypointIndex = 5;
				this.SetBalloonsView(true);
				this.SetArraivalFaceTexture();
				this.SetDistanceDsiplay();
				this.SetDistanceDsiplayPos();
				this.SetPlayerPosition();
			}
			this.AddMapClearEvents();
		}
		else
		{
			SoundManager.BgmChange("bgm_sys_menu", "BGM");
			MileageMapDataManager.Instance.SetCurrentData(ServerInterface.MileageMapState.m_episode, ServerInterface.MileageMapState.m_chapter);
			MileageMapState mileageMapState = new MileageMapState();
			mileageMapState.m_episode = ServerInterface.MileageMapState.m_episode;
			mileageMapState.m_chapter = ServerInterface.MileageMapState.m_chapter;
			mileageMapState.m_point = ServerInterface.MileageMapState.m_point;
			mileageMapState.m_score = ServerInterface.MileageMapState.m_stageTotalScore;
			this.m_mapInfo.UpdateFrom(mileageMapState);
			this.SetPlayBtnImg();
			this.SetBG();
			this.SetAll();
		}
		this.m_advanceDistanceGameObject.SetActive(false);
		if (!this.m_isReachTarget)
		{
			SoundManager.SePlay("sys_arrive", "SE");
		}
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x000CDCDC File Offset: 0x000CBEDC
	public IEnumerator DelayStart()
	{
		int waite_frame = 5;
		while (waite_frame > 0)
		{
			waite_frame--;
			yield return null;
		}
		if (this.m_mapInfo.highscore >= 0L)
		{
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateHighScoreEvent)));
		}
		else
		{
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateEvent)));
		}
		yield break;
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x000CDCF8 File Offset: 0x000CBEF8
	private void OnStartMileage()
	{
		this.m_isStart = true;
		if (this.m_isInit && this.m_isProduction)
		{
			this.StartMileageMapProduction();
		}
		this.SetEventBanner();
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x000CDD24 File Offset: 0x000CBF24
	private void OnEndMileage()
	{
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x000CDD28 File Offset: 0x000CBF28
	public void OnUpdateMileageMapDisplay()
	{
		if (this.m_isInit)
		{
			return;
		}
		this.Initialize();
		MileageMapState mileageMapState = new MileageMapState();
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			mileageMapState.m_episode = ServerInterface.MileageMapState.m_episode;
			mileageMapState.m_chapter = ServerInterface.MileageMapState.m_chapter;
			mileageMapState.m_point = ServerInterface.MileageMapState.m_point;
			mileageMapState.m_score = ServerInterface.MileageMapState.m_stageTotalScore;
		}
		else
		{
			mileageMapState.m_episode = MileageMapDataManager.Instance.GetMileageMapData().scenario.episode;
			mileageMapState.m_chapter = MileageMapDataManager.Instance.GetMileageMapData().scenario.chapter;
			mileageMapState.m_point = 0;
			mileageMapState.m_score = 0L;
		}
		this.m_mapInfo.UpdateFrom(mileageMapState);
		this.SetPlayBtnImg();
		this.SetBG();
		this.SetSkipBtnEnable(false);
		this.SetAll();
		this.SetRunEffect(false);
		this.SetPlanelAlha();
		this.SetEventBanner();
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x000CDE20 File Offset: 0x000CC020
	public void OnPrepareMileageMapProduction(ResultData resultData)
	{
		if (this.m_isInit)
		{
			return;
		}
		if (resultData != null && resultData.m_quickMode)
		{
			this.OnUpdateMileageMapDisplay();
			return;
		}
		this.Initialize();
		this.SetDisplayOffset_FromResultData(resultData);
		this.m_mapInfo.UpdateFrom(resultData);
		this.SetPlayBtnImg();
		this.SetBG();
		this.SetAll();
		if (this.m_patternNextObject != null)
		{
			this.m_patternNextObject.SetActive(true);
		}
		this.SetRunEffect(false);
		this.SetPlanelAlha();
		HudMenuUtility.SendEnableShopButton(false);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		this.m_isProduction = true;
		BackKeyManager.AddMileageCallBack(base.gameObject);
		this.SetSkipBtnEnable(false);
		if (this.m_isStart)
		{
			this.StartMileageMapProduction();
		}
		this.SetEventBanner();
		this.SetBannerCollider(false);
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x000CDEEC File Offset: 0x000CC0EC
	private void OnClickNextBtn()
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x000CDF20 File Offset: 0x000CC120
	public void OnClickAllSkipBtn()
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x000CDF54 File Offset: 0x000CC154
	private void OnClosedCharaGetWindow()
	{
		ui_mm_mileage_page.SimpleEvent simpleEvent = this.m_event as ui_mm_mileage_page.SimpleEvent;
		if (simpleEvent != null)
		{
			simpleEvent.isEnd = true;
		}
	}

	// Token: 0x06002221 RID: 8737 RVA: 0x000CDF7C File Offset: 0x000CC17C
	private void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isProduction)
		{
			this.OnClickNextBtn();
			if (msg != null)
			{
				msg.StaySequence();
			}
		}
	}

	// Token: 0x06002222 RID: 8738 RVA: 0x000CDF9C File Offset: 0x000CC19C
	private void SetDisplayOffset_FromResultData(ResultData resultData)
	{
		if (this.m_displayOffset != null && this.m_displayOffset.Length == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_displayOffset[i] = 0;
			}
			if (MileageMapUtility.IsRankUp_FromResultData(resultData))
			{
				this.m_displayOffset[0] = 1;
			}
			this.m_displayOffset[1] = MileageMapUtility.GetDisplayOffset_FromResultData(resultData, ServerItem.Id.RSRING);
			this.m_displayOffset[2] = MileageMapUtility.GetDisplayOffset_FromResultData(resultData, ServerItem.Id.RING);
		}
	}

	// Token: 0x06002223 RID: 8739 RVA: 0x000CE018 File Offset: 0x000CC218
	private void SetMileageClearDisplayOffset_FromResultData(ResultData resultData)
	{
		if (this.m_displayOffset != null && this.m_displayOffset.Length == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_displayOffset[i] = 0;
			}
			if (MileageMapUtility.IsRankUp_FromResultData(resultData))
			{
				this.m_displayOffset[0] = 1;
			}
			this.m_displayOffset[1] = MileageMapUtility.GetMileageClearDisplayOffset_FromResultData(resultData, ServerItem.Id.RSRING);
			this.m_displayOffset[2] = MileageMapUtility.GetMileageClearDisplayOffset_FromResultData(resultData, ServerItem.Id.RING);
		}
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager == null)
		{
			return;
		}
		PlayerData playerData = saveDataManager.PlayerData;
		if (playerData != null)
		{
			playerData.RankOffset = -this.m_displayOffset[0];
		}
		ItemData itemData = saveDataManager.ItemData;
		if (itemData != null)
		{
			itemData.RedRingCountOffset = -this.m_displayOffset[1];
			itemData.RingCountOffset = -this.m_displayOffset[2];
		}
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x000CE0F0 File Offset: 0x000CC2F0
	private void ResetRewindOffsetToSaveData()
	{
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager == null)
		{
			return;
		}
		PlayerData playerData = saveDataManager.PlayerData;
		if (playerData != null)
		{
			playerData.RankOffset = 0;
		}
		ItemData itemData = saveDataManager.ItemData;
		if (itemData != null)
		{
			itemData.RingCountOffset = 0;
			itemData.RedRingCountOffset = 0;
		}
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000CE140 File Offset: 0x000CC340
	private void SetEventBanner()
	{
		if (!this.m_isInit)
		{
			return;
		}
		bool flag = false;
		if (EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.BGM)
		{
			EventStageData stageData = EventManager.Instance.GetStageData();
			if (stageData != null)
			{
				flag = stageData.IsEndlessModeBGM();
			}
		}
		if (flag)
		{
			if (ServerInterface.NoticeInfo != null && ServerInterface.NoticeInfo.m_eventItems != null)
			{
				foreach (NetNoticeItem netNoticeItem in ServerInterface.NoticeInfo.m_eventItems)
				{
					if (this.m_infoId != netNoticeItem.Id)
					{
						this.m_infoId = netNoticeItem.Id;
						if (InformationImageManager.Instance != null)
						{
							InformationImageManager.Instance.Load(netNoticeItem.ImageId, true, new Action<Texture2D>(this.OnLoadCallback));
						}
						if (this.m_eventBannerObj != null)
						{
							this.m_eventBannerObj.SetActive(true);
						}
						break;
					}
				}
			}
		}
		else
		{
			if (this.m_eventBannerObj != null)
			{
				this.m_eventBannerObj.SetActive(false);
			}
			if (this.m_bannerTex != null && this.m_bannerTex.mainTexture != null)
			{
				this.m_bannerTex.mainTexture = null;
			}
		}
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x000CE2CC File Offset: 0x000CC4CC
	public void OnLoadCallback(Texture2D texture)
	{
		if (this.m_bannerTex != null && texture != null)
		{
			this.m_bannerTex.mainTexture = texture;
		}
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x000CE2F8 File Offset: 0x000CC4F8
	private void OnEventBannerClicked()
	{
		this.m_infoWindow = base.gameObject.GetComponent<InformationWindow>();
		if (this.m_infoWindow == null)
		{
			this.m_infoWindow = base.gameObject.AddComponent<InformationWindow>();
		}
		if (this.m_infoWindow != null && ServerInterface.NoticeInfo != null && ServerInterface.NoticeInfo.m_eventItems != null)
		{
			foreach (NetNoticeItem netNoticeItem in ServerInterface.NoticeInfo.m_eventItems)
			{
				if (this.m_infoId == netNoticeItem.Id)
				{
					InformationWindow.Information info = default(InformationWindow.Information);
					info.pattern = InformationWindow.ButtonPattern.OK;
					info.imageId = netNoticeItem.ImageId;
					info.caption = TextUtility.GetCommonText("Informaion", "announcement");
					GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
					if (cameraUIObject != null)
					{
						GameObject newsWindowObj = GameObjectUtil.FindChildGameObject(cameraUIObject, "NewsWindow");
						this.m_infoWindow.Create(info, newsWindowObj);
						base.enabled = true;
						SoundManager.SePlay("sys_menu_decide", "SE");
					}
					break;
				}
			}
		}
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x000CE448 File Offset: 0x000CC648
	private void SetBannerCollider(bool on)
	{
		if (this.m_bannerObj != null)
		{
			BoxCollider component = this.m_bannerObj.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.isTrigger = !on;
			}
		}
	}

	// Token: 0x04001E9C RID: 7836
	private const int POINT_COUNT = 6;

	// Token: 0x04001E9D RID: 7837
	private const int ROUTE_COUNT = 5;

	// Token: 0x04001E9E RID: 7838
	private const int BALLOON_COUNT = 5;

	// Token: 0x04001E9F RID: 7839
	private const int BOSS_EVENT_INDEX = 4;

	// Token: 0x04001EA0 RID: 7840
	private const float BALLOON_WAIT = 0.5f;

	// Token: 0x04001EA1 RID: 7841
	private static ui_mm_mileage_page instance;

	// Token: 0x04001EA2 RID: 7842
	[SerializeField]
	private bool m_disabled;

	// Token: 0x04001EA3 RID: 7843
	[SerializeField]
	private float m_playerRunSpeed = 1f;

	// Token: 0x04001EA4 RID: 7844
	[SerializeField]
	private float m_eventWait = 0.5f;

	// Token: 0x04001EA5 RID: 7845
	[SerializeField]
	private GameObject m_playerGameObject;

	// Token: 0x04001EA6 RID: 7846
	[SerializeField]
	private UISprite m_playerSprite;

	// Token: 0x04001EA7 RID: 7847
	[SerializeField]
	private UISpriteAnimation[] m_playerSpriteAnimations = new UISpriteAnimation[2];

	// Token: 0x04001EA8 RID: 7848
	[SerializeField]
	private UISlider m_playerSlider;

	// Token: 0x04001EA9 RID: 7849
	[SerializeField]
	private GameObject m_playerEffGameObject;

	// Token: 0x04001EAA RID: 7850
	[SerializeField]
	private UISprite[] m_waypointsSprite = new UISprite[6];

	// Token: 0x04001EAB RID: 7851
	[SerializeField]
	private ui_mm_mileage_page.RouteObjects[] m_routesObjects = new ui_mm_mileage_page.RouteObjects[5];

	// Token: 0x04001EAC RID: 7852
	[SerializeField]
	private ui_mm_mileage_page.BalloonObjects[] m_balloonsObjects = new ui_mm_mileage_page.BalloonObjects[5];

	// Token: 0x04001EAD RID: 7853
	[SerializeField]
	private UILabel m_scenarioNumberLabel;

	// Token: 0x04001EAE RID: 7854
	[SerializeField]
	private UILabel m_titleLabel;

	// Token: 0x04001EAF RID: 7855
	[SerializeField]
	private UILabel m_distanceLabel;

	// Token: 0x04001EB0 RID: 7856
	[SerializeField]
	private UILabel m_advanceDistanceLabel;

	// Token: 0x04001EB1 RID: 7857
	[SerializeField]
	private GameObject m_advanceDistanceGameObject;

	// Token: 0x04001EB2 RID: 7858
	[SerializeField]
	private GameObject m_patternNextObject;

	// Token: 0x04001EB3 RID: 7859
	[SerializeField]
	private GameObject m_btnNextObject;

	// Token: 0x04001EB4 RID: 7860
	[SerializeField]
	private GameObject m_btnSkipObject;

	// Token: 0x04001EB5 RID: 7861
	[SerializeField]
	private GameObject m_btnPlayObject;

	// Token: 0x04001EB6 RID: 7862
	[SerializeField]
	private UITexture m_stageBGTex;

	// Token: 0x04001EB7 RID: 7863
	private UISlider m_distanceSlider;

	// Token: 0x04001EB8 RID: 7864
	private ui_mm_mileage_page.MapInfo m_mapInfo;

	// Token: 0x04001EB9 RID: 7865
	private Queue<ui_mm_mileage_page.BaseEvent> m_events = new Queue<ui_mm_mileage_page.BaseEvent>();

	// Token: 0x04001EBA RID: 7866
	private ui_mm_mileage_page.BaseEvent m_event;

	// Token: 0x04001EBB RID: 7867
	private ui_mm_mileage_page.PointTimeLimit[] m_limitDatas = new ui_mm_mileage_page.PointTimeLimit[5];

	// Token: 0x04001EBC RID: 7868
	private SoundManager.PlayId m_runSePlayId;

	// Token: 0x04001EBD RID: 7869
	private UITexture m_bannerTex;

	// Token: 0x04001EBE RID: 7870
	private GameObject m_bannerObj;

	// Token: 0x04001EBF RID: 7871
	private GameObject m_eventBannerObj;

	// Token: 0x04001EC0 RID: 7872
	private long m_infoId = -1L;

	// Token: 0x04001EC1 RID: 7873
	private InformationWindow m_infoWindow;

	// Token: 0x04001EC2 RID: 7874
	private bool m_isInit;

	// Token: 0x04001EC3 RID: 7875
	private bool m_isStart;

	// Token: 0x04001EC4 RID: 7876
	private bool m_isNext;

	// Token: 0x04001EC5 RID: 7877
	private bool m_isSkipMileage;

	// Token: 0x04001EC6 RID: 7878
	private bool m_isProduction;

	// Token: 0x04001EC7 RID: 7879
	private bool m_isReachTarget;

	// Token: 0x04001EC8 RID: 7880
	private int[] m_displayOffset = new int[3];

	// Token: 0x04001EC9 RID: 7881
	private TinyFsmBehavior m_fsm_behavior;

	// Token: 0x0200046D RID: 1133
	private enum WayPointEventType
	{
		// Token: 0x04001ECB RID: 7883
		NONE,
		// Token: 0x04001ECC RID: 7884
		SIMPLE,
		// Token: 0x04001ECD RID: 7885
		GORGEOUS,
		// Token: 0x04001ECE RID: 7886
		LAST
	}

	// Token: 0x0200046E RID: 1134
	private abstract class BaseEvent
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x000CE488 File Offset: 0x000CC688
		public BaseEvent(GameObject gameObject)
		{
			this.gameObject = gameObject;
			this.mileage_page = gameObject.GetComponent<ui_mm_mileage_page>();
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000CE4B0 File Offset: 0x000CC6B0
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x000CE4B8 File Offset: 0x000CC6B8
		protected GameObject gameObject { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000CE4C4 File Offset: 0x000CC6C4
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x000CE4CC File Offset: 0x000CC6CC
		protected ui_mm_mileage_page mileage_page { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000CE4D8 File Offset: 0x000CC6D8
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x000CE4E0 File Offset: 0x000CC6E0
		public bool isEnd { get; set; }

		// Token: 0x06002230 RID: 8752 RVA: 0x000CE4EC File Offset: 0x000CC6EC
		public virtual void Start()
		{
		}

		// Token: 0x06002231 RID: 8753
		public abstract bool Update();

		// Token: 0x06002232 RID: 8754 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
		public virtual void SkipMileageProcess()
		{
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000CE4F4 File Offset: 0x000CC6F4
		protected bool IsAskSnsFeed()
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					return systemdata.IsFacebookWindow();
				}
			}
			return true;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000CE528 File Offset: 0x000CC728
		protected void SetDisableAskSnsFeed()
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.SetFacebookWindow(false);
					instance.SaveSystemData();
				}
			}
		}
	}

	// Token: 0x0200046F RID: 1135
	private class WaitEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002235 RID: 8757 RVA: 0x000CE564 File Offset: 0x000CC764
		public WaitEvent(GameObject gameObject, float waitTime) : base(gameObject)
		{
			this.waitTime = waitTime;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000CE574 File Offset: 0x000CC774
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x000CE57C File Offset: 0x000CC77C
		private float waitTime { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000CE588 File Offset: 0x000CC788
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x000CE590 File Offset: 0x000CC790
		private float time { get; set; }

		// Token: 0x0600223A RID: 8762 RVA: 0x000CE59C File Offset: 0x000CC79C
		public override void Start()
		{
			this.time = 0f;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000CE5AC File Offset: 0x000CC7AC
		public override bool Update()
		{
			float time = this.time;
			this.time += Time.deltaTime;
			if (time < this.waitTime && this.time >= this.waitTime)
			{
				base.isEnd = true;
			}
			return base.isEnd;
		}
	}

	// Token: 0x02000470 RID: 1136
	private class GeneralEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x0600223C RID: 8764 RVA: 0x000CE5FC File Offset: 0x000CC7FC
		public GeneralEvent(GameObject gameObject, GeneralWindow.ButtonType buttonType, string title, string message) : base(gameObject)
		{
			this.buttonType = buttonType;
			this.title = title;
			this.message = message;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000CE628 File Offset: 0x000CC828
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x000CE630 File Offset: 0x000CC830
		public GeneralWindow.ButtonType buttonType { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x000CE63C File Offset: 0x000CC83C
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x000CE644 File Offset: 0x000CC844
		public string title { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000CE650 File Offset: 0x000CC850
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x000CE658 File Offset: 0x000CC858
		public string message { get; private set; }

		// Token: 0x06002243 RID: 8771 RVA: 0x000CE664 File Offset: 0x000CC864
		public override void Start()
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				buttonType = this.buttonType,
				caption = this.title,
				message = this.message
			});
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000CE6A8 File Offset: 0x000CC8A8
		public override bool Update()
		{
			if (GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				base.isEnd = true;
			}
			return base.isEnd;
		}
	}

	// Token: 0x02000471 RID: 1137
	private class SimpleEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x000CE6C8 File Offset: 0x000CC8C8
		public SimpleEvent(GameObject gameObject, int serverItemId, int count, string title, bool disableSe = false) : base(gameObject)
		{
			this.serverItemId = serverItemId;
			ServerItem serverItem = new ServerItem((ServerItem.Id)serverItemId);
			this.rewardType = serverItem.rewardType;
			this.count = count;
			this.title = title;
			this.disableSe = disableSe;
			this.m_serverItem = new ServerItem(this.rewardType);
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000CE728 File Offset: 0x000CC928
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x000CE730 File Offset: 0x000CC930
		private RewardType rewardType { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000CE73C File Offset: 0x000CC93C
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x000CE744 File Offset: 0x000CC944
		private int serverItemId { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000CE750 File Offset: 0x000CC950
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x000CE758 File Offset: 0x000CC958
		private int count { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000CE764 File Offset: 0x000CC964
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000CE76C File Offset: 0x000CC96C
		private string title { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000CE778 File Offset: 0x000CC978
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x000CE780 File Offset: 0x000CC980
		private bool disableSe { get; set; }

		// Token: 0x06002250 RID: 8784 RVA: 0x000CE78C File Offset: 0x000CC98C
		public override void Start()
		{
			if (this.rewardType != RewardType.NONE && this.count > 0)
			{
				MileageMapUtility.AddReward(this.rewardType, this.count);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000CE7C4 File Offset: 0x000CC9C4
		public void ResourceLoadEndCallback()
		{
			if (FontManager.Instance != null)
			{
				FontManager.Instance.ReplaceFont();
			}
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplaceAtlasForMenu(true);
			}
			this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.START_WIDOW;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000CE810 File Offset: 0x000CCA10
		public override bool Update()
		{
			switch (this.m_stateMode)
			{
			case ui_mm_mileage_page.SimpleEvent.StateMode.SET_WINDOW_TYPE:
				if (this.rewardType != RewardType.NONE && this.count > 0)
				{
					this.SetWindowType();
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.REQUEST_LOAD;
				}
				else
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.END;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.StateMode.REQUEST_LOAD:
				this.RequestLoadWindow();
				if (this.m_windowType == ui_mm_mileage_page.SimpleEvent.WindowType.UNKNOWN)
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.END;
				}
				else if (this.m_windowType == ui_mm_mileage_page.SimpleEvent.WindowType.ITEM)
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.START_WIDOW;
				}
				else
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.WAIT_LOAD;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.StateMode.WAIT_LOAD:
				if (this.m_buttonEventResourceLoader != null && this.m_buttonEventResourceLoader.IsLoaded)
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.START_WIDOW;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.StateMode.START_WIDOW:
				this.StartWindow();
				this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.WAIT_END_WIDOW;
				break;
			case ui_mm_mileage_page.SimpleEvent.StateMode.WAIT_END_WIDOW:
				if (this.UpdateWindow())
				{
					this.m_stateMode = ui_mm_mileage_page.SimpleEvent.StateMode.END;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.StateMode.END:
				if (this.m_buttonEventResourceLoader != null)
				{
					UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
					this.m_buttonEventResourceLoader = null;
				}
				base.isEnd = true;
				break;
			}
			return base.isEnd;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000CE94C File Offset: 0x000CCB4C
		public void SetWindowType()
		{
			if (this.m_serverItem.idType == ServerItem.IdType.CHARA)
			{
				if (this.m_serverItem.charaType != CharaType.UNKNOWN)
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (playerState != null)
					{
						ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_serverItem.charaType);
						if (serverCharacterState != null && serverCharacterState.star > 0)
						{
							this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_LEVEL_UP;
						}
						else
						{
							this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_GET;
						}
					}
				}
			}
			else if (this.m_serverItem.idType == ServerItem.IdType.CHAO)
			{
				DataTable.ChaoData chaoData = ChaoTable.GetChaoData(this.m_serverItem.chaoId);
				if (chaoData != null)
				{
					if (chaoData.level > 0)
					{
						this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_LEVEL_UP;
					}
					else if (chaoData.rarity == DataTable.ChaoData.Rarity.NORMAL || chaoData.rarity == DataTable.ChaoData.Rarity.RARE)
					{
						this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET;
					}
					else
					{
						this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET_SRARE;
					}
				}
			}
			else
			{
				this.m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.ITEM;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000CEA3C File Offset: 0x000CCC3C
		public void RequestLoadWindow()
		{
			string text = string.Empty;
			switch (this.m_windowType)
			{
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_GET:
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_LEVEL_UP:
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET:
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET_SRARE:
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_LEVEL_UP:
				text = "ChaoWindows";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
				this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync(text, new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000CEAC0 File Offset: 0x000CCCC0
		public void StartWindow()
		{
			if (this.rewardType != RewardType.NONE && this.count > 0)
			{
				GameObject parent = GameObject.Find("UI Root (2D)");
				switch (this.m_windowType)
				{
				case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_GET:
					this.m_charaGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(parent, "ro_PlayerGetWindowUI");
					if (this.m_charaGetWindow != null)
					{
						PlayerGetPartsOverlap playerGetPartsOverlap = this.m_charaGetWindow.gameObject.GetComponent<PlayerGetPartsOverlap>();
						if (playerGetPartsOverlap == null)
						{
							playerGetPartsOverlap = this.m_charaGetWindow.gameObject.AddComponent<PlayerGetPartsOverlap>();
						}
						playerGetPartsOverlap.Init((int)this.m_serverItem.id, 100, 0, null, PlayerGetPartsOverlap.IntroType.NO_EGG);
						ChaoGetPartsBase chaoGetParts = playerGetPartsOverlap;
						bool isTutorial = false;
						this.m_charaGetWindow.PlayStart(chaoGetParts, isTutorial, true, RouletteUtility.AchievementType.NONE);
					}
					break;
				case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_LEVEL_UP:
					this.m_playerMergeWindow = GameObjectUtil.FindChildGameObjectComponent<PlayerMergeWindow>(parent, "player_merge_Window");
					if (this.m_playerMergeWindow != null)
					{
						this.m_playerMergeWindow.PlayStart((int)this.m_serverItem.id, RouletteUtility.AchievementType.PlayerGet);
					}
					break;
				case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET:
				{
					DataTable.ChaoData chaoData = ChaoTable.GetChaoData(this.m_serverItem.chaoId);
					this.m_chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(parent, "chao_get_Window");
					ChaoGetPartsRare component = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsRare>();
					ChaoGetPartsNormal chaoGetPartsNormal = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsNormal>();
					if (component != null)
					{
						UnityEngine.Object.Destroy(component);
					}
					if (chaoGetPartsNormal == null)
					{
						chaoGetPartsNormal = this.m_chaoGetWindow.gameObject.AddComponent<ChaoGetPartsNormal>();
					}
					chaoGetPartsNormal.Init((int)this.m_serverItem.id, (int)chaoData.rarity);
					this.m_chaoGetWindow.PlayStart(chaoGetPartsNormal, false, true, RouletteUtility.AchievementType.NONE);
					break;
				}
				case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET_SRARE:
				{
					DataTable.ChaoData chaoData2 = ChaoTable.GetChaoData(this.m_serverItem.chaoId);
					this.m_chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(parent, "chao_rare_get_Window");
					ChaoGetPartsNormal component2 = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsNormal>();
					ChaoGetPartsRare chaoGetPartsRare = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsRare>();
					if (component2 != null)
					{
						UnityEngine.Object.Destroy(component2);
					}
					if (chaoGetPartsRare == null)
					{
						chaoGetPartsRare = this.m_chaoGetWindow.gameObject.AddComponent<ChaoGetPartsRare>();
					}
					chaoGetPartsRare.Init((int)this.m_serverItem.id, (int)chaoData2.rarity);
					this.m_chaoGetWindow.PlayStart(chaoGetPartsRare, false, true, RouletteUtility.AchievementType.NONE);
					break;
				}
				case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_LEVEL_UP:
				{
					DataTable.ChaoData chaoData3 = ChaoTable.GetChaoData(this.m_serverItem.chaoId);
					if (this.m_chaoMergeWindow == null)
					{
						this.m_chaoMergeWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoMergeWindow>(parent, "chao_merge_Window");
					}
					this.m_chaoMergeWindow.PlayStart((int)this.m_serverItem.id, chaoData3.level, (int)chaoData3.rarity, RouletteUtility.AchievementType.NONE);
					break;
				}
				case ui_mm_mileage_page.SimpleEvent.WindowType.ITEM:
				{
					ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
					if (itemGetWindow != null)
					{
						string text = MileageMapUtility.GetText("gw_item_text", new Dictionary<string, string>
						{
							{
								"{COUNT}",
								HudUtility.GetFormatNumString<int>(this.count)
							}
						});
						itemGetWindow.Create(new ItemGetWindow.CInfo
						{
							caption = this.title,
							serverItemId = this.serverItemId,
							imageCount = text
						});
					}
					break;
				}
				}
				if (!this.disableSe)
				{
					SoundManager.SePlay("sys_roulette_itemget", "SE");
				}
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000CEE1C File Offset: 0x000CD01C
		public bool UpdateWindow()
		{
			bool result = false;
			switch (this.m_windowType)
			{
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_GET:
				if (this.m_charaGetWindow != null && this.m_charaGetWindow.IsPlayEnd)
				{
					result = true;
					this.m_charaGetWindow = null;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHARA_LEVEL_UP:
				if (this.m_playerMergeWindow != null && this.m_playerMergeWindow.IsPlayEnd)
				{
					result = true;
					this.m_playerMergeWindow = null;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET:
				if (this.m_chaoGetWindow != null && this.m_chaoGetWindow.IsPlayEnd)
				{
					result = true;
					this.m_chaoGetWindow = null;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_GET_SRARE:
				if (this.m_chaoGetWindow != null && this.m_chaoGetWindow.IsPlayEnd)
				{
					result = true;
					this.m_chaoGetWindow = null;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.WindowType.CHAO_LEVEL_UP:
				if (this.m_chaoMergeWindow != null && this.m_chaoMergeWindow.IsPlayEnd)
				{
					result = true;
					this.m_chaoMergeWindow = null;
				}
				break;
			case ui_mm_mileage_page.SimpleEvent.WindowType.ITEM:
			{
				ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
				if (itemGetWindow != null && itemGetWindow.IsEnd)
				{
					itemGetWindow.Reset();
					result = true;
				}
				break;
			}
			default:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x04001ED7 RID: 7895
		private ServerItem m_serverItem;

		// Token: 0x04001ED8 RID: 7896
		private ChaoGetWindow m_charaGetWindow;

		// Token: 0x04001ED9 RID: 7897
		private ChaoGetWindow m_chaoGetWindow;

		// Token: 0x04001EDA RID: 7898
		private ChaoMergeWindow m_chaoMergeWindow;

		// Token: 0x04001EDB RID: 7899
		private PlayerMergeWindow m_playerMergeWindow;

		// Token: 0x04001EDC RID: 7900
		private ButtonEventResourceLoader m_buttonEventResourceLoader;

		// Token: 0x04001EDD RID: 7901
		private ui_mm_mileage_page.SimpleEvent.WindowType m_windowType = ui_mm_mileage_page.SimpleEvent.WindowType.UNKNOWN;

		// Token: 0x04001EDE RID: 7902
		private ui_mm_mileage_page.SimpleEvent.StateMode m_stateMode;

		// Token: 0x02000472 RID: 1138
		private enum WindowType
		{
			// Token: 0x04001EE5 RID: 7909
			CHARA_GET,
			// Token: 0x04001EE6 RID: 7910
			CHARA_LEVEL_UP,
			// Token: 0x04001EE7 RID: 7911
			CHAO_GET,
			// Token: 0x04001EE8 RID: 7912
			CHAO_GET_SRARE,
			// Token: 0x04001EE9 RID: 7913
			CHAO_LEVEL_UP,
			// Token: 0x04001EEA RID: 7914
			ITEM,
			// Token: 0x04001EEB RID: 7915
			UNKNOWN
		}

		// Token: 0x02000473 RID: 1139
		private enum StateMode
		{
			// Token: 0x04001EED RID: 7917
			SET_WINDOW_TYPE,
			// Token: 0x04001EEE RID: 7918
			REQUEST_LOAD,
			// Token: 0x04001EEF RID: 7919
			WAIT_LOAD,
			// Token: 0x04001EF0 RID: 7920
			START_WIDOW,
			// Token: 0x04001EF1 RID: 7921
			WAIT_END_WIDOW,
			// Token: 0x04001EF2 RID: 7922
			END
		}
	}

	// Token: 0x02000474 RID: 1140
	private class GorgeousEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000CEF74 File Offset: 0x000CD174
		public GorgeousEvent(GameObject gameObject, int windowId, bool isNotPlaybackDefaultBgm = false) : base(gameObject)
		{
			this.windowId = windowId;
			this.isNotPlaybackDefaultBgm = isNotPlaybackDefaultBgm;
			this.m_notAllSkip = false;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000CEF94 File Offset: 0x000CD194
		public GorgeousEvent(GameObject gameObject, int windowId, bool isNotPlaybackDefaultBgm, bool notAllSkip) : base(gameObject)
		{
			this.windowId = windowId;
			this.isNotPlaybackDefaultBgm = isNotPlaybackDefaultBgm;
			this.m_notAllSkip = notAllSkip;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x000CEFB4 File Offset: 0x000CD1B4
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x000CEFBC File Offset: 0x000CD1BC
		public int windowId { get; private set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000CEFC8 File Offset: 0x000CD1C8
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x000CEFD0 File Offset: 0x000CD1D0
		private bool isNotPlaybackDefaultBgm { get; set; }

		// Token: 0x0600225D RID: 8797 RVA: 0x000CEFDC File Offset: 0x000CD1DC
		public override void Start()
		{
			MileageMapData mileageMapData = MileageMapDataManager.Instance.GetMileageMapData();
			if (mileageMapData == null)
			{
				return;
			}
			if (this.windowId >= mileageMapData.window_data.Length)
			{
				return;
			}
			WindowEventData windowEventData = mileageMapData.window_data[this.windowId];
			GeneralWindow.CInfo.Event[] array = new GeneralWindow.CInfo.Event[windowEventData.body.Length];
			for (int i = 0; i < windowEventData.body.Length; i++)
			{
				WindowBodyData windowBodyData = windowEventData.body[i];
				GeneralWindow.CInfo.Event.FaceWindow[] array2 = new GeneralWindow.CInfo.Event.FaceWindow[windowBodyData.product.Length];
				for (int j = 0; j < windowBodyData.product.Length; j++)
				{
					WindowProductData windowProductData = windowBodyData.product[j];
					array2[j] = new GeneralWindow.CInfo.Event.FaceWindow
					{
						texture = MileageMapUtility.GetFaceTexture(windowProductData.face_id),
						name = ((windowProductData.name_cell_id == null) ? null : MileageMapText.GetName(windowProductData.name_cell_id)),
						effectType = windowProductData.effect,
						animType = windowProductData.anim,
						reverseType = windowProductData.reverse,
						showingType = windowProductData.showing
					};
				}
				array[i] = new GeneralWindow.CInfo.Event
				{
					faceWindows = array2,
					arrowType = windowBodyData.arrow,
					bgmCueName = windowBodyData.bgm,
					seCueName = windowBodyData.se,
					message = MileageMapText.GetText(mileageMapData.scenario.episode, windowBodyData.text_cell_id)
				};
			}
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				buttonType = GeneralWindow.ButtonType.OkNextSkipAllSkip,
				caption = MileageMapText.GetText(mileageMapData.scenario.episode, windowEventData.title_cell_id),
				events = array,
				isNotPlaybackDefaultBgm = this.isNotPlaybackDefaultBgm
			});
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000CF1B8 File Offset: 0x000CD3B8
		public override bool Update()
		{
			if (GeneralWindow.IsButtonPressed)
			{
				if (GeneralWindow.IsAllSkipButtonPressed && !this.m_notAllSkip)
				{
					base.mileage_page.OnClickAllSkipBtn();
				}
				GeneralWindow.Close();
				base.isEnd = true;
			}
			return base.isEnd;
		}

		// Token: 0x04001EF3 RID: 7923
		private bool m_notAllSkip;
	}

	// Token: 0x02000475 RID: 1141
	private class BalloonEffectEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x000CF204 File Offset: 0x000CD404
		public BalloonEffectEvent(GameObject gameObject, int waypointIndex) : base(gameObject)
		{
			this.waypointIndex = waypointIndex;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000CF214 File Offset: 0x000CD414
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x000CF21C File Offset: 0x000CD41C
		private int waypointIndex { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000CF228 File Offset: 0x000CD428
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x000CF230 File Offset: 0x000CD430
		private float time { get; set; }

		// Token: 0x06002264 RID: 8804 RVA: 0x000CF23C File Offset: 0x000CD43C
		public override void Start()
		{
			this.SetEffectActive(false);
			this.SetEffectActive(true);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000CF24C File Offset: 0x000CD44C
		public override bool Update()
		{
			float time = this.time;
			this.time += Time.deltaTime;
			if ((time < 0.5f && this.time >= 0.5f) || this.waypointIndex < 1)
			{
				this.SetEffectActive(false);
				base.isEnd = true;
			}
			return base.isEnd;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000CF2B0 File Offset: 0x000CD4B0
		private void SetEffectActive(bool isActive)
		{
			if (this.waypointIndex >= 1)
			{
				GameObject effectGameObject = base.mileage_page.m_balloonsObjects[this.waypointIndex - 1].m_effectGameObject;
				if (effectGameObject != null)
				{
					effectGameObject.SetActive(isActive);
				}
			}
		}
	}

	// Token: 0x02000476 RID: 1142
	private class BalloonEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002267 RID: 8807 RVA: 0x000CF2F8 File Offset: 0x000CD4F8
		public BalloonEvent(GameObject gameObject, int eventIndex, int newFaceId, int oldFaceId) : base(gameObject)
		{
			this.eventIndex = eventIndex;
			this.newFaceId = newFaceId;
			this.oldFaceId = oldFaceId;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000CF324 File Offset: 0x000CD524
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x000CF32C File Offset: 0x000CD52C
		private int eventIndex { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000CF338 File Offset: 0x000CD538
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x000CF340 File Offset: 0x000CD540
		private int newFaceId { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000CF34C File Offset: 0x000CD54C
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x000CF354 File Offset: 0x000CD554
		private int oldFaceId { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000CF360 File Offset: 0x000CD560
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x000CF368 File Offset: 0x000CD568
		private float time { get; set; }

		// Token: 0x06002270 RID: 8816 RVA: 0x000CF374 File Offset: 0x000CD574
		public override void Start()
		{
			this.time = 0f;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000CF384 File Offset: 0x000CD584
		public override bool Update()
		{
			float time = this.time;
			this.time += Time.deltaTime;
			if (time < 0.01f && this.time >= 0.01f)
			{
				this.SkipMileageProcess();
			}
			if (this.time >= 1f)
			{
				base.isEnd = true;
			}
			return base.isEnd;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000CF3E8 File Offset: 0x000CD5E8
		public override void SkipMileageProcess()
		{
			if (base.mileage_page != null)
			{
				base.mileage_page.SetBalloonFaceTexture(this.eventIndex, this.newFaceId);
			}
		}
	}

	// Token: 0x02000477 RID: 1143
	private class BgmEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x000CF420 File Offset: 0x000CD620
		public BgmEvent(GameObject gameObject, string cueName) : base(gameObject)
		{
			this.m_cueName = cueName;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000CF430 File Offset: 0x000CD630
		public override void Start()
		{
			if (string.IsNullOrEmpty(this.m_cueName))
			{
				SoundManager.BgmFadeOut(0.5f);
				base.isEnd = true;
			}
			else
			{
				SoundManager.BgmFadeOut(0.5f);
				this.m_playBgmFlag = true;
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000CF46C File Offset: 0x000CD66C
		public override bool Update()
		{
			if (this.m_playBgmFlag)
			{
				this.m_waitTime += Time.deltaTime;
				if (this.m_waitTime > 0.5f)
				{
					SoundManager.BgmStop();
					SoundManager.BgmPlay(this.m_cueName, "BGM", false);
					base.isEnd = true;
				}
			}
			return base.isEnd;
		}

		// Token: 0x04001EFC RID: 7932
		private string m_cueName;

		// Token: 0x04001EFD RID: 7933
		private float m_waitTime;

		// Token: 0x04001EFE RID: 7934
		private bool m_playBgmFlag;
	}

	// Token: 0x02000478 RID: 1144
	private class MapEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x000CF4CC File Offset: 0x000CD6CC
		public MapEvent(GameObject gameObject, int episode, int chapter) : base(gameObject)
		{
			this.episode = episode;
			this.chapter = chapter;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000CF4E4 File Offset: 0x000CD6E4
		public MapEvent(GameObject gameObject) : base(gameObject)
		{
			this.episode = -1;
			this.chapter = -1;
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000CF4FC File Offset: 0x000CD6FC
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x000CF504 File Offset: 0x000CD704
		public int episode { get; private set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x000CF510 File Offset: 0x000CD710
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x000CF518 File Offset: 0x000CD718
		public int chapter { get; private set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000CF524 File Offset: 0x000CD724
		public bool isNext
		{
			get
			{
				return this.episode == -1 || this.chapter == -1;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000CF540 File Offset: 0x000CD740
		public override void Start()
		{
			this.SkipMileageProcess();
			base.isEnd = true;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000CF550 File Offset: 0x000CD750
		public override bool Update()
		{
			return base.isEnd;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000CF558 File Offset: 0x000CD758
		public override void SkipMileageProcess()
		{
			SoundManager.BgmChange("bgm_sys_menu", "BGM");
			MileageMapDataManager.Instance.SetCurrentData(base.mileage_page.m_mapInfo.m_resultData.m_newMapState.m_episode, base.mileage_page.m_mapInfo.m_resultData.m_newMapState.m_chapter);
			base.mileage_page.m_mapInfo.m_resultData.m_oldMapState.m_episode = base.mileage_page.m_mapInfo.m_resultData.m_newMapState.m_episode;
			base.mileage_page.m_mapInfo.m_resultData.m_oldMapState.m_chapter = base.mileage_page.m_mapInfo.m_resultData.m_newMapState.m_chapter;
			ResultData resultData = base.mileage_page.m_mapInfo.m_resultData;
			base.mileage_page.m_mapInfo = new ui_mm_mileage_page.MapInfo();
			base.mileage_page.m_mapInfo.m_resultData = resultData;
			base.mileage_page.m_mapInfo.isNextMileage = true;
			base.mileage_page.m_mapInfo.ResetMileageIncentive();
			base.mileage_page.SetBG();
			base.mileage_page.SetAll();
			SaveDataManager.Instance.PlayerData.RankOffset = 0;
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		}
	}

	// Token: 0x02000479 RID: 1145
	private class HighscoreEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002280 RID: 8832 RVA: 0x000CF698 File Offset: 0x000CD898
		public HighscoreEvent(GameObject gameObject, long highscore) : base(gameObject)
		{
			this.highscore = highscore;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x000CF6B0 File Offset: 0x000CD8B0
		public long highscore { get; private set; }

		// Token: 0x06002283 RID: 8835 RVA: 0x000CF6BC File Offset: 0x000CD8BC
		private void NextPhase()
		{
			this.m_phase++;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000CF6CC File Offset: 0x000CD8CC
		public override bool Update()
		{
			switch (this.m_phase)
			{
			case ui_mm_mileage_page.HighscoreEvent.Phase.Init:
				if (base.IsAskSnsFeed())
				{
					this.NextPhase();
				}
				else
				{
					base.isEnd = true;
				}
				break;
			case ui_mm_mileage_page.HighscoreEvent.Phase.InitAskWindow:
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					buttonType = GeneralWindow.ButtonType.TweetCancel,
					caption = MileageMapUtility.GetText("gw_highscore_caption", null),
					message = MileageMapUtility.GetText("gw_highscore_text", null)
				});
				this.NextPhase();
				break;
			case ui_mm_mileage_page.HighscoreEvent.Phase.UpdateAskWindow:
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						this.m_phase = ui_mm_mileage_page.HighscoreEvent.Phase.InitSnsFeed;
					}
					else
					{
						this.m_phase = ui_mm_mileage_page.HighscoreEvent.Phase.Term;
						base.SetDisableAskSnsFeed();
					}
					GeneralWindow.Close();
				}
				break;
			case ui_mm_mileage_page.HighscoreEvent.Phase.InitSnsFeed:
				this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/menu_Anim/ui_mm_mileage2_page/Anchor_5_MC", MileageMapUtility.GetText("feed_highscore_caption", null), MileageMapUtility.GetText("feed_highscore_text", new Dictionary<string, string>
				{
					{
						"{HIGHSCORE}",
						this.highscore.ToString()
					}
				}), null);
				this.NextPhase();
				break;
			case ui_mm_mileage_page.HighscoreEvent.Phase.UpdateSnsFeed:
			{
				EasySnsFeed.Result result = this.m_easySnsFeed.Update();
				if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
				{
					this.NextPhase();
				}
				break;
			}
			case ui_mm_mileage_page.HighscoreEvent.Phase.Term:
				base.isEnd = true;
				break;
			}
			return base.isEnd;
		}

		// Token: 0x04001F01 RID: 7937
		private ui_mm_mileage_page.HighscoreEvent.Phase m_phase;

		// Token: 0x04001F02 RID: 7938
		private EasySnsFeed m_easySnsFeed;

		// Token: 0x0200047A RID: 1146
		private enum Phase
		{
			// Token: 0x04001F05 RID: 7941
			Init,
			// Token: 0x04001F06 RID: 7942
			InitAskWindow,
			// Token: 0x04001F07 RID: 7943
			UpdateAskWindow,
			// Token: 0x04001F08 RID: 7944
			InitSnsFeed,
			// Token: 0x04001F09 RID: 7945
			UpdateSnsFeed,
			// Token: 0x04001F0A RID: 7946
			Term
		}
	}

	// Token: 0x0200047B RID: 1147
	private class RankingUPEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x000CF840 File Offset: 0x000CDA40
		public RankingUPEvent(GameObject gameObject) : base(gameObject)
		{
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000CF850 File Offset: 0x000CDA50
		public override void Start()
		{
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("RankingResultBitWindow", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.WAIT_LOAD;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000CF894 File Offset: 0x000CDA94
		public void ResourceLoadEndCallback()
		{
			if (FontManager.Instance != null)
			{
				FontManager.Instance.ReplaceFont();
			}
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplaceAtlasForMenu(true);
			}
			this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.START_WIDOW;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000CF8E0 File Offset: 0x000CDAE0
		public override bool Update()
		{
			switch (this.m_stateMode)
			{
			case ui_mm_mileage_page.RankingUPEvent.StateMode.WAIT_LOAD:
				if (this.m_buttonEventResourceLoader != null && this.m_buttonEventResourceLoader.IsLoaded)
				{
					this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.START_WIDOW;
				}
				break;
			case ui_mm_mileage_page.RankingUPEvent.StateMode.START_WIDOW:
				if (RankingUtil.ShowRankingChangeWindow(RankingUtil.RankingMode.ENDLESS))
				{
					this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.WAIT_END_WIDOW;
				}
				else
				{
					this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.END;
				}
				break;
			case ui_mm_mileage_page.RankingUPEvent.StateMode.WAIT_END_WIDOW:
				if (RankingUtil.IsEndRankingChangeWindow())
				{
					this.m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.END;
				}
				break;
			case ui_mm_mileage_page.RankingUPEvent.StateMode.END:
				if (this.m_buttonEventResourceLoader != null)
				{
					UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
					this.m_buttonEventResourceLoader = null;
				}
				base.isEnd = true;
				break;
			}
			return base.isEnd;
		}

		// Token: 0x04001F0B RID: 7947
		private ui_mm_mileage_page.RankingUPEvent.StateMode m_stateMode = ui_mm_mileage_page.RankingUPEvent.StateMode.UNKNOWN;

		// Token: 0x04001F0C RID: 7948
		private ButtonEventResourceLoader m_buttonEventResourceLoader;

		// Token: 0x0200047C RID: 1148
		private enum StateMode
		{
			// Token: 0x04001F0E RID: 7950
			WAIT_LOAD,
			// Token: 0x04001F0F RID: 7951
			START_WIDOW,
			// Token: 0x04001F10 RID: 7952
			WAIT_END_WIDOW,
			// Token: 0x04001F11 RID: 7953
			END,
			// Token: 0x04001F12 RID: 7954
			UNKNOWN
		}
	}

	// Token: 0x0200047D RID: 1149
	private class RankUpEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x000CF9AC File Offset: 0x000CDBAC
		public RankUpEvent(GameObject gameObject) : base(gameObject)
		{
			this.rank = (int)SaveDataManager.Instance.PlayerData.Rank;
			FoxManager.SendLtvPointMap(this.rank);
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000CF9E0 File Offset: 0x000CDBE0
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x000CF9E8 File Offset: 0x000CDBE8
		public int rank { get; private set; }

		// Token: 0x0600228C RID: 8844 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
		private void NextPhase()
		{
			this.m_phase++;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000CFA04 File Offset: 0x000CDC04
		public override bool Update()
		{
			switch (this.m_phase)
			{
			case ui_mm_mileage_page.RankUpEvent.Phase.Init:
				this.m_askSns = base.IsAskSnsFeed();
				this.m_rankUpObj = GameObjectUtil.FindChildGameObject(base.gameObject.transform.root.gameObject, "Mileage_rankup");
				if (this.m_rankUpObj != null)
				{
					this.m_rankUpObj.SetActive(true);
					GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_rankUpObj, "eff_set");
					if (gameObject != null)
					{
						gameObject.SetActive(true);
					}
					Animation component = this.m_rankUpObj.GetComponent<Animation>();
					if (component != null)
					{
						ActiveAnimation.Play(component, "ui_mileage_rankup_Anim", Direction.Forward);
					}
					SoundManager.SePlay("sys_rank_up", "SE");
				}
				this.m_waitTimer = 2.3f;
				this.NextPhase();
				break;
			case ui_mm_mileage_page.RankUpEvent.Phase.WaitProduction:
				this.m_waitTimer -= Time.deltaTime;
				if (this.m_waitTimer < 0f)
				{
					this.m_phase = ui_mm_mileage_page.RankUpEvent.Phase.InitAskWindow;
				}
				break;
			case ui_mm_mileage_page.RankUpEvent.Phase.InitAskWindow:
			{
				string cellName = (!this.m_askSns) ? "gw_rankup_text_without_post" : "gw_rankup_text";
				GeneralWindow.ButtonType buttonType = (!this.m_askSns) ? GeneralWindow.ButtonType.Ok : GeneralWindow.ButtonType.TweetCancel;
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					buttonType = buttonType,
					caption = MileageMapUtility.GetText("gw_rankup_caption", null),
					message = MileageMapUtility.GetText(cellName, new Dictionary<string, string>
					{
						{
							"{RANK}",
							(this.rank + 1).ToString()
						}
					})
				});
				this.NextPhase();
				break;
			}
			case ui_mm_mileage_page.RankUpEvent.Phase.UpdateAskWindow:
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						this.m_phase = ui_mm_mileage_page.RankUpEvent.Phase.InitSnsFeed;
					}
					else
					{
						this.m_phase = ui_mm_mileage_page.RankUpEvent.Phase.Term;
						if (this.m_askSns)
						{
							base.SetDisableAskSnsFeed();
						}
					}
					GeneralWindow.Close();
				}
				break;
			case ui_mm_mileage_page.RankUpEvent.Phase.InitSnsFeed:
				this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/menu_Anim/ui_mm_mileage2_page/Anchor_5_MC", MileageMapUtility.GetText("feed_rankup_caption", null), MileageMapUtility.GetText("feed_rankup_text", new Dictionary<string, string>
				{
					{
						"{RANK}",
						(this.rank + 1).ToString()
					}
				}), null);
				this.NextPhase();
				break;
			case ui_mm_mileage_page.RankUpEvent.Phase.UpdateSnsFeed:
			{
				EasySnsFeed.Result result = this.m_easySnsFeed.Update();
				if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
				{
					this.NextPhase();
				}
				break;
			}
			case ui_mm_mileage_page.RankUpEvent.Phase.Term:
				if (this.m_rankUpObj != null)
				{
					UnityEngine.Object.Destroy(this.m_rankUpObj);
				}
				base.isEnd = true;
				break;
			}
			return base.isEnd;
		}

		// Token: 0x04001F13 RID: 7955
		private ui_mm_mileage_page.RankUpEvent.Phase m_phase;

		// Token: 0x04001F14 RID: 7956
		private EasySnsFeed m_easySnsFeed;

		// Token: 0x04001F15 RID: 7957
		private GameObject m_rankUpObj;

		// Token: 0x04001F16 RID: 7958
		private float m_waitTimer;

		// Token: 0x04001F17 RID: 7959
		private bool m_askSns;

		// Token: 0x0200047E RID: 1150
		private enum Phase
		{
			// Token: 0x04001F1A RID: 7962
			Init,
			// Token: 0x04001F1B RID: 7963
			WaitProduction,
			// Token: 0x04001F1C RID: 7964
			InitAskWindow,
			// Token: 0x04001F1D RID: 7965
			UpdateAskWindow,
			// Token: 0x04001F1E RID: 7966
			InitSnsFeed,
			// Token: 0x04001F1F RID: 7967
			UpdateSnsFeed,
			// Token: 0x04001F20 RID: 7968
			Term
		}
	}

	// Token: 0x0200047F RID: 1151
	private class DailyMissionEvent : ui_mm_mileage_page.BaseEvent
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x000CFCC4 File Offset: 0x000CDEC4
		public DailyMissionEvent(GameObject gameObject) : base(gameObject)
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000CFCD4 File Offset: 0x000CDED4
		public override void Start()
		{
			this.m_buttonEventResourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
			this.m_buttonEventResourceLoader.LoadResourceIfNotLoadedAsync("DailyWindowUI", new ButtonEventResourceLoader.CallbackIfNotLoaded(this.ResourceLoadEndCallback));
			this.m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.WAIT_LOAD;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000CFD18 File Offset: 0x000CDF18
		public void ResourceLoadEndCallback()
		{
			if (FontManager.Instance != null)
			{
				FontManager.Instance.ReplaceFont();
			}
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplaceAtlasForMenu(true);
			}
			this.m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.START_WIDOW;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000CFD64 File Offset: 0x000CDF64
		public override bool Update()
		{
			switch (this.m_stateMode)
			{
			case ui_mm_mileage_page.DailyMissionEvent.StateMode.WAIT_LOAD:
				if (this.m_buttonEventResourceLoader != null && this.m_buttonEventResourceLoader.IsLoaded)
				{
					this.m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.START_WIDOW;
				}
				break;
			case ui_mm_mileage_page.DailyMissionEvent.StateMode.START_WIDOW:
			{
				GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
				if (menuAnimUIObject != null)
				{
					this.m_dailyWindowUI = GameObjectUtil.FindChildGameObjectComponent<DailyWindowUI>(menuAnimUIObject, "DailyWindowUI");
					if (this.m_dailyWindowUI != null)
					{
						this.m_dailyWindowUI.gameObject.SetActive(true);
						this.m_dailyWindowUI.PlayStart();
					}
				}
				this.m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.WAIT_END_WIDOW;
				break;
			}
			case ui_mm_mileage_page.DailyMissionEvent.StateMode.WAIT_END_WIDOW:
				if (this.m_dailyWindowUI != null && this.m_dailyWindowUI.IsEnd)
				{
					this.m_dailyWindowUI.gameObject.SetActive(false);
					this.m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.END;
				}
				break;
			case ui_mm_mileage_page.DailyMissionEvent.StateMode.END:
				if (this.m_buttonEventResourceLoader != null)
				{
					UnityEngine.Object.Destroy(this.m_buttonEventResourceLoader);
					this.m_buttonEventResourceLoader = null;
				}
				base.isEnd = true;
				break;
			}
			return base.isEnd;
		}

		// Token: 0x04001F21 RID: 7969
		private ui_mm_mileage_page.DailyMissionEvent.StateMode m_stateMode = ui_mm_mileage_page.DailyMissionEvent.StateMode.UNKNOWN;

		// Token: 0x04001F22 RID: 7970
		private ButtonEventResourceLoader m_buttonEventResourceLoader;

		// Token: 0x04001F23 RID: 7971
		private DailyWindowUI m_dailyWindowUI;

		// Token: 0x02000480 RID: 1152
		private enum StateMode
		{
			// Token: 0x04001F25 RID: 7973
			WAIT_LOAD,
			// Token: 0x04001F26 RID: 7974
			START_WIDOW,
			// Token: 0x04001F27 RID: 7975
			WAIT_END_WIDOW,
			// Token: 0x04001F28 RID: 7976
			END,
			// Token: 0x04001F29 RID: 7977
			UNKNOWN
		}
	}

	// Token: 0x02000481 RID: 1153
	private class MapInfo
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x000CFE90 File Offset: 0x000CE090
		public MapInfo()
		{
			this.SetRoutesInfo();
			this.highscore = -1L;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000CFEA8 File Offset: 0x000CE0A8
		public static int routeScoreDistance
		{
			get
			{
				return MileageMapDataManager.Instance.GetMileageMapData().map_data.event_interval;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x000CFEC0 File Offset: 0x000CE0C0
		public static int stageScoreDistance
		{
			get
			{
				return ui_mm_mileage_page.MapInfo.routeScoreDistance * 5;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000CFECC File Offset: 0x000CE0CC
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x000CFED4 File Offset: 0x000CE0D4
		public int waypointIndex
		{
			get
			{
				return this.m_waypointIndex;
			}
			set
			{
				this.m_waypointIndex = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000CFEE0 File Offset: 0x000CE0E0
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x000CFEE8 File Offset: 0x000CE0E8
		public double scoreDistanceRaw
		{
			get
			{
				return this.m_scoreDistanceRaw;
			}
			set
			{
				this.m_scoreDistanceRaw = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000CFEF4 File Offset: 0x000CE0F4
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x000CFEFC File Offset: 0x000CE0FC
		public double scoreDistance
		{
			get
			{
				return this.m_scoreDistance;
			}
			set
			{
				this.m_scoreDistance = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000CFF08 File Offset: 0x000CE108
		// (set) Token: 0x0600229C RID: 8860 RVA: 0x000CFF10 File Offset: 0x000CE110
		public double targetScoreDistance
		{
			get
			{
				return this.m_targetScoreDistance;
			}
			set
			{
				this.m_targetScoreDistance = value;
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000CFF1C File Offset: 0x000CE11C
		public double GetRunDistance()
		{
			double result = 0.0;
			if (this.m_resultData != null && this.m_resultData.m_oldMapState != null)
			{
				result = this.scoreDistance - (double)((float)this.m_resultData.m_oldMapState.m_score);
			}
			return result;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000CFF6C File Offset: 0x000CE16C
		public int nextWaypoint
		{
			get
			{
				return Mathf.Min(this.waypointIndex + 1, 5);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000CFF7C File Offset: 0x000CE17C
		public double waypointDistance
		{
			get
			{
				return (double)(this.waypointIndex * ui_mm_mileage_page.MapInfo.routeScoreDistance);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000CFF8C File Offset: 0x000CE18C
		public double nextWaypointDistance
		{
			get
			{
				return (double)(this.nextWaypoint * ui_mm_mileage_page.MapInfo.routeScoreDistance);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000CFF9C File Offset: 0x000CE19C
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x000CFFA4 File Offset: 0x000CE1A4
		public bool isNextMileage { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x000CFFB0 File Offset: 0x000CE1B0
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x000CFFB8 File Offset: 0x000CE1B8
		public bool isBossStage { get; set; }

		// Token: 0x060022A5 RID: 8869 RVA: 0x000CFFC4 File Offset: 0x000CE1C4
		public bool IsClearMileage()
		{
			return this.m_resultData != null && this.m_resultData.m_oldMapState != null && this.m_resultData.m_newMapState != null && (this.m_resultData.m_oldMapState.m_episode != this.m_resultData.m_newMapState.m_episode || this.m_resultData.m_oldMapState.m_chapter != this.m_resultData.m_newMapState.m_chapter);
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x000D004C File Offset: 0x000CE24C
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x000D0054 File Offset: 0x000CE254
		public bool isBossDestroyed
		{
			get
			{
				return this.m_isBossDestroyed;
			}
			set
			{
				this.m_isBossDestroyed = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000D0060 File Offset: 0x000CE260
		public ui_mm_mileage_page.MapInfo.Route[] routes
		{
			get
			{
				return this.m_routes;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000D0068 File Offset: 0x000CE268
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x000D0070 File Offset: 0x000CE270
		public long highscore { get; set; }

		// Token: 0x060022AB RID: 8875 RVA: 0x000D007C File Offset: 0x000CE27C
		public void SetRoutesInfo()
		{
			this.m_routes = new ui_mm_mileage_page.MapInfo.Route[5];
			for (int i = 0; i < 5; i++)
			{
				this.m_routes[i] = new ui_mm_mileage_page.MapInfo.Route(i);
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000D00B8 File Offset: 0x000CE2B8
		public void ResetMileageIncentive()
		{
			if (this.m_resultData != null && this.m_resultData.m_mileageIncentiveList != null)
			{
				this.m_resultData.m_mileageIncentiveList.Clear();
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000D00E8 File Offset: 0x000CE2E8
		public bool CheckMileageIncentive(int point)
		{
			if (this.m_resultData != null && this.m_resultData.m_mileageIncentiveList != null)
			{
				for (int i = 0; i < this.m_resultData.m_mileageIncentiveList.Count; i++)
				{
					ServerMileageIncentive serverMileageIncentive = this.m_resultData.m_mileageIncentiveList[i];
					if (serverMileageIncentive.m_type == ServerMileageIncentive.Type.POINT && serverMileageIncentive.m_pointId == point && serverMileageIncentive.m_itemId != 0 && serverMileageIncentive.m_num > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000D0178 File Offset: 0x000CE378
		public bool UpdateFrom(ResultData resultData)
		{
			if (!resultData.m_validResult)
			{
				return false;
			}
			this.isBossStage = resultData.m_bossStage;
			this.isBossDestroyed = resultData.m_bossDestroy;
			if (resultData.m_rivalHighScore)
			{
				this.highscore = resultData.m_highScore;
			}
			this.waypointIndex = resultData.m_oldMapState.m_point;
			this.scoreDistanceRaw = (double)resultData.m_totalScore;
			this.scoreDistance = (double)Mathf.Min((float)resultData.m_oldMapState.m_score, (float)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5));
			this.targetScoreDistance = (double)Mathf.Min((float)resultData.m_newMapState.m_score, (float)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5));
			if (resultData.m_newMapState.m_episode > resultData.m_oldMapState.m_episode || (resultData.m_newMapState.m_episode == resultData.m_oldMapState.m_episode && resultData.m_newMapState.m_chapter > resultData.m_oldMapState.m_chapter))
			{
				this.targetScoreDistance = (double)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5);
			}
			this.m_resultData = resultData;
			return true;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000D0288 File Offset: 0x000CE488
		public bool UpdateFrom(MileageMapState mileageMapState)
		{
			this.waypointIndex = mileageMapState.m_point;
			this.scoreDistance = (double)Mathf.Min((float)mileageMapState.m_score, (float)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5));
			this.targetScoreDistance = (double)Mathf.Min((float)mileageMapState.m_score, (float)(ui_mm_mileage_page.MapInfo.routeScoreDistance * 5));
			return true;
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000D02D8 File Offset: 0x000CE4D8
		public ui_mm_mileage_page.MapInfo.TutorialPhase tutorialPhase
		{
			get
			{
				return ui_mm_mileage_page.MapInfo.TutorialPhase.NONE;
			}
		}

		// Token: 0x04001F2A RID: 7978
		public ResultData m_resultData;

		// Token: 0x04001F2B RID: 7979
		private int m_waypointIndex;

		// Token: 0x04001F2C RID: 7980
		private double m_scoreDistanceRaw;

		// Token: 0x04001F2D RID: 7981
		private double m_scoreDistance;

		// Token: 0x04001F2E RID: 7982
		private double m_targetScoreDistance;

		// Token: 0x04001F2F RID: 7983
		private bool m_isBossDestroyed;

		// Token: 0x04001F30 RID: 7984
		private ui_mm_mileage_page.MapInfo.Route[] m_routes;

		// Token: 0x02000482 RID: 1154
		public class Route
		{
			// Token: 0x060022B1 RID: 8881 RVA: 0x000D02DC File Offset: 0x000CE4DC
			public Route(int routeIndex)
			{
				this.routeIndex = routeIndex;
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000D02EC File Offset: 0x000CE4EC
			// (set) Token: 0x060022B3 RID: 8883 RVA: 0x000D02F4 File Offset: 0x000CE4F4
			public int routeIndex { get; private set; }
		}

		// Token: 0x02000483 RID: 1155
		public enum TutorialPhase
		{
			// Token: 0x04001F36 RID: 7990
			NONE,
			// Token: 0x04001F37 RID: 7991
			NAME_ENTRY,
			// Token: 0x04001F38 RID: 7992
			AGE_VERIFICATION,
			// Token: 0x04001F39 RID: 7993
			BEFORE_GAME,
			// Token: 0x04001F3A RID: 7994
			FIRST_EPISODE,
			// Token: 0x04001F3B RID: 7995
			FIRST_BOSS,
			// Token: 0x04001F3C RID: 7996
			FIRST_LOSE_BOSS
		}
	}

	// Token: 0x02000484 RID: 1156
	private class PointTimeLimit
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000D0308 File Offset: 0x000CE508
		public bool LimitFlag
		{
			get
			{
				return this.m_limitFlag;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000D0310 File Offset: 0x000CE510
		public bool FailedFlag
		{
			get
			{
				return this.m_failedFlag;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000D0318 File Offset: 0x000CE518
		public void Reset()
		{
			this.m_limitFlag = false;
			this.m_incentiveFlag = false;
			this.m_failedFlag = false;
			this.m_balloonObjs = null;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000D0338 File Offset: 0x000CE538
		public void SetupLimit(ServerMileageReward reward, ui_mm_mileage_page.BalloonObjects objs, bool incentiveFlag)
		{
			this.m_limitFlag = false;
			this.m_failedFlag = false;
			this.m_balloonObjs = objs;
			this.m_incentiveFlag = incentiveFlag;
			if (reward != null && reward.m_limitTime > 0)
			{
				TimeSpan value = new TimeSpan(0, 0, 0, reward.m_limitTime, 0);
				this.m_limitTime = reward.m_startTime;
				this.m_limitTime = this.m_limitTime.Add(value);
				this.m_limitFlag = true;
				if (this.m_balloonObjs != null)
				{
					if (this.m_balloonObjs.m_timerFrameObject != null)
					{
						UILabel component = this.m_balloonObjs.m_timerLimitObject.GetComponent<UILabel>();
						if (component != null)
						{
							component.enabled = !this.m_incentiveFlag;
						}
					}
					if (this.m_balloonObjs.m_timerWordObject != null)
					{
						UILabel component2 = this.m_balloonObjs.m_timerWordObject.GetComponent<UILabel>();
						if (component2 != null)
						{
							component2.enabled = !this.m_incentiveFlag;
						}
					}
				}
				this.Update();
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000D0440 File Offset: 0x000CE640
		public void Update()
		{
			if (this.m_limitFlag && !this.m_incentiveFlag && !this.m_failedFlag && this.m_balloonObjs != null)
			{
				TimeSpan restTime = this.GetRestTime(this.m_limitTime);
				if (restTime.Seconds < 0)
				{
					if (this.m_balloonObjs.m_gameObject != null)
					{
						this.m_balloonObjs.m_gameObject.SetActive(false);
					}
					this.m_failedFlag = true;
				}
				else if (this.m_balloonObjs.m_timerFrameObject != null)
				{
					UILabel component = this.m_balloonObjs.m_timerLimitObject.GetComponent<UILabel>();
					if (component != null)
					{
						component.text = this.GetRestTimeText(restTime);
					}
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000D0508 File Offset: 0x000CE708
		private TimeSpan GetRestTime(DateTime limitTime)
		{
			return limitTime - NetBase.GetCurrentTime();
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000D0518 File Offset: 0x000CE718
		private string GetRestTimeText(TimeSpan restTime)
		{
			int hours = restTime.Hours;
			int minutes = restTime.Minutes;
			int seconds = restTime.Seconds;
			return string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));
		}

		// Token: 0x04001F3D RID: 7997
		private ui_mm_mileage_page.BalloonObjects m_balloonObjs;

		// Token: 0x04001F3E RID: 7998
		private DateTime m_limitTime;

		// Token: 0x04001F3F RID: 7999
		private bool m_limitFlag;

		// Token: 0x04001F40 RID: 8000
		private bool m_failedFlag;

		// Token: 0x04001F41 RID: 8001
		private bool m_incentiveFlag;
	}

	// Token: 0x02000485 RID: 1157
	private enum PlayerAnimation
	{
		// Token: 0x04001F43 RID: 8003
		RUN,
		// Token: 0x04001F44 RID: 8004
		IDLE,
		// Token: 0x04001F45 RID: 8005
		COUNT
	}

	// Token: 0x02000486 RID: 1158
	private enum SuggestedIconType
	{
		// Token: 0x04001F47 RID: 8007
		TYPE01,
		// Token: 0x04001F48 RID: 8008
		TYPE02,
		// Token: 0x04001F49 RID: 8009
		TYPE03,
		// Token: 0x04001F4A RID: 8010
		NUM
	}

	// Token: 0x02000487 RID: 1159
	[Serializable]
	private class RouteObjects
	{
		// Token: 0x04001F4B RID: 8011
		[SerializeField]
		public UISprite m_lineSprite;

		// Token: 0x04001F4C RID: 8012
		[SerializeField]
		public GameObject m_lineEffectGameObject;

		// Token: 0x04001F4D RID: 8013
		[SerializeField]
		public GameObject m_bonusRootGameObject;

		// Token: 0x04001F4E RID: 8014
		[SerializeField]
		public UISprite m_bonusTypeSprite;

		// Token: 0x04001F4F RID: 8015
		[SerializeField]
		public UILabel m_bonusValueLabel;

		// Token: 0x04001F50 RID: 8016
		[SerializeField]
		public TweenPosition m_bonusTweenPosition;
	}

	// Token: 0x02000488 RID: 1160
	[Serializable]
	private class BalloonObjects
	{
		// Token: 0x04001F51 RID: 8017
		[SerializeField]
		public GameObject m_gameObject;

		// Token: 0x04001F52 RID: 8018
		[SerializeField]
		public UITexture m_texture;

		// Token: 0x04001F53 RID: 8019
		[SerializeField]
		public GameObject m_effectGameObject;

		// Token: 0x04001F54 RID: 8020
		[SerializeField]
		public GameObject m_normalFrameObject;

		// Token: 0x04001F55 RID: 8021
		[SerializeField]
		public GameObject m_timerFrameObject;

		// Token: 0x04001F56 RID: 8022
		[SerializeField]
		public GameObject m_timerLimitObject;

		// Token: 0x04001F57 RID: 8023
		[SerializeField]
		public GameObject m_timerWordObject;
	}

	// Token: 0x02000489 RID: 1161
	private enum DisplayType
	{
		// Token: 0x04001F59 RID: 8025
		RANK,
		// Token: 0x04001F5A RID: 8026
		RSRING,
		// Token: 0x04001F5B RID: 8027
		RING,
		// Token: 0x04001F5C RID: 8028
		NUM
	}

	// Token: 0x0200048A RID: 1162
	private enum ArraveType
	{
		// Token: 0x04001F5E RID: 8030
		POINT,
		// Token: 0x04001F5F RID: 8031
		FINISH,
		// Token: 0x04001F60 RID: 8032
		POINT_FINISH,
		// Token: 0x04001F61 RID: 8033
		RUNNIG
	}

	// Token: 0x0200048B RID: 1163
	private enum EventSignal
	{
		// Token: 0x04001F63 RID: 8035
		CLICK_NEXT = 100,
		// Token: 0x04001F64 RID: 8036
		CLICK_ALL_SKIP
	}
}
