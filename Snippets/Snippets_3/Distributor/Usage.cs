﻿namespace Snippets3.Distributor
{
    using NServiceBus;

    class Usage
    {
        public Usage()
        {
            Configure configure = Configure.With();
            #region ConfiguringDistributor

            // --------------------------------------
            // Running the Distributor and a Worker
            configure.AsMasterNode();
            //or 
            configure.RunDistributor();
            // --------------------------------------

            // --------------------------------------
            // Running the Distributor only
            configure.RunDistributorWithNoWorkerOnItsEndpoint();
            //or
            configure.RunDistributor(false);
            // --------------------------------------

            #endregion
            #region ConfiguringWorker
            configure.EnlistWithDistributor();
            #endregion
        }
    }
}