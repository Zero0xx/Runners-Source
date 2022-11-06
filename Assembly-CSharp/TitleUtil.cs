using System;
using SaveData;

// Token: 0x02000328 RID: 808
public class TitleUtil
{
	// Token: 0x170003AF RID: 943
	// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000885A4 File Offset: 0x000867A4
	public static bool initUser
	{
		get
		{
			return !TitleUtil.IsExistSaveDataGameId() || TitleUtil.s_initUser;
		}
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000885B8 File Offset: 0x000867B8
	public static string GetSystemSaveDataGameId()
	{
		string gameID = SystemSaveManager.GetGameID();
		if (!string.IsNullOrEmpty(gameID))
		{
			return gameID;
		}
		return TitleUtil.FirstGameId;
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000885E0 File Offset: 0x000867E0
	public static bool IsExistSaveDataGameId()
	{
		string systemSaveDataGameId = TitleUtil.GetSystemSaveDataGameId();
		if (systemSaveDataGameId == TitleUtil.FirstGameId)
		{
			TitleUtil.s_initUser = true;
			return false;
		}
		return true;
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x0008860C File Offset: 0x0008680C
	public static bool SetSystemSaveDataGameId(string gameId)
	{
		bool result = false;
		string gameID = SystemSaveManager.GetGameID();
		if (string.IsNullOrEmpty(gameID) || gameID == TitleUtil.FirstGameId)
		{
			SystemSaveManager.SetGameID(gameId);
			result = true;
		}
		return result;
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x00088648 File Offset: 0x00086848
	public static string GetSystemSaveDataPassword()
	{
		string text = SystemSaveManager.GetGamePassword();
		if (text == null)
		{
			text = string.Empty;
		}
		return text;
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00088668 File Offset: 0x00086868
	public static bool SetSystemSaveDataPassword(string password)
	{
		bool result = false;
		if (!string.IsNullOrEmpty(password))
		{
			result = SystemSaveManager.SetGamePassword(password);
		}
		return result;
	}

	// Token: 0x04001581 RID: 5505
	private static bool s_initUser;

	// Token: 0x04001582 RID: 5506
	private static readonly string FirstGameId = "0";
}
