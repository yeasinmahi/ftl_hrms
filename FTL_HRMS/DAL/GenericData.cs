using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using FTL_HRMS.Utility;

namespace FTL_HRMS.DAL
{
    public class GenericData
    {
        readonly object _obj = new object();

        private static GenericData _instance;
        readonly HRMSDbContext _dbContext = new HRMSDbContext();

        private GenericData() { }

        public static GenericData GetInstance()
        {
            return _instance ?? (_instance = new GenericData());
        }


        public List<T> GetAll<T>() where T : class
        {
            lock (_obj)
            {
                List<T> genericList = _dbContext.Set(typeof(T)).Cast<T>().AsNoTracking<T>().ToList();
                return genericList;
            }

        }
        public T GetById<T>(int id) where T : class
        {
            lock (_obj)
            {
                return _dbContext.Set(typeof(T)).Cast<T>().Find(id);
            }
        }
        public DbUtility.Status Insert<T>(object o)
        {
            Type type = typeof(T);
            lock (_obj)
            {
                try
                {
                    _dbContext.Set(type).Add(o);
                    _dbContext.SaveChanges();
                    return DbUtility.Status.AddSuccess;
                }
                catch (Exception)
                {
                    return DbUtility.Status.AddFailed;
                }
            }
        }
        public DbUtility.Status Update<T>(object o) where T : class
        {
            Type type = typeof(T);
            lock (_obj)
            {
                try
                {
                    _dbContext.Set(type).Add(o);
                    _dbContext.Entry(type).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return DbUtility.Status.AddSuccess;
                }
                catch (Exception)
                {
                    return DbUtility.Status.AddFailed;
                }
            }
        }
        public DbUtility.Status Update<T>(int id, string prop, string value) where T : class
        {
            lock (_obj)
            {
                T entity = GetById<T>(id);
                PropertyInfo propertyInfo = DbUtility.GetPropertyInfo(entity, prop);
                if (propertyInfo != null)
                {
                    if (DbUtility.SetValue(entity, propertyInfo, value))
                    {
                        try
                        {
                            _dbContext.SaveChanges();
                            return DbUtility.Status.AddSuccess;
                        }
                        catch (Exception)
                        {
                            return DbUtility.Status.UpdateFailed;
                        }
                    }
                    return DbUtility.Status.Error;
                }
                return DbUtility.Status.NotFound;
            }
        }

        public string Delete<T>(int id) where T : class
        {

            lock (_obj)
            {
                try
                {
                    Type type = typeof(T);
                    T entity = GetById<T>(id);
                    if (entity != null)
                    {
                        _dbContext.Set(type).Attach(entity);
                        _dbContext.Set(type).Remove(entity);
                        _dbContext.SaveChanges();
                        return DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
                    }
                    return DbUtility.GetStatusMessage(DbUtility.Status.NotFound);
                }
                catch (Exception)
                {
                    return DbUtility.GetStatusMessage(DbUtility.Status.DeleteFailed);
                }
            }
        }
    }
}
