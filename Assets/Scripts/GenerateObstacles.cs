using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour {

    public GameObject electricWall;
    public GameObject dataMine;
    public GameObject router;
    public float timeBetweenObstacle;

    public Vector3[] mineSpawnPos;

    float curTime = 0;
    int spawnCount = 0;

    private void Update() {
        if (!LevelManager.instance.gameStarted || LevelManager.instance.gameEnded) {
            return;
        }

        curTime += Time.deltaTime;

        if (curTime >= timeBetweenObstacle) {
            curTime -= timeBetweenObstacle;
            GameObject obstacle;
            if (spawnCount % 10 != 0) {
                int randomObstacle = Random.Range(0, 10);
                if (randomObstacle < 7) {
                    obstacle = Instantiate(dataMine);
                    int spawnPos = Random.Range(0, mineSpawnPos.Length);
                    obstacle.transform.position = mineSpawnPos[spawnPos];
                    if (Random.Range(0, 5) == 0) {
                        GameObject obstacle2 = Instantiate(dataMine);
                        obstacle2.transform.position = mineSpawnPos[(spawnPos + Random.Range(4, 8)) % mineSpawnPos.Length];
                        Destroy(obstacle2, 1.95f);
                    }
                } else {
                    obstacle = Instantiate(electricWall);
                    int wallPos = Random.Range(0, 3);
                    if (wallPos == 0) {
                        obstacle.transform.position = new Vector3(-1f, 1, 100);
                    } else if (wallPos == 1) {
                        obstacle.transform.position = new Vector3(1f, 1, 100);
                    } else {
                        obstacle.transform.position = new Vector3(0, 2.25f, 100);
                        obstacle.transform.Rotate(new Vector3(0, 0, 90));
                    }
                }
            } else {
                obstacle = Instantiate(router);
                timeBetweenObstacle *= 0.9f;
                int curDistance = LevelManager.instance.distance;
                int smallDistance;
                int largeDistance;
                if (curDistance == 1) {
                    smallDistance = 0;
                    largeDistance = 0;
                } else {
                    smallDistance = curDistance - Random.Range(1, 3);
                    largeDistance = curDistance + Random.Range(0, 2);
                }
                Router r = obstacle.GetComponentInChildren<Router>();
                if (Random.Range(0,2) == 0) {
                    r.leftDistance = smallDistance;
                    r.rightDistance = largeDistance;
                } else {
                    r.rightDistance = smallDistance;
                    r.leftDistance = largeDistance;
                }
            }
            spawnCount++;
            Destroy(obstacle, 1.95f);
        }
    }
}
