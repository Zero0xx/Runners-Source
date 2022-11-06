using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x02000030 RID: 48
	public class PlayGamesUserProfile : IUserProfile
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00005898 File Offset: 0x00003A98
		internal PlayGamesUserProfile()
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000058A0 File Offset: 0x00003AA0
		public string userName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000058A8 File Offset: 0x00003AA8
		public string id
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000058B0 File Offset: 0x00003AB0
		public bool isFriend
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000058B4 File Offset: 0x00003AB4
		public UserState state
		{
			get
			{
				return UserState.Online;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000058B8 File Offset: 0x00003AB8
		public Texture2D image
		{
			get
			{
				return null;
			}
		}
	}
}
