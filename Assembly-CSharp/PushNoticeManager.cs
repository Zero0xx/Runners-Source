using System;
using System.Diagnostics;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020009CF RID: 2511
public class PushNoticeManager : MonoBehaviour
{
	// Token: 0x060041EB RID: 16875 RVA: 0x00156BF4 File Offset: 0x00154DF4
	private void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x060041EC RID: 16876 RVA: 0x00156C00 File Offset: 0x00154E00
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		LocalNotification.Initialize();
	}

	// Token: 0x170008F3 RID: 2291
	// (get) Token: 0x060041ED RID: 16877 RVA: 0x00156C10 File Offset: 0x00154E10
	public static PushNoticeManager Instance
	{
		get
		{
			if (PushNoticeManager.instance == null)
			{
				PushNoticeManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(PushNoticeManager)) as PushNoticeManager);
			}
			return PushNoticeManager.instance;
		}
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x00156C4C File Offset: 0x00154E4C
	private bool CheckInstance()
	{
		if (PushNoticeManager.instance == null)
		{
			PushNoticeManager.instance = this;
			return true;
		}
		if (this == PushNoticeManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x060041EF RID: 16879 RVA: 0x00156C90 File Offset: 0x00154E90
	private void OnDestroy()
	{
		if (PushNoticeManager.instance == this)
		{
			PushNoticeManager.instance = null;
		}
	}

	// Token: 0x060041F0 RID: 16880 RVA: 0x00156CA8 File Offset: 0x00154EA8
	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			LocalNotification.OnActive();
		}
		else
		{
			this.PushNotice(PushNoticeManager.GetSecondsToFullChallenge());
		}
	}

	// Token: 0x060041F1 RID: 16881 RVA: 0x00156CC8 File Offset: 0x00154EC8
	private void OnApplicationQuit()
	{
		this.PushNotice(PushNoticeManager.GetSecondsToFullChallenge());
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x00156CD8 File Offset: 0x00154ED8
	private void PushNotice(int secondsToFullChallenge)
	{
		if (SystemSaveManager.Instance != null && SystemSaveManager.Instance.GetSystemdata().pushNoticeFlags.Test(1) && secondsToFullChallenge > 0)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "PushNotice", "challenge_notification").text;
			LocalNotification.RegisterNotification((float)secondsToFullChallenge, text);
		}
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x00156D38 File Offset: 0x00154F38
	private static int GetSecondsToFullChallenge()
	{
		int result = -1;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerSettingState settingState = ServerInterface.SettingState;
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (settingState != null && playerState != null)
			{
				int energyRecoveryMax = settingState.m_energyRecoveryMax;
				result = 0;
				if (NetUtil.GetUnixTime(playerState.m_energyRenewsAt) != 0L && playerState.m_numEnergy < energyRecoveryMax)
				{
					DateTime d = playerState.m_energyRenewsAt.AddSeconds((double)(settingState.m_energyRefreshTime * (long)(energyRecoveryMax - playerState.m_numEnergy - 1)));
					result = (int)(d - NetUtil.GetCurrentTime()).TotalSeconds;
				}
			}
		}
		return result;
	}

	// Token: 0x060041F4 RID: 16884 RVA: 0x00156DD4 File Offset: 0x00154FD4
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x00156DE8 File Offset: 0x00154FE8
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04003836 RID: 14390
	private static PushNoticeManager instance;
}
