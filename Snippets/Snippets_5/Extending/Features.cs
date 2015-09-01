﻿using NServiceBus;
using NServiceBus.Features;

namespace Snippets5.Extending
{
    #region MinimalFeature
    public class MinimalFeature : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {

        }
    }
    #endregion

    public class ComponentRegistrationFeature : Feature
    {
        #region ComponentRegistrationFeature
        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Container.ConfigureComponent<Component>(DependencyLifecycle.InstancePerCall);
            context.Container.ConfigureComponent(
                c => new OtherComponent(c.Build<IServiceA>(), 42, "Hello NServiceBus!"),
                DependencyLifecycle.InstancePerCall);
        }

        interface IServiceA
        {
        }

        interface IServiceB
        {
        }

        class Component : IServiceA, IServiceB
        {

        }
        #endregion

        class OtherComponent
        {
            public OtherComponent(IServiceA serviceA, int a, string b)
            {
            }
        }
    }

    public class DependencyAFeature : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }

    #region DependentFeature
    public class DependentFeature : Feature
    {
        public DependentFeature()
        {
            DependsOn<DependencyAFeature>();
            DependsOn("DependencyB");
            DependsOnAtLeastOne("DependencyC", "DependencyD");
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }
    #endregion

    #region FeatureEnabledByDefault
    public class FeatureEnabledByDefault : Feature
    {
        public FeatureEnabledByDefault()
        {
            EnableByDefault();
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }
    #endregion

    #region FeatureWithDefaults
    public class FeatureWithDefaults : Feature
    {
        public FeatureWithDefaults()
        {
            Defaults(s =>
            {
                s.Set("Key", "Value");
                s.SetDefault("OtherKey", 42);
            });
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }
    #endregion

    public class EnablingOtherFeatures : Feature
    {
        public EnablingOtherFeatures()
        {
            #region EnablingOtherFeatures
            Defaults(s => s.EnableFeatureByDefault<OtherFeature>());
            #endregion
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
        }

        private class OtherFeature : Feature
        {
            protected override void Setup(FeatureConfigurationContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }

    public class FeatureWithPrerequisites : Feature
    {
        #region FeatureWithPrerequisites
        public FeatureWithPrerequisites()
        {
            Prerequisite(c => c.Settings.HasExplicitValue("SomeKey"),"The key SomeKey was not present.");
        }
        #endregion

        protected override void Setup(FeatureConfigurationContext context)
        {
        }
    }
}