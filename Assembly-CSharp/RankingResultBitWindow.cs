using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class RankingResultBitWindow : MonoBehaviour
{
	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000E4BF0 File Offset: 0x000E2DF0
	public UIDraggablePanel draggable
	{
		get
		{
			return this.m_draggable;
		}
	}

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000E4BF8 File Offset: 0x000E2DF8
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x000E4C00 File Offset: 0x000E2E00
	private void Update()
	{
		if (base.gameObject.activeSelf)
		{
			this.m_openTime += Time.deltaTime;
			if (this.m_openTime >= 2f && !this.m_isMove && this.m_rankerList != null && this.m_rankerList.Count > 0)
			{
				foreach (ui_rankingbit_scroll ui_rankingbit_scroll in this.m_rankerList)
				{
					ui_rankingbit_scroll.MoveStart(0.9f + (float)Mathf.Abs(this.m_oldRank - this.m_currentRank) * 0.03f);
				}
				this.m_isMove = true;
			}
			if (this.m_autoMoveTime > 0f)
			{
				this.m_autoMoveTime -= Time.deltaTime;
				if (this.m_rankerList != null)
				{
					this.AutoMove();
				}
			}
		}
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x000E4D18 File Offset: 0x000E2F18
	public void AutoMove()
	{
		if (this.m_autoMoveSpeed > 0f && this.m_draggable != null && this.m_autoMoveTime > 0f)
		{
			float num = this.m_draggable.panel.transform.localPosition.y * -1f + this.m_draggable.panel.clipRange.w * 0.5f + -47f;
			float num2 = this.m_draggable.panel.transform.localPosition.y * -1f + this.m_draggable.panel.clipRange.w * -0.5f + 47f;
			float num3 = 0f;
			if (this.m_autoMoveTargetPos.y < num2)
			{
				num3 = this.m_autoMoveTargetPos.y - num2;
				if (num3 <= this.m_autoMoveSpeed * -1f)
				{
					num3 = this.m_autoMoveSpeed * -1f;
					this.m_autoMoveSpeed = 0f;
				}
			}
			else if (this.m_autoMoveTargetPos.y > num)
			{
				num3 = this.m_autoMoveTargetPos.y - num;
				if (num3 >= this.m_autoMoveSpeed)
				{
					num3 = this.m_autoMoveSpeed;
					this.m_autoMoveSpeed = 0f;
				}
			}
			float num4 = this.m_draggable.panel.transform.localPosition.y * -1f + num3;
			if (num4 >= -0.75f)
			{
				num4 = -0.75f;
			}
			else if (num4 <= -0.75f + (float)(this.m_rankerList.Count * -94))
			{
				num4 = -0.75f + (float)(this.m_rankerList.Count * -94);
			}
			this.m_draggable.panel.clipRange = new Vector4(0f, num4, this.m_draggable.panel.clipRange.z, this.m_draggable.panel.clipRange.w);
			this.m_draggable.panel.transform.localPosition = new Vector3(0f, this.m_draggable.panel.clipRange.y * -1f, this.m_draggable.panel.transform.localPosition.z);
		}
		else
		{
			this.m_autoMoveSpeed = 0f;
			this.m_autoMoveTime = 0f;
		}
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000E4FB0 File Offset: 0x000E31B0
	public void AutoMoveScrollEnd()
	{
		SoundManager.SePlay("sys_rank_kettei", "SE");
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(false);
		}
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x000E4FE0 File Offset: 0x000E31E0
	public void AutoMoveScroll(Vector3 position, bool up)
	{
		if (up)
		{
			this.m_autoMoveTargetPos = new Vector3(position.x, position.y + 188f, 0f);
		}
		else
		{
			this.m_autoMoveTargetPos = new Vector3(position.x, position.y, 0f);
		}
		this.m_autoMoveSpeed = 10000f;
		this.m_autoMoveTime = 0.2f;
		if (this.m_rankerList != null)
		{
			this.AutoMove();
		}
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x000E5064 File Offset: 0x000E3264
	private void OnClickNoButton()
	{
		SingletonGameObject<RankingManager>.Instance.ResetRankingRankChange(this.m_rankingMode);
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x000E5094 File Offset: 0x000E3294
	public void Close()
	{
		this.ResetRankerList();
		this.m_openTime = 0f;
		this.m_isMove = false;
		this.m_isEnd = true;
		if (this.m_panel != null)
		{
			this.m_panel.alpha = 0f;
		}
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(false);
		}
		this.RemoveBackKeyCallBack();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x000E5110 File Offset: 0x000E3310
	public void Init()
	{
		if (this.m_panel != null)
		{
			this.m_panel.alpha = 0f;
		}
		this.m_openTime = 0f;
		this.m_autoMoveSpeed = 0f;
		this.m_isMove = false;
		this.m_isEnd = false;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x000E5164 File Offset: 0x000E3364
	public bool Open(RankingUtil.RankingMode rankingMode)
	{
		this.m_rankingMode = rankingMode;
		if (this.m_panel != null)
		{
			this.m_panel.alpha = 0f;
		}
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(true);
		}
		SoundManager.SePlay("sys_window_open", "SE");
		this.m_autoMoveSpeed = 0f;
		base.gameObject.SetActive(true);
		RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
		RankingUtil.RankingScoreType endlessRivalRankingScoreType = RankingManager.EndlessRivalRankingScoreType;
		this.m_change = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(this.m_rankingMode, endlessRivalRankingScoreType, rankerType, out this.m_currentRank, out this.m_oldRank);
		this.m_isEnd = false;
		if (this.m_change != RankingUtil.RankChange.NONE)
		{
			SingletonGameObject<RankingManager>.Instance.GetRanking(this.m_rankingMode, endlessRivalRankingScoreType, rankerType, 0, new RankingManager.CallbackRankingData(this.CallbackRanking));
			return true;
		}
		base.gameObject.SetActive(false);
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(false);
		}
		return false;
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x000E5268 File Offset: 0x000E3468
	private ui_rankingbit_scroll CreateRankerData(int index, bool mydata, RankingUtil.Ranker ranker, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type)
	{
		this.m_openTime = 0f;
		this.m_isMove = false;
		GameObject gameObject = UnityEngine.Object.Instantiate(this.m_orgRankingbit) as GameObject;
		gameObject.transform.parent = this.m_draggable.transform;
		int rankIndex = ranker.rankIndex;
		Vector3 vector = new Vector3(0f, (float)(153 + ranker.rankIndex * -94), 0f);
		Vector3 movePos = new Vector3(0f, (float)(153 + ranker.rankIndex * -94), 0f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		ranker.userDataType = RankingUtil.UserDataType.RANK_UP;
		ui_rankingbit_scroll componentInChildren = gameObject.GetComponentInChildren<ui_rankingbit_scroll>();
		if (componentInChildren != null)
		{
			if (mydata)
			{
				vector = new Vector3(0f, (float)(153 + (rankIndex + (this.m_oldRank - this.m_currentRank)) * -94), 0f);
				componentInChildren.UpdateView(this, this.m_change, score, type, ranker, this.m_oldRank - this.m_currentRank, movePos, vector);
			}
			else
			{
				if (ranker.isFriend)
				{
					ranker.isSentEnergy = true;
				}
				if (ranker.rankIndex + 1 > this.m_currentRank && ranker.rankIndex + 1 <= this.m_oldRank)
				{
					vector = new Vector3(0f, (float)(153 + (rankIndex - 1) * -94), 0f);
					componentInChildren.UpdateView(this, RankingUtil.RankChange.DOWN, score, type, ranker, -1, movePos, vector);
				}
				else
				{
					componentInChildren.UpdateView(this, RankingUtil.RankChange.NONE, score, type, ranker, 0, vector, vector);
				}
			}
			if (this.m_rankerList == null)
			{
				this.m_rankerList = new List<ui_rankingbit_scroll>();
			}
			this.m_rankerList.Add(componentInChildren);
		}
		return componentInChildren;
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x000E542C File Offset: 0x000E362C
	private void CallbackRanking(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		if (rankerList != null && rankerList.Count > 1 && rankerList[0] != null && this.m_orgRankingbit != null)
		{
			if (this.m_frontCollider != null)
			{
				this.m_frontCollider.SetActive(true);
			}
			if (this.m_oldRank > rankerList.Count - 1)
			{
				this.m_oldRank = rankerList.Count - 1;
			}
			ui_rankingbit_scroll ui_rankingbit_scroll = null;
			for (int i = 1; i < rankerList.Count; i++)
			{
				if (rankerList[i].id == rankerList[0].id)
				{
					ui_rankingbit_scroll = this.CreateRankerData(i - 1, true, rankerList[i], score, type);
				}
				else
				{
					this.CreateRankerData(i - 1, false, rankerList[i], score, type);
				}
			}
			if (this.m_panel != null)
			{
				this.m_panel.alpha = 1f;
			}
			this.m_draggable.ResetPosition();
			if (ui_rankingbit_scroll != null)
			{
				this.AutoMoveScroll(ui_rankingbit_scroll.transform.localPosition, false);
			}
			if (this.m_animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
			}
		}
		else
		{
			this.m_change = RankingUtil.RankChange.NONE;
			if (this.m_panel != null)
			{
				this.m_panel.alpha = 0f;
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x000E55CC File Offset: 0x000E37CC
	private void ResetRankerList()
	{
		if (this.m_rankerList != null)
		{
			if (this.m_rankerList.Count > 0)
			{
				foreach (ui_rankingbit_scroll ui_rankingbit_scroll in this.m_rankerList)
				{
					ui_rankingbit_scroll.Remove();
				}
				this.m_rankerList.Clear();
			}
			this.m_rankerList = null;
		}
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x000E5660 File Offset: 0x000E3860
	public void AddRanker(ui_rankingbit_scroll ranker)
	{
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x000E5664 File Offset: 0x000E3864
	private void AnimationFinishCallback()
	{
	}

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000E5668 File Offset: 0x000E3868
	public static RankingResultBitWindow Instance
	{
		get
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				RankingResultBitWindow rankingResultBitWindow = GameObjectUtil.FindChildGameObjectComponent<RankingResultBitWindow>(gameObject, "RankingResultBitWindow");
				if (rankingResultBitWindow != null)
				{
					rankingResultBitWindow.gameObject.SetActive(true);
				}
			}
			return RankingResultBitWindow.s_instance;
		}
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x000E56B8 File Offset: 0x000E38B8
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x000E56C0 File Offset: 0x000E38C0
	private void OnDestroy()
	{
		if (RankingResultBitWindow.s_instance == this)
		{
			this.RemoveBackKeyCallBack();
			RankingResultBitWindow.s_instance = null;
		}
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x000E56E0 File Offset: 0x000E38E0
	private void SetInstance()
	{
		if (RankingResultBitWindow.s_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			RankingResultBitWindow.s_instance = this;
			this.EntryBackKeyCallBack();
			RankingResultBitWindow.s_instance.Init();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x000E5730 File Offset: 0x000E3930
	private void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x000E5740 File Offset: 0x000E3940
	private void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x000E5750 File Offset: 0x000E3950
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		if (gameObject != null)
		{
			UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x040021ED RID: 8685
	private const float RANK_CHANGE_EFFECT_START_TIME = 2f;

	// Token: 0x040021EE RID: 8686
	private const float RANK_CHANGE_EFFECT_MOVE_TIME = 0.9f;

	// Token: 0x040021EF RID: 8687
	private const int RANKER_DATA_SIZE_H = 94;

	// Token: 0x040021F0 RID: 8688
	private const int RANKER_LIST_INIT_X = 0;

	// Token: 0x040021F1 RID: 8689
	private const int RANKER_LIST_INIT_Y = 153;

	// Token: 0x040021F2 RID: 8690
	private const int RANKER_LIST_ADD_Y = -94;

	// Token: 0x040021F3 RID: 8691
	[SerializeField]
	private GameObject m_orgRankingbit;

	// Token: 0x040021F4 RID: 8692
	[SerializeField]
	private Animation m_animation;

	// Token: 0x040021F5 RID: 8693
	[SerializeField]
	private UIPanel m_panel;

	// Token: 0x040021F6 RID: 8694
	[SerializeField]
	private UIDraggablePanel m_draggable;

	// Token: 0x040021F7 RID: 8695
	[SerializeField]
	private GameObject m_frontCollider;

	// Token: 0x040021F8 RID: 8696
	private List<ui_rankingbit_scroll> m_rankerList;

	// Token: 0x040021F9 RID: 8697
	private RankingUtil.RankingMode m_rankingMode;

	// Token: 0x040021FA RID: 8698
	private RankingUtil.RankChange m_change;

	// Token: 0x040021FB RID: 8699
	private int m_currentRank = -1;

	// Token: 0x040021FC RID: 8700
	private int m_oldRank = -1;

	// Token: 0x040021FD RID: 8701
	private float m_openTime;

	// Token: 0x040021FE RID: 8702
	private float m_autoMoveSpeed;

	// Token: 0x040021FF RID: 8703
	private float m_autoMoveTime;

	// Token: 0x04002200 RID: 8704
	private bool m_isMove;

	// Token: 0x04002201 RID: 8705
	private bool m_isEnd;

	// Token: 0x04002202 RID: 8706
	private Vector3 m_autoMoveTargetPos;

	// Token: 0x04002203 RID: 8707
	private static RankingResultBitWindow s_instance;
}
