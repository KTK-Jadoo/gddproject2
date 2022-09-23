using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    #region Movement_variables
    public float movespeed;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Targeting_variables
    private Transform player;
    public GameObject playerObj;
    #endregion


    #region Unity_functions
    private void Awake() {
        EnemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        player = playerObj.transform;
    }
    #endregion

    private void Update() {
        //check to see if we know where the player is
        if (player == null) {
            return;
        }
        Move();
        Explode();
    }

    #region Movement_functions
    private void Move() {

        int direction;
        if (player.position.x - transform.position.x > 0) {
            direction = 1;
        } else if (player.position.x - transform.position.x < 0) {
            direction = -1;
        } else {
            direction = 0;
        }

        //Vector2 direction = player.position - transform.position;

        EnemyRB.velocity = new Vector2( direction * movespeed, EnemyRB.velocity.y);
    }
    #endregion

    #region Attack_functions

    public GameObject explosionObj;

    private void Explode() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(EnemyRB.position + Vector2.left, Vector2.one, 0f, Vector2.zero);
        RaycastHit2D[] hits2 = Physics2D.BoxCastAll(EnemyRB.position + Vector2.right, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            //Debug.Log(hit);
            if (hit.transform.CompareTag("Player")) {
                Debug.Log("Hit player with explosion");
                // spawn prefab
                //Instantiate(explosionObj, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().Die();
                //Destroy(this.gameObject);

            }
        }
        foreach (RaycastHit2D hit in hits2) {
            //Debug.Log(hit);
            if (hit.transform.CompareTag("Player")) {
                Debug.Log("Hit player with explosion");
                // spawn prefab
                //Instantiate(explosionObj, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().Die();
                //Destroy(this.gameObject);

            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision) {
    //      Explode();
    //}
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    public float explosionRadius;
    #endregion

    #region Health_functions
    public void TakeDamage(float value) {
        currHealth -= value;
        Debug.Log("Health is now" + currHealth.ToString());

        if (currHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(this.gameObject);
    }
    #endregion

}