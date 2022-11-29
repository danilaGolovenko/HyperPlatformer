using System;
using System.Collections;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class RemoveEntityAfterDelaySystem : BaseSystem, IAfterEntityInit
    {
        [Required] private WaitComponent waitComponent;
        public override void InitSystem()
        {
        }
        
        private IEnumerator RemoveAfterDelay(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            Owner.HecsDestroy();
        }

        public void AfterEntityInit()
        {
            var waitTime = waitComponent.waitMonoComponent.waitTime;
            waitComponent.waitMonoComponent.StartCoroutine(RemoveAfterDelay(waitTime));
        }
    }
}