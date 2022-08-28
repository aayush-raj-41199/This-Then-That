using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool inverse = false;
    public GameObject normalBlock, invisibleBlock;
    List<GameObject> normalBlocks;
    List<GameObject> invisbleBlocks;

    // Start is called before the first frame update
    void Start()
    {
        normalBlocks = new List<GameObject>();
        invisbleBlocks = new List<GameObject>();
        spawnPlatforms(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnPlatforms(int platformCount) {
        float y = -2.5f, x = -2f;
        int blockCountInRow = 5;
        for (int i = 0; i < platformCount; i++) {
            x = -2f;
            for (int j = 0; j < blockCountInRow; j++)
            {
                int c = Random.Range(1, 50) % 2;
                Vector3 pos = new Vector3(x, y, 0);
                GameObject block = Instantiate(c == 0 ? normalBlock : invisibleBlock, pos, new Quaternion());
                x += 1;
                if (c == 0)
                {
                    normalBlocks.Add(block);
                }
                else
                {
                    invisbleBlocks.Add(block);
                }
            }
            y += 2.5f;
        }
    }

    void switchBlockColliders() {
        foreach(GameObject gameObject in normalBlocks) {
            gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;
        }
        foreach (GameObject gameObject in invisbleBlocks)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;
        }
    }
}
