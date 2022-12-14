using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009A RID: 154
[AddComponentMenu("NGUI/Interaction/Table")]
[ExecuteInEditMode]
public class UITable : UIWidgetContainer
{
	// Token: 0x0600040A RID: 1034 RVA: 0x0001488C File Offset: 0x00012A8C
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600040B RID: 1035 RVA: 0x000148A0 File Offset: 0x00012AA0
	public List<Transform> children
	{
		get
		{
			if (this.mChildren.Count == 0)
			{
				Transform transform = base.transform;
				this.mChildren.Clear();
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (child && child.gameObject && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
					{
						this.mChildren.Add(child);
					}
				}
				if (this.sorted)
				{
					this.mChildren.Sort(new Comparison<Transform>(UITable.SortByName));
				}
			}
			return this.mChildren;
		}
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00014958 File Offset: 0x00012B58
	private void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + (bounds2.extents.y - bounds2.center.y);
				localPosition.y -= (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00014D10 File Offset: 0x00012F10
	public void Reposition()
	{
		if (this.mStarted)
		{
			Transform transform = base.transform;
			this.mChildren.Clear();
			List<Transform> children = this.children;
			if (children.Count > 0)
			{
				this.RepositionVariableSize(children);
			}
			if (this.mDrag != null)
			{
				this.mDrag.UpdateScrollbars(true);
				this.mDrag.RestrictWithinBounds(true);
			}
			else if (this.mPanel != null)
			{
				this.mPanel.ConstrainTargetToBounds(transform, true);
			}
			if (this.onReposition != null)
			{
				this.onReposition();
			}
		}
		else
		{
			this.repositionNow = true;
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00014DC4 File Offset: 0x00012FC4
	private void Start()
	{
		this.mStarted = true;
		if (this.keepWithinPanel)
		{
			this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			this.mDrag = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
		this.Reposition();
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00014E0C File Offset: 0x0001300C
	private void LateUpdate()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x040002DE RID: 734
	public int columns;

	// Token: 0x040002DF RID: 735
	public UITable.Direction direction;

	// Token: 0x040002E0 RID: 736
	public bool sorted;

	// Token: 0x040002E1 RID: 737
	public bool hideInactive = true;

	// Token: 0x040002E2 RID: 738
	public bool keepWithinPanel;

	// Token: 0x040002E3 RID: 739
	public bool repositionNow;

	// Token: 0x040002E4 RID: 740
	public UITable.OnReposition onReposition;

	// Token: 0x040002E5 RID: 741
	public Vector2 padding = Vector2.zero;

	// Token: 0x040002E6 RID: 742
	private UIPanel mPanel;

	// Token: 0x040002E7 RID: 743
	private UIDraggablePanel mDrag;

	// Token: 0x040002E8 RID: 744
	private bool mStarted;

	// Token: 0x040002E9 RID: 745
	private List<Transform> mChildren = new List<Transform>();

	// Token: 0x0200009B RID: 155
	public enum Direction
	{
		// Token: 0x040002EB RID: 747
		Down,
		// Token: 0x040002EC RID: 748
		Up
	}

	// Token: 0x02000A66 RID: 2662
	// (Invoke) Token: 0x060047D2 RID: 18386
	public delegate void OnReposition();
}
