using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockablePanel.Views
{
    /// <summary>
    /// Interaction logic for Control.xaml
    /// </summary>
    public partial class Control : UserControl, IDockablePaneProvider
    {
        public Control()
        {
            InitializeComponent();
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
        }

        public static DockablePaneId PaneId
        {
            get
            {
                return new DockablePaneId(new Guid("33070E79-BA49-4CFE-BC88-2340BA40EA3C"));
            }
        }

        public static string PaneName
        {
            get
            {
                return "DockablePanel";
            }
        }
    }
}
