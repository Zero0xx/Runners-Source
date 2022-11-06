using System;
using DataTable;
using SaveData;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class ChaoParameter : MonoBehaviour
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00033B9C File Offset: 0x00031D9C
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00033BA4 File Offset: 0x00031DA4
		public ChaoParameterData Data
		{
			get
			{
				return this.m_data;
			}
			set
			{
				this.m_data = value;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00033BB0 File Offset: 0x00031DB0
		private void Start()
		{
			base.enabled = false;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00033BBC File Offset: 0x00031DBC
		public bool IsChaoParameterDataDebugFlag()
		{
			return false;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00033BC0 File Offset: 0x00031DC0
		private void SetDebugChao()
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				int setChaoId = this.GetSetChaoId(instance.PlayerData, true);
				if (setChaoId >= 0)
				{
					this.m_mainChaoId = setChaoId;
					this.m_mainChaoLevel = this.GetChaoLevel(instance.ChaoData, this.m_mainChaoId);
				}
				else
				{
					this.SetMainChao(instance.PlayerData, instance.ChaoData);
				}
				int setChaoId2 = this.GetSetChaoId(instance.PlayerData, false);
				if (setChaoId2 >= 0)
				{
					this.m_subChaoId = setChaoId2;
					this.m_subChaoLevel = this.GetChaoLevel(instance.ChaoData, this.m_subChaoId);
				}
				else
				{
					this.SetSubChao(instance.PlayerData, instance.ChaoData);
				}
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00033C78 File Offset: 0x00031E78
		private void SetMainChao(PlayerData playerData, SaveData.ChaoData chaoData)
		{
			if (playerData != null)
			{
				playerData.MainChaoID = this.m_mainChaoId;
				this.m_mainChaoLevel = Mathf.Clamp(this.m_mainChaoLevel, 0, ChaoTable.ChaoMaxLevel());
				this.SetChaoLevel(chaoData, this.m_mainChaoId, this.m_mainChaoLevel);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00033CC4 File Offset: 0x00031EC4
		private void SetSubChao(PlayerData playerData, SaveData.ChaoData chaoData)
		{
			if (playerData != null)
			{
				playerData.SubChaoID = this.m_subChaoId;
				this.m_subChaoLevel = Mathf.Clamp(this.m_subChaoLevel, 0, ChaoTable.ChaoMaxLevel());
				this.SetChaoLevel(chaoData, this.m_subChaoId, this.m_subChaoLevel);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00033D10 File Offset: 0x00031F10
		private void SetChaoLevel(SaveData.ChaoData chaoData, int chaoId, int level)
		{
			if (chaoData != null)
			{
				for (int i = 0; i < chaoData.CHAO_MAX_NUM; i++)
				{
					if (chaoData.Info[i].chao_id == -1)
					{
						chaoData.Info[i].chao_id = chaoId;
						chaoData.Info[i].level = level;
						break;
					}
				}
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00033D7C File Offset: 0x00031F7C
		private int GetChaoLevel(SaveData.ChaoData chaoData, int chaoId)
		{
			if (chaoData != null)
			{
				for (int i = 0; i < chaoData.CHAO_MAX_NUM; i++)
				{
					if (chaoData.Info[i].chao_id == chaoId)
					{
						return chaoData.Info[i].level;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00033DD0 File Offset: 0x00031FD0
		private int GetSetChaoId(PlayerData playerData, bool mainChaoFlag)
		{
			if (playerData == null)
			{
				return -1;
			}
			if (mainChaoFlag)
			{
				return playerData.MainChaoID;
			}
			return playerData.SubChaoID;
		}

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		private int m_mainChaoId = -1;

		// Token: 0x040006C8 RID: 1736
		[SerializeField]
		private int m_mainChaoLevel;

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		private int m_subChaoId = -1;

		// Token: 0x040006CA RID: 1738
		[SerializeField]
		private int m_subChaoLevel;

		// Token: 0x040006CB RID: 1739
		public ChaoParameterData m_data;
	}
}
