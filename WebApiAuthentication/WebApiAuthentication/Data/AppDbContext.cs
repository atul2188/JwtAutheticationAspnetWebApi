using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiAuthentication.Models;

namespace WebApiAuthentication.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    
    public DbSet<Blogs> Blogs { get; set; }

}
