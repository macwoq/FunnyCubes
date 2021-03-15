using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    CubeColors cubeColors;
    [SerializeField]MinMax minMax;
    public float speed = 2;
    private int direction = 1;
    public bool xMove, yMove, zMove;
    float mid;
    Vector3 startPos = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        cubeColors = FindObjectOfType<CubeColors>();
        cubeColors.isRandom = false;
        cubeColors.smoothBW = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (zMove)
        {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, minMax.maxZ));
        }
        if (yMove)
        {
            
            transform.Translate(transform.position.x, direction * speed * Time.deltaTime, transform.position.z);
            if (transform.position.y >= minMax.maxY)
            {
                direction = -1;
                transform.Translate(transform.position.x, direction * speed * Time.deltaTime, transform.position.z);


            }
            else if (transform.position.y <= minMax.minY)
            {
                transform.Translate(transform.position.x, direction * speed * Time.deltaTime, transform.position.z);
                direction = 1;                
            }
            
        }

        if (xMove)
        {
            Vector3 v = startPos;
            v.x += minMax.maxX * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }
    }

    public void StartX()
    {
        transform.position = startPos;
        xMove = true;
        yMove = false;
        zMove = false;
    }

    public void StartY()
    {
        transform.position = startPos;
        xMove = false;
        yMove = true;
        zMove = false;
    }
    public void StartZ()
    {
        transform.position = startPos;
        xMove = false;
        yMove = false;
        zMove = true;
    }
}

[System.Serializable]
public class MinMax
{
    public float maxX = 5, maxY = 3, maxZ = 6;
    public float minX = -5, minY = -2, minZ = 0;
} 
