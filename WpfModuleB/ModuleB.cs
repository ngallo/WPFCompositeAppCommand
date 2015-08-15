using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace WpfModuleB
{
    public class ModuleB : IModule
    {
        //Fields

        private readonly IRegionViewRegistry _regionViewRegistry;

        //Constructors

        public ModuleB(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        //Methods

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion("ViewContainer", typeof(ViewB));
        }
    }
}
