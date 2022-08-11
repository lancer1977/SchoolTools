using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.SQLite.Interfaces;
using SQLite;

namespace PolyhydraGames.SchoolTools.Core.Services
{
    public abstract class DynamicDataSQLite<T, T2> : IRepositoryLite<T2> where T : class, IDto, T2, new()
    {
        private readonly ISQLiteFactory _factory;

        private SQLiteConnection _connection;

        internal SQLiteConnection Connection
        {
            get
            {
                if (_connection != null) return _connection;
                _connection = _factory.CreateConnection();
                _connection.CreateTable<T>();
                return _connection;
            }
        }

        public virtual string Report()
        {
            return " Count:" + Count();
        }
        public DynamicDataSQLite(ISQLiteFactory factory)
        {
            _factory = factory;
        }


        public IEnumerable<T2> Items => Connection.Table<T>();

        public int Insert(T2 model)
        {
            var dto = model as T;
            if (dto == null)
                return -1;
            Connection.Insert(dto);
            string sql = @"select last_insert_rowid()";
            var lastId = _connection.ExecuteScalar<int>(sql);
            dto.Id = lastId;
            OnCollectionChanged(NotifyCollectionChangedAction.Add, dto);
            return lastId;
        }


        public void Update(T2 model)
        {
            Connection.Update(model);
        }
        public void Delete(T2 model)
        {
            Connection.Delete(model);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, model);

        }
        public void Delete(int id)
        {
            var item = Item(id);
            Delete(item);

        }

        public TableQuery<T> Table => Connection.Table<T>();

        public virtual T2 Item(int id)
        {
            return Table.FirstOrDefault(i => i.Id == id);
        }

        public int Count()
        {
            return Table.Count();
        }
        public IEnumerable<T2> Where(Func<T2, bool> predicate)
        {
            return Connection.Table<T>().Where(predicate);
        }
        public IQueryable<T2> Query => Connection.Table<T>().AsQueryable();
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T2 item)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, new[] { item }));
        }


        public abstract string Title { get; }
        public virtual void Initialize()
        {
        }

        public async Task InitializeAsync()
        { 
        }
    }
}