using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Object, nothing happens");
                break;
            case "Finish":
                Debug.Log("Level Complete");
                break;
            case "Fuel":
                Debug.Log("+Fuel");
                break;
            default:
                Debug.Log("-health");
                break;
        }
    }
}
