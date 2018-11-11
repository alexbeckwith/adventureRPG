using System.Linq;
using UnityEngine;

class WorldManager : MonoBehaviour
{
  [SerializeField]
  private int enemyCount;
  [SerializeField]
  private int worldWidth;
  [SerializeField]
  private int worldHeight;

  private GameObject treant;

  void Awake()
  {
    treant = Resources.Load("Treant") as GameObject;

    foreach (var enemy in Enumerable.Range(0, enemyCount)) {
      var enemyInstance = Instantiate(treant, GetPosition(), Quaternion.identity);
    }
  }

  private Vector2 GetPosition()
  {
    return new Vector2(
      Random.Range(-worldWidth, worldWidth), 
      Random.Range(-worldHeight, worldHeight)
    );
  }
}
