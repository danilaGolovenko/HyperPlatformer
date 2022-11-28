using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct SwitchSceneCommand : ICommand, IGlobalCommand
	{
		public SceneIdentifier TargetSceneId;
		public SceneIdentifier CurrentSceneId;
		public Vector3 PortalSpawnPointPosition;
	}
}