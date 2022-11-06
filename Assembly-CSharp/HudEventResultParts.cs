using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public abstract class HudEventResultParts : MonoBehaviour
{
	// Token: 0x06001B02 RID: 6914
	public abstract void Init(GameObject resultRootObject, long beforeTotalPoint, HudEventResult.AnimationEndCallback callback);

	// Token: 0x06001B03 RID: 6915
	public abstract void PlayAnimation(HudEventResult.AnimType animType);

	// Token: 0x06001B04 RID: 6916 RVA: 0x0009F9C8 File Offset: 0x0009DBC8
	public virtual bool IsBackkeyEnable()
	{
		return true;
	}
}
