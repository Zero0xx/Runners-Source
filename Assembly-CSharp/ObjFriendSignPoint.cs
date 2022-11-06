using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008EB RID: 2283
public class ObjFriendSignPoint : SpawnableObject
{
	// Token: 0x06003C65 RID: 15461 RVA: 0x0013DA80 File Offset: 0x0013BC80
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003C66 RID: 15462 RVA: 0x0013DA84 File Offset: 0x0013BC84
	private void Update()
	{
		if (!this.m_setupFind)
		{
			if (this.IsFindFriendSignPoint())
			{
				this.SetupFriendSign();
				this.m_setup = true;
			}
			else
			{
				this.m_setupDistance = this.GetTotalDistance();
				this.DebugDraw(string.Concat(new object[]
				{
					"IsFindFriendSignPoint NG 1 m_setupDistance=",
					this.m_setupDistance,
					" pos.x=",
					base.transform.position.x
				}));
			}
			this.m_setupFind = true;
		}
		if (!this.m_setup)
		{
			float num = this.GetTotalDistance() - this.m_setupDistance;
			if (num > 20f)
			{
				this.DebugDraw("IsFindFriendSignPoint OK pos.x=" + base.transform.position.x);
				this.SetupFriendSign();
				this.m_setup = true;
			}
		}
		if (this.m_setup && this.m_createCountMax > 0)
		{
			int num2 = 0;
			foreach (FriendSignCreateData friendSignCreateData in this.m_createDataList)
			{
				if (!friendSignCreateData.m_create)
				{
					this.CreateObject(friendSignCreateData.m_texture);
					friendSignCreateData.m_create = true;
					num2++;
					if (num2 >= this.m_createCountMax)
					{
						break;
					}
				}
			}
			if (num2 == 0)
			{
				this.m_createCountMax = 0;
			}
		}
	}

	// Token: 0x06003C67 RID: 15463 RVA: 0x0013DC1C File Offset: 0x0013BE1C
	private void SetupFriendSign()
	{
		this.m_createDataList = new List<FriendSignCreateData>();
		float totalDistance = this.GetTotalDistance();
		float x = this.GetPlayerPos().x;
		List<GameObject> list = new List<GameObject>();
		this.FindFriendSignPoint(ref list);
		List<FriendSignPointData> list2 = new List<FriendSignPointData>();
		foreach (GameObject gameObject in list)
		{
			float num = gameObject.transform.position.x - x + totalDistance - 10f;
			if (num < 0f)
			{
				num = 0f;
			}
			FriendSignPointData friendSignPointData = new FriendSignPointData(gameObject, num, 0f, base.transform.position.x == gameObject.transform.position.x);
			list2.Add(friendSignPointData);
			this.DebugDraw(string.Concat(new object[]
			{
				"ObjFriendSignPoint Data : my=",
				friendSignPointData.m_myPoint.ToString(),
				" distance=",
				friendSignPointData.m_distance,
				" pos.x=",
				gameObject.transform.position.x
			}));
		}
		list2.Sort((FriendSignPointData d1, FriendSignPointData d2) => d2.m_distance.CompareTo(d1.m_distance));
		float num2 = 0f;
		foreach (FriendSignPointData friendSignPointData2 in list2)
		{
			if (num2 == 0f)
			{
				num2 = friendSignPointData2.m_distance + 50f + 10f;
			}
			if (friendSignPointData2.m_myPoint)
			{
				this.m_myData = new FriendSignPointData(friendSignPointData2.m_obj, friendSignPointData2.m_distance, num2, friendSignPointData2.m_myPoint);
				this.DebugDraw(string.Concat(new object[]
				{
					"ObjFriendSignPoint myPoint :  distance=",
					this.m_myData.m_distance,
					" next=",
					this.m_myData.m_nextDistance,
					" pos.x=",
					this.m_myData.m_obj.transform.position.x
				}));
				break;
			}
			num2 = friendSignPointData2.m_distance + 10f;
		}
		FriendSignManager instance = FriendSignManager.Instance;
		if (instance)
		{
			List<FriendSignData> friendSignDataList = instance.GetFriendSignDataList();
			foreach (FriendSignData friendSignData in friendSignDataList)
			{
				if (!friendSignData.m_appear && this.AddFriendSignData(friendSignData.m_distance, friendSignData.m_texture))
				{
					instance.SetAppear(friendSignData.m_index);
				}
			}
		}
		if (this.m_createDataList.Count > 0)
		{
			if (this.m_createDataList.Count >= ObjFriendSignPoint.CREATE_COUNT)
			{
				this.m_createCountMax = this.m_createDataList.Count / ObjFriendSignPoint.CREATE_COUNT + 1;
			}
			else
			{
				this.m_createCountMax = 1;
			}
		}
	}

