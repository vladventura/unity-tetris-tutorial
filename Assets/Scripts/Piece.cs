using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board _board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int position { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public int rotationIndex { get; private set; }
    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float moveDelay = 0.1f;
    private float stepTime;
    private float lockTime;
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        _board = board;
        this.data = data;
        this.position = position;
        this.rotationIndex = 0;
        this.stepTime = Time.time + stepDelay;
        this.lockTime = 0f;
        if (this.cells == null)
        {
            cells = new Vector3Int[this.data.cells.Length];
        }

        for (int i = 0; i < this.data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)this.data.cells[i];
        }
    }

    private void Update()
    {
        this._board.ClearOnTilemap(this);
        this.lockTime += Time.deltaTime;
        RotatingMovement();
        DropMovement();
        SidewaysMovement();
        if (Time.time > this.stepTime) Step();
        this._board.SetOnTilemap(this);
    }
    private void RotatingMovement()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
    }

    private void DropMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
    }

    private void SidewaysMovement()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;
        Move(Vector2Int.down);
        if (this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        this._board.SetOnTilemap(this);
        this._board.ClearLine();
        this._board.SpawnPiece();
    }

    private void Rotate(int direction)
    {
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);
        ApplyRotationMatrix(direction);

        if (!TestWallKicks(rotationIndex, direction))
        {
            this.rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];
            int x, y;
            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int direction)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, direction);
        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];
            if (Move(translation)) return true;
        }
        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int direction)
    {
        int wallKickIndex = rotationIndex * 2;
        if (rotationIndex < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min) return max - (min - input) % (max - min);
        else return min + (input - min) % (max - min);
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
    }

    private bool Move(Vector2Int direction)
    {
        Vector3Int newPos = this.position;
        newPos.x += direction.x;
        newPos.y += direction.y;
        bool valid = this._board.IsValidPosition(this, newPos);
        if (valid)
        {
            this.position = newPos;
            this.lockTime = 0f;
        }
        return valid;
    }

}
