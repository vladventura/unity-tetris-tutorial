using UnityEngine;

public class NextPiece : MonoBehaviour
{
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public void Initialize(TetrominoData data, Vector3Int position)
    {
        this.data = data;
        this.position = position;
        if (cells == null)
        {
            cells = new Vector3Int[this.data.cells.Length];
        }
        for (int i = 0; i < this.data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)this.data.cells[i];
        }
    }
}
