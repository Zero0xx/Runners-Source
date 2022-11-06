using System;

namespace LitJson
{
	// Token: 0x020003E4 RID: 996
	public enum JsonToken
	{
		// Token: 0x04001ABB RID: 6843
		None,
		// Token: 0x04001ABC RID: 6844
		ObjectStart,
		// Token: 0x04001ABD RID: 6845
		PropertyName,
		// Token: 0x04001ABE RID: 6846
		ObjectEnd,
		// Token: 0x04001ABF RID: 6847
		ArrayStart,
		// Token: 0x04001AC0 RID: 6848
		ArrayEnd,
		// Token: 0x04001AC1 RID: 6849
		Int,
		// Token: 0x04001AC2 RID: 6850
		Long,
		// Token: 0x04001AC3 RID: 6851
		Double,
		// Token: 0x04001AC4 RID: 6852
		String,
		// Token: 0x04001AC5 RID: 6853
		Boolean,
		// Token: 0x04001AC6 RID: 6854
		Null
	}
}
