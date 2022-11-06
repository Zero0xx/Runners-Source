using System;
using Message;
using UnityEngine;

// Token: 0x0200056C RID: 1388
[AddComponentMenu("NGUI/Interaction/UIFlck")]
public class UIFlick : MonoBehaviour
{
	// Token: 0x06002AB6 RID: 10934 RVA: 0x001085D4 File Offset: 0x001067D4
	public float GetDragDistance()
	{
		return this.m_base_point.x - this.m_first_touch_pos.x;
	}

	// Token: 0x06002AB7 RID: 10935 RVA: 0x001085F0 File Offset: 0x001067F0
	public void SetCallBack(GameObject obj, string method_name)
	{
		this.m_target_obj = obj;
		this.m_method_name = method_name;
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x00108600 File Offset: 0x00106800
	private void SendMessage(bool right_flick_flag)
	{
		if (this.m_target_obj != null)
		{
			FlickType type = (!right_flick_flag) ? FlickType.LEFT : FlickType.RIGHT;
			MsgUIFlick value = new MsgUIFlick(type);
			this.m_target_obj.SendMessage(this.m_method_name, value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x00108648 File Offset: 0x00106848
	private void Start()
	{
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x0010864C File Offset: 0x0010684C
	private void Update()
	{
		int touchCount = Input.touchCount;
		if (touchCount > 0)
		{
			Touch touch = Input.touches[0];
			Vector2 position = touch.position;
			switch (touch.phase)
			{
			case TouchPhase.Began:
				this.OnTouchBegan(position);
				break;
			case TouchPhase.Moved:
				this.OnTouchMove(position);
				break;
			case TouchPhase.Stationary:
				this.OnTouchStationary(position);
				break;
			case TouchPhase.Ended:
				this.OnTouchEnd(position);
				break;
			}
		}
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x001086DC File Offset: 0x001068DC
	private void UpdateTouchData(Vector2 position, bool first_touch_flag)
	{
		if (first_touch_flag)
		{
			this.m_first_touch_pos = position;
		}
		this.m_base_point = position;
		this.m_start_time = Time.realtimeSinceStartup;
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x00108700 File Offset: 0x00106900
	private void OnTouchBegan(Vector2 position)
	{
		bool first_touch_flag = true;
		this.UpdateTouchData(position, first_touch_flag);
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x00108718 File Offset: 0x00106918
	private void OnTouchMove(Vector2 position)
	{
		float num = Mathf.Abs(position.x - this.m_base_point.x);
		this.m_distance_flag = (num > 10f);
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x0010874C File Offset: 0x0010694C
	private void OnTouchStationary(Vector2 position)
	{
		if (this.m_distance_flag)
		{
			float num = Mathf.Abs(position.x - this.m_base_point.x);
			if (num < 10f)
			{
				this.m_distance_flag = false;
			}
		}
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x00108790 File Offset: 0x00106990
	private void OnTouchEnd(Vector2 position)
	{
		if (this.m_distance_flag)
		{
			float num = Time.realtimeSinceStartup - this.m_start_time;
			if (num < 0.3f)
			{
				float num2 = position.x - this.m_base_point.x;
				bool right_flick_flag = true;
				if (num2 < 0f)
				{
					right_flick_flag = false;
					this.SendMessage(right_flick_flag);
					global::Debug.Log("Left Flick Success");
				}
				else
				{
					this.SendMessage(right_flick_flag);
					global::Debug.Log("Right Flick Success");
				}
			}
		}
	}

	// Token: 0x04002628 RID: 9768
	private const float FLICK_DISTANCE_THRESHOLD_VALUE = 10f;

	// Token: 0x04002629 RID: 9769
	private const float FLICK_TIME_THRESHOLD_VALUE = 0.3f;

	// Token: 0x0400262A RID: 9770
	private Vector2 m_first_touch_pos = Vector2.zero;

	// Token: 0x0400262B RID: 9771
	private Vector2 m_base_point = Vector2.zero;

	// Token: 0x0400262C RID: 9772
	private float m_start_time;

	// Token: 0x0400262D RID: 9773
	private GameObject m_target_obj;

	// Token: 0x0400262E RID: 9774
	private string m_method_name = string.Empty;

	// Token: 0x0400262F RID: 9775
	private bool m_distance_flag;
}
