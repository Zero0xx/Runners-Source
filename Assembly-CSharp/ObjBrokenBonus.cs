using System;
using UnityEngine;

// Token: 0x02000840 RID: 2112
public class ObjBrokenBonus : MonoBehaviour
{
	// Token: 0x06003912 RID: 14610 RVA: 0x0012EA84 File Offset: 0x0012CC84
	private void Start()
	{
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
	}

	// Token: 0x06003913 RID: 14611 RVA: 0x0012EA94 File Offset: 0x0012CC94
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		float d = 3f * deltaTime;
		base.transform.position += Vector3.up * d;
		if (this.m_type == BrokenBonusType.SUPER_RING)
		{
			ObjUtil.SetTextureAnimationSpeed(this.m_obj, 20f);
		}
		else
		{
			float y = 60f * deltaTime * 20f;
			base.transform.rotation = Quaternion.Euler(0f, y, 0f) * base.transform.rotation;
		}
		this.m_time += deltaTime;
		if (this.m_time > 2f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003914 RID: 14612 RVA: 0x0012EB54 File Offset: 0x0012CD54
	public void Setup(BrokenBonusType type, GameObject playerObject)
	{
		this.m_type = type;
		uint type2 = (uint)this.m_type;
		if ((ulong)type2 < (ulong)((long)ObjBrokenBonus.OBJ_MODELNAMES.Length))
		{
			string name = string.Empty;
			string name2 = string.Empty;
			if (this.m_type == BrokenBonusType.CRYSTAL10)
			{
				CtystalParam crystalParam = ObjCrystalData.GetCrystalParam(this.m_ctystalType);
				if (crystalParam != null)
				{
					name = crystalParam.m_model;
					name2 = crystalParam.m_se;
				}
				ObjUtil.LightPlaySE(name2, "SE");
			}
			else
			{
				name = ObjBrokenBonus.OBJ_MODELNAMES[(int)((UIntPtr)type2)];
				if ((ulong)type2 < (ulong)((long)ObjBrokenBonus.OBJ_SENAMES.Length))
				{
					name2 = ObjBrokenBonus.OBJ_SENAMES[(int)((UIntPtr)type2)];
				}
				ObjUtil.PlaySE(name2, "SE");
			}
			this.m_obj = this.CreateBonus(name);
			switch (type)
			{
			case BrokenBonusType.SUPER_RING:
				ObjSuperRing.AddSuperRing(playerObject);
				break;
			case BrokenBonusType.REDSTAR_RING:
				ObjUtil.SendMessageAddRedRing();
				ObjUtil.SendMessageScoreCheck(new StageScoreData(8, 1));
				break;
			case BrokenBonusType.CRYSTAL10:
			{
				CtystalParam crystalParam2 = ObjCrystalData.GetCrystalParam(this.m_ctystalType);
				if (crystalParam2 != null)
				{
					ObjCrystalBase.SetChaoAbliltyScoreEffect(this.m_playerInfo, crystalParam2, this.m_obj);
				}
				break;
			}
			}
		}
	}

	// Token: 0x06003915 RID: 14613 RVA: 0x0012EC70 File Offset: 0x0012CE70
	private GameObject CreateBonus(string name)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_RESOURCE, name);
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
				return gameObject2;
			}
		}
		return null;
	}

	// Token: 0x04002FF0 RID: 12272
	private const float END_TIME = 2f;

	// Token: 0x04002FF1 RID: 12273
	private const float MOVE_SPEED = 3f;

	// Token: 0x04002FF2 RID: 12274
	private const float ROT_SPEED = 20f;

	// Token: 0x04002FF3 RID: 12275
	private static readonly string[] OBJ_MODELNAMES = new string[]
	{
		ObjSuperRing.ModelName,
		ObjRedStarRing.ModelName,
		string.Empty
	};

	// Token: 0x04002FF4 RID: 12276
	private static readonly string[] OBJ_SENAMES = new string[]
	{
		ObjSuperRing.SEName,
		ObjRedStarRing.SEName,
		string.Empty
	};

	// Token: 0x04002FF5 RID: 12277
	private float m_time;

	// Token: 0x04002FF6 RID: 12278
	private BrokenBonusType m_type;

	// Token: 0x04002FF7 RID: 12279
	private PlayerInformation m_playerInfo;

	// Token: 0x04002FF8 RID: 12280
	private GameObject m_obj;

	// Token: 0x04002FF9 RID: 12281
	private CtystalType m_ctystalType = CtystalType.BIG_C;
}
