using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerInventoryComponent : BaseComponent
    {
        public Dictionary<int, int> currentItems = new Dictionary<int, int>();
    }
}