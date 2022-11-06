using System;
using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace GooglePlayGames
{
	// Token: 0x0200004A RID: 74
	internal class PlayGamesClientFactory
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000AC6C File Offset: 0x00008E6C
		internal static IPlayGamesClient GetPlatformPlayGamesClient()
		{
			if (Application.isEditor)
			{
				return new DummyClient();
			}
			return new AndroidClient();
		}
	}
}
