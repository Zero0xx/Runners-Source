using System;
using SaveData;
using UnityEngine;

// Token: 0x0200082C RID: 2092
public class LocalNotification
{
	// Token: 0x060038C5 RID: 14533 RVA: 0x0012D4C8 File Offset: 0x0012B6C8
	public static void Initialize()
	{
		LocalNotification.CancelAllNotifications();
	}

	// Token: 0x060038C6 RID: 14534 RVA: 0x0012D4D0 File Offset: 0x0012B6D0
	public static void OnActive()
	{
		LocalNotification.ClearRecieveNotifications();
	}

	// Token: 0x060038C7 RID: 14535 RVA: 0x0012D4D8 File Offset: 0x0012B6D8
	private static void ClearRecieveNotifications()
	{
	}

	// Token: 0x060038C8 RID: 14536 RVA: 0x0012D4DC File Offset: 0x0012B6DC
	public static void EnableNotification(bool value)
	{
		if (value)
		{
			PnoteNotification.RequestRegister();
		}
		else
		{
			PnoteNotification.RequestUnregister();
		}
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x0012D4F4 File Offset: 0x0012B6F4
	public static void RegisterNotification(float second, string message)
	{
		if (SystemSaveManager.Instance != null && !SystemSaveManager.Instance.GetSystemdata().pushNotice)
		{
			return;
		}
		DateTime dateTime = DateTime.Now.AddSeconds((double)second);
		long num = LocalNotification.ToUnixTime(dateTime);
		AndroidJavaObject androidJavaObject = new AndroidJavaObject(BindingAndroid.GetPackageName() + ".gcm.GCMManager", new object[0]);
		androidJavaObject.Call("registLocalNotification", new object[]
		{
			num,
			message
		});
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x0012D578 File Offset: 0x0012B778
	public static void CancelAllNotifications()
	{
		AndroidJavaObject androidJavaObject = new AndroidJavaObject(BindingAndroid.GetPackageName() + ".gcm.GCMManager", new object[0]);
		androidJavaObject.Call("clearAllNotification", new object[0]);
	}

	// Token: 0x060038CB RID: 14539 RVA: 0x0012D5B4 File Offset: 0x0012B7B4
	private static long ToUnixTime(DateTime dateTime)
	{
		dateTime = dateTime.ToUniversalTime();
		return (long)dateTime.Subtract(LocalNotification.UnixEpoch).TotalMilliseconds;
	}

	// Token: 0x04002F8A RID: 12170
	private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
