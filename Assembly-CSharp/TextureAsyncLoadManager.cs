using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class TextureAsyncLoadManager : MonoBehaviour
{
	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002BA60 File Offset: 0x00029C60
	// (set) Token: 0x06000785 RID: 1925 RVA: 0x0002BA68 File Offset: 0x00029C68
	public Texture CharaDefaultTexture
	{
		get
		{
			return this.m_charaDefaultTexture;
		}
		private set
		{
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000786 RID: 1926 RVA: 0x0002BA6C File Offset: 0x00029C6C
	// (set) Token: 0x06000787 RID: 1927 RVA: 0x0002BA74 File Offset: 0x00029C74
	public Texture ChaoDefaultTexture
	{
		get
		{
			return this.m_chaoDefaultTexture;
		}
		private set
		{
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000788 RID: 1928 RVA: 0x0002BA78 File Offset: 0x00029C78
	// (set) Token: 0x06000789 RID: 1929 RVA: 0x0002BA80 File Offset: 0x00029C80
	public static TextureAsyncLoadManager Instance
	{
		get
		{
			return TextureAsyncLoadManager.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002BA84 File Offset: 0x00029C84
	public bool IsLoaded(TextureRequest request)
	{
		if (request == null)
		{
			return false;
		}
		string fileName = request.GetFileName();
		return this.IsLoaded(fileName);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002BAA8 File Offset: 0x00029CA8
	public bool IsLoaded(string fileName)
	{
		TextureAsyncLoadManager.TextureInfo textureInfo = null;
		return this.m_textureList.TryGetValue(fileName, out textureInfo) && textureInfo.Loaded;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002BAD8 File Offset: 0x00029CD8
	public void Request(TextureRequest request)
	{
		if (request == null)
		{
			return;
		}
		if (!request.IsEnableLoad())
		{
			return;
		}
		string fileName = request.GetFileName();
		TextureAsyncLoadManager.TextureInfo textureInfo = null;
		if (this.m_textureList.TryGetValue(fileName, out textureInfo))
		{
			textureInfo.RequestLoad(request);
		}
		else
		{
			TextureAsyncLoadManager.TextureInfo textureInfo2 = new TextureAsyncLoadManager.TextureInfo(base.gameObject);
			textureInfo2.RequestLoad(request);
			if (this.m_loadQueue.Count <= 0)
			{
				textureInfo2.Load();
			}
			this.m_loadQueue.Enqueue(textureInfo2);
			this.m_textureList.Add(fileName, textureInfo2);
		}
		this.m_DirtyFlag = true;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002BB6C File Offset: 0x00029D6C
	public void Remove(TextureRequest request)
	{
		if (request == null)
		{
			return;
		}
		if (!request.IsEnableLoad())
		{
			return;
		}
		string fileName = request.GetFileName();
		TextureAsyncLoadManager.TextureInfo textureInfo = null;
		if (!this.m_textureList.TryGetValue(fileName, out textureInfo))
		{
			return;
		}
		textureInfo.RequestRemove(request);
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0002BBB0 File Offset: 0x00029DB0
	private void Start()
	{
		TextureAsyncLoadManager.m_instance = this;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0002BBB8 File Offset: 0x00029DB8
	private void Update()
	{
		if (this.m_DirtyFlag)
		{
			UIPanel.SetDirty();
			this.m_DirtyFlag = false;
		}
		if (this.m_loadQueue.Count > 0)
		{
			TextureAsyncLoadManager.TextureInfo textureInfo = this.m_loadQueue.Peek();
			textureInfo.Update();
			if (textureInfo.Loaded)
			{
				this.m_loadQueue.Dequeue();
				if (this.m_loadQueue.Count > 0)
				{
					TextureAsyncLoadManager.TextureInfo textureInfo2 = this.m_loadQueue.Peek();
					textureInfo2.Load();
				}
			}
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, TextureAsyncLoadManager.TextureInfo> keyValuePair in this.m_textureList)
		{
			string key = keyValuePair.Key;
			TextureAsyncLoadManager.TextureInfo value = keyValuePair.Value;
			if (value != null)
			{
				if (value.EnableRemove)
				{
					list.Add(key);
				}
			}
		}
		if (list.Count > 0)
		{
			foreach (string key2 in list)
			{
				TextureAsyncLoadManager.TextureInfo textureInfo3;
				if (this.m_textureList.TryGetValue(key2, out textureInfo3))
				{
					textureInfo3.Remove();
				}
				this.m_textureList.Remove(key2);
			}
		}
	}

	// Token: 0x040005BB RID: 1467
	[SerializeField]
	private Texture m_charaDefaultTexture;

	// Token: 0x040005BC RID: 1468
	[SerializeField]
	private Texture m_chaoDefaultTexture;

	// Token: 0x040005BD RID: 1469
	private static TextureAsyncLoadManager m_instance;

	// Token: 0x040005BE RID: 1470
	private Dictionary<string, TextureAsyncLoadManager.TextureInfo> m_textureList = new Dictionary<string, TextureAsyncLoadManager.TextureInfo>();

	// Token: 0x040005BF RID: 1471
	private Queue<TextureAsyncLoadManager.TextureInfo> m_loadQueue = new Queue<TextureAsyncLoadManager.TextureInfo>();

	// Token: 0x040005C0 RID: 1472
	private bool m_DirtyFlag;

	// Token: 0x020000FF RID: 255
	private class TextureInfo
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public TextureInfo(GameObject obj)
		{
			this.m_gameObject = obj;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0002BD68 File Offset: 0x00029F68
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0002BD7C File Offset: 0x00029F7C
		public bool Loaded
		{
			get
			{
				return this.m_state == TextureAsyncLoadManager.TextureInfo.State.LOADED;
			}
			private set
			{
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0002BD80 File Offset: 0x00029F80
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		public bool EnableRemove
		{
			get
			{
				return this.Loaded && this.m_removeRequestCount > 0;
			}
			private set
			{
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002BDA4 File Offset: 0x00029FA4
		public void RequestLoad(TextureRequest request)
		{
			if (request == null)
			{
				return;
			}
			if (this.m_gameObject == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.m_textureName))
			{
				this.m_textureName = request.GetFileName();
			}
			this.m_requestList.Add(request);
			if (this.m_state == TextureAsyncLoadManager.TextureInfo.State.LOADED)
			{
				request.LoadDone(this.m_texture);
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0002BE0C File Offset: 0x0002A00C
		public void RequestRemove(TextureRequest request)
		{
			if (request == null)
			{
				return;
			}
			if (this.m_gameObject == null)
			{
				return;
			}
			foreach (TextureRequest textureRequest in this.m_requestList)
			{
				if (textureRequest != null)
				{
					if (textureRequest == request)
					{
						break;
					}
				}
			}
			this.m_removeRequestCount++;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public void Load()
		{
			if (this.m_state != TextureAsyncLoadManager.TextureInfo.State.IDLE)
			{
				return;
			}
			this.m_loader = this.m_gameObject.AddComponent<ResourceSceneLoader>();
			this.m_loader.AddLoadAndResourceManager(this.m_textureName, true, ResourceCategory.UI, false, false, this.m_textureName);
			this.m_state = TextureAsyncLoadManager.TextureInfo.State.LOADING;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002BF00 File Offset: 0x0002A100
		public void Update()
		{
			switch (this.m_state)
			{
			case TextureAsyncLoadManager.TextureInfo.State.LOADING:
				if (this.m_loader != null)
				{
					if (this.m_loader.Loaded)
					{
						this.m_texture = null;
						GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, this.m_textureName);
						AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
						this.m_texture = component.m_tex;
						for (int i = 0; i < this.m_requestList.Count; i++)
						{
							TextureRequest textureRequest = this.m_requestList[i];
							if (textureRequest != null)
							{
								textureRequest.LoadDone(this.m_texture);
							}
						}
						UnityEngine.Object.Destroy(this.m_loader);
						this.m_state = TextureAsyncLoadManager.TextureInfo.State.LOADED;
					}
				}
				else
				{
					this.m_state = TextureAsyncLoadManager.TextureInfo.State.LOADED;
				}
				break;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
		public void Remove()
		{
			ResourceManager instance = ResourceManager.Instance;
			if (instance != null)
			{
				string[] removeList = new string[]
				{
					this.m_requestList[0].GetFileName()
				};
				instance.RemoveResources(ResourceCategory.UI, removeList);
			}
		}

		// Token: 0x040005C1 RID: 1473
		private string m_textureName;

		// Token: 0x040005C2 RID: 1474
		private Texture m_texture;

		// Token: 0x040005C3 RID: 1475
		private List<TextureRequest> m_requestList = new List<TextureRequest>();

		// Token: 0x040005C4 RID: 1476
		private int m_removeRequestCount;

		// Token: 0x040005C5 RID: 1477
		private GameObject m_gameObject;

		// Token: 0x040005C6 RID: 1478
		private ResourceSceneLoader m_loader;

		// Token: 0x040005C7 RID: 1479
		private TextureAsyncLoadManager.TextureInfo.State m_state;

		// Token: 0x02000100 RID: 256
		private enum State
		{
			// Token: 0x040005C9 RID: 1481
			IDLE,
			// Token: 0x040005CA RID: 1482
			LOADING,
			// Token: 0x040005CB RID: 1483
			LOADED,
			// Token: 0x040005CC RID: 1484
			NUM
		}
	}
}
