using System;
using GameScore;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
public class ObjFriendSign : SpawnableObject
{
	// Token: 0x06003C5A RID: 15450 RVA: 0x0013D820 File Offset: 0x0013BA20
	protected override string GetModelName()
	{
		return "obj_cmn_friendsign";
	}

	// Token: 0x06003C5B RID: 15451 RVA: 0x0013D828 File Offset: 0x0013BA28
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x0013D82C File Offset: 0x0013BA2C
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x0013D830 File Offset: 0x0013BA30
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (this.m_mode)
		{
		case ObjFriendSign.Mode.Start:
			this.m_rot = 0f;
			this.m_speed = 100f;
			this.m_mode = ObjFriendSign.Mode.Rot;
			break;
		case ObjFriendSign.Mode.Rot:
		{
			float num = 60f * deltaTime * this.m_speed;
			float num2 = this.m_rot + num;
			if (num2 < 360f)
			{
				this.m_rot += num;
			}
			else
			{
				num = 360f - this.m_rot;
				this.m_speed -= this.m_speed * 0.5f;
				this.m_rot = 0f;
				if (this.m_speed < 3f)
				{
					this.m_time = 0f;
					this.m_mode = ObjFriendSign.Mode.End;
				}
			}
			base.transform.rotation = Quaternion.Euler(0f, num, 0f) * base.transform.rotation;
			break;
		}
		case ObjFriendSign.Mode.End:
			this.m_time += deltaTime;
			if (this.m_time > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				this.m_mode = ObjFriendSign.Mode.Idle;
			}
			break;
		}
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x0013D974 File Offset: 0x0013BB74
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				this.HitFriendSign();
			}
		}
	}

	// Token: 0x06003C5F RID: 15455 RVA: 0x0013D9A4 File Offset: 0x0013BBA4
	private void HitFriendSign()
	{
		if (this.m_mode == ObjFriendSign.Mode.Idle)
		{
			ObjUtil.SendMessageAddScore(Data.FriendSign);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(5, Data.FriendSign));
			ObjUtil.PlaySE("obj_item_friendsign", "SE");
			this.m_mode = ObjFriendSign.Mode.Start;
		}
	}

	// Token: 0x06003C60 RID: 15456 RVA: 0x0013D9F0 File Offset: 0x0013BBF0
	public void ChangeTexture(Texture tex)
	{
		if (tex)
		{
			MeshRenderer meshRenderer = GameObjectUtil.FindChildGameObjectComponent<MeshRenderer>(base.gameObject, "obj_cmn_friendsignpicture");
			if (meshRenderer)
			{
				meshRenderer.material.mainTexture = tex;
			}
		}
	}

	// Token: 0x04003487 RID: 13447
	private const string ModelName = "obj_cmn_friendsign";

	// Token: 0x04003488 RID: 13448
	private const float END_TIME = 5f;

	// Token: 0x04003489 RID: 13449
	private const float START_SPEED = 100f;

	// Token: 0x0400348A RID: 13450
	private ObjFriendSign.Mode m_mode;

	// Token: 0x0400348B RID: 13451
	private float m_time;

	// Token: 0x0400348C RID: 13452
	private float m_speed;

	// Token: 0x0400348D RID: 13453
	private float m_rot;

	// Token: 0x020008E8 RID: 2280
	private enum Mode
	{
		// Token: 0x0400348F RID: 13455
		Idle,
		// Token: 0x04003490 RID: 13456
		Start,
		// Token: 0x04003491 RID: 13457
		Rot,
		// Token: 0x04003492 RID: 13458
		End
	}
}
