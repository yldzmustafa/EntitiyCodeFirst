using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using entityfr.codefirst.Migrations;

namespace entityfr.codefirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BloggingContext, Configuration>());
            using (var db=new BloggingContext())
            {
                //Create and save a new Blog
                Console.WriteLine("Lütfen Yeni Blog ismini Giriniz: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog); //Blog tabllosuna yeni bir kayıt ekledik.
                db.SaveChanges(); //İçerik kısmını kaydettik.

                //Display all Blogs from the database
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                Console.WriteLine("Veritabanındaki tüm bloglar:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine("Çıkmak için herhangi bir tuşa basınız...");
                Console.ReadKey();
            }
            
        }
    }

    public class Blog
    {
        public int BlogID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Url2 { get; set; }
        public string Url3 { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual BlogImage BlogImage { get; set; }
    }
    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class Tag
    {
        public int Tagid { get; set; }
        public string Ad { get; set; }
        public virtual List<Tagg> Taggs { get; set; }
    }
    public class Tagg
    {
        public int Taggid { get; set; }
        public string Ad { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }

    public class BloggingContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOptional(e => e.BlogImage)
                .WithRequired(e => e.Blog);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<Tagg> taggs { get; set; }
    }
}
