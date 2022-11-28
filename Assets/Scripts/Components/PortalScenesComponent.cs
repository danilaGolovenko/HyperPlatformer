using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PortalScenesComponent : BaseComponent
    {
        [field: SerializeField] public SceneIdentifier TargetSceneIdentifier{ get; private set; }
        [field: SerializeField] public SceneIdentifier CurrentSceneIdentifier{ get; private set; }
    }
}