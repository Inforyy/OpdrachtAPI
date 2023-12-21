using System.ComponentModel;
using System.Threading.Tasks;
using OpdrachtAPI.Services;

namespace OpdrachtAPI.ViewModels
{
    public class RadioDataViewModel : INotifyPropertyChanged
    {
        private readonly RadioApiService _apiService;

        private string _stationName;
        private string _currentSong;

        public string StationName
        {
            get => _stationName;
            set
            {
                _stationName = value;
                OnPropertyChanged(nameof(StationName));
            }
        }

        public string CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));
            }
        }

        // Additional properties as needed

        public RadioDataViewModel()
        {
            _apiService = new RadioApiService();
            LoadRadioDataAsync();
        }

        private async Task LoadRadioDataAsync()
        {
            // Fetch data from the API
            var apiResponse = await _apiService.GetRadioDataAsync();

            // Parse the API response and update properties
            // Example: You might use JSON serialization/deserialization here
            // Replace this with actual logic based on your API response structure
            StationName = "Your Station Name";
            CurrentSong = "Now Playing: Your Song";

            // Notify UI about property changes
            OnPropertyChanged(nameof(StationName));
            OnPropertyChanged(nameof(CurrentSong));

            // Add additional logic to update other properties
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Add other methods or properties as needed
    }
}
