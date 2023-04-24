using BookStore.Models.DataViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IStatisticalService
    {
        Task<StatisResponse> NumberOfBooksSold(string type);
    }
}
