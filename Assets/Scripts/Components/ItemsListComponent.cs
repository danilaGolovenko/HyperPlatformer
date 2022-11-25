using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class ItemsListComponent : BaseComponent, IWorldSingleComponent
    {
        [SerializeField]  public List<EntityContainer> items;
    }
}