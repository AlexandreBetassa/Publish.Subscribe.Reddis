﻿namespace DatabaseAPI.Contracts.v1
{
    public interface IDatabase<T> where T : class
    {
        Task<T> PostAsync(T entity);
        T GetOneAsync(int id);
        List<T> GetAllAsync();
    }
}
