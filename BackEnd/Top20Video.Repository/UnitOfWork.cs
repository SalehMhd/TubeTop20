using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Data;
using System.Data.Entity;

namespace Top20Video.Repository
{
    /// <summary>
    /// Class for intracting with the database
    /// </summary>
    public class EfUnitOfWork : dbTop20Video_9359Entities, IUnitOfWork
    {
        #region Private memeber

        IRepository<AdminUser> _RepoUser;
        IRepository<Category> _RepoCategory;
        IRepository<Country> _RepoCountry;
        IRepository<SyncSetting> _RepoSyncSetting;
        IRepository<Video> _RepoVideo;
        IRepository<language> _RepoLanguage;
        IRepository<Trending> _RepoTrending;


        #endregion

        #region Override property of context
        //public override DbSet<tblUser> Users { get; set; }
        #endregion

        #region Constructor
        public EfUnitOfWork()
        {
            _RepoUser = new Repository<AdminUser>(AdminUsers);
            _RepoCategory = new Repository<Category>(Categories);
            _RepoCountry = new Repository<Country>(Countries);
            _RepoSyncSetting = new Repository<SyncSetting>(SyncSettings);
            _RepoVideo = new Repository<Video>(Videos);
            _RepoLanguage = new Repository<language>(languages);
            _RepoTrending = new Repository<Trending>(Trendings);

        }

        #endregion

        #region IUnitOfWork Implementation
        public IRepository<AdminUser> RepoUser { get { return _RepoUser; } }
        public IRepository<Category> RepoCategory { get { return _RepoCategory; } }
        public IRepository<Country> RepoCountry { get { return _RepoCountry; } }
        public IRepository<SyncSetting> RepoSyncSetting { get { return _RepoSyncSetting; } }
        public IRepository<Video> RepoVideo { get { return _RepoVideo; } }
        public IRepository<language> Repolanguage { get { return _RepoLanguage; } }
        public IRepository<Trending> RepoTrending { get { return _RepoTrending; } }

        public void Commit()
        {
            this.SaveChanges();
        }

        public bool ExecuteQuery(string query)
        {
           return this.ExecuteQuery(query);
        }
        #endregion
    }
}



