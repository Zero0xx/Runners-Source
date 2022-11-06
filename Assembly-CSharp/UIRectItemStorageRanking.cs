using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B7 RID: 951
[AddComponentMenu("NGUI/Item/UI Rect Item Storage")]
public class UIRectItemStorageRanking : MonoBehaviour
{
	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000A3A90 File Offset: 0x000A1C90
	public List<UIInvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
	public int childCount
	{
		get
		{
			return this.m_childs.Count;
		}
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000A3AD0 File Offset: 0x000A1CD0
	public UIInvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000A3AF8 File Offset: 0x000A1CF8
	public UIInvGameItem Replace(int slot, UIInvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			UIInvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x000A3B30 File Offset: 0x000A1D30
	public bool IsCreating(float line = 0f)
	{
		if (this.m_placeObjects != null && (float)this.m_placeObjects.Count > line)
		{
			return true;
		}
		if (this.m_createLastTime != 0f && Time.realtimeSinceStartup > this.m_createLastTime + 0.1f)
		{
			this.m_createLastTime = 0f;
			return true;
		}
		return false;
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000A3B90 File Offset: 0x000A1D90
	public void CheckCreate()
	{
		this.m_createLastTime = Time.deltaTime;
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000A3BA0 File Offset: 0x000A1DA0
	public bool CheckBothEnd(out bool isNext)
	{
		bool result = false;
		isNext = false;
		if (this.parentPanel != null)
		{
			if (this.isDirectionVertical)
			{
				if (this.parentPanel.verticalScrollBar != null)
				{
					isNext = ((double)this.parentPanel.verticalScrollBar.value >= 1.0);
					if (this.parentPanel.verticalScrollBar.value <= 0.01f || this.parentPanel.verticalScrollBar.value >= 0.99f)
					{
						result = true;
					}
				}
			}
			else if (this.parentPanel.horizontalScrollBar != null)
			{
				isNext = ((double)this.parentPanel.horizontalScrollBar.value >= 1.0);
				if (this.parentPanel.horizontalScrollBar.value <= 0.01f || this.parentPanel.horizontalScrollBar.value >= 0.99f)
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000A3CB0 File Offset: 0x000A1EB0
	private void Start()
	{
		if (!this.m_initCountainer)
		{
			this.InitContainer();
		}
		if (this.parentPanel != null)
		{
			Vector4 clipRange = this.parentPanel.panel.clipRange;
			this.drawArea = new Vector3(clipRange.z * this.drawAreaScale, clipRange.w * this.drawAreaScale, 0f);
		}
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000A3D1C File Offset: 0x000A1F1C
	private void Update()
	{
		if (this.m_placeObjects != null && this.m_placeObjects.Count > 0)
		{
			if (this.m_placeCurrentTime <= 0f)
			{
				PlaceObjectData placeObjectData = this.m_placeObjects[0];
				this.Place(placeObjectData.x, placeObjectData.y, placeObjectData.count, placeObjectData.bound);
				this.m_placeObjects.RemoveAt(0);
				this.m_placeCurrentTime = this.m_placeDelay;
			}
			else
			{
				this.m_placeCurrentTime -= Time.deltaTime;
			}
		}
		if (this.parentPanel != null)
		{
			if (this.CheckItemUpdate())
			{
				if (this.isDirectionVertical)
				{
					if (this.parentPanel.verticalScrollBar != null)
					{
						if ((this.parentPanel.verticalScrollBar.value < 1f && this.parentPanel.verticalScrollBar.value > 0f) || this.m_currentBarSize <= 0f)
						{
							this.m_currentBarSize = this.parentPanel.verticalScrollBar.barSize;
						}
						else if (this.m_currentBarSize > 0f)
						{
							float barSize = this.parentPanel.verticalScrollBar.barSize;
							if (barSize / this.m_currentBarSize <= 0.8f && this.m_childs != null)
							{
								bool flag = true;
								bool flag2 = (double)this.parentPanel.verticalScrollBar.value >= 1.0;
								if (flag2 && this.callbackTopOrLast != null)
								{
									int count = this.m_childs.Count;
									if (this.m_drawLastIndex + 25 >= count && (this.m_placeObjects == null || this.m_placeObjects.Count <= 0) && (this.m_addItemLastTime == 0f || Mathf.Abs(this.m_addItemLastTime - Time.realtimeSinceStartup) >= 0.19999999f))
									{
										flag = !this.callbackTopOrLast(!flag2);
									}
								}
								if (flag)
								{
									this.CheckItemDrawAll(flag2);
								}
							}
						}
					}
				}
				else if (this.parentPanel.horizontalScrollBar != null)
				{
					if (this.parentPanel.horizontalScrollBar.value < 1f && this.parentPanel.horizontalScrollBar.value > 0f)
					{
						this.m_currentBarSize = this.parentPanel.horizontalScrollBar.barSize;
					}
					else if (this.m_currentBarSize > 0f)
					{
						float barSize2 = this.parentPanel.horizontalScrollBar.barSize;
						if (barSize2 / this.m_currentBarSize <= 0.8f)
						{
							this.CheckItemDrawAll(this.parentPanel.verticalScrollBar.value <= 0f);
							this.m_currentBarSize = 0f;
						}
					}
				}
			}
			else if (this.isDirectionVertical)
			{
				if (this.parentPanel.verticalScrollBar.value < 1f && this.parentPanel.verticalScrollBar.value > 0f)
				{
					this.m_currentBarSize = this.parentPanel.verticalScrollBar.barSize;
				}
			}
			else if (this.parentPanel.horizontalScrollBar.value < 1f && this.parentPanel.horizontalScrollBar.value > 0f)
			{
				this.m_currentBarSize = this.parentPanel.horizontalScrollBar.barSize;
			}
		}
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000A40B4 File Offset: 0x000A22B4
	private float GetPosX(int param)
	{
		return (float)this.padding + ((float)param + 0.5f) * (float)this.spacing;
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x000A40D0 File Offset: 0x000A22D0
	private float GetPosY(int param)
	{
		return (float)(-(float)this.padding) - ((float)param + 0.5f) * (float)this.spacing;
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x000A40EC File Offset: 0x000A22EC
	private void PlaceAdd(int x, int y, int count, Bounds b)
	{
		this.m_placeDelay = 0f;
		this.Place(x, y, count, b);
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x000A4104 File Offset: 0x000A2304
	private void Place(int x, int y, int count, Bounds b)
	{
		if (this.m_childsOrg != null && this.m_childsOrg.Count < 80)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
			if (gameObject != null)
			{
				ui_ranking_scroll component = gameObject.GetComponent<ui_ranking_scroll>();
				gameObject.SetActive(false);
				if (component != null)
				{
					this.m_childsOrg.Add(component);
				}
			}
		}
		GameObject gameObject2 = NGUITools.AddChild(base.gameObject, this.templateDummy);
		if (gameObject2 != null)
		{
			ui_ranking_scroll_dummy component2 = gameObject2.GetComponent<ui_ranking_scroll_dummy>();
			if (component2 != null)
			{
				this.m_childs.Add(component2);
				Transform transform = component2.transform;
				if (this.isDirectionVertical)
				{
					transform.localPosition = new Vector3(0f, this.GetPosY(y), 0f);
				}
				else
				{
					transform.localPosition = new Vector3(this.GetPosX(x), 0f, 0f);
				}
				component2.storage = this;
				component2.end = false;
				component2.slot = count;
				if (this.callback != null)
				{
					this.callback(component2, this);
				}
			}
		}
		b.Encapsulate(new Vector3((float)this.padding * 2f + (float)((x + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((y + 1) * this.spacing), 0f));
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000A4274 File Offset: 0x000A2474
	public GameObject GetFreeObject()
	{
		GameObject result = null;
		if (this.m_childsOrg != null && this.m_childsOrg.Count > 0)
		{
			foreach (ui_ranking_scroll ui_ranking_scroll in this.m_childsOrg)
			{
				if (!ui_ranking_scroll.gameObject.activeSelf)
				{
					result = ui_ranking_scroll.gameObject;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x000A4310 File Offset: 0x000A2510
	public int GetFreeObjectNum()
	{
		int num = 0;
		if (this.m_childsOrg != null && this.m_childsOrg.Count > 0)
		{
			foreach (ui_ranking_scroll ui_ranking_scroll in this.m_childsOrg)
			{
				if (!ui_ranking_scroll.gameObject.activeSelf)
				{
					num++;
				}
			}
		}
		num -= 5;
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000A43B0 File Offset: 0x000A25B0
	public void Reset()
	{
		this.m_drawAllLastPoint = 0f;
		this.m_addItemLastTime = 0f;
		this.m_createLastTime = 0f;
		this.m_placeObjects = new List<PlaceObjectData>();
		this.m_drawLastIndex = 0;
		this.m_drawStartIndex = 0;
		if (this.parentPanel != null && this.parentPanel.gameObject.activeSelf && this.parentPanel.panel != null)
		{
			this.parentPanel.panel.clipRange = new Vector4(0f, 0f, this.parentPanel.panel.clipRange.z, this.parentPanel.panel.clipRange.w);
			this.parentPanel.transform.localPosition = new Vector3(this.parentPanel.transform.localPosition.x, 0f, this.parentPanel.transform.localPosition.z);
			Vector4 clipRange = this.parentPanel.panel.clipRange;
			this.drawArea = new Vector3(clipRange.z * this.drawAreaScale, clipRange.w * this.drawAreaScale, 0f);
		}
		this.maxItemCount = 0;
		if (this.m_childs != null && this.m_childs.Count > 0)
		{
			foreach (ui_ranking_scroll_dummy ui_ranking_scroll_dummy in this.m_childs)
			{
				if (ui_ranking_scroll_dummy != null)
				{
					ui_ranking_scroll_dummy.DrawClear();
					ui_ranking_scroll_dummy.transform.parent = null;
					ui_ranking_scroll_dummy.gameObject.SetActive(false);
					UnityEngine.Object.Destroy(ui_ranking_scroll_dummy.gameObject);
				}
			}
			Resources.UnloadUnusedAssets();
			this.m_childs = new List<ui_ranking_scroll_dummy>();
		}
		if (this.m_childsOrg != null && this.m_childsOrg.Count > 0)
		{
			foreach (ui_ranking_scroll ui_ranking_scroll in this.m_childsOrg)
			{
				if (ui_ranking_scroll != null)
				{
					ui_ranking_scroll.transform.parent = null;
					ui_ranking_scroll.gameObject.SetActive(false);
					UnityEngine.Object.Destroy(ui_ranking_scroll.gameObject);
				}
			}
			this.m_childsOrg = new List<ui_ranking_scroll>();
		}
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000A4670 File Offset: 0x000A2870
	public void Restart()
	{
		this.InitContainer();
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000A4678 File Offset: 0x000A2878
	public GameObject GetMyDataGameObject()
	{
		GameObject result = null;
		if (this.m_childs != null && this.m_childs.Count > 0)
		{
			foreach (ui_ranking_scroll_dummy ui_ranking_scroll_dummy in this.m_childs)
			{
				if (ui_ranking_scroll_dummy != null && ui_ranking_scroll_dummy.isMyData)
				{
					result = ui_ranking_scroll_dummy.gameObject;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000A471C File Offset: 0x000A291C
	public bool AddItem(int addItem, float delay = 0.02f)
	{
		this.m_drawAllLastPoint = 0f;
		this.m_addItemLastTime = Time.realtimeSinceStartup;
		this.CheckCreate();
		this.m_placeObjects = new List<PlaceObjectData>();
		if (this.template != null && this.templateDummy != null && addItem > 0)
		{
			this.m_placeDelay = delay;
			this.m_placeCurrentTime = 0f;
			if (this.m_childs.Count < this.maxItemCount)
			{
				return false;
			}
			this.maxItemCount += addItem;
			int num = 0;
			int num2 = 0;
			Bounds b = default(Bounds);
			if (this.callback != null)
			{
				for (int i = 0; i < this.m_childs.Count; i++)
				{
					ui_ranking_scroll_dummy ui_ranking_scroll_dummy = this.m_childs[i];
					if (ui_ranking_scroll_dummy != null)
					{
						this.callback(ui_ranking_scroll_dummy, this);
					}
				}
			}
			int count = this.m_childs.Count;
			if (this.isDirectionVertical)
			{
				for (int j = count; j < this.maxItemCount; j++)
				{
					if (delay > 0f)
					{
						this.PlaceAdd(0, j, num + count, b);
					}
					else
					{
						this.Place(0, j, num + count, b);
					}
					num2++;
					num++;
				}
			}
			else
			{
				for (int k = count; k < this.maxItemCount; k++)
				{
					if (delay > 0f)
					{
						this.PlaceAdd(0, k, num + count, b);
					}
					else
					{
						this.Place(k, 0, num + count, b);
					}
					num2++;
					num++;
				}
			}
			this.m_initCountainer = true;
			return true;
		}
		else
		{
			if (this.template != null && this.templateDummy != null && this.callback != null)
			{
				for (int l = 0; l < this.m_childs.Count; l++)
				{
					ui_ranking_scroll_dummy ui_ranking_scroll_dummy2 = this.m_childs[l];
					if (ui_ranking_scroll_dummy2 != null)
					{
						this.callback(ui_ranking_scroll_dummy2, this);
					}
				}
				return true;
			}
			return false;
		}
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000A4950 File Offset: 0x000A2B50
	private void InitContainer()
	{
		this.m_drawAllLastPoint = 0f;
		this.m_drawLastIndex = 0;
		this.m_drawStartIndex = 0;
		if (this.parentPanel != null)
		{
			this.parentPanel.panel.clipRange = new Vector4(0f, 0f, this.parentPanel.panel.clipRange.z, this.parentPanel.panel.clipRange.w);
			this.parentPanel.transform.localPosition = new Vector3(this.parentPanel.transform.localPosition.x, 0f, this.parentPanel.transform.localPosition.z);
			Vector4 clipRange = this.parentPanel.panel.clipRange;
			this.drawArea = new Vector3(clipRange.z * this.drawAreaScale, clipRange.w * this.drawAreaScale, 0f);
		}
		if (this.template != null && this.templateDummy != null && this.maxItemCount > 0)
		{
			int num = 0;
			Bounds b = default(Bounds);
			if (this.isDirectionVertical)
			{
				for (int i = 0; i < this.maxItemCount; i++)
				{
					this.Place(0, i, num, b);
					num++;
				}
			}
			else
			{
				for (int j = 0; j < this.maxItemCount; j++)
				{
					this.Place(j, 0, num, b);
					num++;
				}
			}
			this.m_initCountainer = true;
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000A4B00 File Offset: 0x000A2D00
	private bool CheckItemUpdate()
	{
		if (this.m_childs == null)
		{
			return true;
		}
		bool result = true;
		int num = this.m_drawLastIndex - 320;
		if (num < 0)
		{
			num = 0;
		}
		for (int i = num; i < this.m_childs.Count; i++)
		{
			ui_ranking_scroll_dummy ui_ranking_scroll_dummy = this.m_childs[i];
			if (ui_ranking_scroll_dummy != null && ui_ranking_scroll_dummy.gameObject.activeSelf && ui_ranking_scroll_dummy.enabled)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000A4B8C File Offset: 0x000A2D8C
	public void CheckItemDrawAllAuto(bool isNext, int num = 20)
	{
		if (this.IsCreating(0f))
		{
			return;
		}
		if (this.GetFreeObjectNum() < 15)
		{
			this.CheckItemDrawAll(isNext);
			return;
		}
		global::Debug.Log(" + CheckItemDrawAllAuto  isNext:" + isNext);
		int num2 = 0;
		int num3 = this.m_drawStartIndex;
		int num4 = this.m_drawLastIndex;
		if (isNext)
		{
			num4 += num;
		}
		else
		{
			num3 -= num;
		}
		if (num4 > this.m_childs.Count)
		{
			num4 = this.m_childs.Count;
		}
		if (num3 < 0)
		{
			num3 = 0;
		}
		this.m_drawStartIndex = this.m_drawLastIndex;
		this.m_drawLastIndex = num3;
		if (!isNext)
		{
			this.m_drawStartIndex = this.m_drawLastIndex;
			this.m_drawLastIndex = 0;
		}
		for (int i = num3; i < num4; i++)
		{
			int num5 = i;
			ui_ranking_scroll_dummy ui_ranking_scroll_dummy = this.m_childs[num5];
			if (ui_ranking_scroll_dummy != null)
			{
				bool flag = this.CheckItemDraw(ui_ranking_scroll_dummy.gameObject);
				if (flag && !ui_ranking_scroll_dummy.gameObject.activeSelf)
				{
					num2++;
					if (num2 == 1 && num5 > 0)
					{
						ui_ranking_scroll_dummy.top = true;
					}
					else
					{
						ui_ranking_scroll_dummy.top = false;
					}
					ui_ranking_scroll_dummy.SetActiveObject(flag, 0.05f * (float)num2);
				}
				else if (!flag && ui_ranking_scroll_dummy.gameObject.activeSelf)
				{
					ui_ranking_scroll_dummy.SetActiveObject(flag, 0f);
				}
				if (flag)
				{
					if (this.m_drawLastIndex < num5)
					{
						this.m_drawLastIndex = num5;
					}
					if (this.m_drawStartIndex > num5)
					{
						this.m_drawStartIndex = num5;
					}
				}
			}
		}
		if (this.parentPanel != null && this.parentPanel.panel != null)
		{
			if (this.isDirectionVertical)
			{
				this.m_drawAllLastPoint = this.parentPanel.panel.clipRange.y;
			}
			else
			{
				this.m_drawAllLastPoint = this.parentPanel.panel.clipRange.x;
			}
		}
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000A4DB0 File Offset: 0x000A2FB0
	public void CheckItemDrawAll(bool isNext)
	{
		if (this.IsCreating(0f))
		{
			return;
		}
		int num = this.m_drawStartIndex - 80;
		if (!isNext)
		{
			num = this.m_drawStartIndex + 80;
			if (num >= this.m_childs.Count)
			{
				num = this.m_childs.Count - 1;
			}
		}
		if (num < 0)
		{
			num = 0;
		}
		int num2 = 0;
		int num3 = this.m_childs.Count - num;
		if (!isNext)
		{
			num3 = num + 1;
			if (num3 > 240)
			{
				num3 = 240;
			}
		}
		else if (num3 > 240)
		{
			num3 = 240;
		}
		this.m_drawStartIndex = this.m_drawLastIndex;
		this.m_drawLastIndex = num;
		if (!isNext)
		{
			this.m_drawStartIndex = num;
			this.m_drawLastIndex = 0;
		}
		for (int i = 0; i < num3; i++)
		{
			int num4 = i + num;
			if (!isNext)
			{
				num4 = num - i;
			}
			if (num4 < 0 || num4 >= this.m_childs.Count)
			{
				break;
			}
			ui_ranking_scroll_dummy ui_ranking_scroll_dummy = this.m_childs[num4];
			if (ui_ranking_scroll_dummy != null)
			{
				bool flag = this.CheckItemDraw(ui_ranking_scroll_dummy.gameObject);
				if (flag && !ui_ranking_scroll_dummy.gameObject.activeSelf)
				{
					num2++;
				}
				if (num2 == 1 && num4 > 0)
				{
					ui_ranking_scroll_dummy.top = true;
				}
				else
				{
					ui_ranking_scroll_dummy.top = false;
				}
				ui_ranking_scroll_dummy.SetActiveObject(flag, 0.05f * (float)num2);
				if (flag)
				{
					if (this.m_drawLastIndex < num4)
					{
						this.m_drawLastIndex = num4;
					}
					if (this.m_drawStartIndex > num4)
					{
						this.m_drawStartIndex = num4;
					}
				}
			}
		}
		if (this.parentPanel != null && this.parentPanel.panel != null)
		{
			if (this.isDirectionVertical)
			{
				this.m_drawAllLastPoint = this.parentPanel.panel.clipRange.y;
			}
			else
			{
				this.m_drawAllLastPoint = this.parentPanel.panel.clipRange.x;
			}
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000A4FE4 File Offset: 0x000A31E4
	private bool CheckMove(out bool isNext, float move = 400f)
	{
		bool flag = false;
		isNext = false;
		if (this.parentPanel != null && this.parentPanel.panel != null)
		{
			if (this.isDirectionVertical)
			{
				if (Mathf.Abs(this.parentPanel.panel.clipRange.y - this.m_drawAllLastPoint) >= move)
				{
					isNext = (this.parentPanel.panel.clipRange.y - this.m_drawAllLastPoint < 0f);
					flag = true;
				}
			}
			else if (Mathf.Abs(this.parentPanel.panel.clipRange.x - this.m_drawAllLastPoint) >= move)
			{
				isNext = (this.parentPanel.panel.clipRange.x - this.m_drawAllLastPoint < 0f);
				flag = true;
			}
			if (flag && this.m_addItemLastTime != 0f && Mathf.Abs(this.m_addItemLastTime - Time.realtimeSinceStartup) <= 2f)
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000A510C File Offset: 0x000A330C
	public bool CheckItemDraw(int slot)
	{
		bool flag = false;
		if (this.rankingType == RankingUtil.RankingRankerType.RIVAL)
		{
			return true;
		}
		if (this.parentPanel != null)
		{
			Vector3 localPosition = this.parentPanel.gameObject.transform.localPosition;
			float f = localPosition.x + this.GetPosX(slot);
			float f2 = localPosition.y + this.GetPosY(slot);
			if (this.isDirectionVertical)
			{
				if (Mathf.Abs(f2) < this.drawArea.y + (float)(this.spacing * 2))
				{
					flag = true;
				}
			}
			else if (Mathf.Abs(f) < this.drawArea.x + (float)(this.spacing * 2))
			{
				flag = true;
			}
		}
		if (this.m_drawLastIndex < slot && flag)
		{
			this.m_drawLastIndex = slot;
		}
		return flag;
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x000A51E0 File Offset: 0x000A33E0
	public bool CheckItemDraw(GameObject go)
	{
		bool result = false;
		if (this.rankingType == RankingUtil.RankingRankerType.RIVAL)
		{
			return true;
		}
		if (this.parentPanel != null)
		{
			Vector3 localPosition = this.parentPanel.gameObject.transform.localPosition;
			float f = localPosition.x + go.transform.localPosition.x;
			float f2 = localPosition.y + go.transform.localPosition.y;
			if (this.isDirectionVertical)
			{
				if (Mathf.Abs(f2) < this.drawArea.y + (float)(this.spacing * 2))
				{
					result = true;
				}
			}
			else if (Mathf.Abs(f) < this.drawArea.x + (float)(this.spacing * 2))
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x000A52B4 File Offset: 0x000A34B4
	public void Strip()
	{
		while (this.m_childs.Count > this.maxItemCount)
		{
			if (this.m_childs.Count > 0)
			{
				GameObject gameObject = this.m_childs[this.m_childs.Count - 1].gameObject;
				this.m_childs.RemoveAt(this.m_childs.Count - 1);
				gameObject.transform.parent = null;
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x04001959 RID: 6489
	public const float NEXT_BAR_SIZE = 0.8f;

	// Token: 0x0400195A RID: 6490
	public const int NEXT_LOAD_LINE = 25;

	// Token: 0x0400195B RID: 6491
	public const int MAX_ORG_ITEM_LIMIT = 80;

	// Token: 0x0400195C RID: 6492
	public const float ADD_CREATE_DELAY = 0.02f;

	// Token: 0x0400195D RID: 6493
	public RankingUtil.RankingRankerType rankingType = RankingUtil.RankingRankerType.RIVAL;

	// Token: 0x0400195E RID: 6494
	public UIRectItemStorageRanking.CallbackCreated callback;

	// Token: 0x0400195F RID: 6495
	public UIRectItemStorageRanking.CallbackTopOrLast callbackTopOrLast;

	// Token: 0x04001960 RID: 6496
	public UIDraggablePanel parentPanel;

	// Token: 0x04001961 RID: 6497
	private float drawAreaScale = 5f;

	// Token: 0x04001962 RID: 6498
	private Vector3 drawArea = default(Vector3);

	// Token: 0x04001963 RID: 6499
	private int m_drawLastIndex;

	// Token: 0x04001964 RID: 6500
	private int m_drawStartIndex;

	// Token: 0x04001965 RID: 6501
	[SerializeField]
	private bool isDirectionVertical;

	// Token: 0x04001966 RID: 6502
	public int maxItemCount = 8;

	// Token: 0x04001967 RID: 6503
	public GameObject template;

	// Token: 0x04001968 RID: 6504
	public GameObject templateDummy;

	// Token: 0x04001969 RID: 6505
	public int spacing = 128;

	// Token: 0x0400196A RID: 6506
	public int padding = 10;

	// Token: 0x0400196B RID: 6507
	private float m_currentBarSize;

	// Token: 0x0400196C RID: 6508
	private List<UIInvGameItem> mItems = new List<UIInvGameItem>();

	// Token: 0x0400196D RID: 6509
	private List<PlaceObjectData> m_placeObjects;

	// Token: 0x0400196E RID: 6510
	private float m_placeDelay;

	// Token: 0x0400196F RID: 6511
	private float m_placeCurrentTime;

	// Token: 0x04001970 RID: 6512
	private float m_createLastTime;

	// Token: 0x04001971 RID: 6513
	private float m_addItemLastTime;

	// Token: 0x04001972 RID: 6514
	private float m_drawAllLastPoint;

	// Token: 0x04001973 RID: 6515
	public UIRectItemStorageRanking.ActiveType m_activeType = UIRectItemStorageRanking.ActiveType.DEFAULT;

	// Token: 0x04001974 RID: 6516
	private bool m_initCountainer;

	// Token: 0x04001975 RID: 6517
	private List<ui_ranking_scroll_dummy> m_childs = new List<ui_ranking_scroll_dummy>();

	// Token: 0x04001976 RID: 6518
	private List<ui_ranking_scroll> m_childsOrg = new List<ui_ranking_scroll>();

	// Token: 0x020003B8 RID: 952
	public enum ActiveType
	{
		// Token: 0x04001978 RID: 6520
		ACTIVE,
		// Token: 0x04001979 RID: 6521
		NOT_ACTTIVE,
		// Token: 0x0400197A RID: 6522
		DEFAULT
	}

	// Token: 0x02000A86 RID: 2694
	// (Invoke) Token: 0x06004852 RID: 18514
	public delegate void CallbackCreated(ui_ranking_scroll_dummy obj, UIRectItemStorageRanking storage);

	// Token: 0x02000A87 RID: 2695
	// (Invoke) Token: 0x06004856 RID: 18518
	public delegate bool CallbackTopOrLast(bool isTop);
}
