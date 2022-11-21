using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct SwitchSceneCommand : ICommand, IGlobalCommand
	{
		public int targetSceneIndex;
	}
}