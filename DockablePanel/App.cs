using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace DockablePanel
{
    public class App : IExternalApplication
    {
        public static Views.Control _control;
        public Result OnStartup(UIControlledApplication a)
        {

            a.Idling += OnIdling;

            _control = new Views.Control()
            {
                DataContext = new ViewModels.MainViewModel(),
            };

            if (!DockablePane.PaneIsRegistered(Views.Control.PaneId))
            {
                a.RegisterDockablePane(Views.Control.PaneId,
                    Views.Control.PaneName, _control);
            }

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        private void OnIdling(object sender, IdlingEventArgs e)
        {

            UIApplication uiapp = sender as UIApplication;

            if (uiapp != null)
            {
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;

                Selection selection = uidoc.Selection;

                var viewModel = _control.DataContext as ViewModels.MainViewModel;

                if (viewModel != null)
                {
                    if (selection.GetElementIds().Count > 0)
                    {
                        viewModel.Id = selection.GetElementIds().FirstOrDefault().ToString();
                    }
                    else
                    {
                        viewModel.Id = "no selectrion";
                    }

                }
            }
        }
    }
}
