using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pattern_study
{
    class Song
    {
        public string Title { get; set; } // 노래 제목
        public string ArtistName { get; set; } // 가수 이름
        public int Length { get; set; } // 연주 시간, 단위는 초
    }

    class SongList {

        private List<Song> songs;

        public SongList(string filePath)
        {
            songs = ReadSongs(filePath);
        }

        private static List<Song> ReadSongs(string filepath)
        {

            List<Song> songs = new List<Song>();

            // 한글이 계속 깨지던 이유가 encoding 방식을 정해주지 않아서 였다.
            // ReadAllLines에 Encoding.Default 추가하니 해결

            // 참고로 ReadAllLines 는 \n을 기준으로 한 줄씩 불러오는 것이며
            // 이는 각각 string 배열의 원소로 들어간다

            // ReadAllText는 txt파일을 한번에 string 형으로 불러오는 것.
            string[] lines = File.ReadAllLines(filepath, Encoding.Default);

            foreach(string s in lines)
            {
                string[] track = s.Split(',');
               Song song = new Song {
                    Title = track[0],
                    ArtistName = track[1],
                    Length = int.Parse(track[2])
                };
            songs.Add(song);

            }
            return songs;


        }

        public List<Song> GetSongList()
        {
            return songs;
        }
    
    }
   

    class Program
    {
        static void Main(string[] args)
        {

            // Song songs = new Song();
            SongList songs = new SongList("songlist.txt");

            List<Song> mysong = songs.GetSongList();
          // foreach(List<SongList> s in )

            foreach(Song s in mysong)
            {
                Console.WriteLine("{0}, {1}, {2}", s.Title, s.ArtistName, s.Length);
            }
        }
    }
}
