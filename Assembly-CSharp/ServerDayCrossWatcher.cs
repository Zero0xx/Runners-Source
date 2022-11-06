using System;
using Message;
using UnityEngine;

// Token: 0x020007CE RID: 1998
public class ServerDayCrossWatcher : MonoBehaviour
{
	// Token: 0x17000739 RID: 1849
	// (get) Token: 0x060034A6 RID: 13478 RVA: 0x0011DA98 File Offset: 0x0011BC98
	public static ServerDayCrossWatcher Instance
	{
		get
		{
			return ServerDayCrossWatcher.m_instance;
		}
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x0011DAA0 File Offset: 0x0011BCA0
	public bool IsDayCross()
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		return DateTime.Compare(currentTime, this.m_nextGetInfoTime) >= 0;
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x0011DAC8 File Offset: 0x0011BCC8
	public bool IsDaylyMissionEnd()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			DateTime endDailyMissionDate = ServerInterface.PlayerState.m_endDailyMissionDate;
			if (DateTime.Compare(currentTime, endDailyMissionDate) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x0011DB08 File Offset: 0x0011BD08
	public bool IsDaylyMissionChallengeEnd()
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		DateTime t = currentTime.AddDays(1.0);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			t = ServerInterface.DailyChallengeState.m_chalEndTime;
		}
		return DateTime.Compare(currentTime, t) >= 0;
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x0011DB5C File Offset: 0x0011BD5C
	public bool IsLoginBonusDayCross()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			if (DateTime.Compare(currentTime, this.m_nextGetLoginBonusTime) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x0011DB98 File Offset: 0x0011BD98
	public void UpdateClientInfosByDayCross(ServerDayCrossWatcher.UpdateInfoCallback callback)
	{
		if (callback == null)
		{
			return;
		}
		this.m_callbackDayCross = callback;
		if (!this.IsDayCross())
		{
			if (this.m_callbackDayCross != null)
			{
				this.m_callbackDayCross(new ServerDayCrossWatcher.MsgDayCross());
				this.m_callbackDayCross = null;
			}
			return;
		}
		this.CalcNextGetInfoTime();
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWheelOptions(base.gameObject);
		}
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x0011DC08 File Offset: 0x0011BE08
	public void UpdateDailyMissionForOneDay(ServerDayCrossWatcher.UpdateInfoCallback callback)
	{
		if (callback == null)
		{
			return;
		}
		this.m_callbackDailyMissionForOneDay = callback;
		if (!this.IsDaylyMissionEnd())
		{
			if (this.m_callbackDailyMissionForOneDay != null)
			{
				this.m_callbackDailyMissionForOneDay(new ServerDayCrossWatcher.MsgDayCross());
				this.m_callbackDailyMissionForOneDay = null;
			}
			return;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerRetrievePlayerState(base.gameObject);
		}
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x0011DC70 File Offset: 0x0011BE70
	public void UpdateDailyMissionInfoByChallengeEnd(ServerDayCrossWatcher.UpdateInfoCallback callback)
	{
		if (callback == null)
		{
			return;
		}
		this.m_callbackDailyMission = callback;
		if (!this.IsDaylyMissionChallengeEnd())
		{
			if (this.m_callbackDailyMission != null)
			{
				this.m_callbackDailyMission(new ServerDayCrossWatcher.MsgDayCross());
				this.m_callbackDailyMission = null;
			}
			return;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyMissionData(base.gameObject);
		}
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x0011DCD8 File Offset: 0x0011BED8
	public void UpdateLoginBonusEnd(ServerDayCrossWatcher.UpdateInfoCallback callback)
	{
		if (callback == null)
		{
			return;
		}
		this.m_callbackLoginBonus = callback;
		if (!this.IsLoginBonusDayCross())
		{
			if (this.m_callbackLoginBonus != null)
			{
				this.m_callbackLoginBonus(new ServerDayCrossWatcher.MsgDayCross());
				this.m_callbackLoginBonus = null;
			}
			return;
		}
		this.CalcNextGetLoginBonusTime();
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerLoginBonus(base.gameObject);
		}
	}

	// Token: 0x060034AF RID: 13487 RVA: 0x0011DD48 File Offset: 0x0011BF48
	private void CalcNextGetInfoTime()
	{
		DateTime dateTime = DateTime.Today.AddHours((double)ServerDayCrossWatcher.DayCrossHour).AddMinutes((double)ServerDayCrossWatcher.DayCrossMinute);
		DateTime currentTime = NetBase.GetCurrentTime();
		if (DateTime.Compare(currentTime, dateTime) < 0)
		{
			this.m_nextGetInfoTime = dateTime;
		}
		else
		{
			DateTime nextGetInfoTime = dateTime.AddDays(1.0);
			this.m_nextGetInfoTime = nextGetInfoTime;
		}
	}

	// Token: 0x060034B0 RID: 13488 RVA: 0x0011DDB0 File Offset: 0x0011BFB0
	private void CalcNextGetLoginBonusTime()
	{
		DateTime dateTime = DateTime.Today.AddHours((double)ServerDayCrossWatcher.DayCrossHour).AddMinutes((double)ServerDayCrossWatcher.DayCrossMinute);
		DateTime currentTime = NetBase.GetCurrentTime();
		if (DateTime.Compare(currentTime, dateTime) < 0)
		{
			dateTime.AddMinutes((double)UnityEngine.Random.Range(1, 30));
			this.m_nextGetInfoTime = dateTime;
		}
		else
		{
			DateTime nextGetInfoTime = dateTime.AddDays(1.0);
			nextGetInfoTime.AddMinutes((double)UnityEngine.Random.Range(1, 30));
			this.m_nextGetInfoTime = nextGetInfoTime;
		}
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x0011DE38 File Offset: 0x0011C038
	private void Start()
	{
		if (ServerDayCrossWatcher.m_instance == null)
		{
			ServerDayCrossWatcher.m_instance = this;
			this.m_nextGetInfoTime = NetUtil.GetLocalDateTime(0L);
			this.m_nextGetLoginBonusTime = NetUtil.GetLocalDateTime(0L);
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060034B2 RID: 13490 RVA: 0x0011DE90 File Offset: 0x0011C090
	private void OnDestroy()
	{
		if (ServerDayCrossWatcher.m_instance == this)
		{
			ServerDayCrossWatcher.m_instance = null;
		}
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
	private void Update()
	{
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x0011DEAC File Offset: 0x0011C0AC
	private void ServerGetWheelOptions_Succeeded(MsgGetWheelOptionsSucceed msg)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCampaignList(base.gameObject);
		}
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x0011DED8 File Offset: 0x0011C0D8
	private void GetCampaignList_Succeeded(MsgGetCampaignListSucceed msg)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventList(base.gameObject);
		}
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x0011DF04 File Offset: 0x0011C104
	private void ServerGetEventList_Succeeded(MsgGetEventListSucceed msg)
	{
		if (this.m_callbackDayCross != null)
		{
			ServerDayCrossWatcher.MsgDayCross msgDayCross = new ServerDayCrossWatcher.MsgDayCross();
			msgDayCross.ServerConnect = true;
			this.m_callbackDayCross(msgDayCross);
			this.m_callbackDayCross = null;
		}
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x0011DF3C File Offset: 0x0011C13C
	private void ServerGetDailyMissionData_Succeeded(MsgGetDailyMissionDataSucceed msg)
	{
		if (this.m_callbackDailyMission != null)
		{
			ServerDayCrossWatcher.MsgDayCross msgDayCross = new ServerDayCrossWatcher.MsgDayCross();
			msgDayCross.ServerConnect = true;
			this.m_callbackDailyMission(msgDayCross);
			this.m_callbackDailyMission = null;
		}
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x0011DF74 File Offset: 0x0011C174
	private void ServerRetrievePlayerState_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		if (this.m_callbackDailyMissionForOneDay != null)
		{
			ServerDayCrossWatcher.MsgDayCross msgDayCross = new ServerDayCrossWatcher.MsgDayCross();
			msgDayCross.ServerConnect = true;
			this.m_callbackDailyMissionForOneDay(msgDayCross);
			this.m_callbackDailyMissionForOneDay = null;
		}
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x0011DFAC File Offset: 0x0011C1AC
	private void ServerLoginBonus_Succeeded(MsgLoginBonusSucceed msg)
	{
		if (this.m_callbackLoginBonus != null)
		{
			ServerDayCrossWatcher.MsgDayCross msgDayCross = new ServerDayCrossWatcher.MsgDayCross();
			msgDayCross.ServerConnect = true;
			this.m_callbackLoginBonus(msgDayCross);
			this.m_callbackLoginBonus = null;
		}
	}

	// Token: 0x04002C5A RID: 11354
	private static ServerDayCrossWatcher m_instance;

	// Token: 0x04002C5B RID: 11355
	private static readonly int DayCrossHour = 15;

	// Token: 0x04002C5C RID: 11356
	private static readonly int DayCrossMinute;

	// Token: 0x04002C5D RID: 11357
	private DateTime m_nextGetInfoTime;

	// Token: 0x04002C5E RID: 11358
	private DateTime m_nextGetLoginBonusTime;

	// Token: 0x04002C5F RID: 11359
	private ServerDayCrossWatcher.UpdateInfoCallback m_callbackDayCross;

	// Token: 0x04002C60 RID: 11360
	private ServerDayCrossWatcher.UpdateInfoCallback m_callbackDailyMission;

	// Token: 0x04002C61 RID: 11361
	private ServerDayCrossWatcher.UpdateInfoCallback m_callbackDailyMissionForOneDay;

	// Token: 0x04002C62 RID: 11362
	private ServerDayCrossWatcher.UpdateInfoCallback m_callbackLoginBonus;

	// Token: 0x020007CF RID: 1999
	public class MsgDayCross
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x0011DFEC File Offset: 0x0011C1EC
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x0011DFF4 File Offset: 0x0011C1F4
		public bool ServerConnect { get; set; }
	}

	// Token: 0x02000AA2 RID: 2722
	// (Invoke) Token: 0x060048C2 RID: 18626
	public delegate void UpdateInfoCallback(ServerDayCrossWatcher.MsgDayCross msg);
}
