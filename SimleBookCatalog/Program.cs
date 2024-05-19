using Microsoft.EntityFrameworkCore;
using SimleBookCatalog.Components;
using SimpleBookCatalog.Application.Interfaces;
using SimpleBookCatalog.Application.Interfaces;
using SimpleBookCatalog.Infrastructure.Context;
using SimpleBookCatalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Register 
builder.Services.AddDbContextFactory<SimpleBookCatalogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleBookCatalogConnection"));
});

//Register book repository service
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
