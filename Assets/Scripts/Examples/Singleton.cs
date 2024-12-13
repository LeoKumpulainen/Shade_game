using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T m_instance;


    /// Usage is unsafe since instance can't be guaranteed and will cause exceptions. Try using obtain() method for safe access!
    public static T Instance {
        get {
            if (m_instance == null) {
                m_instance = FindObjectOfType<T>();
            }

            return m_instance;
        }
        private set => m_instance = value;
    }

    protected bool initialized;

    [SerializeField] protected bool m_initializeOnAwake = false;


    /// <summary>
    /// Safe accessor to instance. Does nothing if instance does not exist, and instead makes a log
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool Obtain(out T result) {
        result = Instance;
        if (Instance == null) {
            Debug.LogWarning($"Instance of {typeof(T).Name} cannot be obtained");
            return false;
        }

        return true;
    }


    protected virtual void Awake() {
        if (m_instance == null) {
            m_instance = this as T;
        }
        else if (m_instance != this) {
            Destroy(gameObject);

            return;
        }

        if (m_initializeOnAwake) {
            Initialize();
        }
    }


    protected virtual void OnDestroy() {
        if (Instance == this) {
            Deinitialize();
            Instance = null;
        }
    }


    public virtual bool Initialize() {
        if (initialized) {
            return false;
        }

        initialized = true;

        return true;
    }


    public virtual void Deinitialize() {
        if (!initialized) {
            return;
        }

        initialized = false;
    }
}


public static class SingletonHelper {
    public static T Create<T>(bool initialize, bool dontDestroyOnLoad = false) where T : Singleton<T> {
        GameObject go = new GameObject(typeof(T).ToString());
        T instance = go.AddComponent<T>();

        if (dontDestroyOnLoad) {
            Object.DontDestroyOnLoad(go);
        }

        if (initialize) {
            instance.Initialize();
        }

        return instance;
    }
}