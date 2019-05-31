using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource[] inGameSounds;

	// Use this for initialization
	void Start () {
        inGameSounds = GetComponents<AudioSource>();
		
	}
    
    public void playShellExplosion()
    {
        inGameSounds[0].Play();
    }

    public void playEngineDriving()
    {
        inGameSounds[1].Play();
    }

    public void playEngineIdling()
    {
        inGameSounds[2].Play();
    }

    public void playShellFiring()
    {
        inGameSounds[3].Play();
    }

    public void playShotCharing()
    {
        inGameSounds[4].Play();
    }

    public void playEngineIdle()
    {
        inGameSounds[2].Play();
    }

    public AudioSource getEngineIdle()
    {
        return inGameSounds[2];
    }

    public AudioSource getEngineDriving()
    {
        return inGameSounds[1];
    }

    public void playEngineIdleLoop()
    {
        if (!inGameSounds[2].loop)
        {
            inGameSounds[2].loop = true;
            inGameSounds[2].Play();
        }
    }

    public void stopEngineIdleLoop()
    {
        inGameSounds[2].loop = false;
        inGameSounds[2].Stop();
    }

    public void playEngineDrive()
    {
        if (!inGameSounds[1].loop)
        {
            inGameSounds[1].loop = true;
            inGameSounds[1].Play();
        }
    }

    public void stopEngineDrive()
    {
        if (!inGameSounds[1].loop)
        {
            inGameSounds[1].loop = false;
            inGameSounds[1].Stop();
        }
    }

}
