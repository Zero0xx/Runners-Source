using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050F RID: 1295
public class RouletteBoardPattern : MonoBehaviour
{
	// Token: 0x060026FF RID: 9983 RVA: 0x000EF588 File Offset: 0x000ED788
	public void Setup(RouletteBoard parent, RouletteItem orgItem, int initCell = 0)
	{
		base.gameObject.SetActive(true);
		this.m_activeEffBase = null;
		this.m_rankEffBase = null;
		this.m_cellEffBase = null;
		this.m_parent = parent;
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_bg_LT");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_bg_RT");
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "bg_LT");
		UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "bg_RT");
		if (this.m_parent != null && uisprite != null && uisprite2 != null && uisprite3 != null && uisprite4 != null)
		{
			uisprite.spriteName = this.m_parent.wheel.GetRouletteBgSprite();
			uisprite2.spriteName = this.m_parent.wheel.GetRouletteBgSprite();
			uisprite3.spriteName = this.m_parent.wheel.GetRouletteBoardSprite();
			uisprite4.spriteName = this.m_parent.wheel.GetRouletteBoardSprite();
		}
		if (this.m_itemPosList == null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "item");
			if (gameObject != null)
			{
				this.m_itemPosList = new List<GameObject>();
				for (int i = 0; i < 8; i++)
				{
					GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "pos_" + i);
					if (!(gameObject2 != null))
					{
						break;
					}
					gameObject2.SetActive(true);
					this.m_itemPosList.Add(gameObject2);
				}
			}
		}
		if (this.m_cellEffList == null)
		{
			this.m_cellEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "cell_eff");
			if (this.m_cellEffBase != null)
			{
				this.m_cellEffList = new List<UISprite>();
				for (int j = 0; j < 8; j++)
				{
					UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_cellEffBase, "cell_eff_" + j);
					if (!(uisprite5 != null))
					{
						break;
					}
					uisprite5.enabled = true;
					uisprite5.gameObject.SetActive(true);
					GeneralUtil.SetGameObjectOutMoveEnabled(uisprite5.gameObject, j == initCell);
					this.m_cellEffList.Add(uisprite5);
				}
				this.m_cellEffBase.SetActive(this.m_parent.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BOARD));
			}
		}
		if (this.m_rankEffList == null)
		{
			this.m_rankEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "rank_eff");
			if (this.m_rankEffBase != null)
			{
				this.m_rankEffList = new List<UISprite>();
				for (int k = 0; k < 8; k++)
				{
					UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rankEffBase, "rank_eff_" + k);
					if (!(uisprite6 != null))
					{
						break;
					}
					uisprite6.enabled = false;
					uisprite6.gameObject.SetActive(false);
					this.m_rankEffList.Add(uisprite6);
				}
				this.m_rankEffBase.SetActive(this.m_parent.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BOARD));
			}
		}
		if (this.m_activeEffList == null)
		{
			this.m_activeEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "active_eff");
			if (this.m_activeEffBase != null)
			{
				this.m_activeEffList = new List<UISprite>();
				for (int l = 0; l < 8; l++)
				{
					UISprite uisprite7 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_activeEffBase, "active_eff_" + l);
					if (!(uisprite7 != null))
					{
						break;
					}
					uisprite7.enabled = false;
					uisprite7.gameObject.SetActive(false);
					this.m_activeEffList.Add(uisprite7);
				}
				this.m_activeEffBase.SetActive(false);
			}
		}
		this.SetupItem(orgItem);
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x000EF988 File Offset: 0x000EDB88
	private void SetupItem(RouletteItem orgItem)
	{
		if (this.m_itemList == null && this.m_itemPosList != null && this.m_itemPosList.Count > 0)
		{
			this.m_itemList = new List<RouletteItem>();
			for (int i = 0; i < this.m_itemPosList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(orgItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
				gameObject.gameObject.transform.parent = this.m_itemPosList[i].transform;
				gameObject.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				RouletteItem componentInChildren = gameObject.GetComponentInChildren<RouletteItem>();
				if (componentInChildren != null)
				{
					componentInChildren.Setup(this.m_parent, i);
					this.m_itemList.Add(componentInChildren);
					if (this.m_rankEffList != null && this.m_rankEffList.Count >= 0 && this.m_rankEffList.Count > i)
					{
						this.m_rankEffList[i].enabled = componentInChildren.isRank;
					}
				}
			}
		}
		else if (this.m_itemList != null)
		{
			for (int j = 0; j < this.m_itemList.Count; j++)
			{
				this.m_itemList[j].Setup(this.m_parent, j);
			}
		}
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x000EFB1C File Offset: 0x000EDD1C
	public void SetCurrentCell(int currentCell)
	{
		if (this.m_cellEffList != null && this.m_cellEffList.Count > currentCell)
		{
			for (int i = 0; i < this.m_cellEffList.Count; i++)
			{
				GeneralUtil.SetGameObjectOutMoveEnabled(this.m_cellEffList[i].gameObject, currentCell == i);
			}
		}
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x000EFB7C File Offset: 0x000EDD7C
	public void Reset()
	{
		if (this.m_itemList != null)
		{
			for (int i = 0; i < this.m_itemList.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_itemList[i].gameObject);
			}
			this.m_itemList.Clear();
			this.m_itemList = null;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x000EFBE4 File Offset: 0x000EDDE4
	public int GetCellIndex(float degree)
	{
		int result = -1;
		if (degree >= 360f)
		{
			int num = (int)(degree / 360f);
			degree -= 360f * (float)num;
		}
		degree += 360f;
		if (this.m_cells != null && this.m_cells.Count > 0)
		{
			for (int i = 0; i < this.m_cells.Count; i++)
			{
				float num2 = this.m_cells[i].x + 360f;
				float num3 = this.m_cells[i].y + 360f;
				if (num2 > num3)
				{
					num2 -= 360f;
				}
				if (num2 <= degree && num3 >= degree)
				{
					result = i;
					break;
				}
				if (num2 + 360f <= degree && num3 + 360f >= degree)
				{
					result = i;
					break;
				}
			}
		}
		else
		{
			float num4 = 45f;
			for (int j = 0; j < 8; j++)
			{
				float num2 = num4 * -0.5f + num4 * (float)j + 360f;
				float num3 = num4 * 0.5f + num4 * (float)j + 360f;
				if (num2 <= degree && num3 >= degree)
				{
					result = j;
					break;
				}
				if (num2 + 360f <= degree && num3 + 360f >= degree)
				{
					result = j;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x000EFD6C File Offset: 0x000EDF6C
	public float GetCelllastSpeed(int cellIndex)
	{
		float result = 1f;
		if (this.m_cells != null && this.m_cells.Count > cellIndex)
		{
			cellIndex = (cellIndex + this.m_cells.Count) % this.m_cells.Count;
			result = this.m_cells[cellIndex].z;
		}
		return result;
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x000EFDCC File Offset: 0x000EDFCC
	public void GetCellData(int cellIndex, out float startRot, out float endRot, out float lastSpeedRate)
	{
		startRot = 0f;
		endRot = 0f;
		lastSpeedRate = 1f;
		if (this.m_cells != null && this.m_cells.Count > cellIndex)
		{
			cellIndex = (cellIndex + this.m_cells.Count) % this.m_cells.Count;
			Vector3 vector = this.m_cells[cellIndex];
			startRot = vector.x;
			endRot = vector.y;
			if (startRot > endRot)
			{
				endRot += 360f;
			}
			else
			{
				startRot += 360f;
				endRot += 360f;
			}
			lastSpeedRate = vector.z;
		}
		else
		{
			cellIndex = (cellIndex + 8) % 8;
			startRot = (float)(-18 + 45 * cellIndex + 360);
			endRot = (float)(18 + 45 * cellIndex + 360);
			lastSpeedRate = 1f;
		}
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x000EFEB0 File Offset: 0x000EE0B0
	public void UpdateEffectSetting()
	{
		if (this.m_parent != null && this.m_parent.parent != null)
		{
			this.m_activeEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "active_eff");
			this.m_cellEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "cell_eff");
			this.m_rankEffBase = GameObjectUtil.FindChildGameObject(base.gameObject, "rank_eff");
			if (this.m_activeEffBase != null)
			{
				this.m_activeEffBase.SetActive(false);
			}
			if (this.m_cellEffBase != null)
			{
				this.m_cellEffBase.SetActive(this.m_parent.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BOARD));
			}
			if (this.m_rankEffBase != null)
			{
				this.m_rankEffBase.SetActive(this.m_parent.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BOARD));
			}
		}
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x000EFFA0 File Offset: 0x000EE1A0
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x000EFFAC File Offset: 0x000EE1AC
	private void Update()
	{
		base.enabled = false;
	}

	// Token: 0x04002364 RID: 9060
	private const int CELL_MAX = 8;

	// Token: 0x04002365 RID: 9061
	[Header("各枠情報 x:開始角度 y:終了角度 z:針速度")]
	[SerializeField]
	private List<Vector3> m_cells;

	// Token: 0x04002366 RID: 9062
	private RouletteBoard m_parent;

	// Token: 0x04002367 RID: 9063
	private List<GameObject> m_itemPosList;

	// Token: 0x04002368 RID: 9064
	private GameObject m_activeEffBase;

	// Token: 0x04002369 RID: 9065
	private GameObject m_rankEffBase;

	// Token: 0x0400236A RID: 9066
	private GameObject m_cellEffBase;

	// Token: 0x0400236B RID: 9067
	private List<UISprite> m_activeEffList;

	// Token: 0x0400236C RID: 9068
	private List<UISprite> m_rankEffList;

	// Token: 0x0400236D RID: 9069
	private List<UISprite> m_cellEffList;

	// Token: 0x0400236E RID: 9070
	private List<RouletteItem> m_itemList;
}
