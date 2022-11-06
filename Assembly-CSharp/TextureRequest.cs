using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public abstract class TextureRequest
{
	// Token: 0x0600076F RID: 1903
	public abstract void LoadDone(Texture tex);

	// Token: 0x06000770 RID: 1904
	public abstract bool IsEnableLoad();

	// Token: 0x06000771 RID: 1905
	public abstract string GetFileName();
}
