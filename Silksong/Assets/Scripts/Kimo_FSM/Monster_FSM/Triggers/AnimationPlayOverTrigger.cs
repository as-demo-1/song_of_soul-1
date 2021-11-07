using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayOverTrigger :EnemyFSMBaseTrigger
{
    AnimatorStateInfo info;
    public override bool IsTriggerReach(EnemyFSMManager fsm_Manager)
    {

        if (fsm_Manager.animator != null)
         {
             info = fsm_Manager.animator.GetCurrentAnimatorStateInfo(0);
             //Debug.Log(info.length);
             if (info.normalizedTime >= 0.99)
                 return true;
         }
        return false;
    }
}
