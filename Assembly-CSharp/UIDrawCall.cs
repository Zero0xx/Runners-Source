using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[AddComponentMenu("NGUI/Internal/Draw Call")]
[ExecuteInEditMode]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060004FB RID: 1275 RVA: 0x00019460 File Offset: 0x00017660
	// (set) Token: 0x060004FC RID: 1276 RVA: 0x00019468 File Offset: 0x00017668
	public UIPanel panel { get; set; }

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060004FD RID: 1277 RVA: 0x00019474 File Offset: 0x00017674
	// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001947C File Offset: 0x0001767C
	public bool isDirty
	{
		get
		{
			return this.mDirty;
		}
		set
		{
			this.mDirty = value;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060004FF RID: 1279 RVA: 0x00019488 File Offset: 0x00017688
	// (set) Token: 0x06000500 RID: 1280 RVA: 0x00019490 File Offset: 0x00017690
	public int renderQueue
	{
		get
		{
			return this.mRenderQueue;
		}
		set
		{
			if (this.mRenderQueue != value)
			{
				this.mRenderQueue = value;
				if (this.mMat != null && this.mSharedMat != null)
				{
					this.mMat.renderQueue = this.mSharedMat.renderQueue + value;
				}
			}
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000501 RID: 1281 RVA: 0x000194EC File Offset: 0x000176EC
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x00019514 File Offset: 0x00017714
	// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001951C File Offset: 0x0001771C
	public Material material
	{
		get
		{
			return this.mSharedMat;
		}
		set
		{
			this.mSharedMat = value;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000504 RID: 1284 RVA: 0x00019528 File Offset: 0x00017728
	// (set) Token: 0x06000505 RID: 1285 RVA: 0x00019558 File Offset: 0x00017758
	public Texture mainTexture
	{
		get
		{
			return (!(this.mMat != null)) ? null : this.mMat.mainTexture;
		}
		set
		{
			if (this.mMat != null)
			{
				this.mMat.mainTexture = value;
			}
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000506 RID: 1286 RVA: 0x00019578 File Offset: 0x00017778
	public int triangles
	{
		get
		{
			Mesh mesh = (!this.mEven) ? this.mMesh1 : this.mMesh0;
			return (!(mesh != null)) ? 0 : (mesh.vertexCount >> 1);
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000507 RID: 1287 RVA: 0x000195BC File Offset: 0x000177BC
	public bool isClipped
	{
		get
		{
			return this.mClipping != UIDrawCall.Clipping.None;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000508 RID: 1288 RVA: 0x000195CC File Offset: 0x000177CC
	// (set) Token: 0x06000509 RID: 1289 RVA: 0x000195D4 File Offset: 0x000177D4
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mClipping = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600050A RID: 1290 RVA: 0x000195F0 File Offset: 0x000177F0
	// (set) Token: 0x0600050B RID: 1291 RVA: 0x000195F8 File Offset: 0x000177F8
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			this.mClipRange = value;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600050C RID: 1292 RVA: 0x00019604 File Offset: 0x00017804
	// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001960C File Offset: 0x0001780C
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoft;
		}
		set
		{
			this.mClipSoft = value;
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00019618 File Offset: 0x00017818
	private Mesh GetMesh(ref bool rebuildIndices, int vertexCount)
	{
		this.mEven = !this.mEven;
		if (this.mEven)
		{
			if (this.mMesh0 == null)
			{
				this.mMesh0 = new Mesh();
				this.mMesh0.hideFlags = HideFlags.DontSave;
				this.mMesh0.name = "Mesh0 for " + this.mSharedMat.name;
				this.mMesh0.MarkDynamic();
				rebuildIndices = true;
			}
			else if (rebuildIndices || this.mMesh0.vertexCount != vertexCount)
			{
				rebuildIndices = true;
				this.mMesh0.Clear();
			}
			return this.mMesh0;
		}
		if (this.mMesh1 == null)
		{
			this.mMesh1 = new Mesh();
			this.mMesh1.hideFlags = HideFlags.DontSave;
			this.mMesh1.name = "Mesh1 for " + this.mSharedMat.name;
			this.mMesh1.MarkDynamic();
			rebuildIndices = true;
		}
		else if (rebuildIndices || this.mMesh1.vertexCount != vertexCount)
		{
			rebuildIndices = true;
			this.mMesh1.Clear();
		}
		return this.mMesh1;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00019750 File Offset: 0x00017950
	public void RebuildMaterial()
	{
		NGUITools.DestroyImmediate(this.mMat);
		this.mMat = new Material(this.mSharedMat);
		this.mMat.hideFlags = HideFlags.DontSave;
		this.mMat.CopyPropertiesFromMaterial(this.mSharedMat);
		this.mMat.renderQueue = this.mSharedMat.renderQueue + this.mRenderQueue;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x000197B4 File Offset: 0x000179B4
	private void UpdateMaterials()
	{
		bool flag = this.mClipping != UIDrawCall.Clipping.None;
		if (this.mMat == null)
		{
			this.RebuildMaterial();
		}
		if (flag && this.mClipping != UIDrawCall.Clipping.None)
		{
			string text = this.mSharedMat.shader.name;
			text = text.Replace(" (AlphaClip)", string.Empty);
			text = text.Replace(" (SoftClip)", string.Empty);
			Shader shader;
			if (this.mClipping == UIDrawCall.Clipping.SoftClip)
			{
				shader = Shader.Find(text + " (SoftClip)");
			}
			else
			{
				shader = Shader.Find(text + " (AlphaClip)");
			}
			if (shader != null)
			{
				this.mMat.shader = shader;
			}
			else
			{
				this.mClipping = UIDrawCall.Clipping.None;
				global::Debug.LogError(text + " doesn't have a clipped shader version for " + this.mClipping);
			}
		}
		if (this.mRen.sharedMaterial != this.mMat)
		{
			this.mRen.sharedMaterials = new Material[]
			{
				this.mMat
			};
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x000198DC File Offset: 0x00017ADC
	public void Set(BetterList<Vector3> verts, BetterList<Vector3> norms, BetterList<Vector4> tans, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		int size = verts.size;
		if (size > 0 && size == uvs.size && size == cols.size && size % 4 == 0)
		{
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.AddComponent<MeshRenderer>();
				this.UpdateMaterials();
			}
			else if (this.mMat != null && this.mMat.mainTexture != this.mSharedMat.mainTexture)
			{
				this.UpdateMaterials();
			}
			if (verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (flag)
				{
					this.mIndices = new int[num];
					int num2 = 0;
					for (int i = 0; i < size; i += 4)
					{
						this.mIndices[num2++] = i;
						this.mIndices[num2++] = i + 1;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 3;
						this.mIndices[num2++] = i;
					}
				}
				Mesh mesh = this.GetMesh(ref flag, verts.size);
				mesh.vertices = verts.ToArray();
				if (norms != null)
				{
					mesh.normals = norms.ToArray();
				}
				if (tans != null)
				{
					mesh.tangents = tans.ToArray();
				}
				mesh.uv = uvs.ToArray();
				mesh.colors32 = cols.ToArray();
				if (flag)
				{
					mesh.triangles = this.mIndices;
				}
				mesh.RecalculateBounds();
				this.mFilter.mesh = mesh;
			}
			else
			{
				if (this.mFilter.mesh != null)
				{
					this.mFilter.mesh.Clear();
				}
				global::Debug.LogError("Too many vertices on one panel: " + verts.size);
			}
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			global::Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00019BA8 File Offset: 0x00017DA8
	public void Set(BetterList<Vector3> verts, BetterList<Vector3> norms, BetterList<Vector4> tans, BetterList<Vector2> uvs, BetterList<Color32> cols, int size)
	{
		if (size > 0 && size % 4 == 0)
		{
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.AddComponent<MeshRenderer>();
				this.UpdateMaterials();
			}
			else if (this.mMat != null && this.mMat.mainTexture != this.mSharedMat.mainTexture)
			{
				this.UpdateMaterials();
			}
			if (size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (flag)
				{
					this.mIndices = new int[num];
					int num2 = 0;
					for (int i = 0; i < size; i += 4)
					{
						this.mIndices[num2++] = i;
						this.mIndices[num2++] = i + 1;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 3;
						this.mIndices[num2++] = i;
					}
				}
				Mesh mesh = this.GetMesh(ref flag, size);
				mesh.vertices = verts.ToArray();
				if (norms != null)
				{
					mesh.normals = norms.ToArray();
				}
				if (tans != null)
				{
					mesh.tangents = tans.ToArray();
				}
				mesh.uv = uvs.ToArray();
				mesh.colors32 = cols.ToArray();
				if (flag)
				{
					mesh.triangles = this.mIndices;
				}
				mesh.RecalculateBounds();
				this.mFilter.mesh = mesh;
			}
			else
			{
				if (this.mFilter.mesh != null)
				{
					this.mFilter.mesh.Clear();
				}
				global::Debug.LogError("Too many vertices on one panel: " + size);
			}
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			global::Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00019E48 File Offset: 0x00018048
	private void OnWillRenderObject()
	{
		if (this.mReset)
		{
			this.mReset = false;
			this.UpdateMaterials();
		}
		if (this.mMat != null && this.isClipped)
		{
			this.mMat.mainTextureOffset = new Vector2(-this.mClipRange.x / this.mClipRange.z, -this.mClipRange.y / this.mClipRange.w);
			this.mMat.mainTextureScale = new Vector2(1f / this.mClipRange.z, 1f / this.mClipRange.w);
			Vector2 v = new Vector2(1000f, 1000f);
			if (this.mClipSoft.x > 0f)
			{
				v.x = this.mClipRange.z / this.mClipSoft.x;
			}
			if (this.mClipSoft.y > 0f)
			{
				v.y = this.mClipRange.w / this.mClipSoft.y;
			}
			this.mMat.SetVector("_ClipSharpness", v);
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00019F88 File Offset: 0x00018188
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh0);
		NGUITools.DestroyImmediate(this.mMesh1);
		NGUITools.DestroyImmediate(this.mMat);
	}

	// Token: 0x04000364 RID: 868
	public static BetterList<UIDrawCall> list = new BetterList<UIDrawCall>();

	// Token: 0x04000365 RID: 869
	private Transform mTrans;

	// Token: 0x04000366 RID: 870
	private Material mSharedMat;

	// Token: 0x04000367 RID: 871
	private Mesh mMesh0;

	// Token: 0x04000368 RID: 872
	private Mesh mMesh1;

	// Token: 0x04000369 RID: 873
	private MeshFilter mFilter;

	// Token: 0x0400036A RID: 874
	private MeshRenderer mRen;

	// Token: 0x0400036B RID: 875
	private UIDrawCall.Clipping mClipping;

	// Token: 0x0400036C RID: 876
	private Vector4 mClipRange;

	// Token: 0x0400036D RID: 877
	private Vector2 mClipSoft;

	// Token: 0x0400036E RID: 878
	private Material mMat;

	// Token: 0x0400036F RID: 879
	private int[] mIndices;

	// Token: 0x04000370 RID: 880
	private bool mDirty;

	// Token: 0x04000371 RID: 881
	private bool mReset = true;

	// Token: 0x04000372 RID: 882
	private bool mEven = true;

	// Token: 0x04000373 RID: 883
	private int mRenderQueue;

	// Token: 0x020000B2 RID: 178
	public enum Clipping
	{
		// Token: 0x04000376 RID: 886
		None,
		// Token: 0x04000377 RID: 887
		AlphaClip = 2,
		// Token: 0x04000378 RID: 888
		SoftClip
	}
}
