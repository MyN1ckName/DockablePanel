using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Interop;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using Autodesk.Windows;

using Revit.Async;

// https://github.com/jeremytammik/the_building_coder_samples/blob/49720c6f8042b1afd4b3aaae1ebade7dd27b6cb9/BuildingCoder/CmdSelectionChanged.cs

namespace DockablePanel
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class Command : IExternalCommand
    {
        private static UIApplication _uiapp;
        private static UIDocument _uidoc;
        private static Views.MainView _window;

        public Result Execute(ExternalCommandData commandData,
                    ref string messege,
                    ElementSet elements)
        {
            try
            {
                _uiapp = commandData.Application;
                _uidoc = _uiapp.ActiveUIDocument;


                // using (TransactionGroup tg = new TransactionGroup(_uidoc.Document, "DockablePanel"))
                // {
                //     tg.Start();
                // 
                //     _window = new Views.MainView()
                //     {
                //         DataContext = new ViewModels.MainViewModel(),
                //     };
                //     WindowInteropHelper helper = new WindowInteropHelper(_window);
                //     helper.Owner = commandData.Application.MainWindowHandle;
                //     _window.Show();
                // 
                //     tg.Assimilate();
                // }
                // 
                // _uiapp.Idling += new EventHandler<IdlingEventArgs>(OnIdling);

                if (DockablePane.PaneIsRegistered(Views.Control.PaneId))
                {
                    DockablePane myCustomPane = _uiapp.GetDockablePane(Views.Control.PaneId);

                    if (myCustomPane.IsShown())
                    {
                        myCustomPane.Hide();
                    }
                    else
                    {
                        myCustomPane.Show();
                    }
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            catch (Exception ex)
            {
                messege = $"{ex.Message} \r\n {ex.TargetSite}";
                return Result.Failed;
            }
        }

        private void OnIdling(object sender, IdlingEventArgs e)
        {

            UIApplication uiapp = sender as UIApplication;

            if (uiapp != null)
            {
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;

                Selection selection = uidoc.Selection;

                var viewModel = _window.DataContext as ViewModels.MainViewModel;

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
