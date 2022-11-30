using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CharacterStateComponent : BaseComponent
    {
        public CharacterState currentState = CharacterState.Default;
    }
    
    public enum CharacterState
    {
        Default,
        Patrol,
        PrepareToAttack,
        Attack,
        MakeDecisionAfterAttack
    }
}