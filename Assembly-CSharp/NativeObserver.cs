using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x020009CC RID: 2508
public class NativeObserver : MonoBehaviour
{
	// Token: 0x170008F0 RID: 2288
	// (get) Token: 0x060041CC RID: 16844 RVA: 0x00156490 File Offset: 0x00154690
	public static int ProductCount
	{
		get
		{
			return ServerInterface.RedStarItemList.Count;
		}
	}

	// Token: 0x060041CD RID: 16845 RVA: 0x0015649C File Offset: 0x0015469C
	public string GetProductName(int productIndex)
	{
		if (productIndex >= NativeObserver.ProductCount)
		{
			return null;
		}
		return ServerInterface.RedStarItemList[productIndex].m_productId;
	}

	// Token: 0x170008F1 RID: 2289
	// (get) Token: 0x060041CE RID: 16846 RVA: 0x001564BC File Offset: 0x001546BC
	// (set) Token: 0x060041CF RID: 16847 RVA: 0x001564F8 File Offset: 0x001546F8
	public static NativeObserver Instance
	{
		get
		{
			if (NativeObserver.instance != null && !NativeObserver.instance.initialized)
			{
				NativeObserver.instance.StartInAppPurchase();
			}
			return NativeObserver.instance;
		}
		private set
		{
		}
	}

	// Token: 0x170008F2 RID: 2290
	// (get) Token: 0x060041D0 RID: 16848 RVA: 0x001564FC File Offset: 0x001546FC
	// (set) Token: 0x060041D1 RID: 16849 RVA: 0x00156504 File Offset: 0x00154704
	public static bool IsBusy { get; set; }

