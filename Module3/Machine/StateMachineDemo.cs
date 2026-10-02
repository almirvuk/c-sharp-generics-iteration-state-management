using static Module3.Machine.OrderState;
using static Module3.Machine.OrderEvent;

namespace Module3.Machine;

public static class StateMachineDemo
{
    public static void Run()
    {
        // A legal lifecycle: pending -> paid -> shipped -> delivered.
        var state = Pending;
        foreach (var evt in new[] { PaymentReceived, OrderEvent.Shipped, OrderEvent.Delivered })
        {
            var next = OrderMachine.Transition(state, evt, daysSincePaid: 5);
            Console.WriteLine($"{state} --{evt}--> {next}");
            state = next;
        }

        // A guarded move: refund allowed only within 30 days of payment.
        var refunded = OrderMachine.Transition(OrderState.Delivered, RefundRequested, daysSincePaid: 5);
        Console.WriteLine($"Delivered --RefundRequested (5 days)--> {refunded}");

        // An illegal move: cannot deliver an order that is still pending.
        try
        {
            OrderMachine.Transition(Pending, OrderEvent.Delivered, daysSincePaid: 0);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Rejected: {ex.Message}");
        }
    }
}
