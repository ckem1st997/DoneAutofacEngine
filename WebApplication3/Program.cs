using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using WebApplication3;
using WebApplication3.Infrastructure;
using WebApplication3.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




///
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Call ConfigureContainer on the Host sub property 
//
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // Declare your services with proper lifetime

    builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
    builder.RegisterType<BaseEngine>().As<IEngine>().SingleInstance();

    // truyền vào tên service và giá trịc cần truyền vào trong hàm khởi tạo
    builder.RegisterGeneric(typeof(TTT<>)).Named("Master", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => (object)"Master"))).InstancePerLifetimeScope();
    builder.RegisterGeneric(typeof(TTT<>)).Named("wh", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => (object)"wh"))).InstancePerLifetimeScope();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// a
app.ConfigureRequestPipeline();
app.Run();
