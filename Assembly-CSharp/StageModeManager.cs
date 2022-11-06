using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class StageModeManager : MonoBehaviour
{
	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001720 RID: 5920 RVA: 0x000853FC File Offset: 0x000835FC
	public static StageModeManager Instance
	{
		get
		{
			return StageModeManager.m_instance;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001721 RID: 5921 RVA: 0x00085404 File Offset: 0x00083604
	// (set) Token: 0x06001722 RID: 5922 RVA: 0x0008540C File Offset: 0x0008360C
	public StageModeManager.Mode StageMode
	{
		get
		{
			return this.m_mode;
		}
		set
		{
			this.m_mode = value;
		}
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x00085418 File Offset: 0x00083618
	public bool IsQuickMode()
	{
		return this.m_mode == StageModeManager.Mode.QUICK;
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06001724 RID: 5924 RVA: 0x00085424 File Offset: 0x00083624
	// (set) Token: 0x06001725 RID: 5925 RVA: 0x0008542C File Offset: 0x0008362C
	public bool FirstTutorial
	{
		get
		{
			return this.m_firstTutorial;
		}
		set
		{
			this.m_firstTutorial = value;
		}
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x00085438 File Offset: 0x00083638
	public void DrawQuickStageIndex()
	{
		this.m_stageIndex = 1;
		if (EventManager.Instance != null && EventManager.Instance.IsQuickEvent())
		{
			EventStageData stageData = EventManager.Instance.GetStageData();
			if (stageData != null)
			{
				this.m_stageIndex = MileageMapUtility.GetStageIndex(stageData.stage_key);
			}
		}
		else
		{
			UnityEngine.Random.seed = NetUtil.GetCurrentUnixTime();
			int num = 1;
			int num2 = 4;
			int num3 = UnityEngine.Random.Range(num, num2);
			if (num3 >= num2)
			{
				num3 = num;
			}
			this.m_stageIndex = num3;
		}
		switch (this.m_stageIndex)
		{
		case 1:
			this.m_stageCharaAttribute = CharacterAttribute.SPEED;
			break;
		case 2:
			this.m_stageCharaAttribute = CharacterAttribute.FLY;
			break;
		case 3:
			this.m_stageCharaAttribute = CharacterAttribute.POWER;
			break;
		default:
			this.m_stageCharaAttribute = CharacterAttribute.UNKNOWN;
			break;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06001727 RID: 5927 RVA: 0x0008550C File Offset: 0x0008370C
	// (set) Token: 0x06001728 RID: 5928 RVA: 0x00085514 File Offset: 0x00083714
	public CharacterAttribute QuickStageCharaAttribute
	{
		get
		{
			return this.m_stageCharaAttribute;
		}
		private set
		{
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06001729 RID: 5929 RVA: 0x00085518 File Offset: 0x00083718
	// (set) Token: 0x0600172A RID: 5930 RVA: 0x00085520 File Offset: 0x00083720
	public int QuickStageIndex
	{
		get
		{
			return this.m_stageIndex;
		}
		private set
		{
		}
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x00085524 File Offset: 0x00083724
	private void Awake()
	{
		if (StageModeManager.m_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			StageModeManager.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x00085558 File Offset: 0x00083758
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x00085564 File Offset: 0x00083764
	private void OnDestroy()
	{
		if (StageModeManager.m_instance == this)
		{
			StageModeManager.m_instance = null;
		}
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x0008557C File Offset: 0x0008377C
	private void SetDebugDraw(string msg)
	{
	}

	// Token: 0x040014AE RID: 5294
	private static StageModeManager m_instance;

	// Token: 0x040014AF RID: 5295
	[Header("debugFlag にチェックを入れると、Console にテキストが表示されます")]
	public bool m_debugFlag;

	// Token: 0x040014B0 RID: 5296
	public bool m_firstTutorial;

	// Token: 0x040014B1 RID: 5297
	[Header("モード設定パラメータ")]
	public StageModeManager.Mode m_mode = StageModeManager.Mode.UNKNOWN;

	// Token: 0x040014B2 RID: 5298
	private CharacterAttribute m_stageCharaAttribute;

	// Token: 0x040014B3 RID: 5299
	private int m_stageIndex = 1;

	// Token: 0x02000314 RID: 788
	public enum Mode
	{
		// Token: 0x040014B5 RID: 5301
		ENDLESS,
		// Token: 0x040014B6 RID: 5302
		QUICK,
		// Token: 0x040014B7 RID: 5303
		UNKNOWN
	}
}
