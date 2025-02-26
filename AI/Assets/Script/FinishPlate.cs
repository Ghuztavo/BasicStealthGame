using UnityEngine;

public class FinishPlate : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.WinGame();
        }
    }
}
