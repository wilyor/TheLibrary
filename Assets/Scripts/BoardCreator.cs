using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardCreator : MonoBehaviour
{
    public GameObject tilePref;
    public GameObject guide;
    Board board;
    int goodTilesTotal;
    int currentGoodtiles = 0;
    int currentGoodIndex = 0;
    float currentSize;
    public int[,] boardMatrix;
    public List<Tile> goodTiles;
    [SerializeField] UnityEvent onCompleteEvent;

    void Start()
    {
        guide.SetActive(false);
        board = new Board(new int[,] { { 1, 0, 0, 0}, { 1, 1, 1, 0}, { 0, 0, 1, 0 }, { 0, 0, 1, 1} });
        currentSize = transform.lossyScale.x;
        transform.localScale = new Vector3(currentSize/board.width, currentSize/board.height, 1);
        goodTiles = new List<Tile>();
        PopulateBoard();
        FindPath(0,0,3,3);
    }

    public void PopulateBoard()
    {
        goodTilesTotal = CountGoodTiles();
        for (int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                CreateTile(j, i);
            }
        }
    }

    int CountGoodTiles()
    {
        int totalGood  = 0;
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                totalGood += board.tiles[j, i] == 1 ? 1:0;
            }
        }
        return totalGood;
    }

    void FindPath(int startX, int startY, int finalX, int finalY)
    {
        int currentX = startX;
        int currentY = startY;
        goodTiles.Add(board.tilesGO[startX, startY].GetComponent<Tile>());
        goodTiles[0].prevSibling = goodTiles[0];
        findNextSibling(goodTiles[0], currentX, currentY, finalX, finalY);
        
    }

    void findNextSibling(Tile currentTile, int currentX, int currentY, int finalX, int finalY)
    {
        for(int i = currentX - 1; i <= currentX + 1; i++)
        {
            for (int j = currentY - 1; j <= currentY + 1; j++)
            {
                if (i >= 0 && i< board.width && j >= 0 && j < board.height && (i == currentX || j == currentY))
                {
                    Tile newtile = board.tilesGO[i, j].GetComponent<Tile>();
                    if (newtile.status == Status.activeGood && newtile.prevSibling == null)
                    {
                        currentTile.nextSibling = newtile;
                        newtile.prevSibling = currentTile.nextSibling;
                        goodTiles.Add(newtile);
                        if (currentX != finalX && currentY != finalY)
                        {
                            findNextSibling(newtile, i, j, finalX, finalY);
                        }
                        return;
                    }
                }
            }
        }
    }

    void CreateTile(int indexX, int indexY)
    {
        GameObject newTile = Instantiate(tilePref, transform.position, transform.rotation, transform);
        Vector3 newPosition = new Vector3(indexY - (board.height/2 - 0.5f), indexX - (board.width / 2 - 0.5f), 0);
        newTile.transform.localPosition = newPosition;
        newTile.GetComponent<Tile>().OnWrongSelected += ResetBoard;
        newTile.GetComponent<Tile>().OnGoodSelected += AddOneToBoard;
        newTile.name =string.Format("({0},{1})", indexX, indexY);
        board.tilesGO[indexY, indexX] = newTile;
        AssignStatusToTile(newTile, board.tiles[indexX, indexY]);
    }
    void AssignStatusToTile(GameObject tile, int statusValue)
    {
        switch (statusValue)
        {
            case 0:
                tile.GetComponent<Tile>().status = Status.activeBad;
                break;
            case 1:
                tile.GetComponent<Tile>().status = Status.activeGood;
                break;
        }
    }

    void ResetBoard(Tile tile)
    {
        board.ResetBoard();
        currentGoodtiles = 0;
        currentGoodIndex = 0;
    }

    void AddOneToBoard(Tile tile)
    {
        if (tile.Equals(goodTiles[currentGoodIndex]))
        {
            currentGoodtiles += 1;
            currentGoodIndex += 1;
            if (currentGoodtiles >= goodTilesTotal)
            {
                onCompleteEvent?.Invoke();
            }
        }
        else
        {
            ResetBoard(tile);
        }
    }
}

public class Board
{
    public int height;
    public int width;
    public int[,] tiles;
    public GameObject[,] tilesGO;

    public Board(int height, int width)
    {
        this.height = height;
        this.width = width;
        tiles = new int[width,height];
        tilesGO = new GameObject[width, height];
    }

    public Board(int[,] boardMatrix)
    {
        height = boardMatrix.GetLength(0);
        width = boardMatrix.GetLength(1);
        tiles = boardMatrix;
        tilesGO = new GameObject[width, height];
    }

    public void InitializeBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = 0;
            }
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tilesGO[i, j].GetComponent<Tile>().CoverTile();
            }
        }
    }
}