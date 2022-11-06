using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class StageInfo : MonoBehaviour
{
	// Token: 0x060017CE RID: 6094 RVA: 0x00088388 File Offset: 0x00086588
	public static string GetStageNameByIndex(int index)
	{
		return "w" + index.ToString("D2");
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x060017CF RID: 6095 RVA: 0x000883B0 File Offset: 0x000865B0
	// (set) Token: 0x060017D0 RID: 6096 RVA: 0x000883B8 File Offset: 0x000865B8
	public string SelectedStageName
	{
		get
		{
			return this.m_stageName;
		}
		set
		{
			this.m_stageName = value;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000883C4 File Offset: 0x000865C4
	// (set) Token: 0x060017D2 RID: 6098 RVA: 0x000883CC File Offset: 0x000865CC
	public BossType BossType
	{
		get
		{
			return this.m_bossType;
		}
		set
		{
			this.m_bossType = value;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000883D8 File Offset: 0x000865D8
	// (set) Token: 0x060017D4 RID: 6100 RVA: 0x000883E0 File Offset: 0x000865E0
	public int NumBossAttack
	{
		get
		{
			return this.m_numBossAttack;
		}
		set
		{
			this.m_numBossAttack = value;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000883EC File Offset: 0x000865EC
	// (set) Token: 0x060017D6 RID: 6102 RVA: 0x000883F4 File Offset: 0x000865F4
	public TenseType TenseType
	{
		get
		{
			return this.m_tenseType;
		}
		set
		{
			this.m_tenseType = value;
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00088400 File Offset: 0x00086600
	// (set) Token: 0x060017D8 RID: 6104 RVA: 0x00088408 File Offset: 0x00086608
	public StageInfo.MileageMapInfo MileageInfo
	{
		get
		{
			return this.m_mapInfo;
		}
		set
		{
			this.m_mapInfo = value;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00088414 File Offset: 0x00086614
	// (set) Token: 0x060017DA RID: 6106 RVA: 0x0008841C File Offset: 0x0008661C
	public bool NotChangeTense
	{
		get
		{
			return this.m_notChangeTense;
		}
		set
		{
			this.m_notChangeTense = value;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x060017DB RID: 6107 RVA: 0x00088428 File Offset: 0x00086628
	// (set) Token: 0x060017DC RID: 6108 RVA: 0x00088430 File Offset: 0x00086630
	public bool ExistBoss
	{
		get
		{
			return this.m_existBoss;
		}
		set
		{
			this.m_existBoss = value;
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x060017DD RID: 6109 RVA: 0x0008843C File Offset: 0x0008663C
	// (set) Token: 0x060017DE RID: 6110 RVA: 0x00088444 File Offset: 0x00086644
	public bool BossStage
	{
		get
		{
			return this.m_bossStage;
		}
		set
		{
			this.m_bossStage = value;
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x060017DF RID: 6111 RVA: 0x00088450 File Offset: 0x00086650
	// (set) Token: 0x060017E0 RID: 6112 RVA: 0x00088458 File Offset: 0x00086658
	public bool FromTitle
	{
		get
		{
			return this.m_fromTitle;
		}
		set
		{
			this.m_fromTitle = value;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00088464 File Offset: 0x00086664
	// (set) Token: 0x060017E2 RID: 6114 RVA: 0x0008846C File Offset: 0x0008666C
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

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00088478 File Offset: 0x00086678
	// (set) Token: 0x060017E4 RID: 6116 RVA: 0x00088480 File Offset: 0x00086680
	public bool TutorialStage
	{
		get
		{
			return this.m_tutorialStage;
		}
		set
		{
			this.m_tutorialStage = value;
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0008848C File Offset: 0x0008668C
	// (set) Token: 0x060017E6 RID: 6118 RVA: 0x00088494 File Offset: 0x00086694
	public bool EventStage
	{
		get
		{
			return this.m_eventStage;
		}
		set
		{
			this.m_eventStage = value;
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000884A0 File Offset: 0x000866A0
	// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000884A8 File Offset: 0x000866A8
	public bool QuickMode
	{
		get
		{
			return this.m_quickMode;
		}
		set
		{
			this.m_quickMode = value;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000884B4 File Offset: 0x000866B4
	// (set) Token: 0x060017EA RID: 6122 RVA: 0x000884BC File Offset: 0x000866BC
	public bool[] BoostItemValid
	{
		get
		{
			return this.m_boostItemValid;
		}
		set
		{
			this.m_boostItemValid = value;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x060017EB RID: 6123 RVA: 0x000884C8 File Offset: 0x000866C8
	// (set) Token: 0x060017EC RID: 6124 RVA: 0x000884D0 File Offset: 0x000866D0
	public ItemType[] EquippedItems
	{
		get
		{
			return this.m_equippedItem;
		}
		set
		{
			this.m_equippedItem = new ItemType[value.Length];
			value.CopyTo(this.m_equippedItem, 0);
		}
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000884F0 File Offset: 0x000866F0
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.m_stageName = string.Empty;
		this.m_bossType = BossType.FEVER;
		this.m_tenseType = TenseType.AFTERNOON;
		this.m_numBossAttack = 0;
		this.m_mapInfo = new StageInfo.MileageMapInfo();
		this.m_boostItemValid = new bool[3];
		this.m_equippedItem = new ItemType[3];
		for (int i = 0; i < 3; i++)
		{
			this.m_equippedItem[i] = ItemType.UNKNOWN;
		}
		base.enabled = false;
	}

	// Token: 0x04001570 RID: 5488
	private string m_stageName;

	// Token: 0x04001571 RID: 5489
	private BossType m_bossType;

	// Token: 0x04001572 RID: 5490
	private int m_numBossAttack;

	// Token: 0x04001573 RID: 5491
	private TenseType m_tenseType;

	// Token: 0x04001574 RID: 5492
	private StageInfo.MileageMapInfo m_mapInfo;

	// Token: 0x04001575 RID: 5493
	private bool m_notChangeTense;

	// Token: 0x04001576 RID: 5494
	private bool m_existBoss;

	// Token: 0x04001577 RID: 5495
	private bool m_bossStage;

	// Token: 0x04001578 RID: 5496
	private bool m_fromTitle;

	// Token: 0x04001579 RID: 5497
	private bool m_tutorialStage;

	// Token: 0x0400157A RID: 5498
	private bool m_eventStage;

	// Token: 0x0400157B RID: 5499
	private bool m_quickMode;

	// Token: 0x0400157C RID: 5500
	private bool m_firstTutorial;

	// Token: 0x0400157D RID: 5501
	private bool[] m_boostItemValid;

	// Token: 0x0400157E RID: 5502
	private ItemType[] m_equippedItem;

	// Token: 0x02000327 RID: 807
	public class MileageMapInfo
	{
		// Token: 0x0400157F RID: 5503
		public MileageMapState m_mapState = new MileageMapState();

		// Token: 0x04001580 RID: 5504
		public long[] m_pointScore = new long[6];
	}
}
