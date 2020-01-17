using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 direction = new Vector3(0, 1, 0 );
    private float limitUp = 5;
    private bool isHit = false;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        GridController gController = GameObject.Find("GameController").GetComponent<GridController>();
        limitUp = gController.rows * gController.size * 0.5f;
        moveSpeed = GameObject.Find("Player").GetComponent<Player>().getBulletSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        position = GameObject.Find("GameController").GetComponent<GridController>().GridCalculate(transform.position);

        GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");
        foreach (GameObject mushroom in mushrooms) {
            if (position.x == mushroom.GetComponent<Mushroom>().getPosition().x && position.y == mushroom.GetComponent<Mushroom>().getPosition().y) {
                mushroom.GetComponent<Mushroom>().getHit();
                isHit = true;
                break;
            }
        }

        GameObject[] centipedes = GameObject.FindGameObjectsWithTag("Centipede");
        foreach (GameObject centipede in centipedes) {
            if (position.x == centipede.GetComponent<Centipede>().getPosition().x && position.y == centipede.GetComponent<Centipede>().getPosition().y) {
                centipede.GetComponent<Centipede>().getHit();
                isHit = true;
                break;
            }
        }

        if (transform.position.y > limitUp || isHit) {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

    }

    public Vector2 getPosition() {
        return position;
    }
}
