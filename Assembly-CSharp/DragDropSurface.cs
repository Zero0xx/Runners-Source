using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DragDropSurface : MonoBehaviour
{
	// Token: 0x060002D6 RID: 726 RVA: 0x0000C870 File Offset: 0x0000AA70
	private void OnDrop(GameObject go)
	{
		DragDropItem component = go.GetComponent<DragDropItem>();
		if (component != null)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, component.prefab);
			Transform transform = gameObject.transform;
			transform.position = UICamera.lastHit.point;
			if (this.rotatePlacedObject)
			{
				transform.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
			}
			UnityEngine.Object.Destroy(go);
		}
	}

	// Token: 0x04000187 RID: 391
	public bool rotatePlacedObject;
}
