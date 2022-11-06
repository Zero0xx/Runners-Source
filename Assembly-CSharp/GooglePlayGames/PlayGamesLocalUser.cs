using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	// Token: 0x0200002D RID: 45
	public class PlayGamesLocalUser : PlayGamesUserProfile, IUserProfile, ILocalUser
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00004E64 File Offset: 0x00003064
		internal PlayGamesLocalUser(PlayGamesPlatform plaf)
		{
			this.mPlatform = plaf;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004E74 File Offset: 0x00003074
		public void Authenticate(Action<bool> callback)
		{
			this.mPlatform.Authenticate(callback);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004E84 File Offset: 0x00003084
		public void Authenticate(Action<bool> callback, bool silent)
		{
			this.mPlatform.Authenticate(callback, silent);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004E94 File Offset: 0x00003094
		public void LoadFriends(Action<bool> callback)
		{
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004EA4 File Offset: 0x000030A4
		public IUserProfile[] friends
		{
			get
			{
				return new IUserProfile[0];
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004EAC File Offset: 0x000030AC
		public bool authenticated
		{
			get
			{
				return this.mPlatform.IsAuthenticated();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004EBC File Offset: 0x000030BC
		public bool underage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004EC0 File Offset: 0x000030C0
		public new string userName
		{
			get
			{
				return (!this.authenticated) ? string.Empty : this.mPlatform.GetUserDisplayName();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004EF0 File Offset: 0x000030F0
		public new string id
		{
			get
			{
				return (!this.authenticated) ? string.Empty : this.mPlatform.GetUserId();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004F20 File Offset: 0x00003120
		public new bool isFriend
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004F24 File Offset: 0x00003124
		public new UserState state
		{
			get
			{
				return UserState.Online;
			}
		}

		// Token: 0x04000080 RID: 128
		private PlayGamesPlatform mPlatform;
	}
}
