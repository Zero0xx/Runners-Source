using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CD RID: 205
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Camera")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001E22C File Offset: 0x0001C42C
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001E23C File Offset: 0x0001C43C
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001E264 File Offset: 0x0001C464
	// (set) Token: 0x060005DE RID: 1502 RVA: 0x0001E26C File Offset: 0x0001C46C
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mSel;
		}
		set
		{
			if (UICamera.mSel != value)
			{
				if (UICamera.mSel != null)
				{
					UICamera uicamera = UICamera.FindCameraForLayer(UICamera.mSel.layer);
					if (uicamera != null)
					{
						UICamera.current = uicamera;
						UICamera.currentCamera = uicamera.mCam;
						UICamera.Notify(UICamera.mSel, "OnSelect", false);
						if (uicamera.useController || uicamera.useKeyboard)
						{
							UICamera.Highlight(UICamera.mSel, false);
						}
						UICamera.current = null;
					}
				}
				UICamera.mSel = value;
				if (UICamera.mSel != null)
				{
					UICamera uicamera2 = UICamera.FindCameraForLayer(UICamera.mSel.layer);
					if (uicamera2 != null)
					{
						UICamera.current = uicamera2;
						UICamera.currentCamera = uicamera2.mCam;
						if (uicamera2.useController || uicamera2.useKeyboard)
						{
							UICamera.Highlight(UICamera.mSel, true);
						}
						UICamera.Notify(UICamera.mSel, "OnSelect", true);
						UICamera.current = null;
					}
				}
			}
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001E384 File Offset: 0x0001C584
	public static int touchCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.pressed != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].pressed != null)
				{
					num++;
				}
			}
			if (UICamera.mController.pressed != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001E44C File Offset: 0x0001C64C
	public static int dragCount
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, UICamera.MouseOrTouch> keyValuePair in UICamera.mTouches)
			{
				if (keyValuePair.Value.dragged != null)
				{
					num++;
				}
			}
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.mController.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0001E514 File Offset: 0x0001C714
	private void OnApplicationQuit()
	{
		UICamera.mHighlighted.Clear();
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001E520 File Offset: 0x0001C720
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001E54C File Offset: 0x0001C74C
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.Count; i++)
			{
				UICamera uicamera = UICamera.list[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
	public static bool Raycast(Vector3 inPos, out RaycastHit hit)
	{
		for (int i = 0; i < UICamera.list.Count; i++)
		{
			UICamera uicamera = UICamera.list[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
				if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y))
				{
					if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
					{
						Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
						int layerMask = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
						float distance = (uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance;
						if (uicamera.eventType == UICamera.EventType.World)
						{
							if (Physics.Raycast(ray, out hit, distance, layerMask))
							{
								UICamera.hoveredObject = hit.collider.gameObject;
								return true;
							}
						}
						else if (uicamera.eventType == UICamera.EventType.UI)
						{
							RaycastHit[] array = Physics.RaycastAll(ray, distance, layerMask);
							if (array.Length > 1)
							{
								for (int j = 0; j < array.Length; j++)
								{
									GameObject gameObject = array[j].collider.gameObject;
									UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
									UICamera.mHit.hit = array[j];
									UICamera.mHits.Add(UICamera.mHit);
								}
								UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
								for (int k = 0; k < UICamera.mHits.size; k++)
								{
									if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
									{
										hit = UICamera.mHits[k].hit;
										UICamera.hoveredObject = hit.collider.gameObject;
										UICamera.mHits.Clear();
										return true;
									}
								}
								UICamera.mHits.Clear();
							}
							else if (array.Length == 1 && UICamera.IsVisible(ref array[0]))
							{
								hit = array[0];
								UICamera.hoveredObject = hit.collider.gameObject;
								return true;
							}
						}
					}
				}
			}
		}
		hit = UICamera.mEmpty;
		return false;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0001E8DC File Offset: 0x0001CADC
	private static bool IsVisible(ref RaycastHit hit)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(hit.collider.gameObject);
		return uipanel == null || uipanel.IsVisible(hit.point);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0001E91C File Offset: 0x0001CB1C
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.hit.collider.gameObject);
		return uipanel == null || uipanel.IsVisible(de.hit.point);
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0001E960 File Offset: 0x0001CB60
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.Count; i++)
		{
			UICamera uicamera = UICamera.list[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (Input.GetKeyDown(up))
		{
			return 1;
		}
		if (Input.GetKeyDown(down))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
		{
			return 1;
		}
		if (Input.GetKeyDown(down0) || Input.GetKeyDown(down1))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0001EA14 File Offset: 0x0001CC14
	private static int GetDirection(string axis)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (UICamera.mNextEvent < realtimeSinceStartup)
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0001EA6C File Offset: 0x0001CC6C
	public static bool IsHighlighted(GameObject go)
	{
		int i = UICamera.mHighlighted.Count;
		while (i > 0)
		{
			UICamera.Highlighted highlighted = UICamera.mHighlighted[--i];
			if (highlighted.go == go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
	private static void Highlight(GameObject go, bool highlighted)
	{
		if (go != null)
		{
			int i = UICamera.mHighlighted.Count;
			while (i > 0)
			{
				UICamera.Highlighted highlighted2 = UICamera.mHighlighted[--i];
				if (highlighted2 == null || highlighted2.go == null)
				{
					UICamera.mHighlighted.RemoveAt(i);
				}
				else if (highlighted2.go == go)
				{
					if (highlighted)
					{
						highlighted2.counter++;
					}
					else if (--highlighted2.counter < 1)
					{
						UICamera.mHighlighted.Remove(highlighted2);
						UICamera.Notify(go, "OnHover", false);
					}
					return;
				}
			}
			if (highlighted)
			{
				UICamera.Highlighted highlighted3 = new UICamera.Highlighted();
				highlighted3.go = go;
				highlighted3.counter = 1;
				UICamera.mHighlighted.Add(highlighted3);
				UICamera.Notify(go, "OnHover", true);
			}
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0001EBB0 File Offset: 0x0001CDB0
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (go != null)
		{
			go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			if (UICamera.genericEventHandler != null && UICamera.genericEventHandler != go)
			{
				UICamera.genericEventHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0001EC00 File Offset: 0x0001CE00
	public static UICamera.MouseOrTouch GetTouch(int id)
	{
		UICamera.MouseOrTouch mouseOrTouch = null;
		if (!UICamera.mTouches.TryGetValue(id, out mouseOrTouch))
		{
			mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.touchBegan = true;
			UICamera.mTouches.Add(id, mouseOrTouch);
		}
		return mouseOrTouch;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0001EC3C File Offset: 0x0001CE3C
	public static void RemoveTouch(int id)
	{
		UICamera.mTouches.Remove(id);
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0001EC4C File Offset: 0x0001CE4C
	private void Awake()
	{
		this.cachedCamera.eventMask = 0;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.BB10Player)
		{
			this.useMouse = false;
			this.useTouch = true;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				this.useKeyboard = false;
				this.useController = false;
			}
		}
		else if (Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.mIsEditor = true;
		}
		UICamera.mMouse[0].pos.x = Input.mousePosition.x;
		UICamera.mMouse[0].pos.y = Input.mousePosition.y;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		if (this.eventReceiverMask == -1)
		{
			this.eventReceiverMask = this.cachedCamera.cullingMask;
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0001ED8C File Offset: 0x0001CF8C
	private void OnEnable()
	{
		UICamera.list.Add(this);
		UICamera.list.Sort(new Comparison<UICamera>(UICamera.CompareFunc));
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
	private void FixedUpdate()
	{
		if (this.useMouse && Application.isPlaying && this.handlesEvents)
		{
			if (!UICamera.Raycast(Input.mousePosition, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			for (int i = 0; i < 3; i++)
			{
				UICamera.mMouse[i].current = UICamera.hoveredObject;
			}
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0001EE48 File Offset: 0x0001D048
	private void Update()
	{
		if (!Application.isPlaying || !this.handlesEvents)
		{
			return;
		}
		UICamera.current = this;
		if (this.useMouse || (this.useTouch && this.mIsEditor))
		{
			this.ProcessMouse();
		}
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if (this.useMouse && UICamera.mSel != null && ((this.cancelKey0 != KeyCode.None && Input.GetKeyDown(this.cancelKey0)) || (this.cancelKey1 != KeyCode.None && Input.GetKeyDown(this.cancelKey1))))
		{
			UICamera.selectedObject = null;
		}
		if (UICamera.mSel != null)
		{
			string text = Input.inputString;
			if (this.useKeyboard && Input.GetKeyDown(KeyCode.Delete))
			{
				text += "\b";
			}
			if (text.Length > 0)
			{
				if (!this.stickyTooltip && this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.Notify(UICamera.mSel, "OnInput", text);
			}
		}
		else
		{
			UICamera.inputHasFocus = false;
		}
		if (UICamera.mSel != null)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float axis = Input.GetAxis(this.scrollAxisName);
			if (axis != 0f)
			{
				UICamera.Notify(UICamera.mHover, "OnScroll", axis);
			}
			if (UICamera.showTooltips && this.mTooltipTime != 0f && (this.mTooltipTime < Time.realtimeSinceStartup || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
		UICamera.current = null;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0001F054 File Offset: 0x0001D254
	public void ProcessMouse()
	{
		bool flag = this.useMouse && Time.timeScale < 0.9f;
		if (!flag)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Input.GetMouseButton(i) || Input.GetMouseButtonUp(i))
				{
					flag = true;
					break;
				}
			}
		}
		UICamera.mMouse[0].pos = Input.mousePosition;
		UICamera.mMouse[0].delta = UICamera.mMouse[0].pos - UICamera.lastTouchPosition;
		bool flag2 = UICamera.mMouse[0].pos != UICamera.lastTouchPosition;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		if (flag)
		{
			if (!UICamera.Raycast(Input.mousePosition, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.mMouse[0].current = UICamera.hoveredObject;
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[j].delta = UICamera.mMouse[0].delta;
			UICamera.mMouse[j].current = UICamera.mMouse[0].current;
		}
		bool flag3 = false;
		for (int k = 0; k < 3; k++)
		{
			if (Input.GetMouseButton(k))
			{
				flag3 = true;
				break;
			}
		}
		if (flag3)
		{
			this.mTooltipTime = 0f;
		}
		else if (this.useMouse && flag2 && (!this.stickyTooltip || UICamera.mHover != UICamera.mMouse[0].current))
		{
			if (this.mTooltipTime != 0f)
			{
				this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			}
			else if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
		}
		if (this.useMouse && !flag3 && UICamera.mHover != null && UICamera.mHover != UICamera.mMouse[0].current)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.Highlight(UICamera.mHover, false);
			UICamera.mHover = null;
		}
		if (this.useMouse)
		{
			for (int l = 0; l < 3; l++)
			{
				bool mouseButtonDown = Input.GetMouseButtonDown(l);
				bool mouseButtonUp = Input.GetMouseButtonUp(l);
				UICamera.currentTouch = UICamera.mMouse[l];
				UICamera.currentTouchID = -1 - l;
				if (mouseButtonDown)
				{
					UICamera.currentTouch.pressedCam = UICamera.currentCamera;
				}
				else if (UICamera.currentTouch.pressed != null)
				{
					UICamera.currentCamera = UICamera.currentTouch.pressedCam;
				}
				this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			}
			UICamera.currentTouch = null;
		}
		if (this.useMouse && !flag3 && UICamera.mHover != UICamera.mMouse[0].current)
		{
			this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			UICamera.mHover = UICamera.mMouse[0].current;
			UICamera.Highlight(UICamera.mHover, true);
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
	public void ProcessTouches()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			UICamera.currentTouchID = ((!this.allowMultiTouch) ? 1 : touch.fingerId);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID);
			bool flag = touch.phase == TouchPhase.Began || UICamera.currentTouch.touchBegan;
			bool flag2 = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
			UICamera.currentTouch.touchBegan = false;
			if (flag)
			{
				UICamera.currentTouch.delta = Vector2.zero;
			}
			else
			{
				UICamera.currentTouch.delta = touch.position - UICamera.currentTouch.pos;
			}
			UICamera.currentTouch.pos = touch.position;
			if (!UICamera.Raycast(UICamera.currentTouch.pos, out UICamera.lastHit))
			{
				UICamera.hoveredObject = UICamera.fallThrough;
			}
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (touch.tapCount > 1)
			{
				UICamera.currentTouch.clickTime = Time.realtimeSinceStartup;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0001F59C File Offset: 0x0001D79C
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.mController;
		UICamera.inputHasFocus = (UICamera.mSel != null && UICamera.mSel.GetComponent<UIInput>() != null);
		bool flag = (this.submitKey0 != KeyCode.None && Input.GetKeyDown(this.submitKey0)) || (this.submitKey1 != KeyCode.None && Input.GetKeyDown(this.submitKey1));
		bool flag2 = (this.submitKey0 != KeyCode.None && Input.GetKeyUp(this.submitKey0)) || (this.submitKey1 != KeyCode.None && Input.GetKeyUp(this.submitKey1));
		if (flag || flag2)
		{
			UICamera.currentTouch.current = UICamera.mSel;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.current = null;
		}
		int num = 0;
		int num2 = 0;
		if (this.useKeyboard)
		{
			if (UICamera.inputHasFocus)
			{
				num += UICamera.GetDirection(KeyCode.UpArrow, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.RightArrow, KeyCode.LeftArrow);
			}
			else
			{
				num += UICamera.GetDirection(KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow);
			}
		}
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.Notify(UICamera.mSel, "OnKey", (num <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow);
		}
		if (num2 != 0)
		{
			UICamera.Notify(UICamera.mSel, "OnKey", (num2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow);
		}
		if (this.useKeyboard && Input.GetKeyDown(KeyCode.Tab))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Tab);
		}
		if (this.cancelKey0 != KeyCode.None && Input.GetKeyDown(this.cancelKey0))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Escape);
		}
		if (this.cancelKey1 != KeyCode.None && Input.GetKeyDown(this.cancelKey1))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Escape);
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0001F82C File Offset: 0x0001DA2C
	public void ProcessTouch(bool pressed, bool unpressed)
	{
		bool flag = UICamera.currentTouch == UICamera.mMouse[0] || UICamera.currentTouch == UICamera.mMouse[1] || UICamera.currentTouch == UICamera.mMouse[2];
		float num = (!flag) ? this.touchDragThreshold : this.mouseDragThreshold;
		float num2 = (!flag) ? this.touchClickThreshold : this.mouseClickThreshold;
		if (pressed)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.pressStarted = true;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = ((!flag) ? UICamera.ClickNotification.Always : UICamera.ClickNotification.BasedOnDelta);
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.currentTouch.pressed != UICamera.mSel)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.selectedObject = null;
			}
		}
		else
		{
			if (!this.stickyPress && !unpressed && UICamera.currentTouch.pressStarted && UICamera.currentTouch.pressed != UICamera.hoveredObject)
			{
				UICamera.isDragging = true;
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				UICamera.currentTouch.pressed = UICamera.hoveredObject;
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
				UICamera.isDragging = false;
			}
			if (UICamera.currentTouch.pressed != null)
			{
				float magnitude = UICamera.currentTouch.delta.magnitude;
				if (magnitude != 0f)
				{
					UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
					magnitude = UICamera.currentTouch.totalDelta.magnitude;
					if (!UICamera.currentTouch.dragStarted && num < magnitude)
					{
						UICamera.currentTouch.dragStarted = true;
						UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
					}
					if (UICamera.currentTouch.dragStarted)
					{
						if (this.mTooltip != null)
						{
							this.ShowTooltip(false);
						}
						UICamera.isDragging = true;
						bool flag2 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
						UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
						UICamera.isDragging = false;
						if (flag2)
						{
							UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
						}
						else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && num2 < magnitude)
						{
							UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
						}
					}
				}
			}
		}
		if (unpressed)
		{
			UICamera.currentTouch.pressStarted = false;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			if (UICamera.currentTouch.pressed != null)
			{
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				if (this.useMouse && UICamera.currentTouch.pressed == UICamera.mHover)
				{
					UICamera.Notify(UICamera.currentTouch.pressed, "OnHover", true);
				}
				if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.magnitude < num))
				{
					if (UICamera.currentTouch.pressed != UICamera.mSel)
					{
						UICamera.mSel = UICamera.currentTouch.pressed;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnSelect", true);
					}
					else
					{
						UICamera.mSel = UICamera.currentTouch.pressed;
					}
					if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None)
					{
						float realtimeSinceStartup = Time.realtimeSinceStartup;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
						if (UICamera.currentTouch.clickTime + 0.35f > realtimeSinceStartup)
						{
							UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
						}
						UICamera.currentTouch.clickTime = realtimeSinceStartup;
					}
				}
				else
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
				}
			}
			UICamera.currentTouch.dragStarted = false;
			UICamera.currentTouch.pressed = null;
			UICamera.currentTouch.dragged = null;
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0001FD20 File Offset: 0x0001DF20
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		UICamera.Notify(this.mTooltip, "OnTooltip", val);
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x04000431 RID: 1073
	public static List<UICamera> list = new List<UICamera>();

	// Token: 0x04000432 RID: 1074
	public UICamera.EventType eventType = UICamera.EventType.UI;

	// Token: 0x04000433 RID: 1075
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04000434 RID: 1076
	public bool debug;

	// Token: 0x04000435 RID: 1077
	public bool useMouse = true;

	// Token: 0x04000436 RID: 1078
	public bool useTouch = true;

	// Token: 0x04000437 RID: 1079
	public bool allowMultiTouch = true;

	// Token: 0x04000438 RID: 1080
	public bool useKeyboard = true;

	// Token: 0x04000439 RID: 1081
	public bool useController = true;

	// Token: 0x0400043A RID: 1082
	public bool stickyPress = true;

	// Token: 0x0400043B RID: 1083
	public bool stickyTooltip = true;

	// Token: 0x0400043C RID: 1084
	public float tooltipDelay = 1f;

	// Token: 0x0400043D RID: 1085
	public float mouseDragThreshold = 4f;

	// Token: 0x0400043E RID: 1086
	public float mouseClickThreshold = 10f;

	// Token: 0x0400043F RID: 1087
	public float touchDragThreshold = 40f;

	// Token: 0x04000440 RID: 1088
	public float touchClickThreshold = 40f;

	// Token: 0x04000441 RID: 1089
	public float rangeDistance = -1f;

	// Token: 0x04000442 RID: 1090
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x04000443 RID: 1091
	public string verticalAxisName = "Vertical";

	// Token: 0x04000444 RID: 1092
	public string horizontalAxisName = "Horizontal";

	// Token: 0x04000445 RID: 1093
	public KeyCode submitKey0 = KeyCode.Return;

	// Token: 0x04000446 RID: 1094
	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	// Token: 0x04000447 RID: 1095
	public KeyCode cancelKey0 = KeyCode.Escape;

	// Token: 0x04000448 RID: 1096
	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	// Token: 0x04000449 RID: 1097
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x0400044A RID: 1098
	public static bool showTooltips = true;

	// Token: 0x0400044B RID: 1099
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x0400044C RID: 1100
	public static RaycastHit lastHit;

	// Token: 0x0400044D RID: 1101
	public static UICamera current = null;

	// Token: 0x0400044E RID: 1102
	public static Camera currentCamera = null;

	// Token: 0x0400044F RID: 1103
	public static int currentTouchID = -1;

	// Token: 0x04000450 RID: 1104
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x04000451 RID: 1105
	public static bool inputHasFocus = false;

	// Token: 0x04000452 RID: 1106
	public static GameObject genericEventHandler;

	// Token: 0x04000453 RID: 1107
	public static GameObject fallThrough;

	// Token: 0x04000454 RID: 1108
	private static List<UICamera.Highlighted> mHighlighted = new List<UICamera.Highlighted>();

	// Token: 0x04000455 RID: 1109
	private static GameObject mSel = null;

	// Token: 0x04000456 RID: 1110
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x04000457 RID: 1111
	private static GameObject mHover;

	// Token: 0x04000458 RID: 1112
	private static UICamera.MouseOrTouch mController = new UICamera.MouseOrTouch();

	// Token: 0x04000459 RID: 1113
	private static float mNextEvent = 0f;

	// Token: 0x0400045A RID: 1114
	private static Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x0400045B RID: 1115
	private GameObject mTooltip;

	// Token: 0x0400045C RID: 1116
	private Camera mCam;

	// Token: 0x0400045D RID: 1117
	private LayerMask mLayerMask;

	// Token: 0x0400045E RID: 1118
	private float mTooltipTime;

	// Token: 0x0400045F RID: 1119
	private bool mIsEditor;

	// Token: 0x04000460 RID: 1120
	public static bool isDragging = false;

	// Token: 0x04000461 RID: 1121
	public static GameObject hoveredObject;

	// Token: 0x04000462 RID: 1122
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x04000463 RID: 1123
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x04000464 RID: 1124
	private static RaycastHit mEmpty = default(RaycastHit);

	// Token: 0x020000CE RID: 206
	public enum ClickNotification
	{
		// Token: 0x04000467 RID: 1127
		None,
		// Token: 0x04000468 RID: 1128
		Always,
		// Token: 0x04000469 RID: 1129
		BasedOnDelta
	}

	// Token: 0x020000CF RID: 207
	public class MouseOrTouch
	{
		// Token: 0x0400046A RID: 1130
		public Vector2 pos;

		// Token: 0x0400046B RID: 1131
		public Vector2 delta;

		// Token: 0x0400046C RID: 1132
		public Vector2 totalDelta;

		// Token: 0x0400046D RID: 1133
		public Camera pressedCam;

		// Token: 0x0400046E RID: 1134
		public GameObject current;

		// Token: 0x0400046F RID: 1135
		public GameObject pressed;

		// Token: 0x04000470 RID: 1136
		public GameObject dragged;

		// Token: 0x04000471 RID: 1137
		public float clickTime;

		// Token: 0x04000472 RID: 1138
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x04000473 RID: 1139
		public bool touchBegan = true;

		// Token: 0x04000474 RID: 1140
		public bool pressStarted;

		// Token: 0x04000475 RID: 1141
		public bool dragStarted;
	}

	// Token: 0x020000D0 RID: 208
	public enum EventType
	{
		// Token: 0x04000477 RID: 1143
		World,
		// Token: 0x04000478 RID: 1144
		UI
	}

	// Token: 0x020000D1 RID: 209
	private class Highlighted
	{
		// Token: 0x04000479 RID: 1145
		public GameObject go;

		// Token: 0x0400047A RID: 1146
		public int counter;
	}

	// Token: 0x020000D2 RID: 210
	private struct DepthEntry
	{
		// Token: 0x0400047B RID: 1147
		public int depth;

		// Token: 0x0400047C RID: 1148
		public RaycastHit hit;
	}

	// Token: 0x02000A71 RID: 2673
	// (Invoke) Token: 0x060047FE RID: 18430
	public delegate void OnCustomInput();
}
