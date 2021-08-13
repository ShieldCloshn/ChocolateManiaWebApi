
using ChocolateMania.DI.Shop;
using SimpleInjector;

namespace ChocolateMania.Settings
{
    public class Configuration
    {
        public Container Container { get; }

        public Configuration()
        {
            Container = new Container();

            Setup();
        }

        public virtual void Setup()
        {
            Container.Register<IShop, Shop>(Lifestyle.Transient);
        }
    }
}
