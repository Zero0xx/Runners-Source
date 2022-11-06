using System;

namespace GooglePlayGames.BasicApi.Multiplayer
{
	// Token: 0x02000026 RID: 38
	public class Player
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00004B0C File Offset: 0x00002D0C
		internal Player(string displayName, string playerId)
		{
			this.mDisplayName = displayName;
			this.mPlayerId = playerId;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004B44 File Offset: 0x00002D44
		public string DisplayName
		{
			get
			{
				return this.mDisplayName;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004B4C File Offset: 0x00002D4C
		public string PlayerId
		{
			get
			{
				return this.mPlayerId;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004B54 File Offset: 0x00002D54
		public override string ToString()
		{
			return string.Format("[Player: '{0}' (id {1})]", this.mDisplayName, this.mPlayerId);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004B6C File Offset: 0x00002D6C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(Player))
			{
				return false;
			}
			Player player = (Player)obj;
			return this.mPlayerId == player.mPlayerId;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public override int GetHashCode()
		{
			return (this.mPlayerId == null) ? 0 : this.mPlayerId.GetHashCode();
		}

		// Token: 0x04000063 RID: 99
		private string mDisplayName = string.Empty;

		// Token: 0x04000064 RID: 100
		private string mPlayerId = string.Empty;
	}
}
