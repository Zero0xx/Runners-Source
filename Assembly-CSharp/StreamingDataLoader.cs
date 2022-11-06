using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020009EB RID: 2539
public class StreamingDataLoader : MonoBehaviour
{
	// Token: 0x060042FD RID: 17149 RVA: 0x0015BF04 File Offset: 0x0015A104
	public void Initialize(GameObject returnObject)
	{
		this.m_localKeyDataList.Clear();
		this.m_serverKeyDataList.Clear();
		this.LoadKeyData(SoundManager.GetDownloadedDataPath() + "StreamingDataList.txt", ref this.m_localKeyDataList);
		this.LoadServerKey(returnObject);
		this.m_downloadList.Clear();
		this.m_loadEndCount = 0;
	}

	// Token: 0x060042FE RID: 17150 RVA: 0x0015BF5C File Offset: 0x0015A15C
	public void LoadServerKey(GameObject returnObject)
	{
		this.m_returnObject = returnObject;
		this.m_state = StreamingDataLoader.State.Init;
	}

	// Token: 0x060042FF RID: 17151 RVA: 0x0015BF6C File Offset: 0x0015A16C
	public bool IsEnableDownlad()
	{
		return this.m_state != StreamingDataLoader.State.Init && this.m_state != StreamingDataLoader.State.WaitLoadServerKey;
	}

	// Token: 0x06004300 RID: 17152 RVA: 0x0015BF8C File Offset: 0x0015A18C
	public void AddFileIfNotDownloaded(string url, string path)
	{
		string fileName = Path.GetFileName(path);
		string key = this.GetKey(this.m_serverKeyDataList, fileName);
		if (this.IsNeedToLoad(path, fileName, key))
		{
			StreamingDataLoader.Info info = new StreamingDataLoader.Info();
			info.url = url;
			info.path = path;
			info.downloaded = false;
			this.m_downloadList.Add(info);
		}
	}

	// Token: 0x06004301 RID: 17153 RVA: 0x0015BFE4 File Offset: 0x0015A1E4
	public void StartDownload(int tryCount, GameObject returnObject)
	{
		this.m_installFailedCount = tryCount;
		if (this.IsEnableDownlad())
		{
			this.DeleteLocalGarbage();
			this.m_returnObject = returnObject;
			this.m_state = StreamingDataLoader.State.LoadReady;
			this.m_loadEndCount = 0;
		}
	}

	// Token: 0x06004302 RID: 17154 RVA: 0x0015C024 File Offset: 0x0015A224
	public void GetLoadList(ref List<string> getData)
	{
		string text = "GetLoadList \n";
		foreach (StreamingKeyData streamingKeyData in this.m_serverKeyDataList)
		{
			getData.Add(streamingKeyData.m_name);
			text = text + streamingKeyData.m_name + "\n";
		}
		this.DebugDraw(text);
	}

	// Token: 0x06004303 RID: 17155 RVA: 0x0015C0B0 File Offset: 0x0015A2B0
	private IEnumerator DownloadDatas(GameObject returnObject)
	{
		this.DebugDrawList("local server", "DownloadDatas START");
		yield return null;
		while (this.m_loadEndCount < this.m_downloadList.Count)
		{
			StreamingDataLoader.Info info = this.m_downloadList[this.m_loadEndCount];
			if (info != null)
			{
				string fileName = Path.GetFileName(info.path);
				string serverKey = this.GetKey(this.m_serverKeyDataList, fileName);
				if (!this.IsNeedToLoad(info.path, fileName, serverKey))
				{
					this.DebugDraw("No Load");
					this.m_loadEndCount++;
				}
				else
				{
					this.DebugDraw("Load !!");
					this.m_error = false;
					yield return base.StartCoroutine(this.UserInstallFile(info.url, info.path));
					info.downloaded = !this.m_error;
					if (this.m_error)
					{
						if (returnObject != null)
						{
							returnObject.SendMessage("StreamingDataLoad_Failed", SendMessageOptions.DontRequireReceiver);
						}
						GC.Collect();
						break;
					}
					this.DebugDraw("Load End");
					this.SetKey(ref this.m_localKeyDataList, fileName, serverKey);
					GC.Collect();
					this.m_loadEndCount++;
				}
			}
		}
		this.SaveKeyData(SoundManager.GetDownloadedDataPath() + "StreamingDataList.txt", this.m_localKeyDataList);
		if (!this.m_error)
		{
			this.DebugDrawList("local server", "DownloadDatas END");
			this.m_downloadList.Clear();
			this.m_state = StreamingDataLoader.State.PrepareEnd;
		}
		yield break;
	}

