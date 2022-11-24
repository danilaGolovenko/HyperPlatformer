using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class DrawPickUpUISystem : BaseSystem, IReactGlobalCommand<Commands.OnPickUpItemCommand>
    {
        public override void InitSystem()
        {
        }
        
        public void CommandGlobalReact(OnPickUpItemCommand command)
        {
            // todo отрисовка сообщения "вы взяли такую-то вещь"
        }
    }
}