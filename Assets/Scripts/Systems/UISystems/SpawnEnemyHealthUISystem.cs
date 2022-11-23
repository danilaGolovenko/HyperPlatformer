using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnEnemyHealthUISystem : BaseSystem, IReactComponentGlobal<HealthBarTagComponent>
    {
        private ConcurrencyList<HealthBarTagComponent> healthBars = new ConcurrencyList<HealthBarTagComponent>();
        public override void InitSystem()
        {
        }
        
        private void ReactOnUI(IEntity ui, IEntity needHealthBar)
        {
            ui.GetEntityHolderComponent().entity = needHealthBar;
            healthBars.Add(needHealthBar.GetHealthBarTagComponent());
            InitEnemyHealthBarCommand initEnemyHealthBarCommand = new InitEnemyHealthBarCommand();
            ui.Command(initEnemyHealthBarCommand);
            needHealthBar.GetHealthBarTagComponent().HealthBar = ui;
        }

        public void ComponentReactGlobal(HealthBarTagComponent component, bool isAdded)
        {
            if (isAdded)
            {
                ShowUIOnAdditionalCommand showUICommand = new ShowUIOnAdditionalCommand();
                showUICommand.UIViewType = UIIdentifierMap.EnemyHealthBarUI_identifier;
                showUICommand.AdditionalCanvasID = AdditionalCanvasIdentifierMap.AdditionalCanvas_identifier;
                showUICommand.MultyView = true;
                showUICommand.OnUILoad += (x) => ReactOnUI(x, component.Owner);
                Owner.World.Command(showUICommand);
            }
            else
            {
                component.HealthBar?.Command(new HideUICommand());
            }
        }
    }
}