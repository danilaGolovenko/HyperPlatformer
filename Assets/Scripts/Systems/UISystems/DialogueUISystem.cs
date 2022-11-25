using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using UnityEngine.UI;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class DialogueUISystem : BaseSystem, IReactCommand<Commands.InitDialogueUITextCommand>, IHaveActor
    {
        private Text text;
        public override void InitSystem()
        {
             text = Actor.GameObject.GetComponentInChildren<Text>();
        }

        public void CommandReact(InitDialogueUITextCommand command)
        {
            text.text = command.DialogueText;
        }
        public IActor Actor { get; set; }
    }
}