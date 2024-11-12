using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Orders.Domain.Orders.Repository;
public interface IOrderRepository
{
    void Add(Order order);
    void Update(Order order);
    Task<Order?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<int> SaveOrderChangesAsync(CancellationToken cancellationToken);
}