	// Token: 0x06004304 RID: 17156 RVA: 0x0015C0DC File Offset: 0x0015A2DC
	private IEnumerator UserInstallFile(string url, string path)
	{
		if (this.m_checkTime)
		{
			global::Debug.Log("LS:start install URL: " + url + " path:" + path);
		}
		WWWRequest request = new WWWRequest(url, false);
		request.SetConnectTime(WWWRequest.DefaultConnectTime + WWWRequest.DefaultConnectTime * (float)this.m_installFailedCount);
		while (!request.IsEnd())
		{
			request.Update();
			if (request.IsTimeOut())
			{
				request.Cancel();
				IL_116:
				global::Debug.Log("UserInstallFile End. ");
				if (request.IsTimeOut())
				{
					this.m_error = true;
					global::Debug.LogError("UserInstallFile TimeOut. ");
				}
				else if (request.GetError() != null)
				{
					this.m_error = true;
					global::Debug.LogError("UserInstallFile Error. " + request.GetError());
				}
				else
				{
					byte[] rowdata = request.GetResult();
					if (rowdata != null)
					{
						using (Stream stream = File.Open(path, FileMode.Create))
						{
							try
							{
								stream.Write(rowdata, 0, request.GetResultSize());
							}
							catch (Exception ex2)
							{
								Exception ex = ex2;
								this.m_error = true;
								global::Debug.Log("UserInstallFile Write Error:" + ex.Message);
							}
						}
					}
				}
				request.Remove();
				yield break;
			}
			float startTime = Time.realtimeSinceStartup;
			float spendTime = 0f;
			do
			{
				yield return null;
				spendTime = Time.realtimeSinceStartup - startTime;
			}
			while (spendTime <= 0.1f);
		}
		goto IL_116;
	}

	// Token: 0x17000927 RID: 2343
	// (get) Token: 0x06004305 RID: 17157 RVA: 0x0015C114 File Offset: 0x0015A314
	public bool Loaded
	{
		get
		{
			return this.m_state == StreamingDataLoader.State.End;
		}
	}

	// Token: 0x17000928 RID: 2344
	// (get) Token: 0x06004306 RID: 17158 RVA: 0x0015C120 File Offset: 0x0015A320
	public int NumInLoadList
	{
		get
		{
			return this.m_downloadList.Count;
		}
	}

	// Token: 0x17000929 RID: 2345
	// (get) Token: 0x06004307 RID: 17159 RVA: 0x0015C130 File Offset: 0x0015A330
	public int NumLoaded
	{
		get
		{
			return this.m_loadEndCount;
		}
	}

	// Token: 0x06004308 RID: 17160 RVA: 0x0015C138 File Offset: 0x0015A338
	private void AddKeyData(string text, ref List<StreamingKeyData> outData)
	{
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		foreach (string text2 in array)
		{
			string[] array3 = text2.Split(new char[]
			{
				','
			});
			if (array3.Length == 2)
			{
				string name = array3[0].Trim();
				string key = array3[1].Trim();
				outData.Add(new StreamingKeyData(name, key));
			}
		}
	}

