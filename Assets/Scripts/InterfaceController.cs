using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject gameoverText;
    private int score;
    private int centipedeNum;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() 
    {
        gameoverText.SetActive(false);
        scoreText.SetActive(true);
        score = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Live : " + player.GetComponent<Player>().getHealth() + " Score : " + score.ToString("00000");
    }

    // Update is called once per frame
    void Update()
    {

        centipedeNum = 0;
        GameObject[] centipedes = GameObject.FindGameObjectsWithTag("Centipede");
        foreach (GameObject centipede in centipedes) {
            centipedeNum++;
        }

        if (centipedeNum == 0 || player.GetComponent<Player>().getHealth() <= 0) {
            GameEnd();
        }
    }

    private void GameEnd() 
    {
        gameoverText.SetActive(true);
        Time.timeScale = 0;

        if (Input.GetButtonDown("Fire1")) {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void Refresh() {
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Live : " + player.GetComponent<Player>().getHealth() + " Score : " + score.ToString("00000");
    }

    public void ScoreIncrease(int point)
    {
        score += point;
        Refresh();
    }
}
