using System;
using System.Linq;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class ItemsSpawnSystem : BaseSystem, IReactGlobalCommand<LoadedSceneCommand>
    {
        [Required] public ItemsOnSceneComponent itemsOnSceneComponent;
        public override void InitSystem()
        {
        }

        public async void CommandGlobalReact(LoadedSceneCommand command)
        {
            var sceneId = Owner.World.GetSingleComponent<CurrentSceneIdentifierComponent>().SceneId;
            foreach (var itemOnScene in itemsOnSceneComponent.ItemsOnScene.Where(itemOnScene => itemOnScene.SceneId == sceneId))
            {
                var actor = await itemOnScene.EntityContainer.GetActor(true, null,
                    itemOnScene.Position);
                actor.Init();
            }
        }
    }
}