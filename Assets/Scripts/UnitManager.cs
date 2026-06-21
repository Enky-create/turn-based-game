using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitManager "
                + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
