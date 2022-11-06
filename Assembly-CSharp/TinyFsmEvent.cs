using System;
using Message;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class TinyFsmEvent : TinyFsmSystemEvent
{
	// Token: 0x0600143E RID: 5182 RVA: 0x0006CF7C File Offset: 0x0006B17C
	public TinyFsmEvent(int sig) : base(sig)
	{
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x0006CF88 File Offset: 0x0006B188
	public TinyFsmEvent(int sig, float deltaTime) : base(sig)
	{
		this.m_type = TinyFsmEvent.EventDataType.DELTATIME;
		this.m_float = deltaTime;
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x0006CFA0 File Offset: 0x0006B1A0
	public TinyFsmEvent(int sig, MessageBase msg) : base(sig)
	{
		this.m_type = TinyFsmEvent.EventDataType.MESSAGE;
		this.m_message = msg;
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x0006CFB8 File Offset: 0x0006B1B8
	public TinyFsmEvent(int sig, UnityEngine.Object obj) : base(sig)
	{
		this.m_type = TinyFsmEvent.EventDataType.OBJECT;
		this.m_object = obj;
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x0006CFD0 File Offset: 0x0006B1D0
	public TinyFsmEvent(int sig, int integer) : base(sig)
	{
		this.m_type = TinyFsmEvent.EventDataType.OBJECT;
		this.m_integer = integer;
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06001443 RID: 5187 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
	public float GetDeltaTime
	{
		get
		{
			return this.m_float;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06001444 RID: 5188 RVA: 0x0006CFF0 File Offset: 0x0006B1F0
	public MessageBase GetMessage
	{
		get
		{
			if (this.m_type == TinyFsmEvent.EventDataType.MESSAGE)
			{
				return this.m_message;
			}
			return null;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06001445 RID: 5189 RVA: 0x0006D008 File Offset: 0x0006B208
	public UnityEngine.Object GetObject
	{
		get
		{
			if (this.m_type == TinyFsmEvent.EventDataType.OBJECT)
			{
				return this.m_object;
			}
			return null;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06001446 RID: 5190 RVA: 0x0006D020 File Offset: 0x0006B220
	public int GetInt
	{
		get
		{
			if (this.m_type == TinyFsmEvent.EventDataType.INTEGER)
			{
				return this.m_integer;
			}
			return 0;
		}
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0006D038 File Offset: 0x0006B238
	public static TinyFsmEvent CreateSignal(int sig)
	{
		return new TinyFsmEvent(sig);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0006D040 File Offset: 0x0006B240
	public static TinyFsmEvent CreateSuper()
	{
		return TinyFsmEvent.CreateSignal(-1);
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0006D048 File Offset: 0x0006B248
	public static TinyFsmEvent CreateInit()
	{
		return TinyFsmEvent.CreateSignal(-2);
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0006D054 File Offset: 0x0006B254
	public static TinyFsmEvent CreateEnter()
	{
		return TinyFsmEvent.CreateSignal(-3);
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0006D060 File Offset: 0x0006B260
	public static TinyFsmEvent CreateLeave()
	{
		return TinyFsmEvent.CreateSignal(-4);
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0006D06C File Offset: 0x0006B26C
	public static TinyFsmEvent CreateUserEvent(int signal)
	{
		return TinyFsmEvent.CreateSignal(signal);
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0006D074 File Offset: 0x0006B274
	public static TinyFsmEvent CreateUpdate(float deltaTime)
	{
		return new TinyFsmEvent(0, deltaTime);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0006D080 File Offset: 0x0006B280
	public static TinyFsmEvent CreateMessage(MessageBase msg)
	{
		return new TinyFsmEvent(1, msg);
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x0006D08C File Offset: 0x0006B28C
	public static TinyFsmEvent CreateUserEventObject(int signal, UnityEngine.Object obj)
	{
		return new TinyFsmEvent(signal, obj);
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0006D098 File Offset: 0x0006B298
	public static TinyFsmEvent CreateUserEventInt(int signal, int integer)
	{
		return new TinyFsmEvent(signal, integer);
	}

	// Token: 0x040011A0 RID: 4512
	private TinyFsmEvent.EventDataType m_type;

	// Token: 0x040011A1 RID: 4513
	private int m_integer;

	// Token: 0x040011A2 RID: 4514
	private float m_float;

	// Token: 0x040011A3 RID: 4515
	private MessageBase m_message;

	// Token: 0x040011A4 RID: 4516
	private UnityEngine.Object m_object;

	// Token: 0x020002C8 RID: 712
	private enum EventDataType
	{
		// Token: 0x040011A6 RID: 4518
		NON,
		// Token: 0x040011A7 RID: 4519
		DELTATIME,
		// Token: 0x040011A8 RID: 4520
		MESSAGE,
		// Token: 0x040011A9 RID: 4521
		OBJECT,
		// Token: 0x040011AA RID: 4522
		INTEGER
	}
}
