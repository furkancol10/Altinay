using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Enums
{
    public enum RequestStatus
    {
        Pending = 0,
        ApprovedByManager = 1,
        ApprovedByHr = 2,
        ApprovedByGeneralManager = 3,
        Rejected = 4
    }
}
