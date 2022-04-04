using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private SoundManager()
    {
        Instance = this;
    }

    [SerializeField]
    AudioSource hit, upgrade, explosion, ice, shoot, electro,soundtrack;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            hit.mute = !hit.mute;
            upgrade.mute = !upgrade.mute;
            explosion.mute = !explosion.mute;
            ice.mute = !ice.mute;
            shoot.mute = !shoot.mute;
            electro.mute = !electro.mute;
            soundtrack.mute = !soundtrack.mute;
        }
    }
    public void PlayHit()
    {
        hit.Play();
    }
    public void PlayExplosion()
    {
        explosion.Play();
    }
    public void PlayIce()
    {
        ice.Play();
    }
    public void PlayShoot()
    {
        shoot.Play();
    }
    public void PlayUpgrade()
    {
        upgrade.Play();
    }
    public void PlayElectro()
    {
        electro.Play();
    }
}
