using HECSFramework.Core;
using HECSFramework.Unity;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct DamageCommand : ICommand
	{
		public float amount;
		public IEntity authorEntity;
	}
}