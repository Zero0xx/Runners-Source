using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class UIGeometry
{
	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001A17C File Offset: 0x0001837C
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001A18C File Offset: 0x0001838C
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0001A1C8 File Offset: 0x000183C8
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001A204 File Offset: 0x00018404
	public void ApplyTransform(Matrix4x4 widgetToPanel)
	{
		if (this.verts.size > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int size = this.verts.size;
			while (i < size)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
			this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001A2D0 File Offset: 0x000184D0
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < this.mRtpVerts.size; i++)
				{
					v.Add(this.mRtpVerts.buffer[i]);
					u.Add(this.uvs.buffer[i]);
					c.Add(this.cols.buffer[i]);
				}
			}
			else
			{
				for (int j = 0; j < this.mRtpVerts.size; j++)
				{
					v.Add(this.mRtpVerts.buffer[j]);
					u.Add(this.uvs.buffer[j]);
					c.Add(this.cols.buffer[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
				}
			}
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0001A400 File Offset: 0x00018600
	public int WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t, int point)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < this.mRtpVerts.size; i++)
				{
					if (v.size > point)
					{
						v[point] = this.mRtpVerts.buffer[i];
						u[point] = this.uvs.buffer[i];
						c[point] = this.cols.buffer[i];
					}
					else
					{
						v.Add(this.mRtpVerts.buffer[i]);
						u.Add(this.uvs.buffer[i]);
						c.Add(this.cols.buffer[i]);
					}
					point++;
				}
			}
			else
			{
				for (int j = 0; j < this.mRtpVerts.size; j++)
				{
					if (v.size > point)
					{
						v[point] = this.mRtpVerts.buffer[j];
						u[point] = this.uvs.buffer[j];
						c[point] = this.cols.buffer[j];
						n[point] = this.mRtpNormal;
						t[point] = this.mRtpTan;
					}
					else
					{
						v.Add(this.mRtpVerts.buffer[j]);
						u.Add(this.uvs.buffer[j]);
						c.Add(this.cols.buffer[j]);
						n.Add(this.mRtpNormal);
						t.Add(this.mRtpTan);
					}
					point++;
				}
			}
		}
		return point;
	}

	// Token: 0x04000385 RID: 901
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x04000386 RID: 902
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x04000387 RID: 903
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x04000388 RID: 904
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x04000389 RID: 905
	private Vector3 mRtpNormal;

	// Token: 0x0400038A RID: 906
	private Vector4 mRtpTan;
}
