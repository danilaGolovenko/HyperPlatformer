using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class SpeedCoeffComponent : BaseComponent
    {
        [field: SerializeField] public float coefficient { get; private set; } = 3;
    }
}