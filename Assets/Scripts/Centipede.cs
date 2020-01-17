using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public GameObject prev = null;
    public GameObject next = null;
    private Vector2 position;
    private Vector2 moveTarget;
    private float moveSpeed = 1.0f;
    public int horizontalMove = 1;  // Move Right
    public int verticalMove = -1;   // Move Down
    private bool escape = false;
    private GridController gController;
    [SerializeField]private float chaseDistance = 0.9f;
    [SerializeField]private int score = 200;
   
    // Start is called before the first frame update
    void Start()
    {
        gController = GameObject.Find("GameController").GetComponent<GridController>();
        position = gController.GridCalculate(transform.position);
        moveSpeed = GameObject.Find("GameController").GetComponent<CentipedeSpawner>().getCentipedeSpeed();

        FindNextGrid();

        if (prev == null) {
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.g = 0f;
            tmp.b = 0f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (moveTarget == null) {
            moveTarget = prev.GetComponent<Centipede>().getPosition();
        }

        position = gController.GridCalculate(transform.position);


        if (prev == null) {
            if (transform.position.x == gController.GridPosition(moveTarget).x && transform.position.y == gController.GridPosition(moveTarget).y ) {
                FindNextGrid();
            }
            if (escape) {
                escape = false;
                ChangeHorizontal();
                if (position.y + verticalMove >= gController.rows || position.y + verticalMove < 0) {
                    ChangeVerticle();
                }
                moveTarget = new Vector2(position.x, position.y + verticalMove);
            }
        }
        else {
            if (transform.position.x == gController.GridPosition(moveTarget).x && transform.position.y == gController.GridPosition(moveTarget).y ) {
                moveTarget = prev.GetComponent<Centipede>().getPosition();
            }
        }

        // make that part try to catch the head nearer
        if (prev != null && Vector2.Distance(transform.position, gController.GridPosition(moveTarget)) > chaseDistance) {
            transform.position = Vector2.MoveTowards(transform.position, gController.GridPosition(moveTarget), Time.deltaTime * moveSpeed * 1.05f);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, gController.GridPosition(moveTarget), Time.deltaTime * moveSpeed );
        }
    }

    // find next grid to go to
    void FindNextGrid() {

        if (position.x + horizontalMove < gController.cols && position.x + horizontalMove >= 0 && !gController.isWall(new Vector2(position.x + horizontalMove, position.y))) {
            moveTarget = new Vector2(position.x + horizontalMove, position.y);
        }
        
        else {
            ChangeHorizontal();
            if (position.y + verticalMove >= gController.rows || position.y + verticalMove < 0) {
               ChangeVerticle();
            }
            moveTarget = new Vector2(position.x, position.y + verticalMove);
            
        }
    }

    public Vector2 getPosition() {
        return position;
    }

    public void getHit() {
        if (prev != null) {
            prev.GetComponent<Centipede>().next = null;
        }
        if (next != null) {
            next.GetComponent<Centipede>().prev = null;
            next.GetComponent<Centipede>().becomeHead();
        }
        GameObject.Find("GameController").GetComponent<InterfaceController>().ScoreIncrease(score);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void becomeHead() {
        escape = true;
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.g = 0f;
        tmp.b = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    public void ChangeVerticle() {
        verticalMove = -verticalMove;
        if (next != null) {
            next.GetComponent<Centipede>().ChangeVerticle();
        }
    }

    public void ChangeHorizontal() {
        horizontalMove = -horizontalMove;
        if (next != null) {
            next.GetComponent<Centipede>().ChangeHorizontal();
        }
    }
}
