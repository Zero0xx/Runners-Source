using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using App;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class DebugServerSelectUI : MonoBehaviour
{
	// Token: 0x06000D87 RID: 3463 RVA: 0x0004F518 File Offset: 0x0004D718
	private void Start()
	{
		this.m_buttonType = DebugServerSelectUI.ButtonType.IDLE;
		this.LoadFile();
		if (this.m_DefaultActionServerType.ContainsKey(CurrentBundleVersion.version))
		{
			string text = this.m_DefaultActionServerType[CurrentBundleVersion.version];
			try
			{
				Env.actionServerType = (Env.ActionServerType)((int)Enum.Parse(typeof(Env.ActionServerType), text));
			}
			catch (ArgumentException ex)
			{
				global::Debug.Log("Load ServerType Error " + text);
			}
		}
		this.m_versionLabel.Append("Ver : ");
		this.m_versionLabel.Append(CurrentBundleVersion.version);
		this.m_serverLabel.Append("Server : ");
		this.m_serverLabel.Append(Env.actionServerType);
		this.m_fontSize = 32;
		this.m_GUIScale = new Vector2((float)Screen.width / this.SCREEN_RECT_SIZE.x, (float)Screen.height / this.SCREEN_RECT_SIZE.y);
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.LOCAL1, this.MatchScreenSize(new Rect(100f, 200f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.LOCAL2, this.MatchScreenSize(new Rect(400f, 200f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.LOCAL3, this.MatchScreenSize(new Rect(700f, 200f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.LOCAL4, this.MatchScreenSize(new Rect(1000f, 200f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.LOCAL5, this.MatchScreenSize(new Rect(1300f, 200f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.DEVELOP1, this.MatchScreenSize(new Rect(100f, 400f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.DEVELOP2, this.MatchScreenSize(new Rect(400f, 400f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.DEVELOP3, this.MatchScreenSize(new Rect(700f, 400f, 250f, 150f)));
		this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.TITLE, this.MatchScreenSize(new Rect(1000f, 600f, 550f, 150f)));
		this.m_hiddenButtonRect = this.MatchScreenSize(new Rect(0f, 0f, 200f, 150f));
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
	private void OnGUI()
	{
		if (!this.m_GUIInited)
		{
			this.m_buttonStyle = new GUIStyle(GUI.skin.button);
			this.m_buttonStyle.fontSize = (int)((float)this.m_fontSize * this.m_GUIScale.x);
			this.m_labelStyle = new GUIStyle(GUI.skin.label);
			this.m_labelStyle.fontSize = (int)((float)this.m_fontSize * this.m_GUIScale.x);
			this.m_labelStyle.alignment = TextAnchor.UpperCenter;
			this.m_GUIInited = true;
		}
		GUI.Label(this.MatchScreenSize(new Rect(0f, 60f, this.SCREEN_RECT_SIZE.x, 100f)), this.m_versionLabel.ToString(), this.m_labelStyle);
		GUI.Label(this.MatchScreenSize(new Rect(0f, 120f, this.SCREEN_RECT_SIZE.x, 100f)), this.m_serverLabel.ToString(), this.m_labelStyle);
		foreach (DebugServerSelectUI.ButtonType buttonType in this.m_ButtonDict.Keys)
		{
			if (buttonType == this.m_buttonType)
			{
				GUI.color = Color.yellow;
				GUI.Box(this.m_ButtonDict[buttonType], string.Empty);
			}
			if (GUI.Button(this.m_ButtonDict[buttonType], this.ButtonName[(int)buttonType], this.m_buttonStyle))
			{
				this.OnClickButton(buttonType);
				this.m_serverLabel.Length = 0;
				this.m_serverLabel.Append("Server : ");
				this.m_serverLabel.Append(Env.actionServerType);
			}
			if (buttonType == this.m_buttonType)
			{
				GUI.color = Color.white;
			}
		}
		if (GUI.Button(this.m_hiddenButtonRect, string.Empty, GUIStyle.none))
		{
			this.m_commandCount++;
			if (this.m_hiddenButtonRect.x == 0f)
			{
				this.m_hiddenButtonRect.x = (float)Screen.width - this.m_hiddenButtonRect.width;
			}
			else
			{
				this.m_hiddenButtonRect.x = 0f;
			}
			if (this.m_commandCount == this.POP_BUTTON_COUNT)
			{
				this.m_ButtonDict.Add(DebugServerSelectUI.ButtonType.RELEASE, this.MatchScreenSize(new Rect(100f, 600f, 250f, 150f)));
			}
		}
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0004FA8C File Offset: 0x0004DC8C
	private void OnClickButton(DebugServerSelectUI.ButtonType type)
	{
		this.m_buttonType = type;
		switch (type)
		{
		case DebugServerSelectUI.ButtonType.LOCAL1:
			Env.actionServerType = Env.ActionServerType.LOCAL1;
			return;
		case DebugServerSelectUI.ButtonType.LOCAL2:
			Env.actionServerType = Env.ActionServerType.LOCAL2;
			return;
		case DebugServerSelectUI.ButtonType.LOCAL3:
			Env.actionServerType = Env.ActionServerType.LOCAL3;
			return;
		case DebugServerSelectUI.ButtonType.LOCAL4:
			Env.actionServerType = Env.ActionServerType.LOCAL4;
			return;
		case DebugServerSelectUI.ButtonType.LOCAL5:
			Env.actionServerType = Env.ActionServerType.LOCAL5;
			return;
		case DebugServerSelectUI.ButtonType.DEVELOP1:
			Env.actionServerType = Env.ActionServerType.DEVELOP;
			return;
		case DebugServerSelectUI.ButtonType.DEVELOP2:
			Env.actionServerType = Env.ActionServerType.DEVELOP2;
			return;
		case DebugServerSelectUI.ButtonType.DEVELOP3:
			Env.actionServerType = Env.ActionServerType.DEVELOP3;
			return;
		case DebugServerSelectUI.ButtonType.RELEASE:
			Env.actionServerType = Env.ActionServerType.RELEASE;
			return;
		case DebugServerSelectUI.ButtonType.TITLE:
			if (this.m_DefaultActionServerType.ContainsKey(CurrentBundleVersion.version))
			{
				this.m_DefaultActionServerType[CurrentBundleVersion.version] = Env.actionServerType.ToString();
			}
			else
			{
				this.m_DefaultActionServerType.Add(CurrentBundleVersion.version, Env.actionServerType.ToString());
			}
			this.SaveFile();
			Application.LoadLevel(TitleDefine.TitleSceneName);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0004FB80 File Offset: 0x0004DD80
	private Rect MatchScreenSize(Rect rect)
	{
		Rect result = new Rect(rect);
		result.x *= this.m_GUIScale.x;
		result.width *= this.m_GUIScale.x;
		result.y *= this.m_GUIScale.y;
		result.height *= this.m_GUIScale.y;
		return result;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0004FBFC File Offset: 0x0004DDFC
	private void SaveFile()
	{
		string path = Application.persistentDataPath + "/" + this.SAVE_FILE_NAME;
		StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8);
		if (streamWriter != null)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.m_DefaultActionServerType)
			{
				streamWriter.Write(keyValuePair.Key + "," + keyValuePair.Value + "\n");
			}
			streamWriter.Close();
		}
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0004FCB0 File Offset: 0x0004DEB0
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
					if (array != null && array.Length > 1)
					{
						this.m_DefaultActionServerType.Add(array[0], array[1]);
					}
				}
				streamReader.Close();
			}
		}
	}

	// Token: 0x04000B59 RID: 2905
	private readonly Vector2 SCREEN_RECT_SIZE = new Vector2(1600f, 900f);

	// Token: 0x04000B5A RID: 2906
	private Vector2 m_GUIScale = default(Vector2);

	// Token: 0x04000B5B RID: 2907
	private Dictionary<DebugServerSelectUI.ButtonType, Rect> m_ButtonDict = new Dictionary<DebugServerSelectUI.ButtonType, Rect>();

	// Token: 0x04000B5C RID: 2908
	private GUIStyle m_buttonStyle;

	// Token: 0x04000B5D RID: 2909
	private GUIStyle m_labelStyle;

	// Token: 0x04000B5E RID: 2910
	private bool m_GUIInited;

	// Token: 0x04000B5F RID: 2911
	private int m_fontSize;

	// Token: 0x04000B60 RID: 2912
	private int m_commandCount;

	// Token: 0x04000B61 RID: 2913
	private readonly int POP_BUTTON_COUNT = 10;

	// Token: 0x04000B62 RID: 2914
	private Rect m_hiddenButtonRect;

	// Token: 0x04000B63 RID: 2915
	private readonly string SAVE_FILE_NAME = "DefaultActionServerType.txt";

	// Token: 0x04000B64 RID: 2916
	private Dictionary<string, string> m_DefaultActionServerType = new Dictionary<string, string>();

	// Token: 0x04000B65 RID: 2917
	private StringBuilder m_versionLabel = new StringBuilder();

	// Token: 0x04000B66 RID: 2918
	private StringBuilder m_serverLabel = new StringBuilder();

	// Token: 0x04000B67 RID: 2919
	private DebugServerSelectUI.ButtonType m_buttonType;

	// Token: 0x04000B68 RID: 2920
	private readonly string[] ButtonName = new string[]
	{
		"Local1",
		"Local2",
		"Local3",
		"Local4",
		"Local5",
		"Develop1",
		"Develop2",
		"Develop3",
		"Release",
		"Title"
	};

	// Token: 0x020001FC RID: 508
	private enum ButtonType
	{
		// Token: 0x04000B6A RID: 2922
		IDLE = -1,
		// Token: 0x04000B6B RID: 2923
		LOCAL1,
		// Token: 0x04000B6C RID: 2924
		LOCAL2,
		// Token: 0x04000B6D RID: 2925
		LOCAL3,
		// Token: 0x04000B6E RID: 2926
		LOCAL4,
		// Token: 0x04000B6F RID: 2927
		LOCAL5,
		// Token: 0x04000B70 RID: 2928
		DEVELOP1,
		// Token: 0x04000B71 RID: 2929
		DEVELOP2,
		// Token: 0x04000B72 RID: 2930
		DEVELOP3,
		// Token: 0x04000B73 RID: 2931
		RELEASE,
		// Token: 0x04000B74 RID: 2932
		TITLE
	}
}
