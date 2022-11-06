using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000089 RID: 137
[AddComponentMenu("NGUI/Interaction/Grid")]
[ExecuteInEditMode]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x0600038F RID: 911 RVA: 0x000113B8 File Offset: 0x0000F5B8
	private void Start()
	{
		this.mStarted = true;
		this.Reposition();
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000113C8 File Offset: 0x0000F5C8
	private void Update()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000113E4 File Offset: 0x0000F5E4
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000113F8 File Offset: 0x0000F5F8
	public void Reposition()
	{
		if (!this.mStarted)
		{
			this.repositionNow = true;
			return;
		}
		Transform transform = base.transform;
		int num = 0;
		int num2 = 0;
		if (this.sorted)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
				{
					list.Add(child);
				}
			}
			list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Transform transform2 = list[j];
				if (NGUITools.GetActive(transform2.gameObject) || !this.hideInactive)
				{
					float z = transform2.localPosition.z;
					transform2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
				j++;
			}
		}
		else
		{
			for (int k = 0; k < transform.childCount; k++)
			{
				Transform child2 = transform.GetChild(k);
				if (NGUITools.GetActive(child2.gameObject) || !this.hideInactive)
				{
					float z2 = child2.localPosition.z;
					child2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z2) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z2));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
			}
		}
		UIDraggablePanel uidraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uidraggablePanel != null)
		{
			uidraggablePanel.UpdateScrollbars(true);
		}
	}

	// Token: 0x04000256 RID: 598
	public UIGrid.Arrangement arrangement;

	// Token: 0x04000257 RID: 599
	public int maxPerLine;

	// Token: 0x04000258 RID: 600
	public float cellWidth = 200f;

	// Token: 0x04000259 RID: 601
	public float cellHeight = 200f;

	// Token: 0x0400025A RID: 602
	public bool repositionNow;

	// Token: 0x0400025B RID: 603
	public bool sorted;

	// Token: 0x0400025C RID: 604
	public bool hideInactive = true;

	// Token: 0x0400025D RID: 605
	private bool mStarted;

	// Token: 0x0200008A RID: 138
	public enum Arrangement
	{
		// Token: 0x0400025F RID: 607
		Horizontal,
		// Token: 0x04000260 RID: 608
		Vertical
	}
}
