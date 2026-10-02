using static Module3.Machine.OrderState;
using static Module3.Machine.OrderEvent;

namespace Module3.Machine;

public enum OrderState { Pending, Paid, Shipped, Delivered, Cancelled, Refunded }
public enum OrderEvent { PaymentReceived, Shipped, Delivered, CancelRequested, RefundRequested }

public static class OrderMachine
{
    public static OrderState Transition(OrderState state, OrderEvent orderEvent, int daysSincePaid) =>
        (state, orderEvent) switch
        {
            (Pending, PaymentReceived) => Paid,
            (Pending, CancelRequested) => Cancelled,
            (Paid, OrderEvent.Shipped) => OrderState.Shipped,
            (Paid, CancelRequested) => Cancelled,
            (OrderState.Shipped, OrderEvent.Delivered) => OrderState.Delivered,

            (OrderState.Delivered, RefundRequested) when daysSincePaid <= 30 => Refunded,

            _ => throw new InvalidOperationException(
                     $"Cannot apply {orderEvent} to an order in {state}.")
        };
}
