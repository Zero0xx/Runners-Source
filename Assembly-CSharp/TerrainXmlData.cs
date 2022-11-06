using System;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class TerrainXmlData : MonoBehaviour
{
	// Token: 0x170002CC RID: 716
	// (get) Token: 0x060011BB RID: 4539 RVA: 0x000647DC File Offset: 0x000629DC
	public static string DataAssetName
	{
		get
		{
			return TerrainXmlData.m_setDataAssetName;
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000647E4 File Offset: 0x000629E4
	public static void SetAssetName(string stageName)
	{
		TerrainXmlData.m_setDataAssetName = stageName + "_TerrainBlockData";
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x060011BD RID: 4541 RVA: 0x000647F8 File Offset: 0x000629F8
	public TextAsset TerrainBlock
	{
		get
		{
			return this.m_terrainBlock;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x060011BE RID: 4542 RVA: 0x00064800 File Offset: 0x00062A00
	public TextAsset SideViewPath
	{
		get
		{
			return this.m_sideViewPath;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x060011BF RID: 4543 RVA: 0x00064808 File Offset: 0x00062A08
	public TextAsset LoopPath
	{
		get
		{
			return this.m_loopPath;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00064810 File Offset: 0x00062A10
	public TextAsset[] SetData
	{
		get
		{
			return this.m_setData;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00064818 File Offset: 0x00062A18
	public TextAsset ItemTableData
	{
		get
		{
			return this.m_itemTableData;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00064820 File Offset: 0x00062A20
	public TextAsset RareEnemyTableData
	{
		get
		{
			return this.m_rareEnemyTableData;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00064828 File Offset: 0x00062A28
	public TextAsset BossTableData
	{
		get
		{
			return this.m_bossTableData;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00064830 File Offset: 0x00062A30
	public TextAsset BossMap3TableData
	{
		get
		{
			return this.m_bossMap3TableData;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00064838 File Offset: 0x00062A38
	public TextAsset ObjectPartTableData
	{
		get
		{
			return this.m_objectPartTableData;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00064840 File Offset: 0x00062A40
	public TextAsset EnemyExtendItemTableData
	{
		get
		{
			return this.m_EnemyExtendItemTableData;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00064848 File Offset: 0x00062A48
	public int MoveTrapBooRand
	{
		get
		{
			return this.m_moveTrapBooRand;
		}
	}

	// Token: 0x0400100D RID: 4109
	public const string DefaultSetDataAssetName = "TerrainBlockData";

	// Token: 0x0400100E RID: 4110
	[SerializeField]
	private TextAsset m_terrainBlock;

	// Token: 0x0400100F RID: 4111
	[SerializeField]
	private TextAsset m_sideViewPath;

	// Token: 0x04001010 RID: 4112
	[SerializeField]
	private TextAsset m_loopPath;

	// Token: 0x04001011 RID: 4113
	[SerializeField]
	private TextAsset[] m_setData = new TextAsset[22];

	// Token: 0x04001012 RID: 4114
	[SerializeField]
	private TextAsset m_itemTableData;

	// Token: 0x04001013 RID: 4115
	[SerializeField]
	private TextAsset m_rareEnemyTableData;

	// Token: 0x04001014 RID: 4116
	[SerializeField]
	private TextAsset m_bossTableData;

	// Token: 0x04001015 RID: 4117
	[SerializeField]
	private TextAsset m_bossMap3TableData;

	// Token: 0x04001016 RID: 4118
	[SerializeField]
	private TextAsset m_objectPartTableData;

	// Token: 0x04001017 RID: 4119
	[SerializeField]
	private TextAsset m_EnemyExtendItemTableData;

	// Token: 0x04001018 RID: 4120
	[SerializeField]
	private int m_moveTrapBooRand;

	// Token: 0x04001019 RID: 4121
	public static string m_setDataAssetName = "TerrainBlockData";
}
