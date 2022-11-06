using System;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public class ui_ranking_reward : MonoBehaviour
{
	// Token: 0x060026C0 RID: 9920 RVA: 0x000EC5E4 File Offset: 0x000EA7E4
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x000EC5F0 File Offset: 0x000EA7F0
	private void Update()
	{
		if (this.m_parent == null)
		{
			this.m_parent = base.gameObject.transform.parent.GetComponent<UIDraggablePanel>();
			if (this.m_parent == null)
			{
				this.m_parent = base.gameObject.transform.parent.transform.parent.GetComponent<UIDraggablePanel>();
				this.m_dragPanelContents.draggablePanel = this.m_parent;
			}
			else
			{
				this.m_dragPanelContents.draggablePanel = this.m_parent;
			}
		}
		if (this.m_table == null)
		{
			this.m_table = base.gameObject.transform.parent.GetComponent<UITable>();
			if (this.m_table == null)
			{
				this.m_table = base.gameObject.transform.parent.transform.parent.GetComponent<UITable>();
			}
		}
		if (this.m_move > 0f)
		{
			this.m_move -= Time.deltaTime;
			if (this.m_move <= 0f)
			{
				if (this.m_table != null)
				{
					this.m_table.repositionNow = true;
				}
				this.m_move = 0f;
			}
		}
		if (this.m_collider.size.y - 48f != (float)this.m_label.height)
		{
			float num = (float)this.m_label.height * 1.2f + 48f;
			this.m_collider.size = new Vector3(this.m_collider.size.x, num, this.m_collider.size.z);
			this.m_bg.height = (int)num;
		}
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
	private void OnClickBg()
	{
		global::Debug.Log("OnClickBg m_icon:" + (this.m_icon != null));
	}

	// Token: 0x040022FC RID: 8956
	public const int ADD_HEGHT = 48;

	// Token: 0x040022FD RID: 8957
	public const int INIT_LINE = 3;

	// Token: 0x040022FE RID: 8958
	public const float OPEN_SPEED = 0.5f;

	// Token: 0x040022FF RID: 8959
	public const float CLOSE_SPEED = 0.25f;

	// Token: 0x04002300 RID: 8960
	[SerializeField]
	private UISprite m_bg;

	// Token: 0x04002301 RID: 8961
	[SerializeField]
	private BoxCollider m_collider;

	// Token: 0x04002302 RID: 8962
	[SerializeField]
	private UILabel m_label;

	// Token: 0x04002303 RID: 8963
	[SerializeField]
	private UISprite m_icon;

	// Token: 0x04002304 RID: 8964
	[SerializeField]
	private UIDragPanelContents m_dragPanelContents;

	// Token: 0x04002305 RID: 8965
	private float m_move;

	// Token: 0x04002306 RID: 8966
	private UIDraggablePanel m_parent;

	// Token: 0x04002307 RID: 8967
	private UITable m_table;
}
