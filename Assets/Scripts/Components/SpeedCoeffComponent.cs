using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class SpeedCoeffComponent : BaseComponent
    {
        [field: SerializeField] public float coefficient { get; set; } = 2;
        [field: SerializeField] public float defaultCoefficient { get; private set; } = 2;
    }
}