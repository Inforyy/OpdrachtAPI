using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpdrachtAPI
{
    public partial class MainPage : ContentPage
    {
		private const string ApiKey = "";
		private const string ApiUrl = "https://api.radiofomix.nl/api/nowplaying/1";
		public ObservableCollection<Root> RadioData { get; set; } = new ObservableCollection<Root>();
		private bool dataLoaded = false;
		public int TIME_REMAINING = 10;

		[Obsolete]
		public MainPage()
        {
            InitializeComponent();
			LoadData();
			BindingContext = this;

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				if (dataLoaded && RadioData.Count > 0 && RadioData[0].now_playing != null)
				{
					int time_remaining = RadioData[0].now_playing.remaining;
					if (time_remaining <= 1)
					{
						RadioData.Clear();
						LoadData();
						return true;
					} else
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							if (this.IsLoaded)
							{
								var remaining_time = this.FindByName<Label>("remaining_time");
								if (remaining_time != null)
								{
									remaining_time.Text = time_remaining.ToString();
								}
							}
						});
						return true;
					}
				}
				return false;
			});
		}

		private async void LoadData()
		{
			try
			{
				HttpClient client = new HttpClient();
				string json = await client.GetStringAsync(ApiUrl);

				var response = JsonSerializer.Deserialize<Root>(json);

				if (response != null)
				{
					RadioData.Add(response);
					dataLoaded = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

        private void refresh_button_Clicked(System.Object sender, System.EventArgs e)
        {
			RadioData.Clear();
			LoadData();
        }
    }
}

public class RadioDataViewModel
{
	public string title { get; set; }
	public string description { get; set; }
	public string type { get; set; }
}

public class Station
{
	public int id { get; set; }
	public string name { get; set; }
	public string shortcode { get; set; }
	public string description { get; set; }
	public string frontend { get; set; }
	public string backend { get; set; }
	public string listen_url { get; set; }
	public string url { get; set; }
	public string public_player_url { get; set; }
	public string playlist_pls_url { get; set; }
	public string playlist_m3u_url { get; set; }
	public bool is_public { get; set; }
	public List<Mount> mounts { get; set; }
	public List<object> remotes { get; set; }
	public bool hls_enabled { get; set; }
	public object hls_url { get; set; }
	public int hls_listeners { get; set; }
}

public class Mount
{
	public int id { get; set; }
	public string name { get; set; }
	public string url { get; set; }
	public int bitrate { get; set; }
	public string format { get; set; }
	public Listeners listeners { get; set; }
	public string path { get; set; }
	public bool is_default { get; set; }
}

public class Listeners
{
	public int total { get; set; }
	public int unique { get; set; }
	public int current { get; set; }
}

public class Root
{
	public Station station { get; set; }
	public Listeners listeners { get; set; }
	public Live live { get; set; }
	public NowPlaying now_playing { get; set; }
	public PlayingNext playing_next { get; set; }
	public List<SongHistoryItem> song_history { get; set; }
}

public class Song
{
	public string id { get; set; }
	public string text { get; set; }
	public string artist { get; set; }
	public string title { get; set; }
	public string album { get; set; }
	public string isrc { get; set; }
}

public class SongHistoryItem
{
	public int sh_id { get; set; }
	public int played_at { get; set; }
	public int elapsed { get; set; }
	public int remaining { get; set; }
	public int duration { get; set; }
	public Song song { get; set; }
	public string streamer { get; set; }
}


public class Live
{
	public bool is_live { get; set; }
	public string streamer_name { get; set; }
	public object broadcast_start { get; set; }
	public object art { get; set; }
}

public class NowPlaying
{
	public int sh_id { get; set; }
	public int played_at { get; set; }
	public int elapsed { get; set; }
	public int remaining { get; set; }
	public int duration { get; set; }
	public Song song { get; set; }
	public string streamer { get; set; }
}

public class PlayingNext
{
	public int sh_id { get; set; }
	public int played_at { get; set; }
	public int elapsed { get; set; }
	public int remaining { get; set; }
	public int duration { get; set; }
	public Song song { get; set; }
	public string streamer { get; set; }
}