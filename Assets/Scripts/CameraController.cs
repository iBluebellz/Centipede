using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float screenRatio = 0.5649f;  // screen ratio is 16:9
    [SerializeField] private int screenPadding = 1;

    // Start is called before the first frame update
    void Start()
    {
        GridController gController = GameObject.Find("GameController").GetComponent<GridController>();
        UnityEngine.Camera.main.orthographicSize = Mathf.Max(gController.rows * gController.size * 0.5f + screenPadding,    // rows
                                                            gController.cols * gController.size * 0.5f * screenRatio + screenPadding);  // cols
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}