using Microsoft.AspNetCore.Mvc;
using loja.models;
using loja.services;
using loja.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 26))));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<FornecedorService>();

var app = builder.Build();

// Configurar as requisições HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/produtos", async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

app.MapGet("/produtos/{id}", async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

app.MapPost("/produtos", async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (int id, Produto produto, ProductService productService, ILogger<Program> logger) =>
{
    if (id != produto.Id)
    {
        logger.LogError("Product ID mismatch.");
        return Results.BadRequest("Product ID mismatch.");
    }

    try
    {
        await productService.UpdateProductAsync(produto);
        return Results.Ok(produto);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (await productService.GetProductByIdAsync(id) == null)
        {
            logger.LogError($"Product with ID {id} not found.");
            return Results.NotFound($"Product with ID {id} not found.");
        }
        else
        {
            throw;
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error updating product.");
        return Results.Problem("Internal server error.", statusCode: 500);
    }
});

app.MapDelete("/produtos/{id}", async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok();
});

// Endpoints para Fornecedores
app.MapGet("/fornecedores", async (FornecedorService fornecedorService) =>
{
    var fornecedores = await fornecedorService.GetAllFornecedoresAsync();
    return Results.Ok(fornecedores);
});

app.MapGet("/fornecedores/{id}", async (int id, FornecedorService fornecedorService) =>
{
    var fornecedor = await fornecedorService.GetFornecedorByIdAsync(id);
    if (fornecedor == null)
    {
        return Results.NotFound($"Fornecedor with ID {id} not found.");
    }
    return Results.Ok(fornecedor);
});

app.MapPost("/fornecedores", async (Fornecedor fornecedor, FornecedorService fornecedorService) =>
{
    await fornecedorService.AddFornecedorAsync(fornecedor);
    return Results.Created($"/fornecedores/{fornecedor.Id}", fornecedor);
});

app.MapPut("/fornecedores/{id}", async (int id, Fornecedor fornecedor, FornecedorService fornecedorService, ILogger<Program> logger) =>
{
    if (id != fornecedor.Id)
    {
        logger.LogError("Fornecedor ID mismatch.");
        return Results.BadRequest("Fornecedor ID mismatch.");
    }

    try
    {
        await fornecedorService.UpdateFornecedorAsync(fornecedor);
        return Results.Ok(fornecedor);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (await fornecedorService.GetFornecedorByIdAsync(id) == null)
        {
            logger.LogError($"Fornecedor with ID {id} not found.");
            return Results.NotFound($"Fornecedor with ID {id} not found.");
        }
        else
        {
            throw;
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error updating fornecedor.");
        return Results.Problem("Internal server error.", statusCode: 500);
    }
});

app.MapDelete("/fornecedores/{id}", async (int id, FornecedorService fornecedorService) =>
{
    await fornecedorService.DeleteFornecedorAsync(id);
    return Results.Ok();
});

app.Run();
