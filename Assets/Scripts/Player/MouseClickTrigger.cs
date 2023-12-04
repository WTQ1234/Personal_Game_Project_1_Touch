using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class MouseClickTrigger : MonoBehaviour
{
    public ParticleSystem CurParticleSystem_White;
    public ParticleSystem CurParticleSystem_Yellow;
    public ParticleSystem CurParticleSystem_Red;

    public AudioClip sfx_white;
    public AudioClip sfx_yellow;
    public AudioClip sfx_red;

    private bool is_trigger = false;

    private void OnEnable()
    {
        TimerManager.Instance.AddTimer(SetDisable, 0.1f);
        is_trigger = false;
        CurParticleSystem_White.transform.position = transform.position;
        CurParticleSystem_Yellow.transform.position = transform.position;
        CurParticleSystem_Red.transform.position = transform.position;
    }

    private void SetDisable()
    {
        gameObject.SetActive(false);
        if (!is_trigger)
        {
            CurParticleSystem_White.Play();
        }
        CurParticleSystem_White.transform.position = transform.position;
        CurParticleSystem_Yellow.transform.position = transform.position;
        CurParticleSystem_Red.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Obstacle")
            {
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                if (obstacle)
                {
                    obstacle.OnClick();
                    ObstacleInfo obstacleInfo = obstacle.obstacleInfo;
                    switch(obstacleInfo.obstacleType)
                    {
                        case ObstacleType.Green:
                            CurParticleSystem_White.Play();
                            break;
                        case ObstacleType.Yellow:
                            CurParticleSystem_Yellow.Play();
                            break;
                        case ObstacleType.Red:
                            CurParticleSystem_Red.Play();
                            break;
                    }
                    if (obstacleInfo.OnClick == null)
                    {
                        switch (obstacleInfo.obstacleType)
                        {
                            case ObstacleType.Green:
                                SoundManager.Instance.PlaySfx(sfx_white);
                                break;
                            case ObstacleType.Yellow:
                                SoundManager.Instance.PlaySfx(sfx_yellow);
                                break;
                            case ObstacleType.Red:
                                SoundManager.Instance.PlaySfx(sfx_red);
                                break;
                        }
                    }
                    is_trigger = true;
                }
            }
        }
    }
}
