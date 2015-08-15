
namespace WpfModuleA
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA
    {
        //Constructors

        public ViewA(ViewAVm viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
