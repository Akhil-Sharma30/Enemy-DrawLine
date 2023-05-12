using UnityEngine;

public class EscapeAnalyzer : MonoBehaviour
{
    public GameObject playerCube; // Reference to the player cube
    public LineRenderer lineRenderer; // Reference to the LineRenderer component

    public void SetTheLine()
    {
        // Get all enemy cubes in the scene
        GameObject[] enemyCubes = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Found " + enemyCubes.Length + " enemy cubes.");

        // Calculate the best escape direction
        Vector3 bestDirection = CalculateBestEscapeDirection(enemyCubes);
        Debug.Log("Best escape direction: " + bestDirection);

        // Draw a line from the player cube to indicate the best direction
        DrawEscapeLine(bestDirection);
    }

    Vector3 CalculateBestEscapeDirection(GameObject[] enemyCubes)
    {
        Vector3 bestDirection = Vector3.zero;
        float bestDistance = Mathf.Infinity;

        foreach (GameObject enemyCube in enemyCubes)
        {
            Vector3 direction = enemyCube.transform.position - playerCube.transform.position;
            float distance = direction.magnitude;

            // Check if the current enemy cube is closer than the previous best distance
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestDirection = direction.normalized;
            }
        }

        return bestDirection;
    }

    void DrawEscapeLine(Vector3 direction)
    {
        Vector3 startPosition = playerCube.transform.position;
        Vector3 endPosition = startPosition + direction * 10f;

        // Set the positions for the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        Debug.Log("Drawing escape line from " + startPosition + " to " + endPosition);
    }
}
