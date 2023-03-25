using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //aux vars
    private float speedMult;//ship unique speed multiplier

    private float speedBase;//speed of this ship
    private void OnEnable()
    {
        speedMult = Random.Range(.9f, 1.1f);//randomize speed

        speedBase = SpeedManager.Instance.Speed * speedMult;//set speed

        UIManager.Instance.attShipsNumber();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speedBase); //move forward (rigidibody removed because transform is more efficient)
    }
    private void OnBecameInvisible()//self destroys
    {
        if (this.gameObject.activeInHierarchy)
        {
            ShipsManager.Instance.ShipsAlive.Remove(this);
            gameObject.SetActive(false);
        }
    }
}
