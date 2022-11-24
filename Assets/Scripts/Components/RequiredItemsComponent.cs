using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class RequiredItemsComponent : BaseComponent, IAfterEntityInit
    {
        public List<Item> requiredItemsList = new List<Item>();
        public Dictionary<int, int> requiredItemsDictionary = new Dictionary<int, int>();

        public void AfterEntityInit()
        {
            foreach (var item in requiredItemsList)
            {
                requiredItemsDictionary.Add(item.entityContainerIndex, item.amount);
            }
        }
    }
}