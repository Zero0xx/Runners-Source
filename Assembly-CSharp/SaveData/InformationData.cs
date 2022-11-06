using System;
using System.Collections.Generic;

namespace SaveData
{
	// Token: 0x020002A5 RID: 677
	public class InformationData
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x00068588 File Offset: 0x00066788
		public InformationData()
		{
			this.Init();
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000685B0 File Offset: 0x000667B0
		private bool CheckData(InformationData.DataType dataType)
		{
			return InformationData.DataType.ID <= dataType && dataType < InformationData.DataType.NUM;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000685C0 File Offset: 0x000667C0
		public string GetData(string id, InformationData.DataType dataType)
		{
			if (this.m_textArray != null && this.CheckData(dataType))
			{
				for (int i = 0; i < this.m_textArray.Count; i++)
				{
					string[] array = this.m_textArray[i].Split(new char[]
					{
						','
					});
					if ((InformationData.DataType)array.Length > dataType && array[0] == id)
					{
						return array[(int)dataType];
					}
				}
			}
			return InformationData.INVALID_PARAM;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00068640 File Offset: 0x00066840
		public string GetData(int index, InformationData.DataType dataType)
		{
			if (this.m_textArray != null && this.CheckData(dataType) && index < this.m_textArray.Count)
			{
				string[] array = this.m_textArray[index].Split(new char[]
				{
					','
				});
				if ((InformationData.DataType)array.Length > dataType)
				{
					return array[(int)dataType];
				}
			}
			return InformationData.INVALID_PARAM;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000686A4 File Offset: 0x000668A4
		public string GetEventRankingData(string id, string saveKey, InformationData.DataType dataType)
		{
			if (this.m_textArray != null && this.CheckData(dataType))
			{
				for (int i = 0; i < this.m_textArray.Count; i++)
				{
					string[] array = this.m_textArray[i].Split(new char[]
					{
						','
					});
					if ((InformationData.DataType)array.Length > dataType && array[0] == id && array[2] == saveKey)
					{
						return array[(int)dataType];
					}
				}
			}
			return InformationData.INVALID_PARAM;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00068730 File Offset: 0x00066930
		public void UpdateShowedTime(string id, string showedTime, string addInfo, string imageId)
		{
			if (this.m_textArray != null)
			{
				bool flag = false;
				for (int i = 0; i < this.m_textArray.Count; i++)
				{
					string[] array = this.m_textArray[i].Split(new char[]
					{
						','
					});
					if (array.Length > 0 && array[0] == id)
					{
						this.m_textArray[i] = string.Concat(new string[]
						{
							id,
							",",
							showedTime,
							",",
							addInfo,
							",",
							imageId
						});
						flag = true;
						this.m_isDirty = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_textArray.Add(string.Concat(new string[]
					{
						id,
						",",
						showedTime,
						",",
						addInfo,
						",",
						imageId
					}));
					this.m_isDirty = true;
				}
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00068834 File Offset: 0x00066A34
		public void UpdateEventRankingShowedTime(string id, string showedTime, string addInfo, string imageId)
		{
			if (this.m_textArray != null)
			{
				bool flag = false;
				for (int i = 0; i < this.m_textArray.Count; i++)
				{
					string[] array = this.m_textArray[i].Split(new char[]
					{
						','
					});
					if (array.Length > 2 && array[0] == id && array[2] == addInfo)
					{
						this.m_textArray[i] = string.Concat(new string[]
						{
							id,
							",",
							showedTime,
							",",
							addInfo,
							",",
							imageId
						});
						flag = true;
						this.m_isDirty = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_textArray.Add(string.Concat(new string[]
					{
						id,
						",",
						showedTime,
						",",
						addInfo,
						",",
						imageId
					}));
					this.m_isDirty = true;
				}
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00068948 File Offset: 0x00066B48
		public int DataCount()
		{
			if (this.m_textArray != null)
			{
				return this.m_textArray.Count;
			}
			return 0;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00068964 File Offset: 0x00066B64
		public void Reset(int index)
		{
			if (this.m_textArray != null && index < this.m_textArray.Count)
			{
				this.m_textArray.RemoveAt(index);
				this.m_isDirty = true;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00068998 File Offset: 0x00066B98
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x000689A0 File Offset: 0x00066BA0
		public List<string> TextArray
		{
			get
			{
				return this.m_textArray;
			}
			set
			{
				this.m_textArray = value;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000689AC File Offset: 0x00066BAC
		public void Init()
		{
			if (this.m_textArray != null)
			{
				for (int i = 0; i < this.m_textArray.Count; i++)
				{
					this.m_textArray[i] = "-1,-1,-1,-1";
				}
			}
			this.m_isDirty = false;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000689F8 File Offset: 0x00066BF8
		public void CopyTo(InformationData dst)
		{
			dst.TextArray = this.m_textArray;
			dst.m_isDirty = this.m_isDirty;
		}

		// Token: 0x04001098 RID: 4248
		public const string RESET_PARAME = "-1,-1,-1,-1";

		// Token: 0x04001099 RID: 4249
		private List<string> m_textArray = new List<string>();

		// Token: 0x0400109A RID: 4250
		public bool m_isDirty;

		// Token: 0x0400109B RID: 4251
		public static string INVALID_PARAM = "-1";

		// Token: 0x020002A6 RID: 678
		public enum DataType
		{
			// Token: 0x0400109D RID: 4253
			ID,
			// Token: 0x0400109E RID: 4254
			SHOWED_TIME,
			// Token: 0x0400109F RID: 4255
			ADD_INFO,
			// Token: 0x040010A0 RID: 4256
			IMAGE_ID,
			// Token: 0x040010A1 RID: 4257
			NUM
		}
	}
}
