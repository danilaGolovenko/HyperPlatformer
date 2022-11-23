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
                if (scene.SceneIdentifier.Id == sceneId)
                {
                    return scene.Scene;
                }
            }
            return null;
        }
    }
    [Serializable]
    public struct IdToScene
    {
        public SceneIdentifier SceneIdentifier;
        public AssetReference Scene;
    }
}