using System;
using UnityEngine;

// Token: 0x0200050C RID: 1292
public class ui_ranking_scroll_dummy : MonoBehaviour
{
	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000EDE50 File Offset: 0x000EC050
	// (set) Token: 0x060026D8 RID: 9944 RVA: 0x000EDE58 File Offset: 0x000EC058
	public RankingUtil.Ranker rankerData
	{
		get
		{
			return this.m_rankerData;
		}
		set
		{
			if (value != null && this.m_rankerData == null)
			{
				this.m_rankerData = value;
			}
			else if (value == null)
			{
				this.m_rankerData = null;
			}
		}
	}

	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000EDE90 File Offset: 0x000EC090
	public bool isMyData
	{
		get
		{
			bool result = false;
			if (this.myRankerData != null && this.m_rankerData != null && this.m_rankerData.id == this.myRankerData.id)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000EDED8 File Offset: 0x000EC0D8
	public void SetActiveObject(bool act, float delay = 0f)
	{
		if (this.dragPanelContent != null)
		{
			this.dragPanelContent.draggablePanel = this.storage.parentPanel;
		}
		if (this.m_boxCollider == null)
		{
			this.m_boxCollider = base.gameObject.GetComponentInChildren<BoxCollider>();
		}
		if (act)
		{
			if (this.m_rankerObject == null && this.storage != null)
			{
				this.m_rankerObject = this.storage.GetFreeObject();
				if (this.m_rankerObject != null)
				{
					this.UpdateRanker(delay);
				}
				else if (this.m_rankerData != null && this.spWindow != null)
				{
					RankingUtil.Ranker currentRanker = this.spWindow.GetCurrentRanker(this.slot);
					if (currentRanker != null && !this.m_rankerData.CheckRankerIdentity(currentRanker))
					{
						this.m_rankerData = currentRanker;
						this.UpdateRanker(delay);
					}
				}
				else if (this.m_rankerData != null && this.rankingUI != null)
				{
					RankingUtil.Ranker currentRanker2 = this.rankingUI.GetCurrentRanker(this.slot);
					if (currentRanker2 != null && !this.m_rankerData.CheckRankerIdentity(currentRanker2))
					{
						this.m_rankerData = currentRanker2;
						this.UpdateRanker(delay);
					}
				}
			}
		}
		else
		{
			if (this.button != null)
			{
				this.button.target = null;
			}
			if (this.label != null)
			{
				this.label.text = string.Empty;
			}
			this.SetMask(1f);
			if (this.m_rankerObject != null)
			{
				this.DrawClear();
				this.m_rankerObject.SetActive(false);
				this.m_rankerObject = null;
			}
			this.top = false;
		}
		if (this.m_boxCollider != null)
		{
			this.m_boxCollider.enabled = act;
		}
		base.gameObject.SetActive(act);
	}

	// Token: 0x060026DB RID: 9947 RVA: 0x000EE0D8 File Offset: 0x000EC2D8
	public void DrawClear()
	{
		if (this.m_rankerObject != null)
		{
			ui_ranking_scroll componentInChildren = this.m_rankerObject.GetComponentInChildren<ui_ranking_scroll>();
			if (componentInChildren != null)
			{
				componentInChildren.DrawClear();
			}
		}
	}

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x060026DC RID: 9948 RVA: 0x000EE114 File Offset: 0x000EC314
	public bool isMask
	{
		get
		{
			bool result = false;
			if (this.dummySprite != null)
			{
				result = this.dummySprite.gameObject.activeSelf;
			}
			return result;
		}
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x000EE148 File Offset: 0x000EC348
	public void SetMask(float alpha = 0f)
	{
		if (this.dummySprite != null)
		{
			if (alpha <= 0f)
			{
				this.dummySprite.gameObject.SetActive(false);
				this.dummySprite.alpha = 0f;
				base.enabled = false;
			}
			else
			{
				this.dummySprite.gameObject.SetActive(true);
				this.dummySprite.alpha = alpha;
				base.enabled = true;
			}
		}
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x000EE1C4 File Offset: 0x000EC3C4
	public bool IsCreating(float line = 0f)
	{
		return this.storage != null && this.storage.IsCreating(line);
	}

	// Token: 0x060026DF RID: 9951 RVA: 0x000EE1EC File Offset: 0x000EC3EC
	public void CheckCreate()
	{
		if (this.storage != null)
		{
			this.storage.CheckCreate();
		}
	}

	// Token: 0x060026E0 RID: 9952 RVA: 0x000EE20C File Offset: 0x000EC40C
	private void UpdateRanker(float delay)
	{
		if (this.m_rankerObject != null)
		{
			this.m_rankerObject.transform.localPosition = base.transform.localPosition;
			if (this.m_rankerData != null)
			{
				this.m_rankerObject.SetActive(true);
				ui_ranking_scroll componentInChildren = this.m_rankerObject.GetComponentInChildren<ui_ranking_scroll>();
				if (componentInChildren != null)
				{
					bool myCell = false;
					if (this.myRankerData != null && this.m_rankerData.id == this.myRankerData.id)
					{
						myCell = true;
					}
					componentInChildren.UpdateViewAsync(this.scoreType, this.rankerType, this.m_rankerData, this.end, myCell, delay, this);
					if (this.button != null)
					{
						this.button.target = null;
					}
					if (this.label != null)
					{
						this.label.text = string.Empty;
					}
					this.end = false;
				}
				UIRectItemStorageSlot component = this.m_rankerObject.GetComponent<UIRectItemStorageSlot>();
				if (component != null)
				{
					component.storage = null;
					component.storageRanking = this.storage;
					component.slot = this.m_rankerData.rankIndex;
				}
			}
		}
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x000EE348 File Offset: 0x000EC548
	private void Update()
	{
		if (this.dummySprite != null)
		{
			if ((double)this.dummySprite.alpha < 1.0)
			{
				this.SetMask(this.dummySprite.alpha - Time.deltaTime * 10f);
			}
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x0400232E RID: 9006
	[SerializeField]
	private UIDragPanelContents dragPanelContent;

	// Token: 0x0400232F RID: 9007
	[SerializeField]
	private UISprite dummySprite;

	// Token: 0x04002330 RID: 9008
	[SerializeField]
	private UIButtonMessage button;

	// Token: 0x04002331 RID: 9009
	[SerializeField]
	private UILabel label;

	// Token: 0x04002332 RID: 9010
	private GameObject m_rankerObject;

	// Token: 0x04002333 RID: 9011
	private RankingUtil.Ranker m_rankerData;

	// Token: 0x04002334 RID: 9012
	public RankingUtil.Ranker myRankerData;

	// Token: 0x04002335 RID: 9013
	public UIRectItemStorageRanking storage;

	// Token: 0x04002336 RID: 9014
	public SpecialStageWindow spWindow;

	// Token: 0x04002337 RID: 9015
	public RankingUI rankingUI;

	// Token: 0x04002338 RID: 9016
	public RankingUtil.RankingScoreType scoreType;

	// Token: 0x04002339 RID: 9017
	public RankingUtil.RankingRankerType rankerType;

	// Token: 0x0400233A RID: 9018
	public bool end;

	// Token: 0x0400233B RID: 9019
	public bool top;

	// Token: 0x0400233C RID: 9020
	public int slot;

	// Token: 0x0400233D RID: 9021
	private BoxCollider m_boxCollider;
}
