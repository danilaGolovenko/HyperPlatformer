using HECSFramework.Core;
using Strategies;
using UnityEngine;

[Documentation(Doc.Strategy, Doc.State, Doc.AI, "Нода которая выясняет - закончился ли интервал ожидания или нет")]
public class IsWaitStateComplete : DilemmaDecision
{
    public override string TitleOfNode { get; } = "Is wait state complete?";

    protected override void Run(IEntity entity)
    {
        var waitStateComponent = entity.GetWaitStateComponent();

        if (waitStateComponent.CurrentWaitTimer <= 0)
            Positive.Execute(entity);
        else
        {
            waitStateComponent.CurrentWaitTimer -= Time.deltaTime;
            Negative.Execute(entity);
        }
    }
}

