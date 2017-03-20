using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(DashboardService.AppStartup))] 
namespace DashboardService
{
    class AppStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            const string rootFolder = @".\Web";
            var fileSystem = new PhysicalFileSystem(rootFolder);

            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };
            options.StaticFileOptions.FileSystem = fileSystem;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };

            app.UseFileServer(options);

        }
    }
}
