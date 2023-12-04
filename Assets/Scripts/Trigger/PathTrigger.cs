using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.name == "Player")
            {
                if (PlayerController_TopDown.Instance.is_auto_moving)
                {
                    PlayerController_TopDown.Instance.myRigidbody.velocity = Vector3.zero;

                    float duration = Vector2.Distance(PlayerController_TopDown.Instance.transform.position, transform.position) / PlayerController_TopDown.Instance.speed_run;
                    var tween = PlayerController_TopDown.Instance.myRigidbody.DOMove(transform.position, duration);
                    tween.onComplete += NextIndex;
                }
            }
        }
    }

    private void NextIndex()
    {
        PlayerController_TopDown.Instance.__DoPath();
    }
}
