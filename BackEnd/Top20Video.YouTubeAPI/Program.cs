using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Video.YouTubeAPI
{
    // //todo: for debug only
    //class Program
    //{

    //    static void Main(string[] args)
    //    {

    //        SyncData();
    //        //Console.Read();
    //    }

    //    // Call request to sync data between YouTube and local server.
    //    private static async void SyncData()
    //    {
    //        Console.WriteLine("============= Sync Data ======== ");
    //        YouTubeAPI.Helper.SyncSettings _settings = new Helper.SyncSettings();
    //        await _settings.SyncData();
    //        Console.WriteLine("============= Data Syncing Process Competed ======== ");
    //        Console.Read();
    //    }
    //}

    public class cSyncService
    {
        public static async Task Run()
        {
            SyncData();
        }


        // Call request to sync data between YouTube and local server.
        private static async void SyncData()
        {
            Console.WriteLine("============= Sync Data ======== ");
            YouTubeAPI.Helper.SyncSettings _settings = new Helper.SyncSettings();
            await _settings.SyncData();
            Console.WriteLine("============= Data Syncing Process Competed ======== ");
        }

    }
}
