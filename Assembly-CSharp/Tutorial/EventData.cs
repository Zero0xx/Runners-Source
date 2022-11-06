using System;

namespace Tutorial
{
	// Token: 0x020002D0 RID: 720
	public class EventData
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0006D9C8 File Offset: 0x0006BBC8
		// Note: this type is marked as 'beforefieldinit'.
		static EventData()
		{
			EventClearType[] array = new EventClearType[8];
			array[4] = EventClearType.NO_DAMAGE;
			array[5] = EventClearType.NO_MISS;
			EventData.EVENT_TYPE_TBL = array;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0006D9E0 File Offset: 0x0006BBE0
		public static EventClearType GetEventClearType(EventID id)
		{
			if (id < EventID.NUM)
			{
				return EventData.EVENT_TYPE_TBL[(int)id];
			}
			return EventClearType.CLEAR;
		}

		// Token: 0x040011D9 RID: 4569
		private static readonly EventClearType[] EVENT_TYPE_TBL;
	}
}
