using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using KrakowiakKozlowski.Games.BL;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.UI.WPF.ViewModel
{
    public class ProducerListViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
        private ListCollectionView _view;
        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand
        {
            get => _filterDataCommand;
        }
        public string FilterValue { get; set; }

        private DataAccess dataAccess = null;
        public ProducerListViewModel()
        {
            try
            {
                dataAccess = Singleton.Instance;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Creating DAO Failed!");
            }
            OnPropertyChanged("Producers");
            GetAllProducers();
            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Producers);
            _filterDataCommand = new RelayCommand(param => FilterData());
            _addNewProducerCommand = new RelayCommand(param => AddNewProducer(), param => CanAddNewProducer());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), param => CanSaveProducer());
            _deleteProducerCommand = new RelayCommand(param => DeleteProducer(), param => CanDeleteProducer());
            EditedProducer = null;
            SelectedProducer = EditedProducer;
        }
        private void GetAllProducers()
        {
            List<IProducer> producers = dataAccess.DAO.GetAllProducers().ToList();
            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }
        private void FilterData()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (p) => ((ProducerViewModel)p).Name.Contains(FilterValue);
            }
        }
        private ProducerViewModel _editedProducer;
        public ProducerViewModel EditedProducer
        {
            get => _editedProducer;
            set
            {
                _editedProducer = value;
                OnPropertyChanged(nameof(EditedProducer));
            }
        }
        private ProducerViewModel _selectedProducer;
        public ProducerViewModel SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                EditedProducer = value;
                OnPropertyChanged(nameof(SelectedProducer));
            }
        }
        private RelayCommand _saveProducerCommand;
        public RelayCommand SaveProducerCommand
        {
            get => _saveProducerCommand;
        }
        private void SaveProducer()
        {
            if (!Producers.Contains(EditedProducer))
            {
                Producers.Add(EditedProducer);
                dataAccess.DAO.UpdateProducer(EditedProducer.Producer);
            }
        }
        private bool CanSaveProducer()
        {
            if (EditedProducer != null && !EditedProducer.HasErrors)
            {
                return true;
            }
            return false;
        }
        private RelayCommand _addNewProducerCommand;
        public RelayCommand AddNewProducerCommand
        {
            get => _addNewProducerCommand;
        }
        private void AddNewProducer()
        {
            int maxProducerId;
            try
            {
                maxProducerId = dataAccess.DAO.GetAllProducers().Max(x => x.Id);
            }
            catch
            {
                maxProducerId = 1;
            }

            AddProducer addProducerDialog = new AddProducer(); // Instantiate the dialog
            addProducerDialog.Owner = Application.Current.MainWindow; // Set the owner of the dialog

            if (addProducerDialog.ShowDialog() == true)
            {
                var newProd = dataAccess.DAO.AddNewProducer(maxProducerId, addProducerDialog.ProducerName, addProducerDialog.ProducerCountry);
                Producers.Add(new ProducerViewModel(newProd));
            }
        }
        private bool CanAddNewProducer()
        {
            return true;
        }
        private RelayCommand _deleteProducerCommand;
        public RelayCommand DeleteProducerCommand
        {
            get => _deleteProducerCommand;
        }
        private void DeleteProducer()
        {
            if (Producers.Contains(SelectedProducer))
            {
                dataAccess.RemoveProducer(SelectedProducer.Id);
                Producers.Remove(SelectedProducer);
                EditedProducer = null;
            }
            EditedProducer = null;
        }
        private bool CanDeleteProducer()
        {
            if (EditedProducer != null && !EditedProducer.HasErrors)
            {
                return true;
            }
            return false;
        }


        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
