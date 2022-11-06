using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace LitJson
{
	// Token: 0x020003DD RID: 989
	public class JsonData : IList, ICollection, IDictionary, IEnumerable, IOrderedDictionary, IEquatable<JsonData>, IJsonWrapper
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x000A9A50 File Offset: 0x000A7C50
		public JsonData()
		{
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000A9A58 File Offset: 0x000A7C58
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000A9A70 File Offset: 0x000A7C70
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000A9A88 File Offset: 0x000A7C88
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000A9AA0 File Offset: 0x000A7CA0
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000A9AB8 File Offset: 0x000A7CB8
		public JsonData(object obj)
		{
			if (obj is bool)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
				return;
			}
			if (obj is double)
			{
				this.type = JsonType.Double;
				this.inst_double = (double)obj;
				return;
			}
			if (obj is int)
			{
				this.type = JsonType.Int;
				this.inst_int = (int)obj;
				return;
			}
			if (obj is long)
			{
				this.type = JsonType.Long;
				this.inst_long = (long)obj;
				return;
			}
			if (obj is string)
			{
				this.type = JsonType.String;
				this.inst_string = (string)obj;
				return;
			}
			throw new ArgumentException("Unable to wrap the given object with JsonData");
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000A9B70 File Offset: 0x000A7D70
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x000A9B88 File Offset: 0x000A7D88
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x000A9B90 File Offset: 0x000A7D90
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000A9BA0 File Offset: 0x000A7DA0
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x000A9BB0 File Offset: 0x000A7DB0
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000A9BC0 File Offset: 0x000A7DC0
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x000A9BD0 File Offset: 0x000A7DD0
		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Key);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000A9C4C File Offset: 0x000A7E4C
		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Value);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000A9CC8 File Offset: 0x000A7EC8
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000A9CD0 File Offset: 0x000A7ED0
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x000A9CD8 File Offset: 0x000A7ED8
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x000A9CE0 File Offset: 0x000A7EE0
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x000A9CE8 File Offset: 0x000A7EE8
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000A9CF0 File Offset: 0x000A7EF0
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x000A9CF8 File Offset: 0x000A7EF8
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000A9D00 File Offset: 0x000A7F00
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x000A9D10 File Offset: 0x000A7F10
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x17000427 RID: 1063
		object IDictionary.this[object key]
		{
			get
			{
				return this.EnsureDictionary()[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = this.ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		// Token: 0x17000428 RID: 1064
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list[idx].Value;
			}
			set
			{
				this.EnsureDictionary();
				JsonData value2 = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list[idx];
				this.inst_object[keyValuePair.Key] = value2;
				KeyValuePair<string, JsonData> value3 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value2);
				this.object_list[idx] = value3;
			}
		}

		// Token: 0x17000429 RID: 1065
		object IList.this[int index]
		{
			get
			{
				return this.EnsureList()[index];
			}
			set
			{
				this.EnsureList();
				JsonData value2 = this.ToJsonData(value);
				this[index] = value2;
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x000A9E1C File Offset: 0x000A801C
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000A9E2C File Offset: 0x000A802C
		void IDictionary.Add(object key, object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, value2);
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>((string)key, value2);
			this.object_list.Add(item);
			this.json = null;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000A9E70 File Offset: 0x000A8070
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000A9E90 File Offset: 0x000A8090
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000A9EA0 File Offset: 0x000A80A0
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000A9EA8 File Offset: 0x000A80A8
		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.Count; i++)
			{
				if (this.object_list[i].Key == (string)key)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000A9F1C File Offset: 0x000A811C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000A9F2C File Offset: 0x000A812C
		bool IJsonWrapper.GetBoolean()
		{
			if (this.type != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000A9F4C File Offset: 0x000A814C
		double IJsonWrapper.GetDouble()
		{
			if (this.type != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000A9F6C File Offset: 0x000A816C
		int IJsonWrapper.GetInt()
		{
			if (this.type != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000A9F8C File Offset: 0x000A818C
		long IJsonWrapper.GetLong()
		{
			if (this.type != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000A9FAC File Offset: 0x000A81AC
		string IJsonWrapper.GetString()
		{
			if (this.type != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000A9FCC File Offset: 0x000A81CC
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000A9FE4 File Offset: 0x000A81E4
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000A9FFC File Offset: 0x000A81FC
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.json = null;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000AA014 File Offset: 0x000A8214
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.json = null;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000AA02C File Offset: 0x000A822C
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000AA044 File Offset: 0x000A8244
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000AA04C File Offset: 0x000A824C
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000AA058 File Offset: 0x000A8258
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000AA064 File Offset: 0x000A8264
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000AA078 File Offset: 0x000A8278
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000AA088 File Offset: 0x000A8288
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000AA098 File Offset: 0x000A8298
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000AA0B0 File Offset: 0x000A82B0
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000AA0C8 File Offset: 0x000A82C8
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000AA0E0 File Offset: 0x000A82E0
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000AA0FC File Offset: 0x000A82FC
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData value2 = this.ToJsonData(value);
			this[text] = value2;
			KeyValuePair<string, JsonData> item = new KeyValuePair<string, JsonData>(text, value2);
			this.object_list.Insert(idx, item);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000AA138 File Offset: 0x000A8338
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000AA178 File Offset: 0x000A8378
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000AA188 File Offset: 0x000A8388
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000AA194 File Offset: 0x000A8394
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000AA1A0 File Offset: 0x000A83A0
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000AA1AC File Offset: 0x000A83AC
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x000AA1B8 File Offset: 0x000A83B8
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000AA1C4 File Offset: 0x000A83C4
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x000AA1D0 File Offset: 0x000A83D0
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000AA1DC File Offset: 0x000A83DC
		public string GetKey(int index)
		{
			this.EnsureCollection();
			if (this.type == JsonType.Array)
			{
				return this.inst_array[index].GetKey(0);
			}
			return this.object_list[index].Key;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000AA224 File Offset: 0x000A8424
		public bool ContainsKey(string prop_name)
		{
			return this.inst_object.ContainsKey(prop_name);
		}

		// Token: 0x17000432 RID: 1074
		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object[prop_name];
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (this.inst_object.ContainsKey(prop_name))
				{
					for (int i = 0; i < this.object_list.Count; i++)
					{
						if (this.object_list[i].Key == prop_name)
						{
							this.object_list[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object[prop_name] = value;
				this.json = null;
			}
		}

		// Token: 0x17000433 RID: 1075
		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					return this.inst_array[index];
				}
				return this.object_list[index].Value;
			}
			set
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					this.inst_array[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list[index];
					KeyValuePair<string, JsonData> value2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					this.object_list[index] = value2;
					this.inst_object[keyValuePair.Key] = value;
				}
				this.json = null;
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000AA3B4 File Offset: 0x000A85B4
		private ICollection EnsureCollection()
		{
			if (this.type == JsonType.Array)
			{
				return (ICollection)this.inst_array;
			}
			if (this.type == JsonType.Object)
			{
				return (ICollection)this.inst_object;
			}
			throw new InvalidOperationException("The JsonData instance has to be initialized first");
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000AA3FC File Offset: 0x000A85FC
		private IDictionary EnsureDictionary()
		{
			if (this.type == JsonType.Object)
			{
				return (IDictionary)this.inst_object;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			this.type = JsonType.Object;
			this.inst_object = new Dictionary<string, JsonData>();
			this.object_list = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)this.inst_object;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000AA460 File Offset: 0x000A8660
		private IList EnsureList()
		{
			if (this.type == JsonType.Array)
			{
				return (IList)this.inst_array;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			this.type = JsonType.Array;
			this.inst_array = new List<JsonData>();
			return (IList)this.inst_array;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000AA4B8 File Offset: 0x000A86B8
		private JsonData ToJsonData(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is JsonData)
			{
				return (JsonData)obj;
			}
			return new JsonData(obj);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000AA4E8 File Offset: 0x000A86E8
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			if (obj.IsString)
			{
				writer.Write(obj.GetString());
				return;
			}
			if (obj.IsBoolean)
			{
				writer.Write(obj.GetBoolean());
				return;
			}
			if (obj.IsDouble)
			{
				writer.Write(obj.GetDouble());
				return;
			}
			if (obj.IsInt)
			{
				writer.Write(obj.GetInt());
				return;
			}
			if (obj.IsLong)
			{
				writer.Write(obj.GetLong());
				return;
			}
			if (obj.IsArray)
			{
				writer.WriteArrayStart();
				foreach (object obj2 in obj)
				{
					JsonData.WriteJson((JsonData)obj2, writer);
				}
				writer.WriteArrayEnd();
				return;
			}
			if (obj.IsObject)
			{
				writer.WriteObjectStart();
				foreach (object obj3 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					writer.WritePropertyName((string)dictionaryEntry.Key);
					JsonData.WriteJson((JsonData)dictionaryEntry.Value, writer);
				}
				writer.WriteObjectEnd();
				return;
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000AA678 File Offset: 0x000A8878
		public int Add(object value)
		{
			JsonData value2 = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(value2);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000AA6A0 File Offset: 0x000A88A0
		public void Clear()
		{
			if (this.IsObject)
			{
				((IDictionary)this).Clear();
				return;
			}
			if (this.IsArray)
			{
				((IList)this).Clear();
				return;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000AA6D4 File Offset: 0x000A88D4
		public bool Equals(JsonData x)
		{
			if (x == null)
			{
				return false;
			}
			if (x.type != this.type)
			{
				return false;
			}
			switch (this.type)
			{
			case JsonType.None:
				return true;
			case JsonType.Object:
				return this.inst_object.Equals(x.inst_object);
			case JsonType.Array:
				return this.inst_array.Equals(x.inst_array);
			case JsonType.String:
				return this.inst_string.Equals(x.inst_string);
			case JsonType.Int:
				return this.inst_int.Equals(x.inst_int);
			case JsonType.Long:
				return this.inst_long.Equals(x.inst_long);
			case JsonType.Double:
				return this.inst_double.Equals(x.inst_double);
			case JsonType.Boolean:
				return this.inst_boolean.Equals(x.inst_boolean);
			default:
				return false;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000AA7B0 File Offset: 0x000A89B0
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000AA7B8 File Offset: 0x000A89B8
		public void SetJsonType(JsonType type)
		{
			if (this.type == type)
			{
				return;
			}
			switch (type)
			{
			case JsonType.Object:
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				break;
			case JsonType.Array:
				this.inst_array = new List<JsonData>();
				break;
			case JsonType.String:
				this.inst_string = null;
				break;
			case JsonType.Int:
				this.inst_int = 0;
				break;
			case JsonType.Long:
				this.inst_long = 0L;
				break;
			case JsonType.Double:
				this.inst_double = 0.0;
				break;
			case JsonType.Boolean:
				this.inst_boolean = false;
				break;
			}
			this.type = type;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000AA87C File Offset: 0x000A8A7C
		public string ToJson()
		{
			if (this.json != null)
			{
				return this.json;
			}
			StringWriter stringWriter = new StringWriter();
			JsonData.WriteJson(this, new JsonWriter(stringWriter)
			{
				Validate = false
			});
			this.json = stringWriter.ToString();
			return this.json;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000AA8C8 File Offset: 0x000A8AC8
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000AA8F4 File Offset: 0x000A8AF4
		public override string ToString()
		{
			switch (this.type)
			{
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return this.inst_string;
			case JsonType.Int:
				return this.inst_int.ToString();
			case JsonType.Long:
				return this.inst_long.ToString();
			case JsonType.Double:
				return this.inst_double.ToString();
			case JsonType.Boolean:
				return this.inst_boolean.ToString();
			default:
				return "Uninitialized JsonData";
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000AA97C File Offset: 0x000A8B7C
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000AA984 File Offset: 0x000A8B84
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000AA98C File Offset: 0x000A8B8C
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000AA994 File Offset: 0x000A8B94
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000AA99C File Offset: 0x000A8B9C
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000AA9A4 File Offset: 0x000A8BA4
		public static explicit operator bool(JsonData data)
		{
			if (data.type != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000AA9C4 File Offset: 0x000A8BC4
		public static explicit operator double(JsonData data)
		{
			if (data.type != JsonType.Double)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		public static explicit operator int(JsonData data)
		{
			if (data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000AAA04 File Offset: 0x000A8C04
		public static explicit operator long(JsonData data)
		{
			if (data.type != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000AAA24 File Offset: 0x000A8C24
		public static explicit operator string(JsonData data)
		{
			if (data.type != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x04001A7E RID: 6782
		private IList<JsonData> inst_array;

		// Token: 0x04001A7F RID: 6783
		private bool inst_boolean;

		// Token: 0x04001A80 RID: 6784
		private double inst_double;

		// Token: 0x04001A81 RID: 6785
		private int inst_int;

		// Token: 0x04001A82 RID: 6786
		private long inst_long;

		// Token: 0x04001A83 RID: 6787
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x04001A84 RID: 6788
		private string inst_string;

		// Token: 0x04001A85 RID: 6789
		private string json;

		// Token: 0x04001A86 RID: 6790
		private JsonType type;

		// Token: 0x04001A87 RID: 6791
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}
