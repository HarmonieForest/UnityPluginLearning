/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Items.Actions.Modules.Melee
{
    using Opsive.Shared.Utility;
    using Opsive.UltimateCharacterController.Items.Actions.Impact;
    using System;
    using UnityEngine;

    public class MeleeImpactCallbackContext : ImpactCallbackContext
    {
        public MeleeAction MeleeAction { get; set; }

        /// <summary>
        /// Reset the data.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            MeleeAction = null;
        }
    }

    /// <summary>
    /// Base class for melee impact modules.
    /// </summary>
    [Serializable]
    public abstract class MeleeImpactModule : MeleeActionModule
    {
        /// <summary>
        /// Invoke actions on impact.
        /// </summary>
        /// <param name="impactCallbackContext">The impact callback data.</param>
        public abstract void OnImpact(ImpactCallbackContext impactCallbackContext);
        
        /// <summary>
        /// Reset the impact with the matching source id.
        /// </summary>
        /// <param name="sourceID">The source if od the impact to resset.</param>
        public abstract void Reset(uint sourceID);
    }
    
    /// <summary>
    /// Invoke generic impacts for melee impact.
    /// </summary>
    [Serializable]
    public class GenericMeleeImpactModule : MeleeImpactModule
    {
        [Tooltip("The attack must match the substate index to invoke (Ignore if less than 0).")]
        [SerializeField] private int m_SubstateIndex = -1;
        [Tooltip("The attack ID must match to invoke (Ignore if less than 0).")]
        [SerializeField] private int m_AttackID = -1;
        [Tooltip("The impact actions to invoke.")]
        [SerializeField] protected ImpactActionGroup m_ImpactActions  = ImpactActionGroup.DefaultDamageGroup(true);

        [Shared.Utility.NonSerialized] public ImpactActionGroup ImpactActions { get => m_ImpactActions; set => m_ImpactActions = value; }

        /// <summary>
        /// Initialize the module.
        /// </summary>
        protected override void InitializeInternal()
        {
            base.InitializeInternal();
            
            m_ImpactActions.Initialize(this);
        }

        /// <summary>
        /// Invoke actions on impact.
        /// </summary>
        /// <param name="impactCallbackContext">The impact callback data.</param>
        public override void OnImpact(ImpactCallbackContext impactCallbackContext)
        {
            if (CanInvoke(impactCallbackContext) == false) { return; }
            m_ImpactActions.OnImpact(impactCallbackContext, true);
        }

        /// <summary>
        /// Reset the impact with the matching source id.
        /// </summary>
        /// <param name="sourceID">The source if od the impact to resset.</param>
        public override void Reset(uint sourceID)
        {
            m_ImpactActions.Reset(sourceID);
        }
        
        /// <summary>
        /// Can the effect be invoked?
        /// </summary>
        /// <param name="meleeUseDataStream">The use data stream.</param>
        /// <returns>True if the conditions pass.</returns>
        private bool CanInvoke(ImpactCallbackContext meleeUseDataStream)
        {
            if (m_AttackID >= 0 && m_AttackID != MeleeAction.MeleeUseDataStream.AttackData.AttackID) { return false; }

            if (m_SubstateIndex >= 0 && m_SubstateIndex != MeleeAction.GetUseItemSubstateIndex()) { return false; }

            return true;
        }

        /// <summary>
        /// Clean up the module when it is destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            m_ImpactActions.OnDestroy();
        }

        /// <summary>
        /// Write the module name in an easy to read format for debugging.
        /// </summary>
        /// <returns>The string representation of the module.</returns>
        public override string ToString()
        {
            if (m_ImpactActions == null || m_ImpactActions.ImpactActions == null) {
                return base.ToString();
            }
            return GetToStringPrefix()+$"Generic ({m_ImpactActions.Count}): " + ListUtility.ToStringDeep(m_ImpactActions.ImpactActions, true);
            
        }
    }
}