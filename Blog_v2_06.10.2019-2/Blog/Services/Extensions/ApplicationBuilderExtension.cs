using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Services.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseRemoveImport(this IApplicationBuilder app, IHostingEnvironment env)
        {
            var path = env.WebRootPath + "/assets/compiled";

            foreach (string file in Directory.EnumerateFiles(path, "*.js"))
            {
                var contents = File.ReadAllLines(file);
                var allExceptImport = contents.Where(l => !l.StartsWith("import"));
                File.WriteAllLines(file, allExceptImport);
            }

            

            return app;
        }
    }

}