	// Token: 0x06004309 RID: 17161 RVA: 0x0015C1B4 File Offset: 0x0015A3B4
	private bool LoadKeyData(string filePath, ref List<StreamingKeyData> outData)
	{
		this.DebugDraw("LoadKeyData filePath=" + filePath);
		if (File.Exists(filePath))
		{
			Stream stream = null;
			try
			{
				stream = File.Open(filePath, FileMode.Open);
				using (StreamReader streamReader = new StreamReader(stream))
				{
					string text = streamReader.ReadToEnd();
					this.AddKeyData(text, ref outData);
				}
			}
			catch
			{
				return false;
			}
			if (stream != null)
			{
				stream.Close();
				this.DebugDrawList("local", "LoadKeyData");
				return true;
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600430A RID: 17162 RVA: 0x0015C278 File Offset: 0x0015A478
	private IEnumerator LoadURLKeyData(string url, GameObject returnObject)
	{
		this.DebugDraw("LoadURLKeyData url=" + url);
		WWWRequest request = new WWWRequest(url, false);
		while (!request.IsEnd())
		{
			request.Update();
			if (request.IsTimeOut())
			{
				request.Cancel();
				break;
			}
			float startTime = Time.realtimeSinceStartup;
			float spendTime = 0f;
			do
			{
				yield return null;
				spendTime = Time.realtimeSinceStartup - startTime;
			}
			while (spendTime <= 0.1f);
		}
		global::Debug.Log("LoadURLKeyData End. ");
		if (request.IsTimeOut())
		{
			global::Debug.LogError("LoadURLKeyData TimeOut. ");
			if (returnObject != null)
			{
				returnObject.SendMessage("StreamingDataLoad_Failed", SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (request.GetError() != null)
		{
			global::Debug.LogError("LoadURLKeyData Error. " + request.GetError());
			if (returnObject != null)
			{
				returnObject.SendMessage("StreamingDataLoad_Failed", SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			try
			{
				string resultText = request.GetResultString();
				if (resultText != null)
				{
					this.DebugDraw("Draw WWW Text.\n" + resultText);
					this.AddKeyData(resultText, ref this.m_serverKeyDataList);
					this.DebugDrawList("server", "LoadURLKeyData");
				}
				else
				{
					global::Debug.LogWarning("text load error www.text == null " + url);
				}
			}
			catch
			{
				global::Debug.LogWarning("error www.text.get " + url);
			}
			if (returnObject != null)
			{
				returnObject.SendMessage("StreamingDataLoad_Succeed", SendMessageOptions.DontRequireReceiver);
			}
			this.m_state = StreamingDataLoader.State.Idle;
		}
		request.Remove();
		yield break;
	}

	// Token: 0x0600430B RID: 17163 RVA: 0x0015C2B0 File Offset: 0x0015A4B0
	private bool SaveKeyData(string filePath, List<StreamingKeyData> inData)
	{
		try
		{
			using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.GetEncoding("utf-8")))
			{
				streamWriter.NewLine = "\n";
				foreach (StreamingKeyData streamingKeyData in inData)
				{
					streamWriter.WriteLine(streamingKeyData.m_name + "," + streamingKeyData.m_key);
				}
				streamWriter.Close();
			}
		}
		catch
		{
			return false;
		}
		return true;
	}

	// Token: 0x0600430C RID: 17164 RVA: 0x0015C3A0 File Offset: 0x0015A5A0
	private bool IsNeedToLoad(string path, string fileName, string serverKey)
	{
		if (!File.Exists(path))
		{
			return true;
		}
		this.DebugDraw("IsNeedToLoad fileName=" + fileName + " serverKey=" + serverKey);
		if (serverKey == string.Empty)
		{
			global::Debug.LogWarning("error : NOT serverKey");
			return false;
		}
		string key = this.GetKey(this.m_localKeyDataList, fileName);
		this.DebugDraw("IsNeedToLoad localKey=" + key);
		return !(key == serverKey);
	}

	// Token: 0x0600430D RID: 17165 RVA: 0x0015C41C File Offset: 0x0015A61C
	private string GetKey(List<StreamingKeyData> keyList, string nameData)
	{
		foreach (StreamingKeyData streamingKeyData in keyList)
		{
			if (nameData == streamingKeyData.m_name)
			{
				return streamingKeyData.m_key;
			}
		}
		return string.Empty;
	}

	// Token: 0x0600430E RID: 17166 RVA: 0x0015C49C File Offset: 0x0015A69C
	private void SetKey(ref List<StreamingKeyData> keyList, string nameData, string key)
	{
		foreach (StreamingKeyData streamingKeyData in keyList)
		{
			if (nameData == streamingKeyData.m_name)
			{
				streamingKeyData.m_key = key;
				return;
			}
		}
		keyList.Add(new StreamingKeyData(nameData, key));
	}

	// Token: 0x0600430F RID: 17167 RVA: 0x0015C524 File Offset: 0x0015A724
	private bool IsStreamingKeyData(List<StreamingKeyData> keyList, string filename)
	{
		foreach (StreamingKeyData streamingKeyData in keyList)
		{
			if (filename == streamingKeyData.m_name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004310 RID: 17168 RVA: 0x0015C59C File Offset: 0x0015A79C
	private void DeleteLocalGarbage()
	{
		this.DebugDrawList("local server", "DeleteLocalGarbage START----");
		List<StreamingKeyData> list = new List<StreamingKeyData>();
		List<string> list2 = new List<string>();
		foreach (StreamingKeyData streamingKeyData in this.m_localKeyDataList)
		{
			if (!this.IsStreamingKeyData(this.m_serverKeyDataList, streamingKeyData.m_name))
			{
				this.DebugDraw("deleteFileList.Add=" + streamingKeyData.m_name);
				list2.Add(streamingKeyData.m_name);
			}
			else
			{
				list.Add(streamingKeyData);
			}
		}
		if (list2.Count > 0)
		{
			foreach (string str in list2)
			{
				string text = SoundManager.GetDownloadedDataPath() + str;
				if (File.Exists(text))
				{
					this.DebugDraw("Delete=" + text);
					File.Delete(text);
				}
			}
			this.m_localKeyDataList.Clear();
			this.m_localKeyDataList.AddRange(list);
			this.SaveKeyData(SoundManager.GetDownloadedDataPath() + "StreamingDataList.txt", this.m_localKeyDataList);
			this.DebugDrawList("local", "DeleteLocalGarbage END---");
		}
	}

	// Token: 0x06004311 RID: 17169 RVA: 0x0015C72C File Offset: 0x0015A92C
	private void DebugDrawList(string type, string msg)
	{
	}

	// Token: 0x06004312 RID: 17170 RVA: 0x0015C73C File Offset: 0x0015A93C
	private void DebugDraw(string msg)
	{
	}

	// Token: 0x06004313 RID: 17171 RVA: 0x0015C740 File Offset: 0x0015A940
	protected void Awake()
	{
		this.m_checkTime = false;
		this.CheckInstance();
	}

	// Token: 0x06004314 RID: 17172 RVA: 0x0015C750 File Offset: 0x0015A950
	public static void Create()
	{
		if (StreamingDataLoader.instance == null)
		{
			GameObject gameObject = new GameObject("StreamingDataLoader");
			gameObject.AddComponent<StreamingDataLoader>();
		}
	}

	// Token: 0x1700092A RID: 2346
	// (get) Token: 0x06004315 RID: 17173 RVA: 0x0015C780 File Offset: 0x0015A980
	public static StreamingDataLoader Instance
	{
		get
		{
			return StreamingDataLoader.instance;
		}
	}

	// Token: 0x06004316 RID: 17174 RVA: 0x0015C788 File Offset: 0x0015A988
	protected bool CheckInstance()
	{
		if (StreamingDataLoader.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			StreamingDataLoader.instance = this;
			return true;
		}
		if (this == StreamingDataLoader.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06004317 RID: 17175 RVA: 0x0015C7D8 File Offset: 0x0015A9D8
	private void OnDestroy()
	{
		if (this == StreamingDataLoader.instance)
		{
			StreamingDataLoader.instance = null;
		}
	}

	// Token: 0x06004318 RID: 17176 RVA: 0x0015C7F0 File Offset: 0x0015A9F0
	private void Update()
	{
		switch (this.m_state)
		{
		case StreamingDataLoader.State.Init:
			base.StartCoroutine(this.LoadURLKeyData(SoundManager.GetDownloadURL() + "StreamingDataList.txt", this.m_returnObject));
			this.m_state = StreamingDataLoader.State.WaitLoadServerKey;
			break;
		case StreamingDataLoader.State.LoadReady:
			base.StartCoroutine(this.DownloadDatas(this.m_returnObject));
			this.m_state = StreamingDataLoader.State.Loading;
			break;
		case StreamingDataLoader.State.PrepareEnd:
			if (this.m_returnObject != null)
			{
				this.m_returnObject.SendMessage("StreamingDataLoad_Succeed", SendMessageOptions.DontRequireReceiver);
			}
			this.m_state = StreamingDataLoader.State.End;
			break;
		}
	}

	// Token: 0x040038CB RID: 14539
	private const string DATALIST_NAME = "StreamingDataList.txt";

	// Token: 0x040038CC RID: 14540
	private bool m_debugInfo;

	// Token: 0x040038CD RID: 14541
	private bool m_checkTime = true;

	// Token: 0x040038CE RID: 14542
	private List<StreamingKeyData> m_localKeyDataList = new List<StreamingKeyData>();

	// Token: 0x040038CF RID: 14543
	private List<StreamingKeyData> m_serverKeyDataList = new List<StreamingKeyData>();

	// Token: 0x040038D0 RID: 14544
	private bool m_error;

	// Token: 0x040038D1 RID: 14545
	private StreamingDataLoader.State m_state;

	// Token: 0x040038D2 RID: 14546
	private List<StreamingDataLoader.Info> m_downloadList = new List<StreamingDataLoader.Info>();

	// Token: 0x040038D3 RID: 14547
	private int m_loadEndCount;

	// Token: 0x040038D4 RID: 14548
	private int m_installFailedCount;

	// Token: 0x040038D5 RID: 14549
	private GameObject m_returnObject;

	// Token: 0x040038D6 RID: 14550
	private static StreamingDataLoader instance;

	// Token: 0x020009EC RID: 2540
	private class Info
	{
		// Token: 0x040038D7 RID: 14551
		public string url;

		// Token: 0x040038D8 RID: 14552
		public string path;

		// Token: 0x040038D9 RID: 14553
		public bool downloaded;
	}

	// Token: 0x020009ED RID: 2541
	private enum State
	{
		// Token: 0x040038DB RID: 14555
		Init,
		// Token: 0x040038DC RID: 14556
		WaitLoadServerKey,
		// Token: 0x040038DD RID: 14557
		Idle,
		// Token: 0x040038DE RID: 14558
		LoadReady,
		// Token: 0x040038DF RID: 14559
		Loading,
		// Token: 0x040038E0 RID: 14560
		PrepareEnd,
		// Token: 0x040038E1 RID: 14561
		End
	}
}
