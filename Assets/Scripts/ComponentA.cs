using UnityEngine;
using Tools;

public class ComponentA : MonoBehaviour
{
    /// <summary>
    /// Information sur l objet courrant
    /// Ou ses enfants
    /// Pas d objet exterieur
    /// </summary>
    private void Awake()
    {
        MDebug.Log("Awake", this);
    }

    /// <summary>
    /// A chaque fois qu on active
    /// le component
    /// </summary>
    private void OnEnable()
    {
        MDebug.Log("OnEnable", this);
    }

    /// <summary>
    /// A chaque fois qu on desactive
    /// le component
    /// </summary>
    private void OnDisable()
    {
        MDebug.Log("OnDisable", this);
    }

    private void OnDestroy()
    {
        MDebug.Log("OnDestroy", this);
    }

    /// <summary>
    /// Quand l application est en pause
    /// N est pas appele avec la pause unity !
    /// Appeler a la frame 0
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        MDebug.Log("OnApplicationPause", this);
    }

    private void OnApplicationQuit()
    {
        MDebug.Log("OnApplicationQuit", this);
    }

    /// <summary>
    /// Call une seule fois
    /// Initialiser l objet
    /// On peut se baser sur des
    /// objets exterieurs
    /// tous les awakes on ete calls
    /// Appeler une frame apres le onEnable()
    /// </summary>
    void Start()
    {
        MDebug.Log("Start", this);
    }

    /// <summary>
    /// Mise a jour
    /// Un par frame
    /// </summary>
    void Update()
    {
        MDebug.Log("Update", this);
    }

    /// <summary>
    /// Nombre d appel gere dans:
    /// Project Settings -> Time -> Fixed Timestep
    /// </summary>
    private void FixedUpdate()
    {
        MDebug.Log("FixedUpdate", this);
    }

    /// <summary>
    /// Tout comme update mais apres
    /// tous les autres updates
    /// Souvent pour mettre a jour la
    /// position de la camera (?)
    /// </summary>
    private void LateUpdate()
    {
        MDebug.Log("LateUpdate", this);
    }
}
