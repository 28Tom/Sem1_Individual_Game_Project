using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float xPos;

    public int lives = 3;
    public int score = 0;

    public GameObject[] gameObjects;
    public GameObject PlayerBullet;

    public Transform attack_point;
    public GUIStyle myStyle;

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;
    private bool canAttack;
    void Start()
    {
        current_Attack_Timer = attack_Timer;

    }

    void Update()
    {
        MoveCharacter();
        xPos = transform.position.x;
        Attack();

    }

    void MoveCharacter()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (xPos > 8.50)
            {
                speed = 0.0f;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                speed = 10.0f;
                transform.Translate(Vector3.right * speed * Time.deltaTime);

            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (xPos < -8.50)
            {
                speed = 0.0f;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                speed = 10.0f;
                transform.Translate(Vector3.left * speed * Time.deltaTime);

            }
        }
    }

    void Attack()
    {
        attack_Timer += Time.deltaTime;
        if(attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(PlayerBullet, attack_point.position, Quaternion.identity);

            }
        }
    }

    void RemovalEnemyBullet()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Bullet");
        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }

    void RemovalEnemyPlane()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("EnemyPlane");
        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "EnemyBullet(Clone)")
        {
            lives -= 1;
            Destroy(other.gameObject);
            if (lives == 0)
            {
                print("GAME OVER");
                RemovalEnemyBullet();
                Time.timeScale = 0;
                SceneManager.LoadScene("GameOver1");
            }
        }

        if (other.gameObject.name == "EnemyPlane(Clone)")
        {
            lives -= 1;
            Destroy(other.gameObject);
            if (lives == 0)
            {
                print("GAME OVER");
                RemovalEnemyPlane();
                Time.timeScale = 0;
                SceneManager.LoadScene("GameOver1");
            }
        }
    }
        private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 30), "Time " + Time.time, myStyle);
        GUI.Box(new Rect(10, 40, 100, 30), "Lives " + lives);
    }
}
