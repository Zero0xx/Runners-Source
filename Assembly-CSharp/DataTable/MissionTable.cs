using System;
using System.IO;
using System.Xml.Serialization;
using Text;
using UnityEngine;

namespace DataTable
{
	// Token: 0x0200019F RID: 415
	public class MissionTable : MonoBehaviour
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00044B84 File Offset: 0x00042D84
		public static MissionTable Instance
		{
			get
			{
				return MissionTable.s_instance;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00044B8C File Offset: 0x00042D8C
		private void Awake()
		{
			if (MissionTable.s_instance == null)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				MissionTable.s_instance = this;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00044BC0 File Offset: 0x00042DC0
		private void OnDestroy()
		{
			if (MissionTable.s_instance == this)
			{
				MissionTable.m_missionDataTable = null;
				MissionTable.s_instance = null;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00044BE0 File Offset: 0x00042DE0
		private void Start()
		{
			if (MissionTable.m_missionDataTable == null)
			{
				string s = AESCrypt.Decrypt(this.m_missionTabel.text);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(MissionData[]));
				StringReader textReader = new StringReader(s);
				MissionTable.m_missionDataTable = (MissionData[])xmlSerializer.Deserialize(textReader);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00044C30 File Offset: 0x00042E30
		public static MissionData[] GetDataTable()
		{
			return MissionTable.m_missionDataTable;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00044C38 File Offset: 0x00042E38
		public static MissionData GetMissionData(int id)
		{
			if (MissionTable.GetDataTable() == null)
			{
				return null;
			}
			foreach (MissionData missionData in MissionTable.GetDataTable())
			{
				if (missionData.id == id)
				{
					return missionData;
				}
			}
			return null;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00044C80 File Offset: 0x00042E80
		public static MissionData GetMissionDataOfIndex(int index)
		{
			return MissionTable.m_missionDataTable[index];
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00044C8C File Offset: 0x00042E8C
		public static void LoadSetup()
		{
			GameObject gameObject = GameObject.Find("MissionTable");
			if (gameObject != null)
			{
				MissionData[] dataTable = MissionTable.GetDataTable();
				if (dataTable != null)
				{
					foreach (MissionData missionData in dataTable)
					{
						int type = (int)missionData.type;
						TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "caption" + type);
						if (text != null)
						{
							missionData.SetText(text.text);
						}
					}
				}
				if (gameObject.transform.parent != null && gameObject.transform.parent.name == "ETC")
				{
					gameObject.transform.parent = null;
				}
			}
		}

		// Token: 0x0400097F RID: 2431
		[SerializeField]
		private TextAsset m_missionTabel;

		// Token: 0x04000980 RID: 2432
		private static MissionData[] m_missionDataTable;

		// Token: 0x04000981 RID: 2433
		private static MissionTable s_instance;
	}
}
