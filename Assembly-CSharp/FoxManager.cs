using System;
using App;
using SaveData;
using UnityEngine;

// Token: 0x0200023E RID: 574
public class FoxManager : MonoBehaviour
{
	// Token: 0x06000FD6 RID: 4054 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
	private void Awake()
	{
		if (this.CheckInstance())
		{
			if (!Env.isReleaseApplication)
			{
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x0005D8E0 File Offset: 0x0005BAE0
	private void Start()
	{
		FoxManager.m_ltvIDTable = FoxDataTable.LtvIDAndroid;
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0005D8EC File Offset: 0x0005BAEC
	private void Update()
	{
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0005D8F0 File Offset: 0x0005BAF0
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
		}
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0005D900 File Offset: 0x0005BB00
	private void OnApplicationQuit()
	{
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0005D904 File Offset: 0x0005BB04
	public static void SendLtvPoint(FoxLtvType ltvType)
	{
		string text;
		if (!FoxManager.IsEnableSendLtvPoint(out text))
		{
			return;
		}
		int num = FoxManager.m_ltvIDTable[(int)ltvType];
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x0005D928 File Offset: 0x0005BB28
	public static void SendLtvPointMap(int rank)
	{
		string text;
		if (!FoxManager.IsEnableSendLtvPoint(out text))
		{
			return;
		}
		int num = FoxManager.m_ltvIDTable[2];
		FoxPlugin.addParameter("rank", rank.ToString());
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0005D95C File Offset: 0x0005BB5C
	public static void SendLtvPointBuyRSR(int rsrType)
	{
		string text;
		if (!FoxManager.IsEnableSendLtvPoint(out text))
		{
			return;
		}
		int num = FoxManager.m_ltvIDTable[4];
		FoxPlugin.addParameter("rsr", rsrType.ToString());
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0005D990 File Offset: 0x0005BB90
	public static void SendLtvPointPremiumRoulette(bool free)
	{
		string text;
		if (!FoxManager.IsEnableSendLtvPoint(out text))
		{
			return;
		}
		int num = (!free) ? 5 : 6;
		int num2 = FoxManager.m_ltvIDTable[num];
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0005D9C0 File Offset: 0x0005BBC0
	private static bool IsEnableSendLtvPoint(out string userID)
	{
		userID = SystemSaveManager.GetGameID();
		return !string.IsNullOrEmpty(userID) && SystemSaveManager.IsUserIDValid() && (FoxManager.m_ltvIDTable == null || FoxManager.Instance == null) && false;
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0005DA0C File Offset: 0x0005BC0C
	public static FoxManager Instance
	{
		get
		{
			return FoxManager.instance;
		}
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0005DA14 File Offset: 0x0005BC14
	private bool CheckInstance()
	{
		if (FoxManager.instance == null)
		{
			FoxManager.instance = this;
			return true;
		}
		if (this == FoxManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0005DA58 File Offset: 0x0005BC58
	private void OnDestroy()
	{
		if (FoxManager.instance == this)
		{
			FoxManager.instance = null;
		}
	}

	// Token: 0x04000DA1 RID: 3489
	private static int[] m_ltvIDTable;

	// Token: 0x04000DA2 RID: 3490
	private static FoxManager instance;
}
