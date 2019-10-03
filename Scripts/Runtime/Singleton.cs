using UnityEngine;

namespace CMTV.DataPersistence
{
    public abstract class Singleton<TSingleton> : MonoBehaviour where TSingleton : Singleton<TSingleton>
    {
        public virtual string GameObjectName()
        {
            return typeof(TSingleton).Name;
        }

        public virtual void Init() {}

        static TSingleton _instance;
        public static TSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("Singleton");

                    _instance = singleton.AddComponent<TSingleton>();
                    singleton.name = _instance.GameObjectName();
                    _instance.Init();

                    DontDestroyOnLoad(singleton);
                }

                return _instance;
            }
        }
    }
}