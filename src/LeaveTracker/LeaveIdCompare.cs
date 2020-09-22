using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace LeaveTracker
{
    public class LeaveIdCompare : IEqualityComparer<Leave>
    {
        public bool Equals(Leave x,Leave y)
        {
            return x.GetLeaveId().Equals(y.GetLeaveId());
        }

        public int GetHashCode([DisallowNull] Leave obj)
        {
            return obj.GetLeaveId().GetHashCode();
        }
    }
}
