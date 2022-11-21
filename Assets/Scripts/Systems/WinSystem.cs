using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WinSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>
    {
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) &&
                actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent))
            {
                if (actor.GetWinPointsComponent().currentAmount.CurrentValue >= actor.GetWinPointsComponent().requiredAmount)
                {
                    Debug.Log("GAME OVER! WIN!");
                }
            }
        }
    }
}