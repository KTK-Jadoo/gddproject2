using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("wat");
        if (collision.transform.CompareTag("Player")) {
            collision.transform.GetComponent<PlayerController>().Die();
        }
    }
}
