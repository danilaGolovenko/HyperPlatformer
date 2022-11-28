using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class AfterPortalSpawnPointsComponent : BaseComponent
    {
        public Dictionary<SceneIdentifier, Vector3> SpawnPoints = new Dictionary<SceneIdentifier, Vector3>();
    }
}