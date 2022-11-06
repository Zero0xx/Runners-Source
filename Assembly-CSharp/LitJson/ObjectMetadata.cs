using System;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x020003E2 RID: 994
	internal struct ObjectMetadata
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000AABD0 File Offset: 0x000A8DD0
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x000AABF0 File Offset: 0x000A8DF0
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000AABFC File Offset: 0x000A8DFC
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x000AAC04 File Offset: 0x000A8E04
		public bool IsDictionary
		{
			get
			{
				return this.is_dictionary;
			}
			set
			{
				this.is_dictionary = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x000AAC10 File Offset: 0x000A8E10
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x000AAC18 File Offset: 0x000A8E18
		public IDictionary<string, PropertyMetadata> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x04001A8F RID: 6799
		private Type element_type;

		// Token: 0x04001A90 RID: 6800
		private bool is_dictionary;

		// Token: 0x04001A91 RID: 6801
		private IDictionary<string, PropertyMetadata> properties;
	}
}
