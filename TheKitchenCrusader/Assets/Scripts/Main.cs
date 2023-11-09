using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    static private Main S;

    // Enemy Spaen setup
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public GameObject prefabPowerUp;
    private BoundsCheck bndCheck;
    public eWeaponType[] powerUpFrequency = new eWeaponType[]
    {
        eWeaponType.none,
        eWeaponType.mustard
    };
    /// <summary>
    /// Called by an enemy whenever its destroyed
    /// </summary>
    /// <param name="e"></param>
    static public void EnemyDestroyed(Enemy e)
    {
        // potentially generate a PowerUp
        if (Random.value <= e.powerUpDropChance)
        {
            // choose a powerup from the possibiities in powerup frequency
            int ndx = Random.Range(0, S.powerUpFrequency.Length);
            eWeaponType pUpType = S.powerUpFrequency[ndx];

            // Spawn a PowerUp
            GameObject go = Instantiate(S.prefabPowerUp);
            MustardBottle mB = go.GetComponent<MustardBottle>();
            
            // set it to the position of the destoryed enemy
            go.transform.position = e.transform.position;
        }
    }
    private void Awake()
    {
        S = this;
        // Set bndCheck to reference the BoundsCheck component on this
        // gameobject
        bndCheck = GetComponent<BoundsCheck>();

        // Invoke SpawnEnemy once (in 2 seconds, based on default values)
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        // Pike a random enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        // Position the Enemy above the screen with random x position
        float enemyInset = enemyInsetDefault;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);

            // Set the initial position for the spawned Enemy
            Vector2 pos = Vector2.zero;
            float xMin = -bndCheck.camWidth + enemyInset;
            float xMax = bndCheck.camHeight - enemyInset;

            pos.x = Random.Range(xMin,xMax);
            pos.y = bndCheck.camHeight + enemyInset;
            go.transform.position = pos;

            // Invoke Spawn Enemy again
            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Restart Level
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Scene0");
            Debug.Log("Scene Restarted");
        }
    }
}
