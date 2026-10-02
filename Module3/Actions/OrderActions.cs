using static Module3.Actions.OrderStatus;

namespace Module3.Actions;

public static class OrderActions
{
    public static string GetNextAction_After(Order order) => order switch
    {
        { Status: Pending, IsPaid: false } => "Send payment reminder",
        { Status: Paid, IsShipped: false } => "Ship the order",
        { Status: Shipped } => "Send tracking details",
        { Status: Delivered } => "Request a review",
        _ => "No action needed"
    };

    public static string GetNextAction_Before(Order order)
    {
        if (order.Status == Pending && !order.IsPaid)
            return "Send payment reminder";
        if (order.Status == Paid && !order.IsShipped)
            return "Ship the order";
        if (order.Status == Shipped)
            return "Send tracking details";
        if (order.Status == Delivered)
            return "Request a review";
        return "No action needed";
    }
}
