using COMP_2007_Assignment1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP2007_Assignment_2.Models
{
    public interface IGenreRepository
    {
        IQueryable<Genre> genres { get; }
    }
    
}