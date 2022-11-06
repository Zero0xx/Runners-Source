using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[AddComponentMenu("NGUI/Examples/Drag and Drop Item")]
public class DragDropItem : MonoBehaviour
{
	// Token: 0x060002CE RID: 718 RVA: 0x0000C56C File Offset: 0x0000A76C
	private void UpdateTable()
	{
		UITable uitable = NGUITools.FindInParents<UITable>(base.gameObject);
		if (uitable != null)
		{
			uitable.repositionNow = true;
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000C598 File Offset: 0x0000A798
	private void Drop()
	{
		Collider collider = UICamera.lastHit.collider;
		DragDropContainer dragDropContainer = (!(collider != null)) ? null : collider.gameObject.GetComponent<DragDropContainer>();
		if (dragDropContainer != null)
		{
			this.mTrans.parent = dragDropContainer.transform;
			Vector3 localPosition = this.mTrans.localPosition;
			localPosition.z = 0f;
			this.mTrans.localPosition = localPosition;
		}
		else
		{
			this.mTrans.parent = this.mParent;
		}
		UIWidget[] componentsInChildren = base.GetComponentsInChildren<UIWidget>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].depth = componentsInChildren[i].depth - 100;
		}
		this.UpdateTable();
		NGUITools.MarkParentAsChanged(base.gameObject);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000C66C File Offset: 0x0000A86C
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0000C67C File Offset: 0x0000A87C
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && UICamera.currentTouchID == this.mTouchID && base.enabled)
		{
			if (!this.mIsDragging)
			{
				this.mIsDragging = true;
				this.mParent = this.mTrans.parent;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.mTrans.gameObject);
				if (DragDropRoot.root != null)
				{
					this.mTrans.parent = DragDropRoot.root;
				}
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
				UIWidget[] componentsInChildren = base.GetComponentsInChildren<UIWidget>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].depth = componentsInChildren[i].depth + 100;
				}
				NGUITools.MarkParentAsChanged(base.gameObject);
			}
			else
			{
				this.mTrans.localPosition += delta * this.mRoot.pixelSizeAdjustment;
			}
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0000C798 File Offset: 0x0000A998
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (isPressed)
			{
				if (this.mPressed)
				{
					return;
				}
				this.mPressed = true;
				this.mTouchID = UICamera.currentTouchID;
				if (!UICamera.current.stickyPress)
				{
					this.mSticky = true;
					UICamera.current.stickyPress = true;
				}
			}
			else
			{
				this.mPressed = false;
				if (this.mSticky)
				{
					this.mSticky = false;
					UICamera.current.stickyPress = false;
				}
			}
			this.mIsDragging = false;
			Collider collider = base.collider;
			if (collider != null)
			{
				collider.enabled = !isPressed;
			}
			if (!isPressed)
			{
				this.Drop();
			}
		}
	}

	// Token: 0x0400017E RID: 382
	public GameObject prefab;

	// Token: 0x0400017F RID: 383
	private Transform mTrans;

	// Token: 0x04000180 RID: 384
	private bool mPressed;

	// Token: 0x04000181 RID: 385
	private int mTouchID;

	// Token: 0x04000182 RID: 386
	private bool mIsDragging;

	// Token: 0x04000183 RID: 387
	private bool mSticky;

	// Token: 0x04000184 RID: 388
	private Transform mParent;

	// Token: 0x04000185 RID: 389
	private UIRoot mRoot;
}
