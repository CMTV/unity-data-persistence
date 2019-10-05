namespace CMTV.DataPersistence
{
    public abstract class PSingleton<TSingleton, TSingletonData> : Singleton<TSingleton>, ISaveLoad 
        where TSingleton : PSingleton<TSingleton, TSingletonData>
        where TSingletonData : SingletonData, new()
    {
        public abstract string Path { get; }

        public TSingletonData Defaults
        {
            get
            {
                var defaultData = new TSingletonData();
                defaultData.InitDefaults();

                return defaultData;
            }
        }

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