using System;
using SaveData;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class SaveDataManager : MonoBehaviour
{
	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06001354 RID: 4948 RVA: 0x00069C38 File Offset: 0x00067E38
	public static SaveDataManager Instance
	{
		get
		{
			if (SaveDataManager.instance == null)
			{
				SaveDataManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(SaveDataManager)) as SaveDataManager);
			}
			return SaveDataManager.instance;
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x00069C74 File Offset: 0x00067E74
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00069C80 File Offset: 0x00067E80
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x00069C8C File Offset: 0x00067E8C
	private bool CheckInstance()
	{
		if (SaveDataManager.instance == null)
		{
			this.LoadSaveData();
			SaveDataManager.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(SaveDataManager.instance);
			return true;
		}
		if (this == SaveDataManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06001358 RID: 4952 RVA: 0x00069CE0 File Offset: 0x00067EE0
	// (set) Token: 0x06001359 RID: 4953 RVA: 0x00069CE8 File Offset: 0x00067EE8
	public PlayerData PlayerData
	{
		get
		{
			return SaveDataManager.m_player_data;
		}
		set
		{
			SaveDataManager.m_player_data = value;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x0600135A RID: 4954 RVA: 0x00069CF0 File Offset: 0x00067EF0
	// (set) Token: 0x0600135B RID: 4955 RVA: 0x00069CF8 File Offset: 0x00067EF8
	public CharaData CharaData
	{
		get
		{
			return SaveDataManager.m_chara_data;
		}
		set
		{
			SaveDataManager.m_chara_data = value;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x0600135C RID: 4956 RVA: 0x00069D00 File Offset: 0x00067F00
	// (set) Token: 0x0600135D RID: 4957 RVA: 0x00069D08 File Offset: 0x00067F08
	public ChaoData ChaoData
	{
		get
		{
			return SaveDataManager.m_chao_data;
		}
		set
		{
			SaveDataManager.m_chao_data = value;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x0600135E RID: 4958 RVA: 0x00069D10 File Offset: 0x00067F10
	// (set) Token: 0x0600135F RID: 4959 RVA: 0x00069D18 File Offset: 0x00067F18
	public ItemData ItemData
	{
		get
		{
			return SaveDataManager.m_item_data;
		}
		set
		{
			SaveDataManager.m_item_data = value;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06001360 RID: 4960 RVA: 0x00069D20 File Offset: 0x00067F20
	// (set) Token: 0x06001361 RID: 4961 RVA: 0x00069D28 File Offset: 0x00067F28
	public OptionData OptionData
	{
		get
		{
			return SaveDataManager.m_option_data;
		}
		set
		{
			SaveDataManager.m_option_data = value;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06001362 RID: 4962 RVA: 0x00069D30 File Offset: 0x00067F30
	// (set) Token: 0x06001363 RID: 4963 RVA: 0x00069D38 File Offset: 0x00067F38
	public ConnectData ConnectData
	{
		get
		{
			return SaveDataManager.m_connect_data;
		}
		set
		{
			SaveDataManager.m_connect_data = value;
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x00069D40 File Offset: 0x00067F40
	public void SaveAllData()
	{
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x00069D44 File Offset: 0x00067F44
	public void SavePlayerData()
	{
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x00069D48 File Offset: 0x00067F48
	public void SaveCharaData()
	{
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00069D4C File Offset: 0x00067F4C
	public void SaveChaoData()
	{
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x00069D50 File Offset: 0x00067F50
	public void SaveItemData()
	{
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x00069D54 File Offset: 0x00067F54
	public void SaveOptionData()
	{
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00069D58 File Offset: 0x00067F58
	public void LoadSaveData()
	{
	}

	// Token: 0x040010D6 RID: 4310
	private static PlayerData m_player_data = new PlayerData();

	// Token: 0x040010D7 RID: 4311
	private static CharaData m_chara_data = new CharaData();

	// Token: 0x040010D8 RID: 4312
	private static ChaoData m_chao_data = new ChaoData();

	// Token: 0x040010D9 RID: 4313
	private static ItemData m_item_data = new ItemData();

	// Token: 0x040010DA RID: 4314
	private static OptionData m_option_data = new OptionData();

	// Token: 0x040010DB RID: 4315
	private static ConnectData m_connect_data = new ConnectData();

	// Token: 0x040010DC RID: 4316
	private static SaveDataManager instance = null;
}
