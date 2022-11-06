using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000097 RID: 151
[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : UIWidgetContainer
{
	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013F6C File Offset: 0x0001216C
	// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00013FA8 File Offset: 0x000121A8
	public float value
	{
		get
		{
			float num = this.rawValue;
			if (this.numberOfSteps > 1)
			{
				num = Mathf.Round(num * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
			}
			return num;
		}
		set
		{
			this.Set(value, false);
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00013FB4 File Offset: 0x000121B4
	// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00013FBC File Offset: 0x000121BC
	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00013FC8 File Offset: 0x000121C8
	// (set) Token: 0x060003FA RID: 1018 RVA: 0x00013FD0 File Offset: 0x000121D0
	public Vector2 fullSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			if (this.mSize != value)
			{
				this.mSize = value;
				this.ForceUpdate();
			}
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00013FF0 File Offset: 0x000121F0
	private void Init()
	{
		this.mInitDone = true;
		if (this.foreground != null)
		{
			this.mFGWidget = this.foreground.GetComponent<UIWidget>();
			this.mFGFilled = ((!(this.mFGWidget != null)) ? null : (this.mFGWidget as UISprite));
			this.mFGTrans = this.foreground.transform;
			if (this.mSize == Vector2.zero)
			{
				UIWidget component = this.foreground.GetComponent<UIWidget>();
				this.mSize = ((!(component != null)) ? this.foreground.localScale : new Vector2((float)component.width, (float)component.height));
			}
			if (this.mCenter == Vector2.zero)
			{
				UIWidget component2 = this.foreground.GetComponent<UIWidget>();
				if (component2 != null)
				{
					Vector3[] localCorners = component2.localCorners;
					this.mCenter = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				}
				else
				{
					this.mCenter = this.foreground.localPosition + this.foreground.localScale * 0.5f;
				}
			}
		}
		else if (this.mCol != null)
		{
			if (this.mSize == Vector2.zero)
			{
				this.mSize = this.mCol.size;
			}
			if (this.mCenter == Vector2.zero)
			{
				this.mCenter = this.mCol.center;
			}
		}
		else
		{
			global::Debug.LogWarning("UISlider expected to find a foreground object or a box collider to work with", this);
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000141CC File Offset: 0x000123CC
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mCol = (base.collider as BoxCollider);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000141EC File Offset: 0x000123EC
	private void Start()
	{
		this.Init();
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (Application.isPlaying && this.thumb != null && this.thumb.collider != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.thumb.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressThumb));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragThumb));
		}
		this.Set(this.rawValue, true);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000142B0 File Offset: 0x000124B0
	private void OnPress(bool pressed)
	{
		if (base.enabled && pressed && UICamera.currentTouchID != -100)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000142D8 File Offset: 0x000124D8
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000142EC File Offset: 0x000124EC
	private void OnPressThumb(GameObject go, bool pressed)
	{
		if (base.enabled && pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00014308 File Offset: 0x00012508
	private void OnDragThumb(GameObject go, Vector2 delta)
	{
		if (base.enabled)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001431C File Offset: 0x0001251C
	private void OnKey(KeyCode key)
	{
		if (base.enabled)
		{
			float num = ((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1));
			if (this.direction == UISlider.Direction.Horizontal)
			{
				if (key == KeyCode.LeftArrow)
				{
					this.Set(this.rawValue - num, false);
				}
				else if (key == KeyCode.RightArrow)
				{
					this.Set(this.rawValue + num, false);
				}
			}
			else if (key == KeyCode.DownArrow)
			{
				this.Set(this.rawValue - num, false);
			}
			else if (key == KeyCode.UpArrow)
			{
				this.Set(this.rawValue + num, false);
			}
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000143E4 File Offset: 0x000125E4
	private void UpdateDrag()
	{
		if (this.mCol == null || UICamera.currentCamera == null || UICamera.currentTouch == null)
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		Plane plane = new Plane(this.mTrans.rotation * Vector3.back, this.mTrans.position);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return;
		}
		Vector3 b = this.mTrans.localPosition + (this.mCenter - this.mSize * 0.5f);
		Vector3 b2 = this.mTrans.localPosition - b;
		Vector3 a = this.mTrans.InverseTransformPoint(ray.GetPoint(distance));
		Vector3 vector = a + b2;
		this.Set((this.direction != UISlider.Direction.Horizontal) ? (vector.y / this.mSize.y) : (vector.x / this.mSize.x), false);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001451C File Offset: 0x0001271C
	private void Set(float input, bool force)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		float num = Mathf.Clamp01(input);
		if (num < 0.001f)
		{
			num = 0f;
		}
		float value = this.value;
		this.rawValue = num;
		float value2 = this.value;
		if (force || value != value2)
		{
			Vector3 localScale = this.mSize;
			if (this.direction == UISlider.Direction.Horizontal)
			{
				localScale.x *= value2;
			}
			else
			{
				localScale.y *= value2;
			}
			if (this.mFGFilled != null && this.mFGFilled.type == UISprite.Type.Filled)
			{
				this.mFGFilled.fillAmount = value2;
			}
			else if (this.mFGWidget != null)
			{
				if (value2 > 0.001f)
				{
					this.mFGWidget.width = Mathf.RoundToInt(localScale.x);
					this.mFGWidget.height = Mathf.RoundToInt(localScale.y);
					this.mFGWidget.enabled = true;
				}
				else
				{
					this.mFGWidget.enabled = false;
				}
			}
			else if (this.foreground != null)
			{
				this.mFGTrans.localScale = localScale;
			}
			if (this.thumb != null)
			{
				Vector3 localPosition = this.thumb.localPosition;
				if (this.mFGFilled != null && this.mFGFilled.type == UISprite.Type.Filled)
				{
					if (this.mFGFilled.fillDirection == UISprite.FillDirection.Horizontal)
					{
						localPosition.x = ((!this.mFGFilled.invert) ? localScale.x : (this.mSize.x - localScale.x));
					}
					else if (this.mFGFilled.fillDirection == UISprite.FillDirection.Vertical)
					{
						localPosition.y = ((!this.mFGFilled.invert) ? localScale.y : (this.mSize.y - localScale.y));
					}
					else
					{
						global::Debug.LogWarning("Slider thumb is only supported with Horizontal or Vertical fill direction", this);
					}
				}
				else if (this.direction == UISlider.Direction.Horizontal)
				{
					localPosition.x = localScale.x;
				}
				else
				{
					localPosition.y = localScale.y;
				}
				this.thumb.localPosition = localPosition;
			}
			UISlider.current = this;
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, value2, SendMessageOptions.DontRequireReceiver);
			}
			UISlider.current = null;
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x000147F0 File Offset: 0x000129F0
	public void ForceUpdate()
	{
		this.Set(this.rawValue, true);
	}

	// Token: 0x040002C9 RID: 713
	public static UISlider current;

	// Token: 0x040002CA RID: 714
	public Transform foreground;

	// Token: 0x040002CB RID: 715
	public Transform thumb;

	// Token: 0x040002CC RID: 716
	public UISlider.Direction direction;

	// Token: 0x040002CD RID: 717
	public int numberOfSteps;

	// Token: 0x040002CE RID: 718
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040002CF RID: 719
	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	// Token: 0x040002D0 RID: 720
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040002D1 RID: 721
	[SerializeField]
	[HideInInspector]
	private string functionName = "OnSliderChange";

	// Token: 0x040002D2 RID: 722
	private BoxCollider mCol;

	// Token: 0x040002D3 RID: 723
	private Transform mTrans;

	// Token: 0x040002D4 RID: 724
	private Transform mFGTrans;

	// Token: 0x040002D5 RID: 725
	private UIWidget mFGWidget;

	// Token: 0x040002D6 RID: 726
	private UISprite mFGFilled;

	// Token: 0x040002D7 RID: 727
	private bool mInitDone;

	// Token: 0x040002D8 RID: 728
	private Vector2 mSize = Vector2.zero;

	// Token: 0x040002D9 RID: 729
	private Vector2 mCenter = Vector3.zero;

	// Token: 0x02000098 RID: 152
	public enum Direction
	{
		// Token: 0x040002DB RID: 731
		Horizontal,
		// Token: 0x040002DC RID: 732
		Vertical
	}
}
