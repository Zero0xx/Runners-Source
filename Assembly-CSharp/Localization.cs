using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x0600047E RID: 1150 RVA: 0x00016AA4 File Offset: 0x00014CA4
	public static bool isActive
	{
		get
		{
			return Localization.mInstance != null;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016AB4 File Offset: 0x00014CB4
	public static Localization instance
	{
		get
		{
			if (Localization.mInstance == null)
			{
				Localization.mInstance = (UnityEngine.Object.FindObjectOfType(typeof(Localization)) as Localization);
				if (Localization.mInstance == null)
				{
					GameObject gameObject = new GameObject("_Localization");
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					Localization.mInstance = gameObject.AddComponent<Localization>();
				}
			}
			return Localization.mInstance;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x00016B1C File Offset: 0x00014D1C
	// (set) Token: 0x06000481 RID: 1153 RVA: 0x00016B24 File Offset: 0x00014D24
	public string currentLanguage
	{
		get
		{
			return this.mLanguage;
		}
		set
		{
			if (this.mLanguage != value)
			{
				this.startingLanguage = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.languages != null)
					{
						int i = 0;
						int num = this.languages.Length;
						while (i < num)
						{
							TextAsset textAsset = this.languages[i];
							if (textAsset != null && textAsset.name == value)
							{
								this.Load(textAsset);
								return;
							}
							i++;
						}
					}
					TextAsset textAsset2 = Resources.Load(value, typeof(TextAsset)) as TextAsset;
					if (textAsset2 != null)
					{
						this.Load(textAsset2);
						return;
					}
				}
				this.mDictionary.Clear();
				PlayerPrefs.DeleteKey("Language");
			}
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00016BE8 File Offset: 0x00014DE8
	private void Awake()
	{
		if (Localization.mInstance == null)
		{
			Localization.mInstance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.currentLanguage = PlayerPrefs.GetString("Language", this.startingLanguage);
			if (string.IsNullOrEmpty(this.mLanguage) && this.languages != null && this.languages.Length > 0)
			{
				this.currentLanguage = this.languages[0].name;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00016C78 File Offset: 0x00014E78
	private void OnEnable()
	{
		if (Localization.mInstance == null)
		{
			Localization.mInstance = this;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00016C90 File Offset: 0x00014E90
	private void OnDestroy()
	{
		if (Localization.mInstance == this)
		{
			Localization.mInstance = null;
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00016CA8 File Offset: 0x00014EA8
	private void Load(TextAsset asset)
	{
		this.mLanguage = asset.name;
		PlayerPrefs.SetString("Language", this.mLanguage);
		ByteReader byteReader = new ByteReader(asset);
		this.mDictionary = byteReader.ReadDictionary();
		UIRoot.Broadcast("OnLocalize", this);
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00016CF0 File Offset: 0x00014EF0
	public string Get(string key)
	{
		string text;
		if (this.mDictionary.TryGetValue(key + " Mobile", out text))
		{
			return text;
		}
		return (!this.mDictionary.TryGetValue(key, out text)) ? key : text;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00016D38 File Offset: 0x00014F38
	public static string Localize(string key)
	{
		return (!(Localization.instance != null)) ? key : Localization.instance.Get(key);
	}

	// Token: 0x0400034E RID: 846
	private static Localization mInstance;

	// Token: 0x0400034F RID: 847
	public string startingLanguage = "English";

	// Token: 0x04000350 RID: 848
	public TextAsset[] languages;

	// Token: 0x04000351 RID: 849
	private Dictionary<string, string> mDictionary = new Dictionary<string, string>();

	// Token: 0x04000352 RID: 850
	private string mLanguage;
}
