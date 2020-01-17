using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public float size = 1;
    public GameObject gridPrefab;
    private Vector2[,] gridPosition;
    private bool[,] wallTable;
    private float firstCol;
    private float firstRow;
    private float halfSize;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }
    
    void Awake() {

        gridPosition = new Vector2[cols, rows];
        
        firstCol = -cols * size * 0.5f;
        firstRow = -rows * size * 0.5f;
        halfSize = size * 0.5f;

        wallTable = new bool[cols, rows];
        
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                wallTable[i,j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                GameObject block = (GameObject)Instantiate(gridPrefab, transform);
                block.transform.position = new Vector3(firstCol + col * size + halfSize , firstRow + row * size + halfSize , 0.1f);
                gridPosition[col, row] = new Vector3(firstCol + col * size + halfSize , firstRow + row * size + halfSize , 0.0f);
            }
        }
    }
    
    // Param should be world position
    public Vector2 GridCalculate(Vector3 position) {
        int col = Mathf.RoundToInt( position.x - firstCol - halfSize );
        int row = Mathf.RoundToInt( position.y - firstRow - halfSize );
        return new Vector2(col,row);
    }
    
    //Param should be col and row position
    public Vector2 GridPosition(Vector2 position) {
        return gridPosition[(int)position.x, (int)position.y];
    }

    //Param should be col and row position
    public void setWall(Vector2 position, bool value) {
        wallTable[(int)position.x, (int)position.y] = value;
    }

    //Param should be col and row position
    public bool isWall(Vector2 position) 
    {
        return wallTable[(int)position.x, (int)position.y];
    }
}
