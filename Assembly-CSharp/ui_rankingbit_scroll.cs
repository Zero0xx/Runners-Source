using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public class ui_rankingbit_scroll : MonoBehaviour
{
	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x060026E3 RID: 9955 RVA: 0x000EE3B4 File Offset: 0x000EC5B4
	public bool isMydata
	{
		get
		{
			return this.m_isMydata;
		}
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x000EE3BC File Offset: 0x000EC5BC
	private void Update()
	{
		if (this.m_moveTimeMax > 0f && this.m_moveTime > 0f)
		{
			this.m_moveTime -= Time.deltaTime;
			if (this.m_moveTime <= 0f)
			{
				this.m_moveTime = 0f;
				this.m_moveTimeMax = 0f;
				base.transform.localPosition = this.m_movePosition;
				base.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_child.UpdateViewRank(-1);
				if (this.m_isMydata)
				{
					this.m_parent.AutoMoveScrollEnd();
				}
			}
			else
			{
				if (this.m_moveTimeMax - this.m_moveTime < 0.2f)
				{
					float num = (this.m_moveTimeMax - this.m_moveTime) / 0.2f * 2f;
					if (num > 1f)
					{
						num = 1f - (num - 1f) * 0.5f;
					}
					if (this.m_isMydata)
					{
						base.transform.localScale = new Vector3(1f + num * 0.2f, 1f + num * 0.2f, 1f);
					}
					else
					{
						base.transform.localScale = new Vector3(1f - num * 0.2f, 1f - num * 0.2f, 1f);
					}
				}
				else if (this.m_moveTime < 0.1f)
				{
					float num = (base.transform.localScale.x - 1f) * 0.2f;
					base.transform.localScale = new Vector3(base.transform.localScale.x - num, base.transform.localScale.y - num, 1f);
				}
				float num2 = this.m_moveTime / this.m_moveTimeMax;
				if (this.m_moveTimeMax - this.m_moveTime < 0.25f)
				{
					num2 = 1f;
				}
				else if (this.m_moveTime < 0.25f)
				{
					num2 = 0f;
				}
				else
				{
					num2 = (this.m_moveTime - 0.25f) / (this.m_moveTimeMax - 0.5f);
				}
				float num3 = 1f - num2 * num2;
				if (num3 < 0f)
				{
					num3 = 0f;
				}
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				this.UpdateRank(num3);
				base.transform.localPosition = new Vector3(this.m_basePosition.x + this.m_vector.x * num3, this.m_basePosition.y + this.m_vector.y * num3, this.m_basePosition.z + this.m_vector.z * num3);
			}
			if (this.m_isMydata)
			{
				this.m_parent.AutoMoveScroll(base.transform.localPosition, true);
			}
		}
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x000EE6C8 File Offset: 0x000EC8C8
	public void MoveStart(float time)
	{
		if (this.m_vector.x != 0f || this.m_vector.y != 0f)
		{
			this.m_moveTime = time;
			this.m_moveTimeMax = time;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x000EE72C File Offset: 0x000EC92C
	private void UpdateRank(float rate)
	{
		if (this.m_addRank != 0 && this.m_frac > 0f)
		{
			bool flag = false;
			int num = 0;
			for (float num2 = 0f; num2 < 1f; num2 += this.m_frac)
			{
				if (num2 >= rate)
				{
					flag = true;
					break;
				}
				num++;
			}
			if (!flag)
			{
				this.m_child.UpdateViewRank(-1);
				this.m_frac = 0f;
			}
			else
			{
				int num3;
				if (this.m_addRank > 0)
				{
					num3 = this.m_addRank - num;
				}
				else
				{
					num3 = this.m_addRank + num;
				}
				if (this.m_differencevalue != null && this.m_currentUpRank != this.m_addRank - num3)
				{
					this.m_currentUpRank = this.m_addRank - num3;
					if (this.m_currentUpRank >= this.m_addRank)
					{
						this.m_currentUpRank = this.m_addRank;
					}
					this.m_differencevalue.text = "+" + this.m_currentUpRank.ToString();
				}
				this.m_child.UpdateViewRank(this.m_ranker.rankIndex + 1 + num3);
			}
		}
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x000EE85C File Offset: 0x000ECA5C
	public void UpdateView(RankingResultBitWindow parent, RankingUtil.RankChange change, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, RankingUtil.Ranker ranker, int addRank, Vector3 movePos, Vector3 basePos)
	{
		this.m_currentUpRank = 0;
		this.m_frac = 0f;
		this.m_moveTime = 0f;
		this.m_moveTimeMax = 0f;
		this.m_change = change;
		this.m_ranker = ranker;
		this.m_movePosition = movePos;
		this.m_basePosition = basePos;
		this.m_vector = this.m_movePosition - this.m_basePosition;
		base.transform.localPosition = this.m_basePosition;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.m_addRank = addRank;
		this.m_parent = parent;
		this.m_dragContents.draggablePanel = this.m_parent.draggable;
		this.SetPattern(this.m_change);
		this.SetUser(scoreType, rankerType, ranker);
		if (this.m_addRank != 0)
		{
			this.m_frac = 1f / (float)(Mathf.Abs(this.m_addRank) + 1);
		}
		if (this.m_child != null && this.m_change == RankingUtil.RankChange.UP)
		{
			this.m_isMydata = true;
			this.m_child.SetMyRanker(this.m_isMydata);
			if (this.m_differencevalue != null)
			{
				this.m_differencevalue.text = "+" + this.m_currentUpRank.ToString();
			}
			UISprite[] componentsInChildren = this.m_child.gameObject.GetComponentsInChildren<UISprite>();
			UILabel[] componentsInChildren2 = this.m_child.gameObject.GetComponentsInChildren<UILabel>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				foreach (UISprite uisprite in componentsInChildren)
				{
					uisprite.depth += 50;
				}
			}
			if (componentsInChildren2 != null && componentsInChildren2.Length > 0)
			{
				foreach (UILabel uilabel in componentsInChildren2)
				{
					uilabel.depth += 50;
				}
			}
		}
		if (this.m_addRank != 0 && this.m_child != null)
		{
			this.m_child.UpdateViewRank(ranker.rankIndex + 1 + this.m_addRank);
		}
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x000EEA98 File Offset: 0x000ECC98
	private void SetUser(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, RankingUtil.Ranker ranker)
	{
		if (this.m_child == null && this.m_orgRankingUser != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.m_orgRankingUser) as GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = default(Vector3);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_child = gameObject.GetComponentInChildren<ui_ranking_scroll>();
		}
		if (this.m_child != null)
		{
			this.m_child.UpdateView(scoreType, rankerType, ranker, false);
		}
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x000EEB48 File Offset: 0x000ECD48
	private void SetPattern(RankingUtil.RankChange change)
	{
		if (this.m_pattern != null && this.m_pattern.Count > 0)
		{
			for (int i = 0; i < this.m_pattern.Count; i++)
			{
				if (change != RankingUtil.RankChange.UP)
				{
					if (change != RankingUtil.RankChange.DOWN)
					{
						this.m_pattern[i].SetActive(false);
					}
					else if (this.m_pattern[i].name.IndexOf("down") != -1)
					{
						this.m_pattern[i].SetActive(true);
					}
					else
					{
						this.m_pattern[i].SetActive(false);
					}
				}
				else if (this.m_pattern[i].name.IndexOf("up") != -1)
				{
					this.m_pattern[i].SetActive(true);
					this.m_differencevalue = this.m_pattern[i].GetComponentInChildren<UILabel>();
				}
				else
				{
					this.m_pattern[i].SetActive(false);
				}
			}
		}
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x000EEC70 File Offset: 0x000ECE70
	public void Remove()
	{
		if (this.m_child != null)
		{
			UnityEngine.Object.Destroy(this.m_child.gameObject);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400233E RID: 9022
	[SerializeField]
	private GameObject m_orgRankingUser;

	// Token: 0x0400233F RID: 9023
	[SerializeField]
	private List<GameObject> m_pattern;

	// Token: 0x04002340 RID: 9024
	[SerializeField]
	private UIDragPanelContents m_dragContents;

	// Token: 0x04002341 RID: 9025
	private ui_ranking_scroll m_child;

	// Token: 0x04002342 RID: 9026
	private RankingResultBitWindow m_parent;

	// Token: 0x04002343 RID: 9027
	private RankingUtil.Ranker m_ranker;

	// Token: 0x04002344 RID: 9028
	private UILabel m_differencevalue;

	// Token: 0x04002345 RID: 9029
	private RankingUtil.RankChange m_change;

	// Token: 0x04002346 RID: 9030
	private int m_addRank;

	// Token: 0x04002347 RID: 9031
	private int m_currentUpRank;

	// Token: 0x04002348 RID: 9032
	private float m_frac;

	// Token: 0x04002349 RID: 9033
	private bool m_isMydata;

	// Token: 0x0400234A RID: 9034
	private float m_moveTime;

	// Token: 0x0400234B RID: 9035
	private float m_moveTimeMax;

	// Token: 0x0400234C RID: 9036
	private Vector3 m_movePosition;

	// Token: 0x0400234D RID: 9037
	private Vector3 m_basePosition;

	// Token: 0x0400234E RID: 9038
	private Vector3 m_vector;
}
