using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject BulletPrefab;
    [SerializeField] private float moveSpeed = 6.0f;
    private float speedX = 0.0f;
    private float speedY = 0.0f;
    private float friction = 0.93f;
    private Vector3 bulletPadding = new Vector3(0, 0.5f, 0);
    private float cooldown = 0.0f;
    private float limitLeft;
    private float limitRight;
    private float limitUp;
    private float limitDown;
    private Vector2 position;
    [SerializeField] private float invaluableTime = 1.5f;
    private float invaluableCurrent = 0;
    [SerializeField] private int maxHp = 3;
    private int curHp;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float bulletCD = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
        GridController gController = GameObject.Find("GameController").GetComponent<GridController>();
        limitUp = -gController.rows * gController.size * 0.5f * 0.7f; // only 15 percent of screen height
        limitDown = -gController.rows * gController.size * 0.5f;
        limitRight = gController.cols * gController.size * 0.5f;
        limitLeft = -gController.cols * gController.size * 0.5f;
        transform.position = new Vector3 (0, limitDown + 0.5f, 0);

        GameObject.Find("GameController").GetComponent<InterfaceController>().Refresh();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        Health();
    }
    
    private void Movement() 
    {
        // input
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0) {
            speedX = horizontalInput;
        }

        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0) {
            speedY = verticalInput;
        }

        // move with speed
        transform.Translate(new Vector3(1,0,0) * Time.deltaTime * moveSpeed * speedX);
        transform.Translate(new Vector3(0,1,0) * Time.deltaTime * moveSpeed * speedY);

        // bounding
        if (transform.position.x < limitLeft) {
            transform.position = new Vector3(limitLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > limitRight) {
            transform.position = new Vector3(limitRight, transform.position.y, transform.position.z);
        }
        if (transform.position.y > limitUp) {
            transform.position = new Vector3(transform.position.x, limitUp, transform.position.z);
        }
        else if (transform.position.y < limitDown) {
            transform.position = new Vector3(transform.position.x, limitDown, transform.position.z);
        }

        position = GameObject.Find("GameController").GetComponent<GridController>().GridCalculate(transform.position);

        // friction
        speedX *= friction;
        speedY *= friction;
    }

    private void Shooting() 
    {
        if (Input.GetButtonDown("Fire1") && cooldown == 0) {
            Instantiate(BulletPrefab, transform.position + bulletPadding, Quaternion.identity);
            cooldown += bulletCD;
        }

        cooldown -= Time.deltaTime;
        if (cooldown <= 0.0f) {
            cooldown = 0;
        }
    }

    private void Health() 
    {
        if (invaluableCurrent >= 0) {
            invaluableCurrent -= Time.deltaTime;
            if (invaluableCurrent <= 0) {
                Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                gameObject.GetComponent<SpriteRenderer>().color = tmp;
                invaluableCurrent = -1f;
            }
        }
        else {
            GameObject[] centipedes = GameObject.FindGameObjectsWithTag("Centipede");
            foreach (GameObject centipede in centipedes) {
                if (position.x == centipede.GetComponent<Centipede>().getPosition().x && position.y == centipede.GetComponent<Centipede>().getPosition().y) {
                    centipede.GetComponent<Centipede>().getHit();
                    getHit();
                    break;
                }
            }
        }
    }

    public Vector2 getPosition() 
    {
        return position;
    }

    public void getHit() 
    {
        if (invaluableCurrent <= 0) {
            curHp--;
            GameObject.Find("GameController").GetComponent<InterfaceController>().Refresh();
        
            invaluableCurrent = invaluableTime;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    public int getHealth() {
        return curHp;
    }

    public float getBulletSpeed() {
        return bulletSpeed;
    }

}
