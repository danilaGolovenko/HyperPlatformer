using HECSFramework.Core;
using UnityEngine;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct RespawnCommand : ICommand
	{
		public Vector2 spawnPoint;
	}
}