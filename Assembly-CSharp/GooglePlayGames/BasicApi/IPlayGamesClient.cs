using System;
using GooglePlayGames.BasicApi.Multiplayer;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x0200001D RID: 29
	public interface IPlayGamesClient
	{
		// Token: 0x060000E9 RID: 233
		void Authenticate(Action<bool> callback, bool silent);

		// Token: 0x060000EA RID: 234
		bool IsAuthenticated();

		// Token: 0x060000EB RID: 235
		void SignOut();

		// Token: 0x060000EC RID: 236
		string GetUserId();

		// Token: 0x060000ED RID: 237
		string GetUserDisplayName();

		// Token: 0x060000EE RID: 238
		Achievement GetAchievement(string achId);

		// Token: 0x060000EF RID: 239
		void UnlockAchievement(string achId, Action<bool> callback);

		// Token: 0x060000F0 RID: 240
		void RevealAchievement(string achId, Action<bool> callback);

		// Token: 0x060000F1 RID: 241
		void IncrementAchievement(string achId, int steps, Action<bool> callback);

		// Token: 0x060000F2 RID: 242
		void ShowAchievementsUI();

		// Token: 0x060000F3 RID: 243
		void ShowLeaderboardUI(string lbId);

		// Token: 0x060000F4 RID: 244
		void SubmitScore(string lbId, long score, Action<bool> callback);

		// Token: 0x060000F5 RID: 245
		void SetCloudCacheEncrypter(BufferEncrypter encrypter);

		// Token: 0x060000F6 RID: 246
		void LoadState(int slot, OnStateLoadedListener listener);

		// Token: 0x060000F7 RID: 247
		void UpdateState(int slot, byte[] data, OnStateLoadedListener listener);

		// Token: 0x060000F8 RID: 248
		IRealTimeMultiplayerClient GetRtmpClient();

		// Token: 0x060000F9 RID: 249
		ITurnBasedMultiplayerClient GetTbmpClient();

		// Token: 0x060000FA RID: 250
		void RegisterInvitationDelegate(InvitationReceivedDelegate deleg);
	}
}
