using DataAPI;
using DataAPI.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfTestData.Command;
using WpfTestData.Model;

namespace WpfTestData.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private DataAPIBase dataAPI = null;
        private DataModel selectedDataModel = null;
        private ObservableCollection<DataModel> data;
        private static object _lock = new object();
        private bool enableSelectedMode = false;
        private bool enableUpdateedMode = false;

        public ObservableCollection<DataModel> DataList
        {
            get
            {
                return data;
            }
        }

        public MainViewModel()
        {
            SelectedDataModel = new DataModel();
            data = new ObservableCollection<DataModel>();
            BindingOperations.EnableCollectionSynchronization(data, _lock);
        }

        #region Public Properties

        public bool EnableSelectedMode
        {
            get
            {
                return enableSelectedMode;
            }
            set
            {
                Set<bool>(() => this.EnableSelectedMode, ref enableSelectedMode, value);
            }
        }

        public bool EnableUpdateMode
        {
            get
            {
                return enableUpdateedMode;
            }
            set
            {
                Set<bool>(() => this.EnableUpdateMode, ref enableUpdateedMode, value);
            }
        }

        #endregion

        #region public
        /// <summary>
        /// Selected record in visual control...
        /// </summary>
        public DataModel SelectedDataModel
        {
            get
            {
                return selectedDataModel;
            }
            set
            {
                selectedDataModel = value;
                EnableSelectedMode = true;
                RaisePropertyChanged("SelectedDataModel");
            }
        }
        /// <summary>
        /// set data type...
        /// </summary>
        /// <param name="db"></param>
        public void SetDataAbility(DataAPIBase db)
        {
            dataAPI = db;
        }
        /// <summary>
        /// get all data
        /// </summary>
        public void GetRecordList()
        {
            var dataRecords = new GetDataCommand(dataAPI).Execute();
            if (dataRecords == null)
                return;
            data.Clear();
            EnableSelectedMode = false;

            dataRecords.ForEach((item) =>
            {
                data.Add(new DataModel(item));
            });

            this.RaisePropertyChanged(() => this.DataList);
             SelectedDataModel = new DataModel();
        }
        /// <summary>
        /// Save selected record
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveRecord()
        {
            return await Task.Factory.StartNew(() =>
            {
                if (SelectedDataModel == null)
                    return false;
                bool cmd_res = new RemoveDataCommand(dataAPI, SelectedDataModel.Id).Execute();
                if (cmd_res)
                {
                    GetRecordList();
                    EnableUpdateMode = false;
                }
                return cmd_res;
            });
        }
        /// <summary>
        /// Add new record or Save update record
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveRecord()
        {
            return await Task.Factory.StartNew(() =>
            {
                if (!Validate(hasErrors))
                {
                    MessageBox.Show("Ошибка валидации данных!");
                    return false;
                }
                bool cmd_res = false;
                if (DataList == null)
                    return false;
                var ia = DataList.Where(i => i.Id == SelectedDataModel.Id).FirstOrDefault();
                if (ia == null)
                    cmd_res = new InsertDataCommand(dataAPI, new DataRecord(SelectedDataModel.Name, SelectedDataModel.Surname, SelectedDataModel.Patronymic, SelectedDataModel.Email)).Execute();
                else
                    cmd_res = new UpdateDataCommand(dataAPI, new DataRecord(SelectedDataModel.Id, SelectedDataModel.Name, SelectedDataModel.Surname, SelectedDataModel.Patronymic, SelectedDataModel.Email)).Execute();
                if (cmd_res)
                {
                    GetRecordList();
                    MessageBox.Show("Данные успешно обновлены");
                }
                return cmd_res;
            });
        }

        bool hasErrors = false;
        public bool Validate(bool errors)
        {
            hasErrors = errors;
            if (hasErrors || SelectedDataModel == null)
            {
                EnableUpdateMode = false;
                return false;
            }
            if (!SelectedDataModel.EmptyData() && !errors)
            {
                EnableUpdateMode = true;
                return true;
            }
            return false;
        }

        #endregion

        #region private

        #endregion
    }
}