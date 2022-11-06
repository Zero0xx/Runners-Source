using System;
using App.Utility;
using Message;
using UnityEngine;

// Token: 0x02000269 RID: 617
[AddComponentMenu("Scripts/Runners/Game/Level")]
public class PlayerInformation : MonoBehaviour
{
	// Token: 0x060010AF RID: 4271 RVA: 0x00060974 File Offset: 0x0005EB74
	private void Start()
	{
		this.m_flags.Set(0, true);
		this.m_GravityDir = new Vector3(0f, -1f, 0f);
		this.m_upDirection = -this.m_GravityDir;
		if (SaveDataManager.Instance != null)
		{
			CharaType mainChara = SaveDataManager.Instance.PlayerData.MainChara;
			this.m_attribute = CharaTypeUtil.GetCharacterAttribute(mainChara);
			this.m_teamAttr = CharaTypeUtil.GetTeamAttribute(mainChara);
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x060010B0 RID: 4272 RVA: 0x000609F4 File Offset: 0x0005EBF4
	// (set) Token: 0x060010B1 RID: 4273 RVA: 0x000609FC File Offset: 0x0005EBFC
	public float TotalDistance
	{
		get
		{
			return this.m_totalDistance;
		}
		set
		{
			this.m_totalDistance = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00060A08 File Offset: 0x0005EC08
	public int NumRings
	{
		get
		{
			return this.m_numRings;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00060A10 File Offset: 0x0005EC10
	public int NumLostRings
	{
		get
		{
			return this.m_lostRings;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00060A18 File Offset: 0x0005EC18
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00060A28 File Offset: 0x0005EC28
	public Quaternion Rotation
	{
		get
		{
			return base.transform.rotation;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00060A38 File Offset: 0x0005EC38
	public float FrontSpeed
	{
		get
		{
			return this.m_frontspeed;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00060A40 File Offset: 0x0005EC40
	public Vector3 Velocity
	{
		get
		{
			return this.m_velocity;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00060A48 File Offset: 0x0005EC48
	public Vector3 HorizonVelocity
	{
		get
		{
			return this.m_horzVelocity;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00060A50 File Offset: 0x0005EC50
	public Vector3 VerticalVelocity
	{
		get
		{
			return this.m_vertVelocity;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x060010BA RID: 4282 RVA: 0x00060A58 File Offset: 0x0005EC58
	public float DefaultSpeed
	{
		get
		{
			return this.m_defaultSpeed;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x060010BB RID: 4283 RVA: 0x00060A60 File Offset: 0x0005EC60
	public float DistanceFromGround
	{
		get
		{
			return this.m_distanceFromGround;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x060010BC RID: 4284 RVA: 0x00060A68 File Offset: 0x0005EC68
	public Vector3 GravityDir
	{
		get
		{
			return this.m_GravityDir;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x060010BD RID: 4285 RVA: 0x00060A70 File Offset: 0x0005EC70
	public Vector3 UpDirection
	{
		get
		{
			return this.m_upDirection;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x060010BE RID: 4286 RVA: 0x00060A78 File Offset: 0x0005EC78
	public PlayerSpeed SpeedLevel
	{
		get
		{
			return this.m_speedLevel;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x060010BF RID: 4287 RVA: 0x00060A80 File Offset: 0x0005EC80
	public Vector3 SideViewPathPos
	{
		get
		{
			return this.m_pathSideViewPos;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00060A88 File Offset: 0x0005EC88
	public Vector3 SideViewPathNormal
	{
		get
		{
			return this.m_pathSideViewNormal;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00060A90 File Offset: 0x0005EC90
	public string MainCharacterName
	{
		get
		{
			return this.m_mainCharacterName;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00060A98 File Offset: 0x0005EC98
	public string SubCharacterName
	{
		get
		{
			return this.m_subCharacterName;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00060AA0 File Offset: 0x0005ECA0
	public int MainCharacterID
	{
		get
		{
			return this.m_mainCharacterID;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00060AA8 File Offset: 0x0005ECA8
	public int SubCharacterID
	{
		get
		{
			return this.m_subCharacterID;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00060AB0 File Offset: 0x0005ECB0
	public CharacterAttribute PlayerAttribute
	{
		get
		{
			return this.m_attribute;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00060AB8 File Offset: 0x0005ECB8
	public TeamAttribute PlayerTeamAttribute
	{
		get
		{
			return this.m_teamAttr;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00060AC0 File Offset: 0x0005ECC0
	public PhantomType PhantomType
	{
		get
		{
			return this.m_phantomType;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00060AC8 File Offset: 0x0005ECC8
	public CharacterAttribute MainCharacterAttribute
	{
		get
		{
			return this.m_mainCharaAttribute;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00060AD0 File Offset: 0x0005ECD0
	public CharacterAttribute SubCharacterAttribute
	{
		get
		{
			return this.m_subCharaAttribute;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x060010CA RID: 4298 RVA: 0x00060AD8 File Offset: 0x0005ECD8
	public TeamAttribute MainTeamAttribute
	{
		get
		{
			return this.m_mainTeamAttribute;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x060010CB RID: 4299 RVA: 0x00060AE0 File Offset: 0x0005ECE0
	public TeamAttribute SubTeamAttribute
	{
		get
		{
			return this.m_subTeamAttribute;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x060010CC RID: 4300 RVA: 0x00060AE8 File Offset: 0x0005ECE8
	public PlayingCharacterType PlayingCharaType
	{
		get
		{
			return this.m_playingCharacterType;
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00060AF0 File Offset: 0x0005ECF0
	public bool IsDead()
	{
		return this.m_flags.Test(2);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x00060B00 File Offset: 0x0005ED00
	public bool IsDamaged()
	{
		return this.m_flags.Test(1);
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x00060B10 File Offset: 0x0005ED10
	public bool IsOnGround()
	{
		return this.m_flags.Test(0);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x00060B20 File Offset: 0x0005ED20
	public bool IsNotCharaChange()
	{
		return this.m_flags.Test(3);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x00060B30 File Offset: 0x0005ED30
	public bool IsNowParaloop()
	{
		return this.m_flags.Test(4);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x00060B40 File Offset: 0x0005ED40
	public bool IsNowLastChance()
	{
		return this.m_flags.Test(5);
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x00060B50 File Offset: 0x0005ED50
	public bool IsNowCombo()
	{
		return this.m_flags.Test(6);
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x00060B60 File Offset: 0x0005ED60
	public bool IsMovementUpdated()
	{
		return this.m_flags.Test(7);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00060B70 File Offset: 0x0005ED70
	private void OnUpSpeedLevel(MsgUpSpeedLevel msg)
	{
		this.SetSpeedLevel(msg.m_level);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x00060B80 File Offset: 0x0005ED80
	public void SetTransform(Transform input)
	{
		base.transform.position = input.position;
		base.transform.rotation = input.rotation;
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00060BB0 File Offset: 0x0005EDB0
	public void SetVelocity(Vector3 velocity)
	{
		this.m_velocity = velocity;
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x00060BBC File Offset: 0x0005EDBC
	public void SetHorzAndVertVelocity(Vector3 horzVelocity, Vector3 vertVelocity)
	{
		this.m_horzVelocity = horzVelocity;
		this.m_vertVelocity = vertVelocity;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00060BCC File Offset: 0x0005EDCC
	public void SetDefautlSpeed(float speed)
	{
		this.m_defaultSpeed = speed;
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00060BD8 File Offset: 0x0005EDD8
	public void SetFrontSpeed(float speed)
	{
		this.m_frontspeed = speed;
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00060BE4 File Offset: 0x0005EDE4
	public void SetNumRings(int numRing)
	{
		this.m_numRings = Mathf.Clamp(numRing, 0, 99999);
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00060BF8 File Offset: 0x0005EDF8
	public void AddNumRings(int addRings)
	{
		this.SetNumRings(this.NumRings + addRings);
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x00060C08 File Offset: 0x0005EE08
	public void LostRings()
	{
		this.m_lostRings += this.NumRings;
		this.SetNumRings(0);
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x00060C24 File Offset: 0x0005EE24
	public void SetDistanceToGround(float distance)
	{
		this.m_distanceFromGround = distance;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00060C30 File Offset: 0x0005EE30
	public void SetGravityDirection(Vector3 dir)
	{
		this.m_GravityDir = dir;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x00060C3C File Offset: 0x0005EE3C
	public void SetUpDirection(Vector3 dir)
	{
		this.m_upDirection = dir;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x00060C48 File Offset: 0x0005EE48
	public void AddTotalDistance(float nowDistance)
	{
		this.m_totalDistance += nowDistance;
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00060C58 File Offset: 0x0005EE58
	public void SetSideViewPath(Vector3 pos, Vector3 normal)
	{
		this.m_pathSideViewPos = pos;
		this.m_pathSideViewNormal = normal;
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x00060C68 File Offset: 0x0005EE68
	private void SetSpeedLevel(PlayerSpeed level)
	{
		this.m_speedLevel = level;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x00060C74 File Offset: 0x0005EE74
	public void SetPhantomType(PhantomType type)
	{
		this.m_phantomType = type;
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00060C80 File Offset: 0x0005EE80
	public void SetPlayerAttribute(CharacterAttribute attr, TeamAttribute teamAttr, PlayingCharacterType playingType)
	{
		this.m_attribute = attr;
		this.m_teamAttr = teamAttr;
		this.m_playingCharacterType = playingType;
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00060C98 File Offset: 0x0005EE98
	public void SetDebugPlayerAttribute(CharaType charaType)
	{
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x00060CA8 File Offset: 0x0005EEA8
	public void SetDead(bool value)
	{
		this.m_flags.Set(2, value);
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x00060CB8 File Offset: 0x0005EEB8
	public void SetDamaged(bool value)
	{
		this.m_flags.Set(1, value);
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x00060CC8 File Offset: 0x0005EEC8
	public void SetOnGround(bool value)
	{
		this.m_flags.Set(0, value);
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x00060CD8 File Offset: 0x0005EED8
	public void SetEnableCharaChange(bool value)
	{
		this.m_flags.Set(3, value);
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00060CE8 File Offset: 0x0005EEE8
	public void SetParaloop(bool value)
	{
		this.m_flags.Set(4, value);
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x00060CF8 File Offset: 0x0005EEF8
	public void SetLastChance(bool value)
	{
		this.m_flags.Set(5, value);
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x00060D08 File Offset: 0x0005EF08
	public void SetCombo(bool value)
	{
		this.m_flags.Set(6, value);
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x00060D18 File Offset: 0x0005EF18
	public void SetMovementUpdated(bool value)
	{
		this.m_flags.Set(7, value);
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x00060D28 File Offset: 0x0005EF28
	public void SetPlayerCharacter(int main, int sub)
	{
		if (CharacterDataNameInfo.Instance)
		{
			CharacterDataNameInfo.Info dataByID = CharacterDataNameInfo.Instance.GetDataByID((CharaType)main);
			CharacterDataNameInfo.Info dataByID2 = CharacterDataNameInfo.Instance.GetDataByID((CharaType)sub);
			if (dataByID != null)
			{
				this.m_mainCharacterName = dataByID.m_name;
				this.m_mainCharacterID = (int)dataByID.m_ID;
				this.m_mainCharaAttribute = dataByID.m_attribute;
				this.m_mainTeamAttribute = dataByID.m_teamAttribute;
			}
			if (dataByID2 != null)
			{
				this.m_subCharacterName = dataByID2.m_name;
				this.m_subCharacterID = (int)dataByID2.m_ID;
				this.m_subCharaAttribute = dataByID2.m_attribute;
				this.m_subTeamAttribute = dataByID2.m_teamAttribute;
			}
		}
	}

	// Token: 0x04000F2A RID: 3882
	private const int MAX_NUM_RINGS = 99999;

	// Token: 0x04000F2B RID: 3883
	private string m_mainCharacterName = "Sonic";

	// Token: 0x04000F2C RID: 3884
	private string m_subCharacterName;

	// Token: 0x04000F2D RID: 3885
	private int m_mainCharacterID;

	// Token: 0x04000F2E RID: 3886
	private int m_subCharacterID = -1;

	// Token: 0x04000F2F RID: 3887
	private CharacterAttribute m_mainCharaAttribute;

	// Token: 0x04000F30 RID: 3888
	private CharacterAttribute m_subCharaAttribute;

	// Token: 0x04000F31 RID: 3889
	private TeamAttribute m_mainTeamAttribute;

	// Token: 0x04000F32 RID: 3890
	private TeamAttribute m_subTeamAttribute;

	// Token: 0x04000F33 RID: 3891
	private PlayingCharacterType m_playingCharacterType;

	// Token: 0x04000F34 RID: 3892
	private CharacterAttribute m_attribute;

	// Token: 0x04000F35 RID: 3893
	private TeamAttribute m_teamAttr;

	// Token: 0x04000F36 RID: 3894
	private float m_totalDistance;

	// Token: 0x04000F37 RID: 3895
	private int m_numRings;

	// Token: 0x04000F38 RID: 3896
	private int m_lostRings;

	// Token: 0x04000F39 RID: 3897
	private Bitset32 m_flags;

	// Token: 0x04000F3A RID: 3898
	private Vector3 m_velocity;

	// Token: 0x04000F3B RID: 3899
	private Vector3 m_horzVelocity;

	// Token: 0x04000F3C RID: 3900
	private Vector3 m_vertVelocity;

	// Token: 0x04000F3D RID: 3901
	private float m_defaultSpeed;

	// Token: 0x04000F3E RID: 3902
	private float m_frontspeed;

	// Token: 0x04000F3F RID: 3903
	private float m_distanceFromGround;

	// Token: 0x04000F40 RID: 3904
	private Vector3 m_GravityDir;

	// Token: 0x04000F41 RID: 3905
	private Vector3 m_upDirection;

	// Token: 0x04000F42 RID: 3906
	private Vector3 m_pathSideViewPos;

	// Token: 0x04000F43 RID: 3907
	private Vector3 m_pathSideViewNormal;

	// Token: 0x04000F44 RID: 3908
	private PhantomType m_phantomType = PhantomType.NONE;

	// Token: 0x04000F45 RID: 3909
	[SerializeField]
	private PlayerSpeed m_speedLevel;

	// Token: 0x04000F46 RID: 3910
	[SerializeField]
	private bool m_drawInfo;

	// Token: 0x04000F47 RID: 3911
	private Rect m_window;

	// Token: 0x0200026A RID: 618
	public enum Flags
	{
		// Token: 0x04000F49 RID: 3913
		OnGround,
		// Token: 0x04000F4A RID: 3914
		Damaged,
		// Token: 0x04000F4B RID: 3915
		Dead,
		// Token: 0x04000F4C RID: 3916
		EnableCharaChange,
		// Token: 0x04000F4D RID: 3917
		Paraloop,
		// Token: 0x04000F4E RID: 3918
		LastChance,
		// Token: 0x04000F4F RID: 3919
		Combo,
		// Token: 0x04000F50 RID: 3920
		MovementUpdated
	}
}
