using UnityEngine;
using UnityEngine.SceneManagement;

public class KillVolume : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.RestartGame();
        }
    }
}

