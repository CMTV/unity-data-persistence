using System;

namespace CMTV.DataPersistence
{
    [Serializable]
    public abstract class SingletonData
    {
        public abstract void InitDefaults();
    }
}