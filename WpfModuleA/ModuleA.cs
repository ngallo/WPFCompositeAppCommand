using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace WpfModuleA
{
    public class ModuleA : IModule
    {
        //Fields

        private readonly IRegionViewRegistry _regionViewRegistry;

        //Constructors

        public ModuleA(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        //Methods

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion("ViewContainer", typeof (ViewA));
        }
    }
}
