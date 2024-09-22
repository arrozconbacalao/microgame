using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject shooter, bulletPrefab, back, goscreen, pscreen;

    public static double SCORE = 0;
    private Rigidbody _rigid;

    private float xBorderLimit = 6;
    private float yBorderLimit = 6;
    private GameObject GOScreen;
    private GameObject PScreen;

    //public --> permite modificar valores desde unity
    //private --> no aparece directamente (es "invisible")

    // Start is called before the first frame update
    void Start()
    {
        back.SetActive(false);
        _rigid = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (back.activeSelf == false)
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                back.SetActive(true);
                goscreen.SetActive(false);
                pscreen.SetActive(true);
            }

        }
        else
        {
            Time.timeScale = 0;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("EneMini"))
        {
            SCORE = 0;

            back.SetActive(true);
            pscreen.SetActive(false);
            goscreen.SetActive(true);

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

    public void ResumeButton() {
        back.SetActive(false);
        Time.timeScale = 1;
    }
}
