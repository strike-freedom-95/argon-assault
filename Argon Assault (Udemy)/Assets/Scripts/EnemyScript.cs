using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int score = 10;
    [SerializeField] float enemyHealth = 5f;
    [SerializeField] GameObject hitFX;
    [SerializeField] GameObject explosionFX;

    ScoreBoard scoreboard;
    float damage = 10f;
    GameObject parentGameObject;


    private void Start()
    {
        AddRigidBody();
        scoreboard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("Spawn");
    }

    private void AddRigidBody()
    {
        // rigidBody = new Rigidbody();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(enemyHealth < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        GameObject instance = Instantiate(hitFX, transform.position, Quaternion.identity);
        instance.transform.parent = parentGameObject.transform;
        enemyHealth--;
        // scoreboard.IncreaseScore(score);
    }
    private void KillEnemy()
    {
        if(enemyHealth < 1)
        {
            PlayParticles();
            scoreboard.IncreaseScore(score);
            Destroy(gameObject);
        }        
    }

    public void PlayParticles()
    {
        GameObject instance = Instantiate(explosionFX, transform.position, Quaternion.identity);
        instance.transform.parent = parentGameObject.transform;
    }
}
