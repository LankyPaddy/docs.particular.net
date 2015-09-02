using System;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;

#region incoming-header-behavior
class IncomingHeaderBehavior : Behavior<TransportReceiveContext>
{
    public override void Invoke(TransportReceiveContext context, Action next)
    {
        context.GetPhysicalMessage()
            .Headers
            .Add("IncomingHeaderBehavior", "ValueIncomingHeaderBehavior");
        next();
    }
}

#endregion