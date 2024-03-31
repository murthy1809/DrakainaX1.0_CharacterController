using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoundClips : MonoBehaviour
{
    public List<SoundObjects> FootstepSounds = new List<SoundObjects>();
    public List<SoundObjects> Lizardsounds = new List<SoundObjects>();
    public List<SoundObjects> MeleeSounds = new List<SoundObjects>();
    public List<SoundObjects> FireSounds = new List<SoundObjects>();
    DragonSoundManager manager;
    InputController input;
    public string audioGroup;
    private AudioClip audioClip, clip;
    DragonCombatAnimator animator;
    bool hasSoundPlayed = false;
   // public bool isMetalHit;
    private void Awake()
    {    
        manager = GetComponent<DragonSoundManager>();
        input = GetComponent<InputController>();
        animator = GetComponent<DragonCombatAnimator>();
    }


    private void Update()
    {
        PlayDragonSounds();
        PlayMeleeSounds();
        PlayFireSounds();
        
    }

    void Landing()
    {
        for (int i = 0; i < Lizardsounds.Count; i++)
        {
            if (animator.eventFunctionName == Lizardsounds[i].AudioGroup)
            {
                audioGroup = Lizardsounds[i].AudioGroup;
                audioClip = Lizardsounds[i].Clips[Random.Range(0, Lizardsounds[i].Clips.Length)];
                manager.DragonSounds(audioClip);
            }
            else
            {
                audioClip = null;
            }
        }
    }

    public void PlaySound()
    {
        hasSoundPlayed = true;
    }

  
    protected virtual AudioClip PlayDragonSounds()
    {
       
        for (int i = 0; i < Lizardsounds.Count; i++)
        {
           
            if (animator.eventFunctionName == Lizardsounds[i].AudioGroup &&
               hasSoundPlayed)
            {
                audioGroup = Lizardsounds[i].AudioGroup;
                audioClip = Lizardsounds[i].Clips[Random.Range(0, Lizardsounds[i].Clips.Length)];
                manager.DragonSounds(audioClip);
                hasSoundPlayed = false;
            }
            else
            {
                audioClip = null;
                hasSoundPlayed = false;
            }
        }
        
        return audioClip;
    }

    public bool HasSoundPlayed()
    {
        return hasSoundPlayed;
    }

    // Method to reset the hasSoundPlayed boolean
    public void ResetSoundPlayed()
    {
        hasSoundPlayed = false;
    }


    protected virtual AudioClip PlayMeleeSounds()
    {
        for (int i = 0; i < MeleeSounds.Count; i++)
        {
            if (animator.eventFunctionName == MeleeSounds[i].AudioGroup &&
                animator.eventFired)
            {
                audioGroup = MeleeSounds[i].AudioGroup;
                audioClip = MeleeSounds[i].Clips[Random.Range(0, MeleeSounds[i].Clips.Length)];
                manager.MeleeSounds(audioClip);                
            }
            else
            {
                audioClip = null;
            }

        }
        return audioClip;
    }
    protected virtual AudioClip PlayFireSounds()
    {
        if (input.isCombatMode)
        {
            for (int i = 0; i < FireSounds.Count; i++)
            {
                if (FireSounds[i].AudioGroup == animator.eventFunctionName &&
                    animator.eventFired)
                {
                    audioGroup = FireSounds[i].AudioGroup;
                    audioClip = FireSounds[i].Clips[Random.Range(0, FireSounds[i].Clips.Length)];
                    manager.MeleeSounds(audioClip);
                    Debug.Log(audioClip);
                }
                else
                {
                    audioClip = null;
                }
            }
        }
        else
        {
            audioClip = FireSounds[0].Clips[Random.Range(0, FireSounds[0].Clips.Length)];
        }

        return audioClip;
    }
    protected virtual void FootSteps()
    {
        audioClip = GetFootStepClip();
        manager.FootStepSound(audioClip);

    }
    protected virtual AudioClip GetFootStepClip()
    {
        return FootstepSounds[0].Clips[Random.Range(0, FootstepSounds[0].Clips.Length)];
    }

   

}