	// Token: 0x06003C68 RID: 15464 RVA: 0x0013DFC0 File Offset: 0x0013C1C0
	private bool AddFriendSignData(float friendDistance, Texture2D texture)
	{
		if (this.m_myData.m_distance <= friendDistance && friendDistance < this.m_myData.m_nextDistance)
		{
			this.m_createDataList.Add(new FriendSignCreateData(texture));
			return true;
		}
		return false;
	}

	// Token: 0x06003C69 RID: 15465 RVA: 0x0013E004 File Offset: 0x0013C204
	private void CreateObject(Texture2D texture)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjFriendSign");
		if (gameObject)
		{
			float num = 2.3f * (float)this.m_createCount;
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + num, base.transform.position.z);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, base.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.SetActive(true);
				gameObject2.transform.parent = base.transform;
				SpawnableObject component = gameObject2.GetComponent<SpawnableObject>();
				if (component)
				{
					component.AttachModelObject();
				}
				BoxCollider component2 = gameObject2.GetComponent<BoxCollider>();
				if (component2)
				{
					component2.center = new Vector3(component2.center.x, component2.center.y - num, component2.center.z);
				}
				ObjFriendSign component3 = gameObject2.GetComponent<ObjFriendSign>();
				if (component3)
				{
					component3.ChangeTexture(texture);
				}
				this.m_createCount++;
			}
		}
	}

	// Token: 0x06003C6A RID: 15466 RVA: 0x0013E154 File Offset: 0x0013C354
	private void FindFriendSignPoint(ref List<GameObject> objList)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("FriendSign");
		foreach (GameObject gameObject in array)
		{
			if (base.transform.position.x <= gameObject.transform.position.x)
			{
				objList.Add(gameObject);
			}
		}
	}

	// Token: 0x06003C6B RID: 15467 RVA: 0x0013E1BC File Offset: 0x0013C3BC
	private bool IsFindFriendSignPoint()
	{
		List<GameObject> list = new List<GameObject>();
		this.FindFriendSignPoint(ref list);
		return list.Count > 1;
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x0013E1E8 File Offset: 0x0013C3E8
	private float GetTotalDistance()
	{
		PlayerInformation playerInfo = this.GetPlayerInfo();
		if (playerInfo != null)
		{
			return playerInfo.TotalDistance;
		}
		return 0f;
	}

	// Token: 0x06003C6D RID: 15469 RVA: 0x0013E214 File Offset: 0x0013C414
	private Vector3 GetPlayerPos()
	{
		PlayerInformation playerInfo = this.GetPlayerInfo();
		if (playerInfo != null)
		{
			return playerInfo.Position;
		}
		return Vector3.zero;
	}

	// Token: 0x06003C6E RID: 15470 RVA: 0x0013E240 File Offset: 0x0013C440
	private PlayerInformation GetPlayerInfo()
	{
		if (this.m_playerInfo == null)
		{
			this.m_playerInfo = ObjUtil.GetPlayerInformation();
		}
		return this.m_playerInfo;
	}

	// Token: 0x06003C6F RID: 15471 RVA: 0x0013E270 File Offset: 0x0013C470
	private void DebugDraw(string msg)
	{
	}

	// Token: 0x06003C70 RID: 15472 RVA: 0x0013E274 File Offset: 0x0013C474
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(base.transform.position, 0.5f);
	}

	// Token: 0x04003499 RID: 13465
	private const float m_offsetPosY = 2.3f;

	// Token: 0x0400349A RID: 13466
	private const float m_offsetArea = 10f;

	// Token: 0x0400349B RID: 13467
	private const float m_checkArea = 20f;

	// Token: 0x0400349C RID: 13468
	private static int CREATE_COUNT = 5;

	// Token: 0x0400349D RID: 13469
	private FriendSignPointData m_myData;

	// Token: 0x0400349E RID: 13470
	private List<FriendSignCreateData> m_createDataList;

	// Token: 0x0400349F RID: 13471
	private int m_createCount;

	// Token: 0x040034A0 RID: 13472
	private int m_createCountMax;

	// Token: 0x040034A1 RID: 13473
	private bool m_setupFind;

	// Token: 0x040034A2 RID: 13474
	private bool m_setup;

	// Token: 0x040034A3 RID: 13475
	private PlayerInformation m_playerInfo;

	// Token: 0x040034A4 RID: 13476
	private float m_setupDistance;

	// Token: 0x040034A5 RID: 13477
	private bool m_debugDraw;
}
