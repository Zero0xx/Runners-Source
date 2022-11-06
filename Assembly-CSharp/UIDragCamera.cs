using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	// Token: 0x06000350 RID: 848 RVA: 0x0000F114 File Offset: 0x0000D314
	private void Awake()
	{
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000F144 File Offset: 0x0000D344
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0000F18C File Offset: 0x0000D38C
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04000204 RID: 516
	public UIDraggableCamera draggableCamera;
}
