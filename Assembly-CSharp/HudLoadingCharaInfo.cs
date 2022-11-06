using System;
using System.Collections;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class HudLoadingCharaInfo : MonoBehaviour
{
	// Token: 0x06001BD9 RID: 7129 RVA: 0x000A5A04 File Offset: 0x000A3C04
	private void Awake()
	{
		this.m_charaInfoList = new List<HudLoadingCharaInfo.CharaInfo>();
		this.m_pictureStack = new Dictionary<string, Texture2D>();
		this.m_loopCount = 0;
		this.m_currentCharaIndex = 0;
		this.m_isStartCoroutine = false;
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x000A5A34 File Offset: 0x000A3C34
	private void Start()
	{
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000A5A38 File Offset: 0x000A3C38
	private void OnDestroy()
	{
		this.m_charaInfoList.Clear();
		this.m_pictureStack.Clear();
		this.m_loopCount = 0;
		this.m_currentCharaIndex = 0;
		this.m_isStartCoroutine = false;
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x000A5A68 File Offset: 0x000A3C68
	private void Update()
	{
		if (!this.m_isStartCoroutine)
		{
			base.StartCoroutine(this.LoadWWW());
			this.m_isStartCoroutine = true;
			this.m_isErrorRestartCoroutine = false;
		}
		else if (NetworkErrorWindow.IsCreated("NetworkErrorReload") || NetworkErrorWindow.IsCreated("NetworkErrorRetry"))
		{
			this.m_isErrorRestartCoroutine = true;
		}
		else
		{
			if (this.m_isErrorRestartCoroutine)
			{
				this.m_isStartCoroutine = false;
			}
			this.m_isErrorRestartCoroutine = false;
		}
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000A5AE4 File Offset: 0x000A3CE4
	private IEnumerator LoadWWW()
	{
		this.m_loopCount = 0;
		if (TitleUtil.initUser)
		{
			this.m_currentCharaIndex = 0;
		}
		else
		{
			this.m_currentCharaIndex = 1;
		}
		string url = string.Empty;
		string serverUrl = NetBaseUtil.InformationServerURL;
		url = serverUrl + "title_load/title_load_index_" + TextUtility.GetSuffixe() + ".html";
		global::Debug.Log("HudLoadingCharaInfo LoadWWW url:" + url + " !!!!");
		GameObject gameObjectParser = HtmlParserFactory.Create(url, HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
		if (gameObjectParser != null)
		{
			HtmlParser parser = gameObjectParser.GetComponent<HtmlParser>();
			if (parser != null)
			{
				while (!parser.IsEndParse)
				{
					yield return null;
				}
				string result = parser.ParsedString;
				string[] contents = result.Split(new char[]
				{
					']'
				});
				for (int index = 0; index < contents.Length; index++)
				{
					contents[index] = contents[index].Remove(0, 2);
				}
				int paramCountMax = 5;
				if (contents.Length < 5)
				{
					paramCountMax = 4;
				}
				else
				{
					bool typeKeyFound = false;
					for (int index2 = 2; index2 < 5; index2++)
					{
						if (contents[index2] == "howtoplay" || contents[index2] == "Howtoplay")
						{
							typeKeyFound = true;
						}
						else if (contents[index2] == "player" || contents[index2] == "Player")
						{
							typeKeyFound = true;
						}
						else if (contents[index2] == "chara" || contents[index2] == "Chara")
						{
							typeKeyFound = true;
						}
						else if (contents[index2] == "object" || contents[index2] == "Object")
						{
							typeKeyFound = true;
						}
					}
					if (!typeKeyFound)
					{
						paramCountMax = 4;
					}
				}
				int contentsLength = contents.Length / paramCountMax;
				for (int index3 = 0; index3 < contentsLength; index3++)
				{
					HudLoadingCharaInfo.CharaInfo charaInfo = default(HudLoadingCharaInfo.CharaInfo);
					charaInfo.isReady = false;
					int contentsIndex = paramCountMax * index3;
					charaInfo.name = contents[contentsIndex];
					string pictureUrl = serverUrl + "pictures/title/" + contents[contentsIndex + 1];
					bool loadingFlg = true;
					if (this.m_pictureStack.Count > 0 && this.m_pictureStack.ContainsKey(pictureUrl))
					{
						loadingFlg = false;
					}
					if (loadingFlg)
					{
						WWW wwwPicture = new WWW(pictureUrl);
						yield return wwwPicture;
						charaInfo.picture = ((!(wwwPicture.texture != null)) ? null : wwwPicture.texture);
						this.m_pictureStack.Add(pictureUrl, wwwPicture.texture);
						wwwPicture.Dispose();
					}
					else
					{
						yield return null;
						charaInfo.picture = this.m_pictureStack[pictureUrl];
					}
					charaInfo.explain = contents[contentsIndex + 2];
					charaInfo.explainCaption = contents[contentsIndex + 3];
					if (paramCountMax == 5)
					{
						charaInfo.type = contents[contentsIndex + 4];
					}
					else
					{
						charaInfo.type = "player";
					}
					charaInfo.isReady = true;
					this.m_charaInfoList.Add(charaInfo);
				}
			}
		}
		yield break;
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000A5B00 File Offset: 0x000A3D00
	public int GetCharaCount()
	{
		return this.m_charaInfoList.Count;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000A5B1C File Offset: 0x000A3D1C
	public bool IsReady()
	{
		return this.m_currentCharaIndex < this.m_charaInfoList.Count && this.m_charaInfoList[this.m_currentCharaIndex].isReady;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000A5B5C File Offset: 0x000A3D5C
	public void GoNext()
	{
		this.m_currentCharaIndex++;
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count && this.m_charaInfoList.Count > 0)
		{
			this.m_currentCharaIndex = 0;
			this.m_loopCount++;
		}
		if (this.m_loopCount > 0)
		{
			for (int i = 0; i < this.m_charaInfoList.Count; i++)
			{
				HudLoadingCharaInfo.CharaInfo charaInfo = this.m_charaInfoList[(this.m_currentCharaIndex + i) % this.m_charaInfoList.Count];
				if (charaInfo.type != "howtoplay" && charaInfo.type != "Howtoplay")
				{
					this.m_currentCharaIndex = (this.m_currentCharaIndex + i) % this.m_charaInfoList.Count;
					break;
				}
			}
		}
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000A5C48 File Offset: 0x000A3E48
	public string GetCharaName()
	{
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count)
		{
			return string.Empty;
		}
		return this.m_charaInfoList[this.m_currentCharaIndex].name;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000A5C8C File Offset: 0x000A3E8C
	public string GetTypeName()
	{
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count)
		{
			return string.Empty;
		}
		return this.m_charaInfoList[this.m_currentCharaIndex].type;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000A5CD0 File Offset: 0x000A3ED0
	public Texture2D GetCharaPicture()
	{
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count)
		{
			return null;
		}
		return this.m_charaInfoList[this.m_currentCharaIndex].picture;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000A5D10 File Offset: 0x000A3F10
	public string GetCharaExplain()
	{
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count)
		{
			return string.Empty;
		}
		return this.m_charaInfoList[this.m_currentCharaIndex].explain;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000A5D54 File Offset: 0x000A3F54
	public string GetCharaExplainCaption()
	{
		if (this.m_currentCharaIndex >= this.m_charaInfoList.Count)
		{
			return string.Empty;
		}
		return this.m_charaInfoList[this.m_currentCharaIndex].explainCaption;
	}

	// Token: 0x0400199B RID: 6555
	private const int WebParamCount = 5;

	// Token: 0x0400199C RID: 6556
	private List<HudLoadingCharaInfo.CharaInfo> m_charaInfoList;

	// Token: 0x0400199D RID: 6557
	private Dictionary<string, Texture2D> m_pictureStack;

	// Token: 0x0400199E RID: 6558
	private int m_currentCharaIndex;

	// Token: 0x0400199F RID: 6559
	private int m_loopCount;

	// Token: 0x040019A0 RID: 6560
	private bool m_isStartCoroutine;

	// Token: 0x040019A1 RID: 6561
	private bool m_isErrorRestartCoroutine;

	// Token: 0x020003BD RID: 957
	private struct CharaInfo
	{
		// Token: 0x040019A2 RID: 6562
		public bool isReady;

		// Token: 0x040019A3 RID: 6563
		public string type;

		// Token: 0x040019A4 RID: 6564
		public string name;

		// Token: 0x040019A5 RID: 6565
		public Texture2D picture;

		// Token: 0x040019A6 RID: 6566
		public string explain;

		// Token: 0x040019A7 RID: 6567
		public string explainCaption;
	}
}
