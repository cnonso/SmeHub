using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public class KeyWordRepository : IKeyWordRepository
    {
         private BusinessSummaryDataContext _dataContext;

        public KeyWordRepository()
        {
            _dataContext = new BusinessSummaryDataContext();
        }

        public IList<_BusinessSummary> GetAllBusinessSummaries()
        {
            var query = from summaries in _dataContext._BusinessSummaries
                        select summaries;
            var content = query.Take(10).ToList<_BusinessSummary>();
            return content;
        }

        public IList<_PLace> GetAllPlaces()
        {
            var query = from places in _dataContext._PLaces
                        select places;
            var content = query.Take(10).ToList<_PLace>();
            return content;
        }
    }
}