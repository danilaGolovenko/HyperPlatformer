using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SwitchSceneSystem : BaseSystem, IReactGlobalCommand<Commands.SwitchSceneCommand>, IGlobalStart
    {
        [Required] private SceneHolderComponent sceneHolderComponent;
        [Required] private CurrentSceneIdentifierComponent currentSceneIdentifierComponent;
        private SceneInstance currentScene;
        public override void InitSystem()
        {
        }
        public async void CommandGlobalReact(SwitchSceneCommand command)
        {
            var oldObjects = Owner.World.Filter(HMasks.AdditionalSceneObjectTagComponent).ToArray();
            foreach (var oldObject in oldObjects)
            {
                oldObject.HecsDestroy();
            }
            Addressables.UnloadSceneAsync(currentScene);
            
            if (command.PortalSpawnPointPosition != Vector3.zero)
            {
                var player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
                if (player.GetAfterPortalSpawnPointsComponent().SpawnPoints.ContainsKey(command.CurrentSceneId))
                {
                    player.GetAfterPortalSpawnPointsComponent().SpawnPoints[command.CurrentSceneId] = command.PortalSpawnPointPosition;
                }
                else
                {
                    player.GetAfterPortalSpawnPointsComponent().SpawnPoints
                    .Add(command.CurrentSceneId, command.PortalSpawnPointPosition);
                }
            }

            var sceneReference = sceneHolderComponent.GetScene(command.TargetSceneId.Id);
            if (sceneReference == null)
            {
                Debug.LogError("Нет сцены для " + command.TargetSceneId);
                return;
            }
            var result = await Addressables.LoadSceneAsync(sceneHolderComponent.GetScene(command.TargetSceneId.Id), LoadSceneMode.Additive).Task;
            currentScene = result;
            currentSceneIdentifierComponent.SceneId = command.TargetSceneId.Id;
            
            var loadedSceneCommand = new LoadedSceneCommand()
            {
                SceneIdentifier = command.TargetSceneId
            };
            Owner.World.Command(loadedSceneCommand);
        }
        
        public async void GlobalStart()
        {
            var result = await Addressables.LoadSceneAsync(sceneHolderComponent.GetScene(SceneIdentifierMap.MainLocation_identifier), LoadSceneMode.Additive).Task;
            currentScene = result;
            currentSceneIdentifierComponent.SceneId = SceneIdentifierMap.MainLocation_identifier;
            Owner.World.Command(new LoadedSceneCommand());
        }
    }
}