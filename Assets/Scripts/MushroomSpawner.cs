using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField]public GameObject mushroomPrefab;
    private int SpareSlot = 0;
    [SerializeField]private int MushroomPercentage = 15;
    [SerializeField]private int MushroomHP = 3; 

    // Start is called before the first frame update
    void Start()
    {
        GenerateMushroom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GenerateMushroom() 
    {
        GridController gController = gameObject.GetComponent<GridController>();
        SpareSlot = gameObject.GetComponent<CentipedeSpawner>().getCentipedePart();

        float firstCol = -gController.cols * gController.size * 0.5f;
        float firstRow = -gController.rows * gController.size * 0.5f;
        float halfSize = gController.size * 0.5f;

        int padding = Mathf.CeilToInt(SpareSlot / gController.cols) + 1;
        for (int row = 1; row < gController.rows - padding; row++) {
            for (int col = 0; col < gController.cols; col++) {
                if (Random.Range(0,100) < MushroomPercentage) {
                    GameObject mushroom = (GameObject)Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
                    mushroom.transform.position = new Vector3( firstCol + col * gController.size + halfSize ,firstRow + row * gController.size + halfSize ,0);
                }
            }
        }
    }

    public int getMushroomHP(){
        return MushroomHP;
    }
}
