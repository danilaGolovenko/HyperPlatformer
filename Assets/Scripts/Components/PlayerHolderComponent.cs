using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerHolderComponent : BaseComponent, IInitable
    {
        public IEntity PlayerEntity;
        public void Init()
        {
            PlayerEntity = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
        }
    }
}