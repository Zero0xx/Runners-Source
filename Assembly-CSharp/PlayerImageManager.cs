using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C6 RID: 1990
public class PlayerImageManager : MonoBehaviour
{
	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x06003484 RID: 13444 RVA: 0x0011D598 File Offset: 0x0011B798
	public static PlayerImageManager Instance
	{
		get
		{
			return PlayerImageManager.mInstance;
		}
	}

	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x06003485 RID: 13445 RVA: 0x0011D5A0 File Offset: 0x0011B7A0
	private int MaxStorageCount
	{
		get
		{
			return this.mMaxStorageCount;
		}
	}

	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x06003486 RID: 13446 RVA: 0x0011D5A8 File Offset: 0x0011B7A8
	private bool IsExistStorageLimit
	{
		get
		{
			return 0 < this.mMaxStorageCount;
		}
	}

	// Token: 0x06003487 RID: 13447 RVA: 0x0011D5B4 File Offset: 0x0011B7B4
	private void Awake()
	{
		if (PlayerImageManager.mInstance == null)
		{
			PlayerImageManager.mInstance = this;
			if (this.IsExistStorageLimit)
			{
				this.mPlayerImageDataDic = new Dictionary<string, PlayerImageManager.PlayerImageData>(this.MaxStorageCount);
			}
			else
			{
				this.mPlayerImageDataDic = new Dictionary<string, PlayerImageManager.PlayerImageData>();
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003488 RID: 13448 RVA: 0x0011D620 File Offset: 0x0011B820
	public bool IsLoaded(string playerId)
	{
		PlayerImageManager.PlayerImageData playerImageData;
		return this.mPlayerImageDataDic.TryGetValue(playerId, out playerImageData) && playerImageData.IsLoaded;
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x0011D648 File Offset: 0x0011B848
	public bool IsLoading(string playerId)
	{
		PlayerImageManager.PlayerImageData playerImageData;
		return this.mPlayerImageDataDic.TryGetValue(playerId, out playerImageData) && playerImageData.IsLoading;
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x0011D670 File Offset: 0x0011B870
	public bool Load(string playerId, string url, Action<Texture2D> callback)
	{
		bool result = false;
		if (playerId != null && string.Empty != playerId)
		{
			PlayerImageManager.PlayerImageData playerImageData;
			if (!this.mPlayerImageDataDic.TryGetValue(playerId, out playerImageData))
			{
				if (url != null && string.Empty != url)
				{
					playerImageData = new PlayerImageManager.PlayerImageData();
					playerImageData.PlayerId = playerId;
					playerImageData.PlayerImageUrl = url;
					playerImageData.PlayerImage = this.mDefaultPlayerImage;
					playerImageData.IsLoading = false;
					playerImageData.IsLoaded = false;
					if (callback != null)
					{
						playerImageData.CallbackList.Add(callback);
					}
					this.mPlayerImageDataDic.Add(playerId, playerImageData);
					this.mPlayerImageQueue.Enqueue(playerImageData);
					result = true;
				}
			}
			else if (!playerImageData.IsLoaded)
			{
				if (callback != null)
				{
					playerImageData.CallbackList.Add(callback);
				}
			}
			else
			{
				if (callback != null)
				{
					callback(playerImageData.PlayerImage);
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x0011D758 File Offset: 0x0011B958
	public void Dispose(string playerId, bool is_removeList = true)
	{
		PlayerImageManager.PlayerImageData playerImageData;
		if (this.mPlayerImageDataDic.TryGetValue(playerId, out playerImageData))
		{
			if (playerImageData == null)
			{
				return;
			}
			if (playerImageData.CallbackList == null)
			{
				return;
			}
			foreach (Action<Texture2D> action in playerImageData.CallbackList)
			{
				if (action != null)
				{
					action(this.mDefaultPlayerImage);
				}
			}
			if (is_removeList)
			{
				this.mPlayerImageDataDic.Remove(playerId);
			}
			playerImageData.CallbackList.Clear();
		}
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x0011D814 File Offset: 0x0011BA14
	private void Update()
	{
		if (this.mPlayerImageQueue.Count <= 0)
		{
			return;
		}
		PlayerImageManager.PlayerImageData playerImageData = this.mPlayerImageQueue.Peek();
		if (playerImageData.IsLoaded)
		{
			this.mPlayerImageQueue.Dequeue();
		}
		else if (!playerImageData.IsLoading)
		{
			playerImageData.IsLoading = true;
			base.StartCoroutine(PlayerImageManager.PlayerImageData.DoFetchPlayerImages(playerImageData));
		}
	}

	// Token: 0x0600348D RID: 13453 RVA: 0x0011D87C File Offset: 0x0011BA7C
	public Texture2D GetPlayerImage(string playerId)
	{
		return this.GetPlayerImage(playerId, string.Empty, null);
	}

	// Token: 0x0600348E RID: 13454 RVA: 0x0011D88C File Offset: 0x0011BA8C
	public Texture2D GetPlayerImage(string playerId, string url, Action<Texture2D> callback)
	{
		if (this.Load(playerId, url, callback))
		{
			PlayerImageManager.PlayerImageData playerImageData = this.mPlayerImageDataDic[playerId];
			return playerImageData.PlayerImage;
		}
		return this.mDefaultPlayerImage;
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x0011D8C4 File Offset: 0x0011BAC4
	public Texture2D GetDefaultImage()
	{
		return this.mDefaultPlayerImage;
	}

	// Token: 0x06003490 RID: 13456 RVA: 0x0011D8CC File Offset: 0x0011BACC
	public void ClearPlayerImage(string playerId, bool is_removeList = true)
	{
		this.Dispose(playerId, is_removeList);
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x0011D8D8 File Offset: 0x0011BAD8
	public void ClearAllPlayerImage()
	{
		foreach (KeyValuePair<string, PlayerImageManager.PlayerImageData> keyValuePair in this.mPlayerImageDataDic)
		{
			string playerId = keyValuePair.Value.PlayerId;
			this.ClearPlayerImage(playerId, false);
		}
		this.mPlayerImageDataDic.Clear();
	}

	// Token: 0x06003492 RID: 13458 RVA: 0x0011D958 File Offset: 0x0011BB58
	public static Texture2D GetPlayerDefaultImage()
	{
		Texture2D result = null;
		if (PlayerImageManager.mInstance != null)
		{
			result = PlayerImageManager.mInstance.GetDefaultImage();
		}
		return result;
	}

	// Token: 0x04002C2D RID: 11309
	private static PlayerImageManager mInstance;

	// Token: 0x04002C2E RID: 11310
	[SerializeField]
	private readonly int mMaxStorageCount = -1;

	// Token: 0x04002C2F RID: 11311
	[SerializeField]
	private Texture2D mDefaultPlayerImage;

	// Token: 0x04002C30 RID: 11312
	private Dictionary<string, PlayerImageManager.PlayerImageData> mPlayerImageDataDic;

	// Token: 0x04002C31 RID: 11313
	private Queue<PlayerImageManager.PlayerImageData> mPlayerImageQueue = new Queue<PlayerImageManager.PlayerImageData>();

	// Token: 0x020007C7 RID: 1991
	private class PlayerImageData
	{
		// Token: 0x06003493 RID: 13459 RVA: 0x0011D984 File Offset: 0x0011BB84
		public PlayerImageData()
		{
			this.IsLoading = false;
			this.IsLoaded = false;
			this.PlayerId = string.Empty;
			this.PlayerImageUrl = string.Empty;
			this.PlayerImage = null;
			this.CallbackList = new List<Action<Texture2D>>(10);
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x0011D9D0 File Offset: 0x0011BBD0
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x0011D9D8 File Offset: 0x0011BBD8
		public bool IsLoading { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x0011D9E4 File Offset: 0x0011BBE4
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x0011D9EC File Offset: 0x0011BBEC
		public bool IsLoaded { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x0011D9F8 File Offset: 0x0011BBF8
		// (set) Token: 0x06003499 RID: 13465 RVA: 0x0011DA00 File Offset: 0x0011BC00
		public string PlayerId { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x0011DA0C File Offset: 0x0011BC0C
		// (set) Token: 0x0600349B RID: 13467 RVA: 0x0011DA14 File Offset: 0x0011BC14
		public string PlayerImageUrl { get; set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x0011DA20 File Offset: 0x0011BC20
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x0011DA28 File Offset: 0x0011BC28
		public Texture2D PlayerImage { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x0011DA34 File Offset: 0x0011BC34
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x0011DA3C File Offset: 0x0011BC3C
		public List<Action<Texture2D>> CallbackList { get; set; }

		// Token: 0x060034A0 RID: 13472 RVA: 0x0011DA48 File Offset: 0x0011BC48
		public static IEnumerator DoFetchPlayerImages(PlayerImageManager.PlayerImageData playerImageData)
		{
			WWW www = new WWW(playerImageData.PlayerImageUrl);
			yield return www;
			if (www.texture != null)
			{
				playerImageData.PlayerImage = www.texture;
			}
			playerImageData.IsLoading = false;
			playerImageData.IsLoaded = true;
			int countCallback = playerImageData.CallbackList.Count;
			if (0 < countCallback)
			{
				for (int i = 0; i < countCallback; i++)
				{
					Action<Texture2D> callback = playerImageData.CallbackList[i];
					if (callback != null)
					{
						callback(playerImageData.PlayerImage);
					}
				}
			}
			www.Dispose();
			www = null;
			yield break;
		}
	}
}
