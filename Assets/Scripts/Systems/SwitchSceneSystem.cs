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
        // [Required] private SpawnPointComponent spawnPointComponent;
        [Required] private SceneHolderComponent sceneHolderComponent;
        private SceneInstance currentScene;
        // todo нужно где-то кешировать Transform игрока до переключения сцены, чтобы при возвращении на эту локацию он появлялся в нужном месте
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

            var sceneReference = sceneHolderComponent.GetScene(command.TargetSceneId);
            if (sceneReference == null)
            {
                Debug.LogError("Нет сцены для " + command.TargetSceneId);
                return;
            }
            var result = await Addressables.LoadSceneAsync(sceneHolderComponent.GetScene(command.TargetSceneId), LoadSceneMode.Additive).Task;
           
            currentScene = result;
            
            LoadedSceneCommand loadedSceneCommand = new LoadedSceneCommand();
            Owner.World.Command(loadedSceneCommand);
            
        }
        public async void GlobalStart()
        {
            var result = await Addressables.LoadSceneAsync(sceneHolderComponent.GetScene(SceneIdentifierMap.MainLocation_identifier), LoadSceneMode.Additive).Task;
            currentScene = result;
            
            LoadedSceneCommand loadedSceneCommand = new LoadedSceneCommand();
            Owner.World.Command(loadedSceneCommand);
        }
    }
}