	// Token: 0x060041D2 RID: 16850 RVA: 0x0015650C File Offset: 0x0015470C
	private void Awake()
	{
		if (NativeObserver.instance == null)
		{
			NativeObserver.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x00156540 File Offset: 0x00154740
	public string GetProductPrice(string productId)
	{
		return Binding.Instance.GetProductInfoPrice(productId);
	}

	// Token: 0x060041D4 RID: 16852 RVA: 0x00156550 File Offset: 0x00154750
	public void StartInAppPurchase()
	{
		Binding.Instance.CreateInAppPurchase("Observer");
		this.initialized = true;
	}

	// Token: 0x060041D5 RID: 16853 RVA: 0x00156568 File Offset: 0x00154768
	public void CheckCurrentTransaction()
	{
		this.Log("NativeObserver : CheckCurrentTransaction");
		Binding.Instance.FinishTransaction(string.Empty);
		string[] array = this.ReciptGet();
		if (array != null)
		{
			foreach (string text in array)
			{
				if (text != null)
				{
					global::Debug.Log("receipts=" + text);
				}
			}
		}
		if (array != null && array.Length > 0)
		{
			this._StartBuyFlow(array[0]);
		}
	}

	// Token: 0x060041D6 RID: 16854 RVA: 0x001565EC File Offset: 0x001547EC
	public void RequestProductsInfo(List<string> productList, NativeObserver.IAPDelegate del)
	{
		if (this.productInfoEnable)
		{
			if (del != null)
			{
				del(NativeObserver.IAPResult.ProductsRequestCompleted);
			}
			return;
		}
		this.iapDelegate = del;
		if (this.isIAPurchasing)
		{
			if (this.iapDelegate != null)
			{
				this.iapDelegate(NativeObserver.IAPResult.PaymentTransactionFailed);
			}
			return;
		}
		base.StartCoroutine(this.GetPriceAsync(productList));
	}

	// Token: 0x060041D7 RID: 16855 RVA: 0x0015664C File Offset: 0x0015484C
	public void ResetIapDelegate()
	{
		this.iapDelegate = null;
	}

	// Token: 0x060041D8 RID: 16856 RVA: 0x00156658 File Offset: 0x00154858
	public void BuyProduct(string productId, NativeObserver.PurchaseSuccessCallback successCallback, NativeObserver.PurchaseFailedCallback failCallback, Action cancelCallback)
	{
		if (this.isIAPurchasing)
		{
			if (failCallback != null)
			{
				failCallback(NativeObserver.FailStatus.Disable);
			}
			return;
		}
		NetMonitor netMonitor = NetMonitor.Instance;
		if (netMonitor != null)
		{
			netMonitor.StartMonitor(null);
		}
		Binding.Instance.BuyProduct(productId);
		this.purchaseSuccessCallback = successCallback;
		this.purchaseFailedCallback = failCallback;
		this.purchaseCancelCallback = cancelCallback;
	}

	// Token: 0x060041D9 RID: 16857 RVA: 0x001566B8 File Offset: 0x001548B8
	public void OnBeforePurchaseFinishedSuccess(string message)
	{
		this.Log("NativeObserver : OnBeforePurchaseFinishedSuccess :" + message);
		this.ReciptPush(message);
	}

	// Token: 0x060041DA RID: 16858 RVA: 0x001566D4 File Offset: 0x001548D4
	public void OnPurchaseFinishedSuccess(string message)
	{
		this.Log("NativeObserver : OnPurchaseFinishedSuccess :" + message);
		NetMonitor netMonitor = NetMonitor.Instance;
		if (netMonitor != null)
		{
			netMonitor.EndMonitorForward(null, null, null);
			netMonitor.EndMonitorBackward();
		}
		this._StartBuyFlow(message);
	}

	// Token: 0x060041DB RID: 16859 RVA: 0x0015671C File Offset: 0x0015491C
	private void _StartBuyFlow(string message)
	{
		string[] array = message.Split(new char[]
		{
			','
		});
		if (array == null || array.Length < 3)
		{
			global::Debug.Log("NativeObserver._StartBuyFlow:no Reciept or invalid Reciept");
			if (this.purchaseCancelCallback != null)
			{
				this.purchaseCancelCallback();
			}
			this.ClearCallback();
			this.OnStopBusy();
		}
		else
		{
			global::Debug.Log("NativeObserver._StartBuyFlow:Retry send reciept");
			this.ExecCpChargeBuyCommit(array[0], WWW.UnEscapeURL(array[1]), WWW.UnEscapeURL(array[2]), message);
			this.OnStopBusy();
		}
	}

	// Token: 0x060041DC RID: 16860 RVA: 0x001567A8 File Offset: 0x001549A8
	public void OnPurchaseFinishedCancel(string message)
	{
		this.Log("NativeObserver : OnPurchaseFinishedCancel :" + message);
		if (this.purchaseCancelCallback != null)
		{
			this.purchaseCancelCallback();
		}
		this.ClearCallback();
		this.OnStopBusy();
		NetMonitor netMonitor = NetMonitor.Instance;
		if (netMonitor != null)
		{
			netMonitor.EndMonitorForward(null, null, null);
			netMonitor.EndMonitorBackward();
		}
	}

	// Token: 0x060041DD RID: 16861 RVA: 0x0015680C File Offset: 0x00154A0C
	public void OnPurchaseFinishedFailed(string message)
	{
		this.Log("NativeObserver : OnPurchaseFinishedFailed :" + message);
		if (this.purchaseFailedCallback != null)
		{
			this.purchaseFailedCallback(NativeObserver.FailStatus.AppStoreFailed);
		}
		this.ClearCallback();
		this.OnStopBusy();
		NetMonitor netMonitor = NetMonitor.Instance;
		if (netMonitor != null)
		{
			netMonitor.EndMonitorForward(null, null, null);
			netMonitor.EndMonitorBackward();
		}
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x00156870 File Offset: 0x00154A70
	private void ExecCpChargeBuyCommit(string productId, string json, string sign, string message)
	{
		this.Log(string.Format("NativeObserver : ExecCpChargeBuyCommit :{0} , {1}, {2}", productId, json, sign));
		this.m_processingReceipt = message;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			this.consumingProductId = productId;
			loggedInServerInterface.RequestServerBuyAndroid(json, sign, base.gameObject);
		}
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x001568C0 File Offset: 0x00154AC0
	private void OnStartBusy()
	{
		global::Debug.Log("NativeObserver : StartBusy");
		this.isIAPurchasing = true;
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x001568D4 File Offset: 0x00154AD4
	private void OnStopBusy()
	{
		global::Debug.Log("NativeObserver : StopBusy");
		this.isIAPurchasing = false;
	}

	// Token: 0x060041E1 RID: 16865 RVA: 0x001568E8 File Offset: 0x00154AE8
	private void Log(string log)
	{
		global::Debug.Log(log);
	}

	// Token: 0x060041E2 RID: 16866 RVA: 0x001568F0 File Offset: 0x00154AF0
	private void ClearCallback()
	{
		this.purchaseSuccessCallback = null;
		this.purchaseFailedCallback = null;
		this.purchaseCancelCallback = null;
	}

	// Token: 0x060041E3 RID: 16867 RVA: 0x00156908 File Offset: 0x00154B08
	private void ReciptPush(string recipt)
	{
		SystemSaveManager systemSaveManager = SystemSaveManager.Instance;
		if (systemSaveManager != null)
		{
			SystemData systemdata = systemSaveManager.GetSystemdata();
			if (systemdata != null)
			{
				string purchasedRecipt = systemdata.purchasedRecipt;
				if (string.IsNullOrEmpty(purchasedRecipt))
				{
					systemdata.purchasedRecipt = recipt;
				}
				else
				{
					systemdata.purchasedRecipt = purchasedRecipt + "@" + recipt;
				}
				systemSaveManager.SaveSystemData();
				global::Debug.Log("NativeObserver.ReciptPush:" + systemdata.purchasedRecipt);
			}
		}
	}

	// Token: 0x060041E4 RID: 16868 RVA: 0x00156980 File Offset: 0x00154B80
	private string[] ReciptGet()
	{
		SystemSaveManager systemSaveManager = SystemSaveManager.Instance;
		if (systemSaveManager != null)
		{
			SystemData systemdata = systemSaveManager.GetSystemdata();
			if (systemdata != null)
			{
				string purchasedRecipt = systemdata.purchasedRecipt;
				global::Debug.Log("NativeObserver ReciptGet: " + purchasedRecipt);
				if (!string.IsNullOrEmpty(purchasedRecipt))
				{
					return purchasedRecipt.Split(new char[]
					{
						'@'
					});
				}
			}
		}
		return null;
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x001569E4 File Offset: 0x00154BE4
	private void ReciptDelete(string recipt)
	{
		SystemSaveManager systemSaveManager = SystemSaveManager.Instance;
		if (systemSaveManager != null)
		{
			SystemData systemdata = systemSaveManager.GetSystemdata();
			if (systemdata != null)
			{
				string purchasedRecipt = systemdata.purchasedRecipt;
				if (string.IsNullOrEmpty(purchasedRecipt))
				{
					return;
				}
				string[] array = purchasedRecipt.Split(new char[]
				{
					'@'
				});
				string text = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					if (!(array[i] == recipt))
					{
						if (text.Length > 0)
						{
							text += "@";
						}
						text += array[i];
					}
				}
				if (text.Length > 0)
				{
					systemdata.purchasedRecipt = text;
					systemSaveManager.SaveSystemData();
				}
				else
				{
					systemdata.purchasedRecipt = text;
					systemSaveManager.SaveSystemData();
				}
			}
		}
	}

	// Token: 0x060041E6 RID: 16870 RVA: 0x00156AC0 File Offset: 0x00154CC0
	private IEnumerator GetPriceAsync(List<string> productList)
	{
		for (int index = 0; index < productList.Count; index++)
		{
			string productName = productList[index];
			if (productName != null)
			{
				DateTime startTime = DateTime.Now;
				for (;;)
				{
					string price = this.GetProductPrice(productName);
					if (!string.IsNullOrEmpty(price))
					{
						break;
					}
					DateTime currentTime = DateTime.Now;
					if ((float)(currentTime - startTime).Seconds >= 10f)
					{
						break;
					}
					yield return null;
				}
			}
		}
		this.productInfoEnable = true;
		if (this.iapDelegate != null)
		{
			this.iapDelegate(NativeObserver.IAPResult.ProductsRequestCompleted);
		}
		yield break;
	}

	// Token: 0x060041E7 RID: 16871 RVA: 0x00156AEC File Offset: 0x00154CEC
	private void ServerBuyAndroid_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.ReciptDelete(this.m_processingReceipt);
		int rsrType = 0;
		for (int i = 0; i < NativeObserver.ProductCount; i++)
		{
			string productName = this.GetProductName(i);
			if (productName == this.consumingProductId)
			{
				rsrType = i;
				break;
			}
		}
		FoxManager.SendLtvPointBuyRSR(rsrType);
		this.consumingProductId = string.Empty;
		if (this.purchaseSuccessCallback != null)
		{
			this.purchaseSuccessCallback(0);
		}
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x00156B68 File Offset: 0x00154D68
	private void ServerBuyAndroid_Failed(MsgServerConnctFailed msg)
	{
		if (msg.m_status == ServerInterface.StatusCode.AlreadyProcessedReceipt)
		{
			int rsrType = 0;
			for (int i = 0; i < NativeObserver.ProductCount; i++)
			{
				string productName = this.GetProductName(i);
				if (productName == this.consumingProductId)
				{
					rsrType = i;
					break;
				}
			}
			FoxManager.SendLtvPointBuyRSR(rsrType);
			this.ReciptDelete(this.m_processingReceipt);
		}
		if (this.purchaseFailedCallback != null)
		{
			this.purchaseFailedCallback(NativeObserver.FailStatus.ServerFailed);
		}
	}

	// Token: 0x0400381F RID: 14367
	private NativeObserver.IAPDelegate iapDelegate;

	// Token: 0x04003820 RID: 14368
	private bool isIAPurchasing;

	// Token: 0x04003821 RID: 14369
	private NativeObserver.PurchaseSuccessCallback purchaseSuccessCallback;

	// Token: 0x04003822 RID: 14370
	private NativeObserver.PurchaseFailedCallback purchaseFailedCallback;

	// Token: 0x04003823 RID: 14371
	private Action purchaseCancelCallback;

	// Token: 0x04003824 RID: 14372
	private bool initialized;

	// Token: 0x04003825 RID: 14373
	private bool productInfoEnable;

	// Token: 0x04003826 RID: 14374
	private string consumingProductId = string.Empty;

	// Token: 0x04003827 RID: 14375
	private string m_processingReceipt = string.Empty;

	// Token: 0x04003828 RID: 14376
	private static NativeObserver instance;

	// Token: 0x020009CD RID: 2509
	public enum IAPResult
	{
		// Token: 0x0400382B RID: 14379
		ProductsRequestCompleted,
		// Token: 0x0400382C RID: 14380
		ProductsRequestInvalid,
		// Token: 0x0400382D RID: 14381
		PaymentTransactionPurchasing,
		// Token: 0x0400382E RID: 14382
		PaymentTransactionPurchased,
		// Token: 0x0400382F RID: 14383
		PaymentTransactionFailed,
		// Token: 0x04003830 RID: 14384
		PaymentTransactionRestored
	}

	// Token: 0x020009CE RID: 2510
	public enum FailStatus
	{
		// Token: 0x04003832 RID: 14386
		Disable,
		// Token: 0x04003833 RID: 14387
		AppStoreFailed,
		// Token: 0x04003834 RID: 14388
		ServerFailed,
		// Token: 0x04003835 RID: 14389
		Deferred
	}

	// Token: 0x02000AA4 RID: 2724
	// (Invoke) Token: 0x060048CA RID: 18634
	public delegate void IAPDelegate(NativeObserver.IAPResult result);

	// Token: 0x02000AA5 RID: 2725
	// (Invoke) Token: 0x060048CE RID: 18638
	public delegate void PurchaseSuccessCallback(int scValue);

	// Token: 0x02000AA6 RID: 2726
	// (Invoke) Token: 0x060048D2 RID: 18642
	public delegate void PurchaseFailedCallback(NativeObserver.FailStatus status);
}
