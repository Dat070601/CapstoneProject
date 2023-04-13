using BookStore.Models.DAL.Interfaces;
using BookStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Base
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapperCustom mapperCustom;
        public BaseService(IUnitOfWork unitOfWork, IMapperCustom mapperCustom)
        {
            this.mapperCustom = mapperCustom;
            this.unitOfWork = unitOfWork;
        }
    }
}
