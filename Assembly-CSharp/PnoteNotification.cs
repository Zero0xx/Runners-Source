using System;
using App.Utility;
using SaveData;

// Token: 0x0200082D RID: 2093
public class PnoteNotification
{
	// Token: 0x060038CE RID: 14542 RVA: 0x0012D610 File Offset: 0x0012B810
	public static void RequestRegister()
	{
		if (SystemSaveManager.Instance != null)
		{
			string gameID = SystemSaveManager.GetGameID();
			if (!string.IsNullOrEmpty(gameID) && !gameID.Equals("0"))
			{
				Binding.Instance.RegistPnote(gameID);
			}
		}
	}

	// Token: 0x060038CF RID: 14543 RVA: 0x0012D65C File Offset: 0x0012B85C
	public static void RequestUnregister()
	{
		Binding.Instance.UnregistPnote();
	}

	// Token: 0x060038D0 RID: 14544 RVA: 0x0012D668 File Offset: 0x0012B868
	public static void SendMessage(string message, string reciever, PnoteNotification.LaunchOption option)
	{
		if (SystemSaveManager.Instance != null)
		{
			string gameID = SystemSaveManager.GetGameID();
			if (!string.IsNullOrEmpty(gameID) && !gameID.Equals("0"))
			{
				string launchOption = PnoteNotification.LaunchString[(int)option];
				Binding.Instance.SendMessagePnote(message, gameID, reciever, launchOption);
			}
		}
	}

	// Token: 0x060038D1 RID: 14545 RVA: 0x0012D6BC File Offset: 0x0012B8BC
	public static bool CheckEnableGetNoLoginIncentive()
	{
		string text = Binding.Instance.GetPnoteLaunchString();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		text = text.ToLower();
		return text.Contains(PnoteNotification.LaunchString[2]);
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x0012D700 File Offset: 0x0012B900
	public static void RegistTagsPnote(Bitset32 tag_bit)
	{
		string gameID = SystemSaveManager.GetGameID();
		string text = string.Empty;
		if (tag_bit.Test(0))
		{
			text = "1";
		}
		else
		{
			text = "0";
		}
		for (int i = 1; i < 5; i++)
		{
			if (tag_bit.Test(i))
			{
				text += ",1";
			}
			else
			{
				text += ",0";
			}
		}
		Binding.Instance.RegistTagsPnote(text, gameID);
	}

	// Token: 0x04002F8B RID: 12171
	private static readonly string[] LaunchString = new string[]
	{
		"None",
		"SendEnergy",
		"nologin"
	};

	// Token: 0x0200082E RID: 2094
	public enum LaunchOption
	{
		// Token: 0x04002F8D RID: 12173
		None,
		// Token: 0x04002F8E RID: 12174
		SendEnergy,
		// Token: 0x04002F8F RID: 12175
		NoLogin
	}
}
