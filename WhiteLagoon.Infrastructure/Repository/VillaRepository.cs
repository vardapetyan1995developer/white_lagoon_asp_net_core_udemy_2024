﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Villa, VillaNumber -- case sensitive
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>();

            if(filter is not null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {   
                //Villa, VillaNumber -- case sensitive
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Add(Villa entity)
        {
            _db.Add(entity);
        }

        public void Update(Villa entity)
        {
            _db.Villas.Update(entity);
        }

        public void Remove(Villa entity)
        {
            _db.Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
