﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME_Hub.Models
{
    public interface IKeyWordRepository
    {
        IList<_BusinessSummary> GetAllBusinessSummaries();
        IList<_PLace> GetAllPlaces();
    }
}
