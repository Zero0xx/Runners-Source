using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DataTable
{
	// Token: 0x02000179 RID: 377
	public class AchievementTable : MonoBehaviour
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x0003F49C File Offset: 0x0003D69C
		private void Start()
		{
			if (AchievementTable.m_achievementDataTable == null)
			{
				string s = AESCrypt.Decrypt(this.m_achievementTabel.text);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(AchievementData[]));
				StringReader textReader = new StringReader(s);
				AchievementTable.m_achievementDataTable = (AchievementData[])xmlSerializer.Deserialize(textReader);
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003F4EC File Offset: 0x0003D6EC
		private void OnDestroy()
		{
			AchievementTable.m_achievementDataTable = null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0003F4F4 File Offset: 0x0003D6F4
		public static AchievementData[] GetDataTable()
		{
			return AchievementTable.m_achievementDataTable;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0003F4FC File Offset: 0x0003D6FC
		public static int GetDataTableCount()
		{
			if (AchievementTable.m_achievementDataTable != null)
			{
				return AchievementTable.m_achievementDataTable.Length;
			}
			return 0;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003F514 File Offset: 0x0003D714
		public static AchievementData GetAchievementData(string id)
		{
			if (AchievementTable.GetDataTable() == null)
			{
				return null;
			}
			foreach (AchievementData achievementData in AchievementTable.GetDataTable())
			{
				if (achievementData.GetID() == id)
				{
					return achievementData;
				}
			}
			return null;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003F560 File Offset: 0x0003D760
		public static AchievementData GetAchievementDataOfIndex(int index)
		{
			if (AchievementTable.GetDataTable() == null)
			{
				return null;
			}
			if (index < AchievementTable.m_achievementDataTable.Length)
			{
				return AchievementTable.m_achievementDataTable[index];
			}
			return null;
		}

		// Token: 0x04000895 RID: 2197
		[SerializeField]
		private TextAsset m_achievementTabel;

		// Token: 0x04000896 RID: 2198
		private static AchievementData[] m_achievementDataTable;
	}
}
