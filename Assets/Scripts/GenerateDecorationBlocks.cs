using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDecorationBlocks : MonoBehaviour {

    public float timeBetweenSpawns;
    public GameObject dataBlock;

    float curTime = 0;

    private void Update() {
        if (!LevelManager.instance.gameStarted) {
            return;
        }
        curTime += Time.deltaTime;
        if (curTime >= timeBetweenSpawns) {
            curTime -= timeBetweenSpawns;
            GameObject go = Instantiate(dataBlock);
            if (Random.Range(0, 2) == 0) {
                go.transform.position = new Vector3(Random.Range(3.5f, 10f), Random.Range(-4f, 6f), 100);
            } else {
                go.transform.position = new Vector3(Random.Range(-10, 10f), Random.Range(5f, 10f), 100);
            }
            Vector3 basePos = new Vector3(0, 1, 100);
            Vector3 distance = go.transform.position;
            int xScale = 1;
            int yScale = 1;
            if (Random.Range(0, 2) == 0)
                xScale = -1;
            if (Random.Range(0, 2) == 0)
                yScale = -1;
            distance.Scale(new Vector3(xScale, yScale, 0));
            go.transform.position = basePos + distance;
            Destroy(go, 1.95f);
        }
    }

}
