// using Microsoft.EntityFrameworkCore;
// using loja.data;
// using loja.models;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// //Configurar a conexão com o BD:
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<LojaDbContext>(options=>options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// //Endpoint para consultar os clientes no BD:
// app.MapGet("/clientes", async (LojaDbContext dbContext) =>
// {
//     var clientes = await dbContext.Clientes.ToListAsync();
//     return Results.Ok(clientes);
// });

// //Endpoint para localizar um cliente a partir de sua ID no BD:
// app.MapGet("/clientes/{id}", async (int id, LojaDbContext dbContext) =>
// {
//     var cliente = await dbContext.Clientes.FindAsync(id);
//     if(cliente == null){
//         return Results.NotFound($"Cliente with ID {id} not found");
//     }
//     return Results.Ok(cliente);
// });

// //Endpoint para atualizar um cliente existente:
// app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updateCliente) =>
// {
//     //Verifica se o cliente existe na base, conforme o id informado
//     //Se o cliente existir na base, será retornado para dentro do objeto existingCliente
//     var existingCliente = await dbContext.Clientes.FindAsync(id);
//     if(existingCliente == null){
//         return Results.NotFound($"Cliente with ID {id} not found");
//     }

//     //Atualiza os dados do existingProduto:
//     existingCliente.Nome = updateCliente.Nome;
//     existingCliente.Cpf = updateCliente.Cpf;
//     existingCliente.Email = updateCliente.Email;

//     //Salva no banco de dados:
//     await dbContext.SaveChangesAsync();

//     //Retorna os dados para o cliente que invocou o endpoint:
//     return Results.Ok(existingCliente);

// });



// app.MapPost("/createcliente", async (LojaDbContext dbContext, Cliente newCliente) =>
// {
//     dbContext.Clientes.Add(newCliente);
//     await dbContext.SaveChangesAsync();
//     return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
// });

// //Endpoint para atualizar um produto existente:
// app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updateProduto) =>
// {
//     //Verifica se o produto existe na base, cdonforme o id informado
//     //Se o produto existir na base, será retornado para dentro do objeto existingProduto
//     var existingProduto = await dbContext.Produtos.FindAsync(id);
//     if(existingProduto == null){
//         return Results.NotFound($"Produto with ID {id} not found");
//     }

//     //Atualiza os dados do existingProduto:
//     existingProduto.Nome = updateProduto.Nome;
//     existingProduto.Preco = updateProduto.Preco;
//     existingProduto.Fornecedor = updateProduto.Fornecedor;

//     //Salva no banco de dados:
//     await dbContext.SaveChangesAsync();

//     //Retorna os dados para o cliente que invocou o endpoint:
//     return Results.Ok(existingProduto);

// });

// app.MapGet("/produtos/{id}", async (int id, LojaDbContext dbContext) =>
// {
//     var produto = await dbContext.Produtos.FindAsync(id);
//     if(produto == null){
//         return Results.NotFound($"Produto with ID {id} not found");
//     }
//     return Results.Ok(produto);
// });

// app.MapGet("/produtos", async (LojaDbContext dbContext) =>
// {
//     var produtos = await dbContext.Produtos.ToListAsync();
//     return Results.Ok(produtos);
// });

// app.MapPost("/createproduto", async (LojaDbContext dbContext, Produto newProduto) =>
// {
//     dbContext.Produtos.Add(newProduto);
//     await dbContext.SaveChangesAsync(); 
//     return Results.Created($"/createproduto/{newProduto.Id}", newProduto);    

// });

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
