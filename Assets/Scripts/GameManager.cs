using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool inverse = false;
    public GameObject normalBlock, invisibleBlock, player, playerhead;
    public Text titleText, scoreText;
    List<GameObject> normalBlocks;
    List<GameObject> invisbleBlocks;
    bool toggle = false;
    float highestY = -100, startY=0;
    public bool startAnimationFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        normalBlocks = new List<GameObject>();
        invisbleBlocks = new List<GameObject>();
        StartCoroutine(SpawnPlatforms(100, 5));
    }

    void setStartAnimationFinished()
    {
        startAnimationFinished = true;
        startY = playerhead.transform.position.y;
        player.GetComponent<Movement>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (startAnimationFinished)
        {
            highestY = Mathf.Max(playerhead.transform.position.y, highestY);
            scoreText.text = "" + (int)((highestY - startY)/2);
        }
    }

    IEnumerator SituationChanger(int seconds) 
    {
        while (true)
        {
            if (toggle)
            {
                titleText.text = "THIS";
                inverse = false;
                switchBlockColliders(false);
            }
            else
            {
                titleText.text = "THEN";
                inverse = true;
                switchBlockColliders(true);
            }
            toggle = !toggle;
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator SpawnPlatforms(int platformCount, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        titleText.gameObject.SetActive(true);
        setStartAnimationFinished();
        StartCoroutine(SituationChanger(20));
        float y = -2.5f, x = -2f;
        int blockCountInRow = 5;
        for (int i = 0; i < platformCount; i++) {
            x = -2f;
            int s = 0;
            int j = 0;
            while (j < blockCountInRow)
            {
                int c = Random.Range(1, 50) % 2;
                Vector3 pos = new Vector3(x, y, 0);
                GameObject block = Instantiate(c == 0 ? normalBlock : invisibleBlock, pos, new Quaternion());
                if (c == 0)
                {
                    normalBlocks.Add(block);
                }
                else
                {
                    invisbleBlocks.Add(block);
                }
                s += c;
                if (s == 0 && normalBlocks.Count >= blockCountInRow)
                {
                    // remove last 5 normal blocks
                    normalBlocks.RemoveRange(normalBlocks.Count - blockCountInRow, blockCountInRow);
                }
                else if (s == blockCountInRow && invisbleBlocks.Count >= blockCountInRow)
                {
                    // remove last 5 invisble blocks
                    invisbleBlocks.RemoveRange(invisbleBlocks.Count - blockCountInRow, blockCountInRow);
                }
                else
                {
                    x += 1;
                    j++;
                    continue;
                }
                x = -2f;
                s = 0;
                j = 0;
            }
            y += 2.5f;

        }
    }

    void switchBlockColliders(bool normal) 
    {
        player.layer = LayerMask.NameToLayer(normal ? "PlayerParts" : "PlayerPartsInvisible");
        foreach (Transform child in player.transform) { 
            child.gameObject.layer = LayerMask.NameToLayer(normal ? "PlayerParts" : "PlayerPartsInvisible");
        }
    }
}
