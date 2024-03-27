using System;
using System.Linq;
using Blog.Data;

namespace Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new BlogDataContext();

            var posts = context.PostWithTagsCounts.ToList();

            foreach (var item in posts)
            {
                var nome = item.Name;
                var quantidade = item.Count;
            }

            Console.WriteLine("Teste");
        }
    }
}