using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x020003DE RID: 990
	internal class OrderedDictionaryEnumerator : IEnumerator, IDictionaryEnumerator
	{
		// Token: 0x06001D0E RID: 7438 RVA: 0x000AAA44 File Offset: 0x000A8C44
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x000AAA54 File Offset: 0x000A8C54
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x000AAA64 File Offset: 0x000A8C64
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000AAA90 File Offset: 0x000A8C90
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000AAAB0 File Offset: 0x000A8CB0
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000AAAD0 File Offset: 0x000A8CD0
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000AAAE0 File Offset: 0x000A8CE0
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x04001A88 RID: 6792
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}
