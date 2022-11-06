using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using Text;
using UnityEngine;

namespace DataTable
{
	// Token: 0x02000197 RID: 407
	public class InformationDataTable : MonoBehaviour
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00044300 File Offset: 0x00042500
		public static InformationDataTable Instance
		{
			get
			{
				return InformationDataTable.s_instance;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00044308 File Offset: 0x00042508
		public static void Create()
		{
			if (InformationDataTable.s_instance == null)
			{
				GameObject gameObject = new GameObject("InformationDataTable");
				gameObject.AddComponent<InformationDataTable>();
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00044338 File Offset: 0x00042538
		public void Initialize(GameObject returnObject)
		{
			this.m_checkTime = false;
			this.m_returnObject = returnObject;
			base.StartCoroutine(this.LoadURL(NetBaseUtil.InformationServerURL + "InformationDataTable.bytes"));
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00044370 File Offset: 0x00042570
		public bool Loaded
		{
			get
			{
				return InformationDataTable.m_infoDataTable != null;
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00044380 File Offset: 0x00042580
		public bool isError()
		{
			return InformationDataTable.m_isError;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00044388 File Offset: 0x00042588
		private void Awake()
		{
			if (InformationDataTable.s_instance == null)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				InformationDataTable.s_instance = this;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000443BC File Offset: 0x000425BC
		private void OnDestroy()
		{
			if (InformationDataTable.s_instance == this)
			{
				InformationDataTable.s_instance = null;
				InformationDataTable.m_infoDataTable = null;
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000443DC File Offset: 0x000425DC
		private IEnumerator LoadURL(string url)
		{
			InformationDataTable.m_isError = false;
			float oldTime = Time.realtimeSinceStartup;
			if (this.m_checkTime)
			{
				global::Debug.Log("LS:start install URL: " + url);
			}
			WWWRequest request = new WWWRequest(url, false);
			request.SetConnectTime(20f);
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
			if (this.m_checkTime)
			{
				float loadTime = Time.realtimeSinceStartup;
				global::Debug.Log("LS:Load File: " + url + " Time is " + (loadTime - oldTime).ToString());
			}
			if (request.IsTimeOut())
			{
				global::Debug.LogError("LoadURLKeyData TimeOut. ");
				if (this.m_returnObject != null)
				{
					this.m_returnObject.SendMessage("InformationDataLoad_Failed", SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (request.GetError() != null)
			{
				global::Debug.LogError("LoadURLKeyData Error. " + request.GetError());
				if (this.m_returnObject != null)
				{
					this.m_returnObject.SendMessage("InformationDataLoad_Failed", SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				try
				{
					string resultText = request.GetResultString();
					if (resultText != null)
					{
						string text_data = AESCrypt.Decrypt(resultText);
						XmlSerializer serializer = new XmlSerializer(typeof(InformationData[]));
						StringReader sr = new StringReader(text_data);
						InformationDataTable.m_infoDataTable = (InformationData[])serializer.Deserialize(sr);
					}
					else
					{
						global::Debug.LogWarning("text load error www.text == null " + url);
						InformationDataTable.m_isError = true;
					}
				}
				catch
				{
					global::Debug.LogWarning("error " + url);
					InformationDataTable.m_isError = true;
				}
				if (this.m_returnObject != null)
				{
					this.m_returnObject.SendMessage("InformationDataLoad_Succeed", SendMessageOptions.DontRequireReceiver);
				}
			}
			request.Remove();
			yield break;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00044408 File Offset: 0x00042608
		public static InformationData[] GetDataTable()
		{
			return InformationDataTable.m_infoDataTable;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00044410 File Offset: 0x00042610
		public static string GetUrl(InformationDataTable.Type type)
		{
			if (InformationDataTable.m_infoDataTable != null && type < (InformationDataTable.Type)InformationDataTable.TypeName.Length)
			{
				foreach (InformationData informationData in InformationDataTable.m_infoDataTable)
				{
					if (informationData.tag == InformationDataTable.TypeName[(int)type] && informationData.sfx == TextUtility.GetSuffixe())
					{
						global::Debug.Log(string.Concat(new string[]
						{
							"GetUrl type=",
							type.ToString(),
							" sfx=",
							informationData.sfx,
							" tag=",
							informationData.tag,
							" url=",
							informationData.url
						}));
						return informationData.url;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x04000954 RID: 2388
		private bool m_checkTime = true;

		// Token: 0x04000955 RID: 2389
		private static string[] TypeName = new string[]
		{
			"copyright",
			"credit",
			"shop_legal",
			"help",
			"terms_of_service",
			"fb_feed_picture_android",
			"fb_feed_picture_ios",
			"install_page_android",
			"install_page_ios",
			"maintenance_page"
		};

		// Token: 0x04000956 RID: 2390
		private static InformationData[] m_infoDataTable;

		// Token: 0x04000957 RID: 2391
		private GameObject m_returnObject;

		// Token: 0x04000958 RID: 2392
		private static InformationDataTable s_instance = null;

		// Token: 0x04000959 RID: 2393
		private static bool m_isError = false;

		// Token: 0x02000198 RID: 408
		public enum Type
		{
			// Token: 0x0400095B RID: 2395
			COPYRIGHT,
			// Token: 0x0400095C RID: 2396
			CREDIT,
			// Token: 0x0400095D RID: 2397
			SHOP_LEGAL,
			// Token: 0x0400095E RID: 2398
			HELP,
			// Token: 0x0400095F RID: 2399
			TERMS_OF_SERVICE,
			// Token: 0x04000960 RID: 2400
			FB_FEED_PICTURE_ANDROID,
			// Token: 0x04000961 RID: 2401
			FB_FEED_PICTURE_IOS,
			// Token: 0x04000962 RID: 2402
			INSTALL_PAGE_ANDROID,
			// Token: 0x04000963 RID: 2403
			INSTALL_PAGE_IOS,
			// Token: 0x04000964 RID: 2404
			MAINTENANCE_PAGE,
			// Token: 0x04000965 RID: 2405
			NUM
		}
	}
}
