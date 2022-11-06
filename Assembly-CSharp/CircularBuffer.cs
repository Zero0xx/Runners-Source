using System;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
internal class CircularBuffer<T>
{
	// Token: 0x060041B2 RID: 16818 RVA: 0x00156214 File Offset: 0x00154414
	public CircularBuffer(int capacity)
	{
		this.m_Buffer = new T[capacity];
	}

	// Token: 0x170008EB RID: 2283
	// (get) Token: 0x060041B3 RID: 16819 RVA: 0x00156228 File Offset: 0x00154428
	public int Capacity
	{
		get
		{
			return this.m_Buffer.Length;
		}
	}

	// Token: 0x170008EC RID: 2284
	// (get) Token: 0x060041B4 RID: 16820 RVA: 0x00156234 File Offset: 0x00154434
	public int Size
	{
		get
		{
			return this.m_Size;
		}
	}

	// Token: 0x170008ED RID: 2285
	// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0015623C File Offset: 0x0015443C
	public int Head
	{
		get
		{
			return this.m_Head;
		}
	}

	// Token: 0x170008EE RID: 2286
	// (get) Token: 0x060041B6 RID: 16822 RVA: 0x00156244 File Offset: 0x00154444
	public int Tail
	{
		get
		{
			return this.m_Tail;
		}
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x0015624C File Offset: 0x0015444C
	public T GetAt(int index)
	{
		return this.m_Buffer[index];
	}

	// Token: 0x060041B8 RID: 16824 RVA: 0x0015625C File Offset: 0x0015445C
	public void Add(T item)
	{
		this.m_Tail = this.m_Cursor;
		this.m_Buffer[this.m_Cursor] = item;
		this.m_Cursor = (this.m_Cursor + 1) % this.Capacity;
		this.m_Head = ((this.m_Size >= this.Capacity) ? this.m_Cursor : 0);
		this.m_Size = Mathf.Min(this.m_Size + 1, this.Capacity);
	}

	// Token: 0x04003811 RID: 14353
	private T[] m_Buffer;

	// Token: 0x04003812 RID: 14354
	private int m_Cursor;

	// Token: 0x04003813 RID: 14355
	private int m_Head;

	// Token: 0x04003814 RID: 14356
	private int m_Tail;

	// Token: 0x04003815 RID: 14357
	private int m_Size;
}
