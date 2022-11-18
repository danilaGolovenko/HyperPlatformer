using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using Components.MonoBehaviourComponents;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnPointComponent : BaseComponent, IHaveActor, IInitable
    {
        public IActor Actor { get; set; }
        [ReadOnly]
        public Transform spawnPointTransform;

        public void Init()
        {
            if (Actor != null)
            {
                var monoComponent = Actor.GameObject.GetComponent<SpawnPointMonoComponent>();
                spawnPointTransform = monoComponent.SpawnPointTransform;
            }
            else
                Actor.RemoveHecsComponent(this);
        }
    }
}