using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000027 RID: 39
	public interface RealTimeMultiplayerListener
	{
		// Token: 0x06000136 RID: 310
		void OnRoomSetupProgress(float percent);

		// Token: 0x06000137 RID: 311
		void OnRoomConnected(bool success);

		// Token: 0x06000138 RID: 312
		void OnLeftRoom();

		// Token: 0x06000139 RID: 313
		void OnPeersConnected(string[] participantIds);

		// Token: 0x0600013A RID: 314
		void OnPeersDisconnected(string[] participantIds);

		// Token: 0x0600013B RID: 315
		void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);
	}
}
