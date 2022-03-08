using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    private FileStream dataFileStream;
    private string dataFileName;
    private int timeLapsed;
    private int reportInterval;
    private const float SPAWN_INTERVAL = 0.3f;
    private const int MAX_BODIES_IN_SCENE = 60;
    private const int MAX_MASS_IN_UNIVERSE = 100000;

    // Start is called before the first frame update
    void Start()
    {
        // Creates a new data file based o ndate and time
        string dateTime = System.DateTime.Now.ToString("dd-MMM_hh-mm-tt");
        dataFileName = "Saved_data_" + dateTime + ".csv";
        using (dataFileStream = new FileStream(dataFileName, FileMode.Append, FileAccess.Write))

        // Adds header to data file
        using (StreamWriter sw = new StreamWriter(dataFileStream))
        {
            sw.WriteLine("Time Lapsed (s), Asteroids in Universe, Total Mass (kg), Average Mass (kg)," +
                         " Most Massive (kg), Average Radius (km), Largest Radius (km)");
        }

        // Begins the asteroid spawn after 1 second delay
        InvokeRepeating(nameof(SpawnAsteroid), 1, SPAWN_INTERVAL);

        // Begins write to file after 1 seconds and repeating every 1 second
        reportInterval = 1;
        InvokeRepeating(nameof(WriteToFile), 1, reportInterval);
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(1000, 1400), 0, Random.Range(-800, 300));

        // Instantiates asteroid and writes new asteroid data to file
        if(Body.Bodies.Count < MAX_BODIES_IN_SCENE && calculateMassInScene() < MAX_MASS_IN_UNIVERSE)
        {
            Instantiate(asteroidPrefab, spawnPosition, asteroidPrefab.transform.rotation);
        }
    }

    private float calculateMassInScene()
    {
        float totalMassInUniverse = 0;
        foreach (Body body in Body.Bodies)
        {
            if (body.gameObject.CompareTag("Asteroid"))
            {
                totalMassInUniverse += body.gameObject.GetComponent<Rigidbody>().mass;
            }
        }
        return totalMassInUniverse;
    }

    private void WriteToFile()
    {
        // Sets time to invoke repeat
        timeLapsed += reportInterval;

        // Sets values for data file
        int asteroidsInUniverse = Body.Bodies.Count - 1;
        float totalMass = calculateMassInScene();
        float averageMass = totalMass / (asteroidsInUniverse);
        float mostMassive = 0;
        float largestRadius = 0;
        float sumOfRadii = 0;
        
        foreach (Body body in Body.Bodies)
        {
            if (body.gameObject.CompareTag("Asteroid"))
            {
                float massThisBody = body.gameObject.GetComponent<Rigidbody>().mass;
                float radiusThisBody = body.gameObject.transform.localScale.magnitude;
                sumOfRadii += radiusThisBody;
                if (massThisBody > mostMassive)
                {
                    mostMassive = massThisBody;
                }

                if (radiusThisBody > largestRadius && body.gameObject.CompareTag("Asteroid"))
                {
                    largestRadius = radiusThisBody;
                }
            }
        }

        float averageRadius = sumOfRadii / asteroidsInUniverse;

        // This code print the data out to an external file
        using (dataFileStream = new FileStream(dataFileName, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(dataFileStream))
        {
            sw.WriteLine(timeLapsed.ToString() + "," +
                         asteroidsInUniverse.ToString() + "," +
                         totalMass.ToString() + "," +
                         averageMass.ToString() + "," +
                         mostMassive.ToString() + "," + 
                         averageRadius.ToString() + "," +
                         largestRadius.ToString());
        }
    }
}
