using System;
using UnityEngine;

// Token: 0x02000764 RID: 1892
public class ServerGetVersionRetry : ServerRetryProcess
{
	// Token: 0x06003292 RID: 12946 RVA: 0x0011988C File Offset: 0x00117A8C
	public ServerGetVersionRetry(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x00119898 File Offset: 0x00117A98
	public override void Retry()
	{
		GameObject gameObject = GameObject.Find("ServerInterface");
		if (gameObject != null)
		{
			ServerInterface component = gameObject.GetComponent<ServerInterface>();
			if (component != null)
			{
				component.RequestServerGetVersion(this.m_callbackObject);
			}
		}
	}
}
