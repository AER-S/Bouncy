using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T: GenericSingleton<T>
{
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
                if(Instance) Destroy(gameObject);
                else
                {
                        Instance = (T)this;
                }
        }
}