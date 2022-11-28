using HECSFramework.Core;
using HECSFramework.Unity;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct LoadedSceneCommand : ICommand, IGlobalCommand
	{
		public SceneIdentifier SceneIdentifier;
	}
}