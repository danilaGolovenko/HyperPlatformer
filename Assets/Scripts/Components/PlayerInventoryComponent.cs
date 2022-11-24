using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerInventoryComponent : BaseComponent, IAfterEntityInit
    {
        public List<Item> StartItemsList = new List<Item>();
        public Dictionary<int, int> currentItems = new Dictionary<int, int>();

        public void AfterEntityInit()
        {
            foreach (var item in StartItemsList)
            {
                currentItems.Add(item.entityContainerIndex, item.amount);
            }
        }
    }

    [Serializable]
    public struct Item {
        // todo как хранить entityContainerIndex, чтобы его было удобно назначать в инспекторе? +  чтобы удобно отображать в UI
        public int entityContainerIndex;
        public int amount;
    }
}