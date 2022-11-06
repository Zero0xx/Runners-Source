using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x0200002B RID: 43
	public interface OnStateLoadedListener
	{
		// Token: 0x0600014B RID: 331
		void OnStateLoaded(bool success, int slot, byte[] data);

		// Token: 0x0600014C RID: 332
		byte[] OnStateConflict(int slot, byte[] localData, byte[] serverData);

		// Token: 0x0600014D RID: 333
		void OnStateSaved(bool success, int slot);
	}
}
