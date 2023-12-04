using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanDetector : MonoBehaviour
{
    public AudioClip sfx_white;
    public AudioClip sfx_yellow;
    public AudioClip sfx_red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                if (obstacle)
                {
                    if (obstacle.enabled)
                    {
                        obstacle.OnTouch();
                    }
                    var info = obstacle.obstacleInfo;
                    if (info.OnTouch == null)
                    {
                        switch (info.obstacleType)
                        {
                            case ObstacleType.Green:
                                if (sfx_white != null)
                                {
                                    SoundManager.Instance.PlaySfx(sfx_white);
                                }
                                break;
                            case ObstacleType.Yellow:
                                SoundManager.Instance.PlaySfx(sfx_yellow);
                                break;
                            case ObstacleType.Red:
                                SoundManager.Instance.PlaySfx(sfx_red);
                                break;
                        }
                    }
                }
            }
        }
    }
}
