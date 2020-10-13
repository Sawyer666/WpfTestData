using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfTestData.View.UCUpdateData
{
    public partial class UCUpdateData : UserControl
    {
        public static readonly DependencyProperty RecordNameProperty =
            DependencyProperty.Register("RecordName", typeof(string), typeof(UCUpdateData), new PropertyMetadata(String.Empty));
        public string RecordName
        {
            get { return (string)GetValue(RecordNameProperty); }
            set { SetValue(RecordNameProperty, value); }
        }

        public static readonly DependencyProperty RecordSurnameProperty =
    DependencyProperty.Register("RecordSurname", typeof(string), typeof(UCUpdateData), new PropertyMetadata(String.Empty));
        public string RecordSurname
        {
            get { return (string)GetValue(RecordSurnameProperty); }
            set { SetValue(RecordSurnameProperty, value); }
        }

        public static readonly DependencyProperty RecordPatronymicProperty =
          DependencyProperty.Register("RecordPatronymic", typeof(string), typeof(UCUpdateData), new PropertyMetadata(String.Empty));
        public string RecordPatronymic
        {
            get { return (string)GetValue(RecordPatronymicProperty); }
            set { SetValue(RecordPatronymicProperty, value); }
        }

        public static readonly DependencyProperty RecordEmailProperty =
           DependencyProperty.Register("RecordEmail", typeof(string), typeof(UCUpdateData), new PropertyMetadata(String.Empty));
        public string RecordEmail
        {
            get { return (string)GetValue(RecordEmailProperty); }
            set { SetValue(RecordEmailProperty, value); }
        }

        public UCUpdateData()
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

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            if (VM == null)
                return;
            if (Validation.GetHasError((FrameworkElement)sender))
            {
                switch (((ValidationErrorEventArgs)e).Action)
                {
                    case ValidationErrorEventAction.Added:
                        {
                            VM.Validate(true);
                            break;
                        }
                }
            }
            else
            {
                VM.Validate(false);
            }
        }
    }
}
