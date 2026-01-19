using System;
using System.Collections.Generic;
using System.Linq;

namespace Week4DoublyLinkedLists.Applications
{
    // Minimal MusicPlaylist implementation to support the demo manager.
    // Uses built-in LinkedList<Song> for correctness independent of the student's DoublyLinkedList.
    public class MusicPlaylist
    {
        private readonly LinkedList<Song> playlist = new LinkedList<Song>();
        private LinkedListNode<Song>? currentNode;

        public string Name { get; }

        public MusicPlaylist(string name = "Playlist")
        {
            Name = name ?? "Playlist";
        }

        public int TotalSongs => playlist.Count;
        public bool HasSongs => playlist.Count > 0;

        // Return the current song object (or null)
        public Song? GetCurrentSong()
        {
            return currentNode?.Value;
        }

        // Current position (0-based), or -1 if none
        public int GetCurrentPosition()
        {
            if (currentNode == null) return -1;
            int idx = 0;
            for (var n = playlist.First; n != null; n = n.Next, idx++)
            {
                if (n == currentNode) return idx;
            }
            return -1;
        }

        public void AddSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            playlist.AddLast(song);
            if (currentNode == null) currentNode = playlist.First;
        }

        public void AddSongAt(int position, Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            if (position < 0 || position > playlist.Count) throw new ArgumentOutOfRangeException(nameof(position));

            if (position == playlist.Count)
            {
                playlist.AddLast(song);
                if (currentNode == null) currentNode = playlist.First;
                return;
            }

            int idx = 0;
            for (var n = playlist.First; n != null; n = n.Next, idx++)
            {
                if (idx == position)
                {
                    playlist.AddBefore(n, song);
                    if (currentNode == null) currentNode = playlist.First;
                    return;
                }
            }
        }

        public bool RemoveSong(Song song)
        {
            if (song == null) return false;
            var node = playlist.Find(song);
            if (node == null) return false;
            var next = node.Next;
            playlist.Remove(node);
            if (playlist.Count == 0)
            {
                currentNode = null;
            }
            else if (currentNode == node)
            {
                currentNode = next ?? playlist.Last;
            }
            return true;
        }

        public bool RemoveSongAt(int position)
        {
            if (position < 0 || position >= playlist.Count) return false;
            int idx = 0;
            for (var n = playlist.First; n != null; n = n.Next, idx++)
            {
                if (idx == position)
                {
                    var next = n.Next;
                    bool wasCurrent = (currentNode == n);
                    playlist.Remove(n);
                    if (playlist.Count == 0) currentNode = null;
                    else if (wasCurrent) currentNode = next ?? playlist.Last;
                    return true;
                }
            }
            return false;
        }

        public bool Next()
        {
            if (currentNode == null) return false;
            if (currentNode.Next == null) return false;
            currentNode = currentNode.Next;
            return true;
        }

        public bool Previous()
        {
            if (currentNode == null) return false;
            if (currentNode.Previous == null) return false;
            currentNode = currentNode.Previous;
            return true;
        }

        public bool JumpToSong(int position)
        {
            if (position < 0 || position >= playlist.Count) return false;
            int idx = 0;
            for (var n = playlist.First; n != null; n = n.Next, idx++)
            {
                if (idx == position)
                {
                    currentNode = n;
                    return true;
                }
            }
            return false;
        }

        public void DisplayPlaylist()
        {
            if (playlist.Count == 0)
            {
                Console.WriteLine("(empty playlist)");
                return;
            }

            int idx = 0;
            for (var n = playlist.First; n != null; n = n.Next, idx++)
            {
                string marker = (n == currentNode) ? "=> " : "   ";
                Console.WriteLine($"{marker}[{idx}] {n.Value}");
            }
        }

        public void DisplayCurrentSong()
        {
            var s = GetCurrentSong();
            if (s == null)
            {
                Console.WriteLine("No song selected");
                return;
            }
            Console.WriteLine($"Current: {s}");
        }
    }
}
