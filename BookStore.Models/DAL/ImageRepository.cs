using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
