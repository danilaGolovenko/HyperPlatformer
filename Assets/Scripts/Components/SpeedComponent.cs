using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class SpeedComponent : BaseComponent
    {
        [field: SerializeField] public float Speed { get; private set; } = 2;
    }
}