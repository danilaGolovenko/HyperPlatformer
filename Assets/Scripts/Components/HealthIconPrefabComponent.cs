using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class HealthIconPrefabComponent : BaseComponent
    {
        [field: SerializeField] public GameObject healthIconPrefab { get; private set; }
    }
}