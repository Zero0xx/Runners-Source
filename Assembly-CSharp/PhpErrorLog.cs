using System;
using UnityEngine;

// Token: 0x02000690 RID: 1680
internal class PhpErrorLog : MonoBehaviour
{
	// Token: 0x06002CE5 RID: 11493 RVA: 0x0010DF20 File Offset: 0x0010C120
	public static PhpErrorLog Create(string errorText)
	{
		GameObject gameObject = new GameObject("PhpErrorLog");
		PhpErrorLog phpErrorLog = gameObject.AddComponent<PhpErrorLog>();
		phpErrorLog.mErrorText = "!!! PHP ERROR : " + errorText;
		return phpErrorLog;
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x0010DF54 File Offset: 0x0010C154
	private void Update()
	{
		global::Debug.Log(this.mErrorText);
	}

	// Token: 0x0400298B RID: 10635
	private string mErrorText = string.Empty;
}
