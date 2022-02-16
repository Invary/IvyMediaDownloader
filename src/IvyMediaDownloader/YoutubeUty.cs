using Invary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;

namespace Invary.IvyMediaDownloader
{


	class YoutubeUty
	{
		//
		// nuget
		//
		// YoutubeExplode
		// LGPL 3.0
		//


		public static async Task<SubscribeItem> GetChannelInfoAsync(string url, CancellationToken ct)
		{
			var youtube = new YoutubeClient();

			SubscribeItem item = null;

			//is url youtube playlist?
			if (item == null)
				item = await GetChannelInfoFromPlaylistAsync(youtube, url, ct);

			//is url youtube channel?
			//"https://www.youtube.com/channel/xxxxxxxx"
			if (item == null)
				item = await GetChannelInfoFromChannelAsync(youtube, url, ct);

			//is url youtube custom channel?
			//"https://www.youtube.com/c/xxxxxxx"
			//
			if (item == null)
			{
				var channelUrl = await CustomChannelToNormalChannel(url, ct);
				if (channelUrl != "")
				{
					//チャンネルとして処理してみる
					item = await GetChannelInfoFromChannelAsync(youtube, channelUrl, ct);
				}
			}

			return item;
		}




		static async Task<SubscribeItem> GetChannelInfoFromPlaylistAsync(YoutubeClient youtube, string url, CancellationToken ct)
		{
			//is url youtube playlist?
			try
			{
				var info = await youtube.Playlists.GetAsync(url, ct);

				SubscribeItem item = DBUty.GetSubscribeItemDB(info.Url);

				if (item == null)
				{
					item = new SubscribeItem();
					item.dtAdd = DateTime.Now;
				}

				item.strAuthor = info.Author.Title;
				item.strAuthorId = info.Author.ChannelId;
				item.strTitle = info.Title;
				item.strUrl = info.Url;
				item.strDescription = info.Description;

				if (info.Thumbnails.Count > 0)
					item.strThumbnail = info.Thumbnails[0].Url;

				return item;
			}
			catch (Exception)
			{
				return null;
			}
		}


		static async Task<SubscribeItem> GetChannelInfoFromChannelAsync(YoutubeClient youtube, string url, CancellationToken ct)
		{
			//is url youtube channel?
			try
			{
				var info = await youtube.Channels.GetAsync(url, ct);

				SubscribeItem item = DBUty.GetSubscribeItemDB(info.Url);

				if (item == null)
				{
					item = new SubscribeItem();
					item.dtAdd = DateTime.Now;
				}
				item.strAuthor = "";
				item.strAuthorId = info.Id;     //チャンネルID
				item.strTitle = info.Title;
				item.strUrl = info.Url;
				item.strDescription = "";

				if (info.Thumbnails.Count > 0)
					item.strThumbnail = info.Thumbnails[0].Url;

				return item;
			}
			catch (Exception)
			{
				return null;
			}
		}












		public static async Task<List<VideoInfo>> GetVideoInfoAsync(string url, CancellationToken ct)
		{

			var youtube = new YoutubeClient();

			//is url youtube single video?
			//"https://www.youtube.com/watch?v=xxxxx"
			//"youtu.be/xxxxxxx"
			{
				var ret = await GetVideoInfoFromVideoAsync(youtube, url, ct);
				if (ret != null)
					return ret;
			}

			//is url youtube playlist?
			{
				var ret = await GetVideoInfoFromPlaylistAsync(youtube, url, ct);
				if (ret != null)
					return ret;
			}


			//is url youtube channel?
			//"https://www.youtube.com/channel/xxxxxxxx"
			{
				var ret = await GetVideoInfoFromChannelAsync(youtube, url, ct);
				if (ret != null)
					return ret;
			}



			//is url youtube custom channel?
			//"https://www.youtube.com/c/xxxxxxx"
			//
			{
				var channelUrl = await CustomChannelToNormalChannel(url, ct);
				if (channelUrl != "")
				{
					//チャンネルとして処理してみる
					var ret = await GetVideoInfoFromChannelAsync(youtube, channelUrl, ct);
					if (ret != null)
						return ret;
				}
			}

			return null;
		}







