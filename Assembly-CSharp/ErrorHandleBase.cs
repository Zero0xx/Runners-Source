using System;
using Message;
using UnityEngine;

// Token: 0x02000680 RID: 1664
public abstract class ErrorHandleBase
{
	// Token: 0x06002C64 RID: 11364
	public abstract void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg);

	// Token: 0x06002C65 RID: 11365
	public abstract void StartErrorHandle();

	// Token: 0x06002C66 RID: 11366
	public abstract void Update();

	// Token: 0x06002C67 RID: 11367
	public abstract bool IsEnd();

	// Token: 0x06002C68 RID: 11368
	public abstract void EndErrorHandle();
}
