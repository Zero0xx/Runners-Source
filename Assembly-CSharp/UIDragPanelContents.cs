using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
[AddComponentMenu("NGUI/Interaction/Drag Panel Contents")]
[ExecuteInEditMode]
public class UIDragPanelContents : MonoBehaviour
{
	// Token: 0x0600035B RID: 859 RVA: 0x0000F874 File Offset: 0x0000DA74
	private void Start()
	{
		if (this.draggablePanel == null)
		{
			this.draggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Press(pressed);
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0000F8EC File Offset: 0x0000DAEC
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Drag();
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0000F928 File Offset: 0x0000DB28
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Scroll(delta);
		}
	}

	// Token: 0x04000218 RID: 536
	public UIDraggablePanel draggablePanel;
}
