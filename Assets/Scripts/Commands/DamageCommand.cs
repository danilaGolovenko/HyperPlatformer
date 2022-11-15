using HECSFramework.Core;
using HECSFramework.Unity;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct DamageCommand : ICommand
	{
		public int amount;
		public IEntity authorEntity;
	}
}