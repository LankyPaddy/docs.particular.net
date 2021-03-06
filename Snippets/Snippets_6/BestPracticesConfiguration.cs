﻿namespace Snippets6
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Features;

    public class BestPracticesConfiguration
    {
        void DisableFeature()
        {
            #region DisableBestPracticeEnforcementPerEndpoint
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.DisableFeature<BestPracticeEnforcement>();
            #endregion
        }

        async Task DisablePerMessage()
        {
            IBus bus = null;

            #region DisableBestPracticeEnforcementPerMessage
            var options = new SendOptions();

            options.DoNotEnforceBestPractices();

            await bus.SendAsync(new MyEvent(), options);
            #endregion
        }

        class MyEvent
        {
        }
    }
}
