using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DragonCombatAnimator : CombatAnimator
{
    internal string weaponType;
    public bool animEvent = true;//
    public string eventFunctionName;
    public bool eventFired;
    public float triggertolerance,lowertolerance,num;
    float triggerTime;
    float animLength;
    float animTime;
    float normTriggerTime;
    int count;
    List<float> nums = new List<float>();

    protected override void Update()
    {
        base.Update();
       // GetEventName();
        weaponType = GetComponent<DragonCombatController>().weaponType;
    }

    protected override void PlayAnim()
    {


        for (int i = 0; i < PAnimator.CombatAnim.Count; i++)
        {
            if ((isMoving == PAnimator.CombatAnim[i].ISMOVING) &&
                isModified == PAnimator.CombatAnim[i].ISMODFIED &&
                isJumpPressed == PAnimator.CombatAnim[i].ISJUMPPRESSED &&
                isFalling == PAnimator.CombatAnim[i].ISFALLING &&
                onGround == PAnimator.CombatAnim[i].ISGROUNDED &&
                isObstacle == PAnimator.CombatAnim[i].OBSTACLEDETECT &&
                isHovermode == PAnimator.CombatAnim[i].ISHOVERMODE &&
                isFlying == PAnimator.CombatAnim[i].ISFLYING &&
                isCrouch == PAnimator.CombatAnim[i].ISCROUCHED &&
                isPrimaryAttack == PAnimator.CombatAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.CombatAnim[i].ISCOMBATMODE &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSheating == PAnimator.CombatAnim[i].ISSHEATING &&
                isDirection == PAnimator.CombatAnim[i].DIRECTIONS &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSecondaryAttack == PAnimator.CombatAnim[i].ISSECONDARYATTACK )
            {
                k = i;
                _Animancer.Animator.applyRootMotion = PAnimator.CombatAnim[i].applyRootMotion;
                if (PAnimator.CombatAnim[i].AnimClips.Count == 1)
                {
                    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >=
                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedEndTime)
                    {

                        if (!isFlying)
                        {
                            GetComponent<InputController>().isPrimaryAttack = false;
                            GetComponent<InputController>().isSheating = false;
                            animEvent = true;
                        }

                    }
                    else
                    {
                        PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
                    }

                }
                else if (PAnimator.CombatAnim[i].AnimClips.Count > 1)
                {
                    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >=
                       _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedEndTime)
                    {
                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);

                        if (j == PAnimator.CombatAnim[i].AnimClips.Count - 1)
                        {
                            GetComponent<InputController>().isPrimaryAttack = false;
                            GetComponent<InputController>().isSheating = false;
                            j = 0;
                        }
                        else
                        {
                            j = j + 1; ;
                        }
                    }
                }
                else
                {
                    PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
                }
            }
            
        }
    }
    private void GetEventName()
    {
        
        for (int i = 0; i < PAnimator.CombatAnim.Count; i++)
        {

            if ((isMoving == PAnimator.CombatAnim[i].ISMOVING) &&
                isModified == PAnimator.CombatAnim[i].ISMODFIED &&
                isJumpPressed == PAnimator.CombatAnim[i].ISJUMPPRESSED &&
                isFalling == PAnimator.CombatAnim[i].ISFALLING &&
                onGround == PAnimator.CombatAnim[i].ISGROUNDED &&
                isObstacle == PAnimator.CombatAnim[i].OBSTACLEDETECT &&
                isHovermode == PAnimator.CombatAnim[i].ISHOVERMODE &&
                isFlying == PAnimator.CombatAnim[i].ISFLYING &&
                isCrouch == PAnimator.CombatAnim[i].ISCROUCHED &&
                isPrimaryAttack == PAnimator.CombatAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.CombatAnim[i].ISCOMBATMODE &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSheating == PAnimator.CombatAnim[i].ISSHEATING &&
                isDirection == PAnimator.CombatAnim[i].DIRECTIONS &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSecondaryAttack == PAnimator.CombatAnim[i].ISSECONDARYATTACK
                )
            {

                if (PAnimator.CombatAnim[i].AnimClips[0].Clip.events.Length == 0)
                {
                    return;
                }
                else
                {
                    if (PAnimator.CombatAnim[i].AnimClips.Count == 1)
                    {
                        triggerTime = PAnimator.CombatAnim[i].AnimClips[j].Clip.events[0].time;
                        animLength = PAnimator.CombatAnim[i].AnimClips[j].Clip.length;
                        var state = _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
                        animTime = state.NormalizedTime%1;
                        normTriggerTime = triggerTime / PAnimator.CombatAnim[i].AnimClips[j].Clip.length;
                        eventFunctionName = PAnimator.CombatAnim[i].AnimClips[0].Clip.events[0].functionName;
                        if (eventFired == false)
                        {
                            num = (animTime - normTriggerTime);
                        }

                        if (num <= triggertolerance && num >= lowertolerance) // may have to add upper and lower limit

                        {
                            eventFired = true;
                            num = 1;
                        }
                        else
                        {
                            eventFired = false;
                        }

                    }

                    if (PAnimator.CombatAnim[i].AnimClips.Count > 1)
                    {
                        if (j == PAnimator.CombatAnim[i].AnimClips.Count)
                        {
                            j = 0;
                        }
                        if(PAnimator.CombatAnim[i].AnimClips[j].Clip.events.Length == 0)
                        {
                            return;
                        }
                        else
                        {
                            triggerTime = PAnimator.CombatAnim[i].AnimClips[j].Clip.events[0].time;
                        }                        
                        animLength = PAnimator.CombatAnim[i].AnimClips[j].Clip.length;
                        var state = _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
                        animTime = state.NormalizedTime % 1;
                        normTriggerTime = triggerTime / PAnimator.CombatAnim[i].AnimClips[j].Clip.length;
                        eventFunctionName = PAnimator.CombatAnim[i].AnimClips[0].Clip.events[0].functionName;
                        if(eventFired == false)
                        {
                           num  = (animTime - normTriggerTime);
                        }
                        
                        if (num <= triggertolerance && num >= lowertolerance)// may have to add upper and lower limit
                        {
                            Debug.Log(Mathf.Abs(animTime - normTriggerTime));
                            eventFired = true;
                            num = 1;
                        }
                        else
                        {
                            eventFired = false;
                        }
                    }
                }
            }
        }
    }

}
