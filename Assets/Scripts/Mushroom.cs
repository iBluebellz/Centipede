using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHp;
    private int curHp;
    [SerializeField]private int score = 100;
    private Vector2 position;

    void Start()
    {
        maxHp = GameObject.Find("GameController").GetComponent<MushroomSpawner>().getMushroomHP();
        curHp = maxHp;
        position = GameObject.Find("GameController").GetComponent<GridController>().GridCalculate(transform.position);
        GameObject.Find("GameController").GetComponent<GridController>().setWall(position, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit() {
        curHp--;
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f * curHp/maxHp;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        if (curHp <= 0) {
            GameObject.Find("GameController").GetComponent<InterfaceController>().ScoreIncrease(score);
            GameObject.Find("GameController").GetComponent<GridController>().setWall(position, false);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public Vector2 getPosition() {
        return position;
    }
}
