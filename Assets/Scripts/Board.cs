using UnityEngine;
using UnityEngine.Tilemaps;
using static PieceUtils;
public class Board : MonoBehaviour
{
    public GameObject nextPieceDisplay;
    public TetrominoData[] tetrominos;
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int pos = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(pos, this.boardSize);
        }
    }

    private TetrominoData nextData;
    private NextPiece nextPiece;
    private Tilemap nextPieceTilemap;

    private void Awake()
    {
        for (int i = 0; i < this.tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }
        this.nextPieceTilemap = nextPieceDisplay.GetComponentInChildren<Tilemap>();
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.nextPiece = GetComponentInChildren<NextPiece>();
    }

    private void Start()
    {
        GenerateNextPiece(false);
        SpawnPiece();
    }

    private void GenerateNextPiece(bool shouldClear = true)
    {
        if (shouldClear)
        {
            PieceUtils.ClearOnTilemap(this.nextPiece, this.nextPieceTilemap);
        }
        this.nextData = PieceUtils.RandomTetromino(this.tetrominos);
        this.nextPiece.Initialize(this.nextData, new Vector3Int(-15, 8, 0));
        PieceUtils.SetOnTilemap(this.nextPiece, this.nextPieceTilemap);
    }


    public void SpawnPiece()
    {
        TetrominoData data = nextData;
        this.activePiece.Initialize(this, this.spawnPosition, data);
        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            SetOnTilemap(this.activePiece);
            GenerateNextPiece();
        }
        else
        {
            GameOver();
        }
        Debug.Log(nextData.tetromino);
    }

    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }

    public void SetOnTilemap(Piece piece)
    {
        PieceUtils.SetOnTilemap(piece, this.tilemap);
    }

    public void ClearLine()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }

    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if (this.tilemap.HasTile(position)) this.tilemap.SetTile(position, null);
        }
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase tileAbove = this.tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, tileAbove);
            }
            row++;
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if (!this.tilemap.HasTile(position)) return false;
        }
        return true;
    }

    public void ClearOnTilemap(Piece piece)
    {
        PieceUtils.ClearOnTilemap(piece, this.tilemap);
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePos)) return false;
            if (this.tilemap.HasTile(tilePos)) return false;
        }
        return true;
    }
}
