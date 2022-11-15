using System.Linq;

namespace SportsStore.Models.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
