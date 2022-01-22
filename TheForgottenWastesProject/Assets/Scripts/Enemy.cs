using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("Damage Target")]
    public HealthManager healthManagerScript;

    private void Start()
    {
        // Sets this GameObject's AI Target to Player
        var aiDestSetter = GetComponent<AIDestinationSetter>();
        aiDestSetter.target = GameObject.FindWithTag("Player").transform;
        if (!aiDestSetter.target)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        if (healthManagerScript.currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("ZombieDeath");
            ScoreManager.instance.AddKill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            healthManagerScript.TakeDmg(25);
        }
    }
}
