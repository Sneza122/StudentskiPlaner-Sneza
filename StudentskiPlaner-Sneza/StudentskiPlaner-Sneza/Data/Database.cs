﻿using System;
using System.Collections.Generic;
using System.Text;
using StudentskiPlanerSneza;
using System.Threading.Tasks;
using SQLite;
using StudentskiPlanerSneza.Models;

namespace StudentskiPlanerSneza.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Zadaci>().Wait();  // Tvoja model klasa
        }

        public Task<List<Zadaci>> GetItemsAsync()
        {
            return _database.Table<Zadaci>().ToListAsync();
        }

        public Task<int> SaveItemAsync(Zadaci item)
        {
            if (item.Id != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Zadaci item)
        {
            return _database.DeleteAsync(item);  // Ovo je ispravna metoda

        }
        public Task<int> DeleteAllItemsAsync()
        {
            return _database.DeleteAllAsync<Zadaci>();
        }


    }
}

