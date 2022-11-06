using System;
using SaveData;
using UnityEngine;

// Token: 0x02000206 RID: 518
public class SystemSettings : MonoBehaviour
{
	// Token: 0x06000DAF RID: 3503 RVA: 0x000501B4 File Offset: 0x0004E3B4
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.AutoRotation;
		this.InitInformation();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x000501F0 File Offset: 0x0004E3F0
	private void Update()
	{
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x000501F4 File Offset: 0x0004E3F4
	private void InitInformation()
	{
		SystemSettings.m_information.m_deviceModel = SystemInfo.deviceModel;
		SystemSettings.m_information.m_targetFrameRate = 60;
		SystemSettings.m_information.m_unityQualityLevel = QualitySettings.GetQualityLevel();
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0005022C File Offset: 0x0004E42C
	public static void ChangeQualityLevel(SystemSettings.QualityLevel level)
	{
		if (SystemSettings.m_information == null)
		{
			return;
		}
		SystemSettings.m_information.m_qualityLevel = level;
		if (level != SystemSettings.QualityLevel.Normal)
		{
			if (level == SystemSettings.QualityLevel.Low)
			{
				SystemSettings.m_information.m_targetFrameRate = 30;
				SystemSettings.m_information.m_unityQualityLevel = 0;
				QualitySettings.SetQualityLevel(SystemSettings.m_information.m_unityQualityLevel);
			}
		}
		else
		{
			SystemSettings.m_information.m_targetFrameRate = 60;
			SystemSettings.m_information.m_unityQualityLevel = 1;
			QualitySettings.SetQualityLevel(SystemSettings.m_information.m_unityQualityLevel);
		}
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x000502BC File Offset: 0x0004E4BC
	public static void ChangeQualityLevelBySaveData()
	{
		SystemData systemSaveData = SystemSaveManager.GetSystemSaveData();
		if (systemSaveData != null)
		{
			bool lightMode = systemSaveData.lightMode;
			SystemSettings.ChangeQualityLevel((!lightMode) ? SystemSettings.QualityLevel.Normal : SystemSettings.QualityLevel.Low);
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x000502F0 File Offset: 0x0004E4F0
	// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0005030C File Offset: 0x0004E50C
	public static int TargetFrameRate
	{
		get
		{
			if (SystemSettings.m_information != null)
			{
				return SystemSettings.m_information.m_targetFrameRate;
			}
			return 60;
		}
		set
		{
			if (SystemSettings.m_information != null)
			{
				SystemSettings.m_information.m_targetFrameRate = value;
			}
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00050324 File Offset: 0x0004E524
	public static string DeviceModel
	{
		get
		{
			if (SystemSettings.m_information != null)
			{
				return SystemSettings.m_information.m_deviceModel;
			}
			return null;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0005033C File Offset: 0x0004E53C
	public static SystemSettings Instance
	{
		get
		{
			if (SystemSettings.instance == null)
			{
				SystemSettings.instance = (UnityEngine.Object.FindObjectOfType(typeof(SystemSettings)) as SystemSettings);
			}
			return SystemSettings.instance;
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00050378 File Offset: 0x0004E578
	private bool CheckInstance()
	{
		if (SystemSettings.instance == null)
		{
			SystemSettings.instance = this;
			return true;
		}
		if (this == SystemSettings.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x000503BC File Offset: 0x0004E5BC
	private void OnDestroy()
	{
		if (SystemSettings.instance == this)
		{
			SystemSettings.instance = null;
		}
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x000503D4 File Offset: 0x0004E5D4
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x04000BAA RID: 2986
	private static SystemSettings.SystemInformation m_information = new SystemSettings.SystemInformation();

	// Token: 0x04000BAB RID: 2987
	private static SystemSettings instance = null;

	// Token: 0x02000207 RID: 519
	public enum QualityLevel
	{
		// Token: 0x04000BAD RID: 2989
		Normal,
		// Token: 0x04000BAE RID: 2990
		Low
	}

	// Token: 0x02000208 RID: 520
	private class SystemInformation
	{
		// Token: 0x04000BAF RID: 2991
		public SystemSettings.QualityLevel m_qualityLevel;

		// Token: 0x04000BB0 RID: 2992
		public string m_deviceModel;

		// Token: 0x04000BB1 RID: 2993
		public int m_targetFrameRate = 60;

		// Token: 0x04000BB2 RID: 2994
		public int m_unityQualityLevel;
	}
}
