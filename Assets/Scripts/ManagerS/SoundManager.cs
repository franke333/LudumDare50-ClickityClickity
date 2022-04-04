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
    AudioSource hit, upgrade, explosion, ice, shoot, electro;

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
