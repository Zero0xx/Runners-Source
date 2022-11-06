using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x0200001C RID: 28
	public class DummyClient : IPlayGamesClient
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000046C8 File Offset: 0x000028C8
		public void Authenticate(Action<bool> callback, bool silent)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000046D8 File Offset: 0x000028D8
		public bool IsAuthenticated()
		{
			return false;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000046DC File Offset: 0x000028DC
		public void SignOut()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000046E0 File Offset: 0x000028E0
		public string GetUserId()
		{
			return "DummyID";
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000046E8 File Offset: 0x000028E8
		public string GetUserDisplayName()
		{
			return "Player";
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000046F0 File Offset: 0x000028F0
		public List<Achievement> GetAchievements()
		{
			return new List<Achievement>();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000046F8 File Offset: 0x000028F8
		public Achievement GetAchievement(string achId)
		{
			return null;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000046FC File Offset: 0x000028FC
		public void UnlockAchievement(string achId, Action<bool> callback)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000470C File Offset: 0x0000290C
		public void RevealAchievement(string achId, Action<bool> callback)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000471C File Offset: 0x0000291C
		public void IncrementAchievement(string achId, int steps, Action<bool> callback)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000472C File Offset: 0x0000292C
		public void ShowAchievementsUI()
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004730 File Offset: 0x00002930
		public void ShowLeaderboardUI(string lbId)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004734 File Offset: 0x00002934
		public void SubmitScore(string lbId, long score, Action<bool> callback)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004744 File Offset: 0x00002944
		public void LoadState(int slot, OnStateLoadedListener listener)
		{
			if (listener != null)
			{
				listener.OnStateLoaded(false, slot, null);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004758 File Offset: 0x00002958
		public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000475C File Offset: 0x0000295C
		public void SetCloudCacheEncrypter(BufferEncrypter encrypter)
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004760 File Offset: 0x00002960
		public IRealTimeMultiplayerClient GetRtmpClient()
		{
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004764 File Offset: 0x00002964
		public ITurnBasedMultiplayerClient GetTbmpClient()
		{
			return null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004768 File Offset: 0x00002968
		public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000476C File Offset: 0x0000296C
		public Invitation GetInvitationFromNotification()
		{
			return null;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004770 File Offset: 0x00002970
		public bool HasInvitationFromNotification()
		{
			return false;
		}
	}
}
