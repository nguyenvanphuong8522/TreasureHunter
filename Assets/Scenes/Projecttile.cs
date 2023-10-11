using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttile : MonoBehaviour
{
    public GameObject tower;
    public GameObject target;

    public float speed = 10f;

    public float towerX;
    public float towerZ;
    public float targetX;
    public float targetZ;

    public float distance;
    public float nextX;
    public float nextZ;

    public float baseY;
    public float height;
    void Start()
    {
        tower = GameObject.Find("Tower3d");
        target = GameObject.Find("Target3D");
    }

    // Update is called once per frame
    void Update()
    {
        towerX = tower.transform.position.x;
        towerZ = tower.transform.position.z;
        targetX = target.transform.position.x;
        targetZ = target.transform.position.z;
        distance = Vector3.Distance(tower.transform.position, target.transform.position);
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        nextZ = Mathf.MoveTowards(transform.position.z, targetZ, speed * Time.deltaTime);

        baseY = Mathf.Lerp(tower.transform.position.y, target.transform.position.y, Vector3.Distance(new Vector3(nextX, 0, nextZ), new Vector3(towerX, 0, towerZ)) / distance);

        height = 10 * (Vector3.Distance(new Vector3(nextX, 0, nextZ), new Vector3(towerX, 0, towerZ))) * (Vector3.Distance(new Vector3(nextX, 0, nextZ), new Vector3(targetX, 0, targetZ))) / (-0.25f * distance * distance);

        Vector3 movePosition = new Vector3(nextX, baseY + height, nextZ);
        transform.position = movePosition;
    }
}
