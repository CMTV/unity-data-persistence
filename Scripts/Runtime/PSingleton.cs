namespace CMTV.DataPersistence
{
    public abstract class PSingleton<TSingleton, TSingletonData> : Singleton<TSingleton>, ISaveLoad 
        where TSingleton : PSingleton<TSingleton, TSingletonData>
        where TSingletonData : SingletonData, new()
    {
        public abstract string Path { get; }

        #region Defaults

        bool defaultsCached = false;
        TSingletonData defaults;

        public TSingletonData Defaults
        {
            get
            {
                if (!defaultsCached)
                {
                    defaults = new TSingletonData();
                    defaults.InitDefaults();

                    defaultsCached = true;
                }

                return defaults;
            }
        }

        public TSingletonData NewDefaults
        {
            get
            {
                defaultsCached = false;
                return Defaults;
            }
        }

        #endregion

        protected abstract void InitData(TSingletonData data);
        protected abstract void InitSelf(TSingletonData data);

        public override void Init()
        {
            base.Init();

            Load();
        }

        protected virtual void OnLoadFailed() 
        {
            TSingletonData data = new TSingletonData();
            data.InitDefaults();

            InitSelf(data);
        }

        public void Save()
        {
            TSingletonData data = new TSingletonData();
            InitData(data);

            IOHelper.Save(Path, data);
        }

        public void Load()
        {
            TSingletonData data = (TSingletonData)IOHelper.Load(Path);

            if (data == null)
            {
                OnLoadFailed();
                return;
            }

            InitSelf(data);
        }
    }
}