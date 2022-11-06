using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x060002A3 RID: 675 RVA: 0x0000B7D0 File Offset: 0x000099D0
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = (UnityEngine.Object.Instantiate(this.mPrefab, transform.position, transform.rotation) as GameObject);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000136 RID: 310
	public InvBaseItem.Slot slot;

	// Token: 0x04000137 RID: 311
	private GameObject mPrefab;

	// Token: 0x04000138 RID: 312
	private GameObject mChild;
}
