using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType { Normal, Rock, Tree, Water, Plant, Dirt, Hoed, Watered }
public class WishTile : TileBase {

    public Sprite sprite;
    public TileType tileType;
    public Tile.ColliderType colliderType;

    [HideInInspector]
    public GameObject instantiatedGameobject; // In case this tile also refers to a gameobject not part of the tile map

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData) {
        tileData.sprite = sprite;
        tileData.color = Color.white;
        tileData.flags = TileFlags.LockTransform;
        tileData.colliderType = colliderType;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/WishTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Wish Tile", "New Wish Tile", "Asset", "Save Wish Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WishTile>(), path);
    }
#endif
}
