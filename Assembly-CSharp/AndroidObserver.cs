using System;
using UnityEngine;

// Token: 0x020009CB RID: 2507
public class AndroidObserver : MonoBehaviour
{
	// Token: 0x060041C5 RID: 16837 RVA: 0x001563B8 File Offset: 0x001545B8
	private void Awake()
	{
		if (AndroidObserver.instance == null)
		{
			AndroidObserver.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060041C6 RID: 16838 RVA: 0x001563EC File Offset: 0x001545EC
	public void OnBeforePurchaseFinishedSuccess(string message)
	{
		global::Debug.Log("AndroidObserver : OnBeforePurchaseFinishedSuccess :" + message);
		NativeObserver.Instance.OnBeforePurchaseFinishedSuccess(message);
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x0015640C File Offset: 0x0015460C
	public void OnPurchaseFinishedSuccess(string message)
	{
		global::Debug.Log("AndroidObserver : OnPurchaseFinishedSuccess :" + message);
		NativeObserver.Instance.OnPurchaseFinishedSuccess(message);
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x0015642C File Offset: 0x0015462C
	public void OnPurchaseFinishedFailed(string message)
	{
		global::Debug.Log("AndroidObserver : OnPurchaseFinishedFailed :" + message);
		NativeObserver.Instance.OnPurchaseFinishedFailed(message);
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x0015644C File Offset: 0x0015464C
	public void OnPurchaseFinishedCancel(string message)
	{
		global::Debug.Log("AndroidObserver : OnPurchaseFinishedCancel :" + message);
		NativeObserver.Instance.OnPurchaseFinishedCancel(message);
	}

	// Token: 0x0400381E RID: 14366
	private static AndroidObserver instance;
}
