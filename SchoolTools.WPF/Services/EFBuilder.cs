using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolyhydraGames.SchoolTools.WPF.Services
{


    public abstract class PolyDBContext<T> : DbContext where T : class
    {
        public PolyDBContext()
            : base("name=SpellContext")
        {
        }

        public DbSet<T> Models { get; set; }

        public async Task Populate(IEnumerable<T> items)
        {
            if (Models.Count() < 20)
            {

                foreach (var item in items)
                {
                    Debug.WriteLine(item.ToString());
                    Models.Add(item);
                }
                await SaveChangesAsync();
            }

        }

    }

    public class PolyEfBuilder<T> : DbContext where T : class
    {
        public PolyEfBuilder()
            : base("name=SpellContext")
        {
        }

        public DbSet<T> Models { get; set; }

        public async Task Populate(IEnumerable<T> items)
        {
            if (Models.Count() < 20)
            {
                Debug.WriteLine("Starting Update for " + typeof(T));
                foreach (var item in items)
                {
                    Debug.WriteLine(item.ToString());
                    Models.Add(item);
                }
                await SaveChangesAsync();
                Debug.WriteLine("Saved Changes for " + typeof(T));
            }

        }
    }
}
