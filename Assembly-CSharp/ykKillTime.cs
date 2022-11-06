using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class ykKillTime : MonoBehaviour
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x0004FF10 File Offset: 0x0004E110
	private void Start()
	{
		this.passedTime = 0f;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0004FF20 File Offset: 0x0004E120
	private void Update()
	{
		this.passedTime += Time.deltaTime;
		if (this.passedTime > this.killTime)
		{
			switch (this.killType)
			{
			case ykKillTime.ykKillType.destroy:
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			case ykKillTime.ykKillType.hide:
			{
				Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer in componentsInChildren)
				{
					renderer.enabled = false;
				}
				break;
			}
			case ykKillTime.ykKillType.deactivate:
				base.gameObject.SetActive(false);
				break;
			}
		}
	}

	// Token: 0x04000B78 RID: 2936
	private float passedTime;

	// Token: 0x04000B79 RID: 2937
	public float killTime = 1f;

	// Token: 0x04000B7A RID: 2938
	public ykKillTime.ykKillType killType;

	// Token: 0x020001FF RID: 511
	public enum ykKillType
	{
		// Token: 0x04000B7C RID: 2940
		destroy,
		// Token: 0x04000B7D RID: 2941
		hide,
		// Token: 0x04000B7E RID: 2942
		deactivate
	}
}
