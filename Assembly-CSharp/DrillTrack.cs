using System;
using UnityEngine;

// Token: 0x020009C5 RID: 2501
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class DrillTrack : MonoBehaviour
{
	// Token: 0x060041A9 RID: 16809 RVA: 0x00155AD0 File Offset: 0x00153CD0
	private void Start()
	{
		this.m_HistoryBuffer = new CircularBuffer<DrillTrack.History>(100);
		this.m_MeshData = new DrillTrack.StripMeshData();
		this.m_MeshData.m_Vertices = new Vector3[400];
		this.m_MeshData.m_UV = new Vector2[400];
		this.m_MeshData.m_Colors = new Color[400];
		this.m_MeshData.m_Triangles = new int[600];
	}

	// Token: 0x060041AA RID: 16810 RVA: 0x00155B4C File Offset: 0x00153D4C
	private void Update()
	{
		if (this.m_Target != null)
		{
			Vector3 vector = this.m_Target.transform.position;
			vector += this.m_Target.transform.forward * this.m_frnotOffset;
			this.AddHistory(vector);
		}
	}

	// Token: 0x060041AB RID: 16811 RVA: 0x00155BA4 File Offset: 0x00153DA4
	private void AddHistory(Vector3 position)
	{
		bool flag = false;
		if (this.m_HistoryBuffer.Size == 0)
		{
			flag = true;
		}
		else
		{
			DrillTrack.History at = this.m_HistoryBuffer.GetAt(this.m_HistoryBuffer.Tail);
			if (Vector3.Distance(position, at.m_Position) > 0.3f)
			{
				flag = true;
			}
		}
		if (flag)
		{
			DrillTrack.History history = new DrillTrack.History();
			history.m_Position = position;
			if (this.m_HistoryBuffer.Size > 0)
			{
				DrillTrack.History at2 = this.m_HistoryBuffer.GetAt(this.m_HistoryBuffer.Tail);
				float num = Vector3.Distance(position, at2.m_Position);
				history.m_UVOffset = at2.m_UVOffset + num / 0.8f;
			}
			if (this.m_disable)
			{
				history.m_Visible = false;
			}
			this.m_HistoryBuffer.Add(history);
			if (this.m_HistoryBuffer.Size > 1)
			{
				this.UpdateTrack();
			}
		}
	}

	// Token: 0x060041AC RID: 16812 RVA: 0x00155C8C File Offset: 0x00153E8C
	private void UpdateTrack()
	{
		if (this.m_Camera == null)
		{
			return;
		}
		for (int i = 1; i < this.m_HistoryBuffer.Size; i++)
		{
			int index = (this.m_HistoryBuffer.Capacity + this.m_HistoryBuffer.Head + i - 1) % this.m_HistoryBuffer.Capacity;
			int index2 = (this.m_HistoryBuffer.Capacity + this.m_HistoryBuffer.Head + i) % this.m_HistoryBuffer.Capacity;
			DrillTrack.History at = this.m_HistoryBuffer.GetAt(index);
			DrillTrack.History at2 = this.m_HistoryBuffer.GetAt(index2);
			Vector3 position = at.m_Position;
			Vector3 position2 = at2.m_Position;
			Vector3 a = this.m_Camera.TransformDirection(Vector3.forward);
			Vector3 normalized = (position2 - position).normalized;
			Vector3 a2 = Vector3.Cross(normalized, -a);
			this.m_MeshData.m_Vertices[(i - 1) * 4] = position - a2 * 0.4f;
			this.m_MeshData.m_Vertices[(i - 1) * 4 + 1] = position + a2 * 0.4f;
			this.m_MeshData.m_Vertices[(i - 1) * 4 + 2] = position2 - a2 * 0.4f;
			this.m_MeshData.m_Vertices[(i - 1) * 4 + 3] = position2 + a2 * 0.4f;
			if (i > 1)
			{
				Vector3 vector = (this.m_MeshData.m_Vertices[(i - 2) * 4 + 2] + this.m_MeshData.m_Vertices[(i - 1) * 4]) / 2f;
				Vector3 vector2 = (this.m_MeshData.m_Vertices[(i - 2) * 4 + 3] + this.m_MeshData.m_Vertices[(i - 1) * 4 + 1]) / 2f;
				this.m_MeshData.m_Vertices[(i - 2) * 4 + 2] = vector;
				this.m_MeshData.m_Vertices[(i - 1) * 4] = vector;
				this.m_MeshData.m_Vertices[(i - 2) * 4 + 3] = vector2;
				this.m_MeshData.m_Vertices[(i - 1) * 4 + 1] = vector2;
			}
			this.m_MeshData.m_UV[(i - 1) * 4] = Vector2.right * at.m_UVOffset;
			this.m_MeshData.m_UV[(i - 1) * 4 + 1] = Vector2.up + Vector2.right * at.m_UVOffset;
			this.m_MeshData.m_UV[(i - 1) * 4 + 2] = Vector2.right * at2.m_UVOffset;
			this.m_MeshData.m_UV[(i - 1) * 4 + 3] = Vector2.up + Vector2.right * at2.m_UVOffset;
			this.m_MeshData.m_Colors[(i - 1) * 4] = ((!at.m_Visible) ? Color.clear : Color.white);
			this.m_MeshData.m_Colors[(i - 1) * 4 + 1] = ((!at.m_Visible) ? Color.clear : Color.white);
			this.m_MeshData.m_Colors[(i - 1) * 4 + 2] = ((!at2.m_Visible) ? Color.clear : Color.white);
			this.m_MeshData.m_Colors[(i - 1) * 4 + 3] = ((!at2.m_Visible) ? Color.clear : Color.white);
			this.m_MeshData.m_Triangles[(i - 1) * 6] = 0 + (i - 1) * 4;
			this.m_MeshData.m_Triangles[(i - 1) * 6 + 1] = 1 + (i - 1) * 4;
			this.m_MeshData.m_Triangles[(i - 1) * 6 + 2] = 2 + (i - 1) * 4;
			this.m_MeshData.m_Triangles[(i - 1) * 6 + 3] = 2 + (i - 1) * 4;
			this.m_MeshData.m_Triangles[(i - 1) * 6 + 4] = 1 + (i - 1) * 4;
			this.m_MeshData.m_Triangles[(i - 1) * 6 + 5] = 3 + (i - 1) * 4;
		}
		Mesh mesh = base.GetComponent<MeshFilter>().mesh;
		mesh.vertices = this.m_MeshData.m_Vertices;
		mesh.uv = this.m_MeshData.m_UV;
		mesh.colors = this.m_MeshData.m_Colors;
		mesh.triangles = this.m_MeshData.m_Triangles;
		mesh.RecalculateBounds();
	}

	// Token: 0x170008E9 RID: 2281
	// (get) Token: 0x060041AD RID: 16813 RVA: 0x001561D0 File Offset: 0x001543D0
	// (set) Token: 0x060041AE RID: 16814 RVA: 0x001561D8 File Offset: 0x001543D8
	public bool Disable
	{
		get
		{
			return this.m_disable;
		}
		set
		{
			this.m_disable = value;
		}
	}

	// Token: 0x170008EA RID: 2282
	// (set) Token: 0x060041AF RID: 16815 RVA: 0x001561E4 File Offset: 0x001543E4
	public float FrontOffset
	{
		set
		{
			this.m_frnotOffset = value;
		}
	}

	// Token: 0x04003801 RID: 14337
	private const float TRACK_RADIUS = 0.4f;

	// Token: 0x04003802 RID: 14338
	private const float THRESHOLD = 0.3f;

	// Token: 0x04003803 RID: 14339
	private const int HISTORY_SIZE = 100;

	// Token: 0x04003804 RID: 14340
	public GameObject m_Target;

	// Token: 0x04003805 RID: 14341
	public Transform m_Camera;

	// Token: 0x04003806 RID: 14342
	private bool m_disable;

	// Token: 0x04003807 RID: 14343
	private float m_frnotOffset;

	// Token: 0x04003808 RID: 14344
	private CircularBuffer<DrillTrack.History> m_HistoryBuffer;

	// Token: 0x04003809 RID: 14345
	private DrillTrack.StripMeshData m_MeshData;

	// Token: 0x020009C6 RID: 2502
	private class History
	{
		// Token: 0x0400380A RID: 14346
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x0400380B RID: 14347
		public float m_UVOffset;

		// Token: 0x0400380C RID: 14348
		public bool m_Visible = true;
	}

	// Token: 0x020009C7 RID: 2503
	private class StripMeshData
	{
		// Token: 0x0400380D RID: 14349
		public Vector3[] m_Vertices;

		// Token: 0x0400380E RID: 14350
		public Vector2[] m_UV;

		// Token: 0x0400380F RID: 14351
		public Color[] m_Colors;

		// Token: 0x04003810 RID: 14352
		public int[] m_Triangles;
	}
}
