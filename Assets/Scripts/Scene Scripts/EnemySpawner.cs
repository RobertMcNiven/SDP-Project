using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds the responsibility of spawning a predetermined amount of enemies,
/// from a predetermined list of enemys, at predetermined locations, at a predetermined rate.
/// This class will most likely never be inherited from so everything is private.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private bool endlessSpawn = false;      // To check whether there should be a maximum amount of enemies spawned
    
    [SerializeField]
    private int totalSpawnAmount; // How many enemies will spawn from this spawner stored via Unity Editor

    [SerializeField]
    private int enemiesPerSpawn; // How many enemies will spawn during each spawning instance

    [SerializeField]
    private bool monsterSpawningMonster = false;      // To check if the spawner is actually a monster which spawns other monsters

    [SerializeField]
    private List<Transform> spawnZones = new List<Transform>(); // Dedicated spawn zone locations stored in the list via Unity Editor

    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>(); // Allowed enemy prefabs will be stored in the list via Unity Editor

    [SerializeField]
    private bool sequentialSpawnZones = false;      // Sets the spawner to choose spawn locations in order

    [SerializeField]
    private bool sequentialEnemySpawns = false;      // Sets the spawner to choose spawned enemies in order

    private bool isAwake = false;   // If the spawner is 'activated.' Player touching the collider activates it

    private int totalEnemiesSpawned = 0; // How many enemies have been spawned from this spawner

    private int enemyListSize;  // Stores the size of the enemy list to use as a bound later.
    private int spawnListSize;  // Stores the size of the spawn list to use as a bound later.

    private int chosenSpawn = 0; // This chooses a random number to pick from the spawn zones
    private int chosenEnemy = 0; // This chooses a random number to pick from the enemies array

    private GameObject enemy; // This grabs the enemy's game object from the list and stores it
    private Transform enemySpawn; // This stores the transform of the chosen spawn zone

    [SerializeField]
    private int spawnDelay = 3; // Amount of time between each enemy spawn 
    private float timer = 0.0f; // Holds how many seconds have passed
    private bool isSpawning = false; // Records when an enemy is spawning so we dont have multiple spawning each frame one is supposed to spawn.


    /// <summary>
    /// Checks for collision with other game objects and runs when it detects one.
    /// When a player enters the 'spawn zone', it will wake up the spawner and disable the collider so that it does not obstruct the player.s
    /// </summary>
    /// <param name="col"> The specific instance of collision. Automatically passed by Unity </param>
    private void OnCollisionEnter2D(Collision2D col)  // if they hit something
    {
        if (col.gameObject.tag == "Player" && !monsterSpawningMonster)      // if its a player and the spawner is not a monster 
        {
            isAwake = true;         // activates the spawner to start spawning
            gameObject.GetComponent<BoxCollider2D>().enabled = false;   // turns off the physical spawner collider
        }
    }

    // EnemySpawner() : enemyListSize(int i = enemyPrefabs.Count) {}

    /// <summary>
    /// Start is called before the first frame update. It is essentially the default constructor of Unity.
    /// This function grabs the list size of the Enemy and Spawn lists. If the spawner is a monster, it wakes the spawner.
    /// Otherwise, it maxes out the timer so that the first spawn instance triggers upon collision with the Player
    /// </summary>
    private void Start()
    {
        // this is where I could use an initialization list.... IF I HAD ONE
        enemyListSize = enemyPrefabs.Count;
        spawnListSize = spawnZones.Count;

        if (monsterSpawningMonster)
            isAwake = true;             // monsters will start already spawning monsters
        else
            timer = (float)spawnDelay;     //  makes it so the spawner will spawn an enemy immediately upon awaking
    }

    /// <summary>
    /// Update is called once every frame. Used for non-physics operations.
    /// This function manages the timer and once it has delayed for the specified amount, 
    /// it sets the spawner to start spawning
    /// </summary>
    private void Update()
    {
        if (isAwake)
        {
            timer += Time.deltaTime;        // each frame, add how much time has passed.

            if (!isSpawning)
            {
                if (timer > spawnDelay)  // if we should not be currently delaying,
                {
                    timer = 0.0f;   // reset timer
                    isSpawning = true;
                }
            }
        }
    }
    
    // FixedUpdate is called at a fixed interval, not always once per frame.
    /// <summary>
    /// FixedUpdate is called at a fixed interval. Used for physics (in this case, spawning game objects).
    /// This function checks to see if the spawner is set to spawn. Then it does one instance of spawning, 
    /// and sets the spawner to stop spawning and wait for the timer again.
    /// </summary>
    private void FixedUpdate()
    {
        if (isSpawning)
        {
            SpawnEnemy();
            isSpawning = false;
        }
    }

    /// <summary>
    /// This functions spawns(Instantiates) enemies at the specified locations.
    /// It spawns the specified amount of enemies and increments the total amount of enemies spawned.
    /// If endlessSpawn is set, it will ignore the limit on set on the total amount of enemies spawned.
    /// </summary>
    private void SpawnEnemy()
    {
        if ((totalEnemiesSpawned < totalSpawnAmount) || endlessSpawn)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                ChooseSpawnZone();
                ChooseSpawnedEnemy();

                Instantiate(enemyPrefabs[chosenEnemy], spawnZones[chosenSpawn].transform.position, transform.rotation);     // creates an enemy of the chose type at the chose spawn zone's location

                totalEnemiesSpawned++;
            }
        }
    }

    /// <summary>
    /// This function checks to see if spawn zones are randomized. If not, then it picks a spawn zone from the list sequentially.
    /// Otherwise, it picks a random spawn zone.
    /// </summary>
    private void ChooseSpawnZone()
    {
        if (sequentialSpawnZones)
        {
            if (chosenSpawn < spawnListSize - 1)
                chosenSpawn++;
            else
                chosenSpawn = 0;
        }
        else
            chosenSpawn = Random.Range(0, spawnListSize);   // grabs a random index of the list
    }

    /// <summary>
    /// This function checks to see if spawned enemies are randomized. If not, then it picks an enemy from the list sequentially.
    /// Otherwise, it picks a random enemy.
    /// </summary>
    private void ChooseSpawnedEnemy()
    {
        if (sequentialEnemySpawns)
        {
            if (chosenEnemy < enemyListSize - 1)
                chosenEnemy++;
            else
                chosenEnemy = 0;
        }
        else
            chosenEnemy = Random.Range(0, enemyListSize);   // grabs a random index of the list
    }
}
