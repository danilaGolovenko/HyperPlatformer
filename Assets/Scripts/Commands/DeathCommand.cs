using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct DeathCommand : ICommand, IGlobalCommand
	{
		public IEntity entity;
	}
}