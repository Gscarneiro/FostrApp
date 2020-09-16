using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Fostr.Areas.Identity.IdentityHostingStartup))]
namespace Fostr.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.configureservices((context, services) =>
            //{
            //    services.adddbcontext<fostrcontext>(options =>
            //        options.usesqlserver(
            //            context.configuration.getconnectionstring("fostrcontextconnection")));

            //    services.adddefaultidentity<fostruser>(options => options.signin.requireconfirmedaccount = true)
            //        .addentityframeworkstores<fostrcontext>();
            //});
        }
    }
}