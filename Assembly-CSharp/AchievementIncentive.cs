using System;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class AchievementIncentive : MonoBehaviour
{
	// Token: 0x06000FF0 RID: 4080 RVA: 0x0005DC0C File Offset: 0x0005BE0C
	private void Start()
	{
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0005DC10 File Offset: 0x0005BE10
	public AchievementIncentive.State GetState()
	{
		return this.m_state;
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0005DC18 File Offset: 0x0005BE18
	public void RequestServer()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			int achievementIncentiveCount = AchievementIncentive.GetAchievementIncentiveCount();
			if (achievementIncentiveCount > 0)
			{
				loggedInServerInterface.RequestServerGetFacebookIncentive(3, achievementIncentiveCount, base.gameObject);
				this.m_state = AchievementIncentive.State.Request;
			}
		}
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0005DC5C File Offset: 0x0005BE5C
	private void ServerGetFacebookIncentive_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		AchievementIncentive.ResetAchievementIncentiveCount();
		if (SaveDataManager.Instance != null && SaveDataManager.Instance.ConnectData != null)
		{
			SaveDataManager.Instance.ConnectData.ReplaceMessageBox = true;
		}
		this.m_state = AchievementIncentive.State.Succeeded;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0005DCA4 File Offset: 0x0005BEA4
	private void ServerGetFacebookIncentive_Failed(MsgServerConnctFailed msg)
	{
		this.m_state = AchievementIncentive.State.Failed;
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0005DCB0 File Offset: 0x0005BEB0
	private static SystemData GetSystemSaveData()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			return instance.GetSystemdata();
		}
		return null;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0005DCD8 File Offset: 0x0005BED8
	private static int GetAchievementIncentiveCount()
	{
		SystemData systemSaveData = AchievementIncentive.GetSystemSaveData();
		if (systemSaveData != null)
		{
			return systemSaveData.achievementIncentiveCount;
		}
		return 0;
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0005DCFC File Offset: 0x0005BEFC
	public static void AddAchievementIncentiveCount(int add)
	{
		if (add > 0)
		{
			SystemData systemSaveData = AchievementIncentive.GetSystemSaveData();
			if (systemSaveData != null)
			{
				systemSaveData.achievementIncentiveCount += add;
				AchievementIncentive.Save();
			}
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0005DD30 File Offset: 0x0005BF30
	private static void ResetAchievementIncentiveCount()
	{
		SystemData systemSaveData = AchievementIncentive.GetSystemSaveData();
		if (systemSaveData != null)
		{
			systemSaveData.achievementIncentiveCount = 0;
			AchievementIncentive.Save();
		}
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0005DD58 File Offset: 0x0005BF58
	private static void Save()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			instance.SaveSystemData();
		}
	}

	// Token: 0x04000DA6 RID: 3494
	private AchievementIncentive.State m_state;

	// Token: 0x02000241 RID: 577
	public enum State
	{
		// Token: 0x04000DA8 RID: 3496
		Idle,
		// Token: 0x04000DA9 RID: 3497
		Request,
		// Token: 0x04000DAA RID: 3498
		Succeeded,
		// Token: 0x04000DAB RID: 3499
		Failed
	}
}
