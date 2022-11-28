using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SceneHolderComponent : BaseComponent
    {
        [SerializeField] private List<IdToScene> scenes;

        public AssetReference GetScene(int sceneId)
        {
            foreach (var scene in scenes)
            {
                if (scene.sceneIdentifier.Id == sceneId)
                {
                    return scene.scene;
                }
            }
            return null;
        }
        
        public SceneIdentifier GetSceneIdentifier(AssetReference scene)
        {
            foreach (var idToScene in scenes)
            {
                if (idToScene.scene == scene)
                {
                    return idToScene.sceneIdentifier;
                }
            }
            return null;
        }
    }
    [Serializable]
    public struct IdToScene
    {
        public SceneIdentifier sceneIdentifier;
        public AssetReference scene;
    }
}