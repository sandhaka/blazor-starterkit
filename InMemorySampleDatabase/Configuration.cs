using System.Collections;
using System.Text;
using System.Text.Json;
using InMemorySampleDatabase.Entities;

namespace InMemorySampleDatabase;

public static class Configuration
{
    public static void Seed(NorthwindContext context)
    {
        var entityTypes = new List<Type>
        {
            typeof(Category),
            typeof(Customer),
            typeof(CustomerDemographic),
            typeof(Demographic),
            typeof(Employee),
            typeof(EmployeeTerritory),
            typeof(Order),
            typeof(OrderDetail),
            typeof(Product),
            typeof(Region),
            typeof(Shipper),
            typeof(Supplier),
            typeof(Territory)
        };

        var ns = typeof(Configuration).Assembly.FullName.Split(',')[0];
        var dataNS = $"{ns}.Data";
        
        foreach (var type in entityTypes)
        {
            var dataFile = $"{dataNS}.{type.Name.ToLower()}.json";
            var json = GetResource(dataFile);

            var listType = typeof(IEnumerable<>).MakeGenericType(type);

            var entities = JsonSerializer.Deserialize(json, listType) ??
                           throw new NullReferenceException(nameof(listType));

            var entitiesList = entities as IEnumerable ??
                               throw new NullReferenceException(nameof(listType));
            
            foreach (var e in entitiesList)
            {
                context.Add(e);
            }
        }

        context.SaveChanges();
    }

    private static string GetResource(string resourceName)
    {
        var result = string.Empty;
        var assembly = typeof(Configuration).Assembly;

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
        {
            result = reader.ReadToEnd();
        }

        return result;
    }
}