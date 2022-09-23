using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setupRB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region movement
    private Rigidbody2D bulletRB;
    public float speed;
    public Vector2 direction;
    void setupRB() {
        bulletRB = transform.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction * speed;
    }
    #endregion

    #region collision
    public float gravity;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) return;
        collision.GetComponent<Rigidbody2D>().gravityScale = gravity;
        Destroy(this.gameObject);
    }
    #endregion
}
