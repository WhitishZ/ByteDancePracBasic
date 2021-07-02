using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ByteDancePracBasic.Models;
using ByteDancePracBasic.Utils;
using SQLite;

namespace ByteDancePracBasic.Data
{
    public class FeedDatabase
    {
        public const string DatabaseFileName = "FeedSQLite.db3";
        public const SQLiteOpenFlags FeedDBOpenFlags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<FeedDatabase> Instance = new AsyncLazy<FeedDatabase>(async () =>
        {
            var instance = new FeedDatabase();
            await Database.CreateTableAsync<ImageFeedModel>();
            await Database.CreateTableAsync<MediaInfoModel>();
            return instance;
        });
        public FeedDatabase()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, FeedDBOpenFlags);
        }
        public Task<List<ImageFeedModel>> GetImageFeedsAsync()
        {
            return Database.Table<ImageFeedModel>().ToListAsync();
        }
        public Task<List<MediaInfoModel>> GetMediaInfosAsync()
        {
            return Database.Table<MediaInfoModel>().ToListAsync();
        }
        public Task<int> SaveImageFeedAsync(ImageFeedModel feed)
        {
            return Database.InsertAsync(feed);
        }
        public Task<int> SaveMediaInfoAsync(MediaInfoModel mediaInfo)
        {
            return Database.InsertAsync(mediaInfo);
        }
    }
}
