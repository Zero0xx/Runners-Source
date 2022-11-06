using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DataTable
{
	// Token: 0x020001A1 RID: 417
	public class NGWordTable : MonoBehaviour
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x00044DA8 File Offset: 0x00042FA8
		private void Start()
		{
			if (NGWordTable.m_ngWordDataTable == null)
			{
				TimeProfiler.StartCountTime("NGWordTable::Start");
				TimeProfiler.StartCountTime("NGWordTable::AESCrypt.Decrypt");
				string s = AESCrypt.Decrypt(this.m_ngWordTabel.text);
				TimeProfiler.EndCountTime("NGWordTable::AESCrypt.Decrypt");
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(NGWordData[]));
				StringReader textReader = new StringReader(s);
				NGWordTable.m_ngWordDataTable = (NGWordData[])xmlSerializer.Deserialize(textReader);
				TimeProfiler.EndCountTime("NGWordTable::Start");
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00044E24 File Offset: 0x00043024
		private void OnDestroy()
		{
			NGWordTable.m_ngWordDataTable = null;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00044E2C File Offset: 0x0004302C
		public static NGWordData[] GetDataTable()
		{
			return NGWordTable.m_ngWordDataTable;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00044E34 File Offset: 0x00043034
		public static int GetDataTableCount()
		{
			if (NGWordTable.m_ngWordDataTable != null)
			{
				return NGWordTable.m_ngWordDataTable.Length;
			}
			return 0;
		}

		// Token: 0x04000984 RID: 2436
		[SerializeField]
		private TextAsset m_ngWordTabel;

		// Token: 0x04000985 RID: 2437
		private static NGWordData[] m_ngWordDataTable;
	}
}
