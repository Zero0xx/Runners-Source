using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class StageDebugInformation : MonoBehaviour
{
	// Token: 0x0600158C RID: 5516 RVA: 0x00077058 File Offset: 0x00075258
	private void Awake()
	{
		if (!this.SetInstance())
		{
			return;
		}
		this.m_playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
		this.m_blockManager = GameObjectUtil.FindGameObjectComponentWithTag<StageBlockManager>("StageManager", "StageBlockManager");
		this.m_window = new Rect(10f, 160f, 250f, 120f);
		this.m_window2 = new Rect(10f, 290f, 280f, 150f);
		this.m_window3 = new Rect(300f, 160f, 280f, 260f);
		for (int i = 0; i < 5; i++)
		{
			this.m_window2text[i] = "0";
		}
		this.m_showInformation = false;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x00077120 File Offset: 0x00075320
	private void Start()
	{
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00077124 File Offset: 0x00075324
	private void Update()
	{
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00077128 File Offset: 0x00075328
	private void OnDestroy()
	{
		if (StageDebugInformation.m_instance == this)
		{
			StageDebugInformation.m_instance = null;
		}
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x00077140 File Offset: 0x00075340
	private void OnGUI()
	{
		if (this.m_showInformation)
		{
			this.m_window = GUI.Window(5, this.m_window, new GUI.WindowFunction(this.WindowFunction), "StageInformation");
			this.m_window2 = GUI.Window(7, this.m_window2, new GUI.WindowFunction(this.WindowFunction2), "StageEdit");
			this.m_window3 = GUI.Window(6, this.m_window3, new GUI.WindowFunction(this.WindowFunction3), "FriendSignInformation");
			if (GUI.Button(new Rect(10f, 120f, 150f, 30f), "Close Info"))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else if (GUI.Button(new Rect(10f, 120f, 150f, 30f), "Show Info"))
		{
			this.m_showInformation = true;
		}
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x00077228 File Offset: 0x00075428
	private void WindowFunction(int windowID)
	{
		string text = string.Empty;
		Vector3 position = this.m_playerInformation.Position;
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"Position : ",
			position.x.ToString("F2"),
			" ",
			position.y.ToString("F2"),
			" ",
			position.z.ToString("F2"),
			"\n"
		});
		StageBlockManager.StageBlockInfo currenBlockInfo = this.m_blockManager.GetCurrenBlockInfo();
		if (currenBlockInfo != null)
		{
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"Block : ",
				currenBlockInfo.m_blockNo.ToString(),
				"  Layer : ",
				currenBlockInfo.m_layerNo.ToString(),
				"\n"
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"Bl Length : ",
				currenBlockInfo.m_totalLength.ToString("F2"),
				"  Distance:",
				this.m_blockManager.GetBlockLocalDistance().ToString("F2"),
				"\n"
			});
			Vector3 blockLocalPosition = this.m_blockManager.GetBlockLocalPosition(position);
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"Bl LocalPos : ",
				blockLocalPosition.x.ToString("F2"),
				" ",
				blockLocalPosition.y.ToString("F2"),
				" ",
				blockLocalPosition.z.ToString("F2"),
				"\n"
			});
		}
		Vector3 sideViewPathPos = this.m_playerInformation.SideViewPathPos;
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"Path     : ",
			sideViewPathPos.x.ToString("F2"),
			" ",
			sideViewPathPos.y.ToString("F2"),
			" ",
			sideViewPathPos.z.ToString("F2"),
			"\n"
		});
		GUIContent guicontent = new GUIContent();
		guicontent.text = text;
		Rect position2 = new Rect(5f, 20f, 240f, 120f);
		GUI.Label(position2, guicontent);
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000774A8 File Offset: 0x000756A8
	private void WindowFunction2(int windowID)
	{
		int num = 5;
		int num2 = 20;
		int num3 = 100;
		int num4 = 70;
		int num5 = 50;
		int num6 = 20;
		int num7 = 0;
		Rect position = new Rect((float)num, (float)num2, (float)num3, (float)num6);
		Rect position2 = new Rect(position.xMax, position.yMin, (float)num4, (float)num6);
		Rect position3 = new Rect(position2.xMax, position.yMin, (float)num5, (float)num6);
		Rect position4 = new Rect(position3.xMax, position.yMin, (float)num5, (float)num6);
		GUI.Label(position, "RingCount");
		this.m_window2text[num7] = GUI.TextField(position2, this.m_window2text[num7]);
		if (GUI.Button(position3, "Add"))
		{
			int num8 = int.Parse(this.m_window2text[num7], NumberStyles.AllowLeadingSign);
			PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
			if (playerInformation != null)
			{
				playerInformation.SetNumRings(playerInformation.NumRings + num8);
			}
		}
		if (GUI.Button(position4, "Stock"))
		{
			ObjUtil.SendMessageTransferRing();
		}
		num2 += 20;
		int num9 = 1;
		Rect position5 = new Rect((float)num, (float)num2, (float)num3, (float)num6);
		Rect position6 = new Rect(position5.xMax, position5.yMin, (float)num4, (float)num6);
		Rect position7 = new Rect(position6.xMax, position5.yMin, (float)num5, (float)num6);
		GUI.Label(position5, "AnimalCount");
		this.m_window2text[num9] = GUI.TextField(position6, this.m_window2text[num9]);
		if (GUI.Button(position7, "Add"))
		{
			int addCount = int.Parse(this.m_window2text[num9], NumberStyles.AllowLeadingSign);
			ObjUtil.SendMessageAddAnimal(addCount);
		}
		num2 += 20;
		int num10 = 2;
		Rect position8 = new Rect((float)num, (float)num2, (float)num3, (float)num6);
		Rect position9 = new Rect(position8.xMax, position8.yMin, (float)num4, (float)num6);
		Rect position10 = new Rect(position9.xMax, position8.yMin, (float)num5, (float)num6);
		GUI.Label(position8, "Distance");
		this.m_window2text[num10] = GUI.TextField(position9, this.m_window2text[num10]);
		if (GUI.Button(position10, "Add"))
		{
			int num11 = int.Parse(this.m_window2text[num10], NumberStyles.AllowLeadingSign);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnDebugAddDistance", num11, SendMessageOptions.DontRequireReceiver);
		}
		num2 += 20;
		int num12 = 3;
		Rect position11 = new Rect((float)num, (float)num2, (float)num3, (float)num6);
		Rect position12 = new Rect(position11.xMax, position11.yMin, (float)num4, (float)num6);
		Rect position13 = new Rect(position12.xMax, position11.yMin, (float)num5, (float)num6);
		GUI.Label(position11, "ComboCount");
		this.m_window2text[num12] = GUI.TextField(position12, this.m_window2text[num12]);
		if (GUI.Button(position13, "Add"))
		{
			int val = int.Parse(this.m_window2text[num12], NumberStyles.AllowLeadingSign);
			StageComboManager instance = StageComboManager.Instance;
			if (instance != null)
			{
				instance.DebugAddCombo(val);
			}
		}
		num2 += 20;
		if (EventManager.Instance != null && EventManager.Instance.IsSpecialStage())
		{
			int num13 = 4;
			Rect position14 = new Rect((float)num, (float)num2, (float)num3, (float)num6);
			Rect position15 = new Rect(position14.xMax, position14.yMin, (float)num4, (float)num6);
			Rect position16 = new Rect(position15.xMax, position14.yMin, (float)num5, (float)num6);
			GUI.Label(position14, "SPObjectCount");
			this.m_window2text[num13] = GUI.TextField(position15, this.m_window2text[num13]);
			if (GUI.Button(position16, "Add"))
			{
				int count = int.Parse(this.m_window2text[num13], NumberStyles.AllowLeadingSign);
				ObjUtil.SendMessageAddSpecialCrystal(count);
			}
			num2 += 20;
		}
		Rect position17 = new Rect((float)num, (float)num2, (float)num3, (float)num6);
		if (GUI.Button(position17, "BossDead"))
		{
			GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgDebugDead", null, SendMessageOptions.DontRequireReceiver);
		}
		num2 += 20;
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x0007789C File Offset: 0x00075A9C
	private void WindowFunction3(int windowID)
	{
		int num = 5;
		int num2 = 20;
		int num3 = 300;
		int num4 = 20;
		FriendSignManager instance = FriendSignManager.Instance;
		if (instance)
		{
			List<FriendSignData> friendSignDataList = instance.GetFriendSignDataList();
			foreach (FriendSignData data in friendSignDataList)
			{
				Rect position = new Rect((float)num, (float)num2, (float)num3, (float)num4);
				GUI.Label(position, FriendSignManager.GetDebugDataString(data));
				num2 += num4;
			}
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06001594 RID: 5524 RVA: 0x00077948 File Offset: 0x00075B48
	public static StageDebugInformation Instance
	{
		get
		{
			return StageDebugInformation.m_instance;
		}
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x00077950 File Offset: 0x00075B50
	public static void CreateActivateButton()
	{
		if (StageDebugInformation.Instance == null)
		{
			StageDebugInformation.Create();
		}
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x00077968 File Offset: 0x00075B68
	public static void DestroyActivateButton()
	{
		if (StageDebugInformation.Instance != null && !StageDebugInformation.Instance.m_showInformation)
		{
			UnityEngine.Object.Destroy(StageDebugInformation.Instance.gameObject);
		}
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000779A4 File Offset: 0x00075BA4
	private static StageDebugInformation Create()
	{
		return null;
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000779B4 File Offset: 0x00075BB4
	private bool SetInstance()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x0400130B RID: 4875
	private PlayerInformation m_playerInformation;

	// Token: 0x0400130C RID: 4876
	private StageBlockManager m_blockManager;

	// Token: 0x0400130D RID: 4877
	private Rect m_window;

	// Token: 0x0400130E RID: 4878
	private Rect m_window2;

	// Token: 0x0400130F RID: 4879
	private Rect m_window3;

	// Token: 0x04001310 RID: 4880
	private string[] m_window2text = new string[5];

	// Token: 0x04001311 RID: 4881
	public bool m_showInformation;

	// Token: 0x04001312 RID: 4882
	private static StageDebugInformation m_instance;

	// Token: 0x020002ED RID: 749
	private enum StageDebugEditItem
	{
		// Token: 0x04001314 RID: 4884
		RING_ADD_COUNT,
		// Token: 0x04001315 RID: 4885
		ANIMAL_ADD_COUNT,
		// Token: 0x04001316 RID: 4886
		DISTANCE_ADD,
		// Token: 0x04001317 RID: 4887
		COMBO_ADD,
		// Token: 0x04001318 RID: 4888
		SPCRYSTAL_ADD_COUNT,
		// Token: 0x04001319 RID: 4889
		NUM
	}
}
