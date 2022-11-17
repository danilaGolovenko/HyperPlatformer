using System;
using Cinemachine;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class InitializationCameraFollowSystem : BaseSystem, ILateStart
    {
        private ConcurrencyList<IEntity> mainCameras;
        private ConcurrencyList<IEntity> players;

        public override void InitSystem()
        {
            mainCameras = Owner.World.Filter(new FilterMask(HMasks.CMPlayerCamTagComponent));
            players = Owner.World.Filter(HMasks.PlayerTagComponent);
            
        }

        public void LateStart()
        {
            //Data - сам список
            IEntity camera = mainCameras.Data[0];
            IEntity player = players.Data[0];
            UnityTransformComponent playerTransformComponent;
            // playerTransformComponent = player.GetOrAddComponent<UnityTransformComponent>(player);
            playerTransformComponent = player.GetUnityTransformComponent();
            // playerTransformComponent = player.GetHECSComponent<UnityTransformComponent>();
            // player.TryGetHecsComponent(HMasks.UnityTransformComponent, out playerTransformComponent);
            // camera.Transform = playerTransform;

            // var cameraActor = camera as IActor;
            if (camera is IActor cameraActor)
            {
                cameraActor.TryGetComponent(out CinemachineVirtualCamera virtualCamera);
                virtualCamera.Follow = playerTransformComponent.Transform;
            }
        }
    }
}