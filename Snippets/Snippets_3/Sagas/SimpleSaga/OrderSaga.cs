﻿namespace Snippets3.Sagas.SimpleSaga
{
    using NServiceBus;
    using NServiceBus.Saga;

    #region 3to4ConfigureHowToFindSaga

    public class OrderSaga : Saga<OrderSagaData>,
                            IAmStartedByMessages<StartOrder>,
                            IHandleMessages<CompleteOrder>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<CompleteOrder>(s => s.OrderId, m => m.OrderId);
        }

    #endregion
        public void Handle(StartOrder message)
        {
            Data.OrderId = message.OrderId;
        }

        public void Handle(CompleteOrder message)
        {
            // code to handle order completion
            MarkAsComplete();
        }
    }


}
