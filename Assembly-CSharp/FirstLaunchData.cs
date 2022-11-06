using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class FirstLaunchData : MonoBehaviour
{
	// Token: 0x06001EC7 RID: 7879 RVA: 0x000B745C File Offset: 0x000B565C
	public bool IsDone(FirstLaunchData.Type type)
	{
		if (type >= FirstLaunchData.Type.TYPE_NUM)
		{
			global::Debug.Log("FirstLaunchData.IsDone: Invalid parameter");
			return false;
		}
		return this.m_isLaunched[(int)type];
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x000B747C File Offset: 0x000B567C
	public void Register(FirstLaunchData.Type type, bool isLaunched)
	{
		if (type >= FirstLaunchData.Type.TYPE_NUM)
		{
			global::Debug.Log("FirstLaunchData.Register: Invalid parameter");
			return;
		}
		this.m_isLaunched[(int)type] = isLaunched;
		this.StoreSaveData();
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x000B74A0 File Offset: 0x000B56A0
	private void Awake()
	{
		if (FirstLaunchData.m_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			FirstLaunchData.m_instance = this;
			this.LoadSaveData();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x000B74E4 File Offset: 0x000B56E4
	private void OnDestroy()
	{
		FirstLaunchData.m_instance = null;
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x000B74EC File Offset: 0x000B56EC
	private void LoadSaveData()
	{
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x000B74F0 File Offset: 0x000B56F0
	private void StoreSaveData()
	{
	}

	// Token: 0x04001C33 RID: 7219
	private static FirstLaunchData m_instance;

	// Token: 0x04001C34 RID: 7220
	private bool[] m_isLaunched = new bool[5];

	// Token: 0x02000408 RID: 1032
	public enum Type
	{
		// Token: 0x04001C36 RID: 7222
		TYPE_NONE = -1,
		// Token: 0x04001C37 RID: 7223
		TYPE_GET_CHAOEGG,
		// Token: 0x04001C38 RID: 7224
		TYPE_MILEAGE_START,
		// Token: 0x04001C39 RID: 7225
		TYPE_MILEAGE_AFTER_TUTORIAL,
		// Token: 0x04001C3A RID: 7226
		TYPE_MILEAGE_BOSS_LOSE_FIRST,
		// Token: 0x04001C3B RID: 7227
		TYPE_MILEAGE_BOSS_LOSE,
		// Token: 0x04001C3C RID: 7228
		TYPE_NUM
	}
}
