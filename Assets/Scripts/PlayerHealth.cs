using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    private Renderer myRender;
    private Animator anim;
    private Rigidbody2D rb2d;
    private PolygonCollider2D polygonCollider2D;

    void Start()
    {
        //BuffManager.Instance.AddBuffByCfgId(gameObject, 0);

        HealthBar.HealthMax = maxHealth;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void SetMaxHp(int _maxHp)
    {
        maxHealth = _maxHp;
        _OnMaxHpChange();
    }

    public void AddMaxHp(int _addMaxHp)
    {
        maxHealth += _addMaxHp;
        _OnMaxHpChange();
    }

    private void _OnMaxHpChange()
    {
        HealthBar.HealthMax = maxHealth;
    }

    public void DamagePlayer(int damage, bool knock_back = false, Transform trans_damage_from = null)
    {
        //sf.FlashScreen();
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if (health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            //rb2d.gravityScale = 0.0f;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(blinks, time);
        //polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        //polygonCollider2D.enabled = true;
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
