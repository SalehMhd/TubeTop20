using Top20Video.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Video.Repository
{
    /// <summary>
    /// Interface for implementing unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AdminUser> RepoUser { get; }
        IRepository<Category> RepoCategory { get; }
        IRepository<Country> RepoCountry{ get; }
        IRepository<SyncSetting> RepoSyncSetting { get; }
        IRepository<Video> RepoVideo { get; }
        IRepository<language> Repolanguage { get; }
        IRepository<Trending> RepoTrending { get; }
        void Commit();
        bool ExecuteQuery(string query);
    }
}
