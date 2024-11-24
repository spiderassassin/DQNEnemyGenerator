using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject environmentPrefab;

    private void Awake()
    {
        // Singleton pattern.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame(GameObject currentEnvironment, Vector3 targetPosition)
    {
        // Destroy the current environment prefab.
        Destroy(currentEnvironment);
        // Instantiate the environment prefab at the stored position.
        Instantiate(environmentPrefab, targetPosition, Quaternion.identity);
    }
}
