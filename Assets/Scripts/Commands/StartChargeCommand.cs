using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct StartChargeCommand : ICommand
	{
		public float GoalPointX;
		public float SpeedCoeff;
	}
}