		static async Task<string> CustomChannelToNormalChannel(string url, CancellationToken ct)
		{
			if (ct.IsCancellationRequested)
				return "";

			//
			// When Custom channel of youtube, read html and get channel ID
			//
			//"https://www.youtube.com/c/xxxxxxx"
			//
			if (url.IndexOf("youtube.com/c/") > 0 || url.IndexOf("youtu.be/c/") > 0)
			{
				//TODO: timeout 30sec 
				var html = await Uty.DownloadTextAsync(url, 30, ct);
				string id = "";
				{
					var matchs = Regex.Matches(html, "<meta itemprop=\"channelId\" content=\"(.+?)\">");

					foreach (Match match in matchs)
					{
						if (match.Groups.Count < 2)
							continue;

						id = match.Groups[1].Value;
						break;
					}
				}
				if (id != "")
					return $"https://www.youtube.com/channel/{id}";
			}

			return "";
		}











		static async Task<List<VideoInfo>> GetVideoInfoFromPlaylistAsync(YoutubeClient youtube, string url, CancellationToken ct)
		{
			//is url youtube playlist?
			try
			{
				if (ct.IsCancellationRequested)
					return null;

				List<VideoInfo> listVideos = new List<VideoInfo>();

				var videos = await youtube.Playlists.GetVideosAsync(url, ct);

				for (int i = 0; i < videos.Count; i++)
				{
					VideoInfo info = new VideoInfo();
					info.strAuthor = videos[i].Author.Title;
					info.strAuthorId = videos[i].Author.ChannelId;
					info.strTitle = videos[i].Title;
					info.strUrl = videos[i].Url;
					info.spanDuration = videos[i].Duration;
					info.strId = videos[i].Id;
					if (videos[i].Thumbnails.Count > 0)
						info.strThumbnail = videos[i].Thumbnails[0].Url;

					listVideos.Add(info);
				}

				return listVideos;
			}
			catch (Exception)
			{
				return null;
			}
		}






		static async Task<List<VideoInfo>> GetVideoInfoFromVideoAsync(YoutubeClient youtube, string url, CancellationToken ct)
		{
			//is url youtube single video?
			try
			{
				if (ct.IsCancellationRequested)
					return null;

				List<VideoInfo> listVideos = new List<VideoInfo>();

				var video = await youtube.Videos.GetAsync(url, ct);

				VideoInfo info = new VideoInfo();
				info.strAuthor = video.Author.Title;
				info.strAuthorId = video.Author.ChannelId;
				info.strTitle = video.Title;
				info.strUrl = video.Url;
				info.spanDuration = video.Duration;
				info.strId = video.Id;
				if (video.Thumbnails.Count > 0)
					info.strThumbnail = video.Thumbnails[0].Url;

				listVideos.Add(info);

				return listVideos;
			}
			catch (Exception)
			{
				return null;
			}
		}





		static async Task<List<VideoInfo>> GetVideoInfoFromChannelAsync(YoutubeClient youtube, string url, CancellationToken ct)
		{
			//is url youtube channel?
			try
			{
				if (ct.IsCancellationRequested)
					return null;

				List<VideoInfo> listVideos = new List<VideoInfo>();

				var videos = await youtube.Channels.GetUploadsAsync(url, ct);

				for (int i = 0; i < videos.Count; i++)
				{
					VideoInfo info = new VideoInfo();
					info.strAuthor = videos[i].Author.Title;
					info.strAuthorId = videos[i].Author.ChannelId;
					info.strTitle = videos[i].Title;
					info.strUrl = videos[i].Url;
					info.spanDuration = videos[i].Duration;
					info.strId = videos[i].Id;
					if (videos[i].Thumbnails.Count > 0)
						info.strThumbnail = videos[i].Thumbnails[0].Url;

					listVideos.Add(info);
				}

				return listVideos;
			}
			catch (Exception)
			{
				return null;
			}
		}







	}
}
