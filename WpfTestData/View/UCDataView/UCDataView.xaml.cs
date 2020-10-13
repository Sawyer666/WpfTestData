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
using WpfTestData.ViewModel;

namespace WpfTestData.View.UCDataView
{
    /// <summary>
    /// Interaction logic for UCDataView.xaml
    /// </summary>
    public partial class UCDataView : UserControl
    {
        public UCDataView()
        {
            InitializeComponent();
        }

        public MainViewModel VM
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.StylusDevice == null)
            {
                FrameworkElement button = sender as FrameworkElement;
                if (button == null)
                    return;
                VisualStateManager.GoToState(button, "PressedState", true);
            }
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.StylusDevice == null)
            {
                FrameworkElement button = sender as FrameworkElement;
                if (button == null)
                    return;
                VisualStateManager.GoToState(button, "NormalState", true);
                //  ViewModel.SelectedVisual = button.DataContext as BaseDescriptor;
                // StartModuleProcess();
            }
        }

        private void Button_PreviewStylusButtonDown(object sender, StylusButtonEventArgs e)
        {
            FrameworkElement button = sender as FrameworkElement;
            if (button == null || VM == null)
                return;
            VisualStateManager.GoToState(button, "PressedState", true);
            button.CaptureStylus();

            //   itemView = button.DataContext as BaseDescriptor;
            //   ViewModel.SelectedVisual = itemView;
        }

        private void Button_PreviewStylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            FrameworkElement button = sender as FrameworkElement;
            if (button == null || VM == null)
                return;
            //   VisualStateManager.GoToState(button, "NormalState", true);
            button.ReleaseStylusCapture();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FrameworkElement button = sender as FrameworkElement;
            if (button == null || VM == null)
                return;
            if (((ListBox)sender).SelectedItem != null)
                VM.EnableUpdateMode = true;
        }
    }
}
