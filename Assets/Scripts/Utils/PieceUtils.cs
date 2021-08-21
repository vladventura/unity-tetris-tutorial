using UnityEngine;
using UnityEngine.Tilemaps;
public static class PieceUtils
{
    public static TetrominoData RandomTetromino(TetrominoData[] tetrominos)
    {
        int random = Random.Range(0, tetrominos.Length);
        TetrominoData data = tetrominos[random];
        return data;
    }
    public static void SetOnTilemap(Piece piece, Tilemap tilemap)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int location = piece.cells[i] + piece.position;
            tilemap.SetTile(location, piece.data.tile);
        }
    }
}