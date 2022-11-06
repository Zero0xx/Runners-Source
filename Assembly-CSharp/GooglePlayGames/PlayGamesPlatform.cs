using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x0200002E RID: 46
	public class PlayGamesPlatform : ISocialPlatform
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00004F28 File Offset: 0x00003128
		private PlayGamesPlatform()
		{
			this.mLocalUser = new PlayGamesLocalUser(this);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004F4C File Offset: 0x0000314C
		public static PlayGamesPlatform Instance
		{
			get
			{
				if (PlayGamesPlatform.sInstance == null)
				{
					PlayGamesPlatform.sInstance = new PlayGamesPlatform();
				}
				return PlayGamesPlatform.sInstance;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004F68 File Offset: 0x00003168
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004F70 File Offset: 0x00003170
		public static bool DebugLogEnabled
		{
			get
			{
				return Logger.DebugLogEnabled;
			}
			set
			{
				Logger.DebugLogEnabled = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004F78 File Offset: 0x00003178
		public IRealTimeMultiplayerClient RealTime
		{
			get
			{
				return this.mClient.GetRtmpClient();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004F88 File Offset: 0x00003188
		public ITurnBasedMultiplayerClient TurnBased
		{
			get
			{
				return this.mClient.GetTbmpClient();
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004F98 File Offset: 0x00003198
		public static PlayGamesPlatform Activate()
		{
			Logger.d("Activating PlayGamesPlatform.");
			Social.Active = PlayGamesPlatform.Instance;
			Logger.d("PlayGamesPlatform activated: " + Social.Active);
			return PlayGamesPlatform.Instance;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004FC8 File Offset: 0x000031C8
		public void AddIdMapping(string fromId, string toId)
		{
			this.mIdMap[fromId] = toId;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004FD8 File Offset: 0x000031D8
		public void Authenticate(Action<bool> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004FE4 File Offset: 0x000031E4
		public void Authenticate(Action<bool> callback, bool silent)
		{
			if (this.mClient == null)
			{
				Logger.d("Creating platform-specific Play Games client.");
				this.mClient = PlayGamesClientFactory.GetPlatformPlayGamesClient();
			}
			this.mClient.Authenticate(callback, silent);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005014 File Offset: 0x00003214
		public void Authenticate(ILocalUser unused, Action<bool> callback)
		{
			this.Authenticate(callback, false);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005020 File Offset: 0x00003220
		public bool IsAuthenticated()
		{
			return this.mClient != null && this.mClient.IsAuthenticated();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000503C File Offset: 0x0000323C
		public void SignOut()
		{
			if (this.mClient != null)
			{
				this.mClient.SignOut();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005054 File Offset: 0x00003254
		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			Logger.w("PlayGamesPlatform.LoadUsers is not implemented.");
			if (callback != null)
			{
				callback(new IUserProfile[0]);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005074 File Offset: 0x00003274
		public string GetUserId()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserId() can only be called after authentication.");
				return "0";
			}
			return this.mClient.GetUserId();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000050A8 File Offset: 0x000032A8
		public string GetUserDisplayName()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("GetUserDisplayName can only be called after authentication.");
				return string.Empty;
			}
			return this.mClient.GetUserDisplayName();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000050DC File Offset: 0x000032DC
		public void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ReportProgress can only be called after authentication.");
				if (callback != null)
				{
					callback(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[]
			{
				"ReportProgress, ",
				achievementID,
				", ",
				progress
			}));
			achievementID = this.MapId(achievementID);
			if (progress < 1E-06)
			{
				Logger.d("Progress 0.00 interpreted as request to reveal.");
				this.mClient.RevealAchievement(achievementID, callback);
				return;
			}
			int num = 0;
			int num2 = 0;
			Achievement achievement = this.mClient.GetAchievement(achievementID);
			bool flag;
			if (achievement == null)
			{
				Logger.w("Unable to locate achievement " + achievementID);
				Logger.w("As a quick fix, assuming it's standard.");
				flag = false;
			}
			else
			{
				flag = achievement.IsIncremental;
				num = achievement.CurrentSteps;
				num2 = achievement.TotalSteps;
				Logger.d("Achievement is " + ((!flag) ? "STANDARD" : "INCREMENTAL"));
				if (flag)
				{
					Logger.d(string.Concat(new object[]
					{
						"Current steps: ",
						num,
						"/",
						num2
					}));
				}
			}
			if (flag)
			{
				Logger.d("Progress " + progress + " interpreted as incremental target (approximate).");
				int num3 = (int)(progress * (double)num2);
				int num4 = num3 - num;
				Logger.d(string.Concat(new object[]
				{
					"Target steps: ",
					num3,
					", cur steps:",
					num
				}));
				Logger.d("Steps to increment: " + num4);
				if (num4 > 0)
				{
					this.mClient.IncrementAchievement(achievementID, num4, callback);
				}
			}
			else
			{
				Logger.d("Progress " + progress + " interpreted as UNLOCK.");
				this.mClient.UnlockAchievement(achievementID, callback);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000052D0 File Offset: 0x000034D0
		public void IncrementAchievement(string achievementID, int steps, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("IncrementAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[]
			{
				"IncrementAchievement: ",
				achievementID,
				", steps ",
				steps
			}));
			achievementID = this.MapId(achievementID);
			this.mClient.IncrementAchievement(achievementID, steps, callback);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005344 File Offset: 0x00003544
		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			Logger.w("PlayGamesPlatform.LoadAchievementDescriptions is not implemented.");
			if (callback != null)
			{
				callback(new IAchievementDescription[0]);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005364 File Offset: 0x00003564
		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			Logger.w("PlayGamesPlatform.LoadAchievements is not implemented.");
			if (callback != null)
			{
				callback(new IAchievement[0]);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005384 File Offset: 0x00003584
		public void LoadAchievementDescriptions(string[] idList, Action<IAchievement[]> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("IncrementAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback(new IAchievement[0]);
				}
				return;
			}
			List<PlayGamesAchievement> list = new List<PlayGamesAchievement>();
			foreach (string text in idList)
			{
				Achievement achievement = this.mClient.GetAchievement(text);
				if (achievement != null)
				{
					PlayGamesAchievement playGamesAchievement = new PlayGamesAchievement();
					playGamesAchievement.id = text;
					if (achievement.IsUnlocked)
					{
						playGamesAchievement.percentCompleted = 100.0;
					}
					else
					{
						playGamesAchievement.percentCompleted = 0.0;
					}
					global::Debug.Log("Add Load AchievementDescriptions" + playGamesAchievement.id);
					list.Add(playGamesAchievement);
				}
			}
			PlayGamesAchievement[] obj = new PlayGamesAchievement[list.Count];
			obj = list.ToArray();
			if (callback != null)
			{
				callback(obj);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005470 File Offset: 0x00003670
		public void LoadAchievements(string[] idList, Action<IAchievement[]> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("IncrementAchievement can only be called after authentication.");
				if (callback != null)
				{
					callback(new IAchievement[0]);
				}
				return;
			}
			List<PlayGamesAchievement> list = new List<PlayGamesAchievement>();
			foreach (string text in idList)
			{
				Achievement achievement = this.mClient.GetAchievement(text);
				if (achievement != null && achievement.IsUnlocked)
				{
					PlayGamesAchievement playGamesAchievement = new PlayGamesAchievement();
					playGamesAchievement.id = text;
					global::Debug.Log("Add Load Achievements" + playGamesAchievement.id);
					playGamesAchievement.percentCompleted = 100.0;
					list.Add(playGamesAchievement);
				}
			}
			PlayGamesAchievement[] obj = new PlayGamesAchievement[list.Count];
			obj = list.ToArray();
			if (callback != null)
			{
				callback(obj);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005548 File Offset: 0x00003748
		public IAchievement CreateAchievement()
		{
			return new PlayGamesAchievement();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005550 File Offset: 0x00003750
		public void ReportScore(long score, string board, Action<bool> callback)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ReportScore can only be called after authentication.");
				if (callback != null)
				{
					callback(false);
				}
				return;
			}
			Logger.d(string.Concat(new object[]
			{
				"ReportScore: score=",
				score,
				", board=",
				board
			}));
			string lbId = this.MapId(board);
			this.mClient.SubmitScore(lbId, score, callback);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000055C4 File Offset: 0x000037C4
		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			Logger.w("PlayGamesPlatform.LoadScores not implemented.");
			if (callback != null)
			{
				callback(new IScore[0]);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000055E4 File Offset: 0x000037E4
		public ILeaderboard CreateLeaderboard()
		{
			Logger.w("PlayGamesPlatform.CreateLeaderboard not implemented. Returning null.");
			return null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000055F4 File Offset: 0x000037F4
		public void ShowAchievementsUI()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ShowAchievementsUI can only be called after authentication.");
				return;
			}
			Logger.d("ShowAchievementsUI");
			this.mClient.ShowAchievementsUI();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005624 File Offset: 0x00003824
		public void ShowLeaderboardUI()
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ShowLeaderboardUI can only be called after authentication.");
				return;
			}
			Logger.d("ShowLeaderboardUI");
			this.mClient.ShowLeaderboardUI(this.MapId(this.mDefaultLbUi));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005668 File Offset: 0x00003868
		public void ShowLeaderboardUI(string lbId)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("ShowLeaderboardUI can only be called after authentication.");
				return;
			}
			Logger.d("ShowLeaderboardUI, lbId=" + lbId);
			if (lbId != null)
			{
				lbId = this.MapId(lbId);
			}
			this.mClient.ShowLeaderboardUI(lbId);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000056B8 File Offset: 0x000038B8
		public void SetDefaultLeaderboardForUI(string lbid)
		{
			Logger.d("SetDefaultLeaderboardForUI: " + lbid);
			if (lbid != null)
			{
				lbid = this.MapId(lbid);
			}
			this.mDefaultLbUi = lbid;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000056EC File Offset: 0x000038EC
		public void LoadFriends(ILocalUser user, Action<bool> callback)
		{
			Logger.w("PlayGamesPlatform.LoadFriends not implemented.");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005708 File Offset: 0x00003908
		public void LoadScores(ILeaderboard board, Action<bool> callback)
		{
			Logger.w("PlayGamesPlatform.LoadScores not implemented.");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005724 File Offset: 0x00003924
		public bool GetLoading(ILeaderboard board)
		{
			return false;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005728 File Offset: 0x00003928
		public void SetCloudCacheEncrypter(BufferEncrypter encrypter)
		{
			this.mClient.SetCloudCacheEncrypter(encrypter);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005738 File Offset: 0x00003938
		public void LoadState(int slot, OnStateLoadedListener listener)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("LoadState can only be called after authentication.");
				if (listener != null)
				{
					listener.OnStateLoaded(false, slot, null);
				}
				return;
			}
			this.mClient.LoadState(slot, listener);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005778 File Offset: 0x00003978
		public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
		{
			if (!this.IsAuthenticated())
			{
				Logger.e("UpdateState can only be called after authentication.");
				if (listener != null)
				{
					listener.OnStateSaved(false, slot);
				}
				return;
			}
			this.mClient.UpdateState(slot, data, listener);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000057B8 File Offset: 0x000039B8
		public ILocalUser localUser
		{
			get
			{
				return this.mLocalUser;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000057C0 File Offset: 0x000039C0
		public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
		{
			this.mClient.RegisterInvitationDelegate(deleg);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000057D0 File Offset: 0x000039D0
		private string MapId(string id)
		{
			if (id == null)
			{
				return null;
			}
			if (this.mIdMap.ContainsKey(id))
			{
				string text = this.mIdMap[id];
				Logger.d("Mapping alias " + id + " to ID " + text);
				return text;
			}
			return id;
		}

		// Token: 0x04000081 RID: 129
		private static PlayGamesPlatform sInstance;

		// Token: 0x04000082 RID: 130
		private PlayGamesLocalUser mLocalUser;

		// Token: 0x04000083 RID: 131
		private IPlayGamesClient mClient;

		// Token: 0x04000084 RID: 132
		private string mDefaultLbUi;

		// Token: 0x04000085 RID: 133
		private Dictionary<string, string> mIdMap = new Dictionary<string, string>();
	}
}
