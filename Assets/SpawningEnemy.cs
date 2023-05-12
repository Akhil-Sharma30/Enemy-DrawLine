
using UnityEngine;

public class SpawningEnemy : MonoBehaviour
{
    public GameObject playerCubePrefab; // Prefab for the player cube
    public GameObject cubePrefab; // Prefab for the cube
    public GameObject plane; // Reference to the plane object
    public float minDistance = 2f; // Minimum distance between cubes

    void Start()
    { 

        // Spawn the other cubes
        SpawnCubes();
    }

    public void SpawnCubes()
    {
        // Destroy any existing enemy objects in the scene
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in existingEnemies)
        {
            Destroy(enemy);
        }


        for (int i = 0; i < 10; i++)
        {
            // Get a random position on the planes
            Vector3 position = GetRandomPositionOnPlane();

            // Check if the position is valid (far enough from the player cube)
            if (Vector3.Distance(position, playerCubePrefab.transform.position) >= minDistance)
            {
                // Instantiate the cube at the valid position
                Instantiate(cubePrefab, position, Quaternion.identity);
            }
            else
            {
                // Try again with a new position
                i--;
            }
        }
    }

    Vector3 GetRandomPositionOnPlane()
    {
        // Get the bounds of the plane
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        Vector3 planeBounds = planeRenderer.bounds.size;

        // Generate a random position within the bounds of the plane
        float x = Random.Range(plane.transform.position.x - planeBounds.x / 2f, plane.transform.position.x + planeBounds.x / 2f);
        float z = Random.Range(plane.transform.position.z - planeBounds.z / 2f, plane.transform.position.z + planeBounds.z / 2f);

        // Set the Y position to be slightly above the plane to avoid clipping
        float y = plane.transform.position.y + 0.1f;

        return new Vector3(x, y, z);
    }
}
