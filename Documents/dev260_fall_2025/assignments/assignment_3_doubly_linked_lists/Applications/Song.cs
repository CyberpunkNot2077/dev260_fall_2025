using System;

namespace Week4DoublyLinkedLists.Applications
{
    public class Song
    {
        public string Title { get; }
        public string Artist { get; }
        public TimeSpan Duration { get; }

        public Song(string title, string artist, TimeSpan duration)
        {
            Title = title ?? string.Empty;
            Artist = artist ?? string.Empty;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"{Title} - {Artist} ({Duration.ToString("mm\\:ss")})";
        }
    }
}
