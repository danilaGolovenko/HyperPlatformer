using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class StartInventoryComponent : BaseComponent
    {
        public List<Item> startItemsList = new List<Item>();
    }
    [Serializable]
    public struct Item {
        public ActorContainer itemContainer;
        public int amount;
    }
}