using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.gameObject.SendMessage("AlertSystem", true);
            transform.parent.gameObject.SendMessage("RefreshPlayerTransform", other.transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.gameObject.SendMessage("AlertSystem", false);
        }
    }
}
