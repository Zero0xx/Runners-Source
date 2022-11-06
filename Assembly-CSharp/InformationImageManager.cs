using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using App;
using Text;
using UnityEngine;

// Token: 0x0200068E RID: 1678
public class InformationImageManager : MonoBehaviour
{
	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x06002CBB RID: 11451 RVA: 0x0010D538 File Offset: 0x0010B738
	public static InformationImageManager Instance
	{
		get
		{
			return InformationImageManager.m_instance;
		}
	}

	// Token: 0x06002CBC RID: 11452 RVA: 0x0010D540 File Offset: 0x0010B740
	private void Awake()
	{
		if (InformationImageManager.m_instance == null)
		{
			InformationImageManager.m_instance = this;
			this.m_imageDataDic = new Dictionary<string, InformationImageManager.ImageData>();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002CBD RID: 11453 RVA: 0x0010D58C File Offset: 0x0010B78C
	private void Start()
	{
		string savePath = this.getSavePath();
		if (!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(savePath);
		}
		string etagFloderPath = this.GetEtagFloderPath();
		if (!Directory.Exists(etagFloderPath))
		{
			Directory.CreateDirectory(etagFloderPath);
		}
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x0010D5CC File Offset: 0x0010B7CC
	private string getSavePath()
	{
		return Application.persistentDataPath + "/infoImage/";
	}

	// Token: 0x06002CBF RID: 11455 RVA: 0x0010D5E0 File Offset: 0x0010B7E0
	private IEnumerator LoadCashImage(InformationImageManager.ImageData imageData)
	{
		if (File.Exists(imageData.ImageEtagPath))
		{
			FileStream fs = new FileStream(imageData.ImageEtagPath, FileMode.Open);
			BinaryReader reader = new BinaryReader(fs);
			string text = reader.ReadString();
			string[] delimiter = new string[]
			{
				"@@@"
			};
			string[] word = text.Split(delimiter, StringSplitOptions.None);
			string etag = word[0];
			string lastModified = word[1];
			reader.Close();
			yield return base.StartCoroutine(InformationImageManager.ImageData.DoFetchCashImages(imageData, etag, lastModified));
		}
		if (!imageData.IsLoaded)
		{
			this.LoadCashImage(imageData.ImagePath, ref imageData);
			imageData.IsLoading = false;
			imageData.IsLoaded = true;
			foreach (Action<Texture2D> callback in imageData.CallbackList)
			{
				if (callback != null)
				{
					callback(imageData.Image);
				}
			}
		}
		yield break;
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x0010D60C File Offset: 0x0010B80C
	private void LoadCashImage(string filePath, ref InformationImageManager.ImageData imageData)
	{
		if (imageData != null)
		{
			imageData.Image = new Texture2D(0, 0, TextureFormat.ARGB32, false);
			imageData.Image.LoadImage(this.LoadImageData(filePath));
		}
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x0010D644 File Offset: 0x0010B844
	private byte[] LoadImageData(string path)
	{
		FileStream input = new FileStream(path, FileMode.Open);
		BinaryReader binaryReader = new BinaryReader(input);
		byte[] result = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
		binaryReader.Close();
		return result;
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x0010D67C File Offset: 0x0010B87C
	private static void SaveImageData(WWW www, Texture2D texture, string path, string etagPath)
	{
		if (texture != null)
		{
			FileStream output = new FileStream(path, FileMode.Create);
			BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write(texture.EncodeToPNG());
			binaryWriter.Close();
		}
		if (www != null && www.responseHeaders.ContainsKey("ETAG") && www.responseHeaders.ContainsKey("LAST-MODIFIED"))
		{
			FileStream output2 = new FileStream(etagPath, FileMode.Create);
			BinaryWriter binaryWriter2 = new BinaryWriter(output2);
			binaryWriter2.Write(www.responseHeaders["ETAG"] + "@@@" + www.responseHeaders["LAST-MODIFIED"]);
			binaryWriter2.Close();
		}
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x0010D72C File Offset: 0x0010B92C
	public void DeleteImageData(string imageId)
	{
		string str = this.getSavePath() + "ad_" + imageId;
		string str2 = this.getSavePath() + imageId;
		string str3 = this.GetEtagFloderPath() + imageId;
		string str4 = this.GetEtagFloderPath() + "ad_" + imageId;
		for (int i = 0; i <= 10; i++)
		{
			string str5 = "_" + TextUtility.GetSuffix((Env.Language)i);
			string path = str + str5 + ".jpg";
			string path2 = str2 + str5 + ".jpg";
			string path3 = str + str5 + ".png";
			string path4 = str2 + str5 + ".png";
			string path5 = str4 + str5 + ".etag";
			string path6 = str3 + str5 + ".etag";
			if (this.ExistsFile(path))
			{
				File.Delete(path);
			}
			if (this.ExistsFile(path2))
			{
				File.Delete(path2);
			}
			if (this.ExistsFile(path5))
			{
				File.Delete(path5);
			}
			if (this.ExistsFile(path6))
			{
				File.Delete(path6);
			}
			if (this.ExistsFile(path3))
			{
				File.Delete(path3);
			}
			if (this.ExistsFile(path4))
			{
				File.Delete(path4);
			}
		}
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x0010D874 File Offset: 0x0010BA74
	private bool ExistsFile(string path)
	{
		return !string.IsNullOrEmpty(path) && File.Exists(path);
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x0010D88C File Offset: 0x0010BA8C
	private string GetCashedFilePath(string imageId)
	{
		if (!string.IsNullOrEmpty(imageId))
		{
			string suffixe = TextUtility.GetSuffixe();
			return string.Concat(new string[]
			{
				this.getSavePath(),
				imageId,
				"_",
				suffixe,
				".jpg"
			});
		}
		return null;
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x0010D8D8 File Offset: 0x0010BAD8
	private string GetCashedEtagFilePath(string imageId)
	{
		if (!string.IsNullOrEmpty(imageId))
		{
			string suffixe = TextUtility.GetSuffixe();
			return string.Concat(new string[]
			{
				this.GetEtagFloderPath(),
				imageId,
				"_",
				suffixe,
				".etag"
			});
		}
		return null;
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x0010D924 File Offset: 0x0010BB24
	private string GetEtagFloderPath()
	{
		return this.getSavePath() + "etag/";
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x0010D938 File Offset: 0x0010BB38
	private string GetServerFileURL(string imageId)
	{
		if (!string.IsNullOrEmpty(imageId))
		{
			string text = "_" + TextUtility.GetSuffix(Env.language);
			return string.Concat(new string[]
			{
				NetBaseUtil.InformationServerURL,
				"pictures/infoImage/",
				imageId,
				text,
				".jpg"
			});
		}
		return null;
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x0010D994 File Offset: 0x0010BB94
	private string GetBannerName(string imageId)
	{
		return "ad_" + imageId;
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x0010D9A4 File Offset: 0x0010BBA4
	public bool IsLoaded(string imageId)
	{
		InformationImageManager.ImageData imageData;
		return this.m_imageDataDic.TryGetValue(imageId, out imageData) && imageData.IsLoaded;
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x0010D9CC File Offset: 0x0010BBCC
	public bool IsLoading(string imageId)
	{
		InformationImageManager.ImageData imageData;
		return this.m_imageDataDic.TryGetValue(imageId, out imageData) && imageData.IsLoading;
	}

	// Token: 0x06002CCC RID: 11468 RVA: 0x0010D9F4 File Offset: 0x0010BBF4
	public bool Load(string imageId, bool bannerFlag, Action<Texture2D> callback)
	{
		bool result = false;
		if (!string.IsNullOrEmpty(imageId))
		{
			if (bannerFlag)
			{
				imageId = this.GetBannerName(imageId);
			}
			InformationImageManager.ImageData imageData = null;
			if (this.m_imageDataDic.TryGetValue(imageId, out imageData))
			{
				if (!imageData.IsLoaded)
				{
					if (callback != null)
					{
						imageData.CallbackList.Add(callback);
					}
				}
				else
				{
					if (callback != null)
					{
						callback(imageData.Image);
					}
					result = true;
				}
			}
			else
			{
				imageData = new InformationImageManager.ImageData();
				imageData.ImageId = imageId;
				imageData.ImageUrl = this.GetServerFileURL(imageId);
				imageData.ImagePath = this.GetCashedFilePath(imageId);
				imageData.ImageEtagPath = this.GetCashedEtagFilePath(imageId);
				if (callback != null)
				{
					imageData.CallbackList.Add(callback);
				}
				imageData.IsLoading = true;
				imageData.IsLoaded = false;
				if (this.ExistsFile(imageData.ImagePath) && this.ExistsFile(imageData.ImageEtagPath))
				{
					this.m_imageDataDic.Add(imageId, imageData);
					base.StartCoroutine(this.LoadCashImage(imageData));
					result = true;
				}
				else
				{
					if (callback != null)
					{
						imageData.CallbackList.Add(callback);
					}
					this.m_imageDataDic.Add(imageId, imageData);
					base.StartCoroutine(InformationImageManager.ImageData.DoFetchImages(imageData));
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06002CCD RID: 11469 RVA: 0x0010DB34 File Offset: 0x0010BD34
	public void ClearWinowImage()
	{
		if (this.m_imageDataDic.Count > 0)
		{
			Dictionary<string, InformationImageManager.ImageData>.KeyCollection keys = this.m_imageDataDic.Keys;
			List<string> list = new List<string>();
			foreach (string text in keys)
			{
				if (text.IndexOf("ad_") < 0)
				{
					list.Add(text);
				}
			}
			foreach (string key in list)
			{
				if (this.m_imageDataDic.ContainsKey(key))
				{
					this.m_imageDataDic.Remove(key);
				}
			}
		}
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x0010DC38 File Offset: 0x0010BE38
	public void ResetImage()
	{
		this.m_imageDataDic.Clear();
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x0010DC48 File Offset: 0x0010BE48
	public void DeleteImageFiles()
	{
		this.ResetImage();
		string savePath = this.getSavePath();
		if (Directory.Exists(savePath))
		{
			string[] files = Directory.GetFiles(savePath, "*.png", SearchOption.AllDirectories);
			if (files != null && files.Length > 0)
			{
				foreach (string path in files)
				{
					if (this.ExistsFile(path))
					{
						File.Delete(path);
					}
				}
			}
			string[] files2 = Directory.GetFiles(savePath, "*.jpg", SearchOption.AllDirectories);
			if (files2 != null && files2.Length > 0)
			{
				foreach (string path2 in files2)
				{
					if (this.ExistsFile(path2))
					{
						File.Delete(path2);
					}
				}
			}
			string etagFloderPath = this.GetEtagFloderPath();
			if (Directory.Exists(etagFloderPath))
			{
				string[] files3 = Directory.GetFiles(etagFloderPath, "*.etag", SearchOption.AllDirectories);
				if (files3 != null && files3.Length > 0)
				{
					foreach (string path3 in files3)
					{
						if (this.ExistsFile(path3))
						{
							File.Delete(path3);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x0010DD80 File Offset: 0x0010BF80
	public Texture2D GetImage(string imageId, bool bannerFlag, Action<Texture2D> callback)
	{
		if (this.Load(imageId, bannerFlag, callback))
		{
			InformationImageManager.ImageData imageData = this.m_imageDataDic[imageId];
			return imageData.Image;
		}
		return null;
	}

	// Token: 0x0400297C RID: 10620
	private const string BANNER_PREFIX = "ad_";

	// Token: 0x0400297D RID: 10621
	private const string IMAGE_FOLDER = "/infoImage/";

	// Token: 0x0400297E RID: 10622
	private const string IMAGE_EXTENSION = ".jpg";

	// Token: 0x0400297F RID: 10623
	private const string PNG_EXTENSION = ".png";

	// Token: 0x04002980 RID: 10624
	private const string IMAGE_ETG_EXTENSION = ".etag";

	// Token: 0x04002981 RID: 10625
	private static InformationImageManager m_instance;

	// Token: 0x04002982 RID: 10626
	private Dictionary<string, InformationImageManager.ImageData> m_imageDataDic;

	// Token: 0x0200068F RID: 1679
	private class ImageData
	{
		// Token: 0x06002CD1 RID: 11473 RVA: 0x0010DDB0 File Offset: 0x0010BFB0
		public ImageData()
		{
			this.IsLoading = false;
			this.IsLoaded = false;
			this.ImageId = string.Empty;
			this.ImageUrl = string.Empty;
			this.ImagePath = string.Empty;
			this.Image = null;
			this.CallbackList = new List<Action<Texture2D>>(10);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x0010DE08 File Offset: 0x0010C008
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x0010DE10 File Offset: 0x0010C010
		public bool IsLoading { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x0010DE1C File Offset: 0x0010C01C
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x0010DE24 File Offset: 0x0010C024
		public bool IsLoaded { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x0010DE30 File Offset: 0x0010C030
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x0010DE38 File Offset: 0x0010C038
		public string ImageId { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x0010DE44 File Offset: 0x0010C044
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x0010DE4C File Offset: 0x0010C04C
		public string ImageUrl { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0010DE58 File Offset: 0x0010C058
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x0010DE60 File Offset: 0x0010C060
		public string ImagePath { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x0010DE6C File Offset: 0x0010C06C
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x0010DE74 File Offset: 0x0010C074
		public string ImageEtagPath { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x0010DE80 File Offset: 0x0010C080
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x0010DE88 File Offset: 0x0010C088
		public Texture2D Image { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x0010DE94 File Offset: 0x0010C094
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x0010DE9C File Offset: 0x0010C09C
		public List<Action<Texture2D>> CallbackList { get; set; }

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0010DEA8 File Offset: 0x0010C0A8
		public static IEnumerator DoFetchImages(InformationImageManager.ImageData imageData)
		{
			WWW www = new WWW(imageData.ImageUrl);
			yield return www;
			imageData.IsLoading = false;
			imageData.IsLoaded = true;
			if (string.IsNullOrEmpty(www.error) && www.texture != null)
			{
				imageData.Image = www.texture;
				int countCallback = imageData.CallbackList.Count;
				if (countCallback > 0)
				{
					for (int index = 0; index < countCallback; index++)
					{
						Action<Texture2D> callback = imageData.CallbackList[index];
						if (callback != null)
						{
							callback(imageData.Image);
						}
					}
				}
				InformationImageManager.SaveImageData(www, imageData.Image, imageData.ImagePath, imageData.ImageEtagPath);
			}
			www.Dispose();
			www = null;
			yield break;
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0010DECC File Offset: 0x0010C0CC
		public static IEnumerator DoFetchCashImages(InformationImageManager.ImageData imageData, string etag, string lastModified)
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers["If-Modified-Since"] = lastModified;
			headers["ETAG"] = etag;
			WWW www = new WWW(imageData.ImageUrl, null, headers);
			yield return www;
			if (string.IsNullOrEmpty(www.error) && www.texture != null && www.size > 0)
			{
				imageData.IsLoading = false;
				imageData.IsLoaded = true;
				imageData.Image = www.texture;
				int countCallback = imageData.CallbackList.Count;
				if (countCallback > 0)
				{
					for (int index = 0; index < countCallback; index++)
					{
						Action<Texture2D> callback = imageData.CallbackList[index];
						if (callback != null)
						{
							callback(imageData.Image);
						}
					}
				}
				InformationImageManager.SaveImageData(www, imageData.Image, imageData.ImagePath, imageData.ImageEtagPath);
			}
			else
			{
				global::Debug.Log("DoFetchCashImages new jpeg is non!!");
			}
			www.Dispose();
			www = null;
			yield break;
		}
	}
}
