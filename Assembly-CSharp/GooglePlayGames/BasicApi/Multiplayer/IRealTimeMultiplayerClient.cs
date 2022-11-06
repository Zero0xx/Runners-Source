using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x0200001E RID: 30
	public interface IRealTimeMultiplayerClient
	{
		// Token: 0x060000FB RID: 251
		void CreateQuickGame(int minOpponents, int maxOpponents, int variant, RealTimeMultiplayerListener listener);

		// Token: 0x060000FC RID: 252
		void CreateWithInvitationScreen(int minOpponents, int maxOppponents, int variant, RealTimeMultiplayerListener listener);

		// Token: 0x060000FD RID: 253
		void AcceptFromInbox(RealTimeMultiplayerListener listener);

		// Token: 0x060000FE RID: 254
		void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener);

		// Token: 0x060000FF RID: 255
		void SendMessageToAll(bool reliable, byte[] data);

		// Token: 0x06000100 RID: 256
		void SendMessageToAll(bool reliable, byte[] data, int offset, int length);

		// Token: 0x06000101 RID: 257
		void SendMessage(bool reliable, string participantId, byte[] data);

		// Token: 0x06000102 RID: 258
		void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length);

		// Token: 0x06000103 RID: 259
		List<Participant> GetConnectedParticipants();

		// Token: 0x06000104 RID: 260
		Participant GetSelf();

		// Token: 0x06000105 RID: 261
		Participant GetParticipant(string participantId);

		// Token: 0x06000106 RID: 262
		void LeaveRoom();

		// Token: 0x06000107 RID: 263
		bool IsRoomConnected();

		// Token: 0x06000108 RID: 264
		void DeclineInvitation(string invitationId);
	}
}
