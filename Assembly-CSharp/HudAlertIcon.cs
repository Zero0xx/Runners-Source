using System;
using Message;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class HudAlertIcon : MonoBehaviour
{
	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0008F888 File Offset: 0x0008DA88
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x0008F890 File Offset: 0x0008DA90
	public void Setup(Camera camera, GameObject chaseObject, float displayTime)
	{
		this.m_camera = camera;
		this.m_chaseObject = chaseObject;
		this.m_displayTime = displayTime;
		this.m_currentTime = 0f;
		this.m_isEnd = false;
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x0008F8BC File Offset: 0x0008DABC
	public bool IsChasingObject(GameObject gameObject)
	{
		return this.m_chaseObject == gameObject;
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x0008F8D4 File Offset: 0x0008DAD4
	private void Start()
	{
		if (HudAlertIcon.m_prefabObject == null)
		{
			HudAlertIcon.m_prefabObject = (Resources.Load("Prefabs/UI/ui_gp_icon_alert") as GameObject);
			if (HudAlertIcon.m_prefabObject == null)
			{
				return;
			}
		}
		this.m_iconObject = (UnityEngine.Object.Instantiate(HudAlertIcon.m_prefabObject, Vector3.zero, Quaternion.identity) as GameObject);
		this.m_iconObject.SetActive(false);
		GameObject gameObject = GameObject.Find("UI Root (2D)/Camera/Anchor_6_MR/");
		if (gameObject == null)
		{
			gameObject = new GameObject("Anchor_6_MR");
			GameObject gameObject2 = GameObject.Find("UI Root (2D)/Camera");
			if (gameObject2 != null)
			{
				gameObject.transform.parent = gameObject2.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			UIAnchor uianchor = gameObject.AddComponent<UIAnchor>();
			uianchor.side = UIAnchor.Side.Right;
			uianchor.halfPixelOffset = false;
			if (gameObject2 != null)
			{
				uianchor.uiCamera = gameObject2.GetComponent<Camera>();
			}
		}
		this.m_iconObject.transform.parent = gameObject.transform;
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StatePlay));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x0008FA70 File Offset: 0x0008DC70
	private void Update()
	{
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x0008FA74 File Offset: 0x0008DC74
	private TinyFsmState StatePlay(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			this.m_iconObject.SetActive(true);
			Vector2 screenPosition = HudUtility.GetScreenPosition(this.m_camera, this.m_chaseObject);
			screenPosition.x = -50f;
			screenPosition.y -= (float)Screen.height * 0.5f;
			Transform component = this.m_iconObject.GetComponent<Transform>();
			component.localPosition = new Vector3(screenPosition.x, screenPosition.y, 0f);
			this.m_currentTime += Time.deltaTime;
			if (this.m_currentTime >= this.m_displayTime)
			{
				this.m_isEnd = true;
				this.m_iconObject.SetActive(false);
				UnityEngine.Object.Destroy(this.m_iconObject);
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x0008FB8C File Offset: 0x0008DD8C
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x0008FBD8 File Offset: 0x0008DDD8
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x04001642 RID: 5698
	private const float X_OFFSET = 50f;

	// Token: 0x04001643 RID: 5699
	private static GameObject m_prefabObject;

	// Token: 0x04001644 RID: 5700
	private GameObject m_iconObject;

	// Token: 0x04001645 RID: 5701
	private Camera m_camera;

	// Token: 0x04001646 RID: 5702
	private GameObject m_chaseObject;

	// Token: 0x04001647 RID: 5703
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001648 RID: 5704
	private float m_displayTime;

	// Token: 0x04001649 RID: 5705
	private float m_currentTime;

	// Token: 0x0400164A RID: 5706
	private bool m_isEnd;
}
