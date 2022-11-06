using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class ByteReader
{
	// Token: 0x0600045E RID: 1118 RVA: 0x000163C8 File Offset: 0x000145C8
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000163D8 File Offset: 0x000145D8
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000460 RID: 1120 RVA: 0x000163EC File Offset: 0x000145EC
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001640C File Offset: 0x0001460C
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001641C File Offset: 0x0001461C
	public string ReadLine()
	{
		int num = this.mBuffer.Length;
		while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
		{
			this.mOffset++;
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_81:
					string result = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return result;
				}
			}
			i++;
			goto IL_81;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000164DC File Offset: 0x000146DC
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] separator = new char[]
		{
			'='
		};
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			if (!text.StartsWith("//"))
			{
				string[] array = text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					string key = array[0].Trim();
					string value = array[1].Trim().Replace("\\n", "\n");
					dictionary[key] = value;
				}
			}
		}
		return dictionary;
	}

	// Token: 0x04000347 RID: 839
	private byte[] mBuffer;

	// Token: 0x04000348 RID: 840
	private int mOffset;
}
