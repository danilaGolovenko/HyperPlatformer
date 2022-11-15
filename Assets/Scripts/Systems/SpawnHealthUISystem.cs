using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnHealthUISystem : BaseSystem, IGlobalStart
    {
        public override void InitSystem()
        {
            
        }

        public void GlobalStart()
        {
            ShowUICommand showUICommand = new ShowUICommand();
            showUICommand.UIViewType = UIIdentifierMap.HealthUI_identifier;
            Owner.World.Command(showUICommand);
        }
    }
}