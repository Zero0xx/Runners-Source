using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UnityEngine;

namespace SaveData
{
	// Token: 0x020002A7 RID: 679
	public class InformationSaveManager : MonoBehaviour
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x00068A2C File Offset: 0x00066C2C
		protected void Awake()
		{
			this.CheckInstance();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00068A38 File Offset: 0x00066C38
		private void Start()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.Init();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00068A48 File Offset: 0x00066C48
		public void Init()
		{
			this.m_informationData.Init();
			if (!this.InformationFileCheck())
			{
				this.m_informationData.m_isDirty = true;
				this.SaveInformationData();
			}
			else
			{
				this.LoadInfomationData();
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00068A80 File Offset: 0x00066C80
		public static InformationData GetInformationSaveData()
		{
			if (InformationSaveManager.instance == null)
			{
				return null;
			}
			return InformationSaveManager.instance.GetInformationData();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00068AA0 File Offset: 0x00066CA0
		public InformationData GetInformationData()
		{
			return this.m_informationData;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00068AA8 File Offset: 0x00066CA8
		private string getSavePath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00068AB0 File Offset: 0x00066CB0
		private string GetHashData(string textdata)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(textdata);
			SHA256 sha = new SHA256CryptoServiceProvider();
			byte[] array = sha.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.AppendFormat("{0:X2}", array[i]);
			}
			sha.Clear();
			return stringBuilder.ToString();
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00068B18 File Offset: 0x00066D18
		public void SaveInformationData()
		{
			if (!this.m_informationData.m_isDirty)
			{
				return;
			}
			string path = this.getSavePath() + "/ifrn.game";
			using (Stream stream = File.Open(path, FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					try
					{
						XmlDocument xmlDocument = this.CreateXmlData();
						string text = AESCrypt.Encrypt(xmlDocument.InnerXml);
						string hashData = this.GetHashData(text);
						streamWriter.Write(hashData + "\n");
						streamWriter.Write(text);
						this.m_errorcode = InformationSaveManager.ErrorCode.NO_ERROR;
					}
					catch
					{
						this.m_errorcode = InformationSaveManager.ErrorCode.FILE_CANNOT_OPEN;
					}
				}
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00068C18 File Offset: 0x00066E18
		public void LoadInfomationData()
		{
			Stream stream = null;
			try
			{
				stream = File.Open(this.getSavePath() + "/ifrn.game", FileMode.Open);
			}
			catch
			{
				this.m_errorcode = InformationSaveManager.ErrorCode.FILE_NOT_EXIST;
				if (stream != null)
				{
					stream.Dispose();
				}
				return;
			}
			if (stream != null)
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					string text = streamReader.ReadLine();
					string text2 = streamReader.ReadToEnd();
					string hashData = this.GetHashData(text2);
					if (!text.Equals(hashData))
					{
						global::Debug.Log("Data is Invalid.");
						this.m_errorcode = InformationSaveManager.ErrorCode.DATA_INVALID;
					}
					else
					{
						string streamData = AESCrypt.Decrypt(text2);
						this.ParseXmlData(streamData);
						this.m_errorcode = InformationSaveManager.ErrorCode.NO_ERROR;
					}
				}
				stream.Close();
				stream = null;
			}
			else
			{
				this.m_errorcode = InformationSaveManager.ErrorCode.FILE_NOT_EXIST;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00068D1C File Offset: 0x00066F1C
		private XmlDocument CreateXmlData()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			xmlDocument.AppendChild(newChild);
			XmlElement xmlElement = xmlDocument.CreateElement("InformationData");
			xmlDocument.AppendChild(xmlElement);
			int num = this.m_informationData.DataCount();
			for (int i = 0; i < num; i++)
			{
				this.CreateElementString(xmlDocument, xmlElement, "string", this.m_informationData.TextArray[i]);
			}
			return xmlDocument;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00068DA0 File Offset: 0x00066FA0
		private void CreateElementString(XmlDocument doc, XmlElement rootElement, string name, string value)
		{
			XmlElement xmlElement = doc.CreateElement(name);
			string text = value;
			if (value == null || value.Length == 0)
			{
				text = string.Empty;
			}
			XmlText newChild = doc.CreateTextNode(text);
			xmlElement.AppendChild(newChild);
			rootElement.AppendChild(xmlElement);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00068DEC File Offset: 0x00066FEC
		private bool ParseXmlData(string streamData)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(streamData);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("InformationData");
			if (xmlNode == null)
			{
				return false;
			}
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("string");
			if (xmlNodeList == null)
			{
				return false;
			}
			this.m_informationData.Init();
			int count = xmlNodeList.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_informationData.TextArray.Add(xmlNodeList.Item(i).InnerText);
			}
			int num = this.m_informationData.DataCount();
			InformationImageManager informationImageManager = InformationImageManager.Instance;
			for (int j = 0; j < num; j++)
			{
				string data = this.m_informationData.GetData(j, InformationData.DataType.ID);
				if (data != InformationData.INVALID_PARAM && long.Parse(data) != (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID && long.Parse(data) != (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID && long.Parse(data) != (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
				{
					string text = this.m_informationData.GetData(j, InformationData.DataType.ADD_INFO);
					if (text.Length > 11)
					{
						text = "-1";
					}
					DateTime localDateTime = NetUtil.GetLocalDateTime(long.Parse(text));
					DateTime localDateTime2 = NetUtil.GetLocalDateTime((long)NetUtil.GetCurrentUnixTime());
					if (localDateTime2 > localDateTime)
					{
						if (informationImageManager != null)
						{
							string data2 = this.m_informationData.GetData(j, InformationData.DataType.IMAGE_ID);
							informationImageManager.DeleteImageData(data2);
						}
						this.m_informationData.Reset(j);
					}
				}
			}
			return true;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00068F78 File Offset: 0x00067178
		private string GetStringByXml(XmlNode rootNode)
		{
			XmlNode xmlNode = rootNode.SelectSingleNode("string");
			if (xmlNode != null)
			{
				return xmlNode.InnerText;
			}
			return null;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00068FA0 File Offset: 0x000671A0
		public bool InformationFileCheck()
		{
			return File.Exists(this.getSavePath() + "/ifrn.game");
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00068FB8 File Offset: 0x000671B8
		public void DeleteInformationFile()
		{
			if (this.InformationFileCheck())
			{
				File.Delete(this.getSavePath() + "/ifrn.game");
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00068FE8 File Offset: 0x000671E8
		private InformationSaveManager.ErrorCode GetErrorCode()
		{
			return this.m_errorcode;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00068FF0 File Offset: 0x000671F0
		public static InformationSaveManager Instance
		{
			get
			{
				if (InformationSaveManager.instance == null)
				{
					InformationSaveManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(InformationSaveManager)) as InformationSaveManager);
				}
				return InformationSaveManager.instance;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0006902C File Offset: 0x0006722C
		private void OnDestroy()
		{
			if (InformationSaveManager.instance == this)
			{
				InformationSaveManager.instance = null;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00069044 File Offset: 0x00067244
		private bool CheckInstance()
		{
			if (InformationSaveManager.instance == null)
			{
				InformationSaveManager.instance = this;
				return true;
			}
			if (this == InformationSaveManager.Instance)
			{
				return true;
			}
			UnityEngine.Object.Destroy(base.gameObject);
			return false;
		}

		// Token: 0x040010A2 RID: 4258
		private const string INFORMATION_FILE_NAME = "ifrn";

		// Token: 0x040010A3 RID: 4259
		private const string EXTENSION = ".game";

		// Token: 0x040010A4 RID: 4260
		private const string XmlRootName = "InformationData";

		// Token: 0x040010A5 RID: 4261
		private InformationData m_informationData = new InformationData();

		// Token: 0x040010A6 RID: 4262
		private InformationSaveManager.ErrorCode m_errorcode;

		// Token: 0x040010A7 RID: 4263
		private static InformationSaveManager instance;

		// Token: 0x020002A8 RID: 680
		public enum ErrorCode
		{
			// Token: 0x040010A9 RID: 4265
			NO_ERROR,
			// Token: 0x040010AA RID: 4266
			FILE_NOT_EXIST,
			// Token: 0x040010AB RID: 4267
			FILE_CANNOT_OPEN,
			// Token: 0x040010AC RID: 4268
			DATA_INVALID
		}
	}
}
