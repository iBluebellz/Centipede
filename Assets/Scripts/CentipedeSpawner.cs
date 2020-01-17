using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    public GameObject centipedePrefab;
    [SerializeField]private int centipedeParts = 15;
    [SerializeField]private float centipedeSpeed = 6.0f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateCentipede();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GenerateCentipede()
    {
        GridController controller = gameObject.GetComponent<GridController>();

        float firstCol = -controller.cols * controller.size * 0.5f;
        float firstRow = -controller.rows * controller.size * 0.5f;
        float halfSize = controller.size * 0.5f;

        int tempCentipedeParts = centipedeParts;
        bool reverse = false;

        GameObject frontCentipede = null;
        GameObject backCentipede = null;

        for (int row = controller.rows - 1; row > 0; row--) {
            if (reverse){
                for (int col = controller.cols - 1; col >= 0; col--) {
                    if (tempCentipedeParts > 0) {
                        frontCentipede = (GameObject)Instantiate(centipedePrefab, transform.position, Quaternion.identity);
                        frontCentipede.transform.position = new Vector3( firstCol + col * controller.size + halfSize , firstRow + row * controller.size + halfSize , 0.0f);
                        if (backCentipede != null) {
                            frontCentipede.GetComponent<Centipede>().next = backCentipede;
                            backCentipede.GetComponent<Centipede>().prev = frontCentipede;
                        }
                        backCentipede = frontCentipede;
                        tempCentipedeParts--;
                    }
                    else {
                        break;
                    } 
                }
            }
            else {
                for (int col = 0; col < controller.cols; col++) {
                    if (tempCentipedeParts > 0) {
                        frontCentipede = (GameObject)Instantiate(centipedePrefab, transform.position, Quaternion.identity);
                        frontCentipede.transform.position = new Vector3( firstCol + col * controller.size + halfSize , firstRow + row * controller.size + halfSize , 0.0f);
                        if (backCentipede != null) {
                            frontCentipede.GetComponent<Centipede>().next = backCentipede;
                            backCentipede.GetComponent<Centipede>().prev = frontCentipede;
                        }
                        backCentipede = frontCentipede;
                        tempCentipedeParts--;
                    }
                    else {
                        break;
                    } 
                }
            }

            reverse = !reverse;
            if (tempCentipedeParts <= 0) break;
        }
    }

    public int getCentipedePart() 
    {
        return centipedeParts;
    }

    public float getCentipedeSpeed()
    {
        return centipedeSpeed;
    }
}