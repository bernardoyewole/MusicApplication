using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_MusicApp
{
    internal class Program
    {
        /// <summary>
        /// A music playlist app that allows user add, play, skip and rewind songs.
        /// Using stack and queue data structures
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // create a queue for adding songs to playlist
            // and a stack for played songs in "memory"
            Queue<string> playlist = new Queue<string>();
            Stack<string> playedSongs = new Stack<string>();

            // user options
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Add a song to your playlist");
            Console.WriteLine("2. Play the next song in your playlist");
            Console.WriteLine("3. Skip the next song");
            Console.WriteLine("4. Rewind one song");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Option: ");
            string input = Console.ReadLine();
            char validInput;

            while (!char.TryParse(input, out validInput))
            {
                Console.WriteLine("Option can only be one character");
                Console.Write("Option: ");
                input = Console.ReadLine();
            }

            char option = Convert.ToChar(input);
            string song = null;
            StringBuilder currentSong = new StringBuilder();

            try
            {
                while (option != '5')
                {
                    switch (option)
                    {
                        case '1':
                            AddSong(ref song, ref playlist);
                            break;
                        case '2':
                            PlayNext(ref playlist, ref playedSongs, ref currentSong);
                            break;
                        case '3':
                            SkipNext(ref playlist, ref currentSong);
                            break;
                        case '4':
                            Rewind(ref playlist, ref playedSongs, ref currentSong);
                            break;
                        default:
                            Console.WriteLine("Pick options only from 1 to 5");
                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("1. Add a song  to your playlist");
                    Console.WriteLine("2. Play the next song in your playlist");
                    Console.WriteLine("3. Skip the next song");
                    Console.WriteLine("4. Rewind one song");
                    Console.WriteLine();

                    // update option for further functionalities
                    Console.Write("Option: ");
                    option = Convert.ToChar(Console.ReadLine());
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Songs over, press any key to exit");
            Console.ReadKey();
        }

        public static void AddSong(ref string song, ref Queue<string> playlist)
        {
            // take user song and add to playlist queue
            Console.Write("Enter Song Name: ");
            song = Console.ReadLine();

            playlist.Enqueue(song);

            Console.WriteLine($"{song} added to your playlist");
            Console.WriteLine($"Next Song - {playlist.Peek()}");
        }

        public static void PlayNext(ref Queue<string> playlist, ref Stack<string> playedSongs, ref StringBuilder currentSong)
        {
            try
            {
                // if string builder - current song is empty, append, else,
                // replace it with a new string (from playlist queue)
                if (currentSong.Length > 0)
                {
                    currentSong.Replace(currentSong.ToString(), playlist.Dequeue());
                } else
                {
                    currentSong.Append(playlist.Dequeue());
                }

                // update played songs stack
                playedSongs.Push(currentSong.ToString());

                Console.WriteLine($"Now playing - {currentSong}");
                Console.WriteLine($"Next song - {playlist.Peek()}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SkipNext(ref Queue<string> playlist, ref StringBuilder currentSong)
        {
            try
            {
                // the next song on queue is removed
                String skipped = playlist.Dequeue();
                Console.WriteLine($"{skipped} has been skipped");

                // print current song and new next song
                Console.WriteLine($"Now playing - {currentSong}");
                Console.WriteLine($"Next song - {playlist.Peek()}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Rewind(ref Queue<string> playlist, ref Stack<string> playedSongs,
            ref StringBuilder currentSong)
        {
            try
            {
                // create an array from playlist queue
                // to re-add the current song to beginning of queue
                var songsInQueue = playlist.ToArray();

                playlist.Clear();
                playlist.Enqueue(currentSong.ToString());

                foreach (var song in songsInQueue)
                {
                    playlist.Enqueue(song.ToString());
                }

                // remove the current song from played songs to retain only previous songs in stack
                if (playedSongs.Peek() == currentSong.ToString()) {
                    playedSongs.Pop();
                }

                // update the current song to the first song in played songs stack
                currentSong.Replace(currentSong.ToString(), playedSongs.Pop());

                Console.WriteLine($"Now playing - {currentSong}");
                Console.WriteLine($"Next song - {playlist.Peek()}");
            } catch (Exception)
            {
                throw;
            }
        }
    }
}