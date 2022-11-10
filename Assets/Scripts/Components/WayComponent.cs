using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using Components.MonoBehaviourComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WayComponent : BaseComponent, IHaveActor, IInitable
    {
        public IActor Actor { get; set; }
        [ReadOnly]
        public WayMonoComponent wayMonoComponent;
        [ReadOnly]
        public List<Vector2> listOfPoints = new List<Vector2>();

        public void Init()
        {
            if (Actor != null)
                wayMonoComponent = Actor.GameObject.GetComponent<WayMonoComponent>();
            else
                Actor.RemoveHecsComponent(this);
            if (wayMonoComponent != null && wayMonoComponent.wayTransforms != null)
            {
                foreach (var transform in wayMonoComponent.wayTransforms)
                {
                    listOfPoints.Add(transform.position);
                }
            }
        }
    }
}