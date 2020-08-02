using FileUploadWebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FileUploadWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "File Upload Web API",
                    Version = "v1",
                    Description = "Sample Web API to upload files.",
                    Contact = new OpenApiContact
                    {
                        Name = "John Doe",
                        Email = string.Empty,
                        Url = new Uri("https://google.com/"),
                    },
                });

                //https://stackoverflow.com/questions/41493130/web-api-how-to-add-a-header-parameter-for-all-api-in-swagger
                //c.OperationFilter<AddRequiredHeaderParameter>();


                //https://stackoverflow.com/questions/57227912/swaggerui-not-adding-apikey-to-header-with-swashbuckle-5-x
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Name = "api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Authorization by x-api-key inside request's header",
                    Scheme = "ApiKeyScheme"
                });

                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                {
                    { key, new List<string>() }
                };
                c.AddSecurityRequirement(requirement);



                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "app-jwt",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });





                //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                //{
                //    Name = "api-key",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Description = "Authorization by api-key inside request's header",
                //    Scheme = "ApiKeyScheme"
                //});

                //https://stackoverflow.com/questions/56535077/how-to-force-swagger-swashbuckle-to-append-an-api-key
                //c.AddSecurityDefinition("api-key", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.ApiKey,
                //    In = ParameterLocation.Header,
                //    Name = "authorization",
                //    Description = "Authorization query string expects API key"
                //});

                //var key0 = new OpenApiSecurityScheme() { Name = "api-key" };
                //var requirement = new OpenApiSecurityRequirement
                //{
                //    { key0, new List<string>() }
                //};
                //c.AddSecurityRequirement(requirement);




                // Bearer token authentication
                //https://stackoverflow.com/questions/58179180/jwt-authentication-and-swagger-with-net-core-3-0
                //                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                //                {
                //                    Name = "Bearer",
                //                    BearerFormat = "JWT",
                //                    Scheme = "bearer",
                //                    Description = "Specify the authorization token.",
                //                    In = ParameterLocation.Header,
                //                    Type = SecuritySchemeType.Http,
                //                };
                //                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                //                // Make sure swagger UI requires a Bearer token specified
                //                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                //                {
                //                    Reference = new OpenApiReference()
                //                    {
                //                        Id = "jwt_auth",
                //                        Type = ReferenceType.SecurityScheme
                //                    }
                //                };
                //                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                //{
                //    {securityScheme, new string[] { }},
                //};
                //                c.AddSecurityRequirement(securityRequirements);




                //    var key = new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "ApiKey"
                //        },
                //        In = ParameterLocation.Header
                //    };
                //    var requirement = new OpenApiSecurityRequirement
                //    {
                //        { key, new List<string>(){"api-key" } }
                //    };
                //    c.AddSecurityRequirement(requirement);

                //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //    {
                //        Name = "Authorization",
                //        In = ParameterLocation.Header,
                //        Type = SecuritySchemeType.Http,
                //        Description = "Authorization by jwt inside request's header",
                //        Scheme = "Bearer"
                //    });

                //    var key1 = new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "Bearer"
                //        },
                //        In = ParameterLocation.Header
                //    };
                //    var requirement1 = new OpenApiSecurityRequirement
                //    {
                //        { key1, new List<string>(){ "app-jwt" } }
                //    };
                //    c.AddSecurityRequirement(requirement1);
            });

            //https://stackoverflow.com/questions/43447688/setting-up-swagger-asp-net-core-using-the-authorization-headers-bearer

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = "https://demo.identityserver.io/";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidIssuer = "https://demo.identityserver.io/",
            //        ValidateIssuerSigningKey = true,
            //        SaveSigninToken = true,
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //    };
            //});
            //https://stackoverflow.com/questions/31464359/how-do-you-create-a-custom-authorizeattribute-in-asp-net-core
            //https://devblogs.microsoft.com/aspnet/jwt-validation-and-authorization-in-asp-net-core/
            //https://www.mithunvp.com/write-custom-asp-net-core-middleware-web-api/
            //https://www.codeproject.com/Articles/1228892/Securing-ASP-NET-CORE-Web-API-using-Custom-API-Key
            //https://github.com/capcom923/MySwashBuckleSwaggerWithJwtToken
            //https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "File Upload API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = "swagger/ui";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

//https://www.coderjony.com/blogs/adding-swagger-to-aspnet-core-31-web-api/
