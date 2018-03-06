using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
	    public VideoService()
	    {
		    FileReader = new FileReader();
			VideoRepository = new VideoRepository();
	    }

	    public IFileReader FileReader { get; set; }
	    public IVideoRepository VideoRepository { get; set; }	

        public string ReadVideoTitle()
        {
            var str = FileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);

			return video == null 
				? "Error parsing the video." 
				: video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

	        var videos = VideoRepository.GetUnprocessedVideos();
			videoIds.AddRange(videos.Select(v => v.Id));

			return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}