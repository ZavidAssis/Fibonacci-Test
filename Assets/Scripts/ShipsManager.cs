using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : SingletonBase<ShipsManager>
{
    [Header("Prefab of ship")]
    [SerializeField]
    private Ship shipPrefab;

    [Header("Spawn Stats")]
    [SerializeField]
    private float timerBtwinSpawns;
    [SerializeField]
    private float distanceBetweenObjects;

    [Header("SpawnArea")]
    [SerializeField]
    private Transform spawnPos;



    //aux vars
    private PoolBase<Ship> shipPool; //pool Component
    private List<Ship> shipsAlive = new List<Ship>(); //list of all alive Ships
    private int lastNumber, lastSecondNumber;//saves the last two Fibonacci Number
    private Vector2 spawnSize;//area to spawn ships
    public List<Ship> ShipsAlive { get => shipsAlive; set => shipsAlive = value; }


    private void Start()
    {
        setSpawnArea();

        lastNumber = 1; //set fibonacci startint number

        shipPool = new PoolBase<Ship>(shipPrefab, 3); //start the ship pool

        InvokeRepeating("destroyLastShip", 1, 1); //start destroy ship everysecond method

        StartCoroutine(recursiveSpawnShips(0, 1));//start first ship spawn
    }
    #region ShipsSpawner
    private void setSpawnArea()//set size of randomizer spawnArea
    {
        Vector3 leftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, Camera.main.nearClipPlane));
        Vector3 rightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, Camera.main.nearClipPlane));
        float distance = Vector3.Distance(leftPoint, rightPoint);
        spawnSize.x = distance * .9f;

        Vector3 downPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, Camera.main.nearClipPlane));
        spawnPos.position = downPoint;
        spawnSize.y = 0;
    }
    private IEnumerator recursiveSpawnShips(int lastN, int index)//spawn ships
    {
        if (index == 0)
        {
            int currentNumber = lastNumber + lastN;
            lastSecondNumber = lastN;
            lastNumber = currentNumber;
        }
        else
        {
            Ship newShip = shipPool.GetObjectFromPool();
            newShip.transform.position = randompos();
            shipsAlive.Add(newShip);
            yield return new WaitForEndOfFrame();
            StartCoroutine(recursiveSpawnShips(lastN, index - 1));
        }
    }
    Vector3 randompos()//return a random pos in the camsizedown
    {
        Vector2 pos = new Vector2(Random.Range(-spawnSize.x / 2, spawnSize.x / 2), Random.Range(-spawnSize.y / 2, spawnSize.y / 2));
        pos += (Vector2)spawnPos.transform.position;
        return pos;
    }
    private void OnDrawGizmos()//draw the range of random positions
    {
        Gizmos.DrawWireCube(spawnPos.transform.position, spawnSize);
    }
    #endregion
    #region ShipsDestroy
    private void destroyLastShip()//destroy ships every second
    {
        if (shipsAlive.Count > 0)
        {
            shipsAlive[0].gameObject.SetActive(false);
            shipsAlive.RemoveAt(0);
        }
        else
        {
            StartCoroutine(recursiveSpawnShips(lastNumber - lastSecondNumber, lastNumber));
            UIManager.Instance.attFinobacciNumber(lastNumber);
        }
    }
    #endregion
}
