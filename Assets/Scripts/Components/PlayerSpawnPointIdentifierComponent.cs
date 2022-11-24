using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerSpawnPointIdentifierComponent : BaseComponent
    {
       [field: SerializeField] public PlayerSpawnPointIdentifier PlayerSpawnPointIdentifier { get; private set; }
    }
}