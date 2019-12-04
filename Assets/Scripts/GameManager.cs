// File: GameManager.cs
// Contributors: Brendan Robinson
// Date Created: 11/11/2019
// Date Last Modified: 11/14/2019

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    #region Constants

    public static GameManager Instance;

    #endregion

    #region Fields

    public Player player;
    public Tilemap topLayer;
    public Tilemap bottomLayer;
    public Dictionary<Vector2Int, GameObject>
        objects; // Because some objects can't be linked to the tile map and must be actual gameobjects e.g. trees

    public WishTile colliderTile; // Invisible tile with collider


    #endregion

    #region Methods

    private void Awake() {
        if (!Instance) { Instance = this; }
        else { Destroy(this); }
        ItemDatabase.ConstructDatabase();
        objects = new Dictionary<Vector2Int, GameObject>();
    }

    public bool HasTopTile(Vector2Int position) {
        return topLayer.GetTile(new Vector3Int(position.x, position.y, 0)) is WishTile;
    }

    public WishTile GetTopTile(Vector2Int position) {
        return topLayer.GetTile<WishTile>(new Vector3Int(position.x, position.y, 0));
    }

    public T GetTopTile<T>(Vector2Int position) where T : WishTile {
        return topLayer.GetTile<T>(new Vector3Int(position.x, position.y, 0));
    }

    public void DeleteTopTile(Vector2Int position) {
        topLayer.SetTile(new Vector3Int(position.x, position.y, 0), null);
        topLayer.RefreshTile(new Vector3Int(position.x, position.y, 0));
    }

    public bool HasBottomTile(Vector2Int position)
    {
        return bottomLayer.GetTile(new Vector3Int(position.x, position.y, 0)) is WishTile;
    }

    public bool HasBottomTile(Vector2Int position, Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (!(bottomLayer.GetTile(new Vector3Int(position.x + i, position.y + j, 0)) is WishTile)) {
                    return false;
                }
            }
        }
        return true;
    }

    public WishTile GetBottomTile(Vector2Int position) {
        return bottomLayer.GetTile<WishTile>(new Vector3Int(position.x, position.y, 0));
    }

    public T GetBottomTile<T>(Vector2Int position) where T : WishTile {
        return bottomLayer.GetTile<T>(new Vector3Int(position.x, position.y, 0));
    }

    public void SetBottomTile(Vector2Int position, TileBase tile) {
        bottomLayer.SetTile(new Vector3Int(position.x, position.y, 0), tile);
    }

    public bool HasObject(Vector2Int position) { return objects.ContainsKey(position); }

    public bool HasObject(Vector2Int position, Vector2Int size) {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                if (objects.ContainsKey(position + new Vector2Int(i, j))) { return true; }
            }
        }
        return false;
    }

    public GameObject GetObject(Vector2Int position) { return objects[position]; }

    public void SetObject(Vector2Int position, GameObject obj) {
        topLayer.SetTile(new Vector3Int(position.x, position.y, 0), colliderTile);
        objects.Add(position, obj);
    }

    public void SetObject(Vector2Int position, Vector2Int size, GameObject obj) {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) { SetObject(new Vector2Int(position.x + i, position.y + j), obj); }
        }
    }

    public void DeleteObject(Vector2Int position) {
        Destroy(objects[position]);
        objects.Remove(position);
        DeleteTopTile(position);
    }

    public void DeleteObject(Vector2Int position, Vector2Int size) {
        Destroy(objects[position]);
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                objects.Remove(new Vector2Int(position.x + i, position.y + j));
                DeleteTopTile(new Vector2Int(position.x + i, position.y + j));
            }
        }
    }

    #endregion

}