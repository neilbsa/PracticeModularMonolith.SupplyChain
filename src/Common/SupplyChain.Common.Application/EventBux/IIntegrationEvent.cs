using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Common.Application.EventBux;
public interface IIntegrationEvent
{
    DateTime OccurredOnUtc { get;  }
    Guid Id { get;  }
}
