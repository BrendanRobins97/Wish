// File: TreeManager.cs
// Contributors: Brendan Robinson
// Date Created: 11/14/2019
// Date Last Modified: 11/14/2019

using UnityEngine;

public class TreeManager : MonoBehaviour {

    #region Fields

    public GameObject tree;
    public RectInt treeSpawnArea;

    private int numTrees;

    #endregion

    #region Methods

    private void Start() {
        //SpawnTrees();
    }

    private void SpawnTrees() {
        for (int i = treeSpawnArea.xMin; i < treeSpawnArea.xMax; i++) {
            for (int j = treeSpawnArea.yMin; j < treeSpawnArea.yMax; j++) {
                if (Random.value < 0.08f && !GameManager.Instance.HasObject(new Vector2Int(i, j))) {
                    Tree newTree = Instantiate(tree, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Tree>();
                    newTree.position = new Vector2Int(i, j);
                    GameManager.Instance.SetObject(new Vector2Int(i, j), newTree.gameObject);
                    numTrees++;
                }
            }
        }
    }

    #endregion

}