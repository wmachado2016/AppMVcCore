using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WSM.WebApp.MVC.Models;

namespace WSM.WebApp.MVC.Data
{
    public class WSMWebAppMVCContext : DbContext
    {
        public WSMWebAppMVCContext (DbContextOptions<WSMWebAppMVCContext> options)
            : base(options)
        {
        }

        public DbSet<WSM.WebApp.MVC.Models.ContaViewModel> ContaViewModel { get; set; } = default!;
    }
}
