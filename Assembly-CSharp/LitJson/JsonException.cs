using System;

namespace LitJson
{
	// Token: 0x020003DF RID: 991
	public class JsonException : ApplicationException
	{
		// Token: 0x06001D15 RID: 7445 RVA: 0x000AAAF0 File Offset: 0x000A8CF0
		public JsonException()
		{
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000AAAF8 File Offset: 0x000A8CF8
		internal JsonException(ParserToken token) : base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000AAB10 File Offset: 0x000A8D10
		internal JsonException(ParserToken token, Exception inner_exception) : base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000AAB2C File Offset: 0x000A8D2C
		internal JsonException(int c) : base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000AAB48 File Offset: 0x000A8D48
		internal JsonException(int c, Exception inner_exception) : base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000AAB64 File Offset: 0x000A8D64
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000AAB70 File Offset: 0x000A8D70
		public JsonException(string message, Exception inner_exception) : base(message, inner_exception)
		{
		}
	}
}
