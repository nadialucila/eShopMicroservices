namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() =>
    [
        new Product()
        {
            Id = new Guid("a45b08dc-854e-4aeb-8515-65104ef73f16"),
            Name = "IPhone X",
            Description = "Lorem ipsum dolor sit amet consectetur, adipiscing elit est laoreet non cum, habitant mauris montes posuere.",
            ImageFile = "product-1.png",
            Price = 100000,
            Category = ["Celular"]
        },
        new Product()
        {
            Id = new Guid("0d360a54-82bd-47f3-b0d3-05c6c8321c66"),
            Name = "Huawei Algo",
            Description = "Lorem ipsum dolor sit amet consectetur, adipiscing elit est laoreet non cum, habitant mauris montes posuere.",
            ImageFile = "product-2.png",
            Price = 200000,
            Category = ["Celular"]
        },
        new Product()
        {
            Id = new Guid("de519520-d354-4f8d-ad40-fce506d5d5be"),
            Name = "Redmi Note 12",
            Description = "Lorem ipsum dolor sit amet consectetur, adipiscing elit est laoreet non cum, habitant mauris montes posuere.",
            ImageFile = "product-3.png",
            Price = 7700000,
            Category = ["Celular"]
        },
        new Product()
                {
            Id = new Guid("3e80c783-7440-4ba7-b5ce-480cc6e55ba9"),
            Name = "Samsung A05",
            Description = "Lorem ipsum dolor sit amet consectetur, adipiscing elit est laoreet non cum, habitant mauris montes posuere.",
            ImageFile = "product-4.png",
            Price = 400000,
            Category = ["Celular"]
        }
    ];

}
