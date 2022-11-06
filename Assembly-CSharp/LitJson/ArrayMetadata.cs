using System;

namespace LitJson
{
	// Token: 0x020003E1 RID: 993
	internal struct ArrayMetadata
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x000AAB7C File Offset: 0x000A8D7C
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x000AAB9C File Offset: 0x000A8D9C
		public Type ElementType
		{
			get
			{
				if (this.element_type == null)
				{
					return typeof(JsonData);
				}
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x000AABA8 File Offset: 0x000A8DA8
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x000AABB0 File Offset: 0x000A8DB0
		public bool IsArray
		{
			get
			{
				return this.is_array;
			}
			set
			{
				this.is_array = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000AABBC File Offset: 0x000A8DBC
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x000AABC4 File Offset: 0x000A8DC4
		public bool IsList
		{
			get
			{
				return this.is_list;
			}
			set
			{
				this.is_list = value;
			}
		}

		// Token: 0x04001A8C RID: 6796
		private Type element_type;

		// Token: 0x04001A8D RID: 6797
		private bool is_array;

		// Token: 0x04001A8E RID: 6798
		private bool is_list;
	}
}
