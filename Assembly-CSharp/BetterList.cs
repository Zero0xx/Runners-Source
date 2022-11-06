using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class BetterList<T>
{
	// Token: 0x0600044F RID: 1103 RVA: 0x00015F28 File Offset: 0x00014128
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			for (int i = 0; i < this.size; i++)
			{
				yield return this.buffer[i];
			}
		}
		yield break;
	}

	// Token: 0x170000A0 RID: 160
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00015F64 File Offset: 0x00014164
	private void AllocateMore()
	{
		T[] array = (this.buffer == null) ? new T[32] : new T[Mathf.Max(this.buffer.Length << 1, 32)];
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00015FCC File Offset: 0x000141CC
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00016044 File Offset: 0x00014244
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00016050 File Offset: 0x00014250
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00016060 File Offset: 0x00014260
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		this.buffer[this.size++] = item;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x000160B0 File Offset: 0x000142B0
	public void Insert(int index, T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		if (index < this.size)
		{
			for (int i = this.size; i > index; i--)
			{
				this.buffer[i] = this.buffer[i - 1];
			}
			this.buffer[index] = item;
			this.size++;
		}
		else
		{
			this.Add(item);
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00016148 File Offset: 0x00014348
	public bool Contains(T item)
	{
		if (this.buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x000161A0 File Offset: 0x000143A0
	public bool Remove(T item)
	{
		if (this.buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.size; i++)
			{
				if (@default.Equals(this.buffer[i], item))
				{
					this.size--;
					this.buffer[i] = default(T);
					for (int j = i; j < this.size; j++)
					{
						this.buffer[j] = this.buffer[j + 1];
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00016244 File Offset: 0x00014444
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x000162BC File Offset: 0x000144BC
	public T Pop()
	{
		if (this.buffer != null && this.size != 0)
		{
			T result = this.buffer[--this.size];
			this.buffer[this.size] = default(T);
			return result;
		}
		return default(T);
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00016324 File Offset: 0x00014524
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00016334 File Offset: 0x00014534
	public void Sort(Comparison<T> comparer)
	{
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = 1; i < this.size; i++)
			{
				if (comparer(this.buffer[i - 1], this.buffer[i]) > 0)
				{
					T t = this.buffer[i];
					this.buffer[i] = this.buffer[i - 1];
					this.buffer[i - 1] = t;
					flag = true;
				}
			}
		}
	}

	// Token: 0x04000345 RID: 837
	public T[] buffer;

	// Token: 0x04000346 RID: 838
	public int size;
}
