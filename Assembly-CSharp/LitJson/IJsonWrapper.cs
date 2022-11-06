using System;
using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
	// Token: 0x020003DC RID: 988
	public interface IJsonWrapper : IList, ICollection, IDictionary, IEnumerable, IOrderedDictionary
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001C9A RID: 7322
		bool IsArray { get; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001C9B RID: 7323
		bool IsBoolean { get; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001C9C RID: 7324
		bool IsDouble { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001C9D RID: 7325
		bool IsInt { get; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001C9E RID: 7326
		bool IsLong { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001C9F RID: 7327
		bool IsObject { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001CA0 RID: 7328
		bool IsString { get; }

		// Token: 0x06001CA1 RID: 7329
		bool GetBoolean();

		// Token: 0x06001CA2 RID: 7330
		double GetDouble();

		// Token: 0x06001CA3 RID: 7331
		int GetInt();

		// Token: 0x06001CA4 RID: 7332
		JsonType GetJsonType();

		// Token: 0x06001CA5 RID: 7333
		long GetLong();

		// Token: 0x06001CA6 RID: 7334
		string GetString();

		// Token: 0x06001CA7 RID: 7335
		void SetBoolean(bool val);

		// Token: 0x06001CA8 RID: 7336
		void SetDouble(double val);

		// Token: 0x06001CA9 RID: 7337
		void SetInt(int val);

		// Token: 0x06001CAA RID: 7338
		void SetJsonType(JsonType type);

		// Token: 0x06001CAB RID: 7339
		void SetLong(long val);

		// Token: 0x06001CAC RID: 7340
		void SetString(string val);

		// Token: 0x06001CAD RID: 7341
		string ToJson();

		// Token: 0x06001CAE RID: 7342
		void ToJson(JsonWriter writer);
	}
}
