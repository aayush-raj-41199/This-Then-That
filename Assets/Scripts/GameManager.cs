using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool inverse = false;
    public GameObject normalBlock, invisibleBlock;
    public Text titleText;
    List<GameObject> normalBlocks;
    List<GameObject> invisbleBlocks;

    // Start is called before the first frame update
    void Start()
    {
        normalBlocks = new List<GameObject>();
        invisbleBlocks = new List<GameObject>();
        StartCoroutine(SpawnPlatforms(100, 5));
        StartCoroutine(SituationChanger(20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SituationChanger(int seconds) 
    {
        while (true)
        {
            int rand = Random.Range(0, 100) % 2;
            switch (rand) {
                case 0:
                    inverse = false;
                    switchBlockColliders(false);
                    titleText.text = "THIS";
                    break;
                case 1:
                    inverse = true;
                    switchBlockColliders(true);
                    titleText.text = "THEN";
                    break;
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator SpawnPlatforms(int platformCount, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        float y = -2.5f, x = -2f;
        int blockCountInRow = 5;
        for (int i = 0; i < platformCount; i++) {
            x = -2f;
            int s = 0;
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
                s += c;
            }
            if (s == 0)
            {
                // remove last 5 normal blocks
                normalBlocks.RemoveRange(normalBlocks.Count - 6, 5);
            }
            else if (s == 5) 
            {
                // remove last 5 invisble blocks
                normalBlocks.RemoveRange(normalBlocks.Count - 6, 5);
            }
            else
            {
                y += 2.5f;
            }
        }
    }

    void switchBlockColliders(bool normal) 
    {
        foreach(GameObject gameObject in normalBlocks) {
            gameObject.GetComponent<BoxCollider2D>().enabled = normal;
        }
        foreach (GameObject gameObject in invisbleBlocks)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = !normal;
        }
    }
}
