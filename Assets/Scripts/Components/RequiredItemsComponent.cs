using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class RequiredItemsComponent : BaseComponent
    {
        public List<Item> itemsList = new List<Item>();
    }
}