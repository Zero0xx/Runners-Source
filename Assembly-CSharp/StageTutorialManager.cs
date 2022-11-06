using System;
using Message;
using Tutorial;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class StageTutorialManager : MonoBehaviour
{
	// Token: 0x0600145D RID: 5213 RVA: 0x0006D17C File Offset: 0x0006B37C
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x0006D188 File Offset: 0x0006B388
	public void SetupTutorial()
	{
		this.DebugDraw("SetupTutorial");
		this.m_tempScore = new TempTutorialScore();
		EventID id = EventID.JUMP;
		this.StartTutorial(id);
		this.m_mode = StageTutorialManager.Mode.Idle;
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0006D1BC File Offset: 0x0006B3BC
	private void StartTutorial(EventID id)
	{
		this.DebugDraw("StartTutorial id=" + id.ToString());
		this.m_currentEventID = (int)id;
		this.m_eventState = 0U;
		this.m_complete = false;
		if (this.m_currentEventID == 9)
		{
			this.CompleteTutorial();
		}
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0006D20C File Offset: 0x0006B40C
	private void CompleteTutorial()
	{
		this.DebugDraw("CompleteTutorial");
		this.m_currentEventID = 9;
		this.m_complete = true;
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0006D228 File Offset: 0x0006B428
	public void OnMsgTutorialStart(MsgTutorialStart msg)
	{
		if (this.IsCompletedTutorial())
		{
			return;
		}
		if (this.m_mode == StageTutorialManager.Mode.Idle)
		{
			this.m_startPos = msg.m_pos;
			this.m_eventState = 0U;
			this.GetTempScore();
			MsgTutorialPlayStart value = new MsgTutorialPlayStart((EventID)this.m_currentEventID);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialPlayStart", value, SendMessageOptions.DontRequireReceiver);
			this.DebugDraw("OnMsgTutorialStart m_startPos=" + this.m_startPos);
			this.m_mode = StageTutorialManager.Mode.PlayStart;
		}
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0006D2A8 File Offset: 0x0006B4A8
	public void OnMsgTutorialEnd(MsgTutorialEnd msg)
	{
		if (this.IsCompletedTutorial())
		{
			return;
		}
		if (this.m_mode == StageTutorialManager.Mode.PlayStart)
		{
			bool flag = this.CheckNextTutorial();
			Vector3 vector = (!flag) ? this.GetNextStartCollisionPosition() : this.m_startPos;
			vector -= Vector3.right * StageTutorialManager.RetryOffsetPos;
			Quaternion identity = Quaternion.identity;
			MsgTutorialPlayEnd value = new MsgTutorialPlayEnd(this.IsCompletedTutorial(), flag, (EventID)this.m_currentEventID, vector, identity);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialPlayEnd", value, SendMessageOptions.DontRequireReceiver);
			this.DebugDraw("OnMsgTutorialEnd nextStartPos=" + vector);
			this.m_mode = StageTutorialManager.Mode.PlayEnd;
		}
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0006D34C File Offset: 0x0006B54C
	public void OnMsgTutorialDebugEnd(MsgTutorialEnd msg)
	{
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0006D350 File Offset: 0x0006B550
	public void OnMsgTutorialNext(MsgTutorialNext msg)
	{
		if (this.m_mode == StageTutorialManager.Mode.PlayEnd)
		{
			if (this.IsCompletedTutorial())
			{
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnBossEnd", new MsgBossEnd(true), SendMessageOptions.DontRequireReceiver);
			}
			this.DebugDraw("OnMsgTutorialNext");
			this.m_mode = StageTutorialManager.Mode.Idle;
		}
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0006D3A0 File Offset: 0x0006B5A0
	public void OnMsgTutorialClear(MsgTutorialClear msg)
	{
		if (this.IsCompletedTutorial() && !this.IsPlayStart())
		{
			return;
		}
		foreach (MsgTutorialClear.Data data in msg.m_data)
		{
			if (data.eventid == (EventID)this.m_currentEventID)
			{
				this.DebugDraw("OnMsgTutorialClear id=" + data.eventid.ToString());
				this.SetEventState(StageTutorialManager.EventState.Clear);
			}
		}
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x0006D450 File Offset: 0x0006B650
	public void OnMsgTutorialDamage(MsgTutorialDamage msg)
	{
		if (this.IsCompletedTutorial() && !this.IsPlayStart())
		{
			return;
		}
		this.SetEventState(StageTutorialManager.EventState.Damage);
		EventClearType eventClearType = Tutorial.EventData.GetEventClearType((EventID)this.m_currentEventID);
		if (eventClearType == EventClearType.NO_DAMAGE)
		{
			this.OnMsgTutorialEnd(new MsgTutorialEnd());
		}
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x0006D49C File Offset: 0x0006B69C
	public void OnMsgTutorialMiss(MsgTutorialMiss msg)
	{
		if (this.IsCompletedTutorial() && !this.IsPlayStart())
		{
			return;
		}
		this.SetEventState(StageTutorialManager.EventState.Miss);
		this.OnMsgTutorialEnd(new MsgTutorialEnd());
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0006D4D4 File Offset: 0x0006B6D4
	public bool IsCompletedTutorial()
	{
		return this.m_complete;
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06001469 RID: 5225 RVA: 0x0006D4DC File Offset: 0x0006B6DC
	public EventID CurrentEventID
	{
		get
		{
			return (EventID)this.m_currentEventID;
		}
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x0006D4E4 File Offset: 0x0006B6E4
	private bool IsClearTutorial()
	{
		if (this.IsCompletedTutorial())
		{
			return true;
		}
		if (this.m_currentEventID == 9)
		{
			return true;
		}
		switch (Tutorial.EventData.GetEventClearType((EventID)this.m_currentEventID))
		{
		case EventClearType.CLEAR:
			if (this.IsEventState(StageTutorialManager.EventState.Clear))
			{
				return true;
			}
			break;
		case EventClearType.NO_DAMAGE:
			if (!this.IsEventState(StageTutorialManager.EventState.Miss) && !this.IsEventState(StageTutorialManager.EventState.Damage))
			{
				return true;
			}
			break;
		case EventClearType.NO_MISS:
			if (!this.IsEventState(StageTutorialManager.EventState.Miss))
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0006D578 File Offset: 0x0006B778
	private bool CheckNextTutorial()
	{
		bool result = false;
		int num = this.m_currentEventID;
		if (this.IsClearTutorial())
		{
			num++;
			if (num >= 8)
			{
				this.DebugDraw("CheckNextTutorial Complete!");
				this.CompleteTutorial();
			}
			else
			{
				this.DebugDraw("CheckNextTutorial Clear!");
				this.StartTutorial((EventID)num);
			}
		}
		else
		{
			this.DebugDraw("CheckNextTutorial Miss!");
			this.StartTutorial((EventID)num);
			result = true;
		}
		return result;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0006D5E8 File Offset: 0x0006B7E8
	private void SetEventState(StageTutorialManager.EventState state)
	{
		this.m_eventState |= (uint)state;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0006D5F8 File Offset: 0x0006B7F8
	private bool IsEventState(StageTutorialManager.EventState state)
	{
		return (this.m_eventState & (uint)state) != 0U;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0006D608 File Offset: 0x0006B808
	private bool IsPlayStart()
	{
		return this.m_mode == StageTutorialManager.Mode.PlayStart;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0006D614 File Offset: 0x0006B814
	private Vector3 GetNextStartCollisionPosition()
	{
		Vector3 vector = Vector3.zero;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Gimmick");
		foreach (GameObject gameObject in array)
		{
			if (gameObject.GetComponent<ObjTutorialStartCollision>() && this.m_startPos.x + 1f < gameObject.transform.position.x)
			{
				if (vector == Vector3.zero)
				{
					vector = gameObject.transform.position;
				}
				else if (vector.x > gameObject.transform.position.x)
				{
					vector = gameObject.transform.position;
				}
			}
		}
		if (vector == Vector3.zero)
		{
			vector = this.m_startPos;
		}
		return vector;
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0006D6F0 File Offset: 0x0006B8F0
	private void GetTempScore()
	{
		if (this.m_tempScore == null)
		{
			return;
		}
		MsgTutorialGetRingNum msgTutorialGetRingNum = new MsgTutorialGetRingNum();
		GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgTutorialGetRingNum", msgTutorialGetRingNum, SendMessageOptions.DontRequireReceiver);
		this.m_tempScore.m_ring = msgTutorialGetRingNum.m_ring;
		StageScoreManager stageScoreManager = StageScoreManager.Instance;
		if (stageScoreManager != null)
		{
			this.m_tempScore.m_stkRing = (int)stageScoreManager.Ring;
			this.m_tempScore.m_score = (int)stageScoreManager.Score;
			this.m_tempScore.m_animal = (int)stageScoreManager.Animal;
		}
		PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
		if (playerInformation != null)
		{
			this.m_tempScore.m_distance = playerInformation.TotalDistance;
		}
		this.DebugDraw(string.Concat(new object[]
		{
			"GetTempScore score=",
			this.m_tempScore.m_score,
			" ring=",
			this.m_tempScore.m_ring,
			" animal=",
			this.m_tempScore.m_animal,
			"distance=",
			this.m_tempScore.m_distance
		}));
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0006D81C File Offset: 0x0006BA1C
	public void OnMsgTutorialResetForRetry(MsgTutorialResetForRetry msg)
	{
		if (this.m_tempScore == null)
		{
			return;
		}
		MsgTutorialResetForRetry value = new MsgTutorialResetForRetry(this.m_tempScore.m_ring, msg.m_blink);
		GameObjectUtil.SendMessageToTagObjects("Player", "OnMsgTutorialResetForRetry", value, SendMessageOptions.DontRequireReceiver);
		MsgResetScore msg2 = new MsgResetScore(this.m_tempScore.m_score, this.m_tempScore.m_animal, this.m_tempScore.m_stkRing);
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.ResetScore(msg2);
		}
		PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
		if (playerInformation != null)
		{
			playerInformation.TotalDistance = this.m_tempScore.m_distance - StageTutorialManager.RetryOffsetPos - 1.05f;
		}
		this.DebugDraw(string.Concat(new object[]
		{
			"SetTempScore score=",
			this.m_tempScore.m_score,
			" ring=",
			this.m_tempScore.m_ring,
			" animal=",
			this.m_tempScore.m_animal,
			"distance=",
			this.m_tempScore.m_distance
		}));
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x0006D94C File Offset: 0x0006BB4C
	private void DebugDraw(string msg)
	{
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x0006D950 File Offset: 0x0006BB50
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06001474 RID: 5236 RVA: 0x0006D95C File Offset: 0x0006BB5C
	public static StageTutorialManager Instance
	{
		get
		{
			return StageTutorialManager.instance;
		}
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0006D964 File Offset: 0x0006BB64
	protected bool CheckInstance()
	{
		if (StageTutorialManager.instance == null)
		{
			StageTutorialManager.instance = this;
			return true;
		}
		if (this == StageTutorialManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x0006D9A8 File Offset: 0x0006BBA8
	private void OnDestroy()
	{
		if (StageTutorialManager.instance == this)
		{
			StageTutorialManager.instance = null;
		}
	}

	// Token: 0x040011B6 RID: 4534
	public bool m_debugDraw;

	// Token: 0x040011B7 RID: 4535
	private bool m_debugSkip;

	// Token: 0x040011B8 RID: 4536
	private static float RetryOffsetPos = 10f;

	// Token: 0x040011B9 RID: 4537
	private StageTutorialManager.Mode m_mode;

	// Token: 0x040011BA RID: 4538
	private uint m_eventState;

	// Token: 0x040011BB RID: 4539
	private int m_currentEventID;

	// Token: 0x040011BC RID: 4540
	private bool m_complete;

	// Token: 0x040011BD RID: 4541
	private Vector3 m_startPos = Vector3.zero;

	// Token: 0x040011BE RID: 4542
	private TempTutorialScore m_tempScore;

	// Token: 0x040011BF RID: 4543
	private static StageTutorialManager instance;

	// Token: 0x020002CC RID: 716
	private enum Mode
	{
		// Token: 0x040011C1 RID: 4545
		Idle,
		// Token: 0x040011C2 RID: 4546
		PlayStart,
		// Token: 0x040011C3 RID: 4547
		PlayEnd
	}

	// Token: 0x020002CD RID: 717
	private enum EventState : uint
	{
		// Token: 0x040011C5 RID: 4549
		Clear = 1U,
		// Token: 0x040011C6 RID: 4550
		Damage,
		// Token: 0x040011C7 RID: 4551
		Miss = 4U
	}
}
