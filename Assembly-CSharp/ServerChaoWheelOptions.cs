using System;

// Token: 0x020007F5 RID: 2037
public class ServerChaoWheelOptions
{
	// Token: 0x06003680 RID: 13952 RVA: 0x00121560 File Offset: 0x0011F760
	public ServerChaoWheelOptions()
	{
		this.Rarities = new int[8];
		this.ItemWeight = new int[8];
		for (int i = 0; i < 8; i++)
		{
			this.Rarities[i] = 0;
			this.ItemWeight[i] = 0;
		}
		this.m_multi = 1;
		this.Cost = 0;
		this.SpinType = ServerChaoWheelOptions.ChaoSpinType.Normal;
		this.NumSpecialEggs = 0;
		this.IsValid = true;
		this.NumRouletteToken = 0;
		this.IsTutorial = false;
		this.IsConnected = false;
	}

	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x06003681 RID: 13953 RVA: 0x001215E8 File Offset: 0x0011F7E8
	// (set) Token: 0x06003682 RID: 13954 RVA: 0x001215F0 File Offset: 0x0011F7F0
	public int[] Rarities { get; set; }

	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x06003683 RID: 13955 RVA: 0x001215FC File Offset: 0x0011F7FC
	// (set) Token: 0x06003684 RID: 13956 RVA: 0x00121604 File Offset: 0x0011F804
	public int[] ItemWeight { get; set; }

	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x06003685 RID: 13957 RVA: 0x00121610 File Offset: 0x0011F810
	// (set) Token: 0x06003686 RID: 13958 RVA: 0x00121618 File Offset: 0x0011F818
	public int Cost { get; set; }

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x06003687 RID: 13959 RVA: 0x00121624 File Offset: 0x0011F824
	// (set) Token: 0x06003688 RID: 13960 RVA: 0x0012162C File Offset: 0x0011F82C
	public ServerChaoWheelOptions.ChaoSpinType SpinType { get; set; }

	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x06003689 RID: 13961 RVA: 0x00121638 File Offset: 0x0011F838
	// (set) Token: 0x0600368A RID: 13962 RVA: 0x00121640 File Offset: 0x0011F840
	public int NumSpecialEggs { get; set; }

	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x0600368B RID: 13963 RVA: 0x0012164C File Offset: 0x0011F84C
	// (set) Token: 0x0600368C RID: 13964 RVA: 0x00121654 File Offset: 0x0011F854
	public bool IsValid { get; set; }

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x0600368D RID: 13965 RVA: 0x00121660 File Offset: 0x0011F860
	// (set) Token: 0x0600368E RID: 13966 RVA: 0x00121668 File Offset: 0x0011F868
	public int NumRouletteToken { get; set; }

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x0600368F RID: 13967 RVA: 0x00121674 File Offset: 0x0011F874
	// (set) Token: 0x06003690 RID: 13968 RVA: 0x0012167C File Offset: 0x0011F87C
	public bool IsTutorial { get; set; }

	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x06003691 RID: 13969 RVA: 0x00121688 File Offset: 0x0011F888
	// (set) Token: 0x06003692 RID: 13970 RVA: 0x00121690 File Offset: 0x0011F890
	public bool IsConnected { get; set; }

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x06003693 RID: 13971 RVA: 0x0012169C File Offset: 0x0011F89C
	public int multi
	{
		get
		{
			return this.m_multi;
		}
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x001216A4 File Offset: 0x0011F8A4
	public bool ChangeMulti(int multi)
	{
		bool flag = this.IsMulti(multi);
		if (flag)
		{
			this.m_multi = multi;
			if (this.m_multi < 1)
			{
				this.m_multi = 1;
			}
		}
		else
		{
			this.m_multi = 1;
		}
		return flag;
	}

	// Token: 0x06003695 RID: 13973 RVA: 0x001216E8 File Offset: 0x0011F8E8
	public bool IsMulti(int multi)
	{
		bool result = false;
		if (multi <= 1)
		{
			result = true;
		}
		else
		{
			int cost = this.Cost;
			int num;
			if (this.NumRouletteToken > 0 && this.NumRouletteToken >= this.Cost)
			{
				num = this.NumRouletteToken;
				cost = this.Cost;
			}
			else
			{
				num = (int)SaveDataManager.Instance.ItemData.RedRingCount;
				cost = this.Cost;
			}
			if (num >= cost * multi)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06003696 RID: 13974 RVA: 0x00121764 File Offset: 0x0011F964
	public void Dump()
	{
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x00121768 File Offset: 0x0011F968
	public void CopyTo(ServerChaoWheelOptions to)
	{
		to.Cost = this.Cost;
		to.SpinType = this.SpinType;
		to.Rarities = (this.Rarities.Clone() as int[]);
		to.ItemWeight = (this.ItemWeight.Clone() as int[]);
		to.NumSpecialEggs = this.NumSpecialEggs;
		to.IsValid = this.IsValid;
		to.NumRouletteToken = this.NumRouletteToken;
		to.IsTutorial = this.IsTutorial;
		to.IsConnected = this.IsConnected;
		to.m_multi = this.m_multi;
	}

	// Token: 0x04002DDF RID: 11743
	private int m_multi;

	// Token: 0x020007F6 RID: 2038
	public enum ChaoSpinType
	{
		// Token: 0x04002DEA RID: 11754
		Normal,
		// Token: 0x04002DEB RID: 11755
		Special,
		// Token: 0x04002DEC RID: 11756
		NUM
	}
}
