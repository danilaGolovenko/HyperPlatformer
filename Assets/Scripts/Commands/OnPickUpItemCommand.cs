using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct OnPickUpItemCommand : ICommand, IGlobalCommand
	{
		public IEntity Item;
	}
}