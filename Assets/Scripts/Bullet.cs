using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 2f;
    public Vector3 targetVector;
    public GameObject miniPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
        GameObject[] go = GameObject.FindGameObjectsWithTag("EneMini");
        foreach (GameObject enemini in go)
        {
            Destroy(enemini, maxLifeTime-1);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject asteroid1 = Instantiate(miniPrefab, collision.transform.position, Quaternion.identity);
            GameObject asteroid2 = Instantiate(miniPrefab, collision.transform.position, Quaternion.identity);

            IncreaseScore(0);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EneMini"))
        {
            IncreaseScore(1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void IncreaseScore(int a)
    {
        var x = Math.Abs(transform.position.x);
        var y = Math.Abs(transform.position.y);

        //detruir solo los asteroides dentro de la pantalla
        if (x < 7 || y < 6)
        {
            if (a == 0)
            {
                Player.SCORE++;
            }
            else
            {
                Player.SCORE += 0.5;
            }
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        go.GetComponent<Text>().text = "PUNTOS: " + Player.SCORE;
    }
}
