
namespace WpfModuleB
{
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    public partial class ViewB
    {
        //Constructors

        public ViewB(ViewBVm viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
