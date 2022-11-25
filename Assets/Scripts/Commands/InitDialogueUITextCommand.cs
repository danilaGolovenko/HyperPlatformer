using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
	public struct InitDialogueUITextCommand : ICommand
	{
		public string DialogueText;
	}
}