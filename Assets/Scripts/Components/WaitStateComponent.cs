using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;
namespace Components
{
    [Serializable]
    [Documentation(Doc.Strategy,  "required by WaitState for delay")]
    public sealed class WaitStateComponent : BaseComponent
    {
        public float CurrentWaitTimer = 1f;
    }
}