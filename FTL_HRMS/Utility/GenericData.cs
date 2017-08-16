using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using FTL_HRMS.Models;

namespace FTL_HRMS.Utility
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

        
        public List<T> ListAll<T>() where T : class
        {
            lock (_obj)
            {
               List<T> genericList= _dbContext.Set(typeof(T)).Cast<T>().AsNoTracking<T>().ToList();
                return genericList;
            }

        }
        public T ListAllById<T>(int id) where T : class
        {
            lock (_obj)
            {
                return _dbContext.Set(typeof(T)).Cast<T>().Find(id);
            }
            

        }
        public string Insert<T>(object o)
        {
            Type type = typeof (T);
            lock (_obj)
            {
                try
                {
                    _dbContext.Set(type).Add(o);
                    _dbContext.SaveChanges();
                    return DbUtility.GetStatusMessage(DbUtility.Status.Success);
                }
                catch (DbUpdateException dbEx)
                {
                    return "Db Update Failed";
                }
                catch (Exception ex)
                {
                    return DbUtility.GetStatusMessage(DbUtility.Status.Fail);
                }
            }
        }

        public string Update<T>(int id, string prop, string value) where T : class
        {
            lock (_obj)
            {
                T bank = _dbContext.Set(typeof (T)).Cast<T>().Find(id);
                PropertyInfo propertyInfo = DbUtility.GetPropertyInfo(bank, prop);
                if (propertyInfo != null)
                {
                    if (DbUtility.SetValue(bank, propertyInfo, value))
                    {
                        try
                        {
                            _dbContext.SaveChanges();
                            return DbUtility.GetStatusMessage(DbUtility.Status.Success);
                        }
                        catch (Exception)
                        {
                            return DbUtility.GetStatusMessage(DbUtility.Status.Fail);
                        }
                    }
                    return DbUtility.GetStatusMessage(DbUtility.Status.Error);
                }
                return DbUtility.GetStatusMessage(DbUtility.Status.Null);
            }
        }

        public string Delete<T>(int id) where T : class
        {
            
            lock (_obj)
            {
                using (HRMSDbContext db = new HRMSDbContext())
                {
                    try
                    {
                        Type type = typeof(T);
                        T bank = db.Set(typeof(T)).Cast<T>().Find(id);
                        if (bank != null)
                        {
                            db.Set(type).Attach(bank);
                            db.Set(type).Remove(bank);
                            db.SaveChanges();
                            return DbUtility.GetStatusMessage(DbUtility.Status.Success);
                        }
                        return DbUtility.GetStatusMessage(DbUtility.Status.Null);
                    }
                    catch (Exception)
                    {
                        return DbUtility.GetStatusMessage(DbUtility.Status.Fail);
                    }
                }
                
            }
        }
    }
}
