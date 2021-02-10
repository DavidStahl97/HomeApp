﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Database.Collections
{
    public interface ICollectionBase<T>
    {
        Task InsertAsync(T document);

        Task InsertManyAsync(IEnumerable<T> documents);

        Task<T> SingleAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task ReplaceOrInsert(Expression<Func<T, bool>> expression, T document);
    }
}
