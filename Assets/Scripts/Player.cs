using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject shooter, bulletPrefab, goscreen;

    public static double SCORE = 0;
    private Rigidbody _rigid;

    private float xBorderLimit = 6;
    private float yBorderLimit = 6;
    private Vector3 finalPos;

    //public --> permite modificar valores desde unity
    //private --> no aparece directamente (es "invisible")

    // Start is called before the first frame update
    void Start()
    {
        goscreen.SetActive(false);
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (goscreen.activeSelf == false)
        {
            var pos = transform.position;

            if (pos.x > xBorderLimit + 0.25)
            {
                pos.x = -xBorderLimit;
            }
            else if (pos.y > yBorderLimit + 0.25)
            {
                pos.y = -yBorderLimit;
            }
            else if (pos.x < -xBorderLimit - 0.25)
            {
                pos.x = xBorderLimit;
            }
            else if (pos.y < -yBorderLimit - 0.25)
            {
                pos.y = yBorderLimit;
            }

            transform.position = pos;

            float thrust = Input.GetAxis("Vertical") * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;

            Vector3 thrustDirection = transform.right;

            _rigid.AddForce(thrustDirection * thrust * thrustForce);

            transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(bulletPrefab, shooter.transform.position, Quaternion.identity);

                Bullet balaScript = bullet.GetComponent<Bullet>();

                balaScript.targetVector = transform.right;
            }

        }
        else
        {
            transform.position = finalPos;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("EneMini"))
        {
            SCORE = 0;

            goscreen.SetActive(true);
            finalPos = transform.position;

            GameObject go = GameObject.FindGameObjectWithTag("Score");
            RectTransform rt = go.GetComponent<Text>().GetComponent<RectTransform>();
            rt.localPosition = new Vector2(-7, -38);
            rt.localScale = new Vector2(0.75f, 0.75f);
        }
    }

    public void RestartButton()
    {
        //SceneManager.LoadScene necesita de un String con el nombre de la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
