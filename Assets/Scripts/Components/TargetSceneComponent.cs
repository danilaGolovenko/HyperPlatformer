using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class TargetSceneComponent : BaseComponent
    {
        [field: SerializeField] public SceneIdentifier SceneIdentifier{ get; private set; }
    }
}