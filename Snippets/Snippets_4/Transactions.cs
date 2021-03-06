﻿namespace Snippets4
{
    using System;
    using System.Transactions;
    using NServiceBus;

    public class Transactions
    {
        public void Unreliable()
        {
            #region TransactionsDisable
            Configure.Transactions.Disable();
            #endregion
        }

        public void TransportTransactions()
        {
            #region TransactionsDisableDistributedTransactions
            Configure.Transactions.Advanced(x => x.DisableDistributedTransactions());
            #endregion

        }

        public void AmbientTransactions()
        {
            #region TransactionsEnable
            Configure.Transactions.Enable().Advanced(x => x.EnableDistributedTransactions());
            #endregion

            #region TransactionsDoNotWrapHandlersExecutionInATransactionScope
            Configure.Transactions.Advanced(x => x.DoNotWrapHandlersExecutionInATransactionScope());
            #endregion
        }

        public void CustomTransactionTimeout()
        {
            #region CustomTransactionTimeout
            Configure.Transactions.Advanced(x => x.DefaultTimeout(TimeSpan.FromSeconds(30)));
            #endregion
        }

        public void CustomTransactionIsolationLevel()
        {
            #region CustomTransactionIsolationLevel
            Configure.Transactions.Advanced(x => x.IsolationLevel(IsolationLevel.RepeatableRead));
            #endregion
        }
    }
}