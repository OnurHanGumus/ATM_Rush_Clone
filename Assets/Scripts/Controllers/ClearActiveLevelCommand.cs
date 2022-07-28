using UnityEngine;

namespace Controllers
{
    public class ClearActiveLevelCommand : MonoBehaviour
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            Debug.Log("destroyed");
            Destroy(levelHolder.GetChild(0).gameObject);

        }
    }
}