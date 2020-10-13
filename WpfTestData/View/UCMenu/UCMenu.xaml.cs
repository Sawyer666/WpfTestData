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

namespace WpfTestData.View.UCMenu
{
    /// <summary>
    /// Interaction logic for UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        public static readonly DependencyProperty EnableInsertProperty =
            DependencyProperty.Register("EnableInsert", typeof(bool), typeof(UCMenu), new PropertyMetadata(true));
        public bool EnableInsert
        {
            get { return (bool)GetValue(EnableInsertProperty); }
            set { SetValue(EnableInsertProperty, value); }
        }

        public static readonly DependencyProperty EnableSelectedProperty =
           DependencyProperty.Register("EnableSelected", typeof(bool), typeof(UCMenu), new PropertyMetadata(true));
        public bool EnableSelected
        {
            get { return (bool)GetValue(EnableSelectedProperty); }
            set { SetValue(EnableSelectedProperty, value); }
        }

        public static readonly DependencyProperty EnableRemoveProperty =
         DependencyProperty.Register("EnableRemove", typeof(bool), typeof(UCMenu), new PropertyMetadata(true));
        public bool EnableRemove
        {
            get { return (bool)GetValue(EnableRemoveProperty); }
            set { SetValue(EnableRemoveProperty, value); }
        }

        public UCMenu()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VM == null)
                return;
            VM.GetRecordList();
            VM.EnableUpdateMode = true;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (VM == null)
                return;
            await VM.RemoveRecord();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (VM == null)
                return;
            await VM.SaveRecord();
        }
    }
}
