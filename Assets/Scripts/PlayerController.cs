using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Constants
    private readonly KeyCode shootButton = KeyCode.LeftShift;
    private readonly KeyCode bulletChangeButton = KeyCode.LeftControl;

    private readonly string bulletsRemaining = "Bullets remaining innit: ";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        setupMovement();
        setupBullets();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        swapGravity();
        changeBullet();
        shoot();
    }

    #region Movement
    private Vector2 currentDirection;
    private Rigidbody2D PlayerRB;
    public float movespeed;
    void setupMovement() {
        PlayerRB = transform.GetComponent<Rigidbody2D>();
        currentDirection = Vector2.right;
    }
    void move() {
        float x_input = Input.GetAxisRaw("Horizontal");
        PlayerRB.velocity = new Vector2(movespeed * x_input, PlayerRB.velocity.y);
        if (x_input != 0) currentDirection = currentDirection * x_input;
    }

    void swapGravity() {
        if (Input.GetKeyDown(KeyCode.Space)) PlayerRB.gravityScale = -PlayerRB.gravityScale;
    }
    #endregion

    #region Shooting
    public GameObject bullet;
    public int maxBullets;
    private int bullets;
    public GameObject bulletUI;
    private Text textComponent;

    private int gravityOfBullet;

    void setupBullets() {
        gravityOfBullet = -1;
        bullets = maxBullets;
        textComponent = bulletUI.GetComponent<Text>();
        updateBulletCountUI();
    }
    void updateBulletCountUI() {
        Debug.Log("what??? " + bullets.ToString());
        textComponent.text = bulletsRemaining + bullets.ToString();
    }

    void shoot() {
        if (Input.GetKeyDown(shootButton) && bullets > 0) {
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = transform.position;
            spawnedBullet.GetComponent<Bullet>().gravity = gravityOfBullet;
            spawnedBullet.GetComponent<Bullet>().direction = currentDirection;
            bullets--;
            updateBulletCountUI();
        }
    }
    void changeBullet() {
        if (Input.GetKeyDown(bulletChangeButton)) {
            gravityOfBullet = ((gravityOfBullet + 2) % 3) - 1;
            Debug.Log("bullet type is: " + gravityOfBullet.ToString());
        }
    }
    #endregion

    public void Die() {
        SceneManager.LoadScene("lose");
    }
}
