using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x0200001F RID: 31
	public interface ITurnBasedMultiplayerClient
	{
		// Token: 0x06000109 RID: 265
		void CreateQuickMatch(int minOpponents, int maxOpponents, int variant, Action<bool, TurnBasedMatch> callback);

		// Token: 0x0600010A RID: 266
		void CreateWithInvitationScreen(int minOpponents, int maxOpponents, int variant, Action<bool, TurnBasedMatch> callback);

		// Token: 0x0600010B RID: 267
		void AcceptFromInbox(Action<bool, TurnBasedMatch> callback);

		// Token: 0x0600010C RID: 268
		void AcceptInvitation(string invitationId, Action<bool, TurnBasedMatch> callback);

		// Token: 0x0600010D RID: 269
		void RegisterMatchDelegate(MatchDelegate del);

		// Token: 0x0600010E RID: 270
		void TakeTurn(string matchId, byte[] data, string pendingParticipantId, Action<bool> callback);

		// Token: 0x0600010F RID: 271
		int GetMaxMatchDataSize();

		// Token: 0x06000110 RID: 272
		void Finish(string matchId, byte[] data, MatchOutcome outcome, Action<bool> callback);

		// Token: 0x06000111 RID: 273
		void AcknowledgeFinished(string matchId, Action<bool> callback);

		// Token: 0x06000112 RID: 274
		void Leave(string matchId, Action<bool> callback);

		// Token: 0x06000113 RID: 275
		void LeaveDuringTurn(string matchId, string pendingParticipantId, Action<bool> callback);

		// Token: 0x06000114 RID: 276
		void Cancel(string matchId, Action<bool> callback);

		// Token: 0x06000115 RID: 277
		void Rematch(string matchId, Action<bool, TurnBasedMatch> callback);

		// Token: 0x06000116 RID: 278
		void DeclineInvitation(string invitationId);
	}
}
