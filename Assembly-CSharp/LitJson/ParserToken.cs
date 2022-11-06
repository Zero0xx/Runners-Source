using System;

namespace LitJson
{
	// Token: 0x020003EB RID: 1003
	internal enum ParserToken
	{
		// Token: 0x04001AFE RID: 6910
		None = 65536,
		// Token: 0x04001AFF RID: 6911
		Number,
		// Token: 0x04001B00 RID: 6912
		True,
		// Token: 0x04001B01 RID: 6913
		False,
		// Token: 0x04001B02 RID: 6914
		Null,
		// Token: 0x04001B03 RID: 6915
		CharSeq,
		// Token: 0x04001B04 RID: 6916
		Char,
		// Token: 0x04001B05 RID: 6917
		Text,
		// Token: 0x04001B06 RID: 6918
		Object,
		// Token: 0x04001B07 RID: 6919
		ObjectPrime,
		// Token: 0x04001B08 RID: 6920
		Pair,
		// Token: 0x04001B09 RID: 6921
		PairRest,
		// Token: 0x04001B0A RID: 6922
		Array,
		// Token: 0x04001B0B RID: 6923
		ArrayPrime,
		// Token: 0x04001B0C RID: 6924
		Value,
		// Token: 0x04001B0D RID: 6925
		ValueRest,
		// Token: 0x04001B0E RID: 6926
		String,
		// Token: 0x04001B0F RID: 6927
		End,
		// Token: 0x04001B10 RID: 6928
		Epsilon
	}
}
