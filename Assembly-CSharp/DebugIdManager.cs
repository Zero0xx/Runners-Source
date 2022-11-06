using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using App;
using SaveData;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class DebugIdManager : MonoBehaviour
{
	// Token: 0x06000C39 RID: 3129 RVA: 0x00046350 File Offset: 0x00044550
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00046360 File Offset: 0x00044560
	private void Start()
	{
		this.actionServerType = Env.actionServerType.ToString();
		this.LoadFile();
		this.SetLoadLabel();
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(gameObject, "Btn_mainmenu");
			if (boxCollider != null)
			{
				boxCollider.center -= new Vector3(300f, 0f, 0f);
			}
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x000463E4 File Offset: 0x000445E4
	private void SetLoadLabel()
	{
		if (this.m_userDataDict.ContainsKey(this.actionServerType))
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ID Load\n\n");
			stringBuilder.Append(this.m_userDataDict[this.actionServerType].id);
			stringBuilder.Append("\n");
			stringBuilder.Append(this.actionServerType);
			stringBuilder.Append("\n");
			stringBuilder.Append(this.m_userDataDict[this.actionServerType].date);
			this.m_loadLabel = stringBuilder.ToString();
		}
		else
		{
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("ID Load\n\n");
			stringBuilder2.Append("----------\n");
			stringBuilder2.Append(this.actionServerType);
			stringBuilder2.Append("\n--/-- --:--:--");
			this.m_loadLabel = stringBuilder2.ToString();
		}
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x000464CC File Offset: 0x000446CC
	private void OnGUI()
	{
		if (GUI.Button(new Rect((float)(Screen.width - 150), (float)(Screen.height / 2 - 80), 140f, 60f), this.m_saveLabel) && SystemSaveManager.Instance != null)
		{
			if (SystemSaveManager.GetGameID() == "0")
			{
				global::Debug.LogWarning("Game ID has not been set!");
				return;
			}
			if (this.m_userDataDict.ContainsKey(this.actionServerType))
			{
				this.m_userDataDict[this.actionServerType].id = SystemSaveManager.GetGameID();
				this.m_userDataDict[this.actionServerType].pass = SystemSaveManager.GetGamePassword();
				this.m_userDataDict[this.actionServerType].date = DateTime.Now.ToString("MM/dd HH:mm:ss");
			}
			else
			{
				this.m_userDataDict.Add(this.actionServerType, new DebugIdManager.UserData(SystemSaveManager.GetGameID(), SystemSaveManager.GetGamePassword(), DateTime.Now.ToString("MM/dd HH:mm:ss")));
			}
			this.SaveFile();
			this.SetLoadLabel();
		}
		if (GUI.Button(new Rect((float)(Screen.width - 150), (float)(Screen.height / 2), 140f, 90f), this.m_loadLabel) && SystemSaveManager.Instance != null && this.m_userDataDict.ContainsKey(this.actionServerType))
		{
			SystemSaveManager.SetGameID(this.m_userDataDict[this.actionServerType].id);
			SystemSaveManager.SetGamePassword(this.m_userDataDict[this.actionServerType].pass);
			SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
			if (systemdata != null)
			{
				SystemSaveManager.Instance.SaveSystemData();
			}
			HudMenuUtility.GoToTitleScene();
		}
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x000466A8 File Offset: 0x000448A8
	private void SaveFile()
	{
		string path = Application.persistentDataPath + "/" + this.SAVE_FILE_NAME;
		StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8);
		if (streamWriter != null)
		{
			foreach (string text in this.m_userDataDict.Keys)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(",");
				stringBuilder.Append(this.m_userDataDict[text].id);
				stringBuilder.Append(",");
				stringBuilder.Append(this.m_userDataDict[text].pass);
				stringBuilder.Append(",");
				stringBuilder.Append(this.m_userDataDict[text].date);
				stringBuilder.Append("\n");
				streamWriter.Write(stringBuilder.ToString());
			}
			streamWriter.Close();
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x000467D8 File Offset: 0x000449D8
	private void LoadFile()
	{
		string path = Application.persistentDataPath + "/" + this.SAVE_FILE_NAME;
		if (File.Exists(path))
		{
			StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
			if (streamReader != null)
			{
				while (streamReader.Peek() >= 0)
				{
					string text = streamReader.ReadLine();
					string[] array = text.Split(new char[]
					{
						','
					});
					if (array != null && array.Length > 3)
					{
						this.m_userDataDict.Add(array[0], new DebugIdManager.UserData(array[1], array[2], array[3]));
					}
				}
				streamReader.Close();
			}
		}
	}

	// Token: 0x0400099E RID: 2462
	private readonly string SAVE_FILE_NAME = "DebugId.txt";

	// Token: 0x0400099F RID: 2463
	private Dictionary<string, DebugIdManager.UserData> m_userDataDict = new Dictionary<string, DebugIdManager.UserData>();

	// Token: 0x040009A0 RID: 2464
	private string actionServerType;

	// Token: 0x040009A1 RID: 2465
	private string m_saveLabel = "ID Save";

	// Token: 0x040009A2 RID: 2466
	private string m_loadLabel;

	// Token: 0x020001AC RID: 428
	private class UserData
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00046874 File Offset: 0x00044A74
		public UserData(string id, string pass, string date)
		{
			this.id = id;
			this.pass = pass;
			this.date = date;
		}

		// Token: 0x040009A3 RID: 2467
		public string id;

		// Token: 0x040009A4 RID: 2468
		public string pass;

		// Token: 0x040009A5 RID: 2469
		public string date;
	}
}
