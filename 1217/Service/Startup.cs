using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            /* 파일 용량이 클경우 설정 */
            services.Configure<FormOptions>(
                //람다식 : option(= 파라미터를 의미)
                options =>
                    {
                        //options.MultipartBodyLengthLimit = 80000000;  //Multipart 전송 시 설정 == html 
                        options.KeyLengthLimit = int.MaxValue;          // key : max
                        options.ValueLengthLimit = int.MaxValue;        // value : max
                        //options.MultipartHeadersLengthLimit = int.MaxValue;
                    });
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }           
            app.UseStaticFiles(); //정적 파일 사용
            app.UseMvc();
        }
    }
}
