// File: MineGenerator.cs
// Contributors: Brendan Robinson
// Date Created: 11/17/2019
// Date Last Modified: 11/17/2019

using UnityEngine;
using UnityEngine.Tilemaps;

public class MineGenerator : MonoBehaviour {

    #region Fields

    public Vector2Int firstMineStart;
    public Vector2Int secondMineStart;

    public GameObject rockTile;
    public GameObject oreTile;
    public GameObject bigOreTile;

    public TileBase groundTile;
    public WishTile wallTile;


    #endregion

    #region Methods

    private void Start() {
        GenerateMine();
    }

    private void Update() {


    }

    private void GenerateMine() {
        float randomSeedX = Random.Range(0, 10000f);
        float randomSeedY = Random.Range(0, 10000f);
        float randommSizeBias = Random.Range(0, 0.125f);
        float randommNoiseBiasX = Random.Range(12f, 25f);
        float randommNoiseBiasY = Random.Range(12f, 25f);


        for (int i = 0; i < 128; i++) {
            for (int j = 0; j < 128; j++) {
                if (Mathf.PerlinNoise(i / randommNoiseBiasX + randomSeedX, j / randommNoiseBiasY + randomSeedY) * 0.3f
                    + (84f - Mathf.Abs(64f - i)) / 64f * ((50f - Mathf.Abs(64f - j)) / 64f) > 0.9125f + randommSizeBias) {
                    GameManager.Instance.SetBottomTile(new Vector2Int(firstMineStart.x + i, firstMineStart.y + j), groundTile);
                }
            }
        }
        
        for (int i = firstMineStart.x; i < firstMineStart.x + 128; i++)
        {
            for (int j = firstMineStart.y + 0; j < firstMineStart.y + 128; j++)
            {
                if (Random.value < 0.0125f 
                    && !GameManager.Instance.HasObject(new Vector2Int(i, j), new Vector2Int(2, 2))
                    && GameManager.Instance.HasBottomTile(new Vector2Int(i, j), new Vector2Int(2, 2)))
                {
                    Rock newRock = Instantiate(bigOreTile, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Rock>();
                    newRock.position = new Vector2Int(i, j);
                    GameManager.Instance.SetObject(new Vector2Int(i, j), new Vector2Int(2, 2), newRock.gameObject);
                }
            }
        }
        float randomCopperAmount = Random.Range(0.1f, 0.13f);
        for (int i = firstMineStart.x; i < firstMineStart.x + 128; i++)
        {
            for (int j = firstMineStart.y; j < firstMineStart.y + 128; j++)
            {
                if (Random.value < Mathf.PerlinNoise(i / 3f, j / 3f) * randomCopperAmount
                    && !GameManager.Instance.HasObject(new Vector2Int(i, j))
                    && GameManager.Instance.HasBottomTile(new Vector2Int(i, j)))
                {

                    Rock newRock = Instantiate(oreTile, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Rock>();
                    newRock.position = new Vector2Int(i, j);
                    GameManager.Instance.SetObject(new Vector2Int(i, j), newRock.gameObject);
                }
            }
        }
        float randomRockAmount = Random.Range(0.3f, 0.5f);
        for (int i = firstMineStart.x; i < firstMineStart.x + 128; i++)
        {
            for (int j = firstMineStart.y; j < firstMineStart.y + 128; j++)
            {
                if (Random.value < Mathf.PerlinNoise(i / 6f, j / 6f) * randomRockAmount
                    && !GameManager.Instance.HasObject(new Vector2Int(i, j))
                    && GameManager.Instance.HasBottomTile(new Vector2Int(i, j)))
                {

                    Rock newRock = Instantiate(rockTile, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Rock>();
                    newRock.position = new Vector2Int(i, j);
                    GameManager.Instance.SetObject(new Vector2Int(i, j), newRock.gameObject);
                }
            }
        }
    }

    #endregion

}