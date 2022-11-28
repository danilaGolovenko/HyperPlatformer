using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HECSFramework.Unity.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class ItemsOnSceneComponent : BaseComponent, IWorldSingleComponent
    {
        public List<ItemOnScene> ItemsOnScene;
    }

    [Serializable]
    public struct ItemOnScene {
        // todo разобраться, почему оно работает вообще :):):)
        [ValueDropdown("DropSceneIdentifiers")]public int SceneId;
        public Vector3 Position;
        public EntityContainer EntityContainer;

        public IEnumerable DropSceneIdentifiers()
        {
            var sceneIdentifiers = new SOProvider<SceneIdentifier>().GetCollection();
            var list = new ValueDropdownList<int>();
            foreach (var id in sceneIdentifiers)
            {
                list.Add(new ValueDropdownItem<int>(id.name, id.Id));
            }
            return list;
        }
    }
    
    
}