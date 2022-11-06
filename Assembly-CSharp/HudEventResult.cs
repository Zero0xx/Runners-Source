using System;
using Text;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class HudEventResult : MonoBehaviour
{
	// Token: 0x06001AED RID: 6893 RVA: 0x0009EDF8 File Offset: 0x0009CFF8
	public bool IsBackkeyEnable()
	{
		bool result = true;
		if (this.m_parts != null)
		{
			result = this.m_parts.IsBackkeyEnable();
		}
		return result;
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0009EE28 File Offset: 0x0009D028
	// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0009EE30 File Offset: 0x0009D030
	public bool IsEndResult
	{
		get
		{
			return this.m_isEndResult;
		}
		private set
		{
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0009EE34 File Offset: 0x0009D034
	// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0009EE3C File Offset: 0x0009D03C
	public bool IsEndOutAnim
	{
		get
		{
			return this.m_isEndOutAnim;
		}
		private set
		{
		}
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0009EE40 File Offset: 0x0009D040
	public void Setup(bool eventTimeup)
	{
		if (this.m_parts != null)
		{
			UnityEngine.Object.Destroy(this.m_parts.gameObject);
			this.m_parts = null;
		}
		this.m_eventTimeup = eventTimeup;
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			instance.SetEventInfo();
		}
		HudEventResultParts component = base.gameObject.GetComponent<HudEventResultParts>();
		if (component != null)
		{
			this.m_parts = component;
			this.m_parts.Init(base.gameObject, this.m_beforeTotalPoint, new HudEventResult.AnimationEndCallback(this.EventResultAnimEndCallback));
		}
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0009EED8 File Offset: 0x0009D0D8
	public void PlayStart()
	{
		this.m_isEndResult = false;
		base.gameObject.SetActive(true);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "EventResult_Anim");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		if (this.m_eventTimeup)
		{
			this.m_state = HudEventResult.State.STATE_TIME_UP_WINDOW_START;
		}
		else
		{
			this.m_state = HudEventResult.State.STATE_RESULT_START;
		}
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x0009EF3C File Offset: 0x0009D13C
	public void PlayOutAnimation()
	{
		if (this.m_eventTimeup)
		{
			this.m_isEndOutAnim = true;
			return;
		}
		this.m_isEndOutAnim = false;
		if (this.m_parts != null)
		{
			this.m_currentAnim = HudEventResult.AnimType.OUT;
			this.m_parts.PlayAnimation(this.m_currentAnim);
		}
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0009EF8C File Offset: 0x0009D18C
	private void Start()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Result");
			if (gameObject2 != null)
			{
				Vector3 localPosition = base.gameObject.transform.localPosition;
				Vector3 localScale = base.gameObject.transform.localScale;
				base.gameObject.transform.parent = gameObject2.transform;
				base.gameObject.transform.localPosition = localPosition;
				base.gameObject.transform.localScale = localScale;
			}
		}
		base.gameObject.SetActive(false);
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			switch (instance.Type)
			{
			case EventManager.EventType.SPECIAL_STAGE:
				base.gameObject.AddComponent<HudEventResultSpStage>();
				if (instance.SpecialStageInfo != null)
				{
					this.m_beforeTotalPoint = instance.SpecialStageInfo.totalPoint;
				}
				break;
			case EventManager.EventType.RAID_BOSS:
				base.gameObject.AddComponent<HudEventResultRaidBoss>();
				if (instance.RaidBossInfo != null)
				{
					this.m_beforeTotalPoint = instance.RaidBossInfo.totalPoint;
				}
				break;
			case EventManager.EventType.COLLECT_OBJECT:
				base.gameObject.AddComponent<HudEventResultCollect>();
				if (instance.EtcEventInfo != null)
				{
					this.m_beforeTotalPoint = instance.EtcEventInfo.totalPoint;
				}
				break;
			}
		}
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0009F0F4 File Offset: 0x0009D2F4
	private void Update()
	{
		switch (this.m_state)
		{
		case HudEventResult.State.STATE_TIME_UP_WINDOW_START:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "EventTimeupResult",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "event_finished_game_result_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "event_finished_game_result").text
			});
			this.m_state = HudEventResult.State.STATE_TIME_UP_WINDOW;
			break;
		case HudEventResult.State.STATE_TIME_UP_WINDOW:
			if (GeneralWindow.IsCreated("EventTimeupResult") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.m_isEndOutAnim = true;
				this.m_state = HudEventResult.State.STATE_RESULT;
			}
			break;
		case HudEventResult.State.STATE_RESULT_START:
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "EventResult_Anim");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			if (this.m_parts != null)
			{
				this.m_currentAnim = HudEventResult.AnimType.IN;
				this.m_parts.PlayAnimation(this.m_currentAnim);
			}
			this.m_state = HudEventResult.State.STATE_RESULT;
			break;
		}
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0009F224 File Offset: 0x0009D424
	private void EventResultAnimEndCallback(HudEventResult.AnimType animType)
	{
		this.m_currentAnim = animType + 1;
		if (this.m_currentAnim == HudEventResult.AnimType.OUT_WAIT)
		{
			this.m_isEndResult = true;
			return;
		}
		if (this.m_currentAnim >= HudEventResult.AnimType.NUM)
		{
			this.m_isEndOutAnim = true;
			this.m_currentAnim = HudEventResult.AnimType.IDLE;
			return;
		}
		if (this.m_parts != null)
		{
			this.m_parts.PlayAnimation(this.m_currentAnim);
		}
	}

	// Token: 0x04001849 RID: 6217
	private HudEventResultParts m_parts;

	// Token: 0x0400184A RID: 6218
	private HudEventResult.AnimType m_currentAnim;

	// Token: 0x0400184B RID: 6219
	private bool m_isEndResult;

	// Token: 0x0400184C RID: 6220
	private bool m_isEndOutAnim;

	// Token: 0x0400184D RID: 6221
	private bool m_eventTimeup;

	// Token: 0x0400184E RID: 6222
	private long m_beforeTotalPoint;

	// Token: 0x0400184F RID: 6223
	private HudEventResult.State m_state;

	// Token: 0x0200038E RID: 910
	public enum EventType
	{
		// Token: 0x04001851 RID: 6225
		SP_STAGE,
		// Token: 0x04001852 RID: 6226
		RAID_BOSS,
		// Token: 0x04001853 RID: 6227
		ANIMAL,
		// Token: 0x04001854 RID: 6228
		NUM
	}

	// Token: 0x0200038F RID: 911
	public enum AnimType
	{
		// Token: 0x04001856 RID: 6230
		IDLE,
		// Token: 0x04001857 RID: 6231
		IN,
		// Token: 0x04001858 RID: 6232
		IN_BONUS,
		// Token: 0x04001859 RID: 6233
		WAIT_ADD_COLLECT_OBJECT,
		// Token: 0x0400185A RID: 6234
		ADD_COLLECT_OBJECT,
		// Token: 0x0400185B RID: 6235
		SHOW_QUOTA_LIST,
		// Token: 0x0400185C RID: 6236
		OUT_WAIT,
		// Token: 0x0400185D RID: 6237
		OUT,
		// Token: 0x0400185E RID: 6238
		NUM
	}

	// Token: 0x02000390 RID: 912
	private enum State
	{
		// Token: 0x04001860 RID: 6240
		STATE_IDLE,
		// Token: 0x04001861 RID: 6241
		STATE_TIME_UP_WINDOW_START,
		// Token: 0x04001862 RID: 6242
		STATE_TIME_UP_WINDOW,
		// Token: 0x04001863 RID: 6243
		STATE_RESULT_START,
		// Token: 0x04001864 RID: 6244
		STATE_RESULT,
		// Token: 0x04001865 RID: 6245
		NUM
	}

	// Token: 0x02000A82 RID: 2690
	// (Invoke) Token: 0x06004842 RID: 18498
	public delegate void AnimationEndCallback(HudEventResult.AnimType animType);
